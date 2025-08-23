using MediatR;

namespace Application.Weather.Queries;

public class GetWeatherQuery : IRequest<string>
{
}

public class GetWeatherQueryHandler : IRequestHandler<GetWeatherQuery, string>
{
    public GetWeatherQueryHandler()
    {
    }

    public async Task<string> Handle(GetWeatherQuery request, CancellationToken cancellationToken)
    {

        await Task.CompletedTask;
        return "Sunny, 25°C";
    }
}
