using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorApp.Web;
using Blazored.LocalStorage;
using BlazorApp.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/accessdenied";
        options.Cookie.Name = "auth_cookie";
        options.LogoutPath = "/logout";
    });

//builder.Services.AddAuthorizationCore();

// Services
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IManageLocalStorageService, ManageLocalStorageService>();

var baseUrl = "https://localhost:7075";
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(baseUrl)
});


builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

//app.UseAuthentication();
//app.UseAuthorization();

await app.RunAsync();
