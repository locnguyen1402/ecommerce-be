using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using ECommerce.Shared.Common.Exceptions;

namespace ECommerce.Shared.Common.Extensions;

public static class PipelineExtensions
{
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app, string module)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                ProblemDetails problemDetails = new()
                {
                    Title = "Internal Server Error",
                    Detail = "An error occurred while processing your request.",
                    Instance = context.Request.Path
                };

                problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
                problemDetails.Extensions.Add("module", module);

                if (context.Request.Headers.TryGetValue("x-request-id", out var requestId) && !string.IsNullOrEmpty(requestId))
                    problemDetails.Extensions.Add("requestId", requestId);

                if (context.RequestServices.GetService<IProblemDetailsService>() is not { } problemDetailsService)
                    return;

                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (exceptionHandlerFeature?.Error is not IDomainException exception)
                    return;

                context.Response.StatusCode = exception.StatusCode;
                problemDetails.Status = exception.StatusCode;
                problemDetails.Title = exception.Message;
                if (exception.Errors != null && exception.Errors.Any())
                {
                    var validationErrors = new List<KeyValuePair<string, string[]>>();
                    foreach (var error in exception.Errors)
                    {
                        validationErrors.Add(new KeyValuePair<string, string[]>(error.Key, error.Value));
                    }
                    problemDetails.Extensions.Add("errors", validationErrors);
                }

                await problemDetailsService.WriteAsync(new ProblemDetailsContext
                {
                    HttpContext = context,
                    ProblemDetails = problemDetails
                });
            });
        });
    }
}