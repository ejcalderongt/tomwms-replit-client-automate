using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WMSPortal.Library.Database;
using ServiceReference2;
using Microsoft.AspNetCore.Http;
using System.ServiceModel;
using Microsoft.Extensions.Configuration;
using WMSPortal.Areas.Reportes.Models;
using WMSPortal.Controllers;

namespace WMSPortal.Areas.Reportes.Controllers
{
    [Area("Reportes")]
    [Route("rpt/movimientos")]
    public class MovimientosController : BaseController
    {
        public MovimientosController(
            ApplicationDbContext dbContext,
            IHttpContextAccessor htContext
        ) : base(dbContext, htContext)
        {
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar(string fdel, string fal)
        {
            try
            {
                ClsMovimientos model = new();
                model.pFechaDel = DateTime.Parse(fdel + " 00:00:00");
                model.pFechaAl = DateTime.Parse(fal + " 23:59:59");

                clsBeVW_Movimientos[] Lista = await GetDatos(model);

                var res = new { exito = true, lista = Lista };
                return Ok(res);
            }
            catch (Exception e)
            {
                return Ok(new
                {
                    exito = false,
                    mensaje = e.ToString()
                });
            }

        }

        private async Task<clsBeVW_Movimientos[]> GetDatos(ClsMovimientos model)
        {
            IsrvReportesClient Client = _WsClientes.GetClienteReportes();
            var Res = await Client.Get_Movimientos_Kardex_By_IdEmpresaAsync(
                _session.UserEmpId,
                model.pFechaDel,
                model.pFechaAl
            );

            if (Res == null)
            {
                return Array.Empty<clsBeVW_Movimientos>();
            }

            return Res;
        }
    }
}
