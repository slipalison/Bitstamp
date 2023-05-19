using Domain.Services;
using Flurl.Http;
using Infra.MassTransitConfiguration;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using WebApi;

namespace UnitTest.IntegratedTests;

public abstract class AbstractIntegratedTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    protected readonly IFlurlClient Client;
    protected readonly CustomWebApplicationFactory<Program> Factory;

    protected AbstractIntegratedTest(CustomWebApplicationFactory<Program> factory)
    {
        Factory = factory;
        var client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        CorrelationService = Factory.Services.GetRequiredService<ICorrelationContextService>();
        BusCustom = factory.Services.GetRequiredService<IBitstampBus>();
        Client = new FlurlClient(client);
    }

    protected IBitstampBus BusCustom { get; set; }

    protected ICorrelationContextService CorrelationService { get; set; }


    protected IFlurlRequest CallHttp(string uri)
    {
        return uri.WithClient(Client).WithHeader("X-Correlation-ID", CorrelationService.GetCorrelationId().ToString()).AllowAnyHttpStatus();
    }
}