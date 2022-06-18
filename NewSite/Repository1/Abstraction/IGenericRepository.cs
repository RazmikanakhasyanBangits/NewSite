using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace NewSite.Repository.Abstraction
{
    public interface IGenericRepository<T>
    {
        Task AddAsync(T entity);
        T Get(object Id);
        Task<T> Get(Func<T, bool> predicate);
        IEnumerable<T> GetAll(Func<T, bool> predicate);
        IEnumerable<T> GetAll();
        Task<T> GetAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, bool disableTracking);
        Task UpdateAsync(T entity);
    }
}
