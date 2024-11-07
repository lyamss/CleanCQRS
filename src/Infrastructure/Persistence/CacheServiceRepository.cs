using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.Persistence
{
    internal sealed class CacheServiceRepository(IDistributedCache cache) : ICacheServiceRepository
    {
        private readonly IDistributedCache _cache = cache;

        public async Task<CacheValue<T>> GetInCacheAsync<T>(string key, CancellationToken cancellationToken)
        {
            var json = await this._cache.GetStringAsync(key, cancellationToken);
            if (json == null)
            {
                return new CacheValue<T> { HasValue = false };
            }
            return JsonSerializer.Deserialize<CacheValue<T>>(json);
        }

        public async Task SetInCacheAsync<T>(CancellationToken cancellationToken, string key, T value, TimeSpan? absoluteExpirationTime = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpirationTime ?? TimeSpan.FromMinutes(30)
            };
            var cacheValue = new CacheValue<T> { Value = value, HasValue = true };
            var json = JsonSerializer.Serialize(cacheValue);
            await this._cache.SetStringAsync(key, json, options, cancellationToken);
        }

        public async Task<T?> SetOrGetInDbOrCache<T>
        (
        string key,
        TimeSpan? expireDataToStock,
        Func<CancellationToken, Task<T>> getDataFromDb,
        CancellationToken cancellationToken
        )
        {
            var cacheValue = await this.GetInCacheAsync<T>(key, cancellationToken);
            if (!cacheValue.HasValue)
            {
                var data = await getDataFromDb(cancellationToken);

                if(data is not null)
                {
                    await this.SetInCacheAsync<T>(cancellationToken, key, data, expireDataToStock);
                    return data;
                }

                await this.SetInCacheAsync<T>(cancellationToken, key, default(T), expireDataToStock);

                return default(T);
            }
            return cacheValue.Value;
        }
    }

    public interface ICacheServiceRepository
    {
        Task<T> SetOrGetInDbOrCache<T>(string key,
        TimeSpan? expireDataToStock,
        Func<CancellationToken, Task<T>> getDataFromDb,
        CancellationToken cancellationToken
        );
        Task<CacheValue<T>> GetInCacheAsync<T>(string key, CancellationToken cancellationToken);
        Task SetInCacheAsync<T>(CancellationToken cancellationToken, string key, T value, TimeSpan? absoluteExpirationTime = null);
    }


    public class CacheValue<T>
    {
        public T Value { get; set; }
        public bool HasValue { get; set; }
    }
}