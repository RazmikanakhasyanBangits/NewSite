using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Helper_s.Implementations
{
    public class AbstractCaching : IAbstractCaching
    {
        private readonly IDistributedCache distributedCache;

        public AbstractCaching(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            var result = await distributedCache.GetStringAsync(key);

            if (result == null)
            {
                return null;
            }

            return JsonSerializer.Deserialize<T>(result,new JsonSerializerOptions() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull});
        }

        public async Task SetAsync<T>(string Key,T Value)
        {
            try
            {
                var setValue = JsonSerializer.Serialize(Value);
                await distributedCache.SetStringAsync(Key, setValue);
            }
            catch (Exception ex)
            {

                throw new InvalidDataException(ex.Message);
            }
        }

        public async Task ClearAsync(string key)
        {
            try
            {
                await distributedCache.RemoveAsync(key);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
