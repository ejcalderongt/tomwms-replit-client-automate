---
id: db-brain-table-marcaje
type: db-table
title: dbo.marcaje
schema: dbo
name: marcaje
kind: table
rows: 3701
modify_date: 2022-12-17
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.marcaje`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 3.701 |
| Schema modify_date | 2022-12-17 |
| Columnas | 24 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdMarcaje` | `int` |  |  |
| 2 | `IdEmpresa` | `int` |  |  |
| 3 | `IdBodega` | `int` |  |  |
| 4 | `IdOperador` | `int` |  |  |
| 5 | `IdDispositivo` | `nvarchar(50)` |  |  |
| 6 | `IdHorarioLaboral` | `int` |  |  |
| 7 | `Fec_lectura` | `date` | ✓ |  |
| 8 | `Hora_inicio_horario` | `datetime` | ✓ |  |
| 9 | `Hora_fin_horario` | `datetime` | ✓ |  |
| 10 | `Ingreso_anticipado` | `bit` | ✓ |  |
| 11 | `Salida_anticipada` | `bit` | ✓ |  |
| 12 | `Ingreso_tardio` | `bit` | ✓ |  |
| 13 | `Salida_tardia` | `bit` | ✓ |  |
| 14 | `Hora_lectura` | `datetime` | ✓ |  |
| 15 | `Entro` | `bit` |  |  |
| 16 | `Salio` | `bit` |  |  |
| 17 | `Hora_entro` | `datetime` | ✓ |  |
| 18 | `Hora_salio` | `datetime` | ✓ |  |
| 19 | `Marcaje_manual` | `bit` | ✓ |  |
| 20 | `Primer_marcaje` | `int` | ✓ |  |
| 21 | `Marcaje_contabilizado` | `bit` | ✓ |  |
| 22 | `Marcaje_aproximado` | `bit` | ✓ |  |
| 23 | `Marcaje_fuera_de_sucursal` | `bit` | ✓ |  |
| 24 | `Es_bitacora` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_marcaje_1` | CLUSTERED · **PK** | IdMarcaje |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

