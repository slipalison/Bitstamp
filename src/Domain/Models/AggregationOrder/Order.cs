using Domain.Commands;

namespace Domain.Models.AggregationOrder;

public record Order(
    Guid Id,
    double Amount,
    TypeCripto Crypto,
    ItemOrder[] Stock,
    TypeOrder type
)
{
    public decimal Price { get => Stock.Sum(x=>x.Price);  }
    public double AmountTotal { get => Stock.Sum(x=>x.Amount); }

}
