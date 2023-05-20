using Domain.Commands;

namespace Domain.Models
{
    public class EthBid : BaseItemBook<EthBid>
    {
        protected override IReadOnlyList<List<string>> GetDataList(OrderBook orderBook) => orderBook.Data.Bids;
    }
}
