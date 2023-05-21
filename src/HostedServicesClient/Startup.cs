using HostedServicesClient.WebSockets;
using Infra;
using Infra.Databases.SqlServers.BitstampData.Extensions;
using Infra.Middlewares;
using Infra.WebSockets.HostedServices;
using System.Net.WebSockets;

namespace HostedServicesClient;

public class Startup : BaseStartup
{
    public Startup(IConfiguration configuration) : base(configuration)
    {
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddCors(policyBuilder =>
            policyBuilder.AddDefaultPolicy(policy =>
                policy.AllowAnyHeader().AllowAnyHeader())
        );

        base.ConfigureServices(services);

        services.AddSignalR();

        services.AddTransient<ClientWebSocket>();
        services.AddHostedService<BtcUsdOrderBookHostedService>();
        services.AddHostedService<EthUsdOrderBookHostedService>();
        services.AddHostedService<WebSocketWorker>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader())
            .UseMiddleware<CorrelationMiddleware>()
            .UseMiddleware<LoggingMiddleware>();

        // TODO: Isso é só para testes e apresentação, hambiente produtivo isso é contra indicado
        app.ExecuteMigartions();

        app
            .UseStaticFiles()
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
                endpoints.MapHub<WebSocketHub>("/websocket");
            });
    }
}