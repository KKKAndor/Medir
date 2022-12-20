using Medir.Application.Common.Pagination;

namespace Medir.Application.MedicPositions.Queries.GetMedicPositionList
{
    public class MedicPositionsListVm
    {
        public PagedList<MedicPositionLookUpDto> MedicPositions { get; set; }
    }
}
