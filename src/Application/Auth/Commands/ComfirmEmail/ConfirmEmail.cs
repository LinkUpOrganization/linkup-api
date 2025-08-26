using Application.Common;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Auth.Commands.ComfirmEmail;

public class ConfirmEmailCommand : IRequest<Result>
{
    public string VerificationToken { get; set; } = null!;
}

public class RefreshTokenCommandHandler(IAccountService accountService) : IRequestHandler<ConfirmEmailCommand, Result>
{
    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken ct)
    {
        return await accountService.ConfirmEmailAsync(request.VerificationToken);
    }
}