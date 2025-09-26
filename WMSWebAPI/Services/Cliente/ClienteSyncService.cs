using AutoMapper;

namespace WMSWebAPI.Services.Cliente
{
    public class ClienteSyncService : IClienteSyncService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ClienteSyncService(IConfiguration configuration, IMapper mapper) { 
            _configuration = configuration;
            _mapper = mapper;
        }

    }
}
