using MediatR;
using Medir.Application.Common.Pagination;

namespace Medir.Application.MedicAvailabilities.Queries.GetMedicAvailabilityList
{
    public class GetMedicAvailabilityListQuery : IRequest<MedicAvailabilityListVm>
    {
        public Guid MedicId { get; set; }

        public MedicAvailabilitiesParameters Parameters { get; set; } = new();
    }
}
