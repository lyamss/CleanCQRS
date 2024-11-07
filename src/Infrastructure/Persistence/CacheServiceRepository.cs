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

        public async Task<TDestination> SetOrGetInDbOrCache<TSource, TDestination>
        (
            string key,
            TimeSpan? expireDataToStock,
            Func<CancellationToken, Task<TSource>> getDataFromDb,
            Func<TSource, TDestination> mapper,
            CancellationToken cancellationToken
        )
        {
            var cacheValue = await this.GetInCacheAsync<TDestination>(key, cancellationToken);
            if (!cacheValue.HasValue)
            {
                var data = await getDataFromDb(cancellationToken);

                if (data is not null)
                {
                    var mappedData = mapper(data);
                    await this.SetInCacheAsync<TDestination>(cancellationToken, key, mappedData, expireDataToStock);
                    return mappedData;
                }

                await this.SetInCacheAsync<TDestination>(cancellationToken, key, default(TDestination), expireDataToStock);

                return default(TDestination);
            }
            return cacheValue.Value;
        }
    }

    public interface ICacheServiceRepository
    {
        Task<TDestination> SetOrGetInDbOrCache<TSource, TDestination>
        (
            string key,
            TimeSpan? expireDataToStock,
            Func<CancellationToken, Task<TSource>> getDataFromDb,
            Func<TSource, TDestination> mapper,
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