using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Application.Posts.Queries.GetPosts;
using Domain.Enums;
using Moq;

namespace Tests.Application.Posts.Queries.GetPosts;

public class GetPostsQueryHandlerTests
{
    private readonly Mock<IPostService> _postService = new();

    public GetPostsQueryHandlerTests()
    {
        // mock default behavior (успішний результат)
        _postService
            .Setup(s => s.GetRecentPostsAsync(It.IsAny<GetPostsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<PagedResult<PostResponseDto>>.Success(new PagedResult<PostResponseDto>()));

        _postService
            .Setup(s => s.GetTopPostsAsync(It.IsAny<GetPostsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<PagedResult<PostResponseDto>>.Success(new PagedResult<PostResponseDto>()));

        _postService
            .Setup(s => s.GetFollowingPostsAsync(It.IsAny<GetPostsQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<PagedResult<PostResponseDto>>.Success(new PagedResult<PostResponseDto>()));
    }

    [Fact]
    public async Task Should_Call_GetRecentPosts_WhenSortTypeIsRecent()
    {
        var handler = new GetPostsQueryHandler(_postService.Object);
        var query = new GetPostsQuery
        {
            Params = new PostParams { SortType = PostSortType.Recent }
        };

        await handler.Handle(query, CancellationToken.None);

        _postService.Verify(s => s.GetRecentPostsAsync(query, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_Call_GetTopPosts_WhenSortTypeIsTop()
    {
        var handler = new GetPostsQueryHandler(_postService.Object);
        var query = new GetPostsQuery
        {
            Params = new PostParams { SortType = PostSortType.Top }
        };

        await handler.Handle(query, CancellationToken.None);

        _postService.Verify(s => s.GetTopPostsAsync(query, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_Call_GetFollowingPosts_WhenSortTypeIsFollowing()
    {
        var handler = new GetPostsQueryHandler(_postService.Object);
        var query = new GetPostsQuery
        {
            Params = new PostParams { SortType = PostSortType.Following }
        };

        await handler.Handle(query, CancellationToken.None);

        _postService.Verify(s => s.GetFollowingPostsAsync(query, It.IsAny<CancellationToken>()), Times.Once);
    }
}
