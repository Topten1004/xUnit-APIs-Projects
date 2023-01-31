using Sales.Domain.Entities;
using Sales.Domain.Interfaces.IServices;
using Sales.Domain.IRepository.Interfaces;
using System.Collections.Generic;

namespace Sales.Domain.Services
{
    public class CacheSalesService : ICacheSalesService
    {
        private readonly ICacheProvider _cacheProvider;

        public CacheSalesService(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public void ClearCache(int key)
        {
            _cacheProvider.ClearCache(key);
        }

        public List<Sale> GetFibonacciCached(int key)
        {
            return _cacheProvider.GetFromCache<List<Sale>>(key);
        }

        public void SetFibonacciCache(int key, List<Sale> lstSales)
        {
            _cacheProvider.SetCache(key, lstSales);
        }
    }
}