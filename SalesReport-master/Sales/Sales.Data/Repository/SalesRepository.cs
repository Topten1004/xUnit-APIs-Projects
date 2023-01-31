using Sales.Data.Context;
using Sales.Domain.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Data.Repository
{
    public class SalesRepository : ISalesRepository
    {
        private readonly SalesDBContext context;

        public SalesRepository(SalesDBContext _context)
        {
            context = _context;
        }

        public async Task<ICollection<String>> SelectDistinctCountries()
        {
            return context.Sales.Select(x => x.Country).Distinct().ToList();
        }
    }
}