using Domain.Models.AggregationBook;

namespace Domain.Contracts.Repositories;

public interface IEthAskRepository: IBitstampRepository<EthAsk>
{
}

public interface IEthBidRepository : IBitstampRepository<EthBid>
{
}

public interface IBtcAskRepository : IBitstampRepository<BtcAsk>
{
}
public interface IBtcBidRepository : IBitstampRepository<BtcBid>
{
}
