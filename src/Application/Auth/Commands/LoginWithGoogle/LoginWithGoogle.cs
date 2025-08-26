using System.Security.Claims;
using Application.Common;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Auth.Commands.LoginWithGoogle;

public record LoginWithGoogleCommand(ClaimsPrincipal ClaimsPrincipal) : IRequest<Result<string>>;

public class LoginWithGoogleCommandHandler(IAccountService accountService, ITokenService tokenService)
    : IRequestHandler<LoginWithGoogleCommand, Result<string>>
{
    public async Task<Result<string>> Handle(LoginWithGoogleCommand request, CancellationToken ct)
    {
        var loginResult = await accountService.LoginWithGoogleAsync(request.ClaimsPrincipal);

        if (!loginResult.IsSuccess || loginResult.Value == null)
            return Result<string>.Failure(loginResult.Error!, loginResult.Code);

        var refreshTokenResult = await tokenService.IssueRefreshToken(loginResult.Value);
        return (refreshTokenResult.IsSuccess && !string.IsNullOrEmpty(refreshTokenResult.Value))
            ? Result<string>.Success(refreshTokenResult.Value)
            : Result<string>.Failure(refreshTokenResult.Error!, refreshTokenResult.Code);
    }
}