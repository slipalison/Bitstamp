﻿using Domain.Commands;

namespace Domain.Models
{
    public class EthAsk : BaseItemBook<EthAsk>
    {
        protected override IReadOnlyList<List<string>> GetDataList(OrderBook orderBook) => orderBook.Data.Asks;
    }
}
