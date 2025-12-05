using Application.Common.DTOs;
using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.EditPost;
using Application.Posts.Queries.GetPosts;

namespace Application.Common.Interfaces;

public interface IPostService
{
    Task<Result<string>> CreatePostAsync(CreatePostDto dto);
    Task<Result> EditPostAsync(EditPostDto dto);
    Task<Result<PagedResult<PostResponseDto>>> GetTopPostsAsync(GetPostsQuery query, CancellationToken ct);
    Task<Result<PagedResult<PostResponseDto>>> GetFollowingPostsAsync(GetPostsQuery query, CancellationToken ct);
    Task<Result<PagedResult<PostResponseDto>>> GetRecentPostsAsync(GetPostsQuery query, CancellationToken ct);
    Task<Result<PostResponseDto>> GetPostDetailsByIdAsync(string postId, CancellationToken ct);
    Task<Result> ValidatePhotoLimitAsync(string postId, int photosToAddCount, List<string>? photosToDeleteList,
        CancellationToken ct);
}
