using Application.Common;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Posts.Commands.DeletePost;

public class DeletePostCommand : IRequest<Result>
{
    public string PostId { get; set; } = null!;
}

public class DeletePostCommandHandler(ICloudinaryService cloudinaryService, IPostRepository postRepo,
    ICurrentUserService currentUser)
    : IRequestHandler<DeletePostCommand, Result>
{
    public async Task<Result> Handle(DeletePostCommand request, CancellationToken ct)
    {
        var post = await postRepo.GetPostWithPhotosAsync(request.PostId, ct);
        if (post == null)
            return Result.Failure("Post not found");

        if (post.AuthorId != currentUser.Id)
            return Result.Failure("Access denied");

        if (post.PostPhotos is not null)
        {
            foreach (var photo in post.PostPhotos)
            {
                await cloudinaryService.DeleteImageAsync(photo.PublicId);
            }
        }

        return await postRepo.DeletePostAsync(post, ct);
    }
}
