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

namespace UnitTest.IntegratedTests.WebApi;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var IBtcAskRepository = Substitute.For<IBtcAskRepository>();
            var IBtcBidRepository = Substitute.For<IBtcBidRepository>();
            var IEthAskRepository = Substitute.For<IEthAskRepository>();
            var IEthBidRepository = Substitute.For<IEthBidRepository>();

            ConfigRepo(IBtcAskRepository, IBtcBidRepository, IEthAskRepository, IEthBidRepository);

            services.AddScoped(x => IBtcAskRepository);
            services.AddScoped(x => IBtcBidRepository);
            services.AddScoped(x => IEthAskRepository);
            services.AddScoped(x => IEthBidRepository);
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

        //var dbConnectionDescriptor = services.SingleOrDefault(
        //    d => d.ServiceType ==typeof(DbConnection));
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

    private static void ConfigRepo(params IBitstampRepository[] bitstampRepository)
    {

        foreach (var bitstamp in bitstampRepository)
        {
            bitstamp.GetMetrics(Arg.Any<CancellationToken>()).ReturnsForAnyArgs(Task.FromResult(new Metric(10, 11, 5, 6, 12)));

            bitstamp.ListItensBookToOrder(Arg.Any<decimal>(), Arg.Any<CancellationToken>())
                .ReturnsForAnyArgs(Task.FromResult(new List<OrderItem> {
                            new OrderItem( 1,  10 , DateTime.UtcNow),
                            new OrderItem( 1,  10 , DateTime.UtcNow),
                            new OrderItem( 1,  10 , DateTime.UtcNow),
                            new OrderItem( 1,  10 , DateTime.UtcNow)
                }));

            bitstamp.InsertOrUpdateRangeAsync(Arg.Any<List<IEntity>>(), Arg.Any<CancellationToken>()).ReturnsForAnyArgs(Task.CompletedTask);
            bitstamp.SaveOrder(Arg.Any<Order>(), Arg.Any<CancellationToken>()).ReturnsForAnyArgs(Task.CompletedTask);

        }
    }
}