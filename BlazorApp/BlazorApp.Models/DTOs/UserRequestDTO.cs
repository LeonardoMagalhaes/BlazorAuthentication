using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models.DTOs
{
    public class UserRequestDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "USER";
    }
}
