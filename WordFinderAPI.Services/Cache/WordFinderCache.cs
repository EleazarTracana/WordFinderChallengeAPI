

using WordFinderAPI.Infrastructure.CacheProvider;
using WordFinderAPI.Interfaces.Cache;

namespace WordFinderAPI.Services.Cache;

public class WordFinderCache : IWordFinderCache
{
    private const string JoinMatrixDelimiter = "";
    private ICacheProvider _cacheProvider;

    public WordFinderCache(ICacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }
    public IEnumerable<string> Get(IEnumerable<string> matrix) => _cacheProvider.Get<IEnumerable<string>>(GetMatrixCacheKey(matrix));
    
    public Task Save(IEnumerable<string> matrix, IEnumerable<string> foundWords)
    {
        _cacheProvider.Set(GetMatrixCacheKey(matrix), matrix, null);
        throw new NotImplementedException();
    }

    private String GetMatrixCacheKey(IEnumerable<string> matrix) => string.Join(JoinMatrixDelimiter, matrix);
}