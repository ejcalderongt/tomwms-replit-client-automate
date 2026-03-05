using WMS.EntityCore.Dtos.Ajustes;

namespace WMSWebAPI.Services.Ajustes
{
    public interface IAjustesEnvioService
    {
        Task<AjustesPendientesEnvioResponse> GetAjustesPendientesEnvioAsync(CancellationToken ct = default);
    }   
}