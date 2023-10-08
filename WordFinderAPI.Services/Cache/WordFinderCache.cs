using WordFinderAPI.Core.Models;
using WordFinderAPI.Infrastructure.CacheProvider;
using WordFinderAPI.Interfaces.Cache;

namespace WordFinderAPI.Services.Cache;

/// <summary>
/// Provides caching functionality for storing and retrieving word search results based on matrix input.
/// </summary>
public class WordFinderCache : IWordFinderCache
{
    private const string JoinMatrixDelimiter = "_"; // Change the delimiter as needed
    private readonly ICacheProvider _cacheProvider;

    public WordFinderCache(ICacheProvider cacheProvider)
    {
        _cacheProvider = cacheProvider;
    }

    public IEnumerable<string>? Get(WordFinder wordFinder)
    {
        return _cacheProvider.Get<IEnumerable<string>?>(GetMatrixCacheKey(wordFinder.Matrix, wordFinder.WordStream));
    }

    public Task Save(WordFinder wordFinder, IEnumerable<string> foundWords)
    {
        _cacheProvider.Set(GetMatrixCacheKey(wordFinder.Matrix, wordFinder.WordStream), foundWords, null);
        return Task.CompletedTask;
    }

    private string GetMatrixCacheKey(IEnumerable<string> matrix, IEnumerable<string> wordStream)
    {
        string matrixKey = string.Join(JoinMatrixDelimiter, matrix);
        string wordStreamKey = string.Join(JoinMatrixDelimiter, wordStream);
        return $"{matrixKey}_{wordStreamKey}";
    }
}