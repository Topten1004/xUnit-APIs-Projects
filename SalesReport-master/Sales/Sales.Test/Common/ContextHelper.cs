using Microsoft.EntityFrameworkCore;
using Sales.Data.Context;

namespace Sales.Test.Common
{
    public static class ContextHelper
    {
        public static DbContextOptionsBuilder<SalesDBContext> GetConnection(string conn)
        {
            DbContextOptionsBuilder<SalesDBContext> connBuilder = new DbContextOptionsBuilder<SalesDBContext>();
            connBuilder.UseSqlServer(conn);
            return connBuilder;
        }
    }
}