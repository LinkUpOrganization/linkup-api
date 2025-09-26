using Application.Posts.Commands.EditPost;
using FluentValidation;

namespace Application.Post.Commands.EditPost;

public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
{
    public EditPostCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MinimumLength(5).WithMessage("Title is too short")
            .MaximumLength(100).WithMessage("Title is too long");

        RuleFor(x => x.Content)
            .MaximumLength(300).WithMessage("Content is too long");
    }
}