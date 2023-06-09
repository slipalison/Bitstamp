﻿using Domain.Contracts.Repositories;
using Domain.Models.AggregationBook;

namespace Infra.Databases.SqlServers.BitstampData.Repositories;

public class EthBidRepository : BitstampRepository<EthBid>, IEthBidRepository
{
    public EthBidRepository(BitstampContext BitstampContext) : base(BitstampContext)
    {
    }
}