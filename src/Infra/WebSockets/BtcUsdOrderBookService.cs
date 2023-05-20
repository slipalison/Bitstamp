using Domain.Commands;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;

namespace Infra.WebSockets
{
    public class BtcUsdOrderBookService : BitstampWebSocket<BtcUsdOrderBookService>, IBtcUsdOrderBookService
    {
        public BtcUsdOrderBookService(ILogger<BtcUsdOrderBookService> logger, ClientWebSocket clientWebSocket)
            : base(logger, clientWebSocket, CurrencyType.btcusd, "wss://ws.bitstamp.net")
        {
        }

        public override async Task ExecuteOrderBook(OrderBook? message)
        {
            _logger.LogInformation("{@resultJson}", message);
        }
    }

  
}
