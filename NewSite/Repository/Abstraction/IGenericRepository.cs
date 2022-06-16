namespace NewSite.Repository.Abstraction
{
    public interface IGenericRepository<T>
    {
        Task AddAsync(T entity);
        T Get(object Id);
        Task<T> Get(Func<T, bool> predicate);
        IEnumerable<T> GetAll(Func<T, bool> predicate);
        IEnumerable<T> GetAll();
        Task UpdateAsync(T entity);
    }
}
