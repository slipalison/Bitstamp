using Domain.Commands;
using Domain.Contracts.Repositories;
using Domain.Models.AggregationBook;
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

    //public IBitstampRepository GetRepositoryToOrders<T>(TypeOrder typeOrder, TypeCripto typeCripto) where T : BaseItemBook<T>, new()
    //{

    //   var d = new Dictionary<(TypeOrder, TypeCripto), IBitstampRepository();

    //    var t =  typeOrder switch
    //    {
    //        TypeOrder.Buy => typeCripto == TypeCripto.BTC ? GetRepositoryToOrder<BtcAsk>(typeOrder, typeCripto) : GetRepositoryToOrder<EthAsk>(typeOrder, typeCripto),
    //        _ => typeCripto == TypeCripto.BTC ? GetRepositoryToOrder<BtcBid>(typeOrder, typeCripto) : GetRepositoryToOrder<EthBid>(typeOrder, typeCripto),
    //    };

    //    return t;
      
    //}

    public IBitstampRepository GetRepositoryToOrder(TypeOrder typeOrder, TypeCripto typeCripto) 
    {
        return typeOrder switch
        {
            TypeOrder.Buy => typeCripto == TypeCripto.BTC ? (IBitstampRepository) _btcAskRepository : (IBitstampRepository)_ethAskRepository,
            _ => typeCripto == TypeCripto.BTC ? (IBitstampRepository)_btcBidRepository : (IBitstampRepository)_ethBidRepository,
        };
    }

}
