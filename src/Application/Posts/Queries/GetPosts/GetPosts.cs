using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Posts.Queries.GetPosts;

public class GetPostsQuery : IRequest<Result<PagedResult<PostResponseDto>>>
{
    public bool Ascending { get; set; }
    public string? Cursor { get; set; }
    public int PageSize { get; set; }
}

public class GetPostsQueryHandler(IPostService postService)
    : IRequestHandler<GetPostsQuery, Result<PagedResult<PostResponseDto>>>
{
    public async Task<Result<PagedResult<PostResponseDto>>> Handle(GetPostsQuery request, CancellationToken ct)
    {
        return await postService.GetPostsAsync(request, ct);
    }
}
