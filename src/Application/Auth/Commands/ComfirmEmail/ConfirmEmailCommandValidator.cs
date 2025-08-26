using FluentValidation;

namespace Application.Auth.Commands.ComfirmEmail;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{

    public ConfirmEmailCommandValidator()
    {
        RuleFor(v => v.VerificationToken)
            .NotEmpty()
            .WithMessage("Verification token is required.");
    }
}