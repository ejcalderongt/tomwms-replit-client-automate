---
id: db-brain-table-cliente-lotes
type: db-table
title: dbo.cliente_lotes
schema: dbo
name: cliente_lotes
status: vacia
sources:
  - extracted_from: TOMWMS_KILLIOS_PRD @ 52.41.114.122,1437
  - extracted_at: 2026-04-27T01:21:57.791Z
referenced_by:
  - wms-brain://entities/modules/mod-cliente-lotes
  - wms-brain://entities/cases/case-2026-04-importar-lotes-cliente
---

# `dbo.cliente_lotes`

| Atributo | Valor |
|---|---|
| Filas | 0 |
| Última modificación schema | 2025-07-13 |
| Columnas | 11 |
| Índices | 1 |
| Flags bit (parametrización) | 0 |

## Columnas

| # | Nombre | Tipo | Null | PK | Flag |
|---:|---|---|:-:|:-:|:-:|
| 1 | `IdClienteLote` | `int` |  |  |  |
| 2 | `IdCliente` | `int` |  |  |  |
| 3 | `Lote` | `nvarchar(50)` | ✓ |  |  |
| 4 | `IdProductoEstado` | `int` | ✓ |  |  |
| 5 | `user_agr` | `nvarchar(25)` | ✓ |  |  |
| 6 | `fec_agr` | `datetime` | ✓ |  |  |
| 7 | `user_mod` | `nvarchar(25)` | ✓ |  |  |
| 8 | `fec_mod` | `datetime` | ✓ |  |  |
| 9 | `activo` | `bit` | ✓ |  |  |
| 10 | `bloquear` | `bit` | ✓ |  |  |
| 11 | `IdProducto` | `int` |  |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_cliente_lote` | CLUSTERED · **PK** · UNIQUE | IdClienteLote |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes). Convención general del WMS — integridad por DAL VB.NET.

## Dependencias (quién la referencia)

**Ninguna referencia desde SPs/vistas/funciones.** Tabla **vacía** + sin referencias = candidata a feature en desarrollo o legacy sin uso.

## Notas

- Tabla **vacía** (0 filas) y **0 dependencias SQL** → confirma que es feature en desarrollo (sin uso productivo aún).
- Existen DAL+Entity en BOF (`clsBeCliente_lotes.vb` 24 líneas, `clsLnCliente_lotes.vb` 458 líneas) pero **NO existe form** que la consuma.
- HH no tiene 0 referencias.
- Caso de uso pendiente: importar lotes por cliente vía Excel. Detalle: `wms-brain://entities/cases/case-2026-04-importar-lotes-cliente`.
