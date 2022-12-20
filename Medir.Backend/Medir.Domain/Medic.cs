namespace Medir.Domain
{
    public class Medic
    {
        public Guid MedicId { get; set; }        
                
        public string MedicFullName { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string FullDescription { get; set; } = string.Empty;

        public string MedicPhoneNumber { get; set; } = string.Empty;

        public string MedicPhoto { get; set; } = string.Empty;

        public List<MedicPolyclinic> MedicPolyclinics { get; set; } = new();

        public List<MedicPosition> MedicPositions { get; set; } = new();

        public List<MedicAppointmentAvailability> MedicAppointmentAvailabilities { get; set; } = new();
    }
}
