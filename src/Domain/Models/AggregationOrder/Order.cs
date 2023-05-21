using Domain.Commands;
using Domain.Models.AggregationMetrics;

namespace Domain.Models.AggregationOrder;

public record Order(
    Guid Id,
    decimal Amount,
    TypeCripto Crypto,
    OrderItem[] Stock,
    TypeOrder type
)
{
    public decimal Price { get => Stock.Sum(x=>x.Price);  }
    public decimal AmountTotal { get => Stock.Sum(x=>x.Amount); }

}
