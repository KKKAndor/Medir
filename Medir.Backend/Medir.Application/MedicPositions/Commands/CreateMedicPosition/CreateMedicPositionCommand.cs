using MediatR;

namespace Medir.Application.MedicPositions.Commands.CreateMedicPosition
{
    public class CreateMedicPositionCommand : IRequest
    {
        public Guid MedicId { get; set; }

        public Guid PositionId { get; set; }

        public DateTime DateOnPosition { get; set; }        
    }
}
