namespace WordFinderAPI.Interfaces.Cache;

public interface IWordFinderCache
{
    IEnumerable<string> Get(IEnumerable<string> matrix);

    Task Save(IEnumerable<string> matrix, IEnumerable<string> foundWords);
}