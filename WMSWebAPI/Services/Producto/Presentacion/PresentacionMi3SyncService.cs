using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Catalogos;
using WMS.EntityCore.Dtos.Productos;
using WMS.EntityCore.Producto;
using WMS.EntityCore.Producto.ProductoSimple;

namespace WMSWebAPI.Services.Producto.Presentacion
{
    public class PresentacionMi3SyncService : IPresentacionMi3SyncService
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PresentacionMi3SyncService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public void ProcesarPresentacionMi3Dto(ProductoPresentacionMi3Dto dto, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                if (dto.Codigo_presentacion != null)
                {
                    var PresentacionMi3 = _mapper.Map<clsBeProducto_presentacionMi3>(dto);
                    clsLnProducto_presentacion.Valida_Atributos(_configuration, PresentacionMi3, conn, tx);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar Clasificación → " + ex.Message, ex);
            }
        }
        // Método en la clase de servicio (PresentacionMi3SyncService)
        public List<clsBeProducto_presentacion> Get_All()
        {
            try
            {
                return clsLnProducto_presentacion.GetAll(_configuration);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener presentaciones → " + ex.Message, ex);
            }
        }

    }
}
