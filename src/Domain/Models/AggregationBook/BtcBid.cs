using Domain.Commands;

namespace Domain.Models.AggregationBook
{
    public class BtcBid : BaseItemBook<BtcBid>
    {
        protected override IReadOnlyList<List<string>> GetDataList(OrderBook orderBook) => orderBook.Data.Bids;
    }
}