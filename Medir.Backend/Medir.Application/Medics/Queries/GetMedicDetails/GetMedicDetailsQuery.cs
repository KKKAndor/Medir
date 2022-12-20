using MediatR;

namespace Medir.Application.Medics.Queries.GetMedicDetails
{
    public class GetMedicDetailsQuery : IRequest<MedicDetailsVm>
    {
        public Guid MedicId { get; set; }
    }
}
