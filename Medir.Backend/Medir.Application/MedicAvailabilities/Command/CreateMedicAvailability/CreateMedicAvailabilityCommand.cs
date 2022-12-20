using MediatR;

namespace Medir.Application.MedicAvailabilities.Command.CreateMedicAvailability
{
    public class CreateMedicAvailabilityCommand : IRequest
    {
        public Guid MedicId { get; set; }

        public Guid PositionId { get; set; }

        public Guid PolyclinicId { get; set; }

        public DateTime Date { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public int ReceptionMinutesTime { get; set; }
    }
}
