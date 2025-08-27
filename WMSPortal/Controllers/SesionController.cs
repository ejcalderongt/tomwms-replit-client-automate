using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceReference1;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMSPortal.Library.Database;
using WMSPortal.Library.Ws;
using WMSPortal.Models;
using WMSPortal.Models.Database;

namespace WMSPortal.Controllers
{
    public class SesionController : Controller
    {
        private IsrvUsuarioClient Client;
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _config;


        public SesionController(
            ApplicationDbContext DbContext,
            IConfiguration Config
        )
        {
            _dbContext = DbContext;
            _config = Config;

            SetClient();
        }

        private void SetClient()
        {
            Client = new Clientes(_config).GetClienteUsuario();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            List<Empresa> lEmp = _dbContext.Empresas.ToList();

            ViewBag.Empresas = lEmp;
            ViewBag.HayEmpresas = lEmp.Count > 0;

            var Model = TempData["ModelUsuario"];

            if (Model == null)
            {
                return View(new ClsUsuario());
            }
            else
            {
                var M = JsonConvert.DeserializeObject<ClsUsuario>(TempData["ModelUsuario"].ToString());
                TempData["ModelUsuario"] = null;

                return View(M);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(ClsUsuario model)
        {
            model.errores = false;
            TempData["ModelUsuario"] = null;
            if (
                string.IsNullOrEmpty(model.idEmpresa.ToString()) || model.idEmpresa.Equals(0) ||
                string.IsNullOrEmpty(model.usuario) ||
                string.IsNullOrEmpty(model.contrasena)
            )
            {
                model.errores = true;
                model.mensaje = "Ingresar los datos solictiados";
            }
            else
            {
                bool res = await UsuarioValido(model);

                if (res)
                {
                    Usuario_Valido_By_ObjResponse Datos = await GetUserdata(model);

                    HttpContext.Session.SetInt32("userId", Datos.Usuario.IdUsuario);
                    HttpContext.Session.SetString("userName", Datos.Usuario.Nombres ?? "");
                    HttpContext.Session.SetString("userLastName", Datos.Usuario.Apellidos ?? "");
                    HttpContext.Session.SetString("userDirection", Datos.Usuario.Direccion ?? "");
                    HttpContext.Session.SetString("userEmail", Datos.Usuario.Email ?? "");
                    HttpContext.Session.SetInt32("userEmpId", Datos.Usuario.IdEmpresa);
                    HttpContext.Session.SetInt32("userRoleId", Datos.Usuario.IdRol);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    model.errores = true;
                    model.mensaje = "Las credenciales no son válidas.";
                    model.usuario = null;

                    TempData["ModelUsuario"] = JsonConvert.SerializeObject(model);
                    return Redirect("login");
                }

            }

            return View(model);
        }

        [HttpGet("logout")]
        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Sesion");
        }

        private async Task<bool> UsuarioValido(ClsUsuario model)
        {
            return await Client.Usuario_Valido_By_ParamsAsync(
                model.usuario,
                model.contrasena,
                model.idEmpresa
            );
        }

        private async Task<Usuario_Valido_By_ObjResponse> GetUserdata(ClsUsuario model)
        {
            var usr = new clsBeUsuario
            {
                Clave = model.contrasena,
                Codigo = model.usuario,
                IdEmpresa = model.idEmpresa
            };

            Usuario_Valido_By_ObjRequest req = new();
            req.Usuario = usr;

            return await Client.Usuario_Valido_By_ObjAsync(req);
        }
    }
}
