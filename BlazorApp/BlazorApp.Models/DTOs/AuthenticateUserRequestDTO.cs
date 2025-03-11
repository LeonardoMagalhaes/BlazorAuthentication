namespace BlazorApp.Models.DTOs
{
    public class AuthenticateUserRequestDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
