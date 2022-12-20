using AutoMapper;
using Medir.Application.Cities.Commands.UpdateCity;
using Medir.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Medir.WebApi.Areas.Administrator.Models.Cities
{
    public class UpdateCityDto : IMapWith<UpdateCityCommand>
    {
        [Required(ErrorMessage = "Не выбран город")]
        [Display(Name = "Город")]
        public Guid CityId { get; set; }

        [StringLength(50)]
        [Display(Name = "Название города")]
        [Required(ErrorMessage = "Не написано название города")]
        public string CityName { get; set; }

        [Display(Name = "Широта")]
        [Required(ErrorMessage = "Введите широту")]
        public double Latitude { get; set; }

        [Display(Name = "Долгота")]
        [Required(ErrorMessage = "Введите долготу")]
        public double Longitude { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCityDto, UpdateCityCommand>()
                .ForMember(Command => Command.CityId,
                    opt => opt.MapFrom(DTO => DTO.CityId))
                .ForMember(Command => Command.CityName,
                    opt => opt.MapFrom(DTO => DTO.CityName))
                .ForMember(Command => Command.Latitude,
                    opt => opt.MapFrom(DTO => DTO.Latitude))
                .ForMember(Command => Command.Longitude,
                    opt => opt.MapFrom(DTO => DTO.Longitude));
        }
    }
}
