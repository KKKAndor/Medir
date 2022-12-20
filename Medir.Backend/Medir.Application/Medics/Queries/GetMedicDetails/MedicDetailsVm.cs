using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Domain;
namespace Medir.Application.Medics.Queries.GetMedicDetails
{
    public class MedicDetailsVm : IMapWith<Medic>
    {
        public Guid MedicId { get; set; }

        public string MedicFullName { get; set; } = string.Empty;

        public string ShortDescription { get; set; } = string.Empty;

        public string FullDescription { get; set; } = string.Empty;

        public string MedicPhoneNumber { get; set; } = string.Empty;

        public string MedicPhoto { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Medic, MedicDetailsVm>()
                .ForMember(Vm => Vm.MedicId,
                    opt => opt.MapFrom(ap => ap.MedicId))
                .ForMember(Vm => Vm.MedicFullName,
                    opt => opt.MapFrom(ap => ap.MedicFullName))
                .ForMember(Vm => Vm.ShortDescription,
                    opt => opt.MapFrom(ap => ap.ShortDescription))
                .ForMember(Vm => Vm.FullDescription,
                    opt => opt.MapFrom(ap => ap.FullDescription))
                .ForMember(Vm => Vm.MedicPhoneNumber,
                    opt => opt.MapFrom(ap => ap.MedicPhoneNumber))
                .ForMember(Vm => Vm.MedicPhoto,
                    opt => opt.MapFrom(ap => ap.MedicPhoto));
        }
    }
}
