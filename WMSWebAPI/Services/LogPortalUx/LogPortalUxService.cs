using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Log;
using WMSWebAPI.Dtos.Log_portal_ux;

namespace WMSWebAPI.Services.LogPortalUx
{
    public class LogPortalUxService : ILogPortalUxService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public LogPortalUxService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public void ProcesarLogPortalUx(LogPortalUxDto logportaluxDTO, SqlConnection conn, SqlTransaction tx)
        {
            
            var logportal_ = _mapper.Map<clsBeLog_portal_ux>(logportaluxDTO);
            clsLnLog_portal_ux.InsertOrUpdate(_configuration, logportal_, conn, tx);
        }
    }
}
