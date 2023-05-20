using Responses;

namespace Domain.Contracts.Repositories;

public interface IBitstampRepository
{
    //Task<List<ToDoItemEntity>> GetAll(CancellationToken cancellationToken = default);
    //Task<ToDoItemEntity> Create(ToDoItemEntity accountPlanEntity, CancellationToken cancellationToken = default);
    //Task<Result<ToDoItemEntity>> Update(ToDoItemEntity entity, CancellationToken cancellationToken = default);
    Task<Result> Delete(Guid id, CancellationToken cancellationToken = default);
}