using FluentValidation;
using WordFinderAPI.Core.Models;

namespace WordFinderAPI.Services.Validator;

/// <summary>
///  Class for business validation rules for WordFinder entity.
/// </summary>
public class WordFinderValidator : AbstractValidator<WordFinder>
{
    public WordFinderValidator()
    {
        RuleSet(ValidationType.WordFinder, () =>
        {
            RuleFor(x => x.Matrix)
                .NotEmpty()
                .WithErrorCode("MatrixNotEmpty")
                .WithMessage("The matrix must not be empty.")
                .WithName("Matrix");
            
            RuleFor(x => x.WordStream)
                .NotEmpty()
                .WithErrorCode("WordStreamNotEmpty")
                .WithMessage("The word stream must not be empty.")
                .WithName("WordStream");
            
            RuleFor(x => x.Matrix)
                .Must(matrix => matrix.All(s => s.Length == matrix.First().Length))
                .WithErrorCode("MatrixInvalidLength")
                .WithMessage("All strings in the matrix must have the same length.")
                .WithName("Matrix");
        });
    }
}