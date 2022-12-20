using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Application.MedicPolyclinics.Commands.CreateMedicPolyclinic;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Medir.WebApi.Areas.Administrator.Models.MedicPolyclinics
{
    public class CreateMedicPolyclinicDto : IMapWith<CreateMedicPolyclinicCommand>
    {
        [Required(ErrorMessage = "Не выбран врач")]
        [Display(Name = "Врач")]
        [BindProperty]
        public Guid MedicId { get; set; }

        [Required(ErrorMessage = "Не выбрана поликлиника")]
        [Display(Name = "Поликлиника")]
        [BindProperty]
        public Guid PolyclinicId { get; set; }

        [Required(ErrorMessage = "Не введена цена за услуги врача в поликлинике")]
        [Display(Name = "Цена за услуги врача в поликлинике")]
        [BindProperty]
        public int Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateMedicPolyclinicDto, CreateMedicPolyclinicCommand>()
                .ForMember(Command => Command.MedicId,
                    opt => opt.MapFrom(DTO => DTO.MedicId))
                .ForMember(Command => Command.PolyclinicId,
                    opt => opt.MapFrom(DTO => DTO.PolyclinicId))
                .ForMember(Command => Command.Price,
                    opt => opt.MapFrom(DTO => DTO.Price));
        }
    }
}
