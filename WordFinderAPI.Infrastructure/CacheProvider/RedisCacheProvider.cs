using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace WordFinderAPI.Infrastructure.CacheProvider;

public class RedisCacheProvider : ICacheProvider
{
    private readonly IDatabase _database;
    public RedisCacheProvider(IOptions<RedisOptions> redis)
    {
        _database = ConnectionMultiplexer.Connect(
            new ConfigurationOptions
            {
                EndPoints = {redis.Value.Server }
            }).GetDatabase();
    }

    public T? Get<T>(string key)
    {
        RedisValue value = _database.StringGet(key);
        return value.IsNullOrEmpty ? default(T?) : JsonConvert.DeserializeObject<T>(value);
    }

    public bool Set<T>(string key, T value, TimeSpan? expiry)
    {
        return _database.StringSet(key, JsonConvert.SerializeObject(value), expiry);
    }

    public bool Delete(string key)
    {
        return _database.KeyDelete(key);
    }
}