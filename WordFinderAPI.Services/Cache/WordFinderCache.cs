using WordFinderAPI.Infrastructure.CacheProvider;
using WordFinderAPI.Interfaces.Cache;

namespace WordFinderAPI.Services.Cache;

/// <summary>
/// Provides caching functionality for storing and retrieving word search results based on matrix input.
/// </summary>
public class WordFinderCache : IWordFinderCache
{
    private const string JoinMatrixDelimiter = "";
    private readonly ICacheProvider _cacheProvider;

    public WordFinderCache(ICacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }
    public IEnumerable<string> Get(IEnumerable<string> matrix) => _cacheProvider.Get<IEnumerable<string>>(GetMatrixCacheKey(matrix));
    
    public Task Save(IEnumerable<string> matrix, IEnumerable<string> foundWords)
    {
        _cacheProvider.Set(GetMatrixCacheKey(matrix), matrix, null);
        return Task.CompletedTask;
    }

    private String GetMatrixCacheKey(IEnumerable<string> matrix) => string.Join(JoinMatrixDelimiter, matrix);
}