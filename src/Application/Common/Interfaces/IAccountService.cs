using System.Security.Claims;
using Application.Common.DTOs;

namespace Application.Common.Interfaces;

public interface IAccountService
{
    Task<Result<string>> LoginWithGoogleAsync(ClaimsPrincipal claimsPrincipal);
}