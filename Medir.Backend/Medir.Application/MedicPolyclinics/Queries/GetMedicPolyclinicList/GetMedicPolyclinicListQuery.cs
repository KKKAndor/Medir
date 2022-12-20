using MediatR;

namespace Medir.Application.MedicPolyclinics.Queries.GetMedicPolyclinicList
{
    public class GetMedicPolyclinicListQuery : IRequest<MedicPolyclinicsListVm>
    {
        public Guid MedicId { get; set; }
    }
}
