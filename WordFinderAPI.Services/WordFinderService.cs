
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

        public async Task<IEnumerable<string>> FindAsync(WordFinder wordFinder)
        {
            _wordFinderValidator.Validate(wordFinder, options =>
            {
                options.IncludeRuleSets(ValidationType.WordFinder);
                options.ThrowOnFailures();
            });

            IEnumerable<string>? wordsFoundInCache = _wordFinderCache.Get(wordFinder);
            if (wordsFoundInCache is not null)
                return wordsFoundInCache;

            _logger.LogDebug("Request to find wordStream {}, in matrix: {}", wordFinder.WordStream, wordFinder.Matrix);

            IEnumerable<string> cleanedWordStream = GetUniqueWordStream(wordFinder.WordStream);
            IEnumerable<string> findWords = await FindWords(wordFinder.Matrix, cleanedWordStream);
                
            IEnumerable<string> latestTopFoundWords = findWords
                .GroupBy(word => word)
                .OrderByDescending(group => group.Count())
                .Select(group => group.Key)
                .Take(LatestTopWords);

            _wordFinderCache.Save(wordFinder, latestTopFoundWords);
            return latestTopFoundWords;
        }
        

        private IEnumerable<string> GetUniqueWordStream(IEnumerable<string> wordStream) => wordStream.Distinct();
        private async Task<IEnumerable<string>> FindWords(IEnumerable<string> matrix, IEnumerable<string> wordStream)
        {
            Task<IEnumerable<string>> horizontalSearchTask = Task.Run(() => FindLeftToRightHorizontally(matrix, wordStream));
            Task<IEnumerable<string>> verticalSearchTask = Task.Run(() => FindTopToBottomVertically(matrix, wordStream));

            await Task.WhenAll(horizontalSearchTask, verticalSearchTask);
            
            IEnumerable<string> horizontalWords = horizontalSearchTask.Result;
            IEnumerable<string> verticalWords = verticalSearchTask.Result;

            return horizontalWords.Concat(verticalWords);
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

        private IEnumerable<string> FindTopToBottomVertically(IEnumerable<string> matrix,
            IEnumerable<string> wordStream)
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
    }
}
