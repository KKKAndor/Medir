using MediatR;

namespace Medir.Application.Polyclinics.Commands.UpdatePolyclinic
{
    public class UpdatePolyclinicCommand : IRequest
    {
        public Guid PolyclinicId { get; set; }

        public Guid CityId { get; set; }

        public string PolyclinicName { get; set; } = string.Empty;

        public string PolyclinicAddress { get; set; } = string.Empty;

        public string PolyclinicPhoneNumber { get; set; } = string.Empty;

        public string PolyclinicPhoto { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
