using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Medir.Application.MedicPolyclinics.Commands.DeleteMedicPolyclinic
{
    
    public class DeleteMedicPolyclinicCommand : IRequest
    {
        [BindProperty]
        public Guid MedicId { get; set; }

        [BindProperty]
        public Guid PolyclinicId { get; set; }
    }
}
