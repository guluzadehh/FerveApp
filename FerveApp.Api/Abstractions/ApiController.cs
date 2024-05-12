using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace FerveApp.Api.Abstractions;

public abstract class ApiController : ControllerBase
{
    protected readonly ISender _sender;

    protected ApiController(ISender sender)
    {
        _sender = sender;
    }

    protected ActionResult HandleFailure(Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        var problemDetails = new ProblemDetails
        {
            Title = GetTitle(result.Error.Type),
            Type = result.Error.Code,
            Status = GetStatusCode(result.Error.Type),
            Detail = result.Error.Message
        };

        if (result is IValidationResult validationResult)
        {
            problemDetails.Extensions["errors"] = validationResult.Errors;
        }

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
    }

    private static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.NotFound => "Not Found",
            ErrorType.Validation => "Bad Request",
            ErrorType.Conflict => "Conflict Error",
            _ => "Internal Error"
        };
}
