using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using TOMWMSUX.Models;

namespace TOMWMSUX.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly IApiClientService _authService;

        public LoginModel(IApiClientService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public string username { get; set; } = "";

        [BindProperty]
        public string password { get; set; } = "";

        public string ErrorMessage { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            //Validar con tu servicio de login
            var loginResponse = await _authService.LoginAsync(username, password);

            //Verificar que la respuesta es correcta y tiene propietario
            if (loginResponse != null && loginResponse.Propietario != null)
            {
                //Crear los claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username), // nombre de usuario
                    new Claim("IdPropietario", loginResponse.Propietario.IdPropietario.ToString()),
                    new Claim("NombrePropietario", loginResponse.Propietario.Nombre_comercial ?? "") // opcional
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                //Iniciar sesi�n con cookie
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true, // persistente (o false si quieres que expire con la sesi�n)
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(60) // duraci�n
                    });

                //Redirigir a la p�gina principal
                return RedirectToPage("/Index");
            }

            //Si las credenciales no son correctas
            ErrorMessage = "Usuario o contrase�a incorrectos.";
            return Page();
        }

    }
}