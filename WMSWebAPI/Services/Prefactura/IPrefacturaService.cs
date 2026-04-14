using WMS.EntityCore.Dtos.Prefactura;

namespace WMSWebAPI.Services.Prefactura
{
    public interface IPrefacturaService
    {
        /// <summary>
        /// Obtiene las prefacturas pendientes de enviar a ERP
        /// </summary>
        /// <param name="fechaDesde">Fecha inicial del período (opcional)</param>
        /// <param name="fechaHasta">Fecha final del período (opcional)</param>
        /// <param name="idCliente">ID del cliente (opcional)</param>
        /// <param name="pageNumber">Número de página</param>
        /// <param name="pageSize">Tamaño de página</param>
        /// <returns>Respuesta paginada con las prefacturas pendientes</returns>
        Task<PrefacturaPendienteResponseDto> GetPrefacturasPendientesAsync(
            DateTime? fechaDesde,
            DateTime? fechaHasta,
            int? idCliente,
            int pageNumber = 1,
            int pageSize = 50);

        /// <summary>
        /// Obtiene una prefactura específica por ID
        /// </summary>
        /// <param name="idPrefacturaEnc">ID de la prefactura</param>
        /// <returns>DTO de la prefactura</returns>
        Task<PrefacturaPendienteDto?> GetPrefacturaByIdAsync(int idPrefacturaEnc);

        /// <summary>
        /// Marca una prefactura como procesada por el ERP
        /// </summary>
        /// <param name="idPrefacturaEnc">ID de la prefactura</param>
        /// <param name="usuarioModificacion">Usuario que realiza la modificación</param>
        /// <returns>Respuesta con el resultado de la operación</returns>
        Task<MarcarProcesadoResponseDto> MarcarComoProcesadaAsync(int idPrefacturaEnc, string usuarioModificacion = "odoo");

        /// <summary>
        /// Verifica si una prefactura existe
        /// </summary>
        /// <param name="idPrefacturaEnc">ID de la prefactura</param>
        /// <returns>True si existe, False si no</returns>
        Task<bool> ExistePrefacturaAsync(int idPrefacturaEnc);

        /// <summary>
        /// Obtiene el total de prefacturas pendientes
        /// </summary>
        /// <param name="fechaDesde">Fecha inicial del período (opcional)</param>
        /// <param name="fechaHasta">Fecha final del período (opcional)</param>
        /// <param name="idCliente">ID del cliente (opcional)</param>
        /// <returns>Total de registros</returns>
        Task<int> GetTotalPrefacturasPendientesAsync(
            DateTime? fechaDesde,
            DateTime? fechaHasta,
            int? idCliente);
    }
}