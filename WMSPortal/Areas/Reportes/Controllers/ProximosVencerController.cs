using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceReference3;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WMSPortal.Controllers;
using WMSPortal.Library.Database;

namespace WMSPortal.Areas.Reportes.Controllers
{
    [Area("Reportes")]
    [Route("rpt/proximos-a-vencer")]
    public class ProximosVencerController : BaseController
    {

        public ProximosVencerController(
            ApplicationDbContext DbContext,
            IHttpContextAccessor HtContext
        ) : base(DbContext, HtContext)
        {
        }
        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar()
        {
            try
            {
                string query = "SELECT " +
                        "Propietario, " +
                        "Codigo as Codigo, " +
                        "nombre as Nombre, " +
                        "UnidadMedida as UM, " +
                        "Presentacion, " +
                        "Lote, " +
                        "NomEstado as Estado, " +
                        "lic_plate as PalletId, " +
                        "SUM(Cantidad) as CantPres, " +
                        "SUM(CantidadSF) as CantUMBas, " +
                        "Fecha_Ingreso as Fecha_Ingreso, " +
                        "fecha_vence as Fecha_Vence, " +
                        "UbicacionCompleta as Ubic," +
                        "Tolerancia_dias " +
                    "FROM VW_ProximosVencimiento ";

                string bodega = Request.Query["bodega"];
                if (String.IsNullOrEmpty(bodega))
                {
                    ServiceBodegaClient Cli = _WsClientes.GetClienteBodega();

                    clsBodegasUsuarioRes[] bodegas = await Cli
                        .Get_All_By_IdEmpresa_And_IdUsuarioAsync(_session.UserEmpId, _session.UserId);

                    List<int> lBodegas = new();

                    foreach (var bod in bodegas)
                    {
                        lBodegas.Add(bod.Codigo);
                    }

                    string StrBodegas = string.Join(", ", lBodegas);
                    query += $"WHERE IdBodega IN({StrBodegas}) ";
                }
                else
                {
                    query += $"WHERE IdBodega = {bodega} ";
                }

                string DiasTolerancia = Request.Query["dias_tolerancia"];
                if (!String.IsNullOrEmpty(DiasTolerancia))
                {
                    query += $"AND Tolerancia_dias BETWEEN 0 and {DiasTolerancia} ";
                }

                query += "GROUP BY Propietario, Codigo, nombre, UnidadMedida, Presentacion, " +
                    "Lote, NomEstado, lic_plate , fecha_ingreso, Fecha_Vence, UbicacionCompleta, " +
                    "Tolerancia_Dias, propietario;";

                DataSet ds = _dbConexion.EjecutarQuery(query);
                object lista = _dbConexion.DataSetToList(ds);

                return Ok(new { exito = true, lista });
            }
            catch (Exception e)
            {
                return Ok(new { exito = false, mensaje= e.ToString() });
            }
        }
    }
}
