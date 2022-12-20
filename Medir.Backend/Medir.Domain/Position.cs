namespace Medir.Domain
{
    public class Position
    {
        public Guid PositionId { get; set; }

        public string PositionName { get; set; } = string.Empty;

        public List<MedicPosition> MedicPositions { get; set; } = new();

        public List<MedicAppointmentAvailability> MedicAppointmentAvailabilities { get; set; } = new();
    }
}
