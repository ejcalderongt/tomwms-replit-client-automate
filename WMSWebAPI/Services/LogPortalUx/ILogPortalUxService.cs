using Microsoft.Data.SqlClient;
using WMSWebAPI.Dtos.Log_portal_ux ;

namespace WMSWebAPI.Services.LogPortalUx
{
    public interface ILogPortalUxService
    {
        void ProcesarLogPortalUx(LogPortalUxDto logportaluxDTO, SqlConnection conn, SqlTransaction tx);

    }
}
