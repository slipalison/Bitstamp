namespace Domain.Contracts.Repositories;

public interface IEntity {
     Guid Id { get; set; } 
    DateTimeOffset InsertAt { get; set; }
    long Timestamp { get; set; }
    long Microtimestamp { get; set; }
    decimal Price { get; set; }
    decimal Amount { get; set; }

    IEntity UpdateTimeStamp(long timestamp, long microtimestamp);
}