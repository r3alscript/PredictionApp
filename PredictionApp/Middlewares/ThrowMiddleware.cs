using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace PredictionApp.Middlewares
{
    public class ThrowMiddleware
    {
        private readonly RequestDelegate _next;

        public ThrowMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/throw-global")
                throw new Exception("Simulated middleware-level error");

            await _next(context);
        }
    }

    public static class ThrowMiddlewareExtensions
    {
        public static IApplicationBuilder UseThrowMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ThrowMiddleware>();
        }
    }
}
