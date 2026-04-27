---
id: db-brain-table-configuracion-qa
type: db-table
title: dbo.configuracion_qa
schema: dbo
name: configuracion_qa
kind: table
rows: 0
modify_date: 2023-10-05
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.configuracion_qa`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2023-10-05 |
| Columnas | 17 |
| Índices | 1 |
| FKs | out:4 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdConfiguracionQA` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `FechaEjecucion` | `datetime` | ✓ |  |
| 4 | `IdEmpresaOrigen` | `int` | ✓ |  |
| 5 | `IdBodegaOrigen` | `int` | ✓ |  |
| 6 | `IdPropietarioOrigen` | `int` | ✓ |  |
| 7 | `IdProducto` | `int` | ✓ |  |
| 8 | `IdCliente` | `int` | ✓ |  |
| 9 | `Cantidad_Pedido_Presentacion` | `float` | ✓ |  |
| 10 | `Cantidad_Pedido_UMBas` | `float` | ✓ |  |
| 11 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 12 | `fec_agr` | `datetime` | ✓ |  |
| 13 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 14 | `fec_mod` | `datetime` | ✓ |  |
| 15 | `activo` | `bit` |  |  |
| 16 | `Resultado` | `nvarchar(250)` | ✓ |  |
| 17 | `Observaciones` | `nvarchar(250)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_configuracion_qa` | CLUSTERED · **PK** | IdConfiguracionQA |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_configuracion_qa_bodega` → `bodega`
- `FK_configuracion_qa_cliente` → `cliente`
- `FK_configuracion_qa_empresa` → `empresa`
- `FK_configuracion_qa_producto` → `producto`

## Quién la referencia

_Ninguna referencia desde SPs/vistas/funciones._

