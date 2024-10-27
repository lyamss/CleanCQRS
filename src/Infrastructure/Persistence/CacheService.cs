using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.Persistence
{
    internal sealed class CacheService(IDistributedCache cache) : ICacheService
    {
        private readonly IDistributedCache _cache = cache;

        private async Task<T> GetInCacheAsync<T>(string key, CancellationToken cancellationToken)
        {
            var json = await this._cache.GetStringAsync(key, cancellationToken);
            if(json == null)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(json);
        }

        private async Task SetInCacheAsync<T>(CancellationToken cancellationToken, string key, T value, TimeSpan? absoluteExpirationTime = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpirationTime ?? TimeSpan.FromMinutes(30)
            };
            var json = JsonSerializer.Serialize(value);
            await this._cache.SetStringAsync(key, json, options, cancellationToken);
        }

        public async Task<T> SetOrGetInDbOrCache<T>
        (
        string key,
        TimeSpan? expireDataToStock,
        Func<CancellationToken, Task<T>> getDataFromDb,
        CancellationToken cancellationToken
        )
        {
            T dataInCache = await this.GetInCacheAsync<T>(key, cancellationToken);
                if (dataInCache is null)
                {
                    T data = await getDataFromDb(cancellationToken);
                    await this.SetInCacheAsync(cancellationToken, key, data, expireDataToStock);
                    return data;
                }
            return dataInCache;
        }
    }
}