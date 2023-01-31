using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Domain.Interfaces.IServices
{
    public interface ISalesService
    {
        Task<List<string>> SelectDistinctCountries();
    }
}