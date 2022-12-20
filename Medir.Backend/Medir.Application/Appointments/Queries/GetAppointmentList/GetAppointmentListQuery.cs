using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Medir.Application.Appointments.Queries.GetAppointmentList
{
    public class GetAppointmentListQuery : IRequest<AppointmentsListVm>
    {
        [BindProperty]
        public string UserEmail { get; set; }
    }
}
