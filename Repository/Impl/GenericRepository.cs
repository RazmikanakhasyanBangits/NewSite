using Helper_s;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interface;
using System.Linq.Expressions;

namespace Repository.Impl
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IServiceScopeFactory scopeFactory;
        public GenericRepository(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Func<T, bool> predicate)
        {
            using (var scope =  scopeFactory.CreateScope())
            {
                var dbContext =  scope.ServiceProvider.GetService<NewSiteContext>();
                return  dbContext.Set<T>().Where(predicate);

            }
        }
        public virtual IEnumerable<T> GetAll()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<NewSiteContext>();
                return dbContext.Set<T>();
            }
        }
        public virtual T Get(object Id)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<NewSiteContext>();
                return dbContext.Set<T>().Find(Id);
            }
        }
        public virtual async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<NewSiteContext>();
                return await dbContext.Set<T>().FirstOrDefaultAsync(predicate);
            }
        }
        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes=null, bool? disableTracking=null)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<NewSiteContext>();
                var query = dbContext.Set<T>().AsQueryable();

                if (disableTracking == true)
                    query = query.AsNoTracking();

                if (includes != null)
                    query = includes(query).IgnoreAutoIncludes();
                var result = await query.Where(filter).FirstOrDefaultAsync();
                return result;
            }
        }
        public virtual async Task AddAsync(T entity)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<NewSiteContext>();
                await dbContext.Set<T>().AddAsync(entity);
                await dbContext.SaveChangesAsync();
            }
        }
        public virtual async Task UpdateAsync(T entity)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<NewSiteContext>();
                dbContext.Set<T>().Update(entity);
                await dbContext.SaveChangesAsync();
            }
        }
        public virtual async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<NewSiteContext>();
                var entity = await GetAsync(predicate);

                dbContext.Set<T>().Remove(entity);
                dbContext.SaveChanges();
            }
        }
        public virtual void Delete(T entity)
        {
            using(var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<NewSiteContext>();
                dbContext.Set<T>().Remove(entity);
                dbContext.SaveChanges();
            }
        }
    }
}
