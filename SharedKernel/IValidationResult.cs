namespace SharedKernel;

public interface IValidationResult
{
    Error[] Errors { get; }
}
