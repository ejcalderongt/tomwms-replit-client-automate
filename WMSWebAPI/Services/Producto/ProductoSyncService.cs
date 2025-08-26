using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Producto;
using WMS.EntityCore.Propietario;
using WMSWebAPI.Dtos.Catalogos;
using WMSWebAPI.Dtos.Productos;
using WMSWebAPI.Services;
public class ProductoSyncService : IProductoSyncService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;

    public ProductoSyncService(IConfiguration configuration, IMapper mapper)
    {
        _configuration = configuration;
        _mapper = mapper;
    }
    public void ProcesarProductoDesdeDto(ProductoTransDto dto, SqlConnection conn, SqlTransaction tx)
    {
        try
        {
            if (dto.Propietario != null) {
                var propietario_bodega = _mapper.Map<List<clsBePropietario_bodega>>(dto.PropietarioBodega);
                clsLnPropietario_bodega.InsertOrUpdate(_configuration, propietario_bodega, conn, tx);
            }
            
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar Propietario → " + ex.Message, ex);
        }

        try
        {
            if (dto.PropietarioBodega != null)
            {
                var propietario_bodega = _mapper.Map<List<clsBePropietario_bodega>>(dto.PropietarioBodega);
                clsLnPropietario_bodega.InsertOrUpdate(_configuration, propietario_bodega, conn, tx);    
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar PropietarioBodega → " + ex.Message, ex);
        }

        try
        {
            if (dto.Marca != null)
                if (dto.Marca.IdMarca != 0)
                    clsLnProductoMarca.InsertOrUpdate(_configuration, _mapper.Map<clsBeProducto_marca>(dto.Marca), conn, tx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar Marca → " + ex.Message, ex);
        }

        try
        {
            if (dto.TipoProducto != null)
                if (dto.TipoProducto.IdTipoProducto != 0)
                    clsLnProducto_tipo.InsertOrUpdate(_configuration, _mapper.Map<clsBeProducto_tipo>(dto.TipoProducto), conn, tx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar Tipo → " + ex.Message, ex);
        }

        try
        {
            if (dto.Clasificacion != null)
                if (dto.Clasificacion.IdClasificacion != 0)
                    clsLnProducto_clasificacion.InsertOrUpdate(_configuration, _mapper.Map<clsBeProducto_clasificacion>(dto.Clasificacion), conn, tx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar Clasificación → " + ex.Message, ex);
        }

        try
        {
            if (dto.Familia != null)
                if (dto.Familia.IdFamilia != 0)
                    clsLnProducto_familia.InsertOrUpdate(_configuration, _mapper.Map<clsBeProducto_familia>(dto.Familia), conn, tx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar Familia → " + ex.Message, ex);
        }

        try
        {
            if (dto.ParametroA != null)
                if (dto.ParametroA.IdProductoParametroA != 0)
                    clsLnProductoParametroA.InsertOrUpdate(_configuration, _mapper.Map<clsBeProducto_parametro_a>(dto.ParametroA), conn, tx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar ParametroA → " + ex.Message, ex);
        }

        try
        {
            if (dto.ParametroB != null)
                if (dto.ParametroB.IdProductoParametroB != 0)
                    clsLnProductoParametroB.InsertOrUpdate(_configuration, _mapper.Map<clsBeProducto_parametro_b>(dto.ParametroB), conn, tx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar ParametroB → " + ex.Message, ex);
        }

        //try
        //{
        //    if (dto.Presentaciones != null)
        //    {
        //        var presentaciones = _mapper.Map<List<clsBeProducto_presentacion>>(dto.Presentaciones);
        //        clsLnProducto_presentacion.InsertOrUpdate(_configuration, presentaciones, conn, tx);
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw new Exception("Error al procesar Presentaciones → " + ex.Message, ex);
        //}        
        try
        {
            if (dto.ProductoEstado != null)
            {
                var producto_estado = _mapper.Map<List<clsBeProducto_estado>>(dto.ProductoEstado);
                clsLnProducto_estado.InsertOrUpdate(_configuration, producto_estado, conn, tx);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar Producto_Estado → " + ex.Message, ex);
        }
        try
        {
            var producto = _mapper.Map<clsBeProducto>(dto);
            if (producto != null)
                if(producto.IdProducto != 0)                
                    clsLnProducto.InsertOrUpdate(_configuration, producto, conn, tx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar Producto → " + ex.Message, ex);
        }
        try
        {
            if (dto.ProductoBodega != null)
            {                
                var producto_bodega = _mapper.Map<List<clsBeProducto_bodega>>(dto.ProductoBodega);
                clsLnProducto_bodega.InsertOrUpdate(_configuration, producto_bodega, conn, tx);
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar Productos_Bodega → " + ex.Message, ex);
        }
        try
        {
            var unidad_medida = _mapper.Map<List<clsBeUnidad_medida>>(dto.UnidadMedida);
            if (unidad_medida != null)
                clsLnUnidad_medida.InsertOrUpdate(_configuration, unidad_medida, conn, tx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar Unidades de medida → " + ex.Message, ex);
        }
    }
    public void ProcesarProductoSingleDto(ProductoDto dto, SqlConnection conn, SqlTransaction tx)
    {
        try
        {
            var producto = _mapper.Map<clsBeProducto>(dto);
            if (producto != null)
                clsLnProducto.InsertOrUpdate(_configuration, producto, conn, tx);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al procesar Producto → " + ex.Message, ex);
        }
    }
}