using AutoMapper;
using IdentityServer4.Models;
using Medir.Application.Appointments.Commands.CreateAppointment;
using Medir.Application.Cities.Commands.CreateCity;
using Medir.Application.Common.Mappings;
using Medir.WebApi.Areas.Administrator.Models.Cities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Medir.WebApi.Entities.Models
{
    public class CreateAppointmentDto : IMapWith<CreateAppointmentCommand>
    {
        [Display(Name = "Id Доступность медика")]
        [Required(ErrorMessage = "Не написан Id Доступность медика")]
        public Guid MedicAppointmentAvailabilityId { get; set; }

        [Display(Name = "Почта")]
        [Required(ErrorMessage = "Не написана почта")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [StringLength(500)]
        [Display(Name = "Что беспокоит")]
        public string? Prescription { get; set; } = string.Empty;

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Time { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAppointmentDto, CreateAppointmentCommand>()
                .ForMember(Command => Command.MedicAppointmentAvailabilityId,
                    opt => opt.MapFrom(DTO => DTO.MedicAppointmentAvailabilityId))
                .ForMember(Command => Command.UserEmail,
                    opt => opt.MapFrom(DTO => DTO.UserEmail))
                .ForMember(Command => Command.Prescription,
                    opt => opt.MapFrom(DTO => DTO.Prescription))
                .ForMember(Command => Command.Date,
                    opt => opt.MapFrom(DTO => DTO.Date))
                .ForMember(Command => Command.Time,
                    opt => opt.MapFrom(DTO => DTO.Time));
        }
    }
}
