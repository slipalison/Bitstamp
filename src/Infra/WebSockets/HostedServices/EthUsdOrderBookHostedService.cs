using Domain.Contracts.WebSockets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infra.WebSockets.HostedServices;

public class EthUsdOrderBookHostedService : BackgroundService
{
    private readonly ILogger<EthUsdOrderBookHostedService> _logger;
    private readonly IEthUsdOrderBookService _ethUsdOrderBookService;

    public EthUsdOrderBookHostedService(ILogger<EthUsdOrderBookHostedService> logger, IEthUsdOrderBookService ethUsdOrderBookService)
    {
        _logger = logger;
        _ethUsdOrderBookService = ethUsdOrderBookService;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            return _ethUsdOrderBookService.ConnectAndListen();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro BtcUsdOrderBookHostedService");
            return Task.CompletedTask;
        }
    }
}
