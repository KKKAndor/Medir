using MediatR;
using Medir.Application.Common.Pagination;

namespace Medir.Application.MedicPositions.Queries.GetMedicPositionList
{
    public class GetMedicPositionListQuery : IRequest<MedicPositionsListVm>
    {
        public Guid MedicId { get; set; }

        public MedicPositionsParameters Parameters { get; set; } = new();
    }
}
