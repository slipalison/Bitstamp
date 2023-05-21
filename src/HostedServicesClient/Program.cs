using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using System.Diagnostics.CodeAnalysis;

namespace HostedServicesClient;

[ExcludeFromCodeCoverage]
[SuppressMessage("Major Code Smell", "S1118:Utility classes should not have public constructors", Justification = "Classe Main")]
public class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).UseDefaultServiceProvider(options => options.ValidateScopes = false)
            .Build().RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
            .UseSerilog((context, configuration) =>
            {
                configuration
                    .MinimumLevel.Verbose()
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Debug()
                    .WriteTo.Console(new JsonFormatter())
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(context.Configuration["Elastic"]!))
                    {
                        AutoRegisterTemplate = true,
                        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                        IndexFormat = "baseapi-{0:yyyy.MM}"
                    })
                    .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!)
                    .ReadFrom.Configuration(context.Configuration);
            });
    }
}