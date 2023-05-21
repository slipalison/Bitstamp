using Domain.Commands;
using Domain.Models.AggregationMetrics;

namespace Domain.Models.AggregationOrder;

public record Order
{
    public Order(
    Guid id,
    decimal amount,
    TypeCripto crypto,
    OrderItem[] stock,
    TypeOrder type)
    {
        Id = id;
        Amount = amount;
        Crypto = crypto;
        Stock = stock;
        Type = type;
        Price = Stock.Sum(x => x.Price);
        AmountTotal = Stock.Sum(x => x.Amount);
    }
    public decimal Price { get; set; }
    public decimal AmountTotal { get; set; }
    public DateTimeOffset InsertAt { get; set; } = DateTimeOffset.UtcNow;
    public Guid Id { get; private set; }
    public decimal Amount { get; private set; }
    public TypeCripto Crypto { get; private set; }
    public OrderItem[] Stock { get; private set; }
    public TypeOrder Type { get; private set; }
}
