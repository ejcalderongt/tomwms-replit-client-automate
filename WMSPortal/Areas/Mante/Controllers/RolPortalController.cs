using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMSPortal.Areas.Mante.Models.Request;
using WMSPortal.Library.Database;
using WMSPortal.Models.Database;
using Microsoft.EntityFrameworkCore;
using WMSPortal.Controllers;

namespace WMSPortal.Areas.Mante.Controllers
{
    [Area("Mante")]
    [Route("mnt/rol-portal")]
    public class RolPortalController : BaseController
    {
        public RolPortalController(
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
        public IActionResult Buscar()
        {
            try
            {
                List<PortalRol> Lista = _dbContext.PortalRols.ToList();
                return Ok(new { exito = true, Lista });
            } 
            catch (Exception e)
            {
                return Ok(new { exito = false, mensaje = e.ToString() });
            }
        }

        [HttpPost("guardar/{id?}")]
        [HttpPut("guardar/{id}")]
        public IActionResult Guardar(int? Id, [FromForm] ReqRolPortal Datos)
        {
            try
            {
                int Exito = 0;
                string Mensaje = "";
                int? Pk = null;
                PortalRol Reg = new ();

                if (Id.HasValue)
                {
                    Reg = _dbContext.PortalRols.SingleOrDefault(r => r.IdPortalRol == Id);
                }

                if (ModelState.IsValid)
                {
                    Reg.Nombre = Datos.Nombre;
                    Reg.Descripcion = Datos.Descripcion;
                    if (Id.HasValue)
                    {
                        _dbContext.Entry(Reg).State = EntityState.Modified;
                        Mensaje = "Registro actualizado correctamente";
                    }
                    else
                    { 
                        _dbContext.PortalRols.Add(Reg);
                        Mensaje = "Registro agregado correctamente";
                    }

                    _dbContext.SaveChanges();

                    Pk = Reg.IdPortalRol;
                    Exito = 1;
                }
                else
                {
                    var Errors = new List<string>();
                    foreach (var state in ModelState)
                    {
                        foreach (var Err in state.Value.Errors)
                        {
                            Errors.Add(Err.ErrorMessage);
                        }
                    }

                    Mensaje = string.Join("<br>", Errors);
                }

                if (Exito == 1)
                {
                    return Ok(new { Exito, Mensaje, Pk, Linea = Reg});
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
    }
}
