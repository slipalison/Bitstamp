using Domain.Commands;
using Domain.Models.AggregationOrder;
using Flurl.Http;
using Responses.Http;
using WebApi;

namespace UnitTest.IntegratedTests.WebApi
{
    public class OrderControllerTests : AbstractIntegratedTest
    {
        public OrderControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Theory]
        [InlineData(2.0, TypeCripto.BTC)]
        [InlineData(1.0, TypeCripto.ETH)]
        public async Task Buy(decimal amount, TypeCripto typeCripto)
        {
            var t = await CallHttp("/api/v1/Order/buy").PostJsonAsync(new CreateOrder { Amount = amount, TypeCripto = typeCripto }).ReceiveResult<Order>();
            Assert.NotNull(t);
            Assert.True(t.IsSuccess);
        }


        [Theory]
        [InlineData(2.0, TypeCripto.BTC)]
        [InlineData(1.0, TypeCripto.ETH)]
        public async Task Sell(decimal amount, TypeCripto typeCripto)
        {
            var t = await CallHttp("/api/v1/Order/sell").PostJsonAsync(new CreateOrder { Amount = amount, TypeCripto = typeCripto }).ReceiveResult<Order>();
            Assert.NotNull(t);
            Assert.True(t.IsSuccess);
        }

    }
}