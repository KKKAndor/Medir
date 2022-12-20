using MediatR;

namespace Medir.Application.Medics.Commands.DeleteMedic
{
    public class DeleteMedicCommand : IRequest
    {
        public Guid MedicId { get; set; }
    }
}
