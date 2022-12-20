using MediatR;

namespace Medir.Application.Cities.Queries.GetCityList
{
    public class GetCityListQuery : IRequest<CitiesListVm>
    {
    }
}
