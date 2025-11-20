namespace Application.Common.Interfaces;

public interface IImageValidationService
{
    bool IsValidImage(Stream fileStream);
}