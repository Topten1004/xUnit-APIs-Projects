using Xunit;

namespace SampleAspNetWithEfCore.Tests
{
    [CollectionDefinition(Name)]
    public class ApiIntegrationTestsCollection
        : ICollectionFixture<ApiIntegrationTestsFixture>
    {
        public const string Name = "ApiIntegrationTests";
    }
}