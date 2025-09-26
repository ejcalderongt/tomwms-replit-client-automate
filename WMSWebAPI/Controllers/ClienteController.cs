using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMS.EntityCore.Dtos.Catalogos;
using WMSWebAPI.Services.Cliente;

namespace WMSWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClienteSyncService _syncService;

        public ClienteController(IMapper mapper, IClienteSyncService syncService) { 
            _mapper = mapper;
            _syncService = syncService;
        }

        //[HttpPost("list/mi3/insert")]
        

    }
}
