using Domain.Commands;
using Domain.Contracts.Repositories;
using Domain.Contracts.WebSockets;
using Domain.Models.AggregationBook;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;

namespace Domain.Contracts.Services.WebSockets;

public class BtcUsdOrderBookService : BitstampWebSocket<BtcUsdOrderBookService>, IBtcUsdOrderBookService
{
    private readonly IBtcAskRepository _btcAskRepository;
    private readonly IBtcBidRepository _btcBidRepository;

    public BtcUsdOrderBookService(ILogger<BtcUsdOrderBookService> logger, ClientWebSocket clientWebSocket,
        IBtcAskRepository btcAskRepository, IBtcBidRepository btcBidRepository)
        : base(logger, clientWebSocket, CurrencyType.btcusd, "wss://ws.bitstamp.net")
    {
        _btcAskRepository = btcAskRepository;
        _btcBidRepository = btcBidRepository;
    }

    public override async Task ExecuteOrderBook(OrderBook? message, CancellationToken cancellationToken = default)
    {
        await _btcAskRepository.InsertOrUpdateRangeAsync(new BtcAsk().Convert(message!).ToList(), cancellationToken);
        await _btcBidRepository.InsertOrUpdateRangeAsync(new BtcBid().Convert(message!).ToList(), cancellationToken);
    }
    

}


