using Domain.Commands;
using Domain.Contracts.Repositories;
using Domain.Models;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;

namespace Infra.WebSockets
{
    public class EthUsdOrderBookService : BitstampWebSocket<EthUsdOrderBookService>, IEthUsdOrderBookService
    {
        private readonly IEthAskRepository _ethAskRepository;
        private readonly IEthBidRepository _ethBidRepository;

        public EthUsdOrderBookService(ILogger<EthUsdOrderBookService> logger, ClientWebSocket clientWebSocket,
            IEthAskRepository ethAskRepository, IEthBidRepository ethBidRepository)
            : base(logger, clientWebSocket, CurrencyType.ethusd, "wss://ws.bitstamp.net")
        {
            _ethAskRepository = ethAskRepository;
            _ethBidRepository = ethBidRepository;
        }

        public override async Task ExecuteOrderBook(OrderBook? message, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("{@resultJson}", message);
            await _ethAskRepository.InsertOrUpdateRangeAsync(new EthAsk().Convert(message!).ToList(), cancellationToken);
            await _ethBidRepository.InsertOrUpdateRangeAsync(new EthBid().Convert(message!).ToList(), cancellationToken);
        }
    }

  
}
