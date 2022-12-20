using MediatR;

namespace Medir.Application.Cities.Commands.CreateCity
{
    public class CreateCityCommand : IRequest<Guid>
    {
        public string CityName { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
