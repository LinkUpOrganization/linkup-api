using Application.Users.Queries;
using MediatR;
using Web.Infrastructure;

namespace Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetUserInfo, "{userId}");
    }


    private async Task<IResult> GetUserInfo(ISender sender, string userId)
    {
        var result = await sender.Send(new GetUserInfoQuery
        {
            UserId = userId
        });

        return result.IsSuccess ? Results.Ok(result.Value) : Results.NotFound(result.Error);
    }
}
