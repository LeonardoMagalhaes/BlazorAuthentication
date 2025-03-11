using BlazorApp.Models.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace BlazorApp.Web.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient? _httpClient;

        public LoginService(HttpClient? httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> Authenticate(string email, string password)
        {
            try
            {   
                var request = new AuthenticateUserRequestDTO { Email = email, Password = password };

                var response = await _httpClient.PostAsJsonAsync("api/User/authenticate", request);


                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return null;
                    }

                    var parsedResponse = response.Content.ReadFromJsonAsync<AuthenticateUserResponseDTO<AuthenticateUserRequestDTO>>();

                    return parsedResponse?.Result?.Token;
                }
                else
                {
                    Console.WriteLine(response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
