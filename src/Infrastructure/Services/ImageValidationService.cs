using Application.Common.Interfaces;
using SixLabors.ImageSharp;

namespace Infrastructure.Services;

public class ImageValidationService : IImageValidationService
{
    public bool IsValidImage(Stream fileStream)
    {
        try
        {
            fileStream.Position = 0;
            var info = Image.Identify(fileStream);
            return info != null;
        }
        catch
        {
            return false;
        }
    }
}
