using WordFinderAPI.Core.Models;

namespace WordFinderAPI.Interfaces;

public interface IWordFinderService
{
    IEnumerable<string> Find(WordFinder wordFinder);
}