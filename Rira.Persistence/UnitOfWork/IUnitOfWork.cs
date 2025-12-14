
namespace Rira.Persistence;

public interface IUnitOfWork
{
    int Commit();

    Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));

    Task AddAsync<T>(T entity, CancellationToken cancellationTokenn) where T : class;

    Task AddRangeAsync<T>(List<T> entity, CancellationToken cancellationTokenn) where T : class;

    Task<T?> FindByIdAsync<T>(object id, CancellationToken cancellationToken) where T : class;

    void Remove<T>(T entity) where T : class;

    IQueryable<T> GetAsQueryable<T>() where T : class;

    void Update<T>(T entity) where T : class;
}
