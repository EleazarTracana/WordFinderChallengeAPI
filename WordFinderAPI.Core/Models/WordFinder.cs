namespace WordFinderAPI.Core.Models;

public class WordFinder
{
    public IEnumerable<string> Matrix { get; set; }
    public IEnumerable<string> WordStream { get; set; }
}