using System.Data;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using WMS.EntityCore.Dtos.Inventario;

namespace WMSWebAPI.Services.Inventario
{
    public class InventarioService : IInventarioService
    {
        private const int MaxPageSize = 1000;
        private const int MaxResumenRows = 5000;
        private readonly IConfiguration _configuration;

        public InventarioService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<InventarioPagedResultDto<InventarioExistenciaLoteDto>> GetExistenciasPorLoteAsync(
            InventarioFiltroDto filtro,
            CancellationToken ct)
        {
            Normalize(filtro);
            EnsureSelectiveFilter(filtro);

            var where = BuildWhere(filtro, out var parameters);
            var offset = (filtro.Page - 1) * filtro.PageSize;
            parameters.Add("p_offset", offset);
            parameters.Add("p_fetch", filtro.PageSize);

            var countSql = $"SELECT COUNT(1) FROM VW_Stock_Res WITH (NOLOCK) {where};";
            var dataSql = $@"
SELECT
    IdBodega,
    ISNULL(Bodega, '') AS Bodega,
    IdPropietario,
    IdPropietarioBodega,
    ISNULL(Propietario, '') AS Propietario,
    IdProducto,
    IdProductoBodega,
    ISNULL(codigo, '') AS Codigo,
    ISNULL(codigo_barra, '') AS CodigoBarra,
    ISNULL(nombre, '') AS Producto,
    ISNULL(UnidadMedida, '') AS UnidadMedida,
    ISNULL(Presentacion, '') AS Presentacion,
    ISNULL(factor, 0) AS Factor,
    IdStock,
    ISNULL(IdStockRes, 0) AS IdStockRes,
    IdUbicacion,
    ISNULL(IdTramo, 0) AS IdTramo,
    ISNULL(Ubicacion_Tramo, '') AS UbicacionTramo,
    ISNULL(Ubicacion_Nombre, '') AS UbicacionNombre,
    ISNULL(Nombre_Completo, '') AS UbicacionCompleta,
    ISNULL(Ubicacion_Nivel, 0) AS UbicacionNivel,
    ISNULL(NomEstado, '') AS Estado,
    ISNULL(EstadoUtilizable, 0) AS EstadoUtilizable,
    ISNULL([dañado], 0) AS Danado,
    ISNULL(lote, '') AS Lote,
    ISNULL(lic_plate, '') AS Licencia,
    ISNULL(serial, '') AS Serial,
    ISNULL(IdRecepcionEnc, 0) AS IdRecepcionEnc,
    ISNULL(IdRecepcionDet, 0) AS IdRecepcionDet,
    ISNULL(fecha_ingreso, '19000101') AS FechaIngreso,
    ISNULL(fecha_vence, '19000101') AS FechaVence,
    ISNULL(CantidadSF, 0) AS CantidadUMBas,
    ISNULL(Cantidad, 0) AS CantidadPresentacion,
    ISNULL(CantidadReservada, 0) AS CantidadReservadaUMBas,
    ISNULL(CantidadSF, 0) - ISNULL(CantidadReservada, 0) AS DisponibleUMBas,
    CASE
        WHEN ISNULL(factor, 0) > 0
        THEN ROUND((ISNULL(CantidadSF, 0) - ISNULL(CantidadReservada, 0)) / factor, 6)
        ELSE 0
    END AS DisponiblePresentacion,
    ISNULL(peso, 0) AS Peso,
    ISNULL(Familia, '') AS Familia,
    ISNULL(Area, '') AS Area,
    ISNULL(Clasificacion, '') AS Clasificacion,
    ISNULL(codigo_poliza, '') AS CodigoPoliza,
    ISNULL(Numero_poliza, '') AS NumeroPoliza,
    ISNULL(ubicacion_picking, 0) AS UbicacionPicking
FROM VW_Stock_Res WITH (NOLOCK)
{where}
ORDER BY IdBodega, codigo, lote, fecha_vence, lic_plate, IdStock
OFFSET @p_offset ROWS FETCH NEXT @p_fetch ROWS ONLY;";

            using var conn = await OpenConnectionAsync(ct);
            var total = await conn.ExecuteScalarAsync<int>(
                new CommandDefinition(countSql, parameters, commandType: CommandType.Text, commandTimeout: 120, cancellationToken: ct));
            var rows = await conn.QueryAsync<InventarioExistenciaLoteDto>(
                new CommandDefinition(dataSql, parameters, commandType: CommandType.Text, commandTimeout: 120, cancellationToken: ct));

            return new InventarioPagedResultDto<InventarioExistenciaLoteDto>
            {
                Page = filtro.Page,
                PageSize = filtro.PageSize,
                Total = total,
                Data = rows.AsList()
            };
        }

        public async Task<IReadOnlyList<InventarioResumenDto>> GetResumenAsync(
            InventarioFiltroDto filtro,
            string grupo,
            CancellationToken ct)
        {
            Normalize(filtro);
            EnsureSelectiveFilter(filtro);

            var groupSpec = GetGroupSpec(grupo);
            var where = BuildWhere(filtro, out var parameters);
            parameters.Add("p_top", MaxResumenRows);

            var sql = $@"
SELECT TOP (@p_top)
    '{groupSpec.Name}' AS Grupo,
    {groupSpec.SelectList},
    SUM(ISNULL(CantidadSF, 0)) AS CantidadUMBas,
    SUM(ISNULL(Cantidad, 0)) AS CantidadPresentacion,
    SUM(ISNULL(CantidadReservada, 0)) AS CantidadReservadaUMBas,
    SUM(ISNULL(CantidadSF, 0) - ISNULL(CantidadReservada, 0)) AS DisponibleUMBas,
    SUM(CASE
        WHEN ISNULL(factor, 0) > 0
        THEN ROUND((ISNULL(CantidadSF, 0) - ISNULL(CantidadReservada, 0)) / factor, 6)
        ELSE 0
    END) AS DisponiblePresentacion,
    COUNT(1) AS LineasStock
FROM VW_Stock_Res WITH (NOLOCK)
{where}
GROUP BY {groupSpec.GroupBy}
ORDER BY {groupSpec.OrderBy};";

            using var conn = await OpenConnectionAsync(ct);
            var rows = await conn.QueryAsync<InventarioResumenDto>(
                new CommandDefinition(sql, parameters, commandType: CommandType.Text, commandTimeout: 120, cancellationToken: ct));
            return rows.AsList();
        }

        private async Task<SqlConnection> OpenConnectionAsync(CancellationToken ct)
        {
            var cnStr = _configuration.GetConnectionString("CST");
            if (string.IsNullOrWhiteSpace(cnStr))
            {
                throw new InvalidOperationException("No se encontró la cadena de conexión 'CST'.");
            }

            var conn = new SqlConnection(cnStr);
            await conn.OpenAsync(ct);
            return conn;
        }

        private static void Normalize(InventarioFiltroDto filtro)
        {
            filtro.Page = filtro.Page <= 0 ? 1 : filtro.Page;
            filtro.PageSize = filtro.PageSize <= 0 ? 100 : Math.Min(filtro.PageSize, MaxPageSize);
            filtro.Codigo = NormalizeText(filtro.Codigo);
            filtro.Lote = NormalizeText(filtro.Lote);
            filtro.Licencia = NormalizeText(filtro.Licencia);
            filtro.Ubicacion = NormalizeText(filtro.Ubicacion);
            filtro.Familia = NormalizeText(filtro.Familia);
            filtro.Area = NormalizeText(filtro.Area);
            filtro.Clasificacion = NormalizeText(filtro.Clasificacion);
        }

        private static string? NormalizeText(string? value)
        {
            return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
        }

        private static void EnsureSelectiveFilter(InventarioFiltroDto filtro)
        {
            if (filtro.IdBodega.HasValue ||
                filtro.IdPropietarioBodega.HasValue ||
                filtro.IdProductoBodega.HasValue ||
                filtro.IdProducto.HasValue ||
                filtro.IdUbicacion.HasValue ||
                filtro.IdPropietario.HasValue ||
                filtro.IdProductoEstado.HasValue ||
                filtro.Codigo is not null ||
                filtro.Lote is not null ||
                filtro.Licencia is not null)
            {
                return;
            }

            throw new ArgumentException("Enviar al menos un filtro selectivo: idBodega, propietario, producto, ubicación, estado, código, lote o licencia.");
        }

        private static string BuildWhere(InventarioFiltroDto filtro, out DynamicParameters parameters)
        {
            parameters = new DynamicParameters();
            var where = new StringBuilder("WHERE 1 = 1");

            AddEquals(where, parameters, "IdBodega", "p_idBodega", filtro.IdBodega);
            AddEquals(where, parameters, "IdPropietario", "p_idPropietario", filtro.IdPropietario);
            AddEquals(where, parameters, "IdPropietarioBodega", "p_idPropietarioBodega", filtro.IdPropietarioBodega);
            AddEquals(where, parameters, "IdProducto", "p_idProducto", filtro.IdProducto);
            AddEquals(where, parameters, "IdProductoBodega", "p_idProductoBodega", filtro.IdProductoBodega);
            AddEquals(where, parameters, "IdUbicacion", "p_idUbicacion", filtro.IdUbicacion);
            AddEquals(where, parameters, "IdProductoEstado", "p_idProductoEstado", filtro.IdProductoEstado);
            AddLike(where, parameters, "codigo", "p_codigo", filtro.Codigo);
            AddLike(where, parameters, "lote", "p_lote", filtro.Lote);
            AddLike(where, parameters, "lic_plate", "p_licencia", filtro.Licencia);
            AddLike(where, parameters, "Nombre_Completo", "p_ubicacion", filtro.Ubicacion);
            AddLike(where, parameters, "Familia", "p_familia", filtro.Familia);
            AddLike(where, parameters, "Area", "p_area", filtro.Area);
            AddLike(where, parameters, "Clasificacion", "p_clasificacion", filtro.Clasificacion);

            if (filtro.VenceDesde.HasValue)
            {
                where.AppendLine(" AND fecha_vence >= @p_venceDesde");
                parameters.Add("p_venceDesde", filtro.VenceDesde.Value.Date);
            }

            if (filtro.VenceHasta.HasValue)
            {
                where.AppendLine(" AND fecha_vence < DATEADD(day, 1, @p_venceHasta)");
                parameters.Add("p_venceHasta", filtro.VenceHasta.Value.Date);
            }

            if (filtro.SoloDisponible)
            {
                where.AppendLine(" AND (ISNULL(CantidadSF, 0) - ISNULL(CantidadReservada, 0)) > 0");
            }

            return where.ToString();
        }

        private static void AddEquals(StringBuilder where, DynamicParameters parameters, string column, string parameter, int? value)
        {
            if (!value.HasValue || value.Value <= 0)
            {
                return;
            }

            where.AppendLine($" AND {column} = @{parameter}");
            parameters.Add(parameter, value.Value);
        }

        private static void AddLike(StringBuilder where, DynamicParameters parameters, string column, string parameter, string? value)
        {
            if (value is null)
            {
                return;
            }

            where.AppendLine($" AND {column} LIKE '%' + @{parameter} + '%'");
            parameters.Add(parameter, value);
        }

        private static GroupSpec GetGroupSpec(string? grupo)
        {
            return (grupo ?? "producto-lote").Trim().ToLowerInvariant() switch
            {
                "producto" => new GroupSpec(
                    "producto",
                    "IdBodega, Bodega, IdPropietario, Propietario, IdProducto, IdProductoBodega, codigo AS Codigo, nombre AS Producto, UnidadMedida, Presentacion",
                    "IdBodega, Bodega, IdPropietario, Propietario, IdProducto, IdProductoBodega, codigo, nombre, UnidadMedida, Presentacion",
                    "codigo"),
                "producto-lote" => new GroupSpec(
                    "producto-lote",
                    "IdBodega, Bodega, IdPropietario, Propietario, IdProducto, IdProductoBodega, codigo AS Codigo, nombre AS Producto, lote AS Lote, UnidadMedida, Presentacion",
                    "IdBodega, Bodega, IdPropietario, Propietario, IdProducto, IdProductoBodega, codigo, nombre, lote, UnidadMedida, Presentacion",
                    "codigo, lote"),
                "producto-ubicacion" => new GroupSpec(
                    "producto-ubicacion",
                    "IdBodega, Bodega, IdPropietario, Propietario, IdProducto, IdProductoBodega, codigo AS Codigo, nombre AS Producto, IdUbicacion, Nombre_Completo AS Ubicacion, UnidadMedida, Presentacion",
                    "IdBodega, Bodega, IdPropietario, Propietario, IdProducto, IdProductoBodega, codigo, nombre, IdUbicacion, Nombre_Completo, UnidadMedida, Presentacion",
                    "codigo, Nombre_Completo"),
                "producto-lote-ubicacion" => new GroupSpec(
                    "producto-lote-ubicacion",
                    "IdBodega, Bodega, IdPropietario, Propietario, IdProducto, IdProductoBodega, codigo AS Codigo, nombre AS Producto, lote AS Lote, IdUbicacion, Nombre_Completo AS Ubicacion, UnidadMedida, Presentacion",
                    "IdBodega, Bodega, IdPropietario, Propietario, IdProducto, IdProductoBodega, codigo, nombre, lote, IdUbicacion, Nombre_Completo, UnidadMedida, Presentacion",
                    "codigo, lote, Nombre_Completo"),
                "licencia" => new GroupSpec(
                    "licencia",
                    "IdBodega, Bodega, IdPropietario, Propietario, lic_plate AS Licencia, IdUbicacion, Nombre_Completo AS Ubicacion",
                    "IdBodega, Bodega, IdPropietario, Propietario, lic_plate, IdUbicacion, Nombre_Completo",
                    "lic_plate"),
                "estado" => new GroupSpec(
                    "estado",
                    "IdBodega, Bodega, IdPropietario, Propietario, NomEstado AS Estado",
                    "IdBodega, Bodega, IdPropietario, Propietario, NomEstado",
                    "NomEstado"),
                "familia" => new GroupSpec(
                    "familia",
                    "IdBodega, Bodega, IdPropietario, Propietario, Familia",
                    "IdBodega, Bodega, IdPropietario, Propietario, Familia",
                    "Familia"),
                "area" => new GroupSpec(
                    "area",
                    "IdBodega, Bodega, IdPropietario, Propietario, Area",
                    "IdBodega, Bodega, IdPropietario, Propietario, Area",
                    "Area"),
                "clasificacion" => new GroupSpec(
                    "clasificacion",
                    "IdBodega, Bodega, IdPropietario, Propietario, Clasificacion",
                    "IdBodega, Bodega, IdPropietario, Propietario, Clasificacion",
                    "Clasificacion"),
                _ => throw new ArgumentException("Grupo no soportado. Valores: producto, producto-lote, producto-ubicacion, producto-lote-ubicacion, licencia, estado, familia, area, clasificacion.")
            };
        }

        private sealed record GroupSpec(string Name, string SelectList, string GroupBy, string OrderBy);
    }
}
