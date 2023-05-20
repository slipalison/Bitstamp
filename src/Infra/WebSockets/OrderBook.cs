using System.Text.Json.Serialization;

namespace Infra.WebSockets
{
    public record OrderBook(
        [property: JsonPropertyName("data")] Data Data,
        [property: JsonPropertyName("channel")] string Channel,
        [property: JsonPropertyName("event")] string Event
    );

  
}
