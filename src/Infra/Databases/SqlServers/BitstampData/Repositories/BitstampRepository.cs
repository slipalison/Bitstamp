using Domain.Contracts.Repositories;
using Domain.Models;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Polly;
using Responses;


namespace Infra.Databases.SqlServers.BitstampData.Repositories;


public class EthAskRepository : BitstampRepository<EthAsk>, IEthAskRepository
{
    public EthAskRepository(BitstampContext BitstampContext) : base(BitstampContext)
    {
    }
}

public class EthBidRepository : BitstampRepository<EthBid>, IEthBidRepository
{
    public EthBidRepository(BitstampContext BitstampContext) : base(BitstampContext)
    {
    }
}

public interface IEthAskRepository: IBitstampRepository<EthAsk>
{
}
public interface IEthBidRepository : IBitstampRepository<EthBid>
{
}
public abstract class BitstampRepository<TEntity> : IBitstampRepository<TEntity> where TEntity : class
{
    private readonly BitstampContext _context;
    private readonly DbSet<TEntity> _dbSet;
    protected BitstampRepository(BitstampContext BitstampContext)
    {
        _context = BitstampContext;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task InsertRange(List<TEntity> entities, CancellationToken cancellationToken)
    {
        using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        await _context.BulkInsertAsync(entities, cancellationToken: cancellationToken);
        await transaction.CommitAsync(cancellationToken);

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
