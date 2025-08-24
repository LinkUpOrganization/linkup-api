using System.Security.Claims;
using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Auth.Commands.LoginWithGoogle;

public sealed record LoginWithGoogleCommand(
    ClaimsPrincipal ClaimsPrincipal
) : IRequest<Result<string>>;


public class LoginWithGoogleHandler(IAccountService accountService) : IRequestHandler<LoginWithGoogleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginWithGoogleCommand request, CancellationToken ct)
    {
        return await accountService.LoginWithGoogleAsync(request.ClaimsPrincipal);
    }
}