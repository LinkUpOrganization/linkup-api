using Application.Common.DTOs;
using Application.Common.Interfaces;
using Application.Posts.Commands.CreatePost;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.DTOs;
using Web.Infrastructure;

namespace Web.Endpoints;

public class Posts : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .RequireAuthorization()
           .MapPost(CreatePost, "");
    }

    private async Task<IResult> CreatePost(
        [FromForm] CreatePostRequest request,
        [FromServices] ISender sender,
        [FromServices] ICloudinaryService cloudinaryService
        )
    {
        var uploadedAssets = new List<CloudinaryUploadDto>();

        if (request.PostPhotos != null && request.PostPhotos.Count != 0)
        {
            foreach (var file in request.PostPhotos)
            {
                if (file == null || file.Length == 0) continue;

                await using var stream = file.OpenReadStream();
                var uploadResult = await cloudinaryService.UploadImageAsync(stream, file.FileName);

                if (!uploadResult.IsSuccess || uploadResult.Value == null)
                    return Results.BadRequest(uploadResult.Error);

                uploadedAssets.Add(uploadResult.Value);
            }
        }

        var command = new CreatePostCommand
        {
            Title = request.Title,
            Content = request.Content,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Address = request.Address,
            ImageRecords = uploadedAssets
        };

        var result = await sender.Send(command);

        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
    }
}
