using SampleAspNetWithEfCore.Entities;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace SampleAspNetWithEfCore.Tests.Integration
{
    public class PeopleControllerTests : IntegrationTests
    {
        public PeopleControllerTests(ApiIntegrationTestsFixture fixture) 
            : base(fixture)
        { }

        [Fact]
        public async Task Delete_returns_not_found_for_non_existent_id()
        {
            var response = await Client.DeleteAsync("/api/people/-1");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_can_follow_post()
        {
            var response = await Client.PostAsJsonAsync("/api/people", new
            {
                name = "John Doe",
                pet = new { name = "Miffy", animal = "Cat" }
            });

            var result = await response.DeserializeContentAs<Person>();

            var deleteResponse = await Client.DeleteAsync($"/api/people/{result.Id}");

            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }

        [Fact]
        public async Task Delete_makes_get_return_not_found()
        {
            var response = await Client.PostAsJsonAsync("/api/people", new
            {
                name = "John Doe",
                pet = new { name = "Miffy", animal = "Cat" }
            });

            var result = await response.DeserializeContentAs<Person>();

            var getResponseBeforeDelete = await Client.GetAsync($"/api/people/{result.Id}");
            getResponseBeforeDelete.EnsureSuccessStatusCode();

            var deleteResponse = await Client.DeleteAsync($"/api/people/{result.Id}");
            deleteResponse.EnsureSuccessStatusCode();

            var getResponseAfterDelete = await Client.GetAsync($"/api/people/{result.Id}");
            Assert.Equal(HttpStatusCode.NotFound, getResponseAfterDelete.StatusCode);
        }

        [Fact]
        public async Task Get_returns_success_result()
        {
            var response = await Client.GetAsync("/api/people");
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_returns_zero_results_by_default()
        {
            var response = await Client.GetAsync("/api/people");
            response.EnsureSuccessStatusCode();

            var result = await response.DeserializeContentAs<Person[]>();

            Assert.Empty(result);
        }

        [Fact]
        public async Task Post_returns_created_item()
        {
            var response = await Client.PostAsJsonAsync("/api/people", new
            {
                name = "John Doe",
                pet = new { name = "Miffy", animal = "Cat" }
            });

            var result = await response.DeserializeContentAs<Person>();

            Assert.Equal("John Doe", result.Name);
        }
    }
}
