using WMS.EntityCore.Dtos.Centro_Costo;
using WMSWebAPI.Be;

namespace WMSWebAPI.Services.Centro_Costo
{
    public interface ICentroCostoService
    {
        Task<IEnumerable<clsBeCentro_costo>> GetAllAsync();
        Task<clsBeCentro_costo?> GetByIdAsync(int id);
        Task<clsBeCentro_costo> CreateAsync(clsBeCentro_costo centroCosto);
        Task<bool> UpdateAsync(clsBeCentro_costo centroCosto);
        Task<bool> DeleteAsync(int id);
        Task<int> GetMaxIdAsync();
        Task<IEnumerable<clsBeCentro_costo>> SearchAsync(string? codigo = null, string? nombre = null, bool? activo = null);
        Task<IEnumerable<clsBeCentro_costo>> GetActivosAsync();
        Task<BatchResultMi3> ProcesarBatchMi3Async(List<CentroCostoMi3Dto> centrosCostoDto);
    }
}