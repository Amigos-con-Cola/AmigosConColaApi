using ErrorOr;
using FluentValidation.Results;

namespace AmigosConCola.Core.Extensions;

public static class ValidationResultExtensions
{
    public static List<Error> ToErrors(this ValidationResult validationResult)
    {
        var errors = validationResult.Errors.ConvertAll(
            error => Error.Validation(
                error.PropertyName,
                error.ErrorMessage));
        return errors;
    }
}