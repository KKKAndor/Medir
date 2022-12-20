using Medir.Application.Common.Pagination;

namespace Medir.Application.Cities.Queries.GetCityList
{
    public class CitiesListVm
    {
        public PagedList<CityLookUpDto> Cities { get; set; }
    }
}
