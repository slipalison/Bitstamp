namespace Infra.WebSockets
{
    public interface IOrderBookService
    {
        Task ExecuteOrderBook(OrderBook? message);
        Task ConnectAndListen();
    }
}