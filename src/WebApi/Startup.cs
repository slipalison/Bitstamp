using System.Text.Json;
using System.Text.Json.Serialization;
using Infra;
using Infra.ConfigsExtensions;
using Infra.Databases.SqlServers.BitstampData.Extensions;
using Infra.Middlewares;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;

namespace WebApi;

public class Startup : BaseStartup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration) : base(configuration)
    {
        _configuration = configuration;
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        services.AddCors(policyBuilder =>
            policyBuilder.AddDefaultPolicy(policy =>
                policy.AllowAnyHeader().AllowAnyHeader())
        );

        services.AddResponseCompression(options =>
        {
            options.Providers.Add<GzipCompressionProvider>();
            options.EnableForHttps = true;
        });

        services.AddEndpointsApiExplorer()
            .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Base API", Version = "v1" });
                    c.UseInlineDefinitionsForEnums();
                }
            );

        base.ConfigureServices(services);
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
        app.UseSwagger();
        app.UseSwaggerUI();
        app//.UseHttpsRedirection()
            .UseRouting()
            .UseResponseCompression()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            })
            .UseAuthorization()
            .HealthCheckConfiguration();
    }
}