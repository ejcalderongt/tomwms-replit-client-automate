using TOMWMSUX.Dtos;
using TOMWMSUX.Dtos.Auth;

namespace TOMWMSUX.Models
{
    public class IApiClientService
    {
        private readonly HttpClient _httpClient;
        
        public IApiClientService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("APIClient");
        }

        public async Task<LoginResponseDto> LoginAsync(string username, string password)
        {
            var loginRequest = new LoginRequestDto
            {
                username = username,
                password = password
            };

            var response = await _httpClient.PostAsJsonAsync("/api/auth/login-propietario", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                if (result != null)
                {
                    // Devuelve tanto el token como el propietario
                    return new LoginResponseDto
                    {
                        Token = result.Token,
                        Propietario = result.Propietario
                    };
                }
            }

            // Si falla la llamada, devuelve null
            return null;
        }

        public async Task<List<DocumentoIngresoDto>> ObtenerDocumentosDeIngresoAsync(DateTime fechaInicio, DateTime fechaFin, int idPropietario)
        {
            var filtro = new DocumentoIngresoFiltroDto
            {
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                IdPropietario = idPropietario,
                IdBodega = 0 // o lo que corresponda
            };

            var response = await _httpClient.PostAsJsonAsync("api/sync/ingresos/documentos-ingreso/listar", filtro);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<DocumentoIngresoDto>>() ?? new();
            }

            throw new Exception($"Error al obtener documentos: {response.ReasonPhrase}");
        }

    }
}