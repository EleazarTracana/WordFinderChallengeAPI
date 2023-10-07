
using FluentValidation;
using Microsoft.Extensions.Logging;
using WordFinderAPI.Core.Models;
using WordFinderAPI.Interfaces;
using WordFinderAPI.Interfaces.Cache;
using WordFinderAPI.Services.Validator;


namespace WordFinderAPI.Services
{
    /// <summary>
    /// Provides functionality for finding words within a matrix using various search patterns.
    /// </summary>
    public class WordFinderService : IWordFinderService
    {
        private const int LatestTopWords = 10;
        private readonly IWordFinderCache _wordFinderCache;
        private readonly IValidator<WordFinder> _wordFinderValidator;
        private readonly ILogger<WordFinderService> _logger;

        public WordFinderService(IWordFinderCache wordFinderCache, 
            ILogger<WordFinderService> logger,
            IValidator<WordFinder> wordFinderValidator)
        {
            _wordFinderValidator = wordFinderValidator;
            _wordFinderCache = wordFinderCache;
            _logger = logger;
        }

        public IEnumerable<string> Find(WordFinder wordFinder)
        {
            _wordFinderValidator.Validate(wordFinder, options =>
            {
                options.IncludeRuleSets(ValidationType.WordFinder);
                options.ThrowOnFailures();
            });
            
            _logger.LogDebug("Request to find wordsStream {}, in matrix: {}", wordFinder.WordStream, wordFinder.Matrix);

            IEnumerable<string> cleanedWordStream = GetUniqueWordStream(wordFinder.WordStream);
            IEnumerable<string> latestTopFoundWords = FindWords(wordFinder.Matrix, cleanedWordStream)
                .GroupBy(word => word)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .Take(LatestTopWords);

            _wordFinderCache.Save(wordFinder.Matrix, latestTopFoundWords);
            return latestTopFoundWords;
        }
        

        private IEnumerable<string> GetUniqueWordStream(IEnumerable<string> wordStream) => wordStream.Distinct();
        private IEnumerable<string> FindWords(IEnumerable<string> matrix, IEnumerable<string> wordStream)
        {
            IEnumerable<string> wordsFoundFromLeftToRightHorizontally = FindLeftToRightHorizontally(matrix, wordStream);
            IEnumerable<string> wordsFoundFromTopToBottomVertically = FindTopToBottomVertically(matrix, wordStream);
            return wordsFoundFromLeftToRightHorizontally.Concat(wordsFoundFromTopToBottomVertically);
        }

        private IEnumerable<string> FindLeftToRightHorizontally(IEnumerable<string> matrix, IEnumerable<string> wordStream)
        {
            foreach (var word in wordStream)
            {
                foreach (var line in matrix)
                {
                    if (line.Contains(word))
                    {
                        yield return word;
                        break;
                    }
                }
            }
        }

        private IEnumerable<string> FindTopToBottomVertically(IEnumerable<string> matrix, IEnumerable<string> wordStream)
        {
            int matrixStringLength = matrix.First().Length;
            for (int col = 0; col < matrixStringLength; col++)
            {
                string column = new string(matrix.Select(row => row[col]).ToArray());
                foreach (var word in wordStream)
                {
                    if (column.Contains(word))
                    {
                        yield return word;
                        break;
                    }
                }
            }
        }

        private static bool IsNullOrEmpty<T>(IEnumerable<T> collection) => collection == null || !collection.Any();
    }
}
