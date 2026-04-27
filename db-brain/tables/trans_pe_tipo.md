---
id: db-brain-table-trans-pe-tipo
type: db-table
title: dbo.trans_pe_tipo
schema: dbo
name: trans_pe_tipo
kind: table
rows: 6
modify_date: 2025-08-01
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.trans_pe_tipo`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 6 |
| Schema modify_date | 2025-08-01 |
| Columnas | 30 |
| Índices | 1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdTipoPedido` | `int` |  |  |
| 2 | `Nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `Descripcion` | `nvarchar(250)` | ✓ |  |
| 4 | `Preparar` | `bit` | ✓ |  |
| 5 | `Verificar` | `bit` | ✓ |  |
| 6 | `ReservaStock` | `bit` | ✓ |  |
| 7 | `ImprimeBarrasPicking` | `bit` | ✓ |  |
| 8 | `ImprimeBarrasPacking` | `bit` | ✓ |  |
| 9 | `control_poliza` | `bit` | ✓ |  |
| 10 | `requerir_documento_ref` | `bit` | ✓ |  |
| 11 | `Generar_pedido_ingreso_bodega_destino` | `bit` | ✓ |  |
| 12 | `IdTipoIngresoOC` | `int` | ✓ |  |
| 13 | `trasladar_lotes_doc_ingreso` | `bit` | ✓ |  |
| 14 | `activo` | `bit` | ✓ |  |
| 15 | `requerir_cliente_es_bodega_wms` | `bit` | ✓ |  |
| 16 | `marcar_registros_enviados_mi3` | `bit` | ✓ |  |
| 17 | `generar_recepcion_auto_bodega_destino` | `bit` |  |  |
| 18 | `recibir_producto_auto_bodega_destino` | `bit` |  |  |
| 19 | `control_cliente_en_detalle` | `bit` |  |  |
| 20 | `permitir_despacho_parcial` | `bit` |  |  |
| 21 | `permitir_despacho_multiple` | `bit` |  |  |
| 22 | `fotografia_verificacion` | `bit` |  |  |
| 23 | `es_devolucion` | `bit` |  |  |
| 24 | `empaque_tarima` | `bit` |  |  |
| 25 | `IdProductoEstado` | `int` | ✓ |  |
| 26 | `IdPropietario` | `int` | ✓ |  |
| 27 | `mover_producto_zona_muelle` | `bit` |  |  |
| 28 | `escanear_muelle_picking` | `bit` |  |  |
| 29 | `transferir_ubicacion` | `bit` |  |  |
| 30 | `genera_guia_remision` | `bit` |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_trans_pe_tipo` | CLUSTERED · **PK** | IdTipoPedido |

## Foreign Keys

**Sin FKs declaradas** (ni entrantes ni salientes).

## Quién la referencia

**10** objetos:

- `CLBD` (stored_procedure)
- `VW_Get_Single_Pedido` (view)
- `vw_Indicador_Despachos` (view)
- `vw_Indicador_Picking` (view)
- `vw_Indicador_Verificaciones` (view)
- `VW_PE_CON_DIFERENCIAS` (view)
- `VW_Pedidos_List` (view)
- `VW_Productividad_Picking` (view)
- `VW_Progreso_Picking_By_Operador` (view)
- `VW_Tiempos_Picking_Operador` (view)

