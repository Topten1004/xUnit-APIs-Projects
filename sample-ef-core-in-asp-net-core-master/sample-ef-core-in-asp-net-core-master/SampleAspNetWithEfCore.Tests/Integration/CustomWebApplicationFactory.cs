using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace SampleAspNetWithEfCore.Tests
{
    public class ApiIntegrationTestsFixture : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json") // Set as "Content" with action "Copy if newer"
                .Build();

            builder.ConfigureTestServices(services => 
                services.AddOptions<SystemOptions>(configuration.GetSection("System")));
        }
    }
}