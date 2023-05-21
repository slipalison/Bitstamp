using Domain.Contracts.Repositories;
using Infra.Databases.SqlServers.BitstampData;
using Infra.Databases.SqlServers.BitstampData.Repositories;
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
            var serviceProvider = services.BuildServiceProvider();
            var myService = serviceProvider.GetService<BitstampContext>();

            var IBtcAskRepository = Substitute.ForPartsOf<BtcAskRepository>(myService);
            var IBtcBidRepository = Substitute.ForPartsOf<BtcBidRepository>(myService);
            var IEthAskRepository = Substitute.ForPartsOf<EthAskRepository>(myService);
            var IEthBidRepository = Substitute.ForPartsOf<EthBidRepository>(myService);

            services.AddScoped<IBtcAskRepository>(x => IBtcAskRepository);
            services.AddScoped<IBtcBidRepository>(x => IBtcBidRepository);
            services.AddScoped<IEthAskRepository>(x => IEthAskRepository);
            services.AddScoped<IEthBidRepository>(x => IEthBidRepository);
            MockInMemoryDatabase(services);
        });

        builder.UseEnvironment("Test");
    }

    private static void MockInMemoryDatabase(IServiceCollection services)
    {
        var dbContextDescriptor = services.SingleOrDefault(
            d => d.ServiceType == typeof(DbContextOptions<BitstampContext>));

        if (dbContextDescriptor != null) services.Remove(dbContextDescriptor);

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