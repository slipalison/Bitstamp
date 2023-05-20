using Domain.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using Responses;


namespace Infra.Databases.SqlServers.BitstampData.Repositories;

public abstract class BitstampRepository<TEntity> : IBitstampRepository<TEntity> where TEntity : class
{
    private readonly BitstampContext _context;
    private readonly DbSet<TEntity> _dbSet;
    public BitstampRepository(BitstampContext BitstampContext)
    {
        _context = BitstampContext;
        _dbSet= _context.Set<TEntity>();
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
