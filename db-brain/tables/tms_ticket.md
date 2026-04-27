---
id: db-brain-table-tms-ticket
type: db-table
title: dbo.tms_ticket
schema: dbo
name: tms_ticket
kind: table
rows: 0
modify_date: 2021-06-02
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.tms_ticket`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2021-06-02 |
| Columnas | 22 |
| Índices | 1 |
| FKs | out:4 in:0 |

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
| 13 | `No_Placa` | `nvarchar(50)` | ✓ |  |
| 14 | `No_Documento_Piloto` | `nvarchar(150)` | ✓ |  |
| 15 | `Tipo_Documento_Piloto` | `nvarchar(150)` | ✓ |  |
| 16 | `Nombres_Piloto` | `nvarchar(150)` | ✓ |  |
| 17 | `Apellidos_Piloto` | `nvarchar(150)` | ✓ |  |
| 18 | `No_TC` | `nvarchar(50)` | ✓ |  |
| 19 | `fecha_procesado` | `datetime` | ✓ |  |
| 20 | `fecha_asignado` | `datetime` | ✓ |  |
| 21 | `procesado_stock_jornada` | `bit` | ✓ |  |
| 22 | `fecha_procesado_stock_jornada` | `datetime` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_tms_ticket` | CLUSTERED · **PK** | IdTicket |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_tms_ticket_empresa_transporte` → `empresa_transporte`
- `FK_tms_ticket_empresa_transporte_pilotos` → `empresa_transporte_pilotos`
- `FK_tms_ticket_empresa_transporte_vehiculos` → `empresa_transporte_vehiculos`
- `FK_tms_ticket_propietarios` → `propietarios`

## Quién la referencia

**7** objetos:

- `VW_Movimientos_Documento` (view)
- `VW_Movimientos_Propietario` (view)
- `VW_Movimientos_Retroactivos` (view)
- `VW_Stock_Jornada` (view)
- `VW_Stock_Res` (view)
- `VW_TMS_Tikcet` (view)
- `VW_TMSTickets_Sin_Retroactivo` (view)

