using LinnworksTest.Common.Config;
using LinnworksTest.Data.Context;
using LinnworksTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LinnworksTest.Test.DBContext
{
    public class LinnworksDBContext : DbContext
    {
        #region Creating Context for the Code First

        //Creating Context for the Code First
        private readonly DbContextOptions<LinnworksDBContext> _connInfo = null;
        public LinnworksDBContext(string connInfo)
        {
            _connInfo = GetConnection(connInfo).Options;
        }

        public DbContextOptionsBuilder<LinnworksDBContext> GetConnection(string conn)
        {
            DbContextOptionsBuilder<LinnworksDBContext> connBuilder = new DbContextOptionsBuilder<LinnworksDBContext>();
            connBuilder.UseSqlServer(conn);
            return connBuilder;
        }

        #endregion Creating Context for the Code First

        #region Singleton

        //Singleton
        private static readonly LinnworksDBContext _instance = null;

        public static LinnworksDBContext GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    return new LinnworksDBContext(Config.GetConnection());
                }
                return _instance;
            }
        }

        #endregion Singleton

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(Config.GetConnection());
        }

        #region DBSets

        public DbSet<Sales> Sales { get; set; }

        #endregion DBSets

        #region Relationship

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Table Person
            builder.Entity<Sales>().ToTable("Sales").HasKey(pk => pk.Id);
        }

        #endregion Relationship

    }
}
