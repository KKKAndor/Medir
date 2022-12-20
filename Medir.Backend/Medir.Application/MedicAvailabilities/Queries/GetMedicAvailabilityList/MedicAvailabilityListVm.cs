
using Medir.Application.Common.Pagination;

namespace Medir.Application.MedicAvailabilities.Queries.GetMedicAvailabilityList
{
    public class MedicAvailabilityListVm
    {
        public PagedList<MedicAvailabilityLookUpDto> LookUpList { get; set; }
    }
}
