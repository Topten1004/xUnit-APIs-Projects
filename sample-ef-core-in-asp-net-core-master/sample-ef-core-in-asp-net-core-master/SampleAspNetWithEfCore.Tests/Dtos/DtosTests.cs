using Newtonsoft.Json;
using SampleAspNetWithEfCore.Dtos;
using Xunit;

namespace SampleAspNetWithEfCore.Tests.Dtos
{
    public class DtosTests
    {
        [Trait("Category", "SmokeTest")]
        [Fact]
        public void PingDto_can_be_serialized()
        {
            var original = new PingDto(useUtc: true, messageSuffix: " test");
            var json = JsonConvert.SerializeObject(original);

            Assert.Contains(original.Message, json);
        }
    }
}
