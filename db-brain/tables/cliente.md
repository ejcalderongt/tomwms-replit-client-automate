---
id: db-brain-table-cliente
type: db-table
title: dbo.cliente
schema: dbo
name: cliente
status: activa
sources:
  - extracted_from: TOMWMS_KILLIOS_PRD @ 52.41.114.122,1437
  - extracted_at: 2026-04-27T01:21:57.791Z
referenced_by:
  # (sin entities en wms-brain todavía)
---

# `dbo.cliente`

| Atributo | Valor |
|---|---|
| Filas | 1.083 |
| Última modificación schema | 2025-09-04 |
| Columnas | 31 |
| Índices | 2 |
| Flags bit (parametrización) | 9 — ver `parametrizacion/flags-cliente` |

## Columnas

| # | Nombre | Tipo | Null | PK | Flag |
|---:|---|---|:-:|:-:|:-:|
| 1 | `IdCliente` | `int` |  |  |  |
| 2 | `IdEmpresa` | `int` |  |  |  |
| 3 | `IdPropietario` | `int` |  |  |  |
| 4 | `IdTipoCliente` | `int` |  |  |  |
| 5 | `IdUbicacionManufactura` | `int` | ✓ |  |  |
| 6 | `codigo` | `nvarchar(150)` | ✓ |  |  |
| 7 | `nombre_comercial` | `nvarchar(150)` | ✓ |  |  |
| 8 | `nombre_contacto` | `nvarchar(150)` | ✓ |  |  |
| 9 | `telefono` | `nvarchar(125)` | ✓ |  |  |
| 10 | `nit` | `nvarchar(125)` | ✓ |  |  |
| 11 | `direccion` | `nvarchar(250)` | ✓ |  |  |
| 12 | `correo_electronico` | `nvarchar(150)` | ✓ |  |  |
| 13 | `activo` | `bit` | ✓ |  | 🚩 |
| 14 | `realiza_manufactura` | `bit` | ✓ |  | 🚩 |
| 15 | `user_agr` | `nvarchar(25)` | ✓ |  |  |
| 16 | `fec_agr` | `datetime` | ✓ |  |  |
| 17 | `user_mod` | `nvarchar(25)` | ✓ |  |  |
| 18 | `fec_mod` | `datetime` | ✓ |  |  |
| 19 | `despachar_lotes_completos` | `bit` | ✓ |  | 🚩 |
| 20 | `sistema` | `bit` | ✓ |  | 🚩 |
| 21 | `es_bodega_recepcion` | `bit` | ✓ |  | 🚩 |
| 22 | `es_bodega_traslado` | `bit` | ✓ |  | 🚩 |
| 23 | `idubicacionvirtual` | `int` | ✓ |  |  |
| 25 | `referencia` | `nvarchar(25)` | ✓ |  |  |
| 26 | `control_ultimo_lote` | `bit` | ✓ |  | 🚩 |
| 27 | `control_calidad` | `bit` | ✓ |  | 🚩 |
| 28 | `IdUbicacionAbastecerCon` | `int` | ✓ |  |  |
| 29 | `IdBodegaAreaSAP` | `int` | ✓ |  |  |
| 30 | `es_proveedor` | `bit` |  |  | 🚩 |
| 31 | `Codigo_Empresa_ERP` | `nvarchar(50)` | ✓ |  |  |
| 32 | `IdProductoEstadoVirtual` | `int` | ✓ |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `IX_CodigoCliente` | NONCLUSTERED · UNIQUE | codigo |
| `PK_cliente` | CLUSTERED · **PK** · UNIQUE | IdCliente |

## Foreign Keys

### FKs salientes (esta tabla → otra)
- `FK_cliente_propietarios` → `propietarios`
- `FK_cliente_cliente_tipo` → `cliente_tipo`

### FKs entrantes (otra tabla → esta)
- `regla_vencimiento` (`FK__regla_ven__IdCli__00983610`)
- `configuracion_qa` (`FK_configuracion_qa_cliente`)
- `cliente_tiempos` (`FK_cliente_tiempos_cliente`)
- `cliente_bodega` (`FK_cliente_bodega_cliente`)
- `trans_pe_enc` (`FK_trans_pedido_enc_cliente`)

## Dependencias (quién la referencia)

**27 objetos** la referencian:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `SET_CLIENTES_REC` (stored_procedure)
- `v_trans_pedido` (view)
- `VW_Ajustes` (view)
- `VW_Cantidad_Pedidos_vrs_Despacho_Clientes` (view)
- `VW_Cliente` (view)
- `VW_Clientes_Tiempos` (view)
- `VW_Despacho_Rep` (view)
- `VW_Despacho_Rep_Det` (view)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Despacho_Rep_Res` (view)
- `VW_Get_Single_Pedido` (view)
- `vw_Indicador_Despachos` (view)
- `VW_Lotes_Despacho` (view)
- `VW_PackingDespachado` (view)
- `VW_Pedido` (view)
- `VW_Pedidos_IdPickingEnc` (view)
- `VW_Pedidos_List` (view)
- `VW_Picking_Det_By_IdPickingEnc` (view)
- `VW_Productividad_Picking` (view)
- `VW_Recepcion_Det` (view)
- `VW_Recepcion_Det_SAT` (view)
- `VW_TiempoCliente` (view)
- `VW_Tiempos_Despacho_By_IdPedidoEnc` (view)
- `VW_UbicacionPicking` (view)

## Notas

- 9 flags bit configurables. Ver `parametrizacion/flags-cliente`.
- 1083 filas en Killios (multi-cliente real, no es solo Killios mismo).
- PK `IdCliente` int. Índice único en `codigo`.
- Tres flags clave para lotes: `despachar_lotes_completos`, `control_ultimo_lote`. **Ambos en 0 para Killios** (validado 2026-04-27).
