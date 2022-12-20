using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Domain;

namespace Medir.Application.MedicPolyclinics.Queries.GetMedicPolyclinicDetails
{
    public class MedicPolyclinicDetailsVm : IMapWith<MedicPolyclinic>
    {
        public Guid MedicId { get; set; }

        public Guid PolyclinicId { get; set; }

        public int Price { get; set; }

        public string PolyclinicName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MedicPolyclinic, MedicPolyclinicDetailsVm>()
                .ForMember(Vm => Vm.MedicId,
                    opt => opt.MapFrom(ap => ap.MedicId))
                .ForMember(Vm => Vm.PolyclinicId,
                    opt => opt.MapFrom(ap => ap.PolyclinicId))
                .ForMember(Vm => Vm.Price,
                    opt => opt.MapFrom(ap => ap.Price))
                .ForMember(Vm => Vm.PolyclinicName,
                    opt => opt.MapFrom(ap => ap.Polyclinic.PolyclinicName));
        }
    }
}
