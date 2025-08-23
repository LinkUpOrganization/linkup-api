using Application.Weather.Queries;
using MediatR;
using Web.Infrastructure;

namespace Web.Endpoints;

public class WeatherForecast : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetWeather, "");
    }

    private async Task<IResult> GetWeather(ISender sender)
    {
        var weather = await sender.Send(new GetWeatherQuery());
        return Results.Ok(weather);
    }

}