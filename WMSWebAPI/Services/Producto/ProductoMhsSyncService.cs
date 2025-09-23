using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Producto;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Productos;
using WMSWebAPI.Services;
using WMSWebAPI.Services.Producto;

public class ProductoMhsSyncService : WMSWebAPI.Services.Producto.IProductoMhsSyncService
{

    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public ProductoMhsSyncService(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }

    public void ProcesarProductoSingleDto(ProductoMhsDto dto, SqlConnection conn, SqlTransaction tx)
    {
        try
        {
            var producto = _mapper.Map<clsBeProductoMhs>(dto);
            if (producto != null)
                clsLnProducto.Valida_Atributos(_configuration, producto, conn, tx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar Producto → " + ex.Message, ex);
        }
    }

   
}


    


