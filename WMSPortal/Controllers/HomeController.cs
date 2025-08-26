using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WMSPortal.Library.Database;
using WMSPortal.Models;

namespace WMSPortal.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(
            ApplicationDbContext dbContext,
            IHttpContextAccessor htContext
        ) : base(dbContext, htContext)
        {
        }

        [HttpGet("/")]
        [HttpGet("/principal")]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
