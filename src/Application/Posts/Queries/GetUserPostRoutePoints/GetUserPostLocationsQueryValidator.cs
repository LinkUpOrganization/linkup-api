using Application.Posts.Queries.GetUserPostRoutePoints;
using FluentValidation;

namespace Application.Posts.Queries.GetUserPostLocations;

public class GetUserPostLocationsQueryValidator : AbstractValidator<GetUserPostLocationsQuery>
{
    public GetUserPostLocationsQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId is required");
    }
}