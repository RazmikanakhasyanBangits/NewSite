namespace Helper_s
{
    public interface IAbstractCaching
    {
        Task ClearAsync(string key);
        Task<T> GetAsync<T>(string key) where T : class;
        Task SetAsync<T>(string Key, T Value);
    }
}
