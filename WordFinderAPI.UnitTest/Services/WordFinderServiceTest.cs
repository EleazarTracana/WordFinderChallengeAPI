using FluentValidation;
using Moq;
using WordFinderAPI.Interfaces;
using WordFinderAPI.Interfaces.Cache;
using Microsoft.Extensions.Logging;
using WordFinderAPI.Core.Models;
using WordFinderAPI.Services;
using Xunit;

namespace WordFinderAPI.UnitTest.Services;

public class WordFinderServiceTest
{
    private readonly Mock<IWordFinderCache> _worldFinderCacheServiceMock;
    private readonly Mock<ILogger<WordFinderService>> _loggerMock;
    private readonly Mock<IValidator<WordFinder>> _wordFinderValidatorMock;
    private readonly IWordFinderService _wordFinderService;

    public WordFinderServiceTest()
    {
        _loggerMock = new Mock<ILogger<WordFinderService>>();
        _wordFinderValidatorMock = new Mock<IValidator<WordFinder>>();
        _worldFinderCacheServiceMock = new Mock<IWordFinderCache>();
        _wordFinderService = new WordFinderService(_worldFinderCacheServiceMock.Object, _loggerMock.Object, _wordFinderValidatorMock.Object);
    }

    [Fact]
    public void FindWords_ShouldReturnEmpty()
    {
        WordFinder wordFinder = new WordFinder
        {
            Matrix = new List<string>
            {
                "abcdefg",
                "hijklmn",
                "opqrstu"
            },
            WordStream = new List<string>
            {
                "xyz",
                "123"
            }
        };
        
        var result = _wordFinderService.Find(wordFinder);
        Assert.Empty(result);
    }

    [Fact]
    public void FindWords_ShouldReturnHorizontalWords()
    {
        WordFinder wordFinder = new WordFinder
        {
            Matrix = new List<string>
            {
                "abcxyzdef",
                "ghixyzklm",
                "nopxyzqrstu"
            },
            WordStream = new List<string>
            {
                "xyz",
                "def",
                "klm"
            }
        };
        
        IEnumerable<string> result = _wordFinderService.Find(wordFinder);
        Assert.Equal(3, result.Count());
        Assert.Equal("xyz", result.First());
        Assert.Equal("def", result.Skip(1).First());
        Assert.Equal("klm", result.Last());
    }
    
    [Fact]
    public void FindWords_ShouldReturnVerticalWords()
    {
        WordFinder wordFinder = new WordFinder
        {
            Matrix = new List<string>
            {
                "abcx",
                "defy",
                "ghiz",
                "jkjm"
            },
            WordStream = new List<string>
            {
                "xyz",
                "fij"
            }
        };
        IEnumerable<string> result = _wordFinderService.Find(wordFinder);
        Assert.Equal(2, result.Count());
        Assert.Contains("xyz", result);
        Assert.Contains("fij", result);
    }
}