---
id: db-brain-view-vw-transubichhdet
type: db-view
title: dbo.VW_TransUbicHhDet
schema: dbo
name: VW_TransUbicHhDet
kind: view
modify_date: 2026-02-11
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_TransUbicHhDet`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2026-02-11 |
| Columnas | 46 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTareaUbicacionEnc` | `int` |  |  |
| 2 | `IdTareaUbicacionDet` | `int` |  |  |
| 3 | `IdStock` | `int` | ✓ |  |
| 4 | `CodigoBodega` | `nvarchar(50)` | ✓ |  |
| 5 | `nombre` | `nvarchar(100)` | ✓ |  |
| 6 | `lic_plate` | `varchar(max)` | ✓ |  |
| 7 | `lote` | `nvarchar(50)` | ✓ |  |
| 8 | `fecha_vence` | `datetime` | ✓ |  |
| 9 | `IdUbicacionDestino` | `int` | ✓ |  |
| 10 | `descripcion` | `nvarchar(50)` | ✓ |  |
| 11 | `IdEstadoOrigen` | `int` | ✓ |  |
| 12 | `IdEstadoDestino` | `int` | ✓ |  |
| 13 | `IdEstado_Origen` | `int` | ✓ |  |
| 14 | `NomEstado_Origen` | `nvarchar(50)` | ✓ |  |
| 15 | `IdEstado_Destino` | `int` | ✓ |  |
| 16 | `NomEstado_Destino` | `nvarchar(50)` | ✓ |  |
| 17 | `IdOperadorBodega` | `int` | ✓ |  |
| 18 | `HoraInicio` | `datetime` | ✓ |  |
| 19 | `HoraFin` | `datetime` | ✓ |  |
| 20 | `Realizado` | `bit` | ✓ |  |
| 21 | `cantidad` | `float` | ✓ |  |
| 22 | `activo` | `bit` | ✓ |  |
| 23 | `nombres` | `nvarchar(100)` | ✓ |  |
| 24 | `codigo` | `nvarchar(50)` | ✓ |  |
| 25 | `serializado` | `bit` | ✓ |  |
| 26 | `añada` | `int` | ✓ |  |
| 27 | `fecha_ingreso` | `char(10)` | ✓ |  |
| 28 | `Presentacion` | `nvarchar(50)` | ✓ |  |
| 29 | `UnidadMedida` | `nvarchar(50)` | ✓ |  |
| 30 | `serial` | `nvarchar(50)` | ✓ |  |
| 31 | `IdPropietario` | `int` | ✓ |  |
| 32 | `nombre_comercial` | `nvarchar(100)` | ✓ |  |
| 33 | `recibido` | `float` | ✓ |  |
| 34 | `IdProducto` | `int` | ✓ |  |
| 35 | `IdProductoBodega` | `int` | ✓ |  |
| 36 | `IdUnidadMedidaBasica` | `int` | ✓ |  |
| 37 | `estado` | `nvarchar(25)` | ✓ |  |
| 38 | `IdUbicacionOrigen` | `int` | ✓ |  |
| 39 | `IdPresentacion` | `int` | ✓ |  |
| 40 | `nivel` | `int` | ✓ |  |
| 41 | `indice_x` | `int` | ✓ |  |
| 42 | `IdBodega` | `int` | ✓ |  |
| 43 | `Tramo` | `nvarchar(50)` | ✓ |  |
| 44 | `NombreCompletoUbicaiconOrigen` | `nvarchar(200)` | ✓ |  |
| 45 | `NombreCompletoUbicaiconDestino` | `nvarchar(200)` | ✓ |  |
| 46 | `No_Linea` | `int` |  |  |

## Consume

- `bodega`
- `bodega_tramo`
- `bodega_ubicacion`
- `Nombre_Completo_Ubicacion`
- `operador`
- `operador_bodega`
- `producto`
- `producto_bodega`
- `producto_estado`
- `producto_presentacion`
- `propietarios`
- `trans_ubic_hh_det`
- `trans_ubic_hh_op`
- `trans_ubic_hh_stock`
- `unidad_medida`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_TransUbicHhDet]
AS
SELECT
    d.IdTareaUbicacionEnc,
    d.IdTareaUbicacionDet,
    d.IdStock,

    b.Codigo AS CodigoBodega,

    p.nombre,
    s.lic_plate,
    s.lote,
    s.fecha_vence,

    d.IdUbicacionDestino,
    ubDest.descripcion,

    d.IdEstadoOrigen,
    d.IdEstadoDestino,

    pe_origen.IdEstado AS IdEstado_Origen,
    pe_origen.nombre  AS NomEstado_Origen,

    pe_destino.IdEstado AS IdEstado_Destino,
    pe_destino.nombre  AS NomEstado_Destino,

    CASE
        WHEN op.IdOperadorBodega IS NULL THEN d.IdOperadorBodega
        ELSE op.IdOperadorBodega
    END AS IdOperadorBodega,

    d.HoraInicio,
    d.HoraFin,
    d.Realizado,
    d.cantidad,
    d.activo,

    o.nombres,

    p.codigo,
    p.serializado,

    s.añada,
    CONVERT(char(10), s.fecha_ingreso, 120) AS fecha_ingreso,

    pp.nombre AS Presentacion,
    um.Nombre AS UnidadMedida,

    s.serial,

    p.IdPropietario,
    pr.nombre_comercial,

    d.recibido,
    p.IdProducto,
    pb.IdProductoBodega,
    p.IdUnidadMedidaBasica,

    d.estado,
    d.IdUbicacionOrigen,

    pp.IdPresentacion,
    ubDest.nivel,
    ubDest.indice_x,

    d.IdBodega,
    t.descripcion AS Tramo,

    dbo.Nombre_Completo_Ubicacion(d.IdUbicacionOrigen, d.IdBodega)  AS NombreCompletoUbicaiconOrigen,
    dbo.Nombre_Completo_Ubicacion(d.IdUbicacionDestino, d.IdBodega) AS NombreCompletoUbicaiconDestino,

    d.No_Linea
FROM dbo.bodega_tramo t
INNER JOIN dbo.bodega_ubicacion AS ubDest
INNER JOIN dbo.trans_ubic_hh_det d
    ON ubDest.IdUbicacion = d.IdUbicacionDestino
   AND ubDest.IdBodega    = d.IdBodega

LEFT OUTER JOIN dbo.producto_bodega pb
INNER JOIN dbo.trans_ubic_hh_stock s
    ON pb.IdProductoBodega = s.IdProductoBodega
INNER JOIN dbo.producto p
    ON pb.IdProducto = p.IdProducto
INNER JOIN dbo.unidad_medida um
    ON s.IdUnidadMedida = um.IdUnidadMedida
INNER JOIN dbo.propietarios pr
    ON p.IdPropietario = pr.IdPropietario
    ON d.IdTareaUbicacionEnc = s.IdTareaUbicacionEnc
   AND d.IdTareaUbicacionDet = s.IdTareaUbicacionDet
   AND d.IdStock             = s.IdStock

INNER JOIN dbo.bodega_ubicacion ubOri
    ON ubOri.IdUbicacion = d.IdUbicacionOrigen
   AND ubOri.IdBodega    = d.IdBodega
    ON t.IdTramo  = ubOri.IdTramo
   AND t.IdBodega = ubOri.IdBodega

LEFT OUTER JOIN dbo.operador_bodega ob
INNER JOIN dbo.trans_ubic_hh_op op
    ON ob.IdOperadorBodega = op.IdOperadorBodega
INNER JOIN dbo.operador o
    ON ob.IdOperador = o.IdOperador
    ON d.IdTareaUbicacionEnc = op.IdTareaUbicacionEnc
   AND d.IdOperadorBodega    = op.IdOperadorBodega

LEFT OUTER JOIN dbo.producto_presentacion pp
    ON s.IdPresentacion = pp.IdPresentacion
   AND s.IdPresentacion = pp.IdPresentacion

LEFT OUTER JOIN dbo.producto_estado AS pe_origen
    ON pe_origen.IdEstado = d.IdEstadoOrigen

LEFT OUTER JOIN dbo.producto_estado AS pe_destino
    ON pe_destino.IdEstado = d.IdEstadoDestino

LEFT OUTER JOIN dbo.bodega b
    ON b.IdBodega = d.IdBodega

GROUP BY
    d.IdTareaUbicacionEnc,
    d.IdTareaUbicacionDet,
    d.IdStock,

    b.Codigo,

    p.nombre,
    s.lic_plate,
    s.lote,
    s.fecha_vence,

    d.IdUbicacionDestino,
    ubDest.descripcion,

    d.IdEstadoOrigen,
    d.IdEstadoDestino,

    pe_origen.IdEstado,
    pe_origen.nombre,
    pe_destino.IdEstado,
    pe_destino.nombre,

    d.IdOperadorBodega,
    op.IdOperadorBodega,

    d.HoraInicio,
    d.HoraFin,
    d.Realizado,
    d.cantidad,
    d.activo,

    o.nombres,

    p.codigo,
    p.serializado,

    s.añada,
    CONVERT(char(10), s.fecha_ingreso, 120),

    pp.nombre,
    um.Nombre,

    s.serial,

    p.IdPropietario,
    pr.nombre_comercial,

    d.recibido,
    p.IdProducto,
    pb.IdProductoBodega,
    p.IdUnidadMedidaBasica,

    d.estado,
    d.IdUbicacionOrigen,

    pp.IdPresentacion,
    ubDest.nivel,
    ubDest.indice_x,

    d.IdBodega,
    t.descripcion,

    d.No_Linea;
```
