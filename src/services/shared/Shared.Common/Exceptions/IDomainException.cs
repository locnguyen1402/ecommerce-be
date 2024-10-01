namespace ECommerce.Shared.Common.Exceptions;

public interface IDomainException
{
    int StatusCode { get; }
    string ErrorCode { get; }
    string? Message { get; }
    public IDictionary<string, string[]>? Errors { get; }
}