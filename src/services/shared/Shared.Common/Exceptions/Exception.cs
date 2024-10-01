using Microsoft.AspNetCore.Http;

namespace ECommerce.Shared.Common.Exceptions;

public class BadRequestException : DomainException
{
    public BadRequestException(string errorCode, string message)
        : base(StatusCodes.Status400BadRequest, errorCode, message)
    {
    }

    public BadRequestException(string errorCode, string message, IDictionary<string, string[]> errors)
        : base(StatusCodes.Status400BadRequest, errorCode, message, errors)
    {
    }
}

public class UnauthorizedException : DomainException
{
    public UnauthorizedException(string errorCode, string message)
        : base(StatusCodes.Status401Unauthorized, errorCode, message)
    {
    }

    public UnauthorizedException(string errorCode, string message, IDictionary<string, string[]> errors)
        : base(StatusCodes.Status401Unauthorized, errorCode, message, errors)
    {
    }
}

public class PaymentRequiredException : DomainException
{
    public PaymentRequiredException(string errorCode, string message)
        : base(StatusCodes.Status402PaymentRequired, errorCode, message)
    {
    }

    public PaymentRequiredException(string errorCode, string message, IDictionary<string, string[]> errors)
        : base(StatusCodes.Status402PaymentRequired, errorCode, message, errors)
    {
    }
}

public class ForbiddenException : DomainException
{
    public ForbiddenException(string errorCode, string message)
        : base(StatusCodes.Status403Forbidden, errorCode, message)
    {
    }

    public ForbiddenException(string errorCode, string message, IDictionary<string, string[]> errors)
        : base(StatusCodes.Status403Forbidden, errorCode, message, errors)
    {
    }
}

public class NotFoundException : DomainException
{
    public NotFoundException(string errorCode, string message)
        : base(StatusCodes.Status404NotFound, errorCode, message)
    {
    }

    public NotFoundException(string errorCode, string message, IDictionary<string, string[]> errors)
        : base(StatusCodes.Status404NotFound, errorCode, message, errors)
    {
    }
}

public class ConflictException : DomainException
{
    public ConflictException(string errorCode, string message)
        : base(StatusCodes.Status409Conflict, errorCode, message)
    {
    }

    public ConflictException(string errorCode, string message, IDictionary<string, string[]> errors)
        : base(StatusCodes.Status409Conflict, errorCode, message, errors)
    {
    }
}

public class InternalServerErrorException : DomainException
{
    public InternalServerErrorException(string errorCode, string message)
        : base(StatusCodes.Status500InternalServerError, errorCode, message)
    {
    }

    public InternalServerErrorException(string errorCode, string message, IDictionary<string, string[]> errors)
        : base(StatusCodes.Status500InternalServerError, errorCode, message, errors)
    {
    }
}

