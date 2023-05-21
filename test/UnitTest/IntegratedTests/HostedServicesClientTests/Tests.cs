using Flurl.Http;
using HostedServicesClient;

namespace UnitTest.IntegratedTests.HostedServicesClientTests
{
    public class Tests : AbstractIntegratedTest<Program>
    {
        public Tests(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Test() {

            var t = await CallHttp("").GetAsync();

            Assert.NotNull(t);
            Assert.True(t.StatusCode == 200);

        }
    }
}
