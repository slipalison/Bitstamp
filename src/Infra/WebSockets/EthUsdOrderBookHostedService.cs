using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infra.WebSockets
{
    public class EthUsdOrderBookHostedService : BackgroundService
    {
        private readonly IEthUsdOrderBookService _ethUsdOrderBookService;

        public EthUsdOrderBookHostedService(ILogger<EthUsdOrderBookHostedService> logger, IEthUsdOrderBookService ethUsdOrderBookService)
        {
            _ethUsdOrderBookService = ethUsdOrderBookService;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _ethUsdOrderBookService.ConnectAndListen();
        }
    }

  
}
