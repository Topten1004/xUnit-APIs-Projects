using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace SampleAspNetWithEfCore.Tests.Integration
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> DeserializeContentAs<T>(this HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }
    }
}
