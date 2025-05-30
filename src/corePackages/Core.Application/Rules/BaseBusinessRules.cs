using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Core.Application.Rules;

public abstract class BaseBusinessRules(IHttpContextAccessor httpContextAccessor)
{
    public string IfUserIdIsNullGetOrCreateGuestId(Guid? userId)
    {
        if (userId.HasValue)
            return string.Empty;

        var guestId = httpContextAccessor.HttpContext?.Request.Cookies["GuestId"];

        if (string.IsNullOrEmpty(guestId))
        {
            guestId = Guid.NewGuid().ToString();
            httpContextAccessor.HttpContext?.Response.Cookies.Append("GuestId", guestId, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        }

        return guestId;
    }
    public Guid GetClaimUserId()
    {
        var userIdClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        return userIdClaim is null ? throw new InvalidOperationException("User ID cannot be retrieved from request.") : Guid.Parse(userIdClaim.Value);
    }
    public string GetClaimName()
    {
        var userNameClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name);
        return userNameClaim is null ? throw new InvalidOperationException("User name cannot be retrieved from request.") : userNameClaim.Value.ToString();
    }
    public string GetClaimSurname()
    {
        var userSurnameClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Surname);
        return userSurnameClaim is null ? throw new InvalidOperationException("User surname cannot be retrieved from request.") : userSurnameClaim.Value.ToString();
    }
    public string GetClaimEmail()
    {
        var userEmailClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email);
        return userEmailClaim is null ? throw new InvalidOperationException("User email cannot be retrieved from request.") : userEmailClaim.Value.ToString();
    }
    public string GetClaimRole()
    {
        var userRoleClaim = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role);
        return userRoleClaim is null ? throw new InvalidOperationException("User role cannot be retrieved from request.") : userRoleClaim.Value.ToString();
    }
}
