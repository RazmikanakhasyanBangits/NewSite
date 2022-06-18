using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NewSite.Repository.Abstraction;
using System.Linq.Expressions;

namespace NewSite.Repository.Impl
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class 
    {
        private readonly DbContext context;

        public GenericRepository(DbContext context)
        {
            this.context = context;
        }

        public  IEnumerable<T> GetAll(Func<T,bool> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }

        public T Get(object Id)
        {
            return context.Set<T>().Find(Id);
        }

        public async Task<T> Get(Func<T,bool> predicate)
        {
            return  context.Set<T>().FirstOrDefault(predicate);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, bool disableTracking)
        {
            var query = context.Set<T>().AsQueryable();

            if (disableTracking)
                query = query.AsNoTracking();

            if (includes != null)
                query = includes(query).IgnoreAutoIncludes();

            return await query.Where(filter).FirstOrDefaultAsync();
        }
        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
           await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
           await context.SaveChangesAsync();
        }

    }
}
