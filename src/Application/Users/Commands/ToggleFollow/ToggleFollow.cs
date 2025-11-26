using Application.Common;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Users.Commands.ToggleFollow;

public class ToggleFollowCommand : IRequest<Result>
{
    public string FolloweeId { get; set; } = null!;
    public bool IsFollowed { get; set; }
}

public class ToggleFollowRequest
{
    public bool IsFollowed { get; set; }
}

public class ToggleFollowCommandHandler(ICurrentUserService currentUserService, IUserService userService)
    : IRequestHandler<ToggleFollowCommand, Result>
{
    public async Task<Result> Handle(ToggleFollowCommand request, CancellationToken ct)
    {
        var userId = currentUserService.Id!;
        return await userService.ToggleFollowAsync(userId, request.FolloweeId, request.IsFollowed);
    }
}
