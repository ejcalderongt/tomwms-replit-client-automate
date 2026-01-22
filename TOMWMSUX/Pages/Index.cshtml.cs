using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace TOMWMSUX.Pages {
    
    public class IndexModel : PageModel {
        public void OnGet() {
            Debug.WriteLine("¿Está autenticado?: " + User.Identity.IsAuthenticated);
            Debug.WriteLine("Usuario: " + User.Identity.Name);
        }
    }
}
