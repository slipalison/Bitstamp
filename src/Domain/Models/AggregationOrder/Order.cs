using Domain.Commands;

namespace Domain.Models.AggregationOrder;

public record Order
{
    public Guid Id { get; set; }
    public double Amount { get; set; }
    public TypeCripto Crypto { get; set; }
    public decimal Price { get; set; }
    public decimal AmountTotal { get; set; }
    public ItemOrder[] Stock { get; set; }
    public TypeOrder Type { get; set; }
}
