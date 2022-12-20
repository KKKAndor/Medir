using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Domain;

namespace Medir.Application.Cities.Queries.GetCityDetails
{
    public class CityDetailsVm : IMapWith<City>
    {
        public Guid CityId { get; set; }

        public string CityName { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<City, CityDetailsVm>()
                .ForMember(Vm => Vm.CityId,
                    opt => opt.MapFrom(ap => ap.CityId))
                .ForMember(Vm => Vm.CityName,
                    opt => opt.MapFrom(ap => ap.CityName))
                .ForMember(Vm => Vm.Latitude,
                    opt => opt.MapFrom(ap => ap.Latitude))
                .ForMember(Vm => Vm.Longitude,
                    opt => opt.MapFrom(ap => ap.Longitude));
        }
    }
}
