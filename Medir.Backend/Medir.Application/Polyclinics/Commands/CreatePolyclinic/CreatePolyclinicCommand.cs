using MediatR;

namespace Medir.Application.Polyclinics.Commands.CreatePolyclinic
{
    public class CreatePolyclinicCommand : IRequest<Guid>
    {
        public Guid CityId { get; set; }

        public string PolyclinicName { get; set; } = string.Empty;

        public string PolyclinicAddress { get; set; } = string.Empty;

        public string PolyclinicPhoneNumber { get; set; } = string.Empty;

        public string PolyclinicPhoto { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
