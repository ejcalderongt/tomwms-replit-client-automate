---
id: MAMPA
tipo: fingerprint
estado: vigente
titulo: Fingerprint MAMPA
clientes: [mampa]
ramas: [dev_2023_estable]
tags: [fingerprint, cliente/mampa]
---

# Fingerprint MAMPA

> Etiqueta human-readable: `MAMPA_CLIENT_TALLA-COLOR-ZAPATERIA_APPLIED`
> Capturado: 29-abr-2026 desde EC2 `52.41.114.122,1437/TOMWMS_MAMPA_QA`

## 1. Macro-tag

> **MAMPA = zapateria con talla y color, 18 bodegas distribuidas en
> Guatemala, modulo verificacion etiquetas ON, modelo log segmentado ON,
> 31K productos, ambiente QAS, primer cliente migrado desde
> dev_2023_estable a la rama de talla y color.**

## 2. Identidad

| | |
|---|---|
| BD en EC2 | `TOMWMS_MAMPA_QA` |
| Ambiente | QAS (no productivo) |
| Version codebase | rama `talla-y-color` (post dev_2023_estable) |
| Total tablas | 356 |
| Total productos | 31,397 |
| Productos con variante (talla×color) | 1,923 combinaciones en `producto_talla_color` |

## 3. Volumen operativo (medido 29-abr-2026)

| Tabla | Filas | Comentario |
|---|---|---|
| `trans_pe_enc` | 52 | pedidos creados |
| `trans_pe_det` | 126 | lineas |
| `trans_despacho_enc` | 44 | despachos materializados |
| `trans_despacho_det` | 434 | lineas despacho |
| `trans_re_enc` | 64 | recepciones |
| `trans_re_det` | 545 | lineas recepcion |
| `i_nav_transacciones_out` | (a medir) | outbox |

Estados de pedido (vista state machine):
- `Despachado`: 28 (~54%)
- `Nuevo`: 15 (~29%)
- `Pendiente`: 4
- `Verificado`: 3 ← **modulo verificacion etiquetas se usa**
- `Anulado`: 2

## 4. Features ON/OFF

| Feature | Estado | Evidencia |
|---|---|---|
| Talla y color (variantes) | **ON** | `producto_talla_color` (1923), `bodega.control_talla_color=1` en TODAS las bodegas |
| Verificacion etiquetas | **ON** | 3 pedidos en estado `Verificado` |
| Log segmentado por proceso | **ON** | tablas `log_error_wms_pe/rec/oc/pick/reab/ubic` presentes |
| License plate (lic_plate) | **ON** | sample outbox: `lic_plate='J100000192'` y `lic_plate='0'` (sentinela) |
| Cuadrillas (operadores agrupados) | **ON** | tablas `cuadrilla_enc/det_montacarga/det_operador/tipo` presentes — **EXCLUSIVO MAMPA** |
| Comparacion inventario | **ON** | tabla `ComparacionInventario` con columnas `IdProductoTallaColor_nuevo` para diff post-migracion |
| Stock por jornada | **ON** | tablas `stock_jornada_*` presentes |

## 5. Bodegas

**18 bodegas activas**:

| IdBodega | Codigo | Nombre | `control_talla_color` | Pantallas (pick/rec/verif) |
|---|---|---|---|---|
| 1 | 01 | TIENDA CENTRAL | ON | 1/1/1 |
| 2 | 02 | PUNTO DE SERVICIO TECULUTAN | ON | 1/1/1 |
| 3 | 03 | PUNTO DE SERVICIO ESCUINTLA | ON | 1/1/1 |
| 4 | 04 | PUNTO DE SERVICIO BARBERENA | ON | 1/1/1 |
| 5 | 05 | PUNTO DE SERVICIO XELA | ON | 1/1/1 |
| 6 | 06 | PUNTO DE SERVICIO SAN MARCOS | ON | 1/1/1 |
| 7 | 07 | PUNTO DE SERVICIO COBAN | ON | 1/1/1 |
| 12 | 12 | CAMBIOS | ON | 1/1/1 |
| 13 | 13 | BODEGA DE IMPARES | ON | 1/1/1 |
| 14 | 14 | BODEGA DE SEGUNDAS | ON | 1/1/1 |
| 15 | 15 | BODEGA DE TERCERAS | ON | 1/1/1 |
| 17 | 17 | MUESTRAS MAMPA | ON | 1/1/1 |
| 18 | 18 | MUESTRAS DISEÑO | ON | 1/1/1 |
| 19 | 19 | GARANTIAS PROVEEDORES | ON | 1/1/1 |
| **21** | **21** | **CEDIS SAN JUAN** | ON | **0/0/0** ← **sub-perfil distinto** |
| 23 | 23 | DIFERENCIAS SAN JUAN | ON | 1/1/1 |
| 24 | 24 | SEGUNDA DEFECTO CEDIS | ON | 1/1/1 |
| 25 | 25 | DAÑADOS DE ORIGEN CEDIS | ON | 1/1/1 |
| 28 | 28 | GIRAS Y CONSIGNACION | ON | 1/1/1 |

**Sub-perfil**: bodega 21 (CEDIS SAN JUAN) tiene `tipo_pantalla_*=0`.
Probablemente usa pantalla legacy. **Hipotesis nueva levantada**:
Q-MAMPA-CEDIS-PANTALLA-LEGACY.

## 6. Interface ERP

- **Pendiente confirmar Erik**: ¿que ERP usa MAMPA? El nombre del
  binario sincronizador y el `i_nav_config_enc` de MAMPA no fueron
  consultados todavia.
- Hint: la presencia de `i_nav_acuerdo_productos`, `i_nav_unidad_medida`
  (tablas estilo NAV/Business Central) sugiere **NAV/BC**.
- Confirmar leyendo `i_nav_config_enc` de MAMPA en proxima pasada.

## 7. Tablas exclusivas MAMPA (40 vs K7)

```
+ ComparacionInventario          + Listado_General
+ bodega_tmp                     + cliente_config (vacia)
+ codigos_barra_ant              + consolidador
+ cuadrilla_det_montacarga       + cuadrilla_det_operador
+ cuadrilla_enc                  + cuadrilla_tipo
+ detalle_de_movimiento          + equiparacion_ubicaciones
+ existencia                     + faltantes
+ i_nav_acuerdo_productos        + i_nav_config_enc_tmp
+ i_nav_unidad_medida            + licencia_pendientes
+ log_error_wms_oc               + log_error_wms_pe
+ log_error_wms_pick             + log_error_wms_reab
+ log_error_wms_rec              + log_error_wms_ubic
+ log_portal_ux                  + log_verificacion_bof
+ proveedor_config (vacia)       + stock_jornada_consecutivo
+ stock_jornada_desfase          + stock_jornada_fecha_consecutiva
+ ... +10 mas
```

## 8. Sub-perfiles internos

- **CEDIS SAN JUAN (bodega 21)**: pantallas tipo `0` mientras el resto
  tiene `1`. Distinto comportamiento HH.
- **Bodegas tipo "PUNTO DE SERVICIO"** (códigos 02-07): bodegas
  satelite distribuidas por departamentos de Guatemala. Pueden tener
  flujo simplificado de recepcion (a confirmar).

## 9. Diagnosticos abiertos para MAMPA

- Q-MAMPA-CEDIS-PANTALLA-LEGACY (nuevo): por que la bodega 21 tiene
  pantallas tipo 0 y el resto tipo 1.
- Q-MAMPA-ERP: que ERP usa MAMPA y que binario sincroniza.
- Q-MAMPA-VOLUMEN-OUTBOX: medir cardinalidad y patron temporal del
  outbox MAMPA.

## 10. Aprendizajes especificos MAMPA

1. **MAMPA es zapateria** (tallas 220, 350, 380, etc — numero de
   talla de calzado).
2. **MAMPA es el primer cliente con talla y color**, migrado desde
   `dev_2023_estable`. Sirve como referencia para futuros clientes
   que necesiten variantes.
3. **MAMPA tiene cuadrillas** (operadores agrupados con jefes y
   montacargas) — concepto que NO existe en K7/BB/BECOFARMA. Util
   para clientes con operacion grande tipo CEDIS.
4. **Las 18 bodegas de MAMPA** son una mezcla de tienda central,
   puntos de servicio departamentales, y bodegas de calidad
   (segundas, terceras, dañados). Modelo de retail multi-sucursal.
5. **MAMPA usa modelo de log segmentado** (igual que BECOFARMA).
   Refuerza L-016: el modelo segmentado se esta adoptando como
   estandar en clientes nuevos.

---

## ANEXO 29-abr-2026 (segunda pasada): cierre de Q-MAMPA-* y datos i_nav_config_enc

### A. ERP confirmado: SAP B1 (Q-MAMPA-ERP CERRADA)

- `i_nav_config_enc.interface_sap = True`
- `i_nav_config_enc.nombre_ejecutable = 'SAPBOSyncMampa.exe'`
- Refuerza L-015 (cada cliente tiene SU PROPIO `SAPBOSync<Cliente>.exe`).
- La presencia de `i_nav_acuerdo_productos` y `i_nav_unidad_medida` no
  era indicador de NAV — son tablas genericas del WMS que tambien
  llevan prefijo `i_nav_*`.

### B. 33 configuraciones en `i_nav_config_enc`

MAMPA tiene 33 filas en `i_nav_config_enc` (mas que las 18 bodegas
activas). Hay configuracion para bodegas adicionales (21=CEDIS,
30=Prorrateo, 31=TMK, etc) que no aparecen en `bodega` pero si tienen
config completa.

### C. Bodega 21 (CEDIS) — sub-perfil documentado

Q-MAMPA-CEDIS-PANTALLA-LEGACY parcialmente cerrada. CEDIS tiene:
- `control_lote = False`
- `control_vencimiento = False`
- `control_peso = False`
- `genera_lp = True`
- `valida_solo_codigo = True` ← **clave: pantalla simplificada**
- `requerir_centro_costo_obligatorio = True`
- `cantidad_en_presentacion_transacciones_out = True` ← MAMPA-only flag
- `bodega_prorrateo = '30'`, `bodega_faltante = '23'`

→ **Conclusion**: CEDIS NO es legacy, es UN MODELO DE OPERACION
DISTINTO: validacion solo por codigo (sin lote ni vencimiento, porque
calzado no es perecedero), con centro de costo obligatorio (control
contable mas estricto), y prorrateo a bodega 30 + faltantes a bodega
23. Es un CEDIS de distribucion, no una tienda.

### D. Modelo de configuracion MAMPA: BODEGA-CENTRIC

MAMPA `producto`:
- genera_lote = 0%
- control_vencimiento = 0%
- control_lote = 0%
- control_peso = 0%
- genera_lp_old = **100%**
- IdTipoEtiqueta poblado = **100%**

Es decir: MAMPA NO usa los flags producto.control_*, todo se controla
a nivel `i_nav_config_enc` (bodega). Esto es **diferente** a K7 (mixto)
y a BECOFARMA (product-centric). Ver L-020 nuevo.

### E. Verificacion etiqueta — flag NO unitario

Q-CAPABILITY-FLAG-VERIF cerrada parcialmente. La activacion de
verificacion NO es un solo flag, son al menos 9 columnas coordinadas:
- `bodega.tipo_pantalla_verificacion` (tipo de pantalla, NO ON/OFF)
- `bodega.IdTipoEtiquetaVerificacion`
- `bodega.impresion_verificacion`
- `bodega.operador_picking_realiza_verificacion`
- `bodega.permitir_reemplazo_verificacion`
- `bodega.Permitir_Verificacion_Consolidada`
- `bodega.verificacion_consolidada`
- `operador.verifica`
- `cuadrilla_tipo.es_verificacion`

Ver L-021 nuevo para el modelo coordinado completo.
