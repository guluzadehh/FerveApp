namespace SharedKernel;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Failure(Error error) => new Result(false, error);

    public static Result Success() => new Result(true, Error.None);

    public static Result<TValue> Failure<TValue>(Error error) => new Result<TValue>(default, false, error);
    public static Result<TValue> Success<TValue>() => new Result<TValue>(default, true, Error.None);

    public static implicit operator Result(Error error) => new Result(false, error);
}


public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public TValue? Value => IsSuccess ? _value! : throw new InvalidOperationException("The failure result's value can't be accessed.");

    public static implicit operator Result<TValue>(TValue result) => new Result<TValue>(result, true, Error.None);
    public static implicit operator Result<TValue>(Error error) => Failure<TValue>(error);
}
