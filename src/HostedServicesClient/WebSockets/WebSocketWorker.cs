using Domain.Contracts.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace HostedServicesClient.WebSockets;

public class WebSocketWorker : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<WebSocketWorker> _logger;

    public WebSocketWorker(IServiceProvider services, ILogger<WebSocketWorker> logger)
    {
        _services = services;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _services.CreateScope();
        var repoBtcBid = scope.ServiceProvider.GetRequiredService<IBtcBidRepository>();
        var repoBtcAsk = scope.ServiceProvider.GetRequiredService<IBtcAskRepository>();
        var repoEthBid = scope.ServiceProvider.GetRequiredService<IEthBidRepository>();
        var repoEthAsk = scope.ServiceProvider.GetRequiredService<IEthAskRepository>();
        var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<WebSocketHub>>();

        while (!stoppingToken.IsCancellationRequested)
        {
            var btcBid = await repoBtcBid.GetMetrics(stoppingToken);
            var btcAsk = await repoBtcAsk.GetMetrics(stoppingToken);
            var ethBid = await repoEthBid.GetMetrics(stoppingToken);
            var ethAsk = await repoEthAsk.GetMetrics(stoppingToken);

            var produtos = new
            {
                BtcBid = btcBid,
                BtcAsk = btcAsk,
                EthBid = ethBid,
                EthAsk = ethAsk,
            };
            _logger.LogInformation("{@metrics}", produtos);

            await hubContext.Clients.All.SendAsync("AtualizarProdutos", produtos);

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}