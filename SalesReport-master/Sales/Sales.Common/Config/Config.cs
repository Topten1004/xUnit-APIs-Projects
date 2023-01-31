using Microsoft.Extensions.Configuration;
using System.IO;

namespace Sales.Common.Config
{
    public static class Config
    {
        private static IConfigurationRoot Configuration { get; set; }
        private static string ConnectionString { get; set; }

        public static string GetConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            ConnectionString = Configuration["ConnectionStrings:SalesConnection"];

            return ConnectionString;
        }

        public static string GetValueFromKeyFromAppSetings(string key)
        {
            var builder = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .AddEnvironmentVariables();
            Configuration = builder.Build();
            return Configuration[key];
        }
    }
}