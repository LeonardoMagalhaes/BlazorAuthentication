﻿namespace BlazorApp.Models.DTOs
{
    public class UserResponseDTO
    {
        public string Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
