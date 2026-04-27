---
id: db-brain-view-vw-movimientos-fix
type: db-view
title: dbo.VW_Movimientos_FIX
schema: dbo
name: VW_Movimientos_FIX
kind: view
modify_date: 2021-12-21
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Movimientos_FIX`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2021-12-21 |
| Columnas | 26 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMovimiento` | `int` |  |  |
| 2 | `IdTransaccion` | `int` |  |  |
| 3 | `IdPresentacion` | `int` | ✓ |  |
| 4 | `IdProductoBodega` | `int` | ✓ |  |
| 5 | `factor` | `float` | ✓ |  |
| 6 | `Propietario` | `nvarchar(100)` |  |  |
| 7 | `Poliza` | `nvarchar(150)` | ✓ |  |
| 8 | `Producto` | `nvarchar(100)` | ✓ |  |
| 9 | `Presentación` | `nvarchar(50)` | ✓ |  |
| 10 | `Estado Origen` | `nvarchar(50)` | ✓ |  |
| 11 | `Estado Destino` | `nvarchar(50)` | ✓ |  |
| 12 | `Unidad de Medida` | `nvarchar(50)` | ✓ |  |
| 13 | `cantidad` | `float` | ✓ |  |
| 14 | `peso` | `float` | ✓ |  |
| 15 | `lote` | `nvarchar(50)` | ✓ |  |
| 16 | `Origen` | `nvarchar(50)` | ✓ |  |
| 17 | `Destino` | `nvarchar(50)` | ✓ |  |
| 18 | `Tipo Tarea` | `nvarchar(50)` | ✓ |  |
| 19 | `IdBodegaOrigen` | `int` |  |  |
| 20 | `fecha` | `datetime` | ✓ |  |
| 21 | `IdProducto` | `int` |  |  |
| 22 | `codigo` | `nvarchar(50)` | ✓ |  |
| 23 | `codigo_barra` | `nvarchar(35)` | ✓ |  |
| 24 | `barra_pallet` | `nvarchar(50)` | ✓ |  |
| 25 | `fecha_vence` | `datetime` | ✓ |  |
| 26 | `Cantidad_Presentacion` | `float` | ✓ |  |

## Consume

- `bodega_ubicacion`
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
CREATE VIEW [dbo].[VW_Movimientos_FIX] AS
SELECT m.IdMovimiento, m.IdTransaccion, m.IdPresentacion, m.IdProductoBodega, pp.factor, pr.nombre_comercial AS Propietario, enc.codigo_poliza as Poliza,p.nombre AS Producto, pp.nombre AS Presentación,
pe1.nombre AS [Estado Origen], pe2.nombre AS [Estado Destino], u.Nombre AS [Unidad de Medida], m.cantidad, m.peso, m.lote,
u1.descripcion AS Origen, u2.descripcion AS Destino, stt.Nombre AS [Tipo Tarea], m.IdBodegaOrigen, m.fecha, p.IdProducto, p.codigo, p.codigo_barra, m.barra_pallet, m.fecha_vence,
m.cantidad / pp.factor AS Cantidad_Presentacion
FROM dbo.trans_movimientos AS m LEFT OUTER JOIN
dbo.propietario_bodega AS prb ON m.IdPropietarioBodega = prb.IdPropietarioBodega INNER JOIN
dbo.propietarios AS pr ON prb.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
dbo.producto_bodega AS pb ON m.IdProductoBodega = pb.IdProductoBodega INNER JOIN
dbo.producto AS p ON pb.IdProducto = p.IdProducto LEFT OUTER JOIN
dbo.bodega_ubicacion AS u1 ON m.IdUbicacionOrigen = u1.IdUbicacion AND m.IdBodegaOrigen = u1.IdBodega LEFT OUTER JOIN
dbo.bodega_ubicacion AS u2 ON m.IdUbicacionDestino = u2.IdUbicacion AND m.IdBodegaDestino = u2.IdBodega LEFT OUTER JOIN
dbo.producto_presentacion AS pp ON m.IdPresentacion = pp.IdPresentacion AND p.IdProducto = pp.IdProducto LEFT OUTER JOIN
dbo.producto_estado AS pe1 ON m.IdEstadoOrigen = pe1.IdEstado AND pe1.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
dbo.producto_estado AS pe2 ON m.IdEstadoDestino = pe2.IdEstado AND pe2.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
dbo.unidad_medida AS u ON m.IdUnidadMedida = u.IdUnidadMedida AND u.IdPropietario = pr.IdPropietario LEFT OUTER JOIN
dbo.sis_tipo_tarea AS stt ON m.IdTipoTarea = stt.IdTipoTarea
LEFT OUTER JOIN
dbo.trans_re_oc re ON m.IdRecepcion = re.IdRecepcionEnc LEFT OUTER JOIN
dbo.trans_oc_pol enc on re.IdOrdenCompraEnc = enc.IdOrdenCompraEnc
GROUP BY pr.nombre_comercial, p.nombre, pp.nombre, pe1.nombre, pe2.nombre, u.Nombre, m.cantidad, m.peso, m.lote, u1.descripcion, u2.descripcion, stt.Nombre, m.IdBodegaOrigen, m.fecha, p.IdProducto, p.codigo, p.codigo_barra,
m.barra_pallet, m.fecha_vence, pp.IdPresentacion, pp.factor, m.cantidad / pp.factor, enc.codigo_poliza, m.IdTransaccion, m.IdPresentacion, pp.factor,m.IdProductoBodega, m.IdMovimiento
```
