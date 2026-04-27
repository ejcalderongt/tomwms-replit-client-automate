---
id: db-brain-view-vw-tms-tikcet
type: db-view
title: dbo.VW_TMS_Tikcet
schema: dbo
name: VW_TMS_Tikcet
kind: view
modify_date: 2022-05-24
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.VW_TMS_Tikcet`

| Atributo | Valor |
|---|---|
| Tipo | VIEW |
| Schema modify_date | 2022-05-24 |
| Columnas | 17 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTicket` | `int` |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |
| 3 | `IdPropietario` | `int` | ✓ |  |
| 4 | `IdUbicacionDestino` | `int` | ✓ |  |
| 5 | `IdPiloto` | `int` | ✓ |  |
| 6 | `IdVehiculo` | `int` | ✓ |  |
| 7 | `IdEmpresaTransporte` | `int` | ✓ |  |
| 8 | `Tipo_Operacion` | `nvarchar(50)` | ✓ |  |
| 9 | `Fecha_Ingreso` | `datetime` | ✓ |  |
| 10 | `Fecha_Salida` | `datetime` | ✓ |  |
| 11 | `Estado` | `nvarchar(50)` | ✓ |  |
| 12 | `No_Poliza` | `nvarchar(50)` | ✓ |  |
| 13 | `Nombre_Piloto` | `nvarchar(150)` | ✓ |  |
| 14 | `Apellidos_Piloto` | `nvarchar(150)` | ✓ |  |
| 15 | `Placa_Vehiculo` | `nvarchar(20)` | ✓ |  |
| 16 | `Placa_TC` | `nvarchar(50)` | ✓ |  |
| 17 | `Empresa_Transporte` | `nvarchar(100)` | ✓ |  |

## Consume

- `empresa_transporte`
- `empresa_transporte_pilotos`
- `empresa_transporte_vehiculos`
- `tms_ticket`

## Definition

> Sensible — no exponer fuera del brain ni a clientes externos.

```sql
/************************************************************************************************************************************************************/
/******************** GT19052022: ajuste de Erik para mostrar la lista de tickets para edición.    **********************************************************/

CREATE VIEW [dbo].[VW_TMS_Tikcet]
AS
SELECT dbo.tms_ticket.IdTicket, dbo.tms_ticket.IdEmpresa, dbo.tms_ticket.IdPropietario, dbo.tms_ticket.IdUbicacionDestino, dbo.tms_ticket.IdPiloto, dbo.tms_ticket.IdVehiculo, dbo.tms_ticket.IdEmpresaTransporte,
dbo.tms_ticket.Tipo_Operacion, dbo.tms_ticket.Fecha_Ingreso, dbo.tms_ticket.Fecha_Salida, dbo.tms_ticket.Estado, dbo.tms_ticket.No_Poliza, dbo.empresa_transporte_pilotos.nombres AS Nombre_Piloto,
dbo.empresa_transporte_pilotos.apellidos AS Apellidos_Piloto, dbo.empresa_transporte_vehiculos.placa AS Placa_Vehiculo, dbo.empresa_transporte_vehiculos.placa_comercial AS Placa_TC,
dbo.empresa_transporte.nombre AS Empresa_Transporte
FROM dbo.tms_ticket LEFT OUTER JOIN
dbo.empresa_transporte_pilotos ON dbo.tms_ticket.IdPiloto = dbo.empresa_transporte_pilotos.IdPiloto LEFT OUTER JOIN
dbo.empresa_transporte ON dbo.empresa_transporte_pilotos.IdEmpresaTransporte = dbo.empresa_transporte.IdEmpresaTransporte LEFT OUTER JOIN
dbo.empresa_transporte_vehiculos ON dbo.tms_ticket.IdVehiculo = dbo.empresa_transporte_vehiculos.IdVehiculo
```
