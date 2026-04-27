---
id: db-brain-view-vw-despacho
type: db-view
title: dbo.VW_Despacho
schema: dbo
name: VW_Despacho
kind: view
modify_date: 2025-08-04
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_Despacho`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2025-08-04 |
| Columnas | 11 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `Código` | `int` |  |  |
| 2 | `Bodega` | `nvarchar(50)` | ✓ |  |
| 3 | `Propietario` | `nvarchar(100)` |  |  |
| 4 | `Piloto` | `nvarchar(301)` |  |  |
| 5 | `Vehiculo` | `nvarchar(179)` |  |  |
| 6 | `Ruta` | `nvarchar(50)` | ✓ |  |
| 7 | `activo` | `bit` | ✓ |  |
| 8 | `fecha` | `datetime` | ✓ |  |
| 9 | `IdBodega` | `int` |  |  |
| 10 | `No_Pase` | `int` | ✓ |  |
| 11 | `Pedidos` | `nvarchar(max)` | ✓ |  |

## Consume

- `bodega`
- `empresa_transporte_pilotos`
- `empresa_transporte_vehiculos`
- `propietario_bodega`
- `propietarios`
- `road_ruta`
- `trans_despacho_det`
- `trans_despacho_enc`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
CREATE VIEW [dbo].[VW_Despacho]
AS
SELECT 
    enc.IdDespachoEnc AS Código, 
    b.nombre AS Bodega, 
    pr.nombre_comercial AS Propietario, 
    ISNULL(p.nombres, '') + ' ' + ISNULL(p.apellidos, '') AS Piloto, 
    ISNULL(v.marca, '') + ' - ' + ISNULL(v.modelo, '') + ' - ' + ISNULL(v.tipo, '') + ' - ' + ISNULL(v.placa, '') AS Vehiculo, 
    r.NOMBRE AS Ruta, 
    enc.activo, 
    enc.fecha, 
    enc.IdBodega, 
    enc.No_Pase,

    -- Pedidos únicos por despacho (sin duplicados)
    (
        SELECT STRING_AGG(CAST(IdPedidoEnc AS NVARCHAR(MAX)), ', ')
        FROM (
            SELECT DISTINCT dd.IdPedidoEnc
            FROM trans_despacho_det dd
            WHERE dd.IdDespachoEnc = enc.IdDespachoEnc
        ) AS sub
    ) AS Pedidos

FROM dbo.trans_despacho_enc AS enc 
LEFT JOIN dbo.empresa_transporte_pilotos AS p ON enc.IdPiloto = p.IdPiloto 
LEFT JOIN dbo.empresa_transporte_vehiculos AS v ON enc.IdVehiculo = v.IdVehiculo 
LEFT JOIN dbo.road_ruta AS r ON enc.IdRuta = r.IdRuta 
INNER JOIN dbo.bodega AS b ON enc.IdBodega = b.IdBodega 
INNER JOIN dbo.propietario_bodega AS pb ON enc.IdPropietarioBodega = pb.IdPropietarioBodega 
INNER JOIN dbo.propietarios AS pr ON pb.IdPropietario = pr.IdPropietario
```
