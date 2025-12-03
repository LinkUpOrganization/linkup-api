using Application.Posts.Queries.GetPosts;
using FluentValidation.TestHelper;

namespace Tests.Application.Posts.Queries.GetPosts;

public class GetPostsQueryValidatorTests
{
    private readonly GetPostsQueryValidator _validator = new();

    [Fact]
    public void Should_NotHaveError_WhenRadiusIsNull()
    {
        var query = new GetPostsQuery
        {
            Params = new PostParams { RadiusKm = null }
        };

        var result = _validator.TestValidate(query);

        result.ShouldNotHaveValidationErrorFor(x => x.Params.RadiusKm);
    }

    [Fact]
    public void Should_HaveError_WhenRadiusIsZero()
    {
        var query = new GetPostsQuery
        {
            Params = new PostParams { RadiusKm = 0 }
        };

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.Params.RadiusKm)
            .WithErrorMessage("Radius must be greater than 0");
    }

    [Fact]
    public void Should_HaveError_WhenRadiusIsNegative()
    {
        var query = new GetPostsQuery
        {
            Params = new PostParams { RadiusKm = -5 }
        };

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.Params.RadiusKm);
    }

    [Fact]
    public void Should_NotHaveError_WhenRadiusIsPositive()
    {
        var query = new GetPostsQuery
        {
            Params = new PostParams { RadiusKm = 12 }
        };

        var result = _validator.TestValidate(query);

        result.ShouldNotHaveValidationErrorFor(x => x.Params.RadiusKm);
    }
}
