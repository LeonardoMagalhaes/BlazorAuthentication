namespace BlazorApp.Web.Services
{
    public interface ILoginService
    {
        Task<string?> Authenticate(string email, string password);
    }
}
