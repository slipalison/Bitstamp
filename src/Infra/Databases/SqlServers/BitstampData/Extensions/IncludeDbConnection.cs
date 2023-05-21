using Domain.Contracts.Repositories;
using Infra.Databases.SqlServers.BitstampData.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Infra.Databases.SqlServers.BitstampData.Extensions;

public static class IncludeDbConnection
{
    public static IServiceCollection AddDbContext(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection
            .AddDbContext<BitstampContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServer")), ServiceLifetime.Transient)
            .AddRepositories();

        return serviceCollection;
    }

    private static void AddRepositories(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddScoped<IEthAskRepository, EthAskRepository>();
        serviceCollection.TryAddScoped<IEthBidRepository, EthBidRepository>();
        serviceCollection.TryAddScoped<IBtcAskRepository, BtcAskRepository>();
        serviceCollection.TryAddScoped<IBtcBidRepository, BtcBidRepository>();
    }

    public static void ExecuteMigartions(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<BitstampContext>();
        dbContext.Database.EnsureCreated();
    }
}