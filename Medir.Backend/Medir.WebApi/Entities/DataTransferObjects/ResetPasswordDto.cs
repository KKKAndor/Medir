using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations;

namespace Medir.WebApi.Entities.DataTransferObjects
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "Пароль обязателен")]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string? ConfirmPassword { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}
