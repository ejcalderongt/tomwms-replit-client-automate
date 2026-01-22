using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WMSWebAPI.Dtos.Stock;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly IConfiguration _config;

    public StockController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("listar")]
    public IActionResult GetAllStock([FromQuery] int IdBodega = 0, [FromQuery] int IdPropietario = 0, [FromQuery] string codigo = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 100)
    {
        var result = new List<StockResDto>();
        int totalCount = 0;

        try
        {
            using var conn = new SqlConnection(_config.GetConnectionString("CST"));
            conn.Open();

            string filterQuery = " WHERE 1=1";
            if (IdBodega != 0)
            {
                filterQuery += " AND IdBodega = @IdBodega";
            }

            if (IdPropietario != 0)
            {
                filterQuery += " AND IdPropietario = @IdPropietario";
            }

            if (!string.IsNullOrWhiteSpace(codigo))
            {
                filterQuery += " AND codigo LIKE '%' + @Codigo + '%'";
            }

            // Total count
            string countQuery = "SELECT COUNT(*) FROM vw_stock_res" + filterQuery;
            using (var countCmd = new SqlCommand(countQuery, conn))
            {
                if (IdBodega != 0)
                    countCmd.Parameters.AddWithValue("@IdBodega", IdBodega);
                if (IdPropietario != 0)
                    countCmd.Parameters.AddWithValue("@IdPropietario", IdPropietario);
                if (!string.IsNullOrWhiteSpace(codigo))
                    countCmd.Parameters.AddWithValue("@Codigo", codigo);

                totalCount = (int)countCmd.ExecuteScalar();
            }

            // Data paginated
            string query = "SELECT * FROM vw_stock_res" + filterQuery + " ORDER BY IdBodega OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            using var cmd = new SqlCommand(query, conn);

            if (IdBodega != 0)
                cmd.Parameters.AddWithValue("@IdBodega", IdBodega);
            if (IdPropietario != 0)
                cmd.Parameters.AddWithValue("@IdPropietario", IdPropietario);
            if (!string.IsNullOrWhiteSpace(codigo))
                cmd.Parameters.AddWithValue("@Codigo", codigo);

            cmd.Parameters.AddWithValue("@Offset", (page - 1) * pageSize);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var dto = new StockResDto();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var prop = typeof(StockResDto).GetProperty(reader.GetName(i));
                    if (prop != null && !reader.IsDBNull(i))
                    {
                        prop.SetValue(dto, Convert.ChangeType(reader.GetValue(i), prop.PropertyType));
                    }
                }

                result.Add(dto);
            }

            return Ok(new { total = totalCount, data = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
        }
    }

    [HttpGet("resumen")]
    public IActionResult GetResumen([FromQuery] int IdBodega, [FromQuery] int IdPropietario)
    {
        try
        {
            using var conn = new SqlConnection(_config.GetConnectionString("CST"));
            conn.Open();

            string query = @"
                    SELECT *
                    FROM VW_Stock_Res stock_res
                    INNER JOIN bodega_ubicacion b_ubicacion ON stock_res.IdUbicacion = b_ubicacion.IdUbicacion
                        AND stock_res.IdTramo = b_ubicacion.IdTramo
                        AND stock_res.IdBodega = b_ubicacion.IdBodega
                    INNER JOIN bodega_tramo b_tramo ON b_tramo.IdTramo = b_ubicacion.IdTramo
                        AND b_tramo.IdBodega = b_ubicacion.IdBodega
                    WHERE stock_res.IdPropietario = @IdPropietario" +
                (IdBodega != 0 ? " AND stock_res.IdBodega = @IdBodega" : "") +
                " ORDER BY stock_res.codigo";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@IdPropietario", IdPropietario);
            if (IdBodega != 0) cmd.Parameters.AddWithValue("@IdBodega", IdBodega);

            using var reader = cmd.ExecuteReader();
            var result = new List<StockResDto>();
            try
            {
                while (reader.Read())
                {
                    var dto = new StockResDto();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var prop = typeof(StockResDto).GetProperty(reader.GetName(i));
                        if (prop != null && !reader.IsDBNull(i))
                        {
                            prop.SetValue(dto, Convert.ChangeType(reader.GetValue(i), prop.PropertyType));
                        }
                    }
                    result.Add(dto);
                }
            }
            finally
            {
                if (!reader.IsClosed)
                    reader.Close();
            }

            var resumen = result
                .GroupBy(i => new { i.IdProducto, i.codigo, i.Propietario, i.nombre, i.Presentacion, i.codigo_barra, i.UnidadMedida, i.Bodega })
                .Select(g => new
                {
                    id = g.Key.IdProducto,
                    cod = g.Key.codigo,
                    prop = g.Key.Propietario,
                    nom = g.Key.nombre,
                    pres = g.Key.Presentacion,
                    barra = g.Key.codigo_barra,
                    um = g.Key.UnidadMedida,
                    bodega = g.Key.Bodega,
                    CantidadUMBas = g.Sum(x => x.Cantidad_UMBas),
                    CantidadPresentacion = g.Sum(x => x.Cantidad_Presentacion)
                }).ToList();

            return Ok(new { ResumenProducto = resumen });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
        }
    }
}