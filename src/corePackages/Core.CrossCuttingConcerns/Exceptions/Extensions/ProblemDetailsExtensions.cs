using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AuthenticationAPI.Core.Exceptions.Extensions;

public static class ProblemDetailsExtensions
{
    public static string ToJson<TProblemDetail>(this TProblemDetail details)
        where TProblemDetail : ProblemDetails
    {
        return JsonSerializer.Serialize(details);
    }
}
