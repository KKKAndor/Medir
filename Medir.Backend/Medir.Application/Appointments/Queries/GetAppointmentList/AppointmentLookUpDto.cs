namespace Medir.Application.Appointments.Queries.GetAppointmentList
{
    public class AppointmentLookUpDto
    {
        public Guid AppointmentId { get; set; }

        public string Prescription { get; set; }

        public string MedicFullName { get; set; } = string.Empty;

        public string PositionName { get; set; } = string.Empty;

        public string PolyclinicName { get; set; } = string.Empty;

        public string PolyclinicAddress { get; set; } = string.Empty;

        public string CityName { get; set; } = string.Empty;

        public double Price { get; set; }

        public DateTime Time { get; set; }

        public DateTime Date { get; set; }
    }
}
