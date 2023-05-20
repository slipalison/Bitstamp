namespace Domain.Models.AggregationOrder;

public record ItemOrder
{
    public decimal Price { get; set; }
    public double Amount { get; set; }
}