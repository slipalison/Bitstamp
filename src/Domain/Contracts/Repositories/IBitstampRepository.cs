using Domain.Models.AggregationMetrics;
using Responses;
using System.Security.Principal;

namespace Domain.Contracts.Repositories;

public interface IBitstampRepository//<TEntity> where TEntity : class
{
    Task InsertOrUpdateRangeAsync(List<IEntity> entities, CancellationToken cancellationToken);

    Task<Metric?> GetMetrics(CancellationToken cancellationToken);
}

public interface IEntity {
     Guid Id { get; set; } 
    DateTimeOffset InsertAt { get; set; }
    long Timestamp { get; set; }
    long Microtimestamp { get; set; }
    decimal Price { get; set; }
    decimal Amount { get; set; }

    IEntity UpdateTimeStamp(long timestamp, long microtimestamp);
}