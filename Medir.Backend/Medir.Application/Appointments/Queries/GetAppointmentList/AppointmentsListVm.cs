using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Application.Appointments.Queries.GetAppointmentList
{
    public class AppointmentsListVm
    {
        public IList<AppointmentLookUpDto> Appointments { get; set; }
    }
}
