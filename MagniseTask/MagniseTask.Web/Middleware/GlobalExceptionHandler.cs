using System.Net;
using MagniseTask.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MagniseTask.Web.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is ApiException apiException)
        {
            var errorResponse = new ErrorResponse
            {
                StatusCode = (int)apiException.Status,
                Message = apiException.Message,
                Errors = apiException.Data
            };
            httpContext.Response.StatusCode = errorResponse.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(errorResponse, cancellationToken);
        }
        else
        {
            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = exception.GetType().Name,
                Title = "An unhandled error occurred",
                Detail = exception.Message
            };
            await httpContext
                .Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);
        }
        return true;
    }
}