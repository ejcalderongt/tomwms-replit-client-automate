using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net.Http.Headers;
using TOMWMSUX.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRazorPages()
    .AddJsonOptions(options =>
    {
        // Para respetar los nombres originales (no camelCase)
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.Cookie.SameSite = SameSiteMode.None; // Para cross-origin si lo necesitas
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Solo en HTTPS
    });

builder.Services.AddHttpClient("APIClient", client =>
{
    //client.BaseAddress = new Uri("https://192.168.1.60:55102/");
    //client.BaseAddress = new Uri("https://localhost:7194/");
    client.BaseAddress = new Uri("http://52.41.114.122:8091");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddScoped<IApiClientService>();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseExceptionHandler("/Error");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.Run();