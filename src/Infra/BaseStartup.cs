using Domain.Contracts.Services;
using Domain.Services;
using Infra.ConfigsExtensions;
using Infra.Databases.SqlServers.BitstampData.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;

public abstract class BaseStartup
{
    private readonly IConfiguration _configuration;

    public BaseStartup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<ICorrelationContextService, CorrelationContextService>();

        services
            .HealthChecksConfiguration(_configuration)
            .AddDbContext(_configuration)
            .AddDomainServices();
    }
}