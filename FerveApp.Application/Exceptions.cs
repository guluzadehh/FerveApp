public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entityType, string identifierType, string identifierValue)
        : base($"The requested entity '{entityType}' with {identifierType.ToLower()} '{identifierValue}' was not found.")
    {

    }
}

public class BadRequestException : Exception
{
    public BadRequestException() { }
    public BadRequestException(string message) : base(message) { }
}

public class ConflictException : Exception
{
    public ConflictException() { }
    public ConflictException(string message) : base(message) { }
}

public class ForbiddenException : Exception
{
    public ForbiddenException() { }
    public ForbiddenException(string message) : base(message) { }
}

public class UnauthorizedException : Exception
{
    public UnauthorizedException() { }
    public UnauthorizedException(string message) : base(message) { }
}

public class InternalServerErrorException : Exception
{
    public InternalServerErrorException() { }
    public InternalServerErrorException(string message) : base(message) { }
}

public class ValidationException : Exception
{
    public ValidationException() { }
    public ValidationException(string message) : base(message) { }
}
