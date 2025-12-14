using Rira.Persistence.Context;


namespace Rira.Persistence;

public class BaseUnitOfWork : IUnitOfWork
{
    private readonly RiraDbContext _context;

    public BaseUnitOfWork(RiraDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
    }

    public async Task AddRangeAsync<T>(List<T> entity, CancellationToken cancellationToken) where T : class
    {
        await _context.Set<T>().AddRangeAsync(entity, cancellationToken);
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Set<T>().Update(entity);
    }

    public async Task<T?> FindByIdAsync<T>(object id, CancellationToken cancellationToken) where T : class
    {
        return await _context.Set<T>().FindAsync(id, cancellationToken);
    }

    public IQueryable<T> GetAsQueryable<T>() where T : class
    {
        return _context.Set<T>().AsQueryable();
    }

    public virtual void Remove<T>(T entity) where T : class =>
        _context.Set<T>().Remove(entity);

    public int Commit()
    {
        return _context.SaveChanges();
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
