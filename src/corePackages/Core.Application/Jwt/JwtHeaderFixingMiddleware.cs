using Microsoft.AspNetCore.Http;

namespace Core.Application.Jwt;

public class JwtHeaderFixingMiddleware
{
    private readonly RequestDelegate _next;

    public JwtHeaderFixingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        if (!string.IsNullOrEmpty(authHeader) && !authHeader.StartsWith("Bearer "))
        {
            context.Request.Headers["Authorization"] = $"Bearer {authHeader}";
        }

        await _next(context);
    }
}
