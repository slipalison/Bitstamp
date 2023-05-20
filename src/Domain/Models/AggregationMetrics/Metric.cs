using System.Text.Json.Serialization;

namespace Domain.Models.AggregationMetrics;

public record Metric(
    [property: JsonPropertyName("minPrice")] decimal? MinPrice,
    [property: JsonPropertyName("maxPrice")] decimal? MaxPrice,
    [property: JsonPropertyName("mediaAmount")] decimal? MediaAmount,
    [property: JsonPropertyName("mediaPrice")] decimal? MediaPrice,
    [property: JsonPropertyName("mediaPrice5")] decimal? MediaPrice5
);

