using System.ComponentModel.DataAnnotations;

namespace Domain.Commands;

public record CreateOrder
{
    [Range(0.00000001, double.MaxValue, ErrorMessage = "O valor deve ser maior ou iqual a 0.00000001.")]
    public decimal Amount { get; set; }
    public TypeCripto TypeCripto { get; set; }
}

public enum TypeCripto
{
    BTC, ETH
}