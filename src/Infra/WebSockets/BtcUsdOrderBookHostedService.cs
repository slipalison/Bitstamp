using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infra.WebSockets
{
    public class BtcUsdOrderBookHostedService : BackgroundService
    {
        private readonly IBtcUsdOrderBookService _btcUsdOrderBookService;

        public BtcUsdOrderBookHostedService(ILogger<BtcUsdOrderBookHostedService> logger, IBtcUsdOrderBookService btcUsdOrderBookService)
        {
            _btcUsdOrderBookService = btcUsdOrderBookService;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _btcUsdOrderBookService.ConnectAndListen();
        }
    }

  
}
