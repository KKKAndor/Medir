using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Medir.Application.MedicPositions.Queries.GetMedicPositionDetails
{
    public class GetMedicPositionDetailsQuery : IRequest<MedicPositionDetailsVm>
    {
        [BindProperty]
        public Guid MedicId { get; set; }

        [BindProperty]
        public Guid PositionId { get; set; }
    }
}
