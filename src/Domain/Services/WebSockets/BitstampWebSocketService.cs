using Domain.Commands;
using Domain.Contracts.WebSockets;
using Microsoft.Extensions.Logging;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace Domain.Contracts.Services.WebSockets;

public abstract class BitstampWebSocket<T> : IOrderBookService, IDisposable where T : BitstampWebSocket<T>
{
    private readonly string _wsUrl;
    protected readonly ILogger<T> _logger;
    private readonly ClientWebSocket _socket;
    private readonly CurrencyType _currencyType;
    private readonly CancellationToken _cancellationToken;
    private readonly Memory<byte> _buffer;
    private bool disposedValue = false;

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
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

    public abstract Task ExecuteOrderBook(OrderBook? message, CancellationToken cancellationToken = default);

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
        if (!string.IsNullOrEmpty(message) && TryParse(message, out var resultJson) && resultJson!.Event == "data")
        {
            await ExecuteOrderBook(resultJson, _cancellationToken);
        }
    }

    public static bool TryParse(string jsonString, out OrderBook? result)
    {
        try
        {
            var obj = JsonSerializer.Deserialize<OrderBook>(jsonString, _jsonSerializerOptions);
            if (obj is null)
            {
                result = default;
                return false;
            }
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