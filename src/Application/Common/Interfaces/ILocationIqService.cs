using Application.Common.DTOs;

namespace Application.Common.Interfaces;

public interface ILocationIqService
{
    Task<Result<LocationIqResponse?>> ReverseGeocode(double lat, double lon);
    Task<Result<string>> ReverseGeocodePlace(double lat, double lon);
}
