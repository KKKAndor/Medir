using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Domain
{
    public class Appointment
    {
        public Guid AppointmentId { get; set; }
        
        public string? UserEmail { get; set; }

        public string? Prescription { get; set; } = string.Empty;

        public double Price { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTime Time { get; set; }

        [Column(TypeName = "Date")]
        public DateTime Date { get; set; }

        public Guid MedicAppointmentAvailabilityId { get; set; }

        public MedicAppointmentAvailability MedicAppointmentAvailability { get; set; } 
    }
}
