using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TOMWMSUX.Dtos;
using TOMWMSUX.Models;

namespace TOMWMSUX.Pages
{
    public class DocumentosIngresoModel : PageModel
    {
        private readonly IApiClientService _apiClient;
        
        public DocumentosIngresoModel(IApiClientService apiClient)
        {
            _apiClient = apiClient;
        }

        public DateTime FechaInicio { get; set; } = DateTime.Today;
        public DateTime FechaFin { get; set; } = DateTime.Today;
        public List<DocumentoIngresoDto> Documentos { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            int idPropietario = ObtenerIdPropietario();

            Documentos = await _apiClient.ObtenerDocumentosDeIngresoAsync(
                FechaInicio,
                FechaFin,
                idPropietario);

            return Page();
        }

        private int ObtenerIdPropietario()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "IdPropietario");
            return claim != null && int.TryParse(claim.Value, out var id) ? id : 0;
        }
    }

}