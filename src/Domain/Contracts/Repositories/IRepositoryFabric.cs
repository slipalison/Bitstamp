using Domain.Commands;
using Domain.Models.AggregationOrder;

namespace Domain.Contracts.Repositories;

public interface IRepositoryFabric
{
    IBitstampRepository GetRepositoryToOrder(TypeOrder typeOrder, TypeCripto typeCripto);
}