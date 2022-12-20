using AutoMapper;
using IdentityServer4.Models;
using Medir.Application.Cities.Commands.CreateCity;
using Medir.Application.Common.Mappings;
using Medir.Application.MedicAvailabilities.Command.CreateMedicAvailability;
using Medir.WebApi.Areas.Administrator.Models.Cities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Medir.WebApi.Areas.Administrator.Models.MedicAvailabilities
{
    public class CreateMedicAvailabilityDto : IMapWith<CreateMedicAvailabilityCommand>
    {
        [Display(Name = "Врач")]
        [Required(ErrorMessage = "Выберите врача")]
        public Guid MedicId { get; set; }

        [Display(Name = "Специальность")]
        [Required(ErrorMessage = "Выберите специальность")]
        public Guid PositionId { get; set; }

        [Display(Name = "Поликилника")]
        [Required(ErrorMessage = "Выберите поликилнику")]
        public Guid PolyclinicId { get; set; }

        [Display(Name = "Дата приема")]
        [Required(ErrorMessage = "Выберите дату приема")]
        public DateTime Date { get; set; }

        [Display(Name = "Начало приема")]
        [Required(ErrorMessage = "Выберите начало приема")]
        public DateTime TimeStart { get; set; }

        [Display(Name = "Конец приема")]
        [Required(ErrorMessage = "Выберите конец приема")]
        public DateTime TimeEnd { get; set; }

        [Display(Name = "Время приема")]
        [Required(ErrorMessage = "Выберите время приема")]
        public int ReceptionMinutesTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateMedicAvailabilityDto, CreateMedicAvailabilityCommand>()
                .ForMember(Command => Command.MedicId,
                    opt => opt.MapFrom(DTO => DTO.MedicId))
                .ForMember(Command => Command.PositionId,
                    opt => opt.MapFrom(DTO => DTO.PositionId))
                .ForMember(Command => Command.PolyclinicId,
                    opt => opt.MapFrom(DTO => DTO.PolyclinicId))
                .ForMember(Command => Command.Date,
                    opt => opt.MapFrom(DTO => DTO.Date))
                .ForMember(Command => Command.TimeStart,
                    opt => opt.MapFrom(DTO => DTO.TimeStart))
                .ForMember(Command => Command.TimeEnd,
                    opt => opt.MapFrom(DTO => DTO.TimeEnd))
                .ForMember(Command => Command.ReceptionMinutesTime,
                    opt => opt.MapFrom(DTO => DTO.ReceptionMinutesTime));
        }
    }
}
