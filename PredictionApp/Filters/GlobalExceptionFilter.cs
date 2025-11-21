using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using PredictionApp.Domain.Exceptions;

namespace PredictionApp.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);

            if (ex is NotFoundException)
            {
                context.Result = new NotFoundObjectResult(new
                {
                    error = ex.Message,
                    type = "NotFound"
                });
            }
            else if (ex is ValidationException)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    error = ex.Message,
                    type = "ValidationError"
                });
            }
            else if (ex is BusinessException businessEx)
            {
                _logger.LogWarning(ex,
                    "BusinessException occurred. Message={Message}, Path={Path}, Method={Method}",
                    businessEx.Message,
                    context.HttpContext.Request.Path,
                    context.HttpContext.Request.Method);

                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                context.Result = new BadRequestObjectResult(new
                {
                    error = businessEx.Message,
                    type = "BusinessError",
                    path = context.HttpContext.Request.Path,
                    method = context.HttpContext.Request.Method
                });
            }
            else
            {
                context.Result = new ObjectResult(new
                {
                    error = "Internal Server Error",
                    details = ex.Message
                })
                { StatusCode = 500 };
            }

            context.ExceptionHandled = true;
        }
    }
}
