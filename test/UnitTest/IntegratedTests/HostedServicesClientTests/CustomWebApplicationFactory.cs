using Domain.Contracts.Repositories;
using Domain.Models.AggregationMetrics;
using Domain.Models.AggregationOrder;
using Infra.Databases.SqlServers.BitstampData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;

namespace UnitTest.IntegratedTests.HostedServicesClientTests;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            //var IBtcAskRepository = Substitute.For<IBtcAskRepository>();
            //var IBtcBidRepository = Substitute.For<IBtcBidRepository>();
            //var IEthAskRepository = Substitute.For<IEthAskRepository>();
            //var IEthBidRepository = Substitute.For<IEthBidRepository>();

            //ConfigRepo(IBtcAskRepository, IBtcBidRepository, IEthAskRepository, IEthBidRepository);

            //services.AddScoped(x => IBtcAskRepository);
            //services.AddScoped(x => IBtcBidRepository);
            //services.AddScoped(x => IEthAskRepository);
            //services.AddScoped(x => IEthBidRepository);
            MockInMemoryDatabase(services);
        });

        builder.UseEnvironment("Test");
    }

    private static void MockInMemoryDatabase(IServiceCollection services)
    {
        var dbContextDescriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbContextOptions<BitstampContext>));

        if (dbContextDescriptor != null) services.Remove(dbContextDescriptor);

        var dbConnectionDescriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                 typeof(DbConnection));

        // if (dbConnectionDescriptor != null) services.Remove(dbConnectionDescriptor);

        services.AddSingleton<DbConnection>(container =>
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            return connection;
        });

        services.AddDbContextPool<BitstampContext>((container, options) =>
        {
            var connection = container.GetRequiredService<DbConnection>();
            options.UseSqlite(connection);
        });
    }
}