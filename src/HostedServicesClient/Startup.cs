using System.Net.WebSockets;
using Infra;
using Infra.Databases.SqlServers.BitstampData.Extensions;
using Infra.Middlewares;
using Infra.WebSockets.HostedServices;
using HostedServicesClient.WebSockets;

namespace HostedServicesClient;

public class Startup : BaseStartup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) : base(configuration)
    {
        _configuration = configuration;
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        //.AddJsonOptions(options =>
        //{
        //    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        //    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        //});

        services.AddCors(policyBuilder =>
            policyBuilder.AddDefaultPolicy(policy =>
                policy.AllowAnyHeader().AllowAnyHeader())
        );
        //    .AddResponseCompression(options =>
        //{
        //    options.Providers.Add<GzipCompressionProvider>();
        //    options.EnableForHttps = true;

        //})
        //.AddEndpointsApiExplorer()
        //.AddSwaggerGen(c =>
        //{
        //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Base API", Version = "v1" });
        //    c.UseInlineDefinitionsForEnums();
        //})
        //.AddVersionedApiExplorer(options =>
        //{
        //    options.GroupNameFormat = "'v'V";
        //    options.SubstituteApiVersionInUrl = true;
        //}).AddApiVersioning(options => { options.ReportApiVersions = true; });

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
        // app.UseSwagger();
        //app.UseSwaggerUI();
        app//.UseHttpsRedirection()
            .UseStaticFiles()
            .UseRouting()
            //.UseResponseCompression()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
                endpoints.MapHub<WebSocketHub>("/websocket");
            });


        //  app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
        //.UseAuthorization()
        //.HealthCheckConfiguration();
    }
}