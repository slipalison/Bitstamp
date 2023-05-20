using Domain.Contracts.Repositories;
using Domain.Models;


namespace Infra.Databases.SqlServers.BitstampData.Repositories;

public class BtcBidRepository : BitstampRepository<BtcBid>, IBtcBidRepository
{
    public BtcBidRepository(BitstampContext BitstampContext) : base(BitstampContext)
    {
    }
}
