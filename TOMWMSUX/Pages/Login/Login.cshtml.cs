using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TOMWMSUX.Pages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; } = "";

        [BindProperty]
        public string Password { get; set; } = "";

        public string ErrorMessage { get; set; } = "";

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (Username == "admin" && Password == "123")
                return RedirectToPage("/Index");

            ErrorMessage = "Usuario o contraseña inválidos.";
            return Page();
        }
    }
}