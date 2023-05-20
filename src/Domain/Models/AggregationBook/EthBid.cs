using Domain.Commands;

namespace Domain.Models.AggregationBook
{
    public class EthBid : BaseItemBook<EthBid>
    {
        protected override IReadOnlyList<List<string>> GetDataList(OrderBook orderBook) => orderBook.Data.Bids;
    }
}
