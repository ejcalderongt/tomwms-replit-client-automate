using Microsoft.Data.SqlClient;
using WMS.EntityCore.Cliente;
using WMS.EntityCore.Dtos.Catalogos;
using WMS.EntityCore.Dtos.Clientes;

namespace WMSWebAPI.Services.Cliente
{
    public interface IClienteMi3SyncService
    {

        void ProcesarClienteDesdeDto(ClienteMi3Dto dto, SqlConnection connection, SqlTransaction transaction);
        List<clsBeCliente> Get_All();
    }
}