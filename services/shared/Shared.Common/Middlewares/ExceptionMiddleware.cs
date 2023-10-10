using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Shared.Common.Middlewares;
public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            if (exception is not BaseException && exception.InnerException != null)
            {
                while (exception.InnerException != null)
                {
                    exception = exception.InnerException;
                }
            }

            ProblemDetails problemDetails = null!;

            if (exception is BaseException except)
            {
                problemDetails = new ProblemDetails()
                {
                    Instance = context.Request.Path,
                    Status = except.Status,
                    Title = except.Title.IsNullOrEmpty() ? except.Message : except.Title,
                    Detail = except.Message,
                };
            }
            else
            {
                problemDetails = new ProblemDetails()
                {
                    Instance = context.Request.Path,
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Unexpected exception occurred",
                    Detail = exception.Message,
                };

            }

            response.StatusCode = (int)problemDetails.Status;
            await response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}