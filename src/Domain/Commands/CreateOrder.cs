
using System.ComponentModel.DataAnnotations;

namespace Domain.Commands;

public record CreateOrder
{
    [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser maior que 0.")]
    public double Amount { get; set; }
    public TypeCripto TypeCripto { get; set; }
}

public enum TypeCripto { 
    BTC, ETH
}
