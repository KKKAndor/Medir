using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Domain;

namespace Medir.Application.Positions.Queries.GetPositionList
{
    public class PositionLookUpDto : IMapWith<Position>
    {
        public Guid PositionId { get; set; }

        public string? PositionName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Position, PositionLookUpDto>()
                .ForMember(Vm => Vm.PositionId,
                    opt => opt.MapFrom(ap => ap.PositionId))
                .ForMember(Vm => Vm.PositionName,
                    opt => opt.MapFrom(ap => ap.PositionName));
        }
    }
}
