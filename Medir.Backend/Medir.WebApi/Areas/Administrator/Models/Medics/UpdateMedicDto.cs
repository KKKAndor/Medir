using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.Application.Medics.Commands.UpdateMedic;
using System.ComponentModel.DataAnnotations;

namespace Medir.WebApi.Areas.Administrator.Models.Medics
{
    public class UpdateMedicDto : IMapWith<UpdateMedicCommand>
    {
        [Required(ErrorMessage = "Не выбран врач")]
        [Display(Name = "Врач")]
        public Guid MedicId { get; set; }

        [StringLength(100)]
        [Display(Name = "Полное имя врача")]
        [Required(ErrorMessage = "Не написано полное имя врача")]
        public string MedicFullName { get; set; } = string.Empty;

        [StringLength(100)]
        [Display(Name = "Короткое описание врача")]
        [Required(ErrorMessage = "Не написано короткое описание врача")]
        public string ShortDescription { get; set; } = string.Empty;

        [StringLength(250)]
        [Display(Name = "Полное описание врача")]
        [Required(ErrorMessage = "Не написано полное описание врача")]
        [DataType(DataType.MultilineText)]
        public string FullDescription { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Номер телефона")]
        [Required(ErrorMessage = "Не написан номер телефона")]
        public string MedicPhoneNumber { get; set; } = string.Empty;

        [Display(Name = "Фото")]
        [Required(ErrorMessage = "Не выбрано на фото")]
        public string MedicPhoto { get; set; } = string.Empty;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateMedicDto, UpdateMedicCommand>()
                .ForMember(Command => Command.MedicId,
                    opt => opt.MapFrom(DTO => DTO.MedicId))
                .ForMember(Command => Command.MedicFullName,
                    opt => opt.MapFrom(DTO => DTO.MedicFullName))
                .ForMember(Command => Command.ShortDescription,
                    opt => opt.MapFrom(DTO => DTO.ShortDescription))
                .ForMember(Command => Command.FullDescription,
                    opt => opt.MapFrom(DTO => DTO.FullDescription))
                .ForMember(Command => Command.MedicPhoneNumber,
                    opt => opt.MapFrom(DTO => DTO.MedicPhoneNumber))
                .ForMember(Command => Command.MedicPhoto,
                    opt => opt.MapFrom(DTO => DTO.MedicPhoto));
        }
    }
}
