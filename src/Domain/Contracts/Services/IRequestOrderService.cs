using Domain.Commands;
using Domain.Models.AggregationOrder;

namespace Domain.Contracts.Services;

public interface IRequestOrderService
{
    Task<Order> CreateAsync(CreateOrder createOrder, TypeOrder typeOrder, CancellationToken cancellationToken);
}
