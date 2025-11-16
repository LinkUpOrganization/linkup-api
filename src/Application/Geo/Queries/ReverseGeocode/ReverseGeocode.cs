using Application.Common;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Geo.Queries.ReverseGeocode;

public class ReverseGeocodeQuery : IRequest<Result<string>>
{
    public double Lat { get; set; }
    public double Lon { get; set; }
}

public class ReverseGeocodeQueryHandler(ILocationIqService locationService)
    : IRequestHandler<ReverseGeocodeQuery, Result<string>>
{
    public async Task<Result<string>> Handle(ReverseGeocodeQuery request, CancellationToken ct)
    {
        var result = await locationService.ReverseGeocodePlace(request.Lat, request.Lon);
        if (!result.IsSuccess || string.IsNullOrEmpty(result.Value))
            return Result<string>.Failure("Failed to reverse geocode");

        return Result<string>.Success(result.Value);
    }
}
