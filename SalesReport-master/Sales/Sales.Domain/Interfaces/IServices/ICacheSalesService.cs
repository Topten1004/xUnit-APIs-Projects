using Sales.Domain.Entities;
using System.Collections.Generic;

namespace Sales.Domain.Interfaces.IServices
{
    public interface ICacheSalesService
    {
        List<Sale> GetFibonacciCached(int key);

        void SetFibonacciCache(int key, List<Sale> lstSales);

        void ClearCache(int key);
    }
}