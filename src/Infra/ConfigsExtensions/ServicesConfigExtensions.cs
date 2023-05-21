using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Contracts.Services.WebSockets;
using Domain.Contracts.WebSockets;
using Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.ConfigsExtensions;

public static class ServicesConfigExtensions
{
    public static void AddDomainServices(this IServiceCollection services)
    {
        services.AddTransient<IBtcUsdOrderBookService, BtcUsdOrderBookService>()
            .AddTransient<IEthUsdOrderBookService, EthUsdOrderBookService>()
            .AddScoped<IRequestOrderService, RequestOrderService>()
            .AddScoped<IRepositoryFabric, RepositoryFabric>();
    }
}