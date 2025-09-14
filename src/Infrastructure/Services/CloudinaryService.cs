using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Application.Common.Options;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinaryOptions> config)
    {
        var account = new Account(
            config.Value.CloudName,
            config.Value.ApiKey,
            config.Value.ApiSecret
        );
        _cloudinary = new Cloudinary(account);
    }

    public async Task<Result<CloudinaryUploadDto>> UploadImageAsync(Stream fileStream, string fileName)
    {
        var generatedId = Guid.NewGuid().ToString("N");
        var folder = "posts";

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, fileStream),
            Folder = folder,
            PublicId = generatedId,
            Overwrite = false
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return Result<CloudinaryUploadDto>.Success(new CloudinaryUploadDto
            {
                Url = uploadResult.SecureUrl.ToString(),
                PublicId = uploadResult.PublicId
            });
        }

        return Result<CloudinaryUploadDto>.Failure($"Cloudinary upload failed: {uploadResult.Error?.Message}");
    }


    public async Task<Result> DeleteImageAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        await _cloudinary.DestroyAsync(deleteParams);
        return Result.Success();
    }
}
