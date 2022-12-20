using MediatR;

namespace Medir.Application.MedicAvailabilities.Queries.GetMedicAvailabilityList
{
    public class GetMedicAvailabilityListQuery : IRequest<MedicAvailabilityListVm>
    {
        public Guid MedicId { get; set; }
    }
}
