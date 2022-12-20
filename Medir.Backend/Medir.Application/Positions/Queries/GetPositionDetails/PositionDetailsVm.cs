using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Domain;

namespace Medir.Application.Positions.Queries.GetPositionDetails
{
    public class PositionDetailsVm : IMapWith<Position>
    {
        public Guid PositionId { get; set; }

        public string PositionName { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Position, PositionDetailsVm>()
                .ForMember(Vm => Vm.PositionId,
                    opt => opt.MapFrom(ap => ap.PositionId))
                .ForMember(Vm => Vm.PositionName,
                    opt => opt.MapFrom(ap => ap.PositionName));
        }
    }
}
