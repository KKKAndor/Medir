using AutoMapper;
using Medir.Application.Cities.Commands.CreateCity;
using Medir.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Medir.WebApi.Areas.Administrator.Models.Cities
{
    public class CreateCityDto : IMapWith<CreateCityCommand>
    {
        [StringLength(50)]
        [Display(Name = "Название города")]
        [Required(ErrorMessage = "Не написано название города")]
        public string CityName { get; set; } = string.Empty;

        [Display(Name = "Широта")]
        [Required(ErrorMessage = "Введите широту")]
        public double Latitude { get; set; }

        [Display(Name = "Долгота")]
        [Required(ErrorMessage = "Введите долготу")]
        public double Longitude { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCityDto, CreateCityCommand>()
                .ForMember(Command => Command.CityName,
                    opt => opt.MapFrom(DTO => DTO.CityName))
                .ForMember(Command => Command.Latitude,
                    opt => opt.MapFrom(DTO => DTO.Latitude))
                .ForMember(Command => Command.Longitude,
                    opt => opt.MapFrom(DTO => DTO.Longitude));
        }
    }
}
