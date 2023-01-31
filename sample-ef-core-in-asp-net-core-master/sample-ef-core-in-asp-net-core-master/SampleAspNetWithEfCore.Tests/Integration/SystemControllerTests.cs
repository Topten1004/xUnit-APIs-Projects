using System.Threading.Tasks;
using Xunit;

namespace SampleAspNetWithEfCore.Tests.Integration
{
    public class SystemControllerTests : IntegrationTests
    {
        public SystemControllerTests(ApiIntegrationTestsFixture fixture) 
            : base(fixture)
        { }

        [Fact]
        public async Task System_Ping_Returns_Dto()
        {
            var response = await Client.GetAsync("/api/system/ping");
            var responseBody = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
            Assert.Contains("suffix-from-test-appsettings", responseBody);
        }
    }
}
