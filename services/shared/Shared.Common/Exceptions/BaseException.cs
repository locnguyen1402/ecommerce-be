namespace ECommerce.Shared.Common.Exceptions;
public class BaseException : Exception
{
    public int Status { get; private set; }
    public int Code { get; private set; }
    public string Title { get; set; } = string.Empty;
    public BaseException(string message, int statusCode) : base(message)
    {
        SetCode(statusCode, statusCode);
    }
    public void SetCode(int status, int code)
    {
        Status = status;
        Code = code;
    }
    public void SetTitle(string title)
    {
        Title = title;
    }
}