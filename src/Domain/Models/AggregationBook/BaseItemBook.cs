using Domain.Commands;

namespace Domain.Models.AggregationBook
{
    public abstract class BaseItemBook<T> where T : BaseItemBook<T>, new()
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTimeOffset InsertAt { get; set; }
        public long Timestamp { get; set; }
        public long Microtimestamp { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }

        protected abstract IReadOnlyList<List<string>> GetDataList(OrderBook orderBook);

        public IReadOnlyList<T> Convert(OrderBook orderBook)
        {
            return GetDataList(orderBook).Select(x => new T
            {
                Amount = decimal.Parse(x[1], System.Globalization.CultureInfo.InvariantCulture),
                Price = decimal.Parse(x[0], System.Globalization.CultureInfo.InvariantCulture),
                Microtimestamp = long.Parse(orderBook.Data.Microtimestamp),
                Timestamp = long.Parse(orderBook.Data.Timestamp),
                InsertAt = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(orderBook.Data.Microtimestamp) / 1000)
            }).ToList();
        }

        public T UpdateTimeStamp(long timestamp, long microtimestamp)
        {
            Timestamp = timestamp;
            Microtimestamp = microtimestamp;
            InsertAt = DateTimeOffset.FromUnixTimeMilliseconds(microtimestamp / 1000);

            return (T)this;
        }
    }
}
