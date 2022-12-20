using MediatR;

namespace Medir.Application.Cities.Commands.DeleteCity
{
    public class DeleteCityCommand : IRequest
    {
        public Guid CityId { get; set; }
    }
}
