using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infra.WebSockets.HostedServices
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
            try
            {
                return _btcUsdOrderBookService.ConnectAndListen();
            }
            catch (Exception e)
            {
                throw;
            }
           
        }
    }


}
