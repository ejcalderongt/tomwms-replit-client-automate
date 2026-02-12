using WMS.EntityCore.Cambio_Ubicacion;
using WMS.EntityCore.Dtos.Cambio_Estado;

namespace WMSWebAPI.Services.Cambio_Estado
{
    public interface ICambioEstadoService
    {
        /// <summary>
        /// Obtiene todas las transacciones de cambio de estado activas y no sincronizadas (sync_mi3 = 0) en formato DTO simplificado
        /// </summary>
        Task<List<CambioEstadoEncabezadoDto>> GetAllPendientesSincronizacionSimplificadoAsync();

        /// <summary>
        /// Obtiene una transacción específica por su Id
        /// </summary>
        Task<clsBeTrans_ubic_hh_enc?> GetByIdAsync(int idTareaUbicacionEnc);

        /// <summary>
        /// Marca una transacción como sincronizada (sync_mi3 = 1)
        /// </summary>
        Task MarcarComoSincronizadoAsync(int idTareaUbicacionEnc);

        /// <summary>
        /// Marca múltiples transacciones como sincronizadas
        /// </summary>
        Task MarcarMultiplesComoSincronizadosAsync(List<int> idsTareaUbicacionEnc);
    }
}