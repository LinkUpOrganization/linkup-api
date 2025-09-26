using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Posts.Queries.GetPost;


public class GetPostQuery : IRequest<Result<PostResponseDto>>
{
    public string PostId { get; set; } = null!;
}

public class GetPostQueryHandler(IPostService postService)
    : IRequestHandler<GetPostQuery, Result<PostResponseDto>>
{
    public async Task<Result<PostResponseDto>> Handle(GetPostQuery request, CancellationToken ct)
    {
        return await postService.GetPostByIdAsync(request.PostId, ct);
    }
}
