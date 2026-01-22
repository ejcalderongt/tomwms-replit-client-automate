using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Cliente;
using WMS.EntityCore.Dtos.Clientes;

namespace WMSWebAPI.Services.Cliente
{
    public class ClienteMi3SyncService : IClienteMi3SyncService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ClienteMi3SyncService(IConfiguration configuration, IMapper mapper) { 
            _configuration = configuration;
            _mapper = mapper;
        }

        public void ProcesarClienteDesdeDto(ClienteMi3Dto dto, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                if (dto.codigo != null)
                {
                    var ClienteMi3 = _mapper.Map<clsBeClientesMi3>(dto);
                    clsLnCliente.Valida_Atributos(ClienteMi3, conn, tx);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Cliente → " + ex.Message, ex);
            }
        }
        public List<clsBeCliente> Get_All()
        {
            try
            {
                
                return clsLnCliente.GetAll(_configuration);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Cliente → " + ex.Message, ex);
            }
        }
    }
}
