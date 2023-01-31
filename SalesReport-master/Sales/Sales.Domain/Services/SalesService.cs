using Sales.Domain.Interfaces.IRepository;
using Sales.Domain.Interfaces.IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales.Domain.Services
{
    public class SalesService : ISalesService
    {
        private ISalesRepository repository;

        public SalesService(ISalesRepository _repository)
        {
            repository = _repository;
        }

        public async Task<List<string>> SelectDistinctCountries()
        {
            var result = await repository.SelectDistinctCountries().ConfigureAwait(false);
            return result.ToList();
        }
    }
}