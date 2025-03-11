namespace BlazorApp.Web.Services
{
    public interface ILoginService
    {
        Task<object> Authenticate(string email, string password);
    }
}
