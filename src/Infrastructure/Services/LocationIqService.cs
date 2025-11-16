
using System.Globalization;
using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using Application.Common.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class LocationIqService : ILocationIqService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public LocationIqService(HttpClient httpClient, IOptions<LocationIqOptions> options)
    {
        _httpClient = httpClient;
        _apiKey = options.Value.ApiKey;
    }

    public async Task<Result<LocationIqResponse?>> ReverseGeocode(double lat, double lon)
    {
        var builder = new UriBuilder("https://us1.locationiq.com/v1/reverse");
        var query = $"key={_apiKey}&lat={lat.ToString(CultureInfo.InvariantCulture)}&lon={lon.ToString(CultureInfo.InvariantCulture)}&format=json";
        builder.Query = query;
        var url = builder.ToString();


        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            return Result<LocationIqResponse?>.Failure("Failed to reverse geocode");

        var json = await response.Content.ReadAsStringAsync();
        return Result<LocationIqResponse?>.Success(JsonConvert.DeserializeObject<LocationIqResponse>(json));
    }

    public async Task<Result<string>> ReverseGeocodePlace(double lat, double lon)
    {
        var raw = await ReverseGeocode(lat, lon);

        if (!raw.IsSuccess || raw.Value == null)
            return Result<string>.Failure("Failed to reverse geocode");

        var address = raw.Value.Address;

        var place = address.City
            ?? address.Town
            ?? address.Village
            ?? address.State
            ?? raw.Value.Display_name;

        if (string.IsNullOrEmpty(place))
            return Result<string>.Failure("Failed to reverse geocode");

        return Result<string>.Success(place);
    }
}
