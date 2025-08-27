using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System.IO;
using WMSPortal.Library.Database;
using WMSPortal.Library.Ws;
using WMSPortal.Models;

namespace WMSPortal.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext _dbContext;
        protected IHttpContextAccessor _htContext;
        protected ClsUserSesion _session;
        protected Database _dbConexion;
        protected Clientes _WsClientes;
        protected IConfiguration _Config;
        public BaseController(
            ApplicationDbContext dbContext,
            IHttpContextAccessor htContext
        )
        {
            SetConfiguration();
            _dbContext = dbContext;
            _htContext = htContext;
            _dbConexion = new(_Config);
            _WsClientes = new(_Config);
            _session = new ClsUserSesion(_htContext);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            int UserId = context.HttpContext.Session.GetInt32("userId") ?? 0;
            if (string.IsNullOrEmpty(UserId.ToString()) || UserId == 0)
            {
                RouteValueDictionary Route = new(new
                {
                    Controller = "login",
                    Action = ""
                });

                context.Result = new RedirectToRouteResult(Route);
                return;
            }
        }

        public void SetConfiguration()
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _Config = builder.Build();
        }
    }
}
