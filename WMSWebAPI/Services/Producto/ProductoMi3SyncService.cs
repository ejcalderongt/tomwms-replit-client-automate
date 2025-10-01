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

    public void ProcesarProductoSingleDto(ProductoMi3Dto dto, SqlConnection conn, SqlTransaction tx)
    {
        try
        {
            var producto = _mapper.Map<clsBeProductoMi3>(dto);
            if (producto != null)
                clsLnProducto.Valida_Atributos(_configuration, producto, conn, tx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar Producto → " + ex.Message, ex);
        }
    }

   
}


    


