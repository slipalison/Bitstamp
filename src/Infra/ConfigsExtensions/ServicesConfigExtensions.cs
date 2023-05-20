using Domain.Contracts.WebSockets;
using Infra.WebSockets;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.ConfigsExtensions;

public static class ServicesConfigExtensions
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddTransient<IBtcUsdOrderBookService, BtcUsdOrderBookService>();
        services.AddTransient<IEthUsdOrderBookService, EthUsdOrderBookService>();
    }
}