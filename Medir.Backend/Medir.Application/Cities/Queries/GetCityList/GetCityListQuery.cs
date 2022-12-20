using MediatR;
using Medir.Application.Common.Pagination;

namespace Medir.Application.Cities.Queries.GetCityList
{
    public class GetCityListQuery : IRequest<CitiesListVm>
    {
        public CitiesListParameters Parameters { get; set; } = new();
    }
}
