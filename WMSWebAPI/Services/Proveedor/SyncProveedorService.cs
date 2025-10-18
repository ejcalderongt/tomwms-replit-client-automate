using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Producto.ProductoSimple;
using WMS.EntityCore.Proveedor;
using WMSWebAPI.Dtos.Catalogos;

namespace WMSWebAPI.Services.Proveedor
{
    public class SyncProveedorService : ISyncProveedorService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public SyncProveedorService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public void ProcesarProveedorDto(ProveedorDto dto, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                if (dto.Codigo != null)
                    throw new ArgumentNullException(nameof(dto), "El proveedor no puede estar vacio.");

                var Proveedor = _mapper.Map<clsBeProveedor>(dto);
                clsLnProveedor.Valida_Atributos(_configuration, Proveedor, conn, tx);

            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Proveedor → " + ex.Message, ex);
            }
        }

        public void ProcesarProveedorListDto(List<ProveedorDto> listaDto, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                if (listaDto == null || listaDto.Count == 0)
                    throw new ArgumentNullException(nameof(listaDto), "La lista de proveedores no puede ser nula o vacía.");

                var proveedorList = _mapper.Map<List<clsBeProveedor>>(listaDto);
                if (proveedorList != null)
                    clsLnProveedor.InsertarOActualizar(_configuration, proveedorList, conn, tx);
                    
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Proveedores → " + ex.Message, ex);
            }
        }
        // Método en la clase de servicio (SyncProveedorService)
        public List<clsBeProveedor> Get_All()
        {
            try
            {
                return clsLnProveedor.GetAll(_configuration);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener proveedores → " + ex.Message, ex);
            }
        }

    }
}
