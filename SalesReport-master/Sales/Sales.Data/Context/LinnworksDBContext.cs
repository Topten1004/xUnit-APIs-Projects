using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sales.Common.Config;
using Sales.Data.Mapping;
using Sales.Domain.Entities;

namespace Sales.Data.Context
{
    public class SalesDBContext : DbContext
    {
        public IConfigurationRoot Configuration { get; set; }
        public static string ConnectionString { get; private set; }
        private readonly DbContextOptions<SalesDBContext> _connInfo = null;

        public SalesDBContext(string connInfo)
        {
            _connInfo = GetConnection(connInfo).Options;
        }

        public DbContextOptionsBuilder<SalesDBContext> GetConnection(string conn)
        {
            DbContextOptionsBuilder<SalesDBContext> connBuilder = new DbContextOptionsBuilder<SalesDBContext>();
            connBuilder.UseSqlServer(conn);
            return connBuilder;
        }

        public SalesDBContext(DbContextOptions<SalesDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(GetConn());
        }

        #region DBSets

        public DbSet<Sale> Sales { get; set; }

        #endregion DBSets

        public string GetConn()
        {
            return Config.GetConnection();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sale>(new SalesMapping().Configure);
        }
    }
}