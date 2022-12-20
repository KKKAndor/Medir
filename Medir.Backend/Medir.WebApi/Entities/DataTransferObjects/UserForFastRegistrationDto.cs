using AutoMapper;
using Medir.Application.Common.Mappings;
using Medir.WebApi.Entities.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Medir.WebApi.Entities.DataTransferObjects
{
    public class UserForFastRegistrationDto
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

        [Required]
        public string? Gender { get; set; }
    }
}
