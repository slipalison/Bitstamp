using Domain.Commands;

namespace Domain.Models;

public class BtcAsk : BaseItemBook<BtcAsk>
{
    protected override IReadOnlyList<List<string>> GetDataList(OrderBook orderBook) => orderBook.Data.Asks;
}

