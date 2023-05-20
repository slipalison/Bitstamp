using Domain.Commands;

namespace Domain.Contracts.WebSockets
{
    public interface IOrderBookService
    {
        Task ExecuteOrderBook(OrderBook? message, CancellationToken cancellationToken = default);
        Task ConnectAndListen();
    }
}