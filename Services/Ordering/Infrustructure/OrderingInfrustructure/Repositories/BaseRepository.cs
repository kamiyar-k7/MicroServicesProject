
using Microsoft.EntityFrameworkCore;
using OrderingApplication.Contracts.Persistence;
using OrderingDomain.Common;
using OrderingInfrastructure.Persistence;
using System.Linq.Expressions;

namespace OrderingInfrastructure.Repositories;

public class BaseRepository<T> : IAsyncRepository<T> where T : EntityBase
{

    #region Ctor

    protected readonly OrderDbContext _dbContext;
    private   DbSet<T> _query;

    public BaseRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
        _query = _dbContext.Set<T>();
    }

    #endregion


    public async Task<IReadOnlyList<T>> GetAllAsync( CancellationToken cancellationToken)
    {
        return await _query.ToListAsync(cancellationToken);
    }
    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await _query.Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
    {

        var query = _query.AsQueryable();

        if (disableTracking == true)
        {
            query = query.AsNoTracking();
        }

        if (!string.IsNullOrEmpty(includeString))
        {
            query = query.Include(includeString);
        }

        if(predicate != null)
        {
            query = query.Where(predicate);
        }

        if(orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> include = null, bool disableTracking = true)
    {
        var query = _query.AsQueryable();
        if (disableTracking == true)
        {
            query = query.AsNoTracking();
        }

        if (include != null)
        {
            query = include.Aggregate(query, (current, include) => current.Include(include));
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }

        return await query.ToListAsync();
    }



    public virtual async Task<T> GetByIdAsync(int Id)
    {
       return await _query.FindAsync(Id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _query.AddAsync(entity);
       await _dbContext.SaveChangesAsync();
        return entity;
    }
    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _query.Remove(entity);
      //  await _query.ExecuteDeleteAsync();
     await   _dbContext.SaveChangesAsync();
    }



}
