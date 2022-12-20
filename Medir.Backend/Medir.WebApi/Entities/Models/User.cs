using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medir.WebApi.Entities.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; } = string.Empty;

        public string? Patronymic { get; set; } = string.Empty;

        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; } = new DateTime();

        public string? Country { get; set; } = string.Empty;

        public string? Region { get; set; } = string.Empty;

        public string? Street { get; set; } = string.Empty;

        public string? House { get; set; } = string.Empty;

        public string? Address { get; set; } = string.Empty;

        public string? Gender { get; set; } = string.Empty;

        public string? PolyceNumber { get; set; } = string.Empty;

        public bool Registered { get; set; } = false;
    }
}
