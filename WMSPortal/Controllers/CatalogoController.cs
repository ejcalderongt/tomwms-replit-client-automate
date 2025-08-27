using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceReference3;
using System.Threading.Tasks;
using WMSPortal.Library.Database;

namespace WMSPortal.Controllers
{
    [Route("catalogo")]
    public class CatalogoController : BaseController
    {
        public CatalogoController(
            ApplicationDbContext dbContext,
            IHttpContextAccessor htContext
        ) : base(dbContext, htContext)
        {
        }

        [HttpGet("buscar/{nombre}")]
        public async Task<IActionResult> Buscar(string nombre)
        {
            switch (nombre)
            {
                case "bodegas":
                    ServiceBodegaClient Cli = _WsClientes.GetClienteBodega();

                    clsBodegasUsuarioRes[] lista = await Cli
                        .Get_All_By_IdEmpresa_And_IdUsuarioAsync(_session.UserEmpId, _session.UserId);

                    return Ok(lista);

                default:
                    return Ok();
            }
        }
    }
}
