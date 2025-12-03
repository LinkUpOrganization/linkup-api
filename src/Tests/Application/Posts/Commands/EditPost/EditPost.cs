using Application.Common;
using Application.Common.Interfaces;
using Application.Posts.Commands.EditPost;
using Moq;

namespace Tests.Application.Posts.Commands.EditPost;

public class EditPostCommandHandlerTests
{
    private readonly Mock<IPostService> _postService = new();

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenPostUpdated()
    {
        // arrange
        _postService
            .Setup(x => x.EditPostAsync(It.IsAny<EditPostDto>()))
            .ReturnsAsync(Result.Success());

        var handler = new EditPostCommandHandler(_postService.Object);

        var command = new EditPostCommand
        {
            PostId = "post-1",
            Content = "Updated content"
        };

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        Assert.True(result.IsSuccess);
        Assert.Null(result.Error);
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenServiceFails()
    {
        // arrange
        _postService
            .Setup(x => x.EditPostAsync(It.IsAny<EditPostDto>()))
            .ReturnsAsync(Result.Failure("Update error"));

        var handler = new EditPostCommandHandler(_postService.Object);

        var command = new EditPostCommand
        {
            PostId = "post-1",
            Content = "Updated content"
        };

        // act
        var result = await handler.Handle(command, CancellationToken.None);

        // assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Update error", result.Error);
    }
}
