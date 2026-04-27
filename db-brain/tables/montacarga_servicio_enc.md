---
id: db-brain-table-montacarga-servicio-enc
type: db-table
title: dbo.montacarga_servicio_enc
schema: dbo
name: montacarga_servicio_enc
kind: table
rows: 0
modify_date: 2016-04-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.montacarga_servicio_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2016-04-25 |
| Columnas | 16 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdServicioEnc` | `int` |  |  |
| 2 | `IdEmpresa` | `int` |  |  |
| 3 | `IdBodega` | `int` |  |  |
| 4 | `Fecha_Sistema` | `datetime` | ✓ |  |
| 5 | `Descripcion` | `nvarchar(100)` | ✓ |  |
| 6 | `Tecnico` | `nvarchar(50)` | ✓ |  |
| 7 | `ObservacionTecnico` | `nvarchar(250)` | ✓ |  |
| 8 | `Estado` | `int` | ✓ |  |
| 9 | `Fecha_Atencion` | `datetime` | ✓ |  |
| 10 | `Fecha_Servicio` | `datetime` | ✓ |  |
| 11 | `FechaInicio` | `datetime` | ✓ |  |
| 12 | `FechaFin` | `datetime` | ✓ |  |
| 13 | `Solicita` | `nvarchar(50)` | ✓ |  |
| 14 | `Garantia` | `bit` | ✓ |  |
| 15 | `CostoServicio` | `float` | ✓ |  |
| 16 | `TipoServicio` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_montacarga_servicio_enc` | CLUSTERED · **PK** | IdServicioEnc |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)

