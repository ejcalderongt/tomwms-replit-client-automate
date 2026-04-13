using AutoMapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Datos_Maestros;
using WMS.EntityCore.Propietario;
using WMSWebAPI.Dtos.Catalogos;

namespace WMSWebAPI.Services.Propietario
{
    public class PropietarioService : IPropietarioService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<PropietarioService> _logger;

        public PropietarioService(
            IConfiguration configuration,
            IMapper mapper,
            ILogger<PropietarioService> logger)
        {
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public void ProcesarPropietarioDesdeDto(PropietarioDto dto, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                if (dto == null)
                    throw new ArgumentNullException(nameof(dto), "El DTO del propietario no puede ser nulo");

                ValidaAtributos(dto, conn, tx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar Propietario (IdPropietario: {IdPropietario})", dto?.IdPropietario);
                throw new Exception($"Error al procesar Propietario (IdPropietario: {dto?.IdPropietario}) → {ex.Message}", ex);
            }
        }

        public void ProcesarListaPropietariosDesdeDto(List<PropietarioDto> dtos, SqlConnection conn, SqlTransaction tx)
        {
            if (dtos == null || !dtos.Any())
                return;

            try
            {
                foreach (var dto in dtos)
                {
                    ProcesarPropietarioDesdeDto(dto, conn, tx);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar lista de propietarios");
                throw new Exception($"Error al procesar lista de propietarios → {ex.Message}", ex);
            }
        }

        public void ValidaAtributos(PropietarioDto entity, SqlConnection conn, SqlTransaction tx)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (conn == null)
                throw new ArgumentNullException(nameof(conn));

            if (tx == null)
                throw new ArgumentNullException(nameof(tx));

            try
            {
                var propietario = _mapper.Map<clsBePropietarios>(entity);
                bool existe = ExistePropietario(propietario.IdPropietario, conn, tx);

                if (!existe)
                {
                    InsertarPropietario(propietario, conn, tx);
                }
                else
                {
                    ActualizarPropietario(propietario, conn, tx);
                }
            }
            catch (Exception ex)
            {
                var method = System.Reflection.MethodBase.GetCurrentMethod();
                _logger.LogError(ex, "Error en ValidaAtributos");
                throw new Exception($"{method?.DeclaringType?.Name}.{method?.Name}: {ex.Message}", ex);
            }
        }

        private void InsertarPropietario(clsBePropietarios propietario, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                if (propietario.IdPropietario == 0)
                {
                    propietario.IdPropietario = GetMaxId(conn, tx) + 1;
                }

                int rowsAffected = clsLnPropietarios.Insertar(_configuration, propietario, conn, tx);

                if (rowsAffected <= 0)
                    throw new Exception("No se pudo insertar el propietario");

                _logger.LogInformation("Propietario insertado exitosamente - ID: {IdPropietario}, Código: {Codigo}",
                    propietario.IdPropietario, propietario.Codigo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al insertar propietario");
                throw new Exception($"Error al insertar propietario: {ex.Message}", ex);
            }
        }

        private void ActualizarPropietario(clsBePropietarios propietario, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                int rowsAffected = clsLnPropietarios.Actualizar(_configuration, propietario, conn, tx);

                if (rowsAffected <= 0)
                    throw new Exception("No se pudo actualizar el propietario");

                _logger.LogInformation("Propietario actualizado exitosamente - ID: {IdPropietario}, Código: {Codigo}",
                    propietario.IdPropietario, propietario.Codigo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar propietario");
                throw new Exception($"Error al actualizar propietario: {ex.Message}", ex);
            }
        }

        public List<clsBePropietarios> GetAll()
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los propietarios");
                return clsLnPropietarios.GetAll(_configuration);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los propietarios");
                throw new Exception($"Error al obtener todos los propietarios → {ex.Message}", ex);
            }
        }

        public clsBePropietarios? GetById(int idPropietario)
        {
            try
            {
                _logger.LogInformation("Obteniendo propietario por ID: {IdPropietario}", idPropietario);
                var propietario = new clsBePropietarios { IdPropietario = idPropietario };
                bool existe = clsLnPropietarios.GetSingle(_configuration, ref propietario);

                return existe ? propietario : null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener propietario por ID: {IdPropietario}", idPropietario);
                throw new Exception($"Error al obtener propietario por ID {idPropietario} → {ex.Message}", ex);
            }
        }

        public clsBePropietarios? GetByCodigo(string codigo)
        {
            try
            {
                _logger.LogInformation("Obteniendo propietario por código: {Codigo}", codigo);
                var propietarios = GetAll();
                return propietarios.FirstOrDefault(p => p.Codigo == codigo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener propietario por código: {Codigo}", codigo);
                throw new Exception($"Error al obtener propietario por código {codigo} → {ex.Message}", ex);
            }
        }

        public List<clsBePropietarios> GetActivos()
        {
            try
            {
                _logger.LogInformation("Obteniendo propietarios activos");
                var all = GetAll();
                return all.Where(p => p.Activo).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener propietarios activos");
                throw new Exception($"Error al obtener propietarios activos → {ex.Message}", ex);
            }
        }

        public PropietarioListResponseDto GetByFilter(PropietarioFilterDto filter)
        {
            try
            {
                _logger.LogInformation("Obteniendo propietarios con filtros: {@Filter}", filter);
                var all = GetAll();
                var query = all.AsQueryable();

                // Aplicar filtros
                if (filter.IdPropietario.HasValue && filter.IdPropietario.Value > 0)
                    query = query.Where(p => p.IdPropietario == filter.IdPropietario.Value);

                if (!string.IsNullOrWhiteSpace(filter.Codigo))
                    query = query.Where(p => p.Codigo.Contains(filter.Codigo, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrWhiteSpace(filter.Nombre_Comercial))
                    query = query.Where(p => p.Nombre_comercial.Contains(filter.Nombre_Comercial, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrWhiteSpace(filter.Contacto))
                    query = query.Where(p => p.Contacto.Contains(filter.Contacto, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrWhiteSpace(filter.Email))
                    query = query.Where(p => p.Email.Contains(filter.Email, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrWhiteSpace(filter.NIT))
                    query = query.Where(p => p.NIT.Contains(filter.NIT, StringComparison.OrdinalIgnoreCase));

                if (filter.Activo.HasValue)
                    query = query.Where(p => p.Activo == filter.Activo.Value);

                if (filter.Sistema.HasValue)
                    query = query.Where(p => p.Sistema == filter.Sistema.Value);

                if (filter.IdEmpresa.HasValue && filter.IdEmpresa.Value > 0)
                    query = query.Where(p => p.IdEmpresa == filter.IdEmpresa.Value);

                // Aplicar ordenamiento
                query = filter.SortDescending
                    ? query.OrderByDescending(p => GetPropertyValue(p, filter.SortBy ?? "IdPropietario"))
                    : query.OrderBy(p => GetPropertyValue(p, filter.SortBy ?? "IdPropietario"));

                var totalCount = query.Count();

                // Aplicar paginación
                var items = query
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToList();

                // Mapear a DTO de respuesta
                var responseItems = _mapper.Map<List<PropietarioResponseDto>>(items);

                return new PropietarioListResponseDto
                {
                    Items = responseItems,
                    TotalCount = totalCount,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al filtrar propietarios");
                throw new Exception($"Error al filtrar propietarios → {ex.Message}", ex);
            }
        }

        private static object GetPropertyValue(object obj, string propertyName)
        {
            var prop = obj.GetType().GetProperty(propertyName);
            return prop?.GetValue(obj) ?? string.Empty;
        }

        public void EliminarPropietario(int idPropietario, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                _logger.LogInformation("Eliminando propietario ID: {IdPropietario}", idPropietario);
                var propietario = new clsBePropietarios { IdPropietario = idPropietario };
                var lnPropietarios = new clsLnPropietarios();
                int rowsAffected = lnPropietarios.Eliminar(_configuration, propietario, conn, tx);

                if (rowsAffected <= 0)
                    throw new Exception($"No se pudo eliminar el propietario con ID {idPropietario}");

                _logger.LogInformation("Propietario eliminado exitosamente - ID: {IdPropietario}", idPropietario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar propietario ID: {IdPropietario}", idPropietario);
                throw new Exception($"Error al eliminar propietario (IdPropietario: {idPropietario}) → {ex.Message}", ex);
            }
        }

        public bool ExistePropietario(int idPropietario, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                return clsLnPropietarios.Existe(_configuration, idPropietario, conn, tx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar existencia del propietario ID: {IdPropietario}", idPropietario);
                throw new Exception($"Error al verificar existencia del propietario: {ex.Message}", ex);
            }
        }

        public bool ExistePropietarioByCodigo(string codigo, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                var propietario = GetByCodigo(codigo);
                return propietario != null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar existencia del propietario por código: {Codigo}", codigo);
                throw new Exception($"Error al verificar existencia del propietario por código: {ex.Message}", ex);
            }
        }

        public int GetMaxId(SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                return clsLnPropietarios.MaxID(_configuration, conn, tx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener máximo ID");
                throw new Exception($"Error al obtener máximo ID: {ex.Message}", ex);
            }
        }

        public List<int> GetBodegasByPropietarioId(int idPropietario)
        {
            try
            {
                _logger.LogInformation("Obteniendo bodegas para propietario ID: {IdPropietario}", idPropietario);

                // Verificar si existe el propietario
                var propietario = GetById(idPropietario);
                if (propietario == null)
                {
                    throw new Exception($"No se encontró el propietario con ID {idPropietario}");
                }

                // Usar el método eficiente de DAL que consulta directamente SQL
                var bodegasIds = clsLnPropietarios.GetBodegasIdsByPropietarioId(_configuration, idPropietario);

                _logger.LogInformation("Se encontraron {Count} bodegas para propietario ID: {IdPropietario}",
                    bodegasIds.Count, idPropietario);

                return bodegasIds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener bodegas para propietario ID: {IdPropietario}", idPropietario);
                throw new Exception($"Error al obtener bodegas del propietario: {ex.Message}", ex);
            }
        }

        public List<clsBeBodega> GetBodegasCompletasByPropietarioId(int idPropietario)
        {
            try
            {
                _logger.LogInformation("Obteniendo bodegas completas para propietario ID: {IdPropietario}", idPropietario);

                // Verificar si existe el propietario
                var propietario = GetById(idPropietario);
                if (propietario == null)
                {
                    throw new Exception($"No se encontró el propietario con ID {idPropietario}");
                }

                // Usar el método eficiente de DAL con JOIN
                var bodegas = clsLnPropietarios.GetBodegasCompletasByPropietarioId(_configuration, idPropietario);

                _logger.LogInformation("Se encontraron {Count} bodegas completas para propietario ID: {IdPropietario}",
                    bodegas.Count, idPropietario);

                return bodegas;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener bodegas completas para propietario ID: {IdPropietario}", idPropietario);
                throw new Exception($"Error al obtener bodegas completas del propietario: {ex.Message}", ex);
            }
        }

        public void SincronizarBodegasPropietario(int idPropietario, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                _logger.LogInformation("Sincronizando bodegas para propietario ID: {IdPropietario}", idPropietario);

                // Verificar si existe el propietario
                var propietario = GetById(idPropietario);
                if (propietario == null)
                {
                    throw new Exception($"No se encontró el propietario con ID {idPropietario}");
                }

                // Sincronizar bodegas usando el método de DAL
                clsLnPropietarios.AsignarPropietarioATodasLasBodegas(_configuration, propietario, conn, tx, "system");

                _logger.LogInformation("Bodegas sincronizadas exitosamente para propietario ID: {IdPropietario}", idPropietario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al sincronizar bodegas para propietario ID: {IdPropietario}", idPropietario);
                throw new Exception($"Error al sincronizar bodegas del propietario: {ex.Message}", ex);
            }
        }

        public List<object> GetAllWithBodegas()
        {
            try
            {
                _logger.LogInformation("Obteniendo todos los propietarios con sus bodegas");

                var propietarios = GetAll();
                var todasAsociaciones = clsLnPropietario_bodega.GetAll(_configuration);
                var todasBodegas = clsLnBodega.GetAll(_configuration);

                var resultado = propietarios.Select(p => new
                {
                    propietario = p,
                    bodegas = todasBodegas
                        .Where(b => todasAsociaciones.Any(a => a.IdPropietario == p.IdPropietario && a.IdBodega == b.IdBodega && a.Activo))
                        .ToList()
                }).ToList();

                return resultado.Cast<object>().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener propietarios con bodegas");
                throw new Exception($"Error al obtener propietarios con bodegas: {ex.Message}", ex);
            }
        }        

    }
}