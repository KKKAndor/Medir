using Medir.Application.Common.Pagination;

namespace Medir.Application.Polyclinics.Queries.GetPolyclinicList
{
    public class PolyclinicsListVm
    {
        public PagedList<PolyclinicLookUpDto> Polyclinics { get; set; }
    }
}
