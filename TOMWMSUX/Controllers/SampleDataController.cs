using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TOMWMSUX.Models;

namespace TOMWMSUX.Controllers
{

    [Authorize]
    [Route("[controller]")]
    public class SampleDataController : Controller
    {

        [HttpGet("Get")]
        public object Get(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(SampleData.Orders, loadOptions);
        }

    }
}