using MediatR;
using Medir.Application.Common.Pagination;

namespace Medir.Application.MedicPolyclinics.Queries.GetMedicPolyclinicList
{
    public class GetMedicPolyclinicListQuery : IRequest<MedicPolyclinicsListVm>
    {
        public Guid MedicId { get; set; }

        public MedicPolyclinicsParameters Parameters { get; set; } = new();
    }
}
