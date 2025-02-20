using AuthenticationAPI.Core.Exceptions.Extensions;
using AuthenticationAPI.Core.Exceptions.HttpProblemDetails;
using AuthenticationAPI.Core.Exceptions.Types;
using Microsoft.AspNetCore.Http;

namespace AuthenticationAPI.Core.Exceptions.Handlers;

public class HttpExceptionHandler : ExceptionHandler
{
    public HttpResponse Response
    {
        get => _response ?? throw new NullReferenceException(nameof(_response));
        set => _response = value;
    }

    private HttpResponse? _response;

    public override Task HandleException(BusinessException businessException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        string details = new BusinessProblemDetails(businessException.Message).ToJson();
        return Response.WriteAsync(details);
    }

    public override Task HandleException(ValidationException validationException)
    {
        Response.StatusCode = StatusCodes.Status400BadRequest;
        string details = new ValidationProblemDetails(validationException.Errors).ToJson();
        return Response.WriteAsync(details);
    }

    public override Task HandleException(AuthorizationException authorizationException)
    {
        Response.StatusCode = StatusCodes.Status401Unauthorized;
        string details = new AuthorizationProblemDetails(authorizationException.Message).ToJson();
        return Response.WriteAsync(details);
    }

    public override Task HandleException(NotFoundException notFoundException)
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        string details = new NotFoundProblemDetails(notFoundException.Message).ToJson();
        return Response.WriteAsync(details);
    }

    public override Task HandleException(Exception exception)
    {
        Response.StatusCode = StatusCodes.Status500InternalServerError;
        string details = new InternalServerErrorProblemDetails(exception.Message).ToJson();
        return Response.WriteAsync(details);
    }
}
