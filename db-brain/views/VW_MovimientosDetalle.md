---
id: db-brain-view-vw-movimientosdetalle
type: db-view
title: dbo.VW_MovimientosDetalle
schema: dbo
name: VW_MovimientosDetalle
kind: view
modify_date: 2024-09-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_MovimientosDetalle`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2024-09-12 |
| Columnas | 21 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Propietario` | `nvarchar(100)` |  |  |
| 2 | `Poliza` | `nvarchar(150)` | ✓ |  |
| 3 | `Producto` | `nvarchar(100)` | ✓ |  |
| 4 | `Presentación` | `nvarchar(50)` | ✓ |  |
| 5 | `Estado Origen` | `nvarchar(50)` | ✓ |  |
| 6 | `Estado Destino` | `nvarchar(50)` | ✓ |  |
| 7 | `Unidad de Medida` | `nvarchar(50)` | ✓ |  |
| 8 | `cantidad` | `float` | ✓ |  |
| 9 | `peso` | `float` | ✓ |  |
| 10 | `lote` | `nvarchar(50)` | ✓ |  |
| 11 | `Origen` | `nvarchar(200)` | ✓ |  |
| 12 | `Destino` | `nvarchar(200)` | ✓ |  |
| 13 | `Tipo Tarea` | `nvarchar(50)` | ✓ |  |
| 14 | `IdBodegaOrigen` | `int` |  |  |
| 15 | `fecha` | `datetime` | ✓ |  |
| 16 | `IdProducto` | `int` |  |  |
| 17 | `codigo` | `nvarchar(50)` | ✓ |  |
| 18 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 19 | `IdRecepcion` | `int` | ✓ |  |
| 20 | `IdRecepcionOc` | `int` | ✓ |  |
| 21 | `IdOrdenCompraEnc` | `int` | ✓ |  |

## Consume

- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `producto`
- `producto_bodega`
- `producto_estado`
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
CREATE View VW_MovimientosDetalle as
SELECT pr.nombre_comercial AS Propietario, enc.codigo_poliza AS Poliza, p.nombre AS Producto, pp.nombre AS Presentación, pe1.nombre AS [Estado Origen], pe2.nombre AS [Estado Destino], u.Nombre AS [Unidad de Medida], m.cantidad, 
                  m.peso, m.lote, dbo.Nombre_Completo_Ubicacion(u1.IdUbicacion, u1.IdBodega) AS Origen, dbo.Nombre_Completo_Ubicacion(u2.IdUbicacion, u2.IdBodega) AS Destino, stt.Nombre AS [Tipo Tarea], m.IdBodegaOrigen, m.fecha, 
                  p.IdProducto, p.codigo, p.codigo_barra, m.IdRecepcion, dbo.trans_re_oc.IdRecepcionOc, dbo.trans_re_oc.IdOrdenCompraEnc
FROM     dbo.trans_movimientos AS m INNER JOIN
                  dbo.propietario_bodega AS prb ON m.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
                  dbo.propietarios AS pr ON prb.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                  dbo.producto_bodega AS pb ON m.IdProductoBodega = pb.IdProductoBodega INNER JOIN
                  dbo.producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
                  dbo.trans_re_oc ON m.IdRecepcion = dbo.trans_re_oc.IdRecepcionEnc LEFT OUTER JOIN
                  dbo.bodega_ubicacion AS u2 ON m.IdUbicacionDestino = u2.IdUbicacion AND m.IdBodegaDestino = u2.IdBodega LEFT OUTER JOIN
                  dbo.bodega_ubicacion AS u1 ON m.IdUbicacionOrigen = u1.IdUbicacion AND m.IdBodegaDestino = u1.IdBodega LEFT OUTER JOIN
                  dbo.producto_presentacion AS pp ON m.IdPresentacion = pp.IdPresentacion AND p.IdProducto = pp.IdProducto LEFT OUTER JOIN
                  dbo.producto_estado AS pe1 ON m.IdEstadoOrigen = pe1.IdEstado AND pe1.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                  dbo.producto_estado AS pe2 ON m.IdEstadoDestino = pe2.IdEstado AND pe2.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                  dbo.unidad_medida AS u ON m.IdUnidadMedida = u.IdUnidadMedida AND u.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
                  dbo.sis_tipo_tarea AS stt ON m.IdTipoTarea = stt.IdTipoTarea LEFT OUTER JOIN
                  dbo.trans_oc_pol AS enc ON trans_re_oc.IdOrdenCompraEnc = enc.IdOrdenCompraEnc
```
