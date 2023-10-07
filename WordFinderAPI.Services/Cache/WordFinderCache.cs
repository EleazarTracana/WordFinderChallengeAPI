

using WordFinderAPI.Infrastructure.CacheProvider;
using WordFinderAPI.Interfaces.Cache;

namespace WordFinderAPI.Services.Cache;

public class WordFinderCache : IWordFinderCache
{
    private const string JoinMatrixDelimiter = "";
    private RedisCacheProvider _redisCacheProvider;

    public WordFinderCache(RedisCacheProvider redisCacheProvider)
    {
        _redisCacheProvider = redisCacheProvider;
    }
    public IEnumerable<string> Get(IEnumerable<string> matrix) => _redisCacheProvider.Get<IEnumerable<string>>(GetMatrixCacheKey(matrix));
    
    public Task Save(IEnumerable<string> matrix, IEnumerable<string> foundWords)
    {
        _redisCacheProvider.Set(GetMatrixCacheKey(matrix), matrix, null);
        throw new NotImplementedException();
    }

    private String GetMatrixCacheKey(IEnumerable<string> matrix) => string.Join(JoinMatrixDelimiter, matrix);
}