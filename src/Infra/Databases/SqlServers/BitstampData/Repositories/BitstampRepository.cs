using Domain.Contracts.Repositories;
using Domain.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Responses;

namespace Infra.Databases.SqlServers.BitstampData.Repositories;



public abstract class BitstampRepository<TEntity> : IBitstampRepository<TEntity> where TEntity : BaseItemBook<TEntity>, new()
{
    private readonly BitstampContext _context;
    private readonly DbSet<TEntity> _dbSet;
    protected BitstampRepository(BitstampContext BitstampContext)
    {
        _context = BitstampContext;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task InsertOrUpdateRangeAsync(List<TEntity> entities, CancellationToken cancellationToken)
    {

        try
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            var timestamp = entities.First().Timestamp;
            var microtimestamp = entities.First().Microtimestamp;

            for (int i = 0; i < entities.Count; i++)
            {
                TEntity? e = entities[i];
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
        catch (Exception e)
        {
            // TODO: Validar a necessidade 

        }

    }

    public async Task<Metric> GetMetrics(CancellationToken cancellationToken)
    {

        var entityType = _context.Model.FindEntityType(typeof(TEntity));
        var tableName = entityType.GetTableName();

        try
        {


            var cteQuery =  _context.Metrics
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
            .AsNoTracking().AsEnumerable()
            .FirstOrDefault();

            return cteQuery;
        }
        catch (Exception e)
        {

            throw;
        }

    }

    //public Task<List<TEntity>> GetAll(CancellationToken cancellationToken = default)
    //{
    //    return _context.ToDos.ToListAsync(cancellationToken);
    //}

    //public async Task<TEntity> Create(TEntity accountPlanEntity,
    //    CancellationToken cancellationToken = default)
    //{
    //    var ent = await _context.AddAsync(accountPlanEntity, cancellationToken);
    //    await _context.SaveChangesAsync(cancellationToken);

    //    return ent.Entity;
    //}

    //public async Task<Result<TEntity>> Update(TEntity entity, CancellationToken cancellationToken = default)
    //{

    //    await _context.SaveChangesAsync(cancellationToken);

    //    return Result.Ok();
    //}

    public async Task<Result> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        //var entitie =
        //    await _context.ToDos.FindAsync(new object?[] { id },
        //        cancellationToken: cancellationToken);

        //if (entitie == null) return Result.Fail("404", "Tarefa não encontrada");

        //_context.ToDos.Remove(entitie);
        //await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}

