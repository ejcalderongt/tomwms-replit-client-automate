using Microsoft.Data.SqlClient;
using System.Data;
using WMS.EntityCore.Dtos.KPI;
using Dapper;

namespace WMSWebAPI.Services.KPI
{
    public class KpiReportService : IKpiReportService
    {
        private readonly IConfiguration _configuration;

        public KpiReportService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<List<KpiPickingRowDto>> GetPickingAsync(DateTime? from, DateTime? to, CancellationToken ct)
        {
            try
            {
                // Ajusta el nombre de tu connection string en appsettings.json
                var cnStr = _configuration.GetConnectionString("CST");
                if (string.IsNullOrWhiteSpace(cnStr))
                    throw new Exception("No se encontró la cadena de conexión 'CST'.");

                var sql = @"
                        -- PICKING KPI
                        SELECT
                        ISNULL(fo.Fecha_Hora_Inicio, DATEADD(day,1, m.Fecha_Pedido)) AS Fecha_Hora_Inicio,
                        ISNULL(fo.Fecha_Hora_Fin, DATEADD(day, 1,m.Fecha_Pedido)) AS Fecha_Hora_Fin,
                        ISNULL(a.fecha_picking, DATEADD(day,1, m.Fecha_Pedido)) AS Fecha_Por_Línea,
                        o.descripcion AS Tipo_Documento_Pedido,
                        ISNULL(e.nombre, 'ND') AS Tipo,
                        f.codigo AS Código_Departamento,
                        f.nombre AS Descripción_Departamento,
                        g.codigo AS Código_Categoría,
                        g.nombretipoproducto AS Descripción_Categoría,
                        d.codigo AS Código_Producto, 
                        d.nombre AS Nombre_Producto,
                        t.cantidad AS Cantidad_Solicitada,
                        ISNULL(a.cantidad_recibida, 0) AS Cantidad_Recibida,
                        ISNULL(h.nombre, '') AS Nombre_Estado_Producto,
                        ROUND((t.cantidad - ISNULL(a.cantidad_recibida, 0)),2) AS Cantidad_Devolución_Picking, 
                        ISNULL(q.nombre, '') AS Nombre_Presentación_MPQ,
                        CASE 
                            WHEN p.IdPresentacion > 0 AND q.factor > 0
                            THEN ROUND(ISNULL(a.cantidad_recibida, 0) / q.factor, 2) 
                            ELSE 0 
                        END AS Cantidad_Pickeadas_Cajas,
                        ISNULL(j.IdRecepcionEnc, 0) AS Id_Recepción,
                        ISNULL(b.IdPickingEnc, 0) AS Número_Picking,
                        ISNULL(a.fecha_vence, '19000101') AS Fecha_Vence,
                        ISNULL(a.lic_plate, '') AS Lic_Plate,
                        ISNULL(l.codigo, 'Operador BOF') AS Código_Operador,
                        ISNULL(l.nombres, 'Operador BOF') + ' ' + ISNULL(l.apellidos, '') AS Descripción_Operador,
                        ISNULL(n.codigo, '') AS Código_Comprador,
                        ISNULL(n.nombre_comercial, '') AS Descripción_Comprador,
                        m.Referencia_Documento_Ingreso_Bodega_Destino AS Solicitud_SAP,
	                    ISNULL(w.codigo, '') AS Código_Cliente,
                        ISNULL(w.nombre_comercial, '') AS Descripción_Cliente
                    FROM trans_pe_enc m WITH (NOLOCK)
                    INNER JOIN trans_pe_det t WITH (NOLOCK) ON t.IdPedidoEnc = m.IdPedidoEnc
                    INNER JOIN trans_pe_tipo o  WITH (NOLOCK) ON o.IdTipoPedido = m.IdTipoPedido 
                    INNER JOIN producto_bodega c  WITH (NOLOCK) ON c.IdProductoBodega = t.IdProductoBodega
                    INNER JOIN producto d  WITH (NOLOCK) ON d.IdProducto = c.IdProducto
                    LEFT JOIN producto_familia e  WITH (NOLOCK) ON e.IdFamilia = d.IdFamilia
                    LEFT JOIN producto_clasificacion f WITH (NOLOCK)  ON f.IdClasificacion = d.IdClasificacion
                    LEFT JOIN producto_tipo g  WITH (NOLOCK) ON g.IdTipoProducto = d.IdTipoProducto
                    LEFT JOIN trans_picking_ubic a WITH (NOLOCK)  ON t.IdPedidoDet = a.IdPedidoDet AND t.IdProductoBodega = a.IdProductoBodega
                    LEFT JOIN trans_picking_enc b WITH (NOLOCK)  ON b.IdPickingEnc = a.IdPickingEnc
                    LEFT JOIN producto_estado h WITH (NOLOCK)  ON h.IdEstado = a.IdProductoEstado
                    LEFT JOIN (
                        SELECT 
                            IdOperadorBodega_Pickeo,
                            IdPickingEnc,
                            MIN(fecha_picking) AS Fecha_Hora_Inicio,
                            MAX(fecha_picking) AS Fecha_Hora_Fin 
                        FROM trans_picking_ubic WITH (NOLOCK) 
                        WHERE no_encontrado = 0 AND dañado_picking = 0
                        GROUP BY IdOperadorBodega_Pickeo, IdPickingEnc
                    ) fo ON fo.IdOperadorBodega_Pickeo = a.IdOperadorBodega_Pickeo AND fo.IdPickingEnc = a.IdPickingEnc
                    LEFT JOIN cliente n  WITH (NOLOCK) ON n.codigo COLLATE Modern_Spanish_CI_AS = m.bodega_destino COLLATE Modern_Spanish_CI_AS
                    LEFT JOIN cliente w  WITH (NOLOCK) ON w.IdCliente = m.IdCliente
                    LEFT JOIN stock j  WITH (NOLOCK) ON j.IdStock = a.IdStock
                    LEFT JOIN operador_bodega k  WITH (NOLOCK) ON k.IdOperadorBodega = a.IdOperadorBodega_Pickeo
                    LEFT JOIN operador l WITH (NOLOCK)  ON l.IdOperador = k.IdOperador
                    LEFT JOIN (
                        SELECT MAX(IdPresentacion) IdPresentacion, IdProducto 
	                    FROM producto_presentacion  WITH (NOLOCK) GROUP BY IdProducto) p ON c.IdProducto = p.IdProducto AND 
	                                                                                        d.IdProducto = p.IdProducto
                    LEFT JOIN producto_presentacion q  WITH (NOLOCK) ON p.IdPresentacion = q.IdPresentacion  
                    WHERE a.dañado_picking = 0 
                    AND a.no_encontrado = 0 
                    AND a.dañado_verificacion = 0 AND m.estado <> 'Anulado'
                    AND m.ubicacion <> 'TMP'
                    ORDER BY b.IdPickingEnc";

                using var cn = new SqlConnection(cnStr);
                await cn.OpenAsync(ct);

                var rows = await cn.QueryAsync<KpiPickingRowDto>(
                    new CommandDefinition(
                        sql,
                        new { from, to },
                        commandType: CommandType.Text,
                        cancellationToken: ct,
                        commandTimeout: 120
                    )
                );

                return rows.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener KPI de Picking → " + ex.Message, ex);
            }
        }

        public async Task<List<KpiVerificacionRowDto>> GetVerificacionAsync(DateTime? from, DateTime? to, CancellationToken ct)
        {
            try
            {
                var cnStr = _configuration.GetConnectionString("CST");
                if (string.IsNullOrWhiteSpace(cnStr))
                    throw new Exception("No se encontró la cadena de conexión 'CST'.");

                var sql = @"
                        -- VERIFICACION KPI
                        SELECT 
                            ISNULL(fo.Fecha_Hora_Inicio, DATEADD(day,1, m.Fecha_Pedido)) AS Fecha_Hora_Inicio,
                            ISNULL(fo.Fecha_Hora_Fin, DATEADD(day, 1,m.Fecha_Pedido)) AS Fecha_Hora_Fin,
                            ISNULL(a.fecha_verificado, DATEADD(day,1, m.Fecha_Pedido)) AS Fecha_Por_Línea,
	                        o.descripcion as Tipo_Documento_Pedido,
	                        ISNULL(e.nombre, 'ND') as Tipo,
	                        f.codigo as Código_Departamento,
	                        f.nombre as Descripción_Departamento,
	                        g.codigo as Código_Categoría,
	                        g.nombretipoproducto as Descripción_Categoría,
	                        d.codigo as Código_Producto, 
	                        d.nombre as Nombre_Producto,
	                        a.cantidad_verificada as Cantidad_Verificada,
	                        t.Cantidad as Cantidad_Solicita_Ver,
	                        h.nombre as Nombre_Producto_Estado,
	                        t.Cantidad - a.cantidad_verificada as Cantidad_Merma_Ver,
	                        ISNULL(q.nombre, '') as Nombre_Presentación_MPQ,
	                        CASE WHEN p.IdPresentacion > 0 AND q.factor > 0
		                        THEN ROUND(a.cantidad_verificada / q.factor, 2) 
		                        ELSE 0 
	                        END Cantidad_Verificada_Cajas,
	                        b.IdPickingEnc as Id_Picking,
	                        a.fecha_vence as Fecha_Vence,
	                        a.lic_plate as Lic_Plate,
	                        ISNULL(l.codigo, '') as Código_Operador,
	                        ISNULL(l.nombres, '') + ' ' + ISNULL(l.apellidos, '') AS Descripción_Operador,
	                        ISNULL(n.codigo, '') as Código_Comprador,
	                        ISNULL(n.nombre_comercial, '') as Nombre_Comprador,
	                        m.Referencia_Documento_Ingreso_Bodega_Destino as Solicitud_SAP
                        FROM trans_pe_enc m WITH (NOLOCK)
                        INNER JOIN trans_pe_det t WITH (NOLOCK) ON t.IdPedidoEnc = m.IdPedidoEnc
                        INNER JOIN trans_pe_tipo o  WITH (NOLOCK) ON o.IdTipoPedido = m.IdTipoPedido 
                        INNER JOIN producto_bodega c  WITH (NOLOCK) ON c.IdProductoBodega = t.IdProductoBodega
                        INNER JOIN producto d  WITH (NOLOCK) ON d.IdProducto = c.IdProducto
                        LEFT JOIN producto_familia e WITH (NOLOCK)  ON e.IdFamilia = d.IdFamilia
                        LEFT JOIN producto_clasificacion f WITH (NOLOCK)  ON f.IdClasificacion = d.IdClasificacion
                        LEFT JOIN producto_tipo g WITH (NOLOCK)  ON g.IdTipoProducto = d.IdTipoProducto
                        LEFT JOIN trans_picking_ubic a WITH (NOLOCK) ON t.IdPedidoDet = a.IdPedidoDet AND t.IdProductoBodega = a.IdProductoBodega
                        INNER JOIN trans_picking_enc b WITH (NOLOCK)  ON b.IdPickingEnc = a.IdPickingEnc
                        LEFT JOIN producto_estado h WITH (NOLOCK)  ON h.IdEstado = a.IdProductoEstado
                        LEFT JOIN (
	                        SELECT 
		                        IdOperadorBodega_Verifico,
		                        IdPickingEnc,
		                        MIN(fecha_verificado) Fecha_Hora_Inicio,
		                        MAX(fecha_verificado) Fecha_Hora_Fin 
	                        FROM trans_picking_ubic WITH (NOLOCK) 
	                        WHERE no_encontrado = 0 AND dañado_picking = 0
	                        GROUP BY IdOperadorBodega_Verifico, IdPickingEnc
                        ) fo ON fo.IdOperadorBodega_Verifico = a.IdOperadorBodega_Verifico AND fo.IdPickingEnc = b.IdPickingEnc 
                        LEFT JOIN producto_presentacion i WITH (NOLOCK)  ON i.IdPresentacion = a.IdPresentacion 
                        LEFT JOIN operador_bodega k  WITH (NOLOCK) ON k.IdOperadorBodega = a.IdOperadorBodega_Verifico
                        LEFT JOIN operador l  WITH (NOLOCK) ON l.IdOperador = k.IdOperador
                        LEFT JOIN cliente n  WITH (NOLOCK) ON n.codigo COLLATE Modern_Spanish_CI_AS = m.bodega_destino COLLATE Modern_Spanish_CI_AS
                        LEFT JOIN (
                            SELECT MAX(IdPresentacion) IdPresentacion, IdProducto 
                            FROM producto_presentacion WITH (NOLOCK)  
                            GROUP BY IdProducto
                        ) p ON c.IdProducto = p.IdProducto AND d.IdProducto = p.IdProducto 
                        LEFT JOIN producto_presentacion q WITH (NOLOCK) ON p.IdPresentacion = q.IdPresentacion
                        WHERE a.dañado_picking = 0 
                        AND a.no_encontrado = 0 
                        AND a.dañado_verificacion = 0 
                        AND m.estado <> 'Anulado'
                        AND m.ubicacion <> 'TMP'
                        AND (@from IS NULL OR m.Fecha_Pedido >= @from)
                        AND (@to   IS NULL OR m.Fecha_Pedido <  DATEADD(day, 1, @to))
                        ORDER BY b.IdPickingEnc;";

                using var cn = new SqlConnection(cnStr);
                await cn.OpenAsync(ct);

                var rows = await cn.QueryAsync<KpiVerificacionRowDto>(
                    new CommandDefinition(
                        sql,
                        new { from, to },
                        commandType: CommandType.Text,
                        cancellationToken: ct,
                        commandTimeout: 120
                    )
                );

                return rows.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener KPI de Verificación → " + ex.Message, ex);
            }
        }

        public async Task<List<KpiRecepcionRowDto>> GetRecepcionAsync(DateTime? from, DateTime? to, CancellationToken ct)
        {
            try
            {
                var cnStr = _configuration.GetConnectionString("CST");
                if (string.IsNullOrWhiteSpace(cnStr))
                    throw new Exception("No se encontró la cadena de conexión 'CST'.");

                var sql = @"
                        -- RECEPCION KPI
                        SELECT  
                            ISNULL(s.fecha_inic, DATEADD(day,1, R.Fecha_OC)) Fecha_Hora_Inicio,
                            ISNULL(s.fecha_fin,  DATEADD(day,1, R.Fecha_OC)) Fecha_Hora_Fin,
                            ISNULL(R.Fecha_por_Linea, DATEADD(day,1, R.Fecha_OC)) AS Fecha_por_Linea,
                            R.Tipo_Local_Importación,
                            R.Código_Proveedor,
                            R.Descripción_Proveedor,
                            R.Tipo,
                            R.Código_Departamento,
                            R.Descripcion_Departamento,
                            R.Código_Categoria,
                            R.Descripcion_Categoría,
                            R.Código_Producto,
                            R.Nombre_Producto,
                            ISNULL(R.Cantidad_Recibida,0) Cantidad_Recibida,
                            R.Cantidad_Solicita_OC,
                            ISNULL(R.Nombre_Producto_Estado,'') Nombre_Producto_Estado,
                            R.Cantidad_Devolucion_OC,
                            R.Nombre_Presentacion_MPQ,
                            ISNULL(R.Cantidad_Recibida_Cajas_OC,0) Cantidad_Recibida_Cajas_OC,
                            ISNULL(MAX(Id_Recepcion),0) Id_Recepcion,
                            MAX(R.Número_de_OC) Número_de_OC,
                            MAX(R.IdOC) IdOC,
                            ISNULL(R.Fecha_Vence,'19000101') Fecha_Vence,
                            ISNULL(R.Lic_Plate,'') Lic_Plate,
                            R.Código_Operador,
                            R.Descripción_Operador,
                            R.Código_Comprador,
                            R.Descripción_Comprador,
                            ISNULL(R.Contenedor,'') Contenedor
                        FROM
                        (
                            SELECT 
                                re_enc.hora_ini_pc Fecha_Hora_Inicio,
                                t.Maxima_Fecha Fecha_Hora_Fin,
                                re_det.fecha_ingreso Fecha_por_Linea,
                                CASE 
                                    WHEN proveedor.codigo like 'PE%' THEN 'Importación' 
                                    ELSE CASE WHEN proveedor.codigo like 'PN%' THEN 'Local' ELSE 'INTERNO WMS' END 
                                END Tipo_Local_Importación,
                                proveedor.codigo Código_Proveedor,
                                proveedor.nombre Descripción_Proveedor,
                                oc_enc.IdProveedorBodega,
                                ISNULL(pf.nombre, 'ND') Tipo,
                                pc.codigo Código_Departamento,
                                pc.nombre Descripcion_Departamento,
                                pt.codigo Código_Categoria,
                                pt.NombreTipoProducto Descripcion_Categoría,
                                oc_det.codigo_producto Código_Producto,
                                pr.nombre Nombre_Producto,
                                re_det.Cantidad_Recibida,
                                oc_det.cantidad Cantidad_Solicita_OC,
                                pe.nombre Nombre_Producto_Estado,
                                (oc_det.cantidad - ISNULL(re_det.cantidad_recibida,0)) Cantidad_Devolucion_OC,
                                ISNULL(pp1.nombre,'ND') Nombre_Presentacion_MPQ,
                                CASE 
                                    WHEN pp.IdPresentacion > 0 AND pp1.factor>0 THEN ROUND(re_det.cantidad_recibida/pp1.factor,2) 
                                    ELSE 0 
                                END Cantidad_Recibida_Cajas_OC,
                                re_enc.IdRecepcionEnc Id_Recepcion,
                                CONCAT(oc_enc.Referencia, ' - ', oc_enc.no_documento) Número_de_OC,
                                oc_enc.IdOrdenCompraEnc IdOC,
                                re_det.Fecha_Vence,
                                re_det.Lic_Plate,
                                ISNULL(op.codigo, IIF(re_det.Cantidad_Recibida>0,'Operado BOF','')) Código_Operador,
                                ISNULL(op.nombres +' '+op.apellidos, IIF(re_det.Cantidad_Recibida>0,'Operado BOF','')) Descripción_Operador,
                                '' Código_Comprador,
                                '' Descripción_Comprador,
                                ISNULL(oc_enc.no_documento_recepcion_erp, '') Contenedor,
                                oc_enc.Fecha_Creacion Fecha_OC
                            FROM trans_oc_enc oc_enc WITH (NOLOCK) 
                            INNER JOIN trans_oc_det oc_det WITH (NOLOCK) ON oc_enc.IdOrdenCompraEnc = oc_det.IdOrdenCompraEnc 
                            INNER JOIN proveedor_bodega pb WITH (NOLOCK) ON oc_enc.IdProveedorBodega=pb.IdAsignacion 
                            INNER JOIN proveedor WITH (NOLOCK) ON pb.IdProveedor= proveedor.IdProveedor 
                            INNER JOIN producto_bodega WITH (NOLOCK) ON oc_det.IdProductoBodega=producto_bodega.IdProductoBodega 
                            INNER JOIN producto pr WITH (NOLOCK) ON producto_bodega.IdProducto= pr.IdProducto 
                            LEFT JOIN producto_familia pf WITH (NOLOCK) ON pr.IdFamilia=pf.IdFamilia 
                            LEFT JOIN producto_clasificacion pc WITH (NOLOCK) ON pr.IdClasificacion=pc.IdClasificacion 
                            LEFT JOIN producto_tipo pt WITH (NOLOCK) ON pr.IdTipoProducto=pt.IdTipoProducto 
                            LEFT JOIN trans_re_det re_det WITH (NOLOCK) ON re_det.No_Linea=oc_det.No_Linea 
                                                                      AND re_det.IdProductoBodega=oc_det.IdProductoBodega 
                                                                      AND re_det.IdOrdenCompraEnc=oc_det.IdOrdenCompraEnc 
                                                                      AND re_det.IdOrdenCompraEnc=oc_enc.IdOrdenCompraEnc  
                            LEFT JOIN trans_re_enc re_enc WITH (NOLOCK) ON re_enc.IdRecepcionEnc=re_det.IdRecepcionEnc 
                            LEFT JOIN (SELECT IdRecepcionEnc, MAX(fecha_ingreso) Maxima_Fecha 
                                       FROM trans_re_det WITH (NOLOCK) 
                                       GROUP BY IdRecepcionEnc) t ON t.IdRecepcionEnc = re_enc.IdRecepcionEnc 
                            LEFT JOIN producto_estado pe WITH (NOLOCK) ON re_det.IdProductoEstado = pe.IdEstado 
                            LEFT JOIN (SELECT MAX(IdPresentacion) IdPresentacion, IdProducto 
                                       FROM producto_presentacion WITH (NOLOCK) 
                                       GROUP BY IdProducto) pp ON producto_bodega.IdProducto = pp.IdProducto AND pr.IdProducto = pp.IdProducto 
                            LEFT JOIN producto_presentacion pp1 WITH (NOLOCK) ON pp.IdPresentacion = pp1.IdPresentacion 
                            LEFT JOIN operador_bodega ob WITH (NOLOCK) ON re_det.IdOperadorBodega=ob.IdOperadorBodega 
                            LEFT JOIN operador op WITH (NOLOCK) ON ob.IdOperador=op.IdOperador
                        ) AS R 
                        LEFT JOIN
                        (
                            SELECT 
                                oc.IdProveedorBodega,
                                MIN(re.hora_ini_pc) fecha_inic, 
                                MAX(t.Maxima_Fecha) fecha_fin
                            FROM trans_oc_enc oc WITH (NOLOCK) 
                            INNER JOIN trans_re_oc ro WITH (NOLOCK) ON oc.IdOrdenCompraEnc = ro.IdOrdenCompraEnc 
                            INNER JOIN trans_re_enc re WITH (NOLOCK) ON re.IdRecepcionEnc = ro.IdRecepcionEnc 
                            LEFT JOIN (SELECT IdRecepcionEnc, MAX(fecha_ingreso) Maxima_Fecha 
                                       FROM trans_re_det WITH (NOLOCK) 
                                       GROUP BY IdRecepcionEnc) t ON t.IdRecepcionEnc = re.IdRecepcionEnc
                            GROUP BY oc.IdProveedorBodega, CONVERT(date,re.hora_ini_pc)
                        ) S 
                            ON S.IdProveedorBodega = R.IdProveedorBodega 
                           AND CONVERT(date, s.fecha_inic) = CONVERT(date, R.Fecha_Hora_Inicio)
                        WHERE
                            (@from IS NULL OR R.Fecha_OC >= @from)
                        AND (@to   IS NULL OR R.Fecha_OC <  DATEADD(day, 1, @to))
                        GROUP BY 
                            s.fecha_inic, s.fecha_fin, 
                            R.Fecha_por_Linea,
                            R.Tipo_Local_Importación,
                            R.Código_Proveedor,
                            R.Descripción_Proveedor,
                            R.Tipo,
                            R.Código_Departamento,
                            R.Descripcion_Departamento,
                            R.Código_Categoria,
                            R.Descripcion_Categoría,
                            R.Código_Producto,
                            R.Nombre_Producto,
                            R.Cantidad_Recibida,
                            R.Cantidad_Solicita_OC,
                            R.Nombre_Producto_Estado,
                            R.Cantidad_Devolucion_OC,
                            R.Nombre_Presentacion_MPQ,
                            R.Cantidad_Recibida_Cajas_OC,
                            R.Fecha_Vence,
                            R.Lic_Plate,
                            R.Código_Operador,
                            R.Descripción_Operador,
                            R.Código_Comprador,
                            R.Descripción_Comprador,
                            R.Contenedor,
                            R.Fecha_OC
                        ORDER BY MAX(R.Id_Recepcion);";

                using var cn = new SqlConnection(cnStr);
                await cn.OpenAsync(ct);

                var rows = await cn.QueryAsync<KpiRecepcionRowDto>(
                    new CommandDefinition(
                        sql,
                        new { from, to },
                        commandType: CommandType.Text,
                        cancellationToken: ct,
                        commandTimeout: 180
                    )
                );

                return rows.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener KPI de Recepción → " + ex.Message, ex);
            }
        }

        public async Task<List<KpiDespachoRowDto>> GetDespachoAsync(DateTime? from, DateTime? to, CancellationToken ct)
        {
            try
            {
                var cnStr = _configuration.GetConnectionString("CST");
                if (string.IsNullOrWhiteSpace(cnStr))
                    throw new Exception("No se encontró la cadena de conexión 'CST'.");

                var sql = @"
                        -- DESPACHO KPI
                        SELECT 
                            ISNULL(b.hora_ini,DATEADD(day,1, c.Fecha_Pedido)) as Fecha_Hora_Inicio,
	                        ISNULL((SELECT MAX(fec_agr) FROM trans_despacho_det WHERE IdDespachoEnc = a.IdDespachoEnc),DATEADD(day,1, c.Fecha_Pedido)) as Fecha_Hora_Fin,
	                        ISNULL(a.fec_agr,DATEADD(day,1, c.Fecha_Pedido)) as Fecha_Por_Linea,
	                        ISNULL(d.Descripcion,'ND') as Tipo_Documento_Pedido,
	                        ISNULL(prov.Cod_Proveedor,'ND') as Código_Proveedor,
	                        ISNULL(pv.nombre,'INTERNO') as Descripción_Proveedor,
	                        ISNULL(n.nombre_comercial, '') as No_Tienda,
	                        ISNULL(g.nombre,'ND') as Tipo,
	                        ISNULL(h.codigo,'ND') as Código_Departamento,
	                        ISNULL(h.nombre,'ND') as Descripción_Departamento,
	                        ISNULL(i.codigo,'ND') as Código_Categoría,
	                        ISNULL(i.nombretipoproducto,'ND') as Descripción_Categoría,
	                        f.codigo as Código_Producto,
	                        f.nombre as Nombre_Producto,
	                        SUM(ISNULL(a.CantidadDespachada,0)) as Cantidad_Despachada,
	                        MAX(ISNULL(t.Cantidad,0)) as Cantidad_Solicitada_Despacho,
	                        ISNULL(a.NombreEstado, '') as Nombre_Producto_Estado,
	                        ROUND(MAX(ISNULL(t.Cantidad,0)) - SUM(ISNULL(a.CantidadDespachada,0)), 2) as Cantidad_Merma_Despacho,
	                        SUM(ISNULL(o.cantidad_solicitada,0)) as Cantidad_Reservada,
	                        SUM(ISNULL(dp.cant,0)) as Cantidad_Dañada_Picking,
	                        SUM(ISNULL(dv.cant,0)) as Cantidad_Dañada_Verificacion,
	                        SUM(ISNULL(ne.cant,0)) as Cantidad_No_Encontrada,
	                        ISNULL(m.nombre, '') as Nombre_Presentación_MPQ,
	                        ISNULL(CASE WHEN l.IdPresentacion > 0 AND m.factor > 0
		                           THEN ROUND(SUM(a.CantidadDespachada) / m.factor, 2) 
		                           ELSE 0 
	                               END,0) Cantidad_Despacho_Cajas,
	                        ISNULL(b.IdDespachoEnc,0) as Id_Despacho,
	                        ISNULL(c.Referencia_Documento_Ingreso_Bodega_Destino,'') as Orden_traslado,
	                        ISNULL(o.fecha_vence,'19000101') as Fecha_Vence,
	                        ISNULL(o.lic_plate,'') as Lic_Plate,
	                        ISNULL(q.no_linea, '') as Licencia_Despacho,
	                        ISNULL(p.IdUsuario, '') as Código_Usuario,
	                        ISNULL(p.nombres, '')  as Descripción_Usuario
                        FROM  
                        trans_pe_enc c 
                        INNER JOIN trans_pe_det t WITH (NOLOCK)  ON t.IdPedidoEnc = c.IdPedidoEnc 
                        INNER JOIN trans_pe_tipo d WITH (NOLOCK)  ON d.IdTipoPedido = c.IdTipoPedido
                        INNER JOIN producto_bodega e WITH (NOLOCK)  ON e.IdProductoBodega = t.IdProductoBodega
                        INNER JOIN producto f WITH (NOLOCK)  ON f.IdProducto = e.IdProducto
                        LEFT JOIN producto_clasificacion h WITH (NOLOCK)  ON h.IdClasificacion = f.IdClasificacion
                        LEFT JOIN producto_tipo i WITH (NOLOCK)  ON i.IdTipoProducto = f.IdTipoProducto
                        LEFT JOIN trans_despacho_det a WITH (NOLOCK)  ON c.IdPedidoEnc = a.IdPedidoEnc AND t.IdPedidoDet = a.IdPedidoDet
                        LEFT JOIN trans_despacho_enc b WITH (NOLOCK)  ON b.IdDespachoEnc = a.IdDespachoEnc
                        LEFT JOIN trans_picking_ubic o WITH (NOLOCK)  ON o.IdPickingUbic = a.IdPickingUbic and o.IdPedidoDet = a.IdPedidoDet AND 
                                                                         o.dañado_picking = 0 and o.dañado_verificacion = 0 and o.no_encontrado = 0
                        LEFT JOIN (SELECT w.IdProductoBodega, w.IdPedidoDet, w.IdPickingDet, IdPedidoEnc, 
                                          w.IdPickingEnc, w.IdPickingUbic, SUM(w.cantidad_solicitada) cant
                                   FROM trans_picking_ubic w WITH(NOLOCK) 
                                   WHERE w.dañado_picking = 1 
		                           GROUP BY w.IdProductoBodega, w.IdPedidoDet, w.IdPickingDet, IdPedidoEnc, 
                                          w.IdPickingEnc, w.IdPickingUbic) dp ON dp.IdPedidoEnc = a.IdPedidoEnc and dp.IdPedidoDet = a.IdPedidoDet 
				                          AND dp.IdPickingDet = o.IdPickingDet
                        LEFT JOIN (SELECT w.IdProductoBodega, w.IdPedidoDet, w.IdPickingDet, IdPedidoEnc, 
                                          w.IdPickingEnc, w.IdPickingUbic, SUM(w.cantidad_solicitada) cant
                                   FROM trans_picking_ubic w WITH(NOLOCK) 
                                   WHERE w.dañado_verificacion = 1  
		                           GROUP BY w.IdProductoBodega, w.IdPedidoDet, w.IdPickingDet, IdPedidoEnc, 
                                          w.IdPickingEnc, w.IdPickingUbic) dv ON dv.IdPedidoEnc = a.IdPedidoEnc and dv.IdPedidoDet = a.IdPedidoDet  
				                          AND dv.IdPickingDet = o.IdPickingDet
                        LEFT JOIN (SELECT distinct w.IdProductoBodega, w.IdPedidoDet, w.IdPickingDet, IdPedidoEnc, 
                                          w.IdPickingEnc, w.IdPickingUbic, SUM(w.cantidad_solicitada) cant
                                   FROM trans_picking_ubic w WITH(NOLOCK) 
                                   WHERE w.no_encontrado = 1  
		                           GROUP BY w.IdProductoBodega, w.IdPedidoDet, w.IdPickingDet, IdPedidoEnc, 
                                          w.IdPickingEnc, w.IdPickingUbic) ne ON ne.IdPedidoEnc = a.IdPedidoEnc and ne.IdPedidoDet = a.IdPedidoDet 
				                          AND ne.IdPickingDet = o.IdPickingDet
                        LEFT JOIN usuario p WITH (NOLOCK)  ON p.IdUsuario = a.user_agr
                        LEFT JOIN producto_familia g WITH (NOLOCK)  ON g.IdFamilia = f.IdFamilia
                        LEFT JOIN (SELECT MAX(IdPresentacion) IdPresentacion, IdProducto 
                                   FROM producto_presentacion  WITH (NOLOCK) 
		                           GROUP BY IdProducto) l ON e.IdProducto = l.IdProducto AND f.IdProducto = l.IdProducto 
                        LEFT JOIN producto_presentacion m  WITH (NOLOCK) ON l.IdPresentacion = m.IdPresentacion
                        LEFT JOIN cliente n  WITH (NOLOCK) ON n.codigo COLLATE Modern_Spanish_CI_AS = c.bodega_destino COLLATE Modern_Spanish_CI_AS
                        LEFT JOIN (SELECT DISTINCT no_linea, lic_plate, idproductobodega,iddespachoenc 
                                   FROM trans_packing_enc WITH (NOLOCK)) q ON q.iddespachoenc = a.IdDespachoEnc AND 
                                                                              q.lic_plate = o.lic_plate AND  
                                                                              q.idproductobodega = a.IdProductoBodega 
                        LEFT JOIN (SELECT d.IdProductoBodega, max(v.codigo) Cod_Proveedor
			                        FROM trans_oc_enc e  WITH (NOLOCK) 
			                             INNER JOIN trans_oc_det d  WITH (NOLOCK) ON e.IdOrdenCompraEnc = d.IdOrdenCompraEnc
		                                 INNER JOIN producto_bodega pb  WITH (NOLOCK) ON pb.IdProductoBodega = d.IdProductoBodega
				                         INNER JOIN proveedor_bodega bp  WITH (NOLOCK) ON bp.IdAsignacion = e.IdProveedorBodega
				                         INNER JOIN proveedor v WITH (NOLOCK) ON v.IdProveedor = bp.IdProveedor
			                        WHERE v.codigo NOT IN ('05','BG-9')
			                        GROUP BY d.IdProductoBodega) prov ON t.IdProductoBodega = prov.IdProductoBodega
                        LEFT JOIN proveedor pv  WITH (NOLOCK) ON pv.codigo = prov.Cod_Proveedor
                        WHERE c.estado <> 'Anulado' AND c.ubicacion <> 'TMP'
                          AND (@from IS NULL OR c.Fecha_Pedido >= @from)
                          AND (@to   IS NULL OR c.Fecha_Pedido <  DATEADD(day, 1, @to))
                        GROUP BY  
                            b.hora_ini,
	                        a.fec_agr,
	                        d.Descripcion,
	                        ISNULL(prov.Cod_Proveedor,'ND'),
	                        ISNULL(pv.nombre,'INTERNO'),
	                        ISNULL(n.nombre_comercial, ''),
	                        ISNULL(g.nombre,'ND'),
	                        ISNULL(h.codigo,'ND'),
	                        ISNULL(h.nombre,'ND'),
	                        ISNULL(i.codigo,'ND'),
	                        ISNULL(i.nombretipoproducto,'ND'),
	                        f.codigo,
	                        f.nombre,
	                        ISNULL(a.NombreEstado, ''),
	                        ISNULL(m.nombre, ''),	
	                        ISNULL(b.IdDespachoEnc,0),
	                        ISNULL(c.Referencia_Documento_Ingreso_Bodega_Destino,''),
	                        ISNULL(o.fecha_vence,'19000101'),
	                        ISNULL(o.lic_plate,''),
	                        ISNULL(q.no_linea, ''),
	                        ISNULL(p.IdUsuario, ''),
	                        ISNULL(p.nombres, ''),
                            a.IdDespachoEnc, l.IdPresentacion, m.factor,
	                        t.IdPedidoDet,
	                        b.IdDespachoEnc, o.lic_plate, c.Fecha_Pedido
                        ORDER BY b.IdDespachoEnc, f.codigo, o.lic_plate, ISNULL(q.no_linea, '');";

                using var cn = new SqlConnection(cnStr);
                await cn.OpenAsync(ct);

                var rows = await cn.QueryAsync<KpiDespachoRowDto>(
                    new CommandDefinition(
                        sql,
                        new { from, to },
                        commandType: CommandType.Text,
                        cancellationToken: ct,
                        commandTimeout: 180
                    )
                );

                return rows.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener KPI de Despacho → " + ex.Message, ex);
            }
        }

        public async Task<List<KpiTendenciaDespachoRowDto>> GetTendenciaDespachoProductoFamiliaAsync(DateTime? from,DateTime? to,string? gran,int? top,CancellationToken ct)
        {
            try
            {
                var cnStr = _configuration.GetConnectionString("CST");
                if (string.IsNullOrWhiteSpace(cnStr))
                    throw new Exception("No se encontró la cadena de conexión 'CST'.");

                // Normalización de parámetros
                var g = string.IsNullOrWhiteSpace(gran) ? "D" : gran.Trim().ToUpperInvariant();
                if (g is not ("D" or "W" or "M")) g = "D";

                var take = (top.HasValue && top.Value > 0) ? top.Value : (int?)null;

                var sql = @"
                            DECLARE @gran char(1) = @p_gran;

                            ;WITH Base AS (
                                SELECT
                                    CAST(a.fec_agr AS datetime) AS FechaBase,
                                    f.codigo AS Codigo_Producto,
                                    f.nombre AS Nombre_Producto,
                                    ISNULL(g.nombre, 'ND') AS Familia,
                                    ISNULL(a.CantidadDespachada, 0) AS Cantidad_Despachada
                                FROM trans_despacho_det a WITH (NOLOCK)
                                INNER JOIN trans_pe_enc c      WITH (NOLOCK) ON c.IdPedidoEnc = a.IdPedidoEnc
                                INNER JOIN trans_pe_det t      WITH (NOLOCK) ON t.IdPedidoEnc = c.IdPedidoEnc AND t.IdPedidoDet = a.IdPedidoDet
                                INNER JOIN producto_bodega e   WITH (NOLOCK) ON e.IdProductoBodega = t.IdProductoBodega
                                INNER JOIN producto f          WITH (NOLOCK) ON f.IdProducto = e.IdProducto
                                LEFT  JOIN producto_familia g  WITH (NOLOCK) ON g.IdFamilia = f.IdFamilia
                                WHERE c.estado <> 'Anulado'
                                  AND c.ubicacion <> 'TMP'
                                  AND a.fec_agr IS NOT NULL
                                  AND ISNULL(a.CantidadDespachada,0) > 0
                                  AND (@p_from IS NULL OR CAST(a.fec_agr AS date) >= @p_from)
                                  AND (@p_to   IS NULL OR CAST(a.fec_agr AS date) <  DATEADD(day, 1, @p_to))
                            ),
                            Bucket AS (
                                SELECT
                                    CASE 
                                        WHEN @gran = 'D' THEN CONVERT(date, FechaBase)
                                        WHEN @gran = 'W' THEN CONVERT(date, DATEADD(week, DATEDIFF(week, 0, FechaBase), 0))
                                        WHEN @gran = 'M' THEN CONVERT(date, DATEFROMPARTS(YEAR(FechaBase), MONTH(FechaBase), 1))
                                        ELSE CONVERT(date, FechaBase)
                                    END AS Periodo,
                                    Codigo_Producto,
                                    Nombre_Producto,
                                    Familia,
                                    Cantidad_Despachada
                                FROM Base
                            ),
                            AggProducto AS (
                                SELECT
                                    Periodo,
                                    'PRODUCTO' AS Nivel,
                                    Codigo_Producto,
                                    Nombre_Producto,
                                    Familia,
                                    SUM(Cantidad_Despachada) AS Cantidad_Despachada
                                FROM Bucket
                                GROUP BY Periodo, Codigo_Producto, Nombre_Producto, Familia
                            ),
                            AggFamilia AS (
                                SELECT
                                    Periodo,
                                    'FAMILIA' AS Nivel,
                                    CAST(NULL AS varchar(50)) AS Codigo_Producto,
                                    CAST(NULL AS varchar(200)) AS Nombre_Producto,
                                    Familia,
                                    SUM(Cantidad_Despachada) AS Cantidad_Despachada
                                FROM Bucket
                                GROUP BY Periodo, Familia
                            ),
                            UnionAgg AS (
                                SELECT * FROM AggProducto
                                UNION ALL
                                SELECT * FROM AggFamilia
                            ),
                            Ranked AS (
                                SELECT
                                    Periodo,
                                    Nivel,
                                    Codigo_Producto,
                                    Nombre_Producto,
                                    Familia,
                                    Cantidad_Despachada,
                                    ROW_NUMBER() OVER (
                                        PARTITION BY Periodo, Nivel
                                        ORDER BY Cantidad_Despachada DESC, ISNULL(Codigo_Producto,'') ASC, Familia ASC
                                    ) AS rn
                                FROM UnionAgg
                            )
                            SELECT
                                Periodo,
                                Nivel,
                                Codigo_Producto,
                                Nombre_Producto,
                                Familia,
                                Cantidad_Despachada
                            FROM Ranked
                            WHERE (@p_top IS NULL OR rn <= @p_top)
                            ORDER BY Periodo, Nivel, Cantidad_Despachada DESC;
                            ";

                using var cn = new SqlConnection(cnStr);
                await cn.OpenAsync(ct);

                var rows = await cn.QueryAsync<KpiTendenciaDespachoRowDto>(
                    new CommandDefinition(
                        sql,
                        new
                        {
                            p_from = from,
                            p_to = to,
                            p_gran = g,
                            p_top = take
                        },
                        commandType: CommandType.Text,
                        cancellationToken: ct,
                        commandTimeout: 180
                    )
                );

                return rows.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener tendencia Despacho (Producto/Familia) → " + ex.Message, ex);
            }
        }

        public async Task<List<KpiHeatmapDiaHoraDto>> GetHeatmapDespachoDiaHoraAsync(
        DateTime? from,
        DateTime? to,
        CancellationToken ct)
        {
            try
            {
                var cnStr = _configuration.GetConnectionString("CST");
                if (string.IsNullOrWhiteSpace(cnStr))
                    throw new Exception("No se encontró la cadena de conexión 'CST'.");

                var sql = @"
                            SET DATEFIRST 1; -- Lunes=1 ... Domingo=7

                            ;WITH Base AS (
                                SELECT
                                    a.IdDespachoEnc,
                                    a.IdPedidoEnc,
                                    a.IdPedidoDet,
                                    a.fec_agr AS FechaOperacion,
                                    ISNULL(a.CantidadDespachada,0) AS CantidadDespachada
                                FROM trans_despacho_det a WITH (NOLOCK)
                                INNER JOIN trans_pe_enc c WITH (NOLOCK) ON c.IdPedidoEnc = a.IdPedidoEnc
                                WHERE c.estado <> 'Anulado'
                                  AND c.ubicacion <> 'TMP'
                                  AND a.fec_agr IS NOT NULL
                                  AND ISNULL(a.CantidadDespachada,0) > 0
                                  AND (@p_from IS NULL OR CAST(a.fec_agr AS date) >= @p_from)
                                  AND (@p_to   IS NULL OR CAST(a.fec_agr AS date) <  DATEADD(day,1,@p_to))
                            )
                            SELECT
                                DATEPART(WEEKDAY, FechaOperacion) AS DiaSemana,
                                DATENAME(WEEKDAY, FechaOperacion) AS DiaNombre,
                                DATEPART(HOUR, FechaOperacion) AS Hora,
                                COUNT(DISTINCT IdDespachoEnc) AS Despachos,
                                COUNT(*) AS Lineas,
                                SUM(CantidadDespachada) AS Cantidad
                            FROM Base
                            GROUP BY
                                DATEPART(WEEKDAY, FechaOperacion),
                                DATENAME(WEEKDAY, FechaOperacion),
                                DATEPART(HOUR, FechaOperacion)
                            ORDER BY
                                DiaSemana, Hora;";

                using var cn = new SqlConnection(cnStr);
                await cn.OpenAsync(ct);

                var rows = await cn.QueryAsync<KpiHeatmapDiaHoraDto>(
                    new CommandDefinition(
                        sql,
                        new { p_from = from, p_to = to },
                        commandType: CommandType.Text,
                        cancellationToken: ct,
                        commandTimeout: 120
                    )
                );
              
                return rows.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener heatmap despacho (día/hora) → " + ex.Message, ex);
            }
        }

        public async Task<List<KpiStockResRowDto>> GetStockResAsync(int? idBodega,string? codigo,string? familia,string? estado,string? licencia,string? lote,DateTime? venceFrom,DateTime? venceTo,int page,int pageSize,CancellationToken ct)
        {
            try
            {
                var cnStr = _configuration.GetConnectionString("CST");
                if (string.IsNullOrWhiteSpace(cnStr))
                    throw new Exception("No se encontró la cadena de conexión 'CST'.");

                if (page <= 0) page = 1;
                if (pageSize <= 0) pageSize = 100;
                if (pageSize > 2000) pageSize = 2000;

                var offset = (page - 1) * pageSize;

                var sql = @"SELECT
                                IDBODEGA,
                                BODEGA,
                                PROPIETARIO,
                                CODIGO,
                                NOMBRE,
                                UnidadMedida,
                                PRESENTACION,
                                LOTE,
                                fecha_ingreso,
                                fecha_vence,
                                Disponible_UMBas,
                                Disponible_Presentacion,
                                NomEstado AS ESTADO,
                                lic_plate AS LICENCIA,
                                FAMILIA,
                                AREA,
                                CLASIFICACION,
                                UBICACION_TRAMO AS UBICACION
                            FROM VW_Stock_Res WITH (NOLOCK)
                            WHERE
                                (@p_idBodega IS NULL OR IDBODEGA = @p_idBodega)
                            AND (@p_codigo  IS NULL OR CODIGO LIKE '%' + @p_codigo + '%')
                            AND (@p_familia IS NULL OR FAMILIA LIKE '%' + @p_familia + '%')
                            AND (@p_estado  IS NULL OR NomEstado LIKE '%' + @p_estado + '%')
                            AND (@p_lic     IS NULL OR lic_plate LIKE '%' + @p_lic + '%')
                            AND (@p_lote    IS NULL OR LOTE LIKE '%' + @p_lote + '%')
                            AND (@p_vFrom   IS NULL OR CAST(fecha_vence AS date) >= @p_vFrom)
                            AND (@p_vTo     IS NULL OR CAST(fecha_vence AS date) <  DATEADD(day,1,@p_vTo))
                            ORDER BY
                                IDBODEGA, CODIGO, fecha_vence, lic_plate
                            OFFSET @p_offset ROWS FETCH NEXT @p_fetch ROWS ONLY;";

                using var cn = new SqlConnection(cnStr);
                await cn.OpenAsync(ct);

                var rows = await cn.QueryAsync<KpiStockResRowDto>(
                    new CommandDefinition(
                        sql,
                        new
                        {
                            p_idBodega = idBodega,
                            p_codigo = string.IsNullOrWhiteSpace(codigo) ? null : codigo.Trim(),
                            p_familia = string.IsNullOrWhiteSpace(familia) ? null : familia.Trim(),
                            p_estado = string.IsNullOrWhiteSpace(estado) ? null : estado.Trim(),
                            p_lic = string.IsNullOrWhiteSpace(licencia) ? null : licencia.Trim(),
                            p_lote = string.IsNullOrWhiteSpace(lote) ? null : lote.Trim(),
                            p_vFrom = venceFrom,
                            p_vTo = venceTo,
                            p_offset = offset,
                            p_fetch = pageSize
                        },
                        commandType: CommandType.Text,
                        cancellationToken: ct,
                        commandTimeout: 120
                    )
                );

                return rows.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener Stock (VW_Stock_Res) → " + ex.Message, ex);
            }
        }
    }
}