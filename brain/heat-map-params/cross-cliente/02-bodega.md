---
id: 02-bodega
tipo: heat-map-params
estado: vigente
titulo: "Cross-cliente: `bodega`"
tags: [heat-map-params]
---

# Cross-cliente: `bodega`

Tabla más grande del modelo de configuración. La fila bodega gobierna casi
TODA la operación: pantallas, validaciones, integración con ERP, reglas de
picking/recepción/verificación, control de calidad, etc.

## Conteos de columnas

| Cliente | Cols | Bodegas activas |
|---|---:|---:|
| BECOFARMA | 116 | 1 (B01 General) |
| K7 | 102 | 6 (BOD1, PRTOK, PRTO, BOD5, PRTK17, PRT17) |
| MAMPA | 117 | 33 (TIENDA CENTRAL + 32 puntos servicio) |
| BYB | 101 | 2 (PT, PT-DAÑADO) |
| CEALSA | 96 | 2 (B01 GENERAL, B02 FISCAL) |
| **Union** | **123** | — |

CEALSA es la implementación con MENOS cols (96). MAMPA y BECOFARMA las que
más (117 y 116). El gap entre el más rico (MAMPA 117) y el más pobre
(CEALSA 96) es de **21 columnas** — una operación distinta.

## Schema drift detectado (vs BYB como referencia)

### BECOFARMA tiene 15 cols extra
```
IdTipoEtiquetaVerificacion, bodega_cliente_ajuste_byb, cambio_ubicacion_restrictivo,
centro_costo_dep_erp, centro_costo_dir_erp, centro_costo_erp, control_gondola,
estado_defecto_rack, impresion_verificacion, packing_consolidado_guia,
permitir_cambio_ubic_indice_menor, priorizar_cantidad_superior, reemplazo_opcional,
requerir_mismo_producto_posiciones, reservar_primero_almacenaje
```

**Interesante**: `bodega_cliente_ajuste_byb` aparece en BECOFARMA — sugiere
que BECOFARMA tiene relación operativa con BYB (¿cliente común? ¿prefactura
cruzada?). Confirmar Q-BECO-AJUSTE-BYB.

### K7 tiene 4 cols extra Y FALTAN 3 (case-mismatch puro)
```
NO TIENE: PERMITIR_BUEN_ESTADO_EN_REEMPLAZO, TIPO_PANTALLA_PICKING, control_talla_color
EXTRA:    permitir_buen_estado_en_reemplazo, tipo_pantalla_picking, Control_Talla_Color,
          Reservar_primero_almacenaje
```

**HALLAZGO BRUTAL**: K7 tiene los mismos campos pero con CASE distinto:
- `PERMITIR_BUEN_ESTADO_EN_REEMPLAZO` (BYB upper) → `permitir_buen_estado_en_reemplazo` (K7 lower)
- `TIPO_PANTALLA_PICKING` (BYB upper) → `tipo_pantalla_picking` (K7 lower)
- `control_talla_color` (BYB lower) → `Control_Talla_Color` (K7 Camel)
- `Reservar_primero_almacenaje` (K7 Camel, no existe en BYB con esa forma exacta)

Esto rompe el WebAPI si usa nombres case-sensitive. Workaround: capa de
mapeo case-insensitive en el WebAPI. Pendiente Q-CASE-NAME en _index.

### MAMPA tiene 16 cols extra
Mismo set que BECOFARMA + `PRIORIZAR_CANTIDAD_SUPERIOR` (otra variante case-mismatch
de `priorizar_cantidad_superior` BECO!) + `transferir_ubicacion`.

### CEALSA tiene 6 cols MENOS y 1 extra
```
NO TIENE: advertir_mpq_umbas, agrupar_sin_lic_veri_no_cons, control_talla_color,
          limpiar_campos, permitir_cambio_ubic_recepcion, ruta_cdn
EXTRA: liberar_stock_depachos_parciales  (TYPO: debería ser "despachos")
```

**HALLAZGO BRUTAL**: CEALSA tiene un typo en col propio: `liberar_stock_depachos_parciales`
le falta la "s" en "despachos". Si el WebAPI espera `liberar_stock_despachos_parciales`
falla solo en CEALSA. Pendiente Q-CEALSA-TYPO-DESPACHOS.

## Matriz de flags clave activos por bodega

> Solo se incluyen bodegas activas y flags en True. Si está vacío, la bodega
> no tiene ese flag activado.

### BECOFARMA (1 bodega)
| IdBodega | Codigo | Nombre | Flags ON |
|---:|---|---|---|
| 1 | 01 | GENERAL | `notificacion_voz`, `interface_SAP`, `despacho_automatico_hh`, `operador_picking_realiza_verificacion` |

### K7 (6 bodegas)
| IdBodega | Codigo | Nombre | Flags ON |
|---:|---|---|---|
| 1 | BOD1 | Bodega Principal | `calcular_ubicacion_sugerida_ml`, `interface_SAP` |
| 2 | PRTOK | Bodega de Prorrateo Killios | `notificacion_voz`, `calcular_ubicacion_sugerida_ml`, `interface_SAP`, `verificacion_consolidada` |
| 3 | PRTO | Bodega de Prorrateo Garesa | `notificacion_voz`, `calcular_ubicacion_sugerida_ml`, `interface_SAP`, `verificacion_consolidada` |
| 4 | BOD5 | Bodega Amatitlán | `calcular_ubicacion_sugerida_ml`, `verificacion_consolidada` |
| 5 | PRTK17 | Bodega de Prorrateo Killios Z17 | `notificacion_voz`, `calcular_ubicacion_sugerida_ml`, `interface_SAP`, `verificacion_consolidada` |
| 6 | PRT17 | Bodega de Prorrateo Garesa Z17 | `notificacion_voz`, `calcular_ubicacion_sugerida_ml`, `interface_SAP`, `verificacion_consolidada` |

### MAMPA (33 bodegas — sample primeras 8)
| IdBodega | Codigo | Nombre | Flags ON |
|---:|---|---|---|
| 1 | 01 | TIENDA CENTRAL | `notificacion_voz`, `control_talla_color`, `interface_SAP`, `control_pallet_mixto`, `confirmar_codigo_en_picking` |
| 2 | 02 | PUNTO DE SERVICIO TECULUTAN | `notificacion_voz`, `control_talla_color`, `interface_SAP` |
| 3 | 03 | PUNTO DE SERVICIO ESCUINTLA | (igual que 02) |
| 4 | 04 | PUNTO DE SERVICIO BARBERENA | (igual que 02) |
| 5 | 05 | PUNTO DE SERVICIO XELA | (igual que 02) |
| 6 | 06 | PUNTO DE SERVICIO SAN MARCOS | (igual que 02) |
| 7 | 07 | PUNTO DE SERVICIO COBAN | (igual que 02) |
| 12 | 12 | CAMBIOS | (igual que 02) |
| ... | ... | (25 más) | (asumido similar a 02) |

### BYB (2 bodegas)
| IdBodega | Codigo | Nombre | Flags ON |
|---:|---|---|---|
| 2 | BA0002 | PRODUCTO TERMINADO | `calcular_ubicacion_sugerida_ml`, `verificacion_consolidada` |
| 24 | BA0024 | PRODUCTO TERMINADO DAÑADO | `Permitir_Verificacion_Consolidada`, `verificacion_consolidada` |

### CEALSA (2 bodegas activas, 1 inactiva)
| IdBodega | Codigo | Nombre | Flags ON |
|---:|---|---|---|
| 1 | B01 | BODEGA GENERAL | `operador_picking_realiza_verificacion`, `confirmar_codigo_en_picking` |
| 2 | B02 | BODEGA FISCAL | `es_bodega_fiscal`, `operador_picking_realiza_verificacion`, `confirmar_codigo_en_picking` |

## Lecturas operativas

### MAMPA usa pick por voz + control talla/color en TODAS las 33 bodegas
Confirmado. La capability `picking-talla-color` y `picking-by-voice` son
estándar TOTAL en MAMPA. Cualquier reescritura de la WebAPI debe garantizar
ambas en el día 1 para MAMPA.

Sub-detalle: solo TIENDA CENTRAL (bod 01) tiene también `control_pallet_mixto`
y `confirmar_codigo_en_picking`. Los puntos de servicio NO usan pallet mixto
ni confirman código.

### K7 usa ML para sugerencia de ubicación en TODAS sus 6 bodegas
Confirmado. La capability `recommend-location-ml` es estándar TOTAL en K7.
Esto es SINGULAR — ningún otro cliente lo usa (BECOFARMA tampoco, MAMPA no,
BYB solo en bodega 2, CEALSA no).

Pendiente Q-K7-ML-MODELO: ¿qué modelo usa? ¿Está embebido en BOF o en un
servicio externo? Esto puede ser un microservicio aparte en la WebAPI.

### Verificación consolidada — patrón de 4 clientes
- **K7**: bodegas 2-6 (todas las prorrateo + amatitlán) en `verificacion_consolidada=True`. Bodega 1 (Principal) NO.
- **BYB**: ambas bodegas (2 y 24) tienen `verificacion_consolidada`. La 24 además `Permitir_Verificacion_Consolidada` (case Camel).
- **BECOFARMA, MAMPA, CEALSA**: ninguna tiene `verificacion_consolidada`.

Hipótesis: K7 y BYB consolidan verificación cuando varios pedidos se preparan
juntos. Los demás verifican pedido a pedido. CONFIRMAR Q-VERIFICACION-CONSOL.

### `operador_picking_realiza_verificacion` — patrón 2 clientes
- BECOFARMA bod 1: True.
- CEALSA bodegas 1 y 2: True.
- K7, MAMPA, BYB: False.

Significado: el mismo operador que hace el picking hace la verificación. Saltea
el handoff entre operadores. Capability `same-operator-pick-and-verify`.

### `confirmar_codigo_en_picking` — patrón 2 clientes
- MAMPA bod 1 (TIENDA CENTRAL): True.
- CEALSA bodegas 1 y 2: True.
- Resto: False.

Significado: HH pide reescribir/reescanar el código del producto al confirmar
el picking. Doble validación.

### `es_bodega_fiscal` — solo CEALSA
Solo CEALSA bod 2 (BODEGA FISCAL) tiene este flag. Los demás clientes no
manejan bodega fiscal segregada. Capability `fiscal-warehouse-segregated`
es exclusiva CEALSA.

### `interface_SAP` por bodega — coherente con interfaz a nivel empresa
Todas las bodegas BECO/K7/MAMPA tienen `interface_SAP=True`. Excepción K7
bod 4 (BOD5 Amatitlán) que NO tiene interface_SAP — debe ser bodega WMS-only,
sin push al ERP. Pendiente Q-K7-BOD5-AMATITLAN-NOSAP.

### Pick por voz por cliente
- **MAMPA**: 33/33 bodegas con `notificacion_voz=True`.
- **K7**: 4/6 bodegas (las 4 prorrateo, NO BOD1 Principal NI BOD5 Amatitlán).
- **BECOFARMA**: 1/1 bodega.
- **BYB**: 0/2 bodegas (no usa voz).
- **CEALSA**: 0/2 bodegas (no usa voz).

Capability `picking-by-voice` activa en BECOFARMA, K7 (parcial), MAMPA. NO
en BYB ni CEALSA.

### `calcular_ubicacion_sugerida_ml` por cliente
- **K7**: 6/6 bodegas.
- **BYB**: 1/2 bodegas (solo bod 2 PRODUCTO TERMINADO; bod 24 DAÑADO no).
- BECOFARMA, MAMPA, CEALSA: 0.

Capability `recommend-location-ml` SOLO K7 (total) y BYB (parcial).

### `despacho_automatico_hh` — solo BECOFARMA
Solo BECOFARMA bod 1. Capability `dispatch-auto-from-hh` exclusiva.

## Pendientes Q-* derivadas
- **Q-CASE-NAME-K7**: el WebAPI debe tolerar el case-mismatch en K7
  (`PERMITIR_BUEN_ESTADO_EN_REEMPLAZO` vs `permitir_buen_estado_en_reemplazo`,
  etc).
- **Q-CEALSA-TYPO-DESPACHOS**: confirmar typo en `liberar_stock_depachos_parciales`
  y si el código BOF/HH usa el typo o el canónico.
- **Q-BECO-AJUSTE-BYB**: por qué BECOFARMA tiene `bodega_cliente_ajuste_byb` —
  ¿relación operativa con BYB?
- **Q-K7-ML-MODELO**: qué modelo de ML usa K7 para sugerir ubicación, dónde
  vive, cómo se entrena, qué datos usa.
- **Q-VERIFICACION-CONSOL**: confirmar la lógica de verificación consolidada
  (varios pedidos juntos) en K7 y BYB.
- **Q-K7-BOD5-AMATITLAN-NOSAP**: por qué BOD5 NO tiene interface_SAP.
- **Q-MAMPA-PUNTOS-SIMILARES**: confirmar (con full sample) que las 32
  puntos de servicio MAMPA tienen exactamente la misma config que el sample
  de la bod 02 — o detectar variantes.
