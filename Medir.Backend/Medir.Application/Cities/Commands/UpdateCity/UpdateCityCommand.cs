using MediatR;

namespace Medir.Application.Cities.Commands.UpdateCity
{
    public class UpdateCityCommand : IRequest
    {
        public Guid CityId { get; set; }

        public string CityName { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
