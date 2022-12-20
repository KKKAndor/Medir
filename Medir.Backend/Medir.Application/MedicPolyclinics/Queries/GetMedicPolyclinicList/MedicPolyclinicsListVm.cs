using Medir.Application.Common.Pagination;

namespace Medir.Application.MedicPolyclinics.Queries.GetMedicPolyclinicList
{
    public class MedicPolyclinicsListVm
    {
        public PagedList<MedicPolyclinicLookUpDto> MedicPolyclinics { get; set; }
    }
}
