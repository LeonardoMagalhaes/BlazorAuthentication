using Blazored.LocalStorage;

namespace BlazorApp.Web.Services
{
    public class ManageLocalStorageService : IManageLocalStorageService
    {
        const string key = "authToken";

        private readonly ILocalStorageService _localStorageService;

        public ManageLocalStorageService(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async Task<string> GetAuthToken() => await _localStorageService.GetItemAsync<string>(key);
        public async Task AddAuthToken(string token) => await _localStorageService.SetItemAsync(key, token);
        public async Task RemoveAuthToken(string token) => await _localStorageService.RemoveItemAsync(key);
    }
}
