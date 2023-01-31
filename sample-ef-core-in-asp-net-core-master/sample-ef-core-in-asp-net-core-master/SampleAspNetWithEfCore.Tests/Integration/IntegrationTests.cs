using System.Net.Http;
using Xunit;

namespace SampleAspNetWithEfCore.Tests.Integration
{
    [Trait("Category", "Integration")]
    [Collection(ApiIntegrationTestsCollection.Name)]
    public abstract class IntegrationTests
        : IClassFixture<ApiIntegrationTestsFixture>
    {
        protected HttpClient Client { get; }

        protected IntegrationTests(ApiIntegrationTestsFixture fixture)
        {
            Client = fixture.CreateClient();
        }
    }
}
