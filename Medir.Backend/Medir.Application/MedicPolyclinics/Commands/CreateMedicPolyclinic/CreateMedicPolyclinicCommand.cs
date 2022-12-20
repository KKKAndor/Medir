using MediatR;

namespace Medir.Application.MedicPolyclinics.Commands.CreateMedicPolyclinic
{
    public class CreateMedicPolyclinicCommand : IRequest
    {
        public Guid MedicId { get; set; }

        public Guid PolyclinicId { get; set; }

        public int Price { get; set; }
    }
}
