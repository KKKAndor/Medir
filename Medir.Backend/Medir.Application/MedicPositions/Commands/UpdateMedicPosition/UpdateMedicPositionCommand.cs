using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Medir.Application.MedicPositions.Commands.UpdateMedicPosition
{
    public class UpdateMedicPositionCommand : IRequest
    {
        [BindProperty]
        public Guid MedicId { get; set; }

        [BindProperty]
        public Guid PositionId { get; set; }

        [BindProperty]
        public DateTime DateOnPosition { get; set; }
    }
}
