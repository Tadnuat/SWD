using System.ComponentModel.DataAnnotations;

namespace Users.Services.API.Models.Dtos
{
    public class UpdateUserDTO
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Role { get; set; }

        public bool IsActive { get; set; }
    }
} 