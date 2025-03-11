namespace BlazorApp.Web.Services
{
    public interface IManageLocalStorageService
    {
        Task<string> GetAuthToken();
        Task AddAuthToken(string token);
        Task RemoveAuthToken(string token);
    }
}
