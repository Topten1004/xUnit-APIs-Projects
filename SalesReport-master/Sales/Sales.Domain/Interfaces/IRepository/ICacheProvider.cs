using System;

namespace Sales.Domain.IRepository.Interfaces
{
    public interface ICacheProvider
    {
        T GetFromCache<T>(int key) where T : class;

        void SetCache<T>(int key, T value) where T : class;

        void SetCache<T>(int key, T value, DateTimeOffset duration) where T : class;

        void ClearCache(int key);
    }
}