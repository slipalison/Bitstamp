﻿using Domain.Commands;

namespace Domain.Models.AggregationBook;

public class BtcAsk : BaseItemBook<BtcAsk>
{
    protected override IReadOnlyList<List<string>> GetDataList(OrderBook orderBook) => orderBook.Data.Asks;
}