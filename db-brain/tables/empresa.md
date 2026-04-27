---
id: db-brain-table-empresa
type: db-table
title: dbo.empresa
schema: dbo
name: empresa
kind: table
rows: 1
modify_date: 2023-10-05
extracted_at: 2026-04-27T01:29:47.537Z
extracted_from: TOMWMS_KILLIOS_PRD
---
# `dbo.empresa`

| Atributo | Valor |
|---|---|
| Tipo | USER_TABLE |
| Filas | 1 |
| Schema modify_date | 2023-10-05 |
| Columnas | 36 |
| Índices | 1 |
| FKs | out:0 in:16 |

## Columnas

| # | Nombre | Tipo | Null | Ident |
|---:|---|---|:-:|:-:|
| 1 | `IdEmpresa` | `int` |  |  |
| 2 | `nombre` | `nvarchar(50)` | ✓ |  |
| 3 | `direccion` | `nvarchar(250)` | ✓ |  |
| 4 | `telefono` | `nvarchar(50)` | ✓ |  |
| 5 | `email` | `nvarchar(50)` | ✓ |  |
| 6 | `razon_social` | `nvarchar(50)` | ✓ |  |
| 7 | `representante` | `nvarchar(50)` | ✓ |  |
| 8 | `corr_cod_barra` | `bigint` | ✓ |  |
| 9 | `path_printer` | `nvarchar(500)` | ✓ |  |
| 10 | `activo` | `bit` | ✓ |  |
| 11 | `user_agr` | `nvarchar(25)` | ✓ |  |
| 12 | `fec_agr` | `datetime` | ✓ |  |
| 13 | `user_mod` | `nvarchar(25)` | ✓ |  |
| 14 | `fec_mod` | `datetime` | ✓ |  |
| 15 | `clienteRapido` | `bit` | ✓ |  |
| 16 | `imagen` | `image` | ✓ |  |
| 17 | `operador_logistico` | `bit` | ✓ |  |
| 18 | `puerto_escaner` | `int` | ✓ |  |
| 19 | `control_presentaciones` | `bit` | ✓ |  |
| 20 | `anulaciones_por_supervisor` | `bit` | ✓ |  |
| 21 | `codigo` | `nvarchar(50)` | ✓ |  |
| 22 | `clave` | `nvarchar(50)` | ✓ |  |
| 23 | `intento` | `int` | ✓ |  |
| 24 | `duracionclave` | `int` | ✓ |  |
| 25 | `duracionclavetemporal` | `int` | ✓ |  |
| 26 | `codigo_automatico` | `bit` | ✓ |  |
| 27 | `politica_contraseñas` | `bit` | ✓ |  |
| 28 | `IdMotivoAjusteInventario` | `int` | ✓ |  |
| 29 | `cantidad_decimales_despliegue` | `int` | ✓ |  |
| 30 | `cantidad_decimales_calculo` | `int` | ✓ |  |
| 31 | `minutos_timer_jornada_sistema` | `float` | ✓ |  |
| 32 | `hora_corte_jornada_sistema` | `datetime` | ✓ |  |
| 33 | `generar_stock_jornada` | `bit` |  |  |
| 34 | `buscar_actualizacion_hh` | `bit` |  |  |
| 35 | `version_bd` | `nvarchar(10)` | ✓ |  |
| 36 | `aws_token` | `nvarchar(50)` | ✓ |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `PK_empresa` | CLUSTERED · **PK** | IdEmpresa |

## Foreign Keys

### Entrantes (otra tabla → esta)

- `bodega` (`FK_bodega_empresa`)
- `configuracion_qa` (`FK_configuracion_qa_empresa`)
- `empresa_transporte` (`FK_empresa_transporte_empresa`)
- `i_nav_config_enc` (`FK_i_nav_config_enc_empresa`)
- `impresora` (`FK_impresora_empresa`)
- `motivo_anulacion` (`FK_motivo_anulacion_empresa`)
- `motivo_devolucion` (`FK_motivo_devolucion_empresa`)
- `motivo_ubicacion` (`FK_motivo_ubicacion_empresa`)
- `operador` (`FK_operador_empresa`)
- `propietarios` (`FK_propietarios_empresa`)
- `proveedor` (`FK_proveedor_empresa`)
- `rol` (`FK_rol_empresa`)
- `tarimas` (`FK_tarimas_empresa`)
- `trans_servicio_enc` (`FK_trans_servicio_enc_empresa`)
- `transacciones_log` (`FK_transacciones_log_empresa`)
- `usuario` (`FK_usuario_empresa`)

## Quién la referencia

**14** objetos:

- `CLBD` (stored_procedure)
- `VW_Bodega` (view)
- `VW_Cliente` (view)
- `VW_Configuracioninv` (view)
- `VW_Impresion_Pallet` (view)
- `VW_Impresion_Pallet_Rec` (view)
- `VW_MotivoAnulacion` (view)
- `VW_MotivoAnulacionBodega` (view)
- `VW_MotivoDevolucion` (view)
- `VW_Operador` (view)
- `VW_Packing` (view)
- `VW_Propietario` (view)
- `VW_Proveedor` (view)
- `VW_ProveedorBodega` (view)

