namespace Infrastructure.Repository;
public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void Delete(TEntity entity);

    void UpdateSwap(TEntity NewEntity, TEntity OldEntity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
}