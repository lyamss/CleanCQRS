using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly BackendDbContext _context;

    public Repository(BackendDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().FindAsync(id, cancellationToken);
    }

    public void UpdateSwap(TEntity NewEntity, TEntity OldEntity) 
    {
        NewEntity = OldEntity;
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void Delete(TEntity entity);

    void UpdateSwap(TEntity NewEntity, TEntity OldEntity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
}