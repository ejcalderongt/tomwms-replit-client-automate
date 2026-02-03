using WMS.EntityCore.Dtos.KPI;

namespace WMSWebAPI.Services.KPI
{
    public interface IKpiReportService
    {
        Task<List<KpiPickingRowDto>> GetPickingAsync(DateTime? from, DateTime? to, CancellationToken ct);
        Task<List<KpiVerificacionRowDto>> GetVerificacionAsync(DateTime? from, DateTime? to, CancellationToken ct);
        Task<List<KpiRecepcionRowDto>> GetRecepcionAsync(DateTime? from, DateTime? to, CancellationToken ct);
        Task<List<KpiDespachoRowDto>> GetDespachoAsync(DateTime? from, DateTime? to, CancellationToken ct);
        Task<List<KpiTendenciaDespachoRowDto>> GetTendenciaDespachoProductoFamiliaAsync(DateTime? from,DateTime? to,string? gran,int? top,CancellationToken ct);
        Task<List<KpiHeatmapDiaHoraDto>> GetHeatmapDespachoDiaHoraAsync(DateTime? from,DateTime? to,CancellationToken ct);
        Task<List<KpiStockResRowDto>> GetStockResAsync(int? idBodega,string? codigo,string? familia,string? estado,string? licencia,string? lote,DateTime? venceFrom,DateTime? venceTo,int page,int pageSize,CancellationToken ct);
    }
}