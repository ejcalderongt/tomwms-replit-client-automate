---
id: db-brain-table-producto
type: db-table
title: dbo.producto
schema: dbo
name: producto
status: activa
sources:
  - extracted_from: TOMWMS_KILLIOS_PRD @ 52.41.114.122,1437
  - extracted_at: 2026-04-27T01:21:57.791Z
referenced_by:
  # (sin entities en wms-brain todavía)
---

# `dbo.producto`

| Atributo | Valor |
|---|---|
| Filas | 319 |
| Última modificación schema | 2025-05-13 |
| Columnas | 60 |
| Índices | 7 |
| Flags bit (parametrización) | 17 — ver `parametrizacion/flags-producto` |

## Columnas

| # | Nombre | Tipo | Null | PK | Flag |
|---:|---|---|:-:|:-:|:-:|
| 1 | `IdProducto` | `int` |  |  |  |
| 2 | `IdPropietario` | `int` |  |  |  |
| 3 | `IdClasificacion` | `int` | ✓ |  |  |
| 4 | `IdFamilia` | `int` | ✓ |  |  |
| 5 | `IdMarca` | `int` | ✓ |  |  |
| 6 | `IdTipoProducto` | `int` | ✓ |  |  |
| 7 | `IdUnidadMedidaBasica` | `int` | ✓ |  |  |
| 8 | `IdCamara` | `int` | ✓ |  |  |
| 9 | `IdTipoRotacion` | `int` | ✓ |  |  |
| 10 | `IdPerfilSerializado` | `int` | ✓ |  |  |
| 11 | `IdIndiceRotacion` | `int` | ✓ |  |  |
| 12 | `IdSimbologia` | `int` | ✓ |  |  |
| 13 | `IdArancel` | `int` | ✓ |  |  |
| 14 | `codigo` | `nvarchar(50)` | ✓ |  |  |
| 15 | `nombre` | `nvarchar(100)` | ✓ |  |  |
| 16 | `codigo_barra` | `nvarchar(35)` | ✓ |  |  |
| 17 | `precio` | `float` | ✓ |  |  |
| 18 | `existencia_min` | `float` | ✓ |  |  |
| 19 | `existencia_max` | `float` | ✓ |  |  |
| 20 | `costo` | `float` | ✓ |  |  |
| 21 | `peso_referencia` | `float` | ✓ |  |  |
| 22 | `peso_tolerancia` | `float` | ✓ |  |  |
| 23 | `temperatura_referencia` | `float` | ✓ |  |  |
| 24 | `temperatura_tolerancia` | `float` | ✓ |  |  |
| 25 | `activo` | `bit` | ✓ |  | 🚩 |
| 26 | `serializado` | `bit` | ✓ |  | 🚩 |
| 27 | `genera_lote` | `bit` | ✓ |  | 🚩 |
| 28 | `genera_lp_old` | `bit` | ✓ |  | 🚩 |
| 29 | `control_vencimiento` | `bit` | ✓ |  | 🚩 |
| 30 | `control_lote` | `bit` | ✓ |  | 🚩 |
| 31 | `peso_recepcion` | `bit` | ✓ |  | 🚩 |
| 32 | `peso_despacho` | `bit` | ✓ |  | 🚩 |
| 33 | `temperatura_recepcion` | `bit` | ✓ |  | 🚩 |
| 34 | `temperatura_despacho` | `bit` | ✓ |  | 🚩 |
| 35 | `materia_prima` | `bit` | ✓ |  | 🚩 |
| 36 | `kit` | `bit` | ✓ |  | 🚩 |
| 37 | `tolerancia` | `int` | ✓ |  |  |
| 38 | `ciclo_vida` | `int` | ✓ |  |  |
| 39 | `user_agr` | `nvarchar(50)` | ✓ |  |  |
| 40 | `fec_agr` | `datetime` | ✓ |  |  |
| 41 | `user_mod` | `nvarchar(50)` | ✓ |  |  |
| 42 | `fec_mod` | `datetime` | ✓ |  |  |
| 43 | `imagen` | `image` | ✓ |  |  |
| 44 | `noserie` | `nvarchar(50)` | ✓ |  |  |
| 45 | `noparte` | `nvarchar(50)` | ✓ |  |  |
| 46 | `fechamanufactura` | `bit` | ✓ |  | 🚩 |
| 47 | `capturar_aniada` | `bit` | ✓ |  | 🚩 |
| 48 | `control_peso` | `bit` | ✓ |  | 🚩 |
| 49 | `captura_arancel` | `bit` | ✓ |  | 🚩 |
| 50 | `es_hardware` | `bit` | ✓ |  | 🚩 |
| 51 | `largo` | `float` | ✓ |  |  |
| 52 | `alto` | `float` | ✓ |  |  |
| 53 | `ancho` | `float` | ✓ |  |  |
| 54 | `IdUnidadMedidaCobro` | `int` | ✓ |  |  |
| 55 | `IdTipoEtiqueta` | `int` | ✓ |  |  |
| 56 | `dias_inventario_promedio` | `int` | ✓ |  |  |
| 57 | `IDPRODUCTOPARAMETROA` | `int` | ✓ |  |  |
| 58 | `IDPRODUCTOPARAMETROB` | `int` | ✓ |  |  |
| 59 | `IdTipoManufactura` | `int` | ✓ |  |  |
| 60 | `margen_impresion` | `float` |  |  |  |

## Índices

| Nombre | Tipo | Columnas |
|---|---|---|
| `IX_producto` | NONCLUSTERED · UNIQUE | codigo |
| `NCL_PRODUCTO_20191104` | NONCLUSTERED | IdProducto, codigo, nombre, IdPropietario |
| `NCL_Producto_20191122_EJC` | NONCLUSTERED | codigo, nombre, IdPropietario |
| `NCL_Producto_20200115_ejc` | NONCLUSTERED | codigo, nombre, codigo_barra, IdUnidadMedidaBasica |
| `NCLI_Producto_20191210_EJC` | NONCLUSTERED | IdFamilia, codigo, nombre, codigo_barra, IdClasificacion |
| `NCLI_Producto_20191210A_EJC` | NONCLUSTERED | IdProducto, codigo, nombre, IdPropietario |
| `PK_producto_1` | CLUSTERED · **PK** · UNIQUE | IdProducto |

## Foreign Keys

### FKs salientes (esta tabla → otra)
- `FK_producto_unidad_medida` → `unidad_medida`
- `FK_producto_perfil_serializado` → `perfil_serializado`
- `FK_producto_simbologias_codigo_barra` → `simbologias_codigo_barra`
- `FK_producto_camara` → `camara`
- `FK_producto_propietarios` → `propietarios`
- `FK_producto_producto_familia` → `producto_familia`
- `FK_producto_producto_marca` → `producto_marca`
- `FK_producto_producto_clasificacion` → `producto_clasificacion`
- `FK_producto_producto_tipo` → `producto_tipo`
- `FK_producto_Arancel` → `arancel`
- `FK_producto_tipo_rotacion` → `tipo_rotacion`
- `FK_producto_indice_rotacion` → `indice_rotacion`

### FKs entrantes (otra tabla → esta)
- `configuracion_qa` (`FK_configuracion_qa_producto`)
- `trans_tras_det` (`FK_trans_tras_det_producto`)
- `producto_presentacion` (`FK_producto_presentacion_producto`)
- `producto_parametros` (`FK_producto_parametros_producto`)
- `producto_bodega` (`FK_producto_bodega_producto`)
- `producto_sustituto` (`FK_producto_sustituto_producto`)
- `producto_sustituto` (`FK_producto_sustituto_producto1`)
- `producto_talla_color` (`FK__producto___IdPro__57B61C0E`)

## Dependencias (quién la referencia)

**119 objetos** la referencian:

- `CLBD` (stored_procedure)
- `CLBD_INICIARBD` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion` (stored_procedure)
- `SP_Importa_Stock_Bodegas_General_y_Dañado_Actualizacion_Sin_Importacion` (stored_procedure)
- `VW_Ajustes` (view)
- `VW_CalculoVencimientos` (view)
- `VW_Cambios_Estado` (view)
- `VW_Cambios_Ubicacion` (view)
- `VW_CodigoBarra_OC` (view)
- `VW_Comparativo_NAV_WMS_ConCostos` (view)
- `VW_Conteo_By_Operador` (view)
- `VW_ControlCalidad_CambioEstado` (view)
- `VW_Despacho_Detalle` (view)
- `VW_Despacho_Rep` (view)
- `VW_Despacho_Rep_Det` (view)
- `VW_Despacho_Rep_Det_I` (view)
- `VW_Despacho_Rep_DyD` (view)
- `VW_Despacho_Rep_Res` (view)
- `VW_Detalle_Licencias_Inconsistentes` (view)
- `VW_EstacionalidadProducto` (view)
- `VW_Existencia_Valores_Fiscales` (view)
- `vw_existencias_producto_categoria` (view)
- `VW_ExistenciasPorNoDocumento` (view)
- `VW_Fiscal_Merca_Vencida` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Consolidado` (view)
- `VW_Get_All_PickingUbic_By_IdPickingEnc_Detallado` (view)
- `VW_Get_All_Stock_Detalle_Resumen` (view)
- `VW_Get_Detalle_By_IdRecepcionEnc` (view)
- `VW_Impresion_Pallet` (view)

_... +89 más_

## Notas

- 17 flags bit — el maestro más configurable después de `bodega` (~57 flags).
- 319 productos en Killios.
- Killios actual: **318/319 con `control_lote=1`**, **0/319 con `genera_lote=1`** → trazan lote pero NO autogeneran (lote viene del proveedor en recepción).
- Otros flags clave: `control_vencimiento`, `serializado`, `peso_recepcion/peso_despacho`, `temperatura_recepcion/temperatura_despacho`, `materia_prima`, `kit`.
