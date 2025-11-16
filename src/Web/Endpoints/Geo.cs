using Application.Geo.Queries.ReverseGeocode;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Infrastructure;

namespace Web.Endpoints;

public class Geo : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(ReverseGeocode, "reverse");
    }


    private async Task<IResult> ReverseGeocode(ISender sender, [FromQuery] double lat, [FromQuery] double lon)
    {
        var result = await sender.Send(new ReverseGeocodeQuery
        {
            Lat = lat,
            Lon = lon
        });

        return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Error);
    }
}
