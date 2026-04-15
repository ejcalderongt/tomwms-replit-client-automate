using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Acuerdos;
using WMSWebAPI.Services.AcuerdosComerciales;

namespace WMSWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AcuerdoComercialController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAcuerdoComercialService _acuerdoService;

        public AcuerdoComercialController(IConfiguration configuration, IAcuerdoComercialService acuerdoService)
        {
            _configuration = configuration;
            _acuerdoService = acuerdoService;
        }

        [HttpPost("Procesar")]
        public IActionResult ProcesarAcuerdo([FromBody] AcuerdoComercialEncDto dto)
        {
            try
            {
                using var connection = new SqlConnection(_configuration.GetConnectionString("CST"));
                connection.Open();
                using var transaction = connection.BeginTransaction();

                try
                {
                    _acuerdoService.ProcesarAcuerdoComercialDesdeDto(dto, connection, transaction);
                    transaction.Commit();
                    return Ok(new { mensaje = "Acuerdo comercial procesado exitosamente", id = dto.IdAcuerdoEnc });
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var acuerdos = _acuerdoService.Get_All();
                return Ok(acuerdos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("GetByIdCliente/{idCliente}")]
        public IActionResult GetByIdCliente(int idCliente)
        {
            try
            {
                var acuerdos = _acuerdoService.Get_All_By_IdCliente(idCliente);
                return Ok(acuerdos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("GetById/{idAcuerdoEnc}")]
        public IActionResult GetById(int idAcuerdoEnc)
        {
            try
            {
                var acuerdo = _acuerdoService.Get_By_Id(idAcuerdoEnc);
                if (acuerdo == null)
                    return NotFound($"No se encontró el acuerdo con ID {idAcuerdoEnc}");

                return Ok(acuerdo);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}