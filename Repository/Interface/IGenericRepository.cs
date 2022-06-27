using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Repository.Interface
{
    public interface IGenericRepository<T>
    {
        T Get(object Id);
        void Delete(T entity);
        IEnumerable<T> GetAll();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync(Func<T, bool> predicate);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null, bool? disableTracking = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null, bool? disableTracking = null);
    }
}
