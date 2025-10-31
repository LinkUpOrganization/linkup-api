using Microsoft.EntityFrameworkCore;

namespace Application.Common.DTOs;

[Keyless]
public class PostRoutePointDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}