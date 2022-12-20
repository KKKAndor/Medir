namespace Medir.Domain
{
    public class City
    {
        public Guid CityId { get; set; }

        public string CityName { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public List<Polyclinic> Polyclinics { get; set; } = new();
    }
}
