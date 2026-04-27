---
id: db-brain-table-i-nav-config-enc
type: db-table
title: dbo.i_nav_config_enc
schema: dbo
name: i_nav_config_enc
kind: table
rows: 6
modify_date: 2026-02-12
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.i_nav_config_enc`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 6 |
| Schema modify_date | 2026-02-12 |
| Columnas | 69 |
| Índices | 1 |
| FKs | out:4 in:1 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `idnavconfigenc` | `int` |  |  |
| 2 | `idempresa` | `int` |  |  |
| 3 | `idbodega` | `int` |  |  |
| 4 | `idPropietario` | `int` | ✓ |  |
| 5 | `idUsuario` | `int` | ✓ |  |
| 6 | `nombre` | `varchar(256)` | ✓ |  |
| 7 | `fec_agr` | `datetime` | ✓ |  |
| 8 | `user_agr` | `nvarchar(50)` | ✓ |  |
| 9 | `fec_mod` | `datetime` | ✓ |  |
| 10 | `user_mod` | `nvarchar(50)` | ✓ |  |
| 11 | `IdProductoEstado` | `int` | ✓ |  |
| 12 | `rechazar_pedido_incompleto` | `int` | ✓ |  |
| 13 | `despachar_existencia_parcial` | `int` | ✓ |  |
| 14 | `convertir_decimales_a_umbas` | `int` | ✓ |  |
| 15 | `generar_pedido_ingreso_bodega_destino` | `bit` | ✓ |  |
| 16 | `generar_recepcion_auto_bodega_destino` | `bit` | ✓ |  |
| 17 | `codigo_proveedor_produccion` | `nvarchar(50)` | ✓ |  |
| 18 | `idFamilia` | `int` | ✓ |  |
| 19 | `idclasificacion` | `int` | ✓ |  |
| 20 | `idMarca` | `int` | ✓ |  |
| 21 | `idTipoProducto` | `int` | ✓ |  |
| 22 | `control_lote` | `bit` | ✓ |  |
| 23 | `control_vencimiento` | `bit` | ✓ |  |
| 24 | `genera_lp` | `bit` | ✓ |  |
| 25 | `nombre_ejecutable` | `nvarchar(50)` | ✓ |  |
| 26 | `IdTipoDocumentoTransferenciasIngreso` | `int` | ✓ |  |
| 27 | `crear_recepcion_de_transferencia_nav` | `bit` | ✓ |  |
| 28 | `IdTipoEtiqueta` | `int` | ✓ |  |
| 29 | `equiparar_cliente_con_propietario_en_doc_salida` | `bit` |  |  |
| 30 | `control_peso` | `bit` | ✓ |  |
| 31 | `crear_recepcion_de_compra_nav` | `bit` | ✓ |  |
| 32 | `IdAcuerdoEnc` | `int` | ✓ |  |
| 33 | `push_ingreso_nav_desde_hh` | `bit` | ✓ |  |
| 34 | `reservar_umbas_primero` | `bit` |  |  |
| 35 | `implosion_automatica` | `bit` |  |  |
| 36 | `explosion_automatica` | `bit` |  |  |
| 37 | `Ejecutar_En_Despacho_Automaticamente` | `bit` |  |  |
| 38 | `IdTipoRotacion` | `int` | ✓ |  |
| 40 | `explosio_automatica_nivel_max` | `int` | ✓ |  |
| 41 | `explosion_automatica_desde_ubicacion_picking` | `bit` | ✓ |  |
| 42 | `explosion_automatica_nivel_max` | `int` | ✓ |  |
| 43 | `conservar_zona_picking_clavaud` | `bit` | ✓ |  |
| 44 | `excluir_ubicaciones_reabasto` | `bit` |  |  |
| 45 | `considerar_paletizado_en_reabasto` | `bit` |  |  |
| 46 | `considerar_disponibilidad_ubicacion_reabasto` | `bit` |  |  |
| 47 | `dias_vida_defecto_perecederos` | `int` |  |  |
| 48 | `codigo_bodega_nc_erp` | `nvarchar(50)` | ✓ |  |
| 49 | `lote_defecto_nc` | `nvarchar(50)` | ✓ |  |
| 50 | `vence_defecto_nc` | `datetime` | ✓ |  |
| 51 | `Codigo_Bodega_ERP_NC` | `nvarchar(50)` | ✓ |  |
| 52 | `Lote_Defecto_Entrada_NC` | `nvarchar(50)` | ✓ |  |
| 53 | `IdProductoEstado_NC` | `int` | ✓ |  |
| 54 | `interface_sap` | `bit` |  |  |
| 55 | `sap_control_draft_ajustes` | `bit` |  |  |
| 56 | `sap_control_draft_traslados` | `bit` |  |  |
| 57 | `IdIndiceRotacion` | `int` | ✓ |  |
| 58 | `Rango_Dias_Importacion` | `int` | ✓ |  |
| 60 | `inferir_bonificacion_pedido_sap` | `bit` |  |  |
| 61 | `rechazar_bonificacion_incompleta` | `bit` |  |  |
| 62 | `equiparar_productos` | `bit` |  |  |
| 63 | `bodega_facturacion` | `nvarchar(50)` | ✓ |  |
| 64 | `valida_solo_codigo` | `bit` |  |  |
| 65 | `excluir_recepcion_picking` | `bit` |  |  |
| 66 | `bodega_prorrateo` | `nvarchar(50)` | ✓ |  |
| 67 | `bodega_prorrateo1` | `nvarchar(50)` | ✓ |  |
| 68 | `centro_costo_erp` | `int` | ✓ |  |
| 69 | `centro_costo_dir_erp` | `int` | ✓ |  |
| 70 | `centro_costo_dep_erp` | `int` | ✓ |  |
| 71 | `bodega_faltante` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_i_nav_config_enc_1` | CLUSTERED · **PK** | idnavconfigenc |

## Foreign Keys

### Salientes (esta tabla → otra)

- `FK_i_nav_config_enc_bodega` → `bodega`
- `FK_i_nav_config_enc_empresa` → `empresa`
- `FK_i_nav_config_enc_propietarios` → `propietarios`
- `FK_i_nav_config_enc_usuario` → `usuario`

### Entrantes (otra tabla → esta)

- `i_nav_config_det` (`FK_i_nav_config_det_i_nav_config_enc`)

## Quién la referencia

**1** objetos:

- `VW_Configuracioninv` (view)

