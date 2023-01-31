using Microsoft.Extensions.Caching.Memory;
using Sales.Common.Config;
using Sales.Domain.IRepository.Interfaces;
using System;

namespace Sales.Data.Repository
{
    public class CacheProvider : ICacheProvider
    {
        private readonly int CacheSeconds = Convert.ToInt32(Config.GetValueFromKeyFromAppSetings("CacheOptions:CacheTime")); //600 seconds

        private readonly IMemoryCache _cache;

        public CacheProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T GetFromCache<T>(int key) where T : class
        {
            var cachedResponse = _cache.Get(key);
            return cachedResponse as T;
        }

        public void SetCache<T>(int key, T value) where T : class
        {
            SetCache(key, value, DateTimeOffset.Now.AddSeconds(CacheSeconds));
        }

        public void SetCache<T>(int key, T value, DateTimeOffset duration) where T : class
        {
            _cache.Set(key, value, duration);
        }

        public void ClearCache(int key)
        {
            _cache.Remove(key);
        }
    }
}