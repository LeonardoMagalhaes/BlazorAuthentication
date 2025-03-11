namespace BlazorApp.Models.DTOs
{
    public class AuthenticateUserResponseDTO<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public T? User { get; set; }
    }
}
