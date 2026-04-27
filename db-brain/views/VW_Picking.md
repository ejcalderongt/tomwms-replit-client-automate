---
id: db-brain-view-vw-picking
type: db-view
title: dbo.VW_Picking
schema: dbo
name: VW_Picking
kind: view
modify_date: 2025-06-26
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Picking`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-06-26 |
| Columnas | 17 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Código` | `int` |  |  |
| 2 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 3 | `Propietario` | `nvarchar(100)` |  |  |
| 4 | `Ubicación Picking` | `nvarchar(50)` | ✓ |  |
| 5 | `estado` | `nvarchar(20)` | ✓ |  |
| 6 | `Detalle Operador` | `bit` | ✓ |  |
| 7 | `Hora Inicial` | `char(5)` | ✓ |  |
| 8 | `Hora Final` | `char(5)` | ✓ |  |
| 9 | `Duracion_Minutos` | `varchar(5)` | ✓ |  |
| 10 | `Fecha Picking` | `datetime` | ✓ |  |
| 11 | `activo` | `bit` | ✓ |  |
| 12 | `IdBodega` | `int` |  |  |
| 13 | `procesado_bof` | `bit` | ✓ |  |
| 14 | `Codigo_Muelle` | `nvarchar(50)` | ✓ |  |
| 15 | `Muelle` | `nvarchar(50)` | ✓ |  |
| 16 | `Observacion` | `nvarchar(3000)` | ✓ |  |
| 17 | `Pedidos` | `int` |  |  |

## Consume

- `bodega`
- `bodega_muelles`
- `bodega_ubicacion`
- `propietario_bodega`
- `propietarios`
- `trans_picking_det`
- `trans_picking_enc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Picking]
AS
SELECT 
    enc.IdPickingEnc AS Código, 
    b.nombre AS Bodega, 
    p.nombre_comercial AS Propietario, 
    bu.descripcion AS [Ubicación Picking], 
    enc.estado, 
    enc.detalle_operador AS [Detalle Operador], 
    CONVERT(char(5), enc.hora_ini, 108) AS [Hora Inicial], 
    CONVERT(char(5), enc.hora_fin, 108) AS [Hora Final], 
    CASE 
        WHEN procesado_bof = 1 THEN '00:00' 
        ELSE CONVERT(char(5), hora_fin - hora_ini, 108) 
    END AS Duracion_Minutos, 
    enc.fecha_picking AS [Fecha Picking], 
    enc.activo, 
    enc.IdBodega, 
    enc.procesado_bof, 
    dbo.bodega_muelles.codigo_barra AS Codigo_Muelle, 
    dbo.bodega_muelles.nombre AS Muelle,
    enc.Observacion,
    ISNULL(pedidos.CantPedidos, 0) AS Pedidos
FROM dbo.trans_picking_enc AS enc
INNER JOIN dbo.propietario_bodega AS pb 
    ON enc.IdPropietarioBodega = pb.IdPropietarioBodega 
INNER JOIN dbo.propietarios AS p 
    ON pb.IdPropietario = p.IdPropietario 
INNER JOIN dbo.bodega AS b 
    ON enc.IdBodega = b.IdBodega 
INNER JOIN dbo.bodega_ubicacion AS bu 
    ON enc.IdUbicacionPicking = bu.IdUbicacion AND enc.IdBodega = bu.IdBodega 
LEFT JOIN dbo.bodega_muelles 
    ON b.IdBodega = dbo.bodega_muelles.IdBodega AND enc.IdBodegaMuelle = dbo.bodega_muelles.IdMuelle
LEFT JOIN (
    SELECT IdPickingEnc, COUNT(IdPedidoEnc) AS CantPedidos
    FROM trans_picking_det
    GROUP BY IdPickingEnc
) AS pedidos ON pedidos.IdPickingEnc = enc.IdPickingEnc
```
