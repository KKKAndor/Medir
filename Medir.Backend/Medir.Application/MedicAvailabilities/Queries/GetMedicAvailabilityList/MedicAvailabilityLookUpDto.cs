
namespace Medir.Application.MedicAvailabilities.Queries.GetMedicAvailabilityList
{
    public class MedicAvailabilityLookUpDto
    {
        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public DateTime Date { get; set; }

        public string? PolyclinicName { get; set; }

        public string? PositionName { get; set; }

        public Guid PolyclinicId { get; set; }

        public Guid PositionId { get; set; }
    }
}
