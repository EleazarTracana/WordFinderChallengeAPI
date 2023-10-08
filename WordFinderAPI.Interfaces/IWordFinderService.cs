using WordFinderAPI.Core.Models;

namespace WordFinderAPI.Interfaces;

/// <summary>
/// Represents a service for finding words within a matrix using various search patterns.
/// </summary>
public interface IWordFinderService
{
    /// <summary>
    /// Finds and returns a list of words from the provided matrix based on the given search patterns.
    /// </summary>
    /// <param name="wordFinder">The WordFinder object containing matrix and search patterns.</param>
    /// <returns>An IEnumerable containing the found words.</returns>
    Task<IEnumerable<string>> FindAsync(WordFinder wordFinder);
}