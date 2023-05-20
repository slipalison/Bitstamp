using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infra.WebSockets
{

    public class BtcUsdOrderBookHostedService : BackgroundService
    {
        private readonly BtcUsdOrderBookService _btcUsdOrderBookService;

        public BtcUsdOrderBookHostedService(ILogger<BtcUsdOrderBookHostedService> logger, BtcUsdOrderBookService btcUsdOrderBookService)
        {
            _btcUsdOrderBookService = btcUsdOrderBookService;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _btcUsdOrderBookService.ConnectAndListen();
        }
    }

    public record Data(
        [property: JsonPropertyName("timestamp")] string Timestamp,
        [property: JsonPropertyName("microtimestamp")] string Microtimestamp,
        [property: JsonPropertyName("bids")] IReadOnlyList<List<string>> Bids,
        [property: JsonPropertyName("asks")] IReadOnlyList<List<string>> Asks
    );

    public record OrderBook(
        [property: JsonPropertyName("data")] Data Data,
        [property: JsonPropertyName("channel")] string Channel,
        [property: JsonPropertyName("event")] string Event
    );


    public enum CurrencyType
    {
        btcusd,
        ethusd
    }

    public class BtcUsdOrderBookService : BitstampWebSocket<BtcUsdOrderBookService>
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

    public abstract class BitstampWebSocket<T> : IDisposable where T : BitstampWebSocket<T>
    {
        private readonly string _wsUrl;
        protected readonly ILogger<T> _logger;
        private readonly ClientWebSocket _socket;
        private readonly CurrencyType _currencyType;
        private readonly CancellationToken _cancellationToken;
        private readonly Memory<byte> _buffer;
        private bool disposedValue = false;

        static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

    protected BitstampWebSocket(ILogger<T> logger, ClientWebSocket clientWebSocket, CurrencyType currencyType, string wsUrl)
        {
            _wsUrl = wsUrl;
            _logger = logger;
            _socket = clientWebSocket;
            _currencyType = currencyType;
            _cancellationToken = new CancellationToken();
            _buffer = new Memory<byte>(new byte[4096 * 2]);
        }

        public abstract Task ExecuteOrderBook(OrderBook? message);

        public async Task ConnectAndListen()
        {
            await _socket.ConnectAsync(new Uri(_wsUrl), _cancellationToken);

            await SubscribeWebSocketMessage();

            while (_socket.State == WebSocketState.Open)
            {
                await ReceiveMessage();
            }
        }

        private async Task SubscribeWebSocketMessage()
        {
            var message = $"{{\"event\":\"bts:subscribe\",\"data\":{{\"channel\":\"order_book_{_currencyType}\"}}}}";

            await _socket.SendAsync(new Memory<byte>(Encoding.UTF8.GetBytes(message)), WebSocketMessageType.Text, true, _cancellationToken);
        }

        private async Task ReceiveMessage()
        {
            var result = await _socket.ReceiveAsync(_buffer, _cancellationToken);

            if (result.MessageType != WebSocketMessageType.Text)
                return;
            
            var message = Encoding.UTF8.GetString(_buffer.Span.Slice(0, result.Count));
            if (!string.IsNullOrEmpty(message) && TryParse(message, out var resultJson))
                await ExecuteOrderBook(resultJson);
        }

        public static bool TryParse(string jsonString, out OrderBook? result)
        {
            try
            {
                var obj = JsonSerializer.Deserialize<OrderBook>(jsonString, JsonSerializerOptions);
                result = obj;
                return true;
            }
            catch (JsonException)
            {
                result = default;
                return false;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _socket.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

  
}
