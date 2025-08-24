namespace Application.Common.Interfaces;

public interface ICookieService
{
    void SetCookie(string key, string value, int? expireMinutes = null, bool httpOnly = true, bool secure = true);
    string? GetCookie(string key);
    void DeleteCookie(string key);
}
