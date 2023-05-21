using Domain.Commands;
using Domain.Contracts.Repositories;
using Domain.Models.AggregationOrder;

namespace Domain.Services;

public class RepositoryFabric : IRepositoryFabric
{
    private readonly IBtcAskRepository _btcAskRepository;
    private readonly IBtcBidRepository _btcBidRepository;
    private readonly IEthAskRepository _ethAskRepository;
    private readonly IEthBidRepository _ethBidRepository;

    public RepositoryFabric(
        IBtcAskRepository btcAskRepository,
        IBtcBidRepository btcBidRepository,
        IEthAskRepository ethAskRepository,
        IEthBidRepository ethBidRepository)
    {
        _btcAskRepository = btcAskRepository;
        _btcBidRepository = btcBidRepository;
        _ethAskRepository = ethAskRepository;
        _ethBidRepository = ethBidRepository;
    }

    public IBitstampRepository GetRepositoryToOrder(TypeOrder typeOrder, TypeCripto typeCripto)
    {
        return typeOrder switch
        {
            TypeOrder.Buy => typeCripto == TypeCripto.BTC ? _btcAskRepository : _ethAskRepository,
            _ => typeCripto == TypeCripto.BTC ? _btcBidRepository : _ethBidRepository,
        };
    }
}