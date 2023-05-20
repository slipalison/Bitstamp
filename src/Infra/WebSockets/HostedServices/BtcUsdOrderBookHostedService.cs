using Domain.Contracts.WebSockets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infra.WebSockets.HostedServices;

public class BtcUsdOrderBookHostedService : BackgroundService
{
    private readonly ILogger<BtcUsdOrderBookHostedService> _logger;
    private readonly IBtcUsdOrderBookService _btcUsdOrderBookService;

    public BtcUsdOrderBookHostedService(ILogger<BtcUsdOrderBookHostedService> logger, IBtcUsdOrderBookService btcUsdOrderBookService)
    {
        _logger = logger;
        _btcUsdOrderBookService = btcUsdOrderBookService;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            return _btcUsdOrderBookService.ConnectAndListen();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Erro BtcUsdOrderBookHostedService");
            return Task.CompletedTask;
        }
    }
}
