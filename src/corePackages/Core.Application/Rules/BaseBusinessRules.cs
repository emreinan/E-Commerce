using Microsoft.AspNetCore.Http;

namespace Core.Application.Rules;

public abstract class BaseBusinessRules (IHttpContextAccessor httpContextAccessor)
{
    public string IfUserIdIsNullGetOrCreateGuestId(Guid? userId)
    {
        if (userId.HasValue)
            return string.Empty; // Kullanıcı giriş yapmışsa guestId kullanma.

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
}
