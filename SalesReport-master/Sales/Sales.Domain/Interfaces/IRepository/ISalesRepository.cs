using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sales.Domain.Interfaces.IRepository
{
    public interface ISalesRepository
    {
        Task<ICollection<string>> SelectDistinctCountries();
    }
}