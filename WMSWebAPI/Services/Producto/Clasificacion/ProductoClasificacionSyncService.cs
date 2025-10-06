using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Catalogos;
using WMS.EntityCore.Producto;
using WMS.EntityCore.Producto.ProductoSimple;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Productos;

namespace WMSWebAPI.Services.Producto.Clasificacion
{
    public class ProductoClasificacionSyncService : IProductoClasificacionSyncService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProductoClasificacionSyncService(IConfiguration configuration, IMapper mapper) {
            _configuration = configuration;
            _mapper = mapper;
        }

        public void ProcesarClasificacionDesdeDto(ProductoClasificacionMi3Dto dto, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                if (dto.Codigo != null)
                {
                    var Clasificacion = _mapper.Map<clsBeProducto_clasificacionSimple>(dto);
                    clsLnProducto_clasificacion.Valida_Atributos(_configuration, Clasificacion, conn, tx);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Clasificación " + ex.Message, ex);
            }
        }
    }
}
