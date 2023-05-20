using Domain.Commands;

namespace Domain.Models
{
    public class EthAsk : Eth<EthAsk>
    {
        protected override IReadOnlyList<List<decimal>> GetDataList(OrderBook orderBook) => orderBook.Data.Asks;
    }
    public class EthBid : Eth<EthBid>
    {
        protected override IReadOnlyList<List<decimal>> GetDataList(OrderBook orderBook) => orderBook.Data.Bids;
    }
}
