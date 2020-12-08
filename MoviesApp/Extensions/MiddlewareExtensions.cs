using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using MoviesApp.Middlewares;

namespace MoviesApp.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseActorControllerLogger(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ActorControllerLoggerMiddleware>();
        }
    }
}
