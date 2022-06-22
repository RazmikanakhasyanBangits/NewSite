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
        private readonly DbContext context;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IAbstractCaching abstractCaching;
        public GenericRepository(DbContext context, IServiceScopeFactory scopeFactory, IAbstractCaching abstractCaching)
        {
            this.context = context;
            this.scopeFactory = scopeFactory;
            this.abstractCaching = abstractCaching;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Func<T, bool> predicate)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<NewSiteContext>();
                var result= dbContext.Set<T>().Where(predicate);
                await abstractCaching.SetAsync(nameof(result), result);
                return result;
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
                var result = dbContext.Set<T>().Find(Id);
                abstractCaching.SetAsync(nameof(result), result);
                return result;
            }
        }

        public virtual async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<NewSiteContext>();
                var result = await dbContext.Set<T>().FirstOrDefaultAsync(predicate);
                await abstractCaching.SetAsync(nameof(result), result);
                return result;
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
                var result = await query.Where(filter).FirstOrDefaultAsync();
                await abstractCaching.SetAsync(nameof(result), result);
                return result;
            }
        }
        public virtual async Task AddAsync(T entity)
        {
            await abstractCaching.SetAsync(nameof(T), entity);
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<NewSiteContext>();
                await dbContext.Set<T>().AddAsync(entity);
                await dbContext.SaveChangesAsync();
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await abstractCaching.ClearAsync(nameof(T));
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<NewSiteContext>();
                dbContext.Set<T>().Update(entity);
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
