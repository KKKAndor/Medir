using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Domain;

namespace Medir.Application.Cities.Queries.GetCityList
{
    public class CityLookUpDto : IMapWith<City>
    {
        public Guid CityId { get; set; }

        public string? CityName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<City, CityLookUpDto>()
                .ForMember(Vm => Vm.CityId,
                    opt => opt.MapFrom(ap => ap.CityId))
                .ForMember(Vm => Vm.CityName,
                    opt => opt.MapFrom(ap => ap.CityName));
        }
    }
}
