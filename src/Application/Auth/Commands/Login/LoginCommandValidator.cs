using FluentValidation;

namespace Application.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Please enter a valid email address")
            .Must(email => email == email.Trim()).WithMessage("Please enter a valid email address");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$")
            .WithMessage("Password must have at least one uppercase letter, one lowercase letter and one number");
    }
}