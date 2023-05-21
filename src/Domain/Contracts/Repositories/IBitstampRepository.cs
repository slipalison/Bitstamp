using Domain.Models.AggregationMetrics;

namespace Domain.Contracts.Repositories;

public interface IBitstampRepository
{
    Task InsertOrUpdateRangeAsync(List<IEntity> entities, CancellationToken cancellationToken);
    Task<Metric?> GetMetrics(CancellationToken cancellationToken);
    Task<List<OrderItem>> ListItensBookToOrder(decimal amout, CancellationToken cancellationToken);
}
