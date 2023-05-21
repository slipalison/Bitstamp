using Domain.Commands;
using Domain.Models.AggregationOrder;
using Responses;

namespace Domain.Contracts.Services;

public interface IRequestOrderService
{
    Task<Result<Order>> CreateAsync(CreateOrder createOrder, TypeOrder typeOrder, CancellationToken cancellationToken);
}