using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Elasticsearch;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

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
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
            .UseSerilog((context, configuration) =>
            {
                NewMethod(context, configuration)
                                    .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!)
                                    .ReadFrom.Configuration(context.Configuration);
            });
    }

    private static LoggerConfiguration NewMethod(HostBuilderContext context, LoggerConfiguration configuration)
    {

        var elk = new ElasticsearchSinkOptions()
        {
            AutoRegisterTemplate = true,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
            IndexFormat = "webapi-{0:yyyy.MM}"
        };


        if (!string.IsNullOrEmpty(context.Configuration["Elastic"]))
            elk = new ElasticsearchSinkOptions(new Uri(context.Configuration["Elastic"]!))
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                IndexFormat = "webapi-{0:yyyy.MM}"
            };


        return configuration
                    .MinimumLevel.Verbose()
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Debug()
                    .WriteTo.Console(new JsonFormatter())
                    .WriteTo.Elasticsearch(elk);

    }
}