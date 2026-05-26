using WMS.EntityCore.Dtos.Inventario;

namespace WMSWebAPI.Services.Inventario
{
    public interface IInventarioService
    {
        Task<InventarioPagedResultDto<InventarioExistenciaLoteDto>> GetExistenciasPorLoteAsync(
            InventarioFiltroDto filtro,
            CancellationToken ct);

        Task<IReadOnlyList<InventarioResumenDto>> GetResumenAsync(
            InventarioFiltroDto filtro,
            string grupo,
            CancellationToken ct);
    }
}
