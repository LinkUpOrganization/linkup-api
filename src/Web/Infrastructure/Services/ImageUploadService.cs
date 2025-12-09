using Application.Common.DTOs;
using Application.Common.Interfaces;

namespace Web.Infrastructure.Services;

public interface IImageUploadService
{
    Task<List<CloudinaryUploadDto>> UploadAsync(
        IFormFileCollection? files,
        int? maxFiles = null,
        CancellationToken cancellationToken = default);
}

public sealed class ImageUploadService : IImageUploadService
{
    private readonly ICloudinaryService _cloudinary;
    private readonly IImageValidationService _imageValidation;

    public ImageUploadService(
        ICloudinaryService cloudinary,
        IImageValidationService imageValidation)
    {
        _cloudinary = cloudinary;
        _imageValidation = imageValidation;
    }

    public async Task<List<CloudinaryUploadDto>> UploadAsync(
        IFormFileCollection? files,
        int? maxFiles = null,
        CancellationToken cancellationToken = default)
    {
        var uploaded = new List<CloudinaryUploadDto>();

        if (files is null || files.Count == 0)
            return uploaded;

        if (maxFiles.HasValue && files.Count > maxFiles.Value)
            throw new InvalidOperationException(
                $"You can't upload more than {maxFiles.Value} files.");

        foreach (var file in files)
        {
            if (file is null || file.Length == 0)
                continue;

            await using var stream = file.OpenReadStream();

            if (!_imageValidation.IsValidImage(stream))
                throw new InvalidOperationException(
                    "One of the files is not a valid image.");

            stream.Position = 0;

            var uploadResult = await _cloudinary
                .UploadImageAsync(stream, file.FileName);

            if (!uploadResult.IsSuccess || uploadResult.Value is null)
                throw new InvalidOperationException(uploadResult.Error);

            uploaded.Add(uploadResult.Value);
        }

        return uploaded;
    }
}
