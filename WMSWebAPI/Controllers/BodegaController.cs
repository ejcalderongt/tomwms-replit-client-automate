using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WMSWebAPI.Dtos.Bodega;

namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BodegasController : ControllerBase
    {
        private readonly IConfiguration _config;

        public BodegasController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("listar")]
        public IActionResult GetAll()
        {
            var result = new List<BodegaDto>();

            try
            {
                using var conn = new SqlConnection(_config.GetConnectionString("CST"));
                conn.Open();

                string query = "SELECT IdBodega, Codigo + ' - ' + Nombre AS Bodega FROM bodega";

                using var cmd = new SqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new BodegaDto
                    {
                        IdBodega = reader.GetInt32(reader.GetOrdinal("IdBodega")),                        
                        Nombre = reader["Bodega"]?.ToString() ?? ""
                    });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Exito = false, Mensaje = ex.Message });
            }
        }
    }    
}