using Core.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Repositories
{
    public interface IAsyncRepository<T> : IQuery<T> where T : Entity
    {
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>,
                      IIncludableQueryable<T, object>>? include = null, bool enableTracking = false, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                       Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                       bool enableTracking = false, CancellationToken cancellationToken = default);

        Task<IPaginate<T>> GetListPaginateAsync(Expression<Func<T, bool>>? predicate = null,
                       Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                       Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                       int index = 0, int size = 10,
                       bool enableTracking = false, CancellationToken cancellationToken = default);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<List<T>> BulkAddAsync(List<T> entites, CancellationToken cancellationToken = default);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task<List<T>> BulkUpdateAsync(List<T> entities, CancellationToken cancellationToken = default);
        Task<T> DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, bool enableTracking = false, CancellationToken cancellationToken = default);
    }
}