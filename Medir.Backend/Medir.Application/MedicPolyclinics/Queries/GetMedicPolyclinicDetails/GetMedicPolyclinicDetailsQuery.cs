using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Medir.Application.MedicPolyclinics.Queries.GetMedicPolyclinicDetails
{
    public class GetMedicPolyclinicDetailsQuery : IRequest<MedicPolyclinicDetailsVm>
    {
        [BindProperty]
        public Guid MedicId { get; set; }

        [BindProperty]
        public Guid PolyclinicId { get; set; }
    }
}
