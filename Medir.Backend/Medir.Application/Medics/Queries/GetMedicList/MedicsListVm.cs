using Medir.Application.Common.Pagination;

namespace Medir.Application.Medics.Queries.GetMedicList
{
    public class MedicsListVm
    {
        public PagedList<MedicLookUpDto>? Medics { get; set; }
    }
}
