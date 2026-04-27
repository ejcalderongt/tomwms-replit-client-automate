---
id: db-brain-table-reglas-vencimiento-contacto
type: db-table
title: dbo.reglas_vencimiento_contacto
schema: dbo
name: reglas_vencimiento_contacto
kind: table
rows: 0
modify_date: 2024-02-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.reglas_vencimiento_contacto`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2024-02-01 |
| Columnas | 10 |
| Índices | 1 |
| FKs | out:1 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdContacto` | `int` |  | ✓ |
| 2 | `NombreContacto` | `nvarchar(255)` | ✓ |  |
| 3 | `CorreoElectronico` | `nvarchar(255)` | ✓ |  |
| 4 | `TelefonoFijo` | `nvarchar(50)` | ✓ |  |
| 5 | `TelefonoMovil` | `nvarchar(50)` | ✓ |  |
| 6 | `IdReglaVencimiento` | `int` |  |  |
| 7 | `FechaCreacion` | `datetime` |  |  |
| 8 | `UsuarioCreacion` | `nvarchar(255)` | ✓ |  |
| 9 | `FechaModificacion` | `datetime` | ✓ |  |
| 10 | `UsuarioModificacion` | `nvarchar(255)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK__reglas_v__A4D6BBFAFA1AD45F` | CLUSTERED · **PK** | IdContacto |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK__reglas_ve__IdReg__0374A2BB` → `regla_vencimiento`

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

