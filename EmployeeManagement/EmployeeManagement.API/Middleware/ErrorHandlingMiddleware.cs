using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using EmployeeManagement.Core.Exceptions;

namespace EmployeeManagement.API.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unhandled exception occurred.");

        var response = context.Response;
        response.ContentType = "application/json";

        var errorResponse = new ErrorResponse
        {
            TraceId = context.TraceIdentifier,
            Message = "An error occurred while processing your request."
        };

        switch (exception)
        {
            case NotFoundException notFoundEx:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse.Message = notFoundEx.Message;
                break;

            case ValidationException validationEx:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.Message = validationEx.Message;
                errorResponse.Errors = validationEx.Errors;
                break;

            case BusinessRuleException businessEx:
                response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                errorResponse.Message = businessEx.Message;
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                break;
        }

        var result = JsonSerializer.Serialize(errorResponse);
        await response.WriteAsync(result);
    }
}

public class ErrorResponse
{
    public required string TraceId { get; set; }
    public required string Message { get; set; }
    public IDictionary<string, string[]>? Errors { get; set; }
}

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(string message, IDictionary<string, string[]> errors) : base(message)
    {
        Errors = errors;
    }
}

public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message) { }
} 