---
id: db-brain-view-vw-movimientos-n
type: db-view
title: dbo.VW_Movimientos_N
schema: dbo
name: VW_Movimientos_N
kind: view
modify_date: 2025-03-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Movimientos_N`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-03-18 |
| Columnas | 41 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Propietario` | `nvarchar(100)` |  |  |
| 2 | `Producto` | `nvarchar(100)` | ✓ |  |
| 3 | `Poliza` | `nvarchar(150)` | ✓ |  |
| 4 | `Presentación` | `nvarchar(50)` | ✓ |  |
| 5 | `EstadoOrigen` | `nvarchar(50)` | ✓ |  |
| 6 | `EstadoDestino` | `nvarchar(50)` | ✓ |  |
| 7 | `UMBas` | `nvarchar(50)` | ✓ |  |
| 8 | `cantidad` | `float` | ✓ |  |
| 9 | `peso` | `float` | ✓ |  |
| 10 | `lote` | `nvarchar(50)` | ✓ |  |
| 11 | `UbicOrigen` | `nvarchar(200)` | ✓ |  |
| 12 | `UbicDestino` | `nvarchar(200)` | ✓ |  |
| 13 | `TipoTarea` | `nvarchar(50)` | ✓ |  |
| 14 | `fecha` | `datetime` | ✓ |  |
| 15 | `IdProducto` | `int` |  |  |
| 16 | `codigo` | `nvarchar(50)` | ✓ |  |
| 17 | `CodigoBarra` | `nvarchar(35)` | ✓ |  |
| 18 | `IdTipoTarea` | `int` | ✓ |  |
| 19 | `Contabilizar` | `bit` | ✓ |  |
| 20 | `fecha_vence` | `datetime` | ✓ |  |
| 21 | `IdTipoActualizacionCosto` | `int` | ✓ |  |
| 22 | `IdPresentacion` | `int` | ✓ |  |
| 23 | `IdUnidadMedida` | `int` | ✓ |  |
| 24 | `IdEstadoOrigen` | `int` | ✓ |  |
| 25 | `IdProductoBodega` | `int` | ✓ |  |
| 26 | `IdPropietarioBodega` | `int` | ✓ |  |
| 27 | `IdBodega` | `int` | ✓ |  |
| 28 | `Licencia` | `nvarchar(50)` | ✓ |  |
| 29 | `Clasificacion` | `nvarchar(50)` | ✓ |  |
| 30 | `Familia` | `nvarchar(50)` | ✓ |  |
| 31 | `IdBodegaOrigen` | `int` |  |  |
| 32 | `IdBodegaDestino` | `int` | ✓ |  |
| 33 | `Codigo_Bodega_Destino` | `nvarchar(50)` | ✓ |  |
| 34 | `Nombre_Bodega_Destino` | `nvarchar(50)` | ✓ |  |
| 35 | `IdMovimiento` | `int` |  |  |
| 36 | `Codigo_Bodega_Origen` | `nvarchar(50)` | ✓ |  |
| 37 | `Nombre_Bodega_Origen` | `nvarchar(50)` | ✓ |  |
| 38 | `NombreArea` | `nvarchar(200)` | ✓ |  |
| 39 | `factor` | `float` | ✓ |  |
| 40 | `IdTicketTMS` | `nvarchar(50)` | ✓ |  |
| 41 | `Operador` | `nvarchar(201)` |  |  |

## Consume

- `bodega`
- `bodega_ubicacion`
- `Nombre_Area`
- `Nombre_Completo_Ubicacion`
- `operador`
- `operador_bodega`
- `producto`
- `producto_bodega`
- `producto_clasificacion`
- `producto_estado`
- `producto_familia`
- `producto_presentacion`
- `propietario_bodega`
- `propietarios`
- `sis_tipo_tarea`
- `trans_movimientos`
- `trans_oc_pol`
- `trans_re_oc`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW VW_Movimientos_N
AS
SELECT        pr.nombre_comercial AS Propietario, p.nombre AS Producto, enc.codigo_poliza AS Poliza, pp.nombre AS Presentación, pe1.nombre AS EstadoOrigen, pe2.nombre AS EstadoDestino, u.Nombre AS UMBas, m.cantidad, m.peso,
                         m.lote, dbo.Nombre_Completo_Ubicacion(u1.IdUbicacion, u1.IdBodega) AS UbicOrigen, dbo.Nombre_Completo_Ubicacion(u2.IdUbicacion, u2.IdBodega) AS UbicDestino, stt.Nombre AS TipoTarea, m.fecha, p.IdProducto, p.codigo,
                         p.codigo_barra AS CodigoBarra, stt.IdTipoTarea, stt.Contabilizar, m.fecha_vence, pr.IdTipoActualizacionCosto, m.IdPresentacion, m.IdUnidadMedida, m.IdEstadoOrigen, m.IdProductoBodega, prb.IdPropietarioBodega,
                         prb.IdBodega, m.barra_pallet Licencia, dbo.producto_clasificacion.nombre AS Clasificacion, dbo.producto_familia.nombre AS Familia, m.IdBodegaOrigen, m.IdBodegaDestino, bodega_1.codigo AS Codigo_Bodega_Destino,
                         bodega_1.nombre AS Nombre_Bodega_Destino, m.IdMovimiento, dbo.bodega.codigo AS Codigo_Bodega_Origen, dbo.bodega.nombre AS Nombre_Bodega_Origen, dbo.Nombre_Area(u1.IdArea, m.IdBodegaOrigen)
                         AS NombreArea, pp.factor, enc.ticket AS IdTicketTMS, ISNULL(o.nombres,'') + ' ' +  ISNULL(o.apellidos,'') Operador
FROM            dbo.trans_movimientos AS m LEFT OUTER JOIN
                         dbo.propietario_bodega AS prb ON m.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                         dbo.propietarios AS pr ON prb.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                         dbo.producto_bodega AS pb ON m.IdProductoBodega = pb.IdProductoBodega INNER JOIN
                         dbo.producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
                         dbo.producto_clasificacion ON p.IdClasificacion = dbo.producto_clasificacion.IdClasificacion LEFT OUTER JOIN
                         dbo.producto_familia ON p.IdFamilia = dbo.producto_familia.IdFamilia LEFT OUTER JOIN
                         dbo.bodega ON m.IdBodegaOrigen = dbo.bodega.IdBodega AND m.IdEmpresa = dbo.bodega.IdEmpresa LEFT OUTER JOIN
                         dbo.bodega AS bodega_1 ON m.IdEmpresa = bodega_1.IdEmpresa AND m.IdBodegaDestino = bodega_1.IdBodega LEFT OUTER JOIN
                         dbo.bodega_ubicacion AS u2 ON m.IdUbicacionDestino = u2.IdUbicacion AND u2.IdBodega = m.IdBodegaDestino LEFT OUTER JOIN
                         dbo.bodega_ubicacion AS u1 ON m.IdUbicacionOrigen = u1.IdUbicacion AND u1.IdBodega = m.IdBodegaOrigen LEFT OUTER JOIN
                         dbo.producto_presentacion AS pp ON m.IdPresentacion = pp.IdPresentacion LEFT OUTER JOIN
                         dbo.producto_estado AS pe1 ON m.IdEstadoOrigen = pe1.IdEstado LEFT OUTER JOIN
                         dbo.producto_estado AS pe2 ON m.IdEstadoDestino = pe2.IdEstado LEFT OUTER JOIN
                         dbo.unidad_medida AS u ON m.IdUnidadMedida = u.IdUnidadMedida LEFT OUTER JOIN
                         dbo.sis_tipo_tarea AS stt ON m.IdTipoTarea = stt.IdTipoTarea LEFT OUTER JOIN
                         dbo.trans_re_oc AS re ON m.IdRecepcion = re.IdRecepcionEnc LEFT OUTER JOIN
                         dbo.trans_oc_pol AS enc ON re.IdOrdenCompraEnc = enc.IdOrdenCompraEnc LEFT OUTER JOIN
	                 dbo.operador_bodega ob on m.IdOperadorBodega = ob.IdOperadorBodega LEFT OUTER JOIN
	                 dbo.operador o ON o.IdOperador = ob.IdOperador
```
