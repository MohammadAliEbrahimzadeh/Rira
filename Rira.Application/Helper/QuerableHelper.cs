using Ardalis.GuardClauses;
using System.Linq;

namespace Rira.Application.Helper;

public static class QueryableHelper
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int page, int size)
    {
        Guard.Against.NegativeOrZero(page, nameof(page), "Page number must be greater than zero.");
        Guard.Against.NegativeOrZero(size, nameof(size), "Page size must be greater than zero.");

        return query.Skip((page - 1) * size).Take(size);
    }

    public static IQueryable<T> PaginateWithCount<T>(this IQueryable<T> query, int page, int size, out int totalCount)
    {
        Guard.Against.NegativeOrZero(page, nameof(page), "Page number must be greater than zero.");
        Guard.Against.NegativeOrZero(size, nameof(size), "Page size must be greater than zero.");

        totalCount = query.Count();

        return query.Skip((page - 1) * size).Take(size);
    }
}
