using Application.Common;
using Application.Common.DTOs;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Posts.Queries.GetUserPostRoutePoints;

public class GetUserPostLocationsQuery : IRequest<Result<List<LocationDto>>>
{
    public string UserId { get; set; } = null!;
}

public class GetUserPostLocationsQueryHandler(IGeoService geoService)
    : IRequestHandler<GetUserPostLocationsQuery, Result<List<LocationDto>>>
{
    public async Task<Result<List<LocationDto>>> Handle(GetUserPostLocationsQuery request, CancellationToken ct)
    {
        return await geoService.GetUserPostLocations(request.UserId);
    }
}
