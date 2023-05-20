using Domain.Commands;

namespace Infra.WebSockets
{
    public interface IOrderBookService
    {
        Task ExecuteOrderBook(OrderBook? message, CancellationToken cancellationToken = default);
        Task ConnectAndListen();
    }
}