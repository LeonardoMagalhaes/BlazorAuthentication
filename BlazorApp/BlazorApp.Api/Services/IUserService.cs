using BlazorApp.Api.Models;

namespace BlazorApp.Api.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUser(string id);
        User CreateUser(User user);
        string AuthenticateUser(string email, string password);
    }
}
