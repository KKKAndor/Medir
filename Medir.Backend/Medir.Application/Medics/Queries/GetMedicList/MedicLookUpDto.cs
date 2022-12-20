using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Domain;

namespace Medir.Application.Medics.Queries.GetMedicList
{
    public class MedicLookUpDto : IMapWith<Medic>
    {
        public Guid MedicId { get; set; }

        public string MedicFullName { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Medic, MedicLookUpDto>()
                .ForMember(Vm => Vm.MedicId,
                    opt => opt.MapFrom(ap => ap.MedicId))
                .ForMember(Vm => Vm.MedicFullName,
                    opt => opt.MapFrom(ap => ap.MedicFullName))
                .ForMember(Vm => Vm.ShortDescription,
                    opt => opt.MapFrom(ap => ap.ShortDescription));
        }
    }
}
