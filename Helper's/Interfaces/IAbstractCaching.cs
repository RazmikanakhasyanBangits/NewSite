namespace Helper_s
{
    public interface IAbstractCaching
    {
        Task ClearAsync(string key);
        Task SetAsync<T>(string Key, T Value);
        Task<T> GetAsync<T>(string key) where T : class;
    }
}
