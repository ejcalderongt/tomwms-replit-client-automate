using Microsoft.AspNetCore.Mvc;
using WMS.EntityCore.Dtos.Centro_Costo;
using WMSWebAPI.Be;
using WMSWebAPI.Services.Centro_Costo;

namespace WMSWebAPI.Controllers
{

    namespace WMSWebAPI.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class CentroCostoController : ControllerBase
        {
            private readonly ICentroCostoService _centroCostoService;

            public CentroCostoController(ICentroCostoService centroCostoService)
            {
                _centroCostoService = centroCostoService ?? throw new ArgumentNullException(nameof(centroCostoService));
            }

            // GET: api/CentroCosto
            [HttpGet]
            public async Task<ActionResult<IEnumerable<clsBeCentro_costo>>> GetAll()
            {
                try
                {
                    var centrosCosto = await _centroCostoService.GetAllAsync();
                    return Ok(centrosCosto);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error al obtener los centros de costo: {ex.Message}");
                }
            }

            // GET: api/CentroCosto/5
            [HttpGet("{id}")]
            public async Task<ActionResult<clsBeCentro_costo>> GetById(int id)
            {
                try
                {
                    var centroCosto = await _centroCostoService.GetByIdAsync(id);

                    if (centroCosto == null)
                    {
                        return NotFound($"Centro de costo con ID {id} no encontrado.");
                    }

                    return Ok(centroCosto);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error al obtener el centro de costo: {ex.Message}");
                }
            }

            // POST: api/CentroCosto
            [HttpPost]
            public async Task<ActionResult<clsBeCentro_costo>> Create([FromBody] clsBeCentro_costo centroCosto)
            {
                try
                {
                    if (centroCosto == null)
                    {
                        return BadRequest("El objeto centro de costo no puede ser nulo.");
                    }

                    var centroCreado = await _centroCostoService.CreateAsync(centroCosto);
                    return CreatedAtAction(nameof(GetById), new { id = centroCreado.IdCentroCosto }, centroCreado);
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error al crear el centro de costo: {ex.Message}");
                }
            }

            // PUT: api/CentroCosto/5
            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] clsBeCentro_costo centroCosto)
            {
                try
                {
                    if (centroCosto == null)
                    {
                        return BadRequest("El objeto centro de costo no puede ser nulo.");
                    }

                    if (id != centroCosto.IdCentroCosto)
                    {
                        return BadRequest("El ID en la URL no coincide con el ID del objeto.");
                    }

                    bool actualizado = await _centroCostoService.UpdateAsync(centroCosto);

                    if (actualizado)
                    {
                        return NoContent();
                    }

                    return StatusCode(500, "No se pudo actualizar el centro de costo.");
                }
                catch (ArgumentException ex)
                {
                    return BadRequest(ex.Message);
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error al actualizar el centro de costo: {ex.Message}");
                }
            }

            // DELETE: api/CentroCosto/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                try
                {
                    bool eliminado = await _centroCostoService.DeleteAsync(id);

                    if (eliminado)
                    {
                        return NoContent();
                    }

                    return StatusCode(500, "No se pudo eliminar el centro de costo.");
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error al eliminar el centro de costo: {ex.Message}");
                }
            }

            // GET: api/CentroCosto/maxid
            [HttpGet("maxid")]
            public async Task<ActionResult<int>> GetMaxId()
            {
                try
                {
                    int maxId = await _centroCostoService.GetMaxIdAsync();
                    return Ok(maxId);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error al obtener el máximo ID: {ex.Message}");
                }
            }

            // GET: api/CentroCosto/search
            [HttpGet("search")]
            public async Task<ActionResult<IEnumerable<clsBeCentro_costo>>> Search(
                [FromQuery] string? codigo = null,
                [FromQuery] string? nombre = null,
                [FromQuery] bool? activo = null)
            {
                try
                {
                    var resultados = await _centroCostoService.SearchAsync(codigo, nombre, activo);
                    return Ok(resultados);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error en la búsqueda: {ex.Message}");
                }
            }

            // GET: api/CentroCosto/activos
            [HttpGet("activos")]
            public async Task<ActionResult<IEnumerable<clsBeCentro_costo>>> GetActivos()
            {
                try
                {
                    var activos = await _centroCostoService.GetActivosAsync();
                    return Ok(activos);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error al obtener centros de costo activos: {ex.Message}");
                }
            }

            // POST: api/CentroCosto/batch
            [HttpPost("batch")]
            public async Task<ActionResult<BatchResult>> CreateBatch([FromBody] List<clsBeCentro_costo> centrosCosto)
            {
                try
                {
                    if (centrosCosto == null || centrosCosto.Count == 0)
                    {
                        return BadRequest("La lista de centros de costo no puede estar vacía.");
                    }

                    int exitosos = 0;
                    int fallidos = 0;
                    var errores = new List<string>();

                    foreach (var centro in centrosCosto)
                    {
                        try
                        {
                            await _centroCostoService.CreateAsync(centro);
                            exitosos++;
                        }
                        catch (Exception ex)
                        {
                            fallidos++;
                            errores.Add($"Error con centro de costo {centro.Codigo}: {ex.Message}");
                        }
                    }

                    var resultado = new BatchResult
                    {
                        Exitosos = exitosos,
                        Fallidos = fallidos,
                        Errores = errores
                    };

                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error en el proceso batch: {ex.Message}");
                }
            }

            // POST: api/CentroCosto/list/mi3/insert
            [HttpPost("list/mi3/insert")]
            public async Task<ActionResult<BatchResultMi3>> CreateBatchMi3([FromBody] List<CentroCostoMi3Dto> centrosCostoDto)
            {
                try
                {
                    if (centrosCostoDto == null || centrosCostoDto.Count == 0)
                    {
                        return BadRequest(new BatchResultMi3
                        {
                            Fallidos = 0,
                            Exitosos = 0,
                            Errores = new List<string> { "La lista de centros de costo no puede estar vacía." }
                        });
                    }

                    // Validación rápida (antes de ir a BD)
                    var erroresValidacion = new List<string>();
                    for (int i = 0; i < centrosCostoDto.Count; i++)
                    {
                        var dto = centrosCostoDto[i];
                        if (string.IsNullOrWhiteSpace(dto.Codigo))
                            erroresValidacion.Add($"Elemento {i + 1}: El código es requerido");
                        if (string.IsNullOrWhiteSpace(dto.Nombre))
                            erroresValidacion.Add($"Elemento {i + 1}: El nombre es requerido");
                    }

                    if (erroresValidacion.Count > 0)
                    {
                        return BadRequest(new BatchResultMi3
                        {
                            Fallidos = centrosCostoDto.Count,
                            Exitosos = 0,
                            Errores = erroresValidacion
                        });
                    }

                    // FIX: Await del service (antes era Task<> sin await)
                    var resultado = await _centroCostoService.ProcesarBatchMi3Async(centrosCostoDto);

                    return Ok(resultado);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new BatchResultMi3
                    {
                        Fallidos = centrosCostoDto?.Count ?? 0,
                        Exitosos = 0,
                        Errores = new List<string> { $"Error en el proceso batch MI3: {ex.Message}" }
                    });
                }
            }

            // POST: api/CentroCosto/mi3/insert
            [HttpPost("mi3/insert")]
            public async Task<ActionResult<CentroCostoDetalleResult>> InsertSingleMi3([FromBody] CentroCostoMi3Dto centroCostoDto)
            {
                try
                {
                    if (centroCostoDto == null)
                        return BadRequest("El objeto centro de costo no puede ser nulo.");

                    if (string.IsNullOrWhiteSpace(centroCostoDto.Codigo))
                        return BadRequest("El código es requerido.");

                    if (string.IsNullOrWhiteSpace(centroCostoDto.Nombre))
                        return BadRequest("El nombre es requerido.");

                    var resultado = await _centroCostoService.ProcesarBatchMi3Async(new List<CentroCostoMi3Dto> { centroCostoDto });

                    var detalle = resultado.Detalles?.FirstOrDefault()
                                 ?? new CentroCostoDetalleResult
                                 {
                                     Codigo = centroCostoDto.Codigo,
                                     Nombre = centroCostoDto.Nombre,
                                     Procesado = resultado.Exitosos > 0,
                                     Mensaje = resultado.Exitosos > 0
                                         ? "Centro de costo procesado exitosamente"
                                         : (resultado.Errores?.FirstOrDefault() ?? "Error desconocido")
                                 };

                    if (detalle.Procesado) return Ok(detalle);
                    return BadRequest(detalle);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new CentroCostoDetalleResult
                    {
                        Codigo = centroCostoDto?.Codigo ?? string.Empty,
                        Nombre = centroCostoDto?.Nombre ?? string.Empty,
                        Procesado = false,
                        Mensaje = $"Error al procesar: {ex.Message}"
                    });
                }
            }

            // GET: api/CentroCosto/mi3/search?codigo=ABC
            [HttpGet("mi3/search")]
            public async Task<ActionResult<IEnumerable<CentroCostoMi3Dto>>> SearchMi3(
                [FromQuery] string? codigo = null,
                [FromQuery] string? nombre = null)
            {
                try
                {
                    var centros = await _centroCostoService.SearchAsync(codigo, nombre, true); // Solo activos

                    var dtos = centros.Select(centro => new CentroCostoMi3Dto
                    {
                        Codigo = centro.Codigo,
                        Nombre = centro.Nombre,
                        Activo = centro.Activo,
                        Referencia = centro.Referencia,
                        ControlInventario = centro.Control_inventario
                    }).ToList();

                    return Ok(dtos);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error en búsqueda MI3: {ex.Message}");
                }
            }
          

        }

    }

        // Clase auxiliar para resultados batch
        public class BatchResult
        {
            public int Exitosos { get; set; }
            public int Fallidos { get; set; }
            public List<string> Errores { get; set; } = new List<string>();
        }
    }