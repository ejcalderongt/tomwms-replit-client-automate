using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ServiceReference2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using WMSPortal.Controllers;
using WMSPortal.Library.Database;

namespace WMSPortal.Areas.Reportes.Controllers
{
    [Area("Reportes")]
    [Route("rpt/ocupacion-bodega")]
    public class OcupacionBodegaController : BaseController
    {

        public OcupacionBodegaController(
            ApplicationDbContext dbContext,
            IHttpContextAccessor htContext
        ) : base(dbContext, htContext)
        {
            
        }

        [HttpGet("index")]
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
                IsrvReportesClient Client = _WsClientes.GetClienteReportes();
                string bodega = Request.Query["bodega"];
                
                if (!String.IsNullOrEmpty(bodega))
                {
                    int idBodega = int.Parse(bodega);
                    var req = new Get_Indicadores_Ocupacion_By_IdBodegaRequest
                    {
                        pIdBodega = idBodega,
                        UbicacionesVacias = 0,
                        UbicacionesOcupadas = 0
                    };

                    var datos = await Client.Get_Indicadores_Ocupacion_By_IdBodegaAsync(req);

                    return Ok(new
                    {
                        exito = true,
                        lista = datos
                    });
                } else
                {
                    return Ok(new { exito = false, mensaje = "Debe seleccionar una bodega." });
                }
            }
            catch (Exception e)
            {
                return Ok(new {
                    exito = false, mensaje = e.ToString()
                });
            }
        }
    }
}
