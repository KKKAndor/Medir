using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Domain;

namespace Medir.Application.Polyclinics.Queries.GetPolyclinicList
{
    public class PolyclinicLookUpDto : IMapWith<Polyclinic>
    {
        public Guid PolyclinicId { get; set; }

        public string PolyclinicName { get; set; } = string.Empty;

        public string PolyclinicAddress { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Polyclinic, PolyclinicLookUpDto>()
                .ForMember(Vm => Vm.PolyclinicId,
                    opt => opt.MapFrom(ap => ap.PolyclinicId))
                .ForMember(Vm => Vm.PolyclinicName,
                    opt => opt.MapFrom(ap => ap.PolyclinicName))
                .ForMember(Vm => Vm.PolyclinicAddress,
                    opt => opt.MapFrom(ap => ap.PolyclinicAddress));
        }
    }
}
