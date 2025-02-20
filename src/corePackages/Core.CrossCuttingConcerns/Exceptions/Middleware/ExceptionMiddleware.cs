using System.Net.Mime;
using AuthenticationAPI.Core.Exceptions.Handlers;
using Microsoft.AspNetCore.Http;

namespace AuthenticationAPI.Core.Exceptions.Middleware;

public class ExceptionMiddleware
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly HttpExceptionHandler _httpExceptionHandler;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor)
    {
        _next = next;
        _contextAccessor = contextAccessor;
        _httpExceptionHandler = new HttpExceptionHandler();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context.Response, exception);
        }
    }

    protected virtual Task HandleExceptionAsync(HttpResponse response, dynamic exception)
    {
        response.ContentType = MediaTypeNames.Application.Json;
        _httpExceptionHandler.Response = response;

        return _httpExceptionHandler.HandleException(exception);
    }
}
