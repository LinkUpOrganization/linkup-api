using Application.Common;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Posts.Commands.TogglePostReaction;

public class TogglePostReactionCommand : IRequest<Result>
{
    public string PostId { get; set; } = null!;
    public bool IsLiked { get; set; }
}

public class TogglePostLikeRequest
{
    public bool IsLiked { get; set; }
}

public class TogglePostReactionCommandHandler(ICurrentUserService currentUserService, IPostRepository postRepo,
    IPostReactionRepository reactionRepo) : IRequestHandler<TogglePostReactionCommand, Result>
{
    public async Task<Result> Handle(TogglePostReactionCommand request, CancellationToken ct)
    {
        var userId = currentUserService.Id!;

        var post = await postRepo.GetPostByIdAsync(request.PostId, default);
        if (post == null)
            return Result.Failure("Post does not exist");

        var reaction = await reactionRepo.GetReactionAsync(request.PostId, userId, ct);

        if (reaction == null && request.IsLiked)
            await reactionRepo.AddReactionAsync(new PostReaction { PostId = request.PostId, UserId = userId }, ct);

        else if (reaction != null && !request.IsLiked)
            await reactionRepo.RemoveReactionAsync(reaction, ct);

        return Result.Success();
    }
}
