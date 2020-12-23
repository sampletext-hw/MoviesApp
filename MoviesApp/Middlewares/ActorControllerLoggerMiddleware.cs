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
            if (httpContext.Request.Path.Value.ToLower().Contains("actor"))
            {
                _logger.LogDebug(
                    $"ContentType: {httpContext.Request.ContentType}\n" +
                    $"ContentLength: {httpContext.Request.ContentLength}\n" +
                    $"Query: {string.Join(", ", from q in httpContext.Request.Query select $"{q.Key} = {string.Join(",", q.Value)};")}\n" +
                    $"Cookies: {string.Join(", ", from c in httpContext.Request.Cookies select $"{c.Key} = {c.Value};")}\n" +
                    $"HasFormContentType: {httpContext.Request.HasFormContentType}\n" +
                    $"Headers: {string.Join(", ", from h in httpContext.Request.Headers select $"{h.Key} = {string.Join(",", h.Value)};")}\n" +
                    $"Host: {httpContext.Request.Host.Value}\n" +
                    $"IsHttps: {httpContext.Request.IsHttps}\n" +
                    $"Method: {httpContext.Request.Method}\n" +
                    $"PathBase: {httpContext.Request.PathBase.Value}\n" +
                    $"Protocol: {httpContext.Request.Protocol}\n" +
                    $"QueryString: {httpContext.Request.QueryString.Value}\n" +
                    $"RouteValues: {string.Join(", ", from rv in httpContext.Request.RouteValues select $"{rv.Key}={rv.Value};")}\n" +
                    $"Scheme: {httpContext.Request.Scheme}");
            }

            await _next.Invoke(httpContext);
        }
    }
}