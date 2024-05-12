using FluentValidation;
using MediatR;
using SharedKernel;

namespace FerveApp.Application;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        Error[] errors = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(validationResult => validationResult.Errors)
            .Where(validationFailure => validationFailure != null)
            .Select(failure => Error.Validation(failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errors.Length > 0)
        {
            return MakeValidationResult(errors);
        }

        return await next();
    }

    private static TResponse MakeValidationResult(Error[] errors)
    {
        var responseType = typeof(TResponse);

        if (!responseType.IsGenericType)
        {
            return (ValidationResult.Create(errors) as TResponse)!;
        }

        object validationResult = typeof(ValidationResult)
            .GetMethods()
            .Where(m => m.Name == nameof(ValidationResult.Create))
            .Where(m => m.IsGenericMethod)
            .First()
            .MakeGenericMethod(typeof(TResponse).GenericTypeArguments[0])
            .Invoke(null, new object?[] { errors })!;

        return (validationResult as TResponse)!;
    }
}
