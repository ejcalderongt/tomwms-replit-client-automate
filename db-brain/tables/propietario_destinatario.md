---
id: db-brain-table-propietario-destinatario
type: db-table
title: dbo.propietario_destinatario
schema: dbo
name: propietario_destinatario
kind: table
rows: 1
modify_date: 2016-07-25
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.propietario_destinatario`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2016-07-25 |
| Columnas | 9 |
| Índices | 1 |
| FKs | out:0 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdDestinatarioPropietario` | `int` |  |  |
| 2 | `IdPropietario` | `int` | ✓ |  |
| 3 | `nombre` | `nvarchar(50)` | ✓ |  |
| 4 | `apellido` | `nvarchar(50)` | ✓ |  |
| 5 | `correo_electronico` | `nvarchar(50)` | ✓ |  |
| 6 | `telefono` | `nvarchar(50)` | ✓ |  |
| 7 | `telefono1` | `nvarchar(50)` | ✓ |  |
| 8 | `cargo` | `nvarchar(50)` | ✓ |  |
| 9 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_propietario_destinatario` | CLUSTERED · **PK** | IdDestinatarioPropietario |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `propietario_reglas_det` (`FK_propietario_reglas_det_propietario_destinatario`)

## Quién la referencia

**2** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)

