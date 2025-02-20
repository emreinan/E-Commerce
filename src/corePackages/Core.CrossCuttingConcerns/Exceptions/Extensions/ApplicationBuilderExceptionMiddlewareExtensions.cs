using AuthenticationAPI.Core.Exceptions.Middleware;
using Microsoft.AspNetCore.Builder;

namespace AuthenticationAPI.Core.Exceptions.Extensions;

public static class ApplicationBuilderExceptionMiddlewareExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}
