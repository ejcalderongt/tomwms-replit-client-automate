using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Producto;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Services;


public class ProductoMi3SyncService : IProductoMi3SyncService
{

    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public ProductoMi3SyncService(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }
    public int ProcesarProductoSingleDto(ProductoMi3Dto dto, SqlConnection conn, SqlTransaction tx)
    {
        if (dto == null) throw new ArgumentNullException(nameof(dto));
        if (conn == null) throw new ArgumentNullException(nameof(conn));
        if (tx == null) throw new ArgumentNullException(nameof(tx));

        try
        {
            var be = _mapper.Map<clsBeProductoMi3>(dto);
            if (be == null) throw new InvalidOperationException("No fue posible mapear el DTO a clsBeProductoMi3.");
            
            int idProducto = clsLnProducto.Procesar_Producto(_configuration, be, conn, tx);

            if (idProducto <= 0)
                throw new InvalidOperationException($"No se obtuvo IdProducto para el código '{be.codigo}'.");

            return idProducto;
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar Producto → " + ex.Message, ex);
        }
    }

    public List<clsBeProducto> Get_All()
    {
        try
        {
            return clsLnProducto.GetAll(_configuration);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener productos → " + ex.Message, ex);
        }
    }

}





