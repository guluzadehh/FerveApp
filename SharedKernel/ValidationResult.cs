namespace SharedKernel;

public class ValidationResult : Result, IValidationResult
{
    public static readonly Error ValidationError = Error.Validation(
        "ValidationError",
        "There is a validation problem"
    );

    private ValidationResult(Error[] erorrs)
        : base(false, ValidationError)
    {
        Errors = erorrs;
    }


    public Error[] Errors { get; }

    public static ValidationResult Create(Error[] errors) => new(errors);
    public static ValidationResult<TValue> Create<TValue>(Error[] errors) => new(errors);

}

public class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    public Error[] Errors { get; }

    protected internal ValidationResult(Error[] erorrs)
        : base(default, false, ValidationResult.ValidationError)
    {
        Errors = erorrs;
    }

}
