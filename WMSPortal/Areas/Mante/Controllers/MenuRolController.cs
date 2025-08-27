using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMSPortal.Controllers;
using WMSPortal.Library.Database;
using WMSPortal.Models.Database;

namespace WMSPortal.Areas.Mante.Controllers
{
    [Area("Mante")]
    [Route("mnt/menu-rol")]
    public class MenuRolController : BaseController
    {
        public MenuRolController(
            ApplicationDbContext DbContext,
            IHttpContextAccessor HtContext
        ) : base(DbContext, HtContext)
        {
        }

        [HttpGet("buscar")]
        public IActionResult Buscar()
        {
            try
            {
                List<PortalMenuRol> Lista = new();
                string sRol = Request.Query["rol_id"];

                if (!string.IsNullOrEmpty(sRol))
                {
                    int rol = int.Parse(sRol);

                    Lista = _dbContext
                        .PortalMenuRols
                        .AsNoTracking()
                        .Include(r => r.IdPortalMenuNavigation)
                        .Where(r => r.IdPortalMenuNavigation.Activo == true && r.IdPortalRol == rol && r.Activo == true)
                        .ToList();
                }

                return Ok(new { exito = true, Lista });
            }
            catch (Exception e)
            {
                return Ok(new { exito = false, mensaje = e.ToString() });
            }
        }

        [HttpPost("guardar/{Opcion?}/{Rol?}")]
        [HttpPut("guardar/{Opcion?}/{Rol?}")]
        public IActionResult Guardar(int? Opcion, int? Rol)
        {
            try
            {
                bool Exito = false;
                string Mensaje = "";
                int? Pk = null;
                PortalMenuRol Reg = new();

                if (Opcion.HasValue && Rol.HasValue)
                {
                    Reg.IdPortalMenu = (int)Opcion;
                    Reg.IdPortalRol = (int)Rol;
                    Reg.Activo = true;

                    _dbContext.PortalMenuRols.Add(Reg);
                    Mensaje = "Permiso agregado correctamente";

                    _dbContext.SaveChanges();

                    Reg = _dbContext
                        .PortalMenuRols
                        .AsNoTracking()
                        .Include(r => r.IdPortalMenuNavigation)
                        .SingleOrDefault(r => r.IdPortalMenuRol == Reg.IdPortalMenuRol && r.Activo == true);

                    Pk = Reg.IdPortalMenu;
                    Exito = true;
                }
                else
                {
                    Mensaje = "Debe indicar el rol y la opcion a asignar";
                }

                if (Exito)
                {
                    return Ok(new { Exito, Mensaje, Pk, Linea = Reg });
                }
                else
                {
                    return Ok(new { Exito, Mensaje });
                }
            }
            catch (Exception e)
            {
                return Ok(new { Exito = false, Mensaje = $"Error: {e}" });
            }
        }

        [HttpPost("eliminar/{Opcion?}/{Rol?}")]
        [HttpDelete("eliminar/{Opcion?}/{Rol?}")]
        public IActionResult Eliminar(int? Opcion, int? Rol)
        {
            try
            {
                string Mensaje = "";
                bool Exito = false;

                if (Opcion.HasValue && Rol.HasValue)
                {
                    string query = "UPDATE portal_menu_rol SET activo = 0 " +
                        $"WHERE IdPortalMenu = {Opcion} " +
                        $"AND IdPortalRol = {Rol} " +
                        $"AND activo = 1";

                    _dbConexion.EjecutarQuery(query);
                    Exito = true;
                    Mensaje = "Permiso eliminado correctamente.";
                }
                else
                {
                    Mensaje = "Debe indicar el rol y la opcion a eliminar";
                }

                return Ok(new { Exito, Mensaje });
            }
            catch (Exception e)
            {
                return Ok(new { exito = false, mensaje = e.ToString() });
            }
        }

        [HttpGet("usuario")]
        public IActionResult Usuario()
        {
            try
            {
                List<PortalMenuRol> Lista = new();

                Lista = _dbContext
                    .PortalMenuRols
                    .AsNoTracking()
                    .Include(r => r.IdPortalMenuNavigation)
                    .Where(r => r.IdPortalMenuNavigation.Activo == true && r.IdPortalRol == _session.UserRoleId && r.Activo == true)
                    .ToList();

                return Ok(new { exito = true, Lista });
            }
            catch (Exception e)
            {
                return Ok(new { exito = false, mensaje = e.ToString() });
            }
        }
    }
}
