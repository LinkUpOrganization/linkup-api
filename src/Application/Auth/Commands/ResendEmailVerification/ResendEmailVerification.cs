using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Options;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.Auth.Commands.ResendEmailVerification;

public class ResendEmailVerificationCommand : IRequest<Result>
{
}

public class ResendEmailVerificationCommandHandler(
    IUserService userService,
    IEmailService emailService,
    IAccountService accountService,
    IOptions<ClientOptions> clientOptions,
    ITokenService tokenService)
    : IRequestHandler<ResendEmailVerificationCommand, Result>
{
    private readonly ClientOptions _clientOptions = clientOptions.Value;

    public async Task<Result> Handle(ResendEmailVerificationCommand request, CancellationToken ct)
    {
        var userId = userService.Id;
        if (userId == null) return Result.Failure("Anauthorized", 401);
        var currentUserResult = await accountService.FindUserByIdAsync(userId);
        if (!currentUserResult.IsSuccess || currentUserResult.Value == null)
            return Result.Failure(currentUserResult.Error!, currentUserResult.Code);

        var remainingSeconds = await tokenService
            .GetCooldownRemainingSecondsAsync(userId, VerificationTokenType.EmailVerification);
        if (remainingSeconds > 0)
            return Result.Failure($"Please wait {remainingSeconds} seconds before requesting another email.", 429);

        var verificationTokenResult = await tokenService
            .IssueVerificationToken(currentUserResult.Value, VerificationTokenType.EmailVerification);
        if (!verificationTokenResult.IsSuccess || verificationTokenResult.Value == null)
            return Result.Failure(verificationTokenResult.Error!, verificationTokenResult.Code);

        var confirmationUrl = $"{_clientOptions.Url}/confirm?verificationToken={verificationTokenResult.Value}";
        await emailService.SendEmailAsync(currentUserResult.Value.Email, "Email confirmation", confirmationUrl);

        return Result.Success();
    }
}
