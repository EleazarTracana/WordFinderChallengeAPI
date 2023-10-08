using WordFinderAPI.Core.Models;

namespace WordFinderAPI.Interfaces.Cache;

/// <summary>
/// Defines an interface for caching word search results based on matrix input.
/// </summary>
public interface IWordFinderCache
{
    /// <summary>
    /// Retrieves cached word search results for a given matrix.
    /// </summary>
    /// <param name="matrix">The matrix for which to retrieve cached results.</param>
    /// <returns>An IEnumerable containing cached word search results.</returns>
    IEnumerable<string>? Get(WordFinder wordFinder);

    /// <summary>
    /// Saves word search results for a given matrix in the cache.
    /// </summary>
    /// <param name="matrix">The matrix for which to save results in the cache.</param>
    /// <param name="foundWords">The list of found words to be cached.</param>
    /// <returns>A Task representing the asynchronous save operation.</returns>
    Task Save(WordFinder wordFinder, IEnumerable<string> foundWords);
}
