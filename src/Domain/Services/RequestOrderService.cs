using Domain.Commands;
using Domain.Contracts.Repositories;
using Domain.Models.AggregationOrder;

namespace Domain.Services;

public class RequestOrderService : IRequestOrderService
{
    private readonly IRepositoryFabric _repositoryFabric;
    private readonly ICorrelationContextService _correlation;

    public RequestOrderService(ICorrelationContextService correlation, IRepositoryFabric repositoryFabric)
    {
        _repositoryFabric = repositoryFabric;
        _correlation = correlation;
    }
    public Task<Order> CreateAsync(CreateOrder createOrder, TypeOrder typeOrder, CancellationToken cancellationToken)
    {
        var t = new Order(
            _correlation.GetCorrelationId(),
            createOrder.Amount,
            createOrder.TypeCripto,
            new[] { new ItemOrder { Price = 27000.00m, Amount = 0.00669 } },
            typeOrder
        );

        var repo = _repositoryFabric.GetRepositoryToOrder(typeOrder, createOrder.TypeCripto);

        return Task.FromResult(t);
    }
}

public interface IRequestOrderService
{
    Task<Order> CreateAsync(CreateOrder createOrder, TypeOrder typeOrder, CancellationToken cancellationToken);
}
