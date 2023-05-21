using Domain.Commands;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Models.AggregationOrder;
using Responses;

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

    public async Task<Result<Order>> CreateAsync(CreateOrder createOrder, TypeOrder typeOrder, CancellationToken cancellationToken)
    {
        var repo = _repositoryFabric.GetRepositoryToOrder(typeOrder, createOrder.TypeCripto);
        var list = await repo.ListItensBookToOrder(createOrder.Amount, cancellationToken);

        if (list is null)
            return Result.Fail<Order>("Null", "Não encontrado valor na base");

        var order = new Order(_correlation.GetCorrelationId(), createOrder.Amount, createOrder.TypeCripto, list.ToArray(), typeOrder);

        await repo.SaveOrder(order, cancellationToken);

        return Result.Ok(order);
    }
}