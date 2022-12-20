using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Domain;

namespace Medir.Application.MedicPositions.Queries.GetMedicPositionList
{
    public class MedicPositionLookUpDto : IMapWith<MedicPosition>
    {
        public Guid MedicId { get; set; }

        public Guid PositionId { get; set; }

        public DateTime DateOnPosition { get; set; }

        public string PositionName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MedicPosition, MedicPositionLookUpDto>()
                .ForMember(Vm => Vm.MedicId,
                    opt => opt.MapFrom(ap => ap.MedicId))
                .ForMember(Vm => Vm.PositionId,
                    opt => opt.MapFrom(ap => ap.PositionId))
                .ForMember(Vm => Vm.DateOnPosition,
                    opt => opt.MapFrom(ap => ap.DateOnPosition))
                .ForMember(Vm => Vm.PositionName,
                    opt => opt.MapFrom(ap => ap.Position.PositionName));
        }
    }
}
