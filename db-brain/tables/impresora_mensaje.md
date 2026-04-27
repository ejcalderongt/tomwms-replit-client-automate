---
id: db-brain-table-impresora-mensaje
type: db-table
title: dbo.impresora_mensaje
schema: dbo
name: impresora_mensaje
kind: table
rows: 0
modify_date: 2025-03-27
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.impresora_mensaje`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2025-03-27 |
| Columnas | 7 |
| Índices | 1 |
| FKs | out:2 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdImpresoraMensaje` | `int` |  |  |
| 2 | `IdImpresora` | `int` | ✓ |  |
| 3 | `mensaje` | `nvarchar(100)` | ✓ |  |
| 4 | `IdMensaje` | `int` | ✓ |  |
| 5 | `host` | `nvarchar(50)` | ✓ |  |
| 6 | `user_agr` | `int` | ✓ |  |
| 7 | `fec_agr` | `datetime` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_impresora_mensaje` | CLUSTERED · **PK** | IdImpresoraMensaje |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_impresora_mensaje_impresora` → `impresora`
- `FK_impresora_mensaje_usuario` → `usuario`

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

