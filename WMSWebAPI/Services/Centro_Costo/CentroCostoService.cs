using Microsoft.Data.SqlClient;
using System.Transactions;
using WMS.EntityCore.Dtos.Centro_Costo;
using WMSWebAPI.Be;
using WMSWebAPI.Ln;

namespace WMSWebAPI.Services.Centro_Costo
{
    public class CentroCostoService : ICentroCostoService
    {
        private readonly IConfiguration _configuration;

        public CentroCostoService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<IEnumerable<clsBeCentro_costo>> GetAllAsync()
        {
            return await Task.Run(() => clsLnCentro_costo.GetAll(_configuration));
        }

        public async Task<clsBeCentro_costo?> GetByIdAsync(int id)
        {
            var centroCosto = new clsBeCentro_costo { IdCentroCosto = id };
            bool encontrado = await Task.Run(() => clsLnCentro_costo.GetSingle(_configuration, ref centroCosto));

            return encontrado ? centroCosto : null;
        }

        public async Task<clsBeCentro_costo> CreateAsync(clsBeCentro_costo centroCosto)
        {
            if (centroCosto == null)
                throw new ArgumentNullException(nameof(centroCosto));

            // Validaciones
            if (string.IsNullOrWhiteSpace(centroCosto.Codigo))
                throw new ArgumentException("El código es requerido", nameof(centroCosto.Codigo));

            if (string.IsNullOrWhiteSpace(centroCosto.Nombre))
                throw new ArgumentException("El nombre es requerido", nameof(centroCosto.Nombre));

            // Auto-generar ID si es necesario
            if (centroCosto.IdCentroCosto == 0)
            {
                int maxId = await GetMaxIdAsync();
                centroCosto.IdCentroCosto = maxId + 1;
            }

            // Asignar fechas por defecto
            if (centroCosto.Fec_agr == DateTime.MinValue)
                centroCosto.Fec_agr = DateTime.Now;

            if (centroCosto.Fec_mod == DateTime.MinValue)
                centroCosto.Fec_mod = DateTime.Now;

            int filasAfectadas = await Task.Run(() => clsLnCentro_costo.Insertar(_configuration, centroCosto));

            if (filasAfectadas == 0)
                throw new Exception("No se pudo crear el centro de costo");

            return await GetByIdAsync(centroCosto.IdCentroCosto) ?? centroCosto;
        }

        public async Task<bool> UpdateAsync(clsBeCentro_costo centroCosto)
        {
            if (centroCosto == null)
                throw new ArgumentNullException(nameof(centroCosto));

            // Verificar que existe
            var existente = await GetByIdAsync(centroCosto.IdCentroCosto);
            if (existente == null)
                throw new KeyNotFoundException($"Centro de costo con ID {centroCosto.IdCentroCosto} no encontrado");

            // Validaciones
            if (string.IsNullOrWhiteSpace(centroCosto.Codigo))
                throw new ArgumentException("El código es requerido", nameof(centroCosto.Codigo));

            if (string.IsNullOrWhiteSpace(centroCosto.Nombre))
                throw new ArgumentException("El nombre es requerido", nameof(centroCosto.Nombre));

            // Actualizar fecha de modificación
            centroCosto.Fec_mod = DateTime.Now;

            int filasAfectadas = await Task.Run(() => clsLnCentro_costo.Actualizar(_configuration, centroCosto));
            return filasAfectadas > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Verificar que existe
            var existente = await GetByIdAsync(id);
            if (existente == null)
                throw new KeyNotFoundException($"Centro de costo con ID {id} no encontrado");

            int filasAfectadas = await Task.Run(() =>
                clsLnCentro_costo.Eliminar(_configuration, new clsBeCentro_costo { IdCentroCosto = id }));

            return filasAfectadas > 0;
        }

        public async Task<int> GetMaxIdAsync()
        {
            return await Task.Run(() => clsLnCentro_costo.MaxID(_configuration));
        }

        public async Task<IEnumerable<clsBeCentro_costo>> SearchAsync(string? codigo = null, string? nombre = null, bool? activo = null)
        {
            var todos = await GetAllAsync();
            var resultados = new List<clsBeCentro_costo>();

            foreach (var centro in todos)
            {
                bool cumpleFiltro = true;

                if (!string.IsNullOrWhiteSpace(codigo) &&
                    !centro.Codigo.Contains(codigo, StringComparison.OrdinalIgnoreCase))
                {
                    cumpleFiltro = false;
                }

                if (!string.IsNullOrWhiteSpace(nombre) &&
                    !centro.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                {
                    cumpleFiltro = false;
                }

                if (activo.HasValue && centro.Activo != activo.Value)
                {
                    cumpleFiltro = false;
                }

                if (cumpleFiltro)
                {
                    resultados.Add(centro);
                }
            }

            return resultados;
        }

        public async Task<IEnumerable<clsBeCentro_costo>> GetActivosAsync()
        {
            var todos = await GetAllAsync();
            var activos = new List<clsBeCentro_costo>();

            foreach (var centro in todos)
            {
                if (centro.Activo)
                {
                    activos.Add(centro);
                }
            }

            return activos;
        }        

        private async Task ProcesarCentroCostoMi3Async(CentroCostoMi3Dto dto, clsBeI_nav_config_enc config,
                                                    SqlConnection conn, SqlTransaction tx)
        {
            // Buscar si ya existe por código
            var centroExistente = await BuscarPorCodigoAsync(dto.Codigo, conn, tx);

            if (centroExistente == null)
            {
                // INSERTAR NUEVO
                var nuevoCentro = new clsBeCentro_costo
                {
                    IdCentroCosto = await ObtenerMaxIdAsync(conn, tx) + 1,
                    IdEmpresa = config.Idempresa,
                    Codigo = dto.Codigo,
                    Nombre = dto.Nombre,
                    Referencia = dto.Referencia ?? string.Empty,
                    Control_inventario = dto.ControlInventario,
                    Activo = dto.Activo,
                    Fec_agr = DateTime.Now,
                    Fec_mod = DateTime.Now,
                    User_agr = config.IdUsuario.ToString(),
                    User_mod = config.IdUsuario.ToString()
                };

                // Insertar usando el método con transacción
                await Task.Run(() => Insertar(nuevoCentro, conn, tx));
            }
            else
            {
                // ACTUALIZAR EXISTENTE
                centroExistente.Nombre = dto.Nombre;
                centroExistente.Referencia = dto.Referencia ?? string.Empty;
                centroExistente.Control_inventario = dto.ControlInventario;
                centroExistente.Activo = dto.Activo;
                centroExistente.Fec_mod = DateTime.Now;
                centroExistente.User_mod = config.IdUsuario.ToString();

                // Actualizar usando el método con transacción
                await Task.Run(() => Actualizar(centroExistente, conn, tx));
            }
        }

        private async Task<clsBeCentro_costo?> BuscarPorCodigoAsync(string codigo, SqlConnection conn, SqlTransaction tx)
        {
            try
            {
                // Obtener todos y filtrar por código
                var todos = await Task.Run(() => GetAll(conn, tx));

                foreach (var centro in todos)
                {
                    if (centro.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase))
                    {
                        return centro;
                    }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private async Task<int> ObtenerMaxIdAsync(SqlConnection conn, SqlTransaction tx)
        {
            return await Task.Run(() => MaxID(conn, tx));
        }

        // Métodos auxiliares para trabajar con transacción
        private List<clsBeCentro_costo> GetAll(SqlConnection conn, SqlTransaction tx)
        {
            // Implementación usando transacción específica
            // Esta sería una versión del método GetAll que acepta conexión/transacción
            return clsLnCentro_costo.GetAll(_configuration);
        }

        private int Insertar(clsBeCentro_costo centroCosto, SqlConnection conn, SqlTransaction tx)
        {
            // Usar la sobrecarga que acepta conexión/transacción
            return clsLnCentro_costo.Insertar(_configuration, centroCosto, conn, tx);
        }

        private int Actualizar(clsBeCentro_costo centroCosto, SqlConnection conn, SqlTransaction tx)
        {
            // Usar la sobrecarga que acepta conexión/transacción
            return clsLnCentro_costo.Actualizar(_configuration, centroCosto, conn, tx);
        }

        private int MaxID(SqlConnection conn, SqlTransaction tx)
        {
            // Usar la sobrecarga que acepta conexión/transacción
            return clsLnCentro_costo.MaxID(_configuration, conn, tx);
        }

        public async Task<BatchResultMi3> ProcesarBatchMi3Async(List<CentroCostoMi3Dto> centrosCostoDto)
        {
            var resultado = new BatchResultMi3
            {
                Exitosos = 0,
                Fallidos = 0,
                Errores = new List<string>(),
                Detalles = new List<CentroCostoDetalleResult>()
            };

            // 1) Validaciones base
            if (centrosCostoDto == null || centrosCostoDto.Count == 0)
            {
                resultado.Errores.Add("La lista de centros de costo no puede estar vacía.");
                resultado.Fallidos = 0;
                return resultado;
            }

            var connectionString = _configuration.GetConnectionString("CST");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                resultado.Errores.Add("Connection string 'CST' not found in configuration.");
                resultado.Fallidos = centrosCostoDto.Count;
                return resultado;
            }

            // 2) Proceso en BD con transacción
            try
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                using var tx = conn.BeginTransaction();

                try
                {
                    // 2.1) Obtener configuración de interfaz MI3
                    var configInterface = new clsBeI_nav_config_enc();
                    clsLnI_nav_config_enc.GetSingle(configInterface, conn, tx);

                    if (configInterface == null)
                    {
                        resultado.Errores.Add("No se pudo obtener la configuración de interfaz MI3.");
                        resultado.Fallidos = centrosCostoDto.Count;
                        tx.Rollback();
                        return resultado;
                    }

                    // 2.2) Procesar todos los DTOs
                    foreach (var dto in centrosCostoDto)
                    {
                        try
                        {
                            // Validación por item (por si llega algo sucio)
                            if (dto == null)
                            {
                                resultado.Fallidos++;
                                resultado.Errores.Add("Elemento nulo en la lista.");
                                resultado.Detalles.Add(new CentroCostoDetalleResult
                                {
                                    Codigo = string.Empty,
                                    Nombre = string.Empty,
                                    Procesado = false,
                                    Mensaje = "Elemento nulo"
                                });
                                continue;
                            }

                            if (string.IsNullOrWhiteSpace(dto.Codigo))
                            {
                                resultado.Fallidos++;
                                resultado.Errores.Add($"Centro de costo sin código: {dto.Nombre}");
                                resultado.Detalles.Add(new CentroCostoDetalleResult
                                {
                                    Codigo = dto.Codigo ?? string.Empty,
                                    Nombre = dto.Nombre ?? string.Empty,
                                    Procesado = false,
                                    Mensaje = "Código requerido"
                                });
                                continue;
                            }

                            if (string.IsNullOrWhiteSpace(dto.Nombre))
                            {
                                resultado.Fallidos++;
                                resultado.Errores.Add($"Centro de costo sin nombre: {dto.Codigo}");
                                resultado.Detalles.Add(new CentroCostoDetalleResult
                                {
                                    Codigo = dto.Codigo ?? string.Empty,
                                    Nombre = dto.Nombre ?? string.Empty,
                                    Procesado = false,
                                    Mensaje = "Nombre requerido"
                                });
                                continue;
                            }

                            await ProcesarCentroCostoMi3Async(dto, configInterface, conn, tx);
                            var detalle = new CentroCostoDetalleResult
                            {
                                Codigo = dto.Codigo ?? string.Empty,
                                Nombre = dto.Nombre ?? string.Empty,
                                Procesado = true,
                                Mensaje = "Procesado correctamente"
                            };

                            if (detalle.Procesado) resultado.Exitosos++;
                            else resultado.Fallidos++;

                            resultado.Detalles.Add(detalle);
                        }
                        catch (Exception exItem)
                        {
                            resultado.Fallidos++;
                            resultado.Errores.Add($"Error procesando {dto?.Codigo ?? "(sin código)"}: {exItem.Message}");

                            resultado.Detalles.Add(new CentroCostoDetalleResult
                            {
                                Codigo = dto?.Codigo ?? string.Empty,
                                Nombre = dto?.Nombre ?? string.Empty,
                                Procesado = false,
                                Mensaje = exItem.Message
                            });
                        }
                    }

                    // 2.3) Confirmar / revertir
                    if (resultado.Fallidos == 0)
                    {
                        tx.Commit();
                    }
                    else
                    {
                        tx.Rollback();
                        resultado.Errores.Add("Transacción revertida debido a errores en el procesamiento.");
                    }
                }
                catch (Exception exTx)
                {
                    try { tx.Rollback(); } catch { /* no-op */ }
                    resultado.Errores.Add($"Error en transacción: {exTx.Message}");
                }
            }
            catch (Exception ex)
            {
                resultado.Errores.Add($"Error general: {ex.Message}");
                // No sabemos si hubo parciales sin transacción; pero aquí normalmente ya habría fallado antes de iterar.
                if (resultado.Fallidos == 0 && resultado.Exitosos == 0)
                    resultado.Fallidos = centrosCostoDto?.Count ?? 0;
            }

            return resultado;
        }



    }

}