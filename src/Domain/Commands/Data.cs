using System.Text.Json.Serialization;

namespace Domain.Commands
{
    public record Data(
        [property: JsonPropertyName("timestamp")] long Timestamp,
        [property: JsonPropertyName("microtimestamp")] long Microtimestamp,
        [property: JsonPropertyName("bids")] IReadOnlyList<List<decimal>> Bids,
        [property: JsonPropertyName("asks")] IReadOnlyList<List<decimal>> Asks
    );
}
