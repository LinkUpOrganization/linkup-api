using FluentValidation;

namespace Application.Auth.Commands.LoginWithGoogle;

public class CreateLeagueCommandValidator : AbstractValidator<LoginWithGoogleCommand>
{

    public CreateLeagueCommandValidator()
    {
        RuleFor(v => v.ClaimsPrincipal)
            .NotNull()
            .WithMessage("ClaimsPrincipal is required.");
    }
}