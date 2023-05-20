using Domain.Commands;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;

namespace Infra.WebSockets
{
    public class EthUsdOrderBookService : BitstampWebSocket<EthUsdOrderBookService>, IEthUsdOrderBookService
    {
        public EthUsdOrderBookService(ILogger<EthUsdOrderBookService> logger, ClientWebSocket clientWebSocket)
            : base(logger, clientWebSocket, CurrencyType.ethusd, "wss://ws.bitstamp.net")
        {
        }

        public override async Task ExecuteOrderBook(OrderBook? message)
        {
            _logger.LogInformation("{@resultJson}", message);
        }
    }

  
}
