using System.Linq.Expressions;

namespace Rent.DAL.RepositoryBase;

public interface IRepositoryBase<T>
{
    Task<IEnumerable<T>> GetAllAsync(
        params Expression<Func<T, object>>[] includes);
    Task<IEnumerable<T>> GetByConditionAsync(
        Expression<Func<T, bool>> expression,
        params Expression<Func<T, object>>[] includes);

    Task<T?> GetSingleByConditionAsync(
        Expression<Func<T, bool>> expression, 
        params Expression<Func<T, object>>[] includes);

    void Update(T entity);
    void Delete(T entity);
}