using MediatR;
using Medir.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medir.Application.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommand : IRequest<Guid>
    {
        public Guid MedicAppointmentAvailabilityId { get; set; }

        public string? UserEmail { get; set; }

        public string? Prescription { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }
    }
}
