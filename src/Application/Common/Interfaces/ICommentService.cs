using Application.PostComments.Commands.CreatePostComment;
using Application.PostComments.Queries.GetPostComments;

namespace Application.Common.Interfaces;

public interface ICommentService
{
    Task<Result<string>> CreatePostCommentAsync(CreatePostCommentDto dto);
    Task<Result<List<PostCommentResponseDto>>> GetPostCommentsAsync(string postId);

    Task<Result> DeletePostCommentAsync(string commentId);
    Task<Result> TogglePostCommentReactionAsync(string commentId, string userId, bool isLiked);
}
