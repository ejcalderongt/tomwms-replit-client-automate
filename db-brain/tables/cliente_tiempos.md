---
id: db-brain-table-cliente-tiempos
type: db-table
title: dbo.cliente_tiempos
schema: dbo
name: cliente_tiempos
kind: table
rows: 0
modify_date: 2025-03-18
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.cliente_tiempos`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 0 |
| Schema modify_date | 2025-03-18 |
| Columnas | 11 |
| Índices | 2 |
| FKs | out:3 in:0 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTiempoCliente` | `int` |  |  |
| 2 | `IdCliente` | `int` |  |  |
| 3 | `IdFamilia` | `int` | ✓ |  |
| 4 | `IdClasificacion` | `int` | ✓ |  |
| 5 | `Dias_Local` | `int` | ✓ |  |
| 6 | `Dias_Exterior` | `int` | ✓ |  |
| 7 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 8 | `fec_agr` | `datetime` | ✓ |  |
| 9 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 10 | `fec_mod` | `datetime` | ✓ |  |
| 11 | `activo` | `bit` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_cliente_tiempos_1` | CLUSTERED · **PK** | IdTiempoCliente |
| `NCI_Cliente_Tiempos_CKFK20250303` | NONCLUSTERED | activo, fec_mod, user_mod, fec_agr, user_agr, Dias_Exterior, Dias_Local, IdClasificacion, IdFamilia, IdCliente |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_cliente_tiempos_cliente` → `cliente`
- `FK_cliente_tiempos_producto_clasificacion` → `producto_clasificacion`
- `FK_cliente_tiempos_producto_familia` → `producto_familia`

## Quién la referencia

**8** objetos:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `fdias_Exterior_by_IdCliente` (scalar_function)
- `fdias_locales_by_IdCliente` (scalar_function)
- `VW_Clientes_Tiempos` (view)
- `VW_Stock_Especifico` (view)
- `VW_Stock_Por_Producto_Ubicacion_CI` (view)
- `VW_TiempoCliente` (view)

