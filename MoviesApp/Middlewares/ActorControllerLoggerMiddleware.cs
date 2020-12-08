using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MoviesApp.Middlewares
{
    public class ActorControllerLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ActorControllerLoggerMiddleware> _logger;

        public ActorControllerLoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ActorControllerLoggerMiddleware>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Value.ToLower().Contains("api/actor"))
            {
                _logger.LogDebug($"Actor Request: {httpContext.Request.Path.Value}, Method={httpContext.Request.Method}");
            }

            await _next.Invoke(httpContext);
        }
    }
}