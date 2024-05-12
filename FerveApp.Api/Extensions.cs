using SharedKernel;

namespace FerveApp.Api;

public static class Extensions
{
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            statusCode: GetStatusCode(result.Error.Type),
            title: GetTitle(result.Error.Type),
            extensions: new Dictionary<string, object?>
            {
                {"errors", new[] {
                    new {
                        message=result.Error.Message,
                        code=result.Error.Code
                    }
                }}
            }
        );
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
