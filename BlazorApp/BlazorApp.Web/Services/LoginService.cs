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

        public async Task<object> Authenticate(string email, string password)
        {
            try
            {
                var request = new AuthenticateUserRequestDTO { Email = email, Password = password };

                var response = await _httpClient.PostAsJsonAsync<object>("api/User/authenticate", request);

                if (response.IsSuccessStatusCode)// status code entre 200 a 299
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        // retorna o valor "padrão" ou vazio
                        // para uma objeto do tipo carrinhoItemDto
                        //return default(CarrinhoItemDto);
                        return null;
                    }
                    //le o conteudo HTTP e retorna o valor resultante
                    //da serialização do conteudo JSON para o objeto Dto
                        return null;
                    //return await response.Content.ReadFromJsonAsync<CarrinhoItemDto>();
                }
                else
                {
                    //serializa o conteudo HTTP como uma string
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.StatusCode} Message -{message}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            //_httpClient.BaseAddress = new Uri("https://localhost:7075");
            //var loginData = new { Email = Model.Email, Model.Password };
            //var response = await _httpClient.PostAsJsonAsync("api/User/authenticate", loginData);

            //if (response.IsSuccessStatusCode)
            //{
            //    var token = await response.Content.ReadAsStringAsync();
            //    await JS.InvokeVoidAsync("localStorage.setItem", "authToken", token);
            //    Navigation.NavigateTo("/");
            //}
            //else
            //{
            //    errorMessage = "Authentication failed.";
            //}
        }
    }
}
