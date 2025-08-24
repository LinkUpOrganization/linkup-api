namespace Application.Common.DTOs;

public sealed record TokenPair(
    string AccessToken,
    string RefreshToken
);