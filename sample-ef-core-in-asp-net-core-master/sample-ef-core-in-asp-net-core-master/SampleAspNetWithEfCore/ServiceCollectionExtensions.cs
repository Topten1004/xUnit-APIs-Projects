using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SampleAspNetWithEfCore
{
    public static class ServiceCollectionExtensions
    {
        public static T AddOptions<T>(this IServiceCollection services, IConfigurationSection section)
            where T : class, new()
        {
            services.Configure<T>(section);
            services.AddSingleton(provider => provider.GetRequiredService<IOptions<T>>().Value);

            return section.Get<T>();
        }
    }
}