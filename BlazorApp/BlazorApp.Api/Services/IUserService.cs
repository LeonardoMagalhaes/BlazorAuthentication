using BlazorApp.Api.Models;
using BlazorApp.Models.DTOs;

namespace BlazorApp.Api.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(string id);
        User CreateUser(UserRequestDTO user);
        string AuthenticateUser(string email, string password);
    }
}
