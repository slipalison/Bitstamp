using System.Text.Json.Serialization;

namespace Infra.WebSockets
{
    public record Data(
        [property: JsonPropertyName("timestamp")] string Timestamp,
        [property: JsonPropertyName("microtimestamp")] string Microtimestamp,
        [property: JsonPropertyName("bids")] IReadOnlyList<List<string>> Bids,
        [property: JsonPropertyName("asks")] IReadOnlyList<List<string>> Asks
    );

  
}
