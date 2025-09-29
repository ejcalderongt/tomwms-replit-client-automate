using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Catalogos;
using WMS.EntityCore.Producto.ProductoSimple;

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

        public void ProcesarFamiliaDesdeDto(ProductoFamiliaMi3Dto dto, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                if (dto.Codigo != null)
                {
                    var Familia = _mapper.Map<clsBeProducto_familiaSimple>(dto);
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
