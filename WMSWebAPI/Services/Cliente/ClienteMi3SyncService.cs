using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Cliente;
using WMS.EntityCore.Dtos.Catalogos;
using WMS.EntityCore.Dtos.Clientes;
using WMS.EntityCore.Producto.ProductoSimple;

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
                    clsLnCliente.Valida_Atributos(_configuration, ClienteMi3, conn, tx);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Clasificación → " + ex.Message, ex);
            }
        }
    }
}
