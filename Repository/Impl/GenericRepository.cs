using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interface;
using System.Linq.Expressions;

namespace Repository.Impl
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext context;
        private readonly IServiceScopeFactory scopeFactory;
        public GenericRepository(DbContext context, IServiceScopeFactory scopeFactory)
        {
            this.context = context;
            this.scopeFactory = scopeFactory;
        }

        public virtual IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<NewSiteContext>();
                return dbContext.Set<T>().Where(predicate);
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

        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, bool disableTracking)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<NewSiteContext>();
                var query = dbContext.Set<T>().AsQueryable();

                if (disableTracking)
                    query = query.AsNoTracking();

                if (includes != null)
                    query = includes(query).IgnoreAutoIncludes();

                return await query.Where(filter).FirstOrDefaultAsync();
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

    }
}
