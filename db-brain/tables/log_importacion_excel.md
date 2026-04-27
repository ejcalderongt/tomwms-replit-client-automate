---
id: db-brain-table-log-importacion-excel
type: db-table
title: dbo.log_importacion_excel
schema: dbo
name: log_importacion_excel
status: vacia
sources:
  - extracted_from: TOMWMS_KILLIOS_PRD @ 52.41.114.122,1437
  - extracted_at: 2026-04-27T01:21:57.791Z
referenced_by:
  - wms-brain://entities/modules/mod-importacion-excel
  - wms-brain://entities/cases/case-2026-04-importar-lotes-cliente
---

# `dbo.log_importacion_excel`

| Atributo | Valor |
|---|---|
| Filas | 0 |
| Última modificación schema | 2022-05-06 |
| Columnas | 6 |
| Índices | 1 |
| Flags bit (parametrización) | 0 |

## Columnas

| # | Nombre | Tipo | Null | PK | Flag |
|---:|---|---|:-:|:-:|:-:|
| 1 | `IdImportacion` | `int` |  |  |  |
| 2 | `IdEmpresa` | `int` | ✓ |  |  |
| 3 | `IdBodega` | `int` | ✓ |  |  |
| 4 | `IdUsuario` | `int` | ✓ |  |  |
| 5 | `hash_archivo` | `nvarchar(150)` | ✓ |  |  |
| 6 | `fecha` | `datetime` | ✓ |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_log_importacion_excel` | CLUSTERED · **PK** · UNIQUE | IdImportacion |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes). Convención general del WMS — integridad por DAL VB.NET.

## Dependencias (quién la referencia)

**Ninguna referencia desde SPs/vistas/funciones.** Tabla **vacía** + sin referencias = candidata a feature en desarrollo o legacy sin uso.

## Notas

- Tabla **vacía** (0 filas), última modify_date **2022-05-06** → patrón staging Excel diseñado pero **sin uso productivo** en Killios.
- 6 columnas mínimas: `IdImportacion` PK, `IdEmpresa`, `IdBodega`, `IdUsuario`, `hash_archivo` nvarchar(300), `fecha`.
- Se complementa con clases por feature (`clsBeLog_importacion_excel_<X>`).
- Patrón completo: `wms-brain://entities/modules/mod-importacion-excel`.
