using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Application.MedicPositions.Commands.UpdateMedicPosition;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Medir.WebApi.Areas.Administrator.Models.MedicPositions
{
    public class UpdateMedicPositionDto : IMapWith<UpdateMedicPositionCommand>
    {
        [Required(ErrorMessage = "Не выбран врач")]
        [Display(Name = "Врач")]
        [BindProperty]
        public Guid MedicId { get; set; }

        [Required(ErrorMessage = "Не выбрана специальность")]
        [Display(Name = "Специальность")]
        [BindProperty]
        public Guid PositionId { get; set; }

        [Required(ErrorMessage = "Не введена дата начала работы по специальности")]
        [Display(Name = "Дата начала работы по специальности")]
        [DataType(DataType.Date)]
        [BindProperty]
        public string? DateOnPosition { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateMedicPositionDto, UpdateMedicPositionCommand>()
                .ForMember(Command => Command.MedicId,
                    opt => opt.MapFrom(DTO => DTO.MedicId))
                .ForMember(Command => Command.PositionId,
                    opt => opt.MapFrom(DTO => DTO.PositionId))
                .ForMember(Command => Command.DateOnPosition,
                    opt => opt.MapFrom(DTO => DTO.DateOnPosition));
        }
    }
}
