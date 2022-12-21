using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.WebApi.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace Medir.WebApi.Entities.DataTransferObjects
{
    public class UserForRegistrationDto : IMapWith<User>
    {
        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "Имя")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Фамилия")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Не указано отчество")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Отчество")]
        public string? Patronymic { get; set; }

        [Required(ErrorMessage = "Не указан электронный адрес")]
        [EmailAddress]
        [Display(Name = "e-mail адрес")]
        [ProtectedPersonalData]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Не указан телефонный номер")]
        [Phone]
        [Display(Name = "Телефонный номер")]
        [ProtectedPersonalData]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Не указана дата рождения")]
        [Display(Name = "Дата рождения")]
        public string? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Не указан адрес")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Адрес")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Не указана страна")]
        [Display(Name = "Страна")]
        public string? Country { get; set; } = string.Empty;

        [Required(ErrorMessage = "Не указан регион")]
        [Display(Name = "Регион")]
        public string? Region { get; set; } = string.Empty;

        [Required(ErrorMessage = "Не указана улица")]
        [Display(Name = "Улица")]
        public string? Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "Не указан дом")]
        [Display(Name = "Дом")]
        public string? House { get; set; } = string.Empty;

        [Required(ErrorMessage = "Не указан номер полиса")]
        [Display(Name = "Номер полиса")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Номер полиса состоит из 16 цифр")]
        [RegexStringValidator(@"^\d$")]
        public string? PolyceNumber { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Укажите пароль повторно")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string? PasswordConfirm { get; set; }        

        [Required]
        public string? Gender { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserForRegistrationDto, User>()
                .ForMember(Vm => Vm.UserName,
                    opt => opt.MapFrom(ap => ap.Email))
                .ForMember(Vm => Vm.Email,
                    opt => opt.MapFrom(ap => ap.Email))
                .ForMember(Vm => Vm.FirstName,
                    opt => opt.MapFrom(ap => ap.FirstName))
                .ForMember(Vm => Vm.LastName,
                    opt => opt.MapFrom(ap => ap.LastName))
                .ForMember(Vm => Vm.Patronymic,
                    opt => opt.MapFrom(ap => ap.Patronymic))
                .ForMember(Vm => Vm.DateOfBirth,
                    opt => opt.MapFrom(ap => ap.DateOfBirth))
                .ForMember(Vm => Vm.Address,
                    opt => opt.MapFrom(ap => ap.Address))
                .ForMember(Vm => Vm.Gender,
                    opt => opt.MapFrom(ap => ap.Gender))
                .ForMember(Vm => Vm.Country,
                    opt => opt.MapFrom(ap => ap.Country))
                .ForMember(Vm => Vm.Region,
                    opt => opt.MapFrom(ap => ap.Region))
                .ForMember(Vm => Vm.Street,
                    opt => opt.MapFrom(ap => ap.Street))
                .ForMember(Vm => Vm.House,
                    opt => opt.MapFrom(ap => ap.House))
                .ForMember(Vm => Vm.PolyceNumber,
                    opt => opt.MapFrom(ap => ap.PolyceNumber));
        }        
    }
}
