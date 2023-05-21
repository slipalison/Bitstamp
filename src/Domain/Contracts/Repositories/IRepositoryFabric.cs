using Domain.Commands;
using Domain.Models.AggregationBook;
using Domain.Models.AggregationOrder;

namespace Domain.Contracts.Repositories;

public interface IRepositoryFabric
{
    IBitstampRepository GetRepositoryToOrder(TypeOrder typeOrder, TypeCripto typeCripto);
    //IBitstampRepository<T> GetRepositoryToOrder<T>(TypeOrder typeOrder, TypeCripto typeCripto) where T : BaseItemBook<T>, new();
}
