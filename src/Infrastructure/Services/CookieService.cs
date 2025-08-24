using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class CookieService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : ICookieService
{
    public void SetCookie(string key, string value, int? expireMinutes = null, bool httpOnly = true, bool secure = true)
    {
        var options = new CookieOptions
        {
            HttpOnly = httpOnly,
            Secure = secure,
            SameSite = SameSiteMode.None,
            Expires = expireMinutes.HasValue
                ? DateTimeOffset.UtcNow.AddMinutes(expireMinutes.Value)
                : DateTimeOffset.UtcNow.AddDays(Convert.ToDouble(configuration["Jwt:RefreshToken:ExpireDays"]!))
        };

        httpContextAccessor.HttpContext?.Response.Cookies.Append(key, value, options);
    }

    public string? GetCookie(string key)
    {
        return httpContextAccessor.HttpContext?.Request.Cookies[key];
    }

    public void DeleteCookie(string key)
    {
        httpContextAccessor.HttpContext?.Response.Cookies.Delete(key);
    }
}
