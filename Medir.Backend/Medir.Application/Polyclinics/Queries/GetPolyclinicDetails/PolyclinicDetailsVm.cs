using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Domain;

namespace Medir.Application.Polyclinics.Queries.GetPolyclinicDetails
{
    public class PolyclinicDetailsVm : IMapWith<Polyclinic>
    {
        public Guid PolyclinicId { get; set; }

        public Guid CityId { get; set; }

        public string PolyclinicName { get; set; } = string.Empty;

        public string PolyclinicAddress { get; set; } = string.Empty;

        public string PolyclinicPhoneNumber { get; set; } = string.Empty;

        public string PolyclinicPhoto { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Polyclinic, PolyclinicDetailsVm>()
                .ForMember(Vm => Vm.PolyclinicId,
                    opt => opt.MapFrom(ap => ap.PolyclinicId))
                .ForMember(Vm => Vm.CityId,
                    opt => opt.MapFrom(ap => ap.CityId))
                .ForMember(Vm => Vm.PolyclinicName,
                    opt => opt.MapFrom(ap => ap.PolyclinicName))
                .ForMember(Vm => Vm.PolyclinicAddress,
                    opt => opt.MapFrom(ap => ap.PolyclinicAddress))
                .ForMember(Vm => Vm.PolyclinicPhoneNumber,
                    opt => opt.MapFrom(ap => ap.PolyclinicPhoneNumber))
                .ForMember(Vm => Vm.PolyclinicPhoto,
                    opt => opt.MapFrom(ap => ap.PolyclinicPhoto))
                .ForMember(Vm => Vm.Latitude,
                    opt => opt.MapFrom(ap => ap.Latitude))
                .ForMember(Vm => Vm.Longitude,
                    opt => opt.MapFrom(ap => ap.Longitude));
        }
    }
}
