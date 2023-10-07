namespace WordFinderAPI.Infrastructure.CacheProvider;

public interface ICacheProvider
{
    T? Get<T>(string key);
    bool Set<T>(string key, T value, TimeSpan? expiry);
    bool Delete(string key);
}