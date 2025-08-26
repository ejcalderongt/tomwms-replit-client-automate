using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMSPortal.Library.Database;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using WMSPortal.Models.Database;
using System.Data;
using WMSPortal.Controllers;

namespace WMSPortal.Areas.Mante.Controllers
{
    [Area("Mante")]
    [Route("mnt/[Controller]")]
    public class MenuController : BaseController
    {
        public MenuController(
            ApplicationDbContext dbContext,
            IHttpContextAccessor htContext
        ) : base(dbContext, htContext)
        {
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("buscar")]
        [HttpGet("/menu/buscar")]
        public IActionResult Buscar()
        {
            List<PortalMenu> Lista = _dbContext.PortalMenus.FromSqlRaw("" +
                "SELECT " +
                    "IdPortalMenu, " +
                    "trim(nombre) as nombre, " +
                    "trim(enlace) as enlace, " +
                    "trim(icono) as icono, " +
                    "padre, " +
                    "activo " +
                "FROM portal_menu " +
                "WHERE activo=1 "
            ).ToList();

            return Ok(new { lista = Lista });
        }

        [HttpPost("guardar/{IdPortalMenu?}")]
        public IActionResult Guardar(PortalMenu Model, int? IdPortalMenu)
        {
            try
            {
                string Mensaje = "";
                int? Pk = null;

                if (!IdPortalMenu.HasValue)
                {
                    _dbContext.Add(Model);
                    Mensaje = "Opcion agregada correctamente.";
                    Pk = Model.IdPortalMenu;
                }
                else
                {
                    Model.IdPortalMenu = (int)IdPortalMenu;

                    _dbContext.Entry(Model).State = EntityState.Modified;
                    Mensaje = "Opcion actualizada correctamente.";
                    Pk = IdPortalMenu;
                }
                _dbContext.SaveChanges();
                return Ok(new { exito = 1, mensaje = Mensaje, pk = Pk });
            }
            catch (Exception e)
            {
                return Ok(new { exito = 0, mensaje = "Error: " + e.ToString(), });
            }
        }

        [HttpGet("usuario")]
        public IActionResult Usuario()
        {
            try
            {

                string query = "SELECT distinct(IdPortalRol) as IdPortalrol " +
                    "FROM usuario_bodega " +
                    "WHERE Activo = 1 " +
                    "AND IdPortalRol IS NOT NULL " +
                    "AND IdUsuario = " + _session.UserId;

                DataSet ds = _dbConexion.EjecutarQuery(query);
                List<int> roles = new();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    foreach (object item in row.ItemArray)
                    {
                        roles.Add(int.Parse(item.ToString()));
                    }
                }

                var Lista = _dbContext
                            .PortalMenuRols
                            .AsNoTracking()
                            .Include(r => r.IdPortalMenuNavigation)
                            .Where(r => roles.Contains(r.IdPortalRol) && r.IdPortalMenuNavigation.Activo == true && r.Activo == true)
                            .ToList();

                return Ok(new { exito = true, lista = Lista });
            }
            catch (Exception e)
            {
                return Ok(new { exito = false, mensaje = e.ToString() });
            }
        }
    }
}
