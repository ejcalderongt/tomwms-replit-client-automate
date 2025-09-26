using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Catalogos;
using WMS.EntityCore.Producto.ProductoSimple;


namespace WMSWebAPI.Services.Producto.Marca
{
    public class ProductoMarcaSyncService : IProductoMarcaSyncService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProductoMarcaSyncService(IConfiguration configuration, IMapper mapper) { 
            _configuration = configuration;
            _mapper = mapper;
        }

      
        public void ProcesarMarcaDesdeDto(ProductoMarcaSimpleDto dto, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                var Marca = _mapper.Map<clsBeProducto_marcaSimple>(dto);
                if (Marca != null)
                    clsLnProductoMarca.Valida_Atributos(_configuration, Marca, conn, tx);
                    
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Marca → " + ex.Message, ex);
            }
        }
    }
}
