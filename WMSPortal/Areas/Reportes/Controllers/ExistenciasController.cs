using Microsoft.AspNetCore.Mvc;
using WMSPortal.Library.Database;
using Microsoft.AspNetCore.Http;
using System;
using ServiceReference2;
using System.Threading.Tasks;
using System.ServiceModel;
using Microsoft.Extensions.Configuration;
using WMSPortal.Controllers;

namespace WMSPortal.Areas.Reportes.Controllers
{
    [Area("Reportes")]
    [Route("rpt/existencias")]
    public class ExistenciasController : BaseController
    {
        private readonly IsrvReportesClient Client = new();
        public ExistenciasController(
            ApplicationDbContext DbContext,
            IHttpContextAccessor HtContext
        ) : base(DbContext, HtContext)
        {
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar()
        {
            try
            {
                clsBeVW_stock_res[] Lista = await GetDatos();
                return Ok(new { exito = true, lista = Lista });
            }
            catch (Exception e)
            {
                return Ok(new { exito = false, mensaje = e.ToString() });
            }
        }

        private async Task<clsBeVW_stock_res[]> GetDatos()
        {
            clsBeVW_stock_res[] res = null;
            IsrvReportesClient Client = _WsClientes.GetClienteReportes();

            try
            {
                res = await Client.Get_Stock_By_IdEmpresaAsync(
                    _session.UserEmpId
                );

                if (res == null)
                {
                    return Array.Empty<clsBeVW_stock_res>();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return res;
        }
    }
}
