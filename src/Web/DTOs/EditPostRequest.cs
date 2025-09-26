namespace Web.DTOs;

public class EditPostRequest
{
    public string? Title { get; set; } = null!;
    public string? Content { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public string? Address { get; set; }
    public IFormFileCollection? PhotosToAdd { get; set; }
    public List<string>? PostPhotosToDelete { get; set; }
}

