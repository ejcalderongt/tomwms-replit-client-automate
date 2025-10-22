using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Productos;
using WMS.EntityCore.Producto;

namespace WMSWebAPI.Services.Producto.Tipo
{
    public class ProductoTipoMi3SyncService : IProductoTipoMi3SyncService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProductoTipoMi3SyncService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public void ProcesarTipoProductoMi3Dto(Producto_tipoMi3Dto dto, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                if (string.IsNullOrEmpty(dto.Codigo))
                    throw new ArgumentNullException(nameof(dto), "El TipoProductoMi3 no puede estar vacio.");

                var TipoProductoMi3 = _mapper.Map<clsBeProducto_tipoMi3>(dto);
                clsLnProducto_tipo.Valida_Atributos(_configuration, TipoProductoMi3, conn, tx);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar TipoProductoMi3 → " + ex.Message, ex);
            }
        }
        public List<clsBeProducto_tipo> Get_All()
        {
            try
            {                             
                return clsLnProducto_tipo.GetAll(_configuration);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar TipoProductoMi3 → " + ex.Message, ex);
            }
        }
    }
}
