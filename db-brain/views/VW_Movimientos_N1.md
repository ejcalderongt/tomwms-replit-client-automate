---
id: db-brain-view-vw-movimientos-n1
type: db-view
title: dbo.VW_Movimientos_N1
schema: dbo
name: VW_Movimientos_N1
kind: view
modify_date: 2021-08-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Movimientos_N1`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-08-25 |
| Columnas | 32 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idmovimiento` | `int` |  |  |
| 2 | `Propietario` | `nvarchar(100)` |  |  |
| 3 | `Poliza` | `nvarchar(150)` | ✓ |  |
| 4 | `Producto` | `nvarchar(100)` | ✓ |  |
| 5 | `Presentación` | `nvarchar(50)` | ✓ |  |
| 6 | `EstadoOrigen` | `nvarchar(50)` | ✓ |  |
| 7 | `EstadoDestino` | `nvarchar(50)` | ✓ |  |
| 8 | `UMBas` | `nvarchar(50)` | ✓ |  |
| 9 | `cantidad` | `float` | ✓ |  |
| 10 | `peso` | `float` | ✓ |  |
| 11 | `lote` | `nvarchar(50)` | ✓ |  |
| 12 | `UbicOrigen` | `nvarchar(50)` | ✓ |  |
| 13 | `UbicDestino` | `nvarchar(50)` | ✓ |  |
| 14 | `TipoTarea` | `nvarchar(50)` | ✓ |  |
| 15 | `IdBodegaOrigen` | `int` |  |  |
| 16 | `fecha` | `datetime` | ✓ |  |
| 17 | `IdProducto` | `int` |  |  |
| 18 | `codigo` | `nvarchar(50)` | ✓ |  |
| 19 | `CodigoBarra` | `nvarchar(35)` | ✓ |  |
| 20 | `IdTipoTarea` | `int` | ✓ |  |
| 21 | `Contabilizar` | `bit` | ✓ |  |
| 22 | `No_Doc_Ingreso` | `nvarchar(30)` |  |  |
| 23 | `No_Ref_Ingreso` | `nvarchar(100)` |  |  |
| 24 | `No_Doc_Salida` | `nvarchar(50)` | ✓ |  |
| 25 | `No_Ref_Salida` | `nvarchar(50)` | ✓ |  |
| 26 | `fecha_vence` | `datetime` | ✓ |  |
| 27 | `IdTipoActualizacionCosto` | `int` | ✓ |  |
| 28 | `IdPresentacion` | `int` | ✓ |  |
| 29 | `IdUnidadMedida` | `int` | ✓ |  |
| 30 | `IdEstadoOrigen` | `int` | ✓ |  |
| 31 | `IdProductoBodega` | `int` | ✓ |  |
| 32 | `barra_pallet` | `nvarchar(50)` | ✓ |  |

## Consume

- `bodega_ubicacion`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `sis_tipo_tarea`
- `trans_ajuste_enc`
- `trans_despacho_det`
- `trans_movimientos`
- `trans_oc_enc`
- `trans_oc_pol`
- `trans_pe_enc`
- `trans_re_enc`
- `trans_re_oc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE View VW_Movimientos_N1 as
SELECT        m.idmovimiento, pr.nombre_comercial AS Propietario,enc.codigo_poliza as Poliza, p.nombre AS Producto, pp.nombre AS Presentación, pe1.nombre AS EstadoOrigen, pe2.nombre AS EstadoDestino, u.Nombre AS UMBas, m.cantidad, m.peso, m.lote, 
                         u1.descripcion AS UbicOrigen, u2.descripcion AS UbicDestino, stt.Nombre AS TipoTarea, m.IdBodegaOrigen, m.fecha, p.IdProducto, p.codigo, p.codigo_barra AS CodigoBarra, stt.IdTipoTarea, stt.Contabilizar, 
                         ISNULL(toce.No_Documento, N'') AS No_Doc_Ingreso, ISNULL(toce.Referencia, N'') AS No_Ref_Ingreso, '' AS No_Doc_Salida, '' AS No_Ref_Salida, m.fecha_vence, pr.IdTipoActualizacionCosto, m.IdPresentacion, 
                         m.IdUnidadMedida, m.IdEstadoOrigen, m.IdProductoBodega, m.barra_pallet
FROM            dbo.trans_movimientos AS m LEFT OUTER JOIN
                         dbo.propietario_bodega AS prb ON m.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                         dbo.propietarios AS pr ON prb.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                         dbo.producto_bodega AS pb ON m.IdProductoBodega = pb.IdProductoBodega INNER JOIN
                         dbo.producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
                         dbo.trans_re_enc AS tre ON prb.IdPropietarioBodega = tre.IdPropietarioBodega AND m.IdRecepcion = tre.IdRecepcionEnc LEFT OUTER JOIN
                         dbo.trans_re_oc AS troc ON tre.IdRecepcionEnc = troc.IdRecepcionEnc LEFT OUTER JOIN
                         dbo.trans_oc_enc AS toce ON troc.IdOrdenCompraEnc = toce.IdOrdenCompraEnc LEFT OUTER JOIN
                         dbo.bodega_ubicacion AS u2 ON m.IdUbicacionDestino = u2.IdUbicacion AND u2.IdBodega = m.IdBodegaDestino LEFT OUTER JOIN
                         dbo.bodega_ubicacion AS u1 ON m.IdUbicacionOrigen = u1.IdUbicacion AND u1.IdBodega = m.IdBodegaOrigen LEFT OUTER JOIN
                         dbo.producto_presentacion AS pp ON m.IdPresentacion = pp.IdPresentacion AND p.IdProducto = pp.IdProducto LEFT OUTER JOIN
                         dbo.producto_estado AS pe1 ON m.IdEstadoOrigen = pe1.IdEstado AND pe1.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                         dbo.producto_estado AS pe2 ON m.IdEstadoDestino = pe2.IdEstado AND pe2.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                         dbo.unidad_medida AS u ON m.IdUnidadMedida = u.IdUnidadMedida AND u.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                         dbo.sis_tipo_tarea AS stt ON m.IdTipoTarea = stt.IdTipoTarea
						 LEFT OUTER JOIN
                         dbo.trans_re_oc re ON m.IdRecepcion = re.IdRecepcionEnc LEFT OUTER JOIN
						 dbo.trans_oc_pol enc on re.IdOrdenCompraEnc = enc.IdOrdenCompraEnc
WHERE        M.IdTipoTarea IN (1, 2, 6, 13, 14, 15, 16)
UNION
SELECT        m.idmovimiento, pr.nombre_comercial AS Propietario, enc.codigo_poliza as Poliza,p.nombre AS Producto, pp.nombre AS Presentación, pe1.nombre AS EstadoOrigen, pe2.nombre AS EstadoDestino, u.Nombre AS UMBas, m.cantidad, m.peso, m.lote, 
                         u1.descripcion AS UbicOrigen, u2.descripcion AS UbicDestino, stt.Nombre AS TipoTarea, m.IdBodegaOrigen, m.fecha, p.IdProducto, p.codigo, p.codigo_barra AS CodigoBarra, stt.IdTipoTarea, stt.Contabilizar, 
                         ISNULL(toce.No_Documento, N'') AS No_Doc_Ingreso, ISNULL(toce.Referencia, N'') AS No_Ref_Ingreso, ISNULL(CASE WHEN penc.IdPedidoEnc = '0' THEN '' END, N'') AS No_Doc_Salida, ISNULL(penc.Referencia, N'') 
                         AS No_Ref_Salida, m.fecha_vence, pr.IdTipoActualizacionCosto, m.IdPresentacion, m.IdUnidadMedida, m.IdEstadoOrigen, m.IdProductoBodega, m.barra_pallet					 
FROM            dbo.trans_movimientos AS m LEFT OUTER JOIN
                         dbo.propietario_bodega AS prb ON m.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                         dbo.propietarios AS pr ON prb.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                         dbo.producto_bodega AS pb ON m.IdProductoBodega = pb.IdProductoBodega INNER JOIN
                         dbo.producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
                             (SELECT DISTINCT d .IdDespachoEnc, d .IDPEDIDOENC, d .IDPRODUCTOBODEGA, d .IdUnidadMedidaBasica, d .IdPresentacion
                               FROM            trans_despacho_det d) Desp ON DESP.IdDespachoEnc = M.IdTransaccion AND m.IdProductoBodega = DESP.IdProductoBodega AND m.IdUnidadMedida = DESP.IdUnidadMedidaBasica AND 
                         ISNULL(m.IdPresentacion, 0) = DESP.IdPresentacion LEFT OUTER JOIN
                         trans_pe_enc penc ON penc.IdPedidoEnc = desp.IdPedidoEnc LEFT OUTER JOIN
                         dbo.trans_re_enc AS tre ON prb.IdPropietarioBodega = tre.IdPropietarioBodega AND m.IdRecepcion = tre.IdRecepcionEnc LEFT OUTER JOIN
                         dbo.trans_re_oc AS troc ON tre.IdRecepcionEnc = troc.IdRecepcionEnc LEFT OUTER JOIN
                         dbo.trans_oc_enc AS toce ON troc.IdOrdenCompraEnc = toce.IdOrdenCompraEnc LEFT OUTER JOIN
                         dbo.bodega_ubicacion AS u2 ON m.IdUbicacionDestino = u2.IdUbicacion AND u2.IdBodega = m.IdBodegaDestino LEFT OUTER JOIN
                         dbo.bodega_ubicacion AS u1 ON m.IdUbicacionOrigen = u1.IdUbicacion AND u1.IdBodega = m.IdBodegaOrigen LEFT OUTER JOIN
                         dbo.producto_presentacion AS pp ON m.IdPresentacion = pp.IdPresentacion AND p.IdProducto = pp.IdProducto LEFT OUTER JOIN
                         dbo.producto_estado AS pe1 ON m.IdEstadoOrigen = pe1.IdEstado AND pe1.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                         dbo.producto_estado AS pe2 ON m.IdEstadoDestino = pe2.IdEstado AND pe2.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                         dbo.unidad_medida AS u ON m.IdUnidadMedida = u.IdUnidadMedida AND u.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                         dbo.sis_tipo_tarea AS stt ON m.IdTipoTarea = stt.IdTipoTarea
						 LEFT OUTER JOIN
                         dbo.trans_re_oc re ON m.IdRecepcion = re.IdRecepcionEnc LEFT OUTER JOIN
						 dbo.trans_oc_pol enc on re.IdOrdenCompraEnc = enc.IdOrdenCompraEnc
WHERE        M.IdTipoTarea = 5
UNION
SELECT        m.idmovimiento, pr.nombre_comercial AS Propietario, enc.codigo_poliza as Poliza, p.nombre AS Producto, pp.nombre AS Presentación, pe1.nombre AS EstadoOrigen, pe2.nombre AS EstadoDestino, u.Nombre AS UMBas, m.cantidad, m.peso, m.lote, 
                         u1.descripcion AS UbicOrigen, u2.descripcion AS UbicDestino, stt.Nombre AS TipoTarea, m.IdBodegaOrigen, m.fecha, p.IdProducto, p.codigo, p.codigo_barra AS CodigoBarra, stt.IdTipoTarea, stt.Contabilizar, 
                         ISNULL(toce.No_Documento, N'') AS No_Doc_Ingreso, ISNULL(toce.Referencia, N'') AS No_Ref_Ingreso, CONVERT(NVARCHAR(50), ISNULL(aje.idajusteenc, N'')) AS No_Doc_Salida, CONVERT(NVARCHAR(50), 
                         ISNULL(aje.idajusteenc, N'')) AS No_Ref_Salida, m.fecha_vence, pr.IdTipoActualizacionCosto, m.IdPresentacion, m.IdUnidadMedida, m.IdEstadoOrigen, m.IdProductoBodega, m.barra_pallet
FROM            dbo.trans_movimientos AS m LEFT OUTER JOIN
                         dbo.propietario_bodega AS prb ON m.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                         dbo.propietarios AS pr ON prb.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                         dbo.producto_bodega AS pb ON m.IdProductoBodega = pb.IdProductoBodega INNER JOIN
                         dbo.producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
                         dbo.trans_re_enc AS tre ON prb.IdPropietarioBodega = tre.IdPropietarioBodega AND m.IdRecepcion = tre.IdRecepcionEnc LEFT OUTER JOIN
                         dbo.trans_re_oc AS troc ON tre.IdRecepcionEnc = troc.IdRecepcionEnc LEFT OUTER JOIN
                         dbo.trans_oc_enc AS toce ON troc.IdOrdenCompraEnc = toce.IdOrdenCompraEnc LEFT OUTER JOIN
                         dbo.bodega_ubicacion AS u2 ON m.IdUbicacionDestino = u2.IdUbicacion AND u2.IdBodega = m.IdBodegaDestino LEFT OUTER JOIN
                         dbo.bodega_ubicacion AS u1 ON m.IdUbicacionOrigen = u1.IdUbicacion AND u1.IdBodega = m.IdBodegaOrigen LEFT OUTER JOIN
                         dbo.producto_presentacion AS pp ON m.IdPresentacion = pp.IdPresentacion AND p.IdProducto = pp.IdProducto LEFT OUTER JOIN
                         dbo.producto_estado AS pe1 ON m.IdEstadoOrigen = pe1.IdEstado AND pe1.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                         dbo.producto_estado AS pe2 ON m.IdEstadoDestino = pe2.IdEstado AND pe2.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                         dbo.unidad_medida AS u ON m.IdUnidadMedida = u.IdUnidadMedida AND u.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                         dbo.sis_tipo_tarea AS stt ON m.IdTipoTarea = stt.IdTipoTarea LEFT OUTER JOIN
                         dbo.trans_ajuste_enc aje ON aje.idajusteenc = m.IdTransaccion
						 LEFT OUTER JOIN
                         dbo.trans_re_oc re ON m.IdRecepcion = re.IdRecepcionEnc LEFT OUTER JOIN
						 dbo.trans_oc_pol enc on re.IdOrdenCompraEnc = enc.IdOrdenCompraEnc
WHERE        M.IdTipoTarea = 17
```
