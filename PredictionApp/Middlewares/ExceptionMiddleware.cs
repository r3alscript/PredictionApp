using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using FluentValidation; 

namespace PredictionApp.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
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

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var request = context.Request;
            var errorId = Guid.NewGuid(); 

            var statusCode = ex switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                ValidationException => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError
            };

            _logger.LogError(ex,
                "\nEXCEPTION #{ErrorId}" +
                "\nURL: {Method} {Url}" +
                "\nService: {ServiceName}" +
                "\nMessage: {Message}" +
                "\nInner: {Inner}" +
                "\nStackTrace:\n{Stack}\n",
                errorId,
                request.Method,
                $"{request.Path}{request.QueryString}",
                ex.TargetSite?.DeclaringType?.FullName ?? "Unknown service",
                ex.Message,
                ex.InnerException?.Message ?? "null",
                ex.StackTrace
            );

            var response = new
            {
                errorId,
                statusCode,
                errorType = ex.GetType().Name,
                message = ex.Message,
                details = ex.InnerException?.Message,
                path = request.Path,
                method = request.Method
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
