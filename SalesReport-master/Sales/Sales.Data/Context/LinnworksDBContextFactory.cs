using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Sales.Common.Config;
using Sales.Data.Context;

namespace LinnworksTest.Data.Context
{
    public class SalesDBContextFactory : IDesignTimeDbContextFactory<SalesDBContext>
    {
        public SalesDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SalesDBContext>();
            optionsBuilder.UseSqlServer(Config.GetConnection());

            return new SalesDBContext(optionsBuilder.Options);
        }
    }
}