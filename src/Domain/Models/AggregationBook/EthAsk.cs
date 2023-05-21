﻿using Domain.Commands;
using Domain.Contracts.Repositories;

namespace Domain.Models.AggregationBook
{
    public class EthAsk : BaseItemBook<EthAsk>
    {
        protected override IReadOnlyList<List<string>> GetDataList(OrderBook orderBook) => orderBook.Data.Asks;
    }
}
