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
        //
    }

    [Fact]
    public void FindWords_ShouldReturnMultipleRepeated()
    {
        //
    }
}