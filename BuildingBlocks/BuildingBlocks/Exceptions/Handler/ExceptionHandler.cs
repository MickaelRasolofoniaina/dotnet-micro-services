using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;

public class ExceptionHandler(ILogger<ExceptionHandler> logger) : Microsoft.AspNetCore.Diagnostics.IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

        (string Message, string Title, int StatusCode) = exception switch
        {
            ValidationException => (exception.Message, "Validation Error", StatusCodes.Status400BadRequest),
            BadRequestException => (exception.Message, "Bad Request Error", StatusCodes.Status400BadRequest),
            NotFoundException => (exception.Message, "Not Found Error", StatusCodes.Status404NotFound),
            InternalServerException => ("An internal server error occurred.", "Internal Server Error", StatusCodes.Status500InternalServerError),
            // Add your custom exception handling logic here
            _ => ("An unexpected error occurred.", "Unexpected Error", StatusCodes.Status500InternalServerError)
        };

        var problemDetails = new ProblemDetails
        {
            Title = Title,
            Status = StatusCode,
            Detail = Message,
            Instance = httpContext.Request.Path,
            Type = exception.GetType().Name
        };

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("validationErrors", validationException.Errors);
        }

        httpContext.Response.StatusCode = StatusCode;
        httpContext.Response.ContentType = "application/problem+json";

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

        return true;
    }
}
