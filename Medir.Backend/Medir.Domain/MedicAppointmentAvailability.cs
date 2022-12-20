using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Domain
{
    public class MedicAppointmentAvailability
    {
        public Guid MedicAppointmentAvailabilityId { get; set; }

        public Guid MedicId { get; set; }

        public Medic? Medic { get; set; }

        public Guid PositionId { get; set; }

        public Position? Position { get; set; }

        public Guid PolyclinicId { get; set; }

        public Polyclinic? Polyclinic { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTime TimeStart { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTime TimeEnd { get; set; }

        public bool IsAvailable { get; set; } = true;

        public Guid AppointmentId { get; set; } = Guid.Empty;

        public Appointment? Appointment { get; set; }
    }
}
