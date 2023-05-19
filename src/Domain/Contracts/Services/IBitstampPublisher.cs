namespace Domain.Contracts.Services;

public interface IBitstampPublisher<in T>: IEventPublisher<T> where T : class, IEventBase
{
    
}