using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Rent.DAL.RepositoryBase;

public class RepositoryBase<T>(RentContext context) : IRepositoryBase<T>
    where T : class
{
    protected readonly RentContext Context = context;

    public async Task<IEnumerable<T>> GetAllAsync(
        params Expression<Func<T, object>>[] includes)
    {
        var query = Context
            .Set<T>()
            .AsQueryable();

        return await includes
            .Aggregate(query, (current, next) => current.Include(next))
            .ToListAsync();
    }

    public async Task<IEnumerable<T>> GetByConditionAsync(
        Expression<Func<T, bool>> expression,
        params Expression<Func<T, object>>[] includes)
    {
        var query = Context
            .Set<T>()
            .Where(expression)
            .AsQueryable();

        return await includes
            .Aggregate(query, (current, next) => current.Include(next))
            .ToListAsync();
    }

    //public void Create(T entity) => Context.Set<T>().Add(entity);
    public void Update(T entity) => Context.Set<T>().Update(entity);
    public void Delete(T entity) => Context.Set<T>().Remove(entity);
}