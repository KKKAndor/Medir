using MediatR;

namespace Medir.Application.MedicPositions.Queries.GetMedicPositionList
{
    public class GetMedicPositionListQuery : IRequest<MedicPositionsListVm>
    {
        public Guid MedicId { get; set; }
    }
}
