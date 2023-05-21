using Domain.Services;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.IntegratedTests.HostedServicesClientTests;

public abstract class AbstractIntegratedTest<TProgram> : IClassFixture<CustomWebApplicationFactory<TProgram>> where TProgram : class
{
    protected readonly IFlurlClient Client;
    protected readonly CustomWebApplicationFactory<TProgram> Factory;

    protected AbstractIntegratedTest(CustomWebApplicationFactory<TProgram> factory)
    {
        Factory = factory;
        var client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        CorrelationService = Factory.Services.GetRequiredService<ICorrelationContextService>();
        Client = new FlurlClient(client);
    }

    protected ICorrelationContextService CorrelationService { get; set; }

    protected IFlurlRequest CallHttp(string uri)
    {
        return uri.WithClient(Client).WithHeader("X-Correlation-ID", CorrelationService.GetCorrelationId().ToString()).AllowAnyHttpStatus();
    }
}