using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Medir.Application.MedicPositions.Commands.DeleteMedicPosition
{
    public class DeleteMedicPositionCommand : IRequest
    {
        [BindProperty]
        public Guid MedicId { get; set; }

        [BindProperty]
        public Guid PositionId { get; set; }
    }
}
