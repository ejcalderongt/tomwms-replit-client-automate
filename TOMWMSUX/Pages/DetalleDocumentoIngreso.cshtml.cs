using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TOMWMSUX.Pages
{
    public class DetalleDocumentoIngresoModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int IdOrdenCompraEnc{ get; set; } = 0;

        public void OnGet(int IdOrdenCompraEnc)
        {
            this.IdOrdenCompraEnc = IdOrdenCompraEnc;
        }
    }
}
