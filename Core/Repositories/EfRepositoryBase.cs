using Core.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Repositories
{
    public class EfRepositoryBase<TEntity, TContext> : IAsyncRepository<TEntity>
      where TEntity : Entity
      where TContext : DbContext
    {
        protected TContext Context { get; }

        public EfRepositoryBase(TContext context)
        {
            Context = context;
        }
        public async Task<IQueryable<TEntity>> Query()
        {
            return Context.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Context.Entry(entity).State = EntityState.Added;
            await Context.SaveChangesAsync(cancellationToken);
            return entity;
        }
        public async Task<List<TEntity>> BulkAddAsync(List<TEntity> entites, CancellationToken cancellationToken = default)
        {
            Context.Entry(entites).State = EntityState.Added;
            await Context.SaveChangesAsync(cancellationToken);
            return entites;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            await Context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>,
                           IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = await Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<IPaginate<TEntity>> GetListPaginateAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool enableTracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = await Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            if (orderBy != null)
                return await orderBy(queryable).ToPaginateAsync(index, size, cancellationToken: cancellationToken);
            return await queryable.ToPaginateAsync(index, size, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool enableTracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = await Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            if (orderBy != null)
            {
                return await orderBy(queryable).ToListAsync(cancellationToken);
            }
            return await queryable.ToListAsync(cancellationToken);

        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, bool enableTracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = await Query();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (predicate != null) queryable = queryable.Where(predicate);
            return await queryable.CountAsync(cancellationToken);

        }

        public async Task<List<TEntity>> BulkUpdateAsync(List<TEntity> entities, CancellationToken cancellationToken = default)
        {
            Context.UpdateRange(entities);
            await Context.SaveChangesAsync(cancellationToken);
            return entities;
        }

    }
}