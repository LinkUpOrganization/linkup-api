namespace Application.Common.Options;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    public AccessTokenOptions AccessToken { get; set; } = null!;
    public RefreshTokenOptions RefreshToken { get; set; } = null!;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;

    public class AccessTokenOptions
    {
        public string Key { get; set; } = string.Empty;
        public int ExpireMinutes { get; set; }
    }

    public class RefreshTokenOptions
    {
        public string Key { get; set; } = string.Empty;
        public int ExpireDays { get; set; }
    }
}
