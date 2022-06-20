using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Repository.Interface;
using System.Linq.Expressions;

namespace Repository.Impl
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext context;
        protected DbContext Context => Context;
        public GenericRepository(DbContext context)
        {
            this.context = context;
        }

        public virtual IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }

        public virtual T Get(object Id)
        {
            return context.Set<T>().Find(Id);
        }

        public virtual async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, bool disableTracking)
        {
            var query = context.Set<T>().AsQueryable();

            if (disableTracking)
                query = query.AsNoTracking();

            if (includes != null)
                query = includes(query).IgnoreAutoIncludes();

            return await query.Where(filter).FirstOrDefaultAsync();
        }
        public virtual async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();
        }

    }
}
