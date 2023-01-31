using Microsoft.Extensions.Configuration;
using Moq;
using Sales.Common.Config;
using Sales.Data.Context;
using Sales.Data.Repository;
using Sales.Domain.Entities;
using Sales.Domain.Interfaces.IRepository;
using Sales.Domain.Interfaces.IServices;
using Sales.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Sales.Test.Domain
{
    public class SalesTest
    {
        private readonly ITestOutputHelper _output;
        private Mock<IGenericServiceAsync<Sale>> _mockService;
        private Mock<ISalesService> _mockSalesService;
        private Guid id = new Guid();
        private IConfiguration Configuration = null;

        public SalesTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Start the tests");
        }

        internal void Dispose()
        {
            _output.WriteLine("Dispose Executed");
        }

        [Fact(DisplayName = "Add New Sale")]
        public async Task AddNewSale()
        {
            var context = new SalesDBContext(Config.GetConnection());
            IGenericRepositoryAsync<Sale> _repository = new GenericRepositoryAsync<Sale>(context);
            var service = new GenericServiceAsync<Sale>(_repository);
            List<Sale> sales = new List<Sale>
            {
                new Sale
                {
                    Id = id,
                    Region = "America do Sul",
                    Country = "Argentina",
                    ItemType = "Feijao",
                    SalesChannel = "Online",
                    OrderPriority = "H",
                    OrderDate = DateTime.Now.AddMonths(-7),
                    OrderId = "555325",
                    ShipDate = DateTime.Now.AddMonths(-4),
                    UnitsSold = 1530,
                    UnitPrice = 3367,
                    UnitCost = 2123,
                    TotalRevenue = 114.57M,
                    TotalCost = 520,
                    TotalProfit = 25350
                },

                new Sale
                {
                    Id = id,
                    Region = "Europa",
                    Country = "Ireland",
                    ItemType = "Tapioca",
                    SalesChannel = "Online",
                    OrderPriority = "H",
                    OrderDate = DateTime.Now.AddMonths(-22),
                    OrderId = "2223",
                    ShipDate = DateTime.Now.AddMonths(-13),
                    UnitsSold = 53220,
                    UnitPrice = 423,
                    UnitCost = 2352,
                    TotalRevenue = 1545.5M,
                    TotalCost = 32460,
                    TotalProfit = 32500
                }
            };

            foreach (var item in sales)
            {
                await service.Add(item).ConfigureAwait(false);
            }

            await service.SaveAsync();

            Assert.True(true);
        }

        [Fact(DisplayName = "Get All Countries")]
        public async Task GetAllCountries()
        {
            var context = new SalesDBContext(Config.GetConnection());
            ISalesRepository _repository = new SalesRepository(context);
            var salesService = new SalesService(_repository);
            List<Sale> sales = new List<Sale>();

            var lstCountries = await salesService.SelectDistinctCountries().ConfigureAwait(false);

            Assert.True(true);
        }
    }
}