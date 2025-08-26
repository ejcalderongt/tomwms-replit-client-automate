using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WMSWebAPI.Dtos.Movimientos.WMSWebAPI.Dto;

namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly IConfiguration _config;

        public MovimientosController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("listar")]
        public IActionResult GetMovimientos(
            [FromQuery] int IdPropietario,
            [FromQuery] int? IdBodega = null,
            [FromQuery] DateTime? FechaInicio = null,
            [FromQuery] DateTime? FechaFin = null)
        {
            try
            {
                using var conn = new SqlConnection(_config.GetConnectionString("CST"));
                conn.Open();

                var query = @"SELECT * FROM VW_Movimientos_N WHERE IdPropietario = @IdPropietario";

                if (IdBodega.HasValue && IdBodega.Value != 0)
                {
                    query += " AND IdBodega = @IdBodega";
                }

                if (FechaInicio.HasValue && FechaFin.HasValue)
                {
                    query += " AND CONVERT(date, fecha) BETWEEN @FechaInicio AND @FechaFin";
                }

                using var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdPropietario", IdPropietario);

                if (IdBodega.HasValue && IdBodega.Value != 0)
                {
                    cmd.Parameters.AddWithValue("@IdBodega", IdBodega.Value);
                }

                if (FechaInicio.HasValue && FechaFin.HasValue)
                {
                    cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio.Value.Date);
                    cmd.Parameters.AddWithValue("@FechaFin", FechaFin.Value.Date);
                }

                using var reader = cmd.ExecuteReader();
                var result = new List<MovimientosRptDto>();

                while (reader.Read())
                {
                    var dto = new MovimientosRptDto();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var prop = typeof(MovimientosRptDto).GetProperty(reader.GetName(i));
                        if (prop != null && !reader.IsDBNull(i))
                        {
                            prop.SetValue(dto, Convert.ChangeType(reader.GetValue(i), prop.PropertyType));
                        }
                    }
                    result.Add(dto);
                }

                reader.Close();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
            }
        }
    }
}