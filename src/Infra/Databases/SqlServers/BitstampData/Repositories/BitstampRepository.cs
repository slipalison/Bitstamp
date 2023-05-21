using Domain.Contracts.Repositories;
using Domain.Models.AggregationMetrics;
using Domain.Models.AggregationOrder;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infra.Databases.SqlServers.BitstampData.Repositories;

public abstract class BitstampRepository<TEntity> : IBitstampRepository where TEntity : class, IEntity, new()
{
    private readonly BitstampContext _context;
    private readonly DbSet<TEntity> _dbSet;
    protected BitstampRepository(BitstampContext BitstampContext)
    {
        _context = BitstampContext;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task InsertOrUpdateRangeAsync(List<IEntity> entities, CancellationToken cancellationToken)
    {

        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        var timestamp = entities.First().Timestamp;
        var microtimestamp = entities.First().Microtimestamp;

        for (int i = 0; i < entities.Count; i++)
        {
            TEntity? e = (TEntity?)entities[i];
            var en = await _dbSet.FirstOrDefaultAsync(a => a.Amount == e.Amount && a.Price == e.Price, cancellationToken);
            if (en != null)
            {
                var up = en.UpdateTimeStamp(timestamp, microtimestamp);
                _context.Update(up);
                entities.RemoveAll(a => up.Amount == a.Amount && up.Price == a.Price);
                i--;
            }
        }
        await _context.BulkInsertAsync(entities, cancellationToken: cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

    }

    public async Task<Metric?> GetMetrics(CancellationToken cancellationToken)
    {
        var tableName = _context.Model.FindEntityType(typeof(TEntity))!.GetTableName();

        var cteQuery = await _context.Metrics
            .FromSqlRaw(
            $@"WITH cte AS (SELECT TOP 100 Id,
                                        InsertAt,
                                        [Timestamp],
                                        Microtimestamp,
                                        Price,
                                        Amount
                        FROM Bitstamp.dbo.{tableName} WITH (NOLOCK)
                        ORDER BY InsertAt DESC)
            SELECT top 1 MIN(Price)         AS minPrice,
                    MAX(Price)         AS maxPrice,
                    AVG(Amount)        AS mediaAmount,
                    AVG(Price)         AS mediaPrice,
                    (SELECT AVG(Price) AS mediaPrice5
                    FROM Bitstamp.dbo.{tableName} WITH (NOLOCK)
                        WHERE InsertAt >= (SELECT TOP 1 DATEADD(second, -5, MAX(InsertAt)) FROM cte)) AS mediaPrice5
            FROM cte
            OPTION (RECOMPILE)")
        .AsNoTracking()
        .ToListAsync(cancellationToken);

        return cteQuery.FirstOrDefault();
    }

    public async Task<List<OrderItem>> ListItensBookToOrder(decimal amout, CancellationToken cancellationToken)
    {
        var exists = await _dbSet.Where(x => x.Amount == amout).Select(x => new OrderItem(x.Amount, x.Price, x.InsertAt))
                .ToListAsync(cancellationToken);

        if (exists.Any())
            return exists;
        
        var tableName = _context.Model.FindEntityType(typeof(TEntity))!.GetTableName();
        var cteQuery = await _context.OrderItems
        .FromSqlRaw(
        $@"SELECT Amount, Price, InsertAt
            FROM (
                SELECT InsertAt, Price, Amount, SUM(Amount) OVER (ORDER BY Amount) AS running_sum
                    FROM Bitstamp.dbo.{tableName} WITH (NOLOCK)
            ) AS subquery
            WHERE Amount = {amout} or running_sum <= {amout}
            order by Price asc, InsertAt desc
            OPTION (RECOMPILE)").AsNoTracking().ToListAsync(cancellationToken);

        return cteQuery;

    }

    public async Task SaveOrder(Order order, CancellationToken cancellationToken)
    {
        await _context.Orders.AddAsync(order, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}

