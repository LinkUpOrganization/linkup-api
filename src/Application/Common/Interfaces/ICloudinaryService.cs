using Application.Common.DTOs;

namespace Application.Common.Interfaces;

public interface ICloudinaryService
{
    Task<Result<CloudinaryUploadDto>> UploadImageAsync(Stream fileStream, string fileName);
    Task<Result> DeleteImageAsync(string publicId);
}

