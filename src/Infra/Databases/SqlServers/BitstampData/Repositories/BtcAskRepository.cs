﻿using Domain.Contracts.Repositories;
using Domain.Models.AggregationBook;

namespace Infra.Databases.SqlServers.BitstampData.Repositories;

public class BtcAskRepository : BitstampRepository<BtcAsk>, IBtcAskRepository
{
    public BtcAskRepository(BitstampContext BitstampContext) : base(BitstampContext)
    {
    }
}