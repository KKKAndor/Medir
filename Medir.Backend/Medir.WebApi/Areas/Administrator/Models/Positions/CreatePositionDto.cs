using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Application.Positions.Commands.CreatePosition;
using System.ComponentModel.DataAnnotations;

namespace Medir.WebApi.Areas.Administrator.Models.Positions
{
    public class CreatePositionDto : IMapWith<CreatePositionCommand>
    {
        [StringLength(100)]
        [Display(Name = "Название специальности")]
        [Required(ErrorMessage = "Не написано название специальности")]
        public string PositionName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreatePositionDto, CreatePositionCommand>()
                .ForMember(Command => Command.PositionName,
                    opt => opt.MapFrom(DTO => DTO.PositionName));
        }
    }
}
