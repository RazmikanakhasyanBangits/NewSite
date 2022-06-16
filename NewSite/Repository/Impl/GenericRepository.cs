using Microsoft.EntityFrameworkCore;
using NewSite.Repository.Abstraction;

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

        public async Task AddAsync(T entity)
        {
            context.Set<T>().Add(entity);
           await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
           await context.SaveChangesAsync();
        }

    }
}
