namespace Medir.Domain
{
    public class Polyclinic
    {
        public Guid PolyclinicId { get; set; }

        public Guid CityId { get; set; }

        public City? City { get; set; }

        public string PolyclinicName { get; set; } = string.Empty;

        public string PolyclinicAddress { get; set; } = string.Empty;

        public string PolyclinicPhoneNumber { get; set; } = string.Empty;

        public string PolyclinicPhoto { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }        

        public List<MedicPolyclinic> MedicPolyclinics { get; set; } = new();

        public List<MedicAppointmentAvailability> MedicAppointmentAvailabilities { get; set; } = new();
    }
}
