using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Application.Polyclinics.Commands.UpdatePolyclinic;
using System.ComponentModel.DataAnnotations;

namespace Medir.WebApi.Areas.Administrator.Models.Polyclinics
{
    public class UpdatePolyclinicDto : IMapWith<UpdatePolyclinicCommand>
    {
        [Required(ErrorMessage = "Не выбрана поликлиника")]
        [Display(Name = "Поликлиника")]
        public Guid PolyclinicId { get; set; }

        [Required(ErrorMessage = "Не выбран город")]
        [Display(Name = "Город")]
        public Guid CityId { get; set; }

        [StringLength(50)]
        [Display(Name = "Имя поликлиники")]
        [Required(ErrorMessage = "Не написано имя поликлиники")]
        public string PolyclinicName { get; set; }

        //[Url]
        [Display(Name = "Адрес поликлиники")]
        [Required(ErrorMessage = "Не написан адрес поликлиники")]
        public string PolyclinicAddress { get; set; }

        [Phone]
        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Не написан номер телефона")]
        public string PolyclinicPhoneNumber { get; set; }

        //[Url]
        [Display(Name = "Фото")]
        [Required(ErrorMessage = "Не выбрано на фото")]
        public string PolyclinicPhoto { get; set; }

        [Display(Name = "Широта")]
        [Required(ErrorMessage = "Введите широту")]
        public double Latitude { get; set; }

        [Display(Name = "Долгота")]
        [Required(ErrorMessage = "Введите долготу")]
        public double Longitude { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePolyclinicDto, UpdatePolyclinicCommand>()
                .ForMember(Command => Command.PolyclinicId,
                    opt => opt.MapFrom(DTO => DTO.PolyclinicId))
                .ForMember(Command => Command.CityId,
                    opt => opt.MapFrom(DTO => DTO.CityId))
                .ForMember(Command => Command.PolyclinicName,
                    opt => opt.MapFrom(DTO => DTO.PolyclinicName))
                .ForMember(Command => Command.PolyclinicAddress,
                    opt => opt.MapFrom(DTO => DTO.PolyclinicAddress))
                .ForMember(Command => Command.PolyclinicPhoneNumber,
                    opt => opt.MapFrom(DTO => DTO.PolyclinicPhoneNumber))
                .ForMember(Command => Command.PolyclinicPhoto,
                    opt => opt.MapFrom(DTO => DTO.PolyclinicPhoto))
                .ForMember(Command => Command.Latitude,
                    opt => opt.MapFrom(DTO => DTO.Latitude))
                .ForMember(Command => Command.Longitude,
                    opt => opt.MapFrom(DTO => DTO.Longitude));
        }
    }
}
