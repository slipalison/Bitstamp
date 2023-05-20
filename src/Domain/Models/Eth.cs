using Domain.Commands;

namespace Domain.Models
{
    public abstract class Eth<T> where T : Eth<T>, new()
    {
        public Guid Id { get; set; }
        public DateTimeOffset InsertAt { get; set; }
        public long Timestamp { get; set; }
        public long Microtimestamp { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }

        protected abstract IReadOnlyList<List<decimal>> GetDataList(OrderBook orderBook);

        protected IReadOnlyList<T> ConvertBids(OrderBook orderBook)
        {
            return GetDataList(orderBook).Select(x => new T { 
                Amount = x[1], 
                Price = x[0], 
                Microtimestamp = orderBook.Data.Microtimestamp, 
                Timestamp = orderBook.Data.Timestamp, 
                InsertAt = DateTimeOffset.FromUnixTimeMilliseconds(orderBook.Data.Microtimestamp) 
            }).ToList();
        }
    }
}
