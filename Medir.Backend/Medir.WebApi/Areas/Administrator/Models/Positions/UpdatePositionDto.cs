using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Application.Positions.Commands.UpdatePosition;
using System.ComponentModel.DataAnnotations;

namespace Medir.WebApi.Areas.Administrator.Models.Positions
{
    public class UpdatePositionDto : IMapWith<UpdatePositionCommand>
    {
        [Required(ErrorMessage = "Не выбрана специальность")]
        [Display(Name = "Специальность")]
        public Guid PositionId { get; set; }

        [StringLength(100)]
        [Display(Name = "Название специальности")]
        [Required(ErrorMessage = "Не написано название специальности")]
        public string PositionName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdatePositionDto, UpdatePositionCommand>()
                .ForMember(Command => Command.PositionId,
                    opt => opt.MapFrom(DTO => DTO.PositionId))
                .ForMember(Command => Command.PositionName,
                    opt => opt.MapFrom(DTO => DTO.PositionName));
        }
    }
}
