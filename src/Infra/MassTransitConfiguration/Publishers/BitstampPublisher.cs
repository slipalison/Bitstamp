using Domain.Contracts.Services;
using Domain.Services;
using Microsoft.Extensions.Logging;

namespace Infra.MassTransitConfiguration.Publishers;

public class BitstampPublisher<T> : AbstractPublisher<T>, IBitstampPublisher<T> where T : class, IEventBase
{
    public BitstampPublisher(ILogger<BitstampPublisher<T>> logger, IBitstampBus bus,
        ICorrelationContextService correlationContextService) : base(logger, bus, correlationContextService)
    {
    }
}