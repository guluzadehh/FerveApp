namespace SharedKernel;

public sealed record Error
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    private Error(string code, string message, ErrorType errorType)
    {
        Code = code;
        Message = message;
        Type = errorType;
    }

    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }

    public static Error Failure(string code, string message) =>
        new(code, message, ErrorType.Failure);

    public static Error NotFound(string code, string message) =>
        new(code, message, ErrorType.NotFound);

    public static Error Validation(string code, string message) =>
        new(code, message, ErrorType.Validation);

    public static Error Conflict(string code, string message) =>
        new(code, message, ErrorType.Conflict);
}

public enum ErrorType
{
    Failure,
    Validation,
    NotFound,
    Conflict
}