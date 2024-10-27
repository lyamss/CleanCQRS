namespace Infrastructure.Persistence
{
    public interface ICacheService
    {
        Task<T> SetOrGetInDbOrCache<T>(
                string key,
                TimeSpan? expireDataToStock,
                Func<CancellationToken, Task<T>> getDataFromDb,
                CancellationToken cancellationToken);
    }
}