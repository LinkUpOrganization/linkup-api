namespace Application.Common.DTOs;

public class PagedResult<T>
{
    public List<T> Items { get; set; } = [];
    public string? NextCursor { get; set; }
}