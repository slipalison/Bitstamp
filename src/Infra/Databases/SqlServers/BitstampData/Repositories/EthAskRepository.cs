using Domain.Contracts.Repositories;
using Domain.Models.AggregationBook;

namespace Infra.Databases.SqlServers.BitstampData.Repositories;

public class EthAskRepository : BitstampRepository<EthAsk>, IEthAskRepository
{
    public EthAskRepository(BitstampContext BitstampContext) : base(BitstampContext)
    {
    }
}