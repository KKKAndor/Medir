using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Medir.Application.MedicPolyclinics.Commands.UpdateMedicPolyclinic
{
    public class UpdateMedicPolyclinicCommand : IRequest
    {
        [BindProperty]
        public Guid MedicId { get; set; }

        [BindProperty]
        public Guid PolyclinicId { get; set; }

        [BindProperty]
        public int Price { get; set; }
    }
}
