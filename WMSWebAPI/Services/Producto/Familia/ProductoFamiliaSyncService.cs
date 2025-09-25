using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Producto;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Productos;
using WMSWebAPI.Services.Producto.Clasificacion;

namespace WMSWebAPI.Services.Producto.Familia
{
    public class ProductoFamiliaSyncService : IProductoFamiliaSyncService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProductoFamiliaSyncService(IConfiguration configuration, IMapper mapper) { 
            _configuration = configuration;
            _mapper = mapper;
        }

   
        public void ProcesarFamiliaDesdeDto(ProductoFamiliaDto dto, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                if (dto.Codigo != null)
                {
                    var Familia = _mapper.Map<clsBeProducto_familia>(dto);
                    clsLnProducto_familia.Valida_Atributos(_configuration, Familia, conn, tx);

                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Familia → " + ex.Message, ex);
            }
        }
    }
}
