# INDEX maestro del brain — vista arbol human-readable

> Ultima actualizacion: 29-abr-2026
>
> **Como leer este indice**: cada entrada del brain (hipotesis, learning,
> ADR, doc) tiene un **ID raiz inmutable** (H29, L-018, ADR-001) + una
> **etiqueta human-readable** (categoria + slug + estado) que dice de un
> vistazo que es y donde encaja.
>
> Los IDs raiz no cambian nunca (porque ya estan referenciados en otros
> docs y commits). Las etiquetas human-readable pueden mejorarse con el
> tiempo.

## Convencion de etiqueta human-readable

```
<ID_RAIZ>_<CATEGORIA>_<SLUG>_<ESTADO>
```

- **ID_RAIZ**: H, L, ADR, Q + numero (inmutable)
- **CATEGORIA** (una sola, mayusculas):
  - `DIAG`  — diagnostico operativo (problema real detectado en BD/operacion)
  - `ARCH`  — decision arquitectonica (rol componente, modelo de datos)
  - `FIX`   — correccion aplicada (cambio de conclusion, bug fix)
  - `FEAT`  — feature/funcionalidad/modulo (verificacion, talla y color, packing)
  - `DATA`  — patron de datos (sentinelas, claves, cardinalidades)
  - `PROC`  — proceso operativo (state machine, flujo recepcion-despacho)
  - `PARAM` — capability flag o parametro de configuracion
  - `INTEG` — integracion con ERP (SAP B1, NAV)
  - `CLIENT`— especifico de un cliente (mampa, killios, becofarma, byb, cealsa, cumbre, idealsa, inelac)
  - `META`  — meta-aprendizaje sobre como trabajamos (ej: confiabilidad EC2)
- **SLUG**: corto, kebab-case, descriptivo
- **ESTADO**:
  - `OPEN`        — abierto (en `_inbox/`)
  - `APPLIED`     — aplicado/cerrado con conclusion firme
  - `INVALID`     — invalidado (la hipotesis fue refutada)
  - `DEF_PEND`    — definicion pendiente (esperando confirmacion Erik o cliente)
  - `WAIT_CLIENT` — esperando validacion con cliente productivo
  - `FIX_PEND`    — bug detectado, fix pendiente

## Arbol del brain — estado actual

```
TOMWMS-BRAIN
│
├── A. ARQUITECTURA (decisiones de diseno futuras)
│   ├── ADR-001_ARCH_ROL-WEBAPI-NET10_DEF_PEND
│   │   └── Modelo A vs B vs C para la WebAPI nueva. Esperando decision Erik.
│   ├── L-016_ARCH_LOG-ERROR-WMS_MODELO-WEBAPI_APPLIED
│   │   └── Recomendacion: log unificado con discriminador `proceso`.
│   ├── H26_ARCH_LOG-SEGMENTADO-WEBAPI_DEF_PEND
│   │   └── Followup de L-016: confirmar Erik unificado vs segmentado.
│   └── doc-rol-webapi-net10 (3 modelos detallados)
│
├── B. DIAGNOSTICOS OPERATIVOS (problemas reales en BD/operacion)
│   ├── H29_DIAG_BECOFARMA_DESPACHO-ROTO-MAR2026_WAIT_CLIENT
│   │   └── trans_despacho_enc colapso W12-mar-2026. 3800 pedidos en
│   │       Pickeado sin cerrar. Esperando confirmacion cliente.
│   └── (futuras hipotesis diagnosticas aqui)
│
├── C. PATRONES DE DATOS (semantica del esquema)
│   ├── L-017_DATA_FK-SENTINELA-CERO_APPLIED
│   │   └── i_nav_transacciones_out: FKs no aplicables = 0, no NULL.
│   │       Patron uniforme K7/BB/BECOFARMA. Confirmado en MAMPA tambien.
│   ├── L-018_DATA_LIC-PLATE-UNIVERSAL_APPLIED
│   │   └── lic_plate cubre 99-100% transacciones outbox. Identificador
│   │       universal de stock fisico. Cardinalidad: INGRESO 1:1, SALIDA 1:N.
│   └── H30_DATA_FK-NULLS-BECOFARMA_INVALID
│       └── Invalidada por L-017. Las FKs NO son NULL, son 0 sentinela.
│
├── D. FEATURES / MODULOS (que hace el WMS)
│   ├── L-018_FEAT_VERIF-ETIQUETAS_APPLIED
│   │   └── Modulo verificacion etiquetas: CORE-WMS, parametrizable,
│   │       ligado a HH. Emite licencia (lic_plate). K7 ON, BECOFARMA ON,
│   │       MAMPA ON, BB/C9 DESCONOCIDO.
│   ├── H27_FEAT_VERIF-ETIQUETAS_FIX_K7-ON_APPLIED
│   │   └── Confirmado modulo ON en K7 (screenshot Erik 29-abr).
│   ├── doc-funcionalidades-por-cliente (matriz cliente x feature)
│   ├── doc-tablas-por-funcionalidad (tablas core por feature)
│   └── doc-parametros-por-cliente (capability flags conocidos)
│
├── E. PROCESOS OPERATIVOS (state machines, flujos)
│   ├── doc-state-machine-pedido (en becofarma-mapping.md)
│   ├── doc-process-map (general TOMWMS)
│   └── (pendiente: state machine por cliente con/sin Verificado)
│
├── F. INTEGRACION ERP (interfaces salida)
│   ├── L-008_INTEG_OUTBOX-PATRON_APPLIED (i_nav_transacciones_out)
│   ├── L-009_INTEG_SAP-B1-SOLO-ENTEROS_APPLIED (K7)
│   ├── L-010_INTEG_NAV-NO-INGRESOS_APPLIED (BB)
│   ├── L-012_INTEG_NAVSYNC-EXE_APPLIED
│   ├── L-013_INTEG_SAPBOSYNC-EXE_APPLIED (BECOFARMA)
│   └── L-015_INTEG_CLICKONCE-DISPATCH_APPLIED
│       └── Binario por cliente via i_nav_config_enc.nombre_ejecutable
│
├── G. ESPECIFICOS DE CLIENTE
│   ├── fingerprint/MAMPA_CLIENT_TALLA-COLOR-ZAPATERIA_APPLIED
│   │   └── Macro-perfil: 18 bodegas, talla/color (zapateria), 1923
│   │       producto_talla_color, modulo verif ON, modelo log segmentado ON.
│   ├── fingerprint/KILLIOS_CLIENT_GASTRONOMICO-SAP-B1_APPLIED
│   │   └── (a completar) Modulo verif ON (screenshot 29-abr), interface
│   │       SAP B1, solo enteros.
│   ├── fingerprint/BECOFARMA_CLIENT_FARMA-VERIF-COMPLETO_APPLIED
│   │   └── Modulo verif ON, modelo log segmentado, 36K filas outbox,
│   │       cuello de botella despacho desde W12-mar.
│   ├── fingerprint/BYB_CLIENT_NAV-MASIVO_DEF_PEND
│   │   └── 533K filas outbox, NAV no procesa ingresos. Verif DESCONOCIDO.
│   ├── fingerprint/CEALSA_CLIENT_QAS_DEF_PEND
│   │   └── Ambiente QAS, sin trafico real. Tiene Licencias (PascalCase).
│   ├── fingerprint/CUMBRE_CLIENT_*_DEF_PEND (no analizado todavia)
│   ├── fingerprint/IDEALSA_CLIENT_*_DEF_PEND (no analizado)
│   └── fingerprint/INELAC_CLIENT_*_DEF_PEND (no analizado)
│
├── H. META-APRENDIZAJES (sobre como trabajamos)
│   ├── L-014_META_EC2-COPIA-PARCIAL_APPLIED
│   │   └── EC2 es copia parcial/diagnostica. Productiva real en laptop Erik.
│   └── (futuro: convencion de etiquetas, glosario eufemismos)
│
└── I. PREGUNTAS ABIERTAS (en _inbox/, esperan a Erik)
    ├── Q-VERIF-K7-PERIODOS_PARAM_OPEN
    ├── Q-VERIF-BB_FEAT_OPEN
    ├── Q-VERIF-C9_FEAT_OPEN
    ├── Q-CAPABILITY-FLAG-VERIF_PARAM_OPEN
    ├── Q-LICENCIAS-MODELO-NUEVO_DATA_OPEN
    └── Q-EC2-DESFASE_META_OPEN
```

## Tabla rapida — todas las entradas con sus etiquetas

| ID_RAIZ | Etiqueta human-readable | Estado | Archivo |
|---|---|---|---|
| ADR-001 | `ARCH_ROL-WEBAPI-NET10` | DEF_PEND | outputs/explicaciones/rol-arquitectonico-webapi-net10.md |
| L-008 | `INTEG_OUTBOX-PATRON` | APPLIED | learnings/L-008-... |
| L-009 | `INTEG_SAP-B1-SOLO-ENTEROS` | APPLIED | learnings/L-009-... |
| L-010 | `INTEG_NAV-NO-INGRESOS` | APPLIED | learnings/L-010-... |
| L-011 | `META_LOG-ERROR-WMS-LEGACY` | APPLIED | learnings/L-011-... |
| L-012 | `INTEG_NAVSYNC-EXE` | APPLIED | learnings/L-012-... |
| L-013 | `INTEG_SAPBOSYNC-EXE` | APPLIED | learnings/L-013-... |
| L-014 | `META_EC2-COPIA-PARCIAL` | APPLIED | learnings/L-014-... |
| L-015 | `INTEG_CLICKONCE-DISPATCH` | APPLIED | learnings/L-015-... |
| L-016 | `ARCH_LOG-ERROR-WMS_MODELO-WEBAPI` | APPLIED | learnings/L-016-... |
| L-017 | `DATA_FK-SENTINELA-CERO` | APPLIED | learnings/L-017-... |
| L-018 | `DATA_LIC-PLATE + FEAT_VERIF-ETIQUETAS` | APPLIED | learnings/L-018-... |
| H06-H11 | `varios` (Bloque A) | DEF_PEND ratificacion | _inbox/ |
| H26 | `ARCH_LOG-SEGMENTADO-WEBAPI` | DEF_PEND | _processed/ con followup |
| H27 | `FEAT_VERIF-ETIQUETAS_FIX_K7-ON` | APPLIED | _processed/ |
| H29 | `DIAG_BECOFARMA_DESPACHO-ROTO-MAR2026` | WAIT_CLIENT | _inbox/ |
| H30 | `DATA_FK-NULLS-BECOFARMA` | INVALID | _processed/ |
| Q-VERIF-K7-PERIODOS | `PARAM_VERIF-K7-INTERMITENTE` | OPEN | _inbox/ |
| Q-VERIF-BB | `FEAT_VERIF-BB-ON-OFF` | OPEN | _inbox/ |
| Q-VERIF-C9 | `FEAT_VERIF-C9-ON-OFF` | OPEN | _inbox/ |
| Q-CAPABILITY-FLAG-VERIF | `PARAM_FLAG-VERIF-UBICACION` | OPEN | _inbox/ |
| Q-LICENCIAS-MODELO-NUEVO | `DATA_LICENCIAS-PASCALCASE-MIGRACION` | OPEN | _inbox/ |
| Q-EC2-DESFASE | `META_EC2-CALENDARIO-SYNC` | OPEN | _inbox/ |

## Como pedir cosas al brain con esta convencion

Ejemplos de preguntas y la respuesta esperada:

- **"Killios valida etiqueta en verificacion?"**
  → consulta `brain-map/funcionalidades-por-cliente.md` seccion C
  → respuesta: "K7 ON, evidencia screenshot 29-abr (L-018, H27_FIX)"

- **"Que falta resolver para arrancar la WebAPI nueva?"**
  → arbol > A. ARQUITECTURA > entradas con `DEF_PEND`
  → ADR-001 + H26 + decision Modelo A/B

- **"Que clientes ya tengo perfilados?"**
  → arbol > G. ESPECIFICOS DE CLIENTE > entradas con `APPLIED`
  → MAMPA, KILLIOS, BECOFARMA. Pendientes: BYB, CEALSA, CUMBRE, IDEALSA, INELAC.

- **"Hay diagnosticos operativos sin resolver?"**
  → arbol > B. DIAGNOSTICOS > entradas no APPLIED
  → H29 (corte despacho marzo BECOFARMA, esperando cliente)

---

## Actualizacion 29-abr-2026 (sesion fingerprints cross-cliente)

### Nuevos learnings APPLIED
- `L-019_PARAM_CONFIG-ENC-FUENTE-MAESTRA` — `i_nav_config_enc` es la fuente maestra de capability flags por bodega.
- `L-020_DATA_MODELOS-CONFIG-PRODUCTO-VS-BODEGA` — 3 modelos: BODEGA-CENTRIC (MAMPA) / PRODUCT-CENTRIC (BECOFARMA) / MIXTO (K7).
- `L-021_FEAT_VERIF-FLAGS-COORDINADOS` — verificacion etiqueta NO es un flag unico, es coordinado entre bodega+operador+cuadrilla.

### Nuevos fingerprints APPLIED
- `KILLIOS_CLIENT_GASTRONOMICO-SAP-B1` — 6 bodegas con prorrateo, modelo MIXTO, EC2 desfase 8 meses.
- `BECOFARMA_CLIENT_FARMA-SAP-B1-PRODUCT-CENTRIC` — 1 bodega, modelo PRODUCT-CENTRIC, cuello despacho confirmado (3802 Pickeado + 31263 outbox sin enviar).

### Hipotesis CERRADAS (movidas a _processed/)
- Q-MAMPA-ERP → SAP B1 con `SAPBOSyncMampa.exe`.
- Q-MAMPA-CEDIS-PANTALLA-LEGACY → CEDIS NO es legacy, es modelo de operacion distinto (`valida_solo_codigo=True`, sin lote/vence/peso, prorrateo a b30, faltantes a b23).
- Q-MAMPA-VOLUMEN-OUTBOX → 158-443/mes feb-abr-2026, periodo corto (QA reciente).
- Q-PRODUCTO-GENERA-LP-NUEVO → `genera_lp` (sin _old) existe a nivel `i_nav_config_enc` (bodega), no en producto. Modelo dual.

### Hipotesis PARCIALMENTE cerradas
- Q-CAPABILITY-FLAG-VERIF → resuelto via L-021 (no es flag unico).

### Refuerzos cruzados
- L-014 (EC2 copia parcial) reforzado: K7 outbox solo hasta ago-2025, 8 meses de desfase. BECOFARMA al dia (abr-2026). MAMPA QA datos cortos. Confirma asimetria.
- L-015 (ClickOnce dispatch) reforzado: confirmados `SAPBOSyncKillios.exe`, `SAPBOSync.exe` (BECOFARMA), `SAPBOSyncMampa.exe`. Patron `SAPBOSync<Cliente>.exe`.
- L-016 (log segmentado) reforzado: BECOFARMA y MAMPA usan modelo segmentado, K7 NO (solo log_error_wms unificado).
- H29 (despacho roto BECOFARMA) reforzado con datos: 3,802 pedidos en `Pickeado` + 31,263 outbox `enviado=0` desde enero. Es real, masivo, confirmado en BD.

### Estado actualizado del arbol G. ESPECIFICOS DE CLIENTE
```
G. ESPECIFICOS DE CLIENTE
├── MAMPA       APPLIED  (zapateria, talla y color, BODEGA-CENTRIC, SAP B1)
├── KILLIOS     APPLIED  (gastronomico, prorrateo, MIXTO, SAP B1)
├── BECOFARMA   APPLIED  (farma, 1 bodega, PRODUCT-CENTRIC, SAP B1, cuello despacho)
├── BYB         DEF_PEND (no analizado todavia)
├── CEALSA      DEF_PEND (QAS, no analizado)
├── CUMBRE      DEF_PEND
├── IDEALSA     DEF_PEND
└── INELAC      DEF_PEND
```

---

## Actualizacion 29-abr-2026 (sesion P06+P07: cierre Wave 1)

### Nuevos fingerprints APPLIED
- `BYB_CLIENT_NAV-OPERACION-PARADA-2024` — 2 bodegas (PT + PT-DAÑADO),
  PRIMER CLIENTE NAV, modelo PRODUCT-CENTRIC con flags conservadores,
  verificacion HALF-IMPLEMENTED, talla y color preparado pero vacio,
  OPERACION COLAPSADA en 2024.
- `CEALSA_CLIENT_QAS-CEALSASYNC-PROPIO` — ambiente QAS, ERP propio
  con `CEALSASync.exe`, modelo PRODUCT-CENTRIC heterogeneo,
  modulo Polizas y Kits exclusivos, outbox vacio, sin verificacion.

### Nuevos learnings APPLIED
- `L-022_INTEG_NAMING-SYNC-EXE` — patron de naming del binario
  sincronizador por ERP: `SAPBOSync<Cliente>.exe` (SAP),
  `NavSync.exe` (NAV), `<Cliente>Sync.exe` (propio).
- `L-023_DIAG_BYB-CORTE-OPERATIVO-2024` — BYB outbox parado entre
  dic-2023 y oct-2025. Caso de estudio para detectar problemas
  similares en otros clientes.
- `L-024_FEAT_VERIF-HALF-IMPLEMENTED-BYB` — anomalia: BYB tiene
  pedidos `Verificado` pero sin tablas de soporte. Predicado nuevo
  `modulo_completo(c, modulo)` para chequear coherencia.

### Hipotesis NUEVAS abiertas
- Q-BYB-CORTE-2024 (high) — BYB sigue activo?
- Q-BYB-VERIF-INCOMPLETA (medium) — como se setearon los 8 Verificado?
- Q-CEALSA-OUTBOX-VACIO (medium) — por que outbox=0?
- Q-CEALSA-CEALSASYNC-ERP (medium) — que ERP destino?

### Estado FINAL del arbol G. ESPECIFICOS DE CLIENTE (Wave 1 cerrada)
```
G. ESPECIFICOS DE CLIENTE
├── MAMPA       APPLIED  (zapateria, talla y color, BODEGA-CENTRIC, SAP B1)
├── KILLIOS     APPLIED  (gastronomico, prorrateo, MIXTO, SAP B1)
├── BECOFARMA   APPLIED  (farma, 1 bodega, PRODUCT-CENTRIC, SAP B1, cuello despacho H29)
├── BYB         APPLIED  (PT+dañado, PRODUCT-CENTRIC, NAV, OPERACION PARADA 2024)
├── CEALSA      APPLIED  (QAS, PRODUCT-CENTRIC heterogeneo, ERP propio CEALSASync.exe)
├── CUMBRE      DEF_PEND (no analizado todavia, pendiente Wave 1+ o Wave 2)
├── IDEALSA     DEF_PEND (no analizado)
└── INELAC      DEF_PEND (no analizado)
```

### Refuerzos cruzados Wave 1
- L-009 (SAP B1 solo enteros) confirmado para K7.
- L-010 (NAV no procesa ingresos) reforzado dramaticamente con BYB:
  no solo no procesa, el sync esta ROTO desde 2024.
- L-014 (EC2 copia parcial) confirmado: BYB EC2 hasta oct-2025, K7
  hasta ago-2025, BECOFARMA al dia, MAMPA QA reciente, CEALSA QAS.
- L-015 (ClickOnce dispatch) ampliado y movido a L-022.
- L-016 (log segmentado) MATIZADO: CEALSA y BYB usan unificado.
  Adopcion segmentado: BECOFARMA + MAMPA. Adopcion unificado:
  K7 + BYB + CEALSA. Empate 2-2 (sin contar K7 que es el mayor).
  Esto cambia la recomendacion previa para WebAPI.

### MATRIZ COMPLETA (5 clientes)

| Cliente | ERP | Modelo Config | Verif | Talla/Color | Outbox enviado=0 | Estado operativo |
|---|---|---|---|---|---|---|
| MAMPA | SAP B1 | BODEGA-CENTRIC | ON | ON | (a medir) | QA reciente |
| KILLIOS | SAP B1 | MIXTO | ON | OFF | 3 (sano) | EC2 8 meses desfase |
| BECOFARMA | SAP B1 | PRODUCT-CENTRIC | ON bajo | OFF | 31263 (CRITICO) | cuello despacho H29 |
| BYB | NAV | PRODUCT-CENTRIC NULL | HALF | preparado | 255912 | parado 2024 |
| CEALSA | propio | PRODUCT-CENTRIC heterogeneo | OFF | OFF | 0 (vacio) | QAS sin trafico |


---

## Actualizacion 28-abr-2026 (sesion cross-cliente Wave 4)

### Folder nuevo: `brain/heat-map-params/cross-cliente/`
4 archivos con matriz exhaustiva flag x cliente para los 5 clientes:
- `README.md` — indice + conteos macro.
- `01-i_nav_config_enc.md` — 78 cols union, schema drift severo, taxonomia
  ERP corregida.
- `02-bodega.md` — 123 cols union, flags por bodega y matriz capabilities.
- `03-tipos-documento.md` — `trans_oc_ti` y `trans_pe_tipo` cross-cliente.

### Hallazgos brutales aplicados
- **Taxonomia ERP corregida**: 3 clientes son SAP B1 (BECOFARMA, K7, MAMPA),
  no 2. Solo BYB es NAV. CEALSA es PREFACTURA dedicada. Esto invalida
  presunciones previas sobre NAV mayoritario.
- **MAMPA tiene 31.397 productos activos** (catalogo masivo por talla x color).
- **CEALSA tiene 3.200 estados de producto** (vs 10-24 en el resto) — pendiente
  Q-CEALSA-3200-ESTADOS.
- **NINGUN cliente usa propietarios** (no hay 3PL en TOM hoy).
- **K7 tiene case-mismatch + typo + duplicados** en `i_nav_config_enc`:
  `Codigo_Bodega_ERP_NC` (Camel) vs `codigo_bodega_erp_nc` (BYB lower),
  `codigo_bodega_nc_erp` (reordered), `lote_defecto_nc` (variante adicional),
  `explosio_automatica_nivel_max` (typo le falta "n").
- **K7 case-mismatch en bodega**: `PERMITIR_BUEN_ESTADO_EN_REEMPLAZO` (BYB
  upper) vs `permitir_buen_estado_en_reemplazo` (K7 lower), `TIPO_PANTALLA_PICKING`
  vs `tipo_pantalla_picking`, `Control_Talla_Color` vs `control_talla_color`.
- **CEALSA tiene typo propio**: `liberar_stock_depachos_parciales` (le falta
  una "s" — debería ser "despachos").
- **MAMPA usa pick por voz + control talla/color en TODAS sus 33 bodegas**.
- **K7 usa ML para sugerencia de ubicacion en TODAS sus 6 bodegas** — capability
  `recommend-location-ml` exclusiva K7 (BYB la tiene parcial).
- **CEALSA es la implementacion mas austera**: sin voz, sin ML, sin verificacion
  consolidada, sin pallet mixto. Pero con la unica `bodega fiscal` (B02).
- **BECOFARMA bod 1**: pick por voz + interface SAP + despacho automatico HH +
  operador picking realiza verificacion (mismo operador hace pick + veri).

### Hipotesis nuevas abiertas
- **Q-K7-DUPLICADOS-CONFIG**: 4 variantes del mismo campo en K7
  (`codigo_bodega_erp_nc` lower / Camel / reordered / `lote_defecto_nc`).
- **Q-K7-TYPO-EXPLOSION**: typo `explosio_automatica_nivel_max` en K7.
- **Q-CASE-NAME-K7**: WebAPI debe normalizar case en K7 bodega.
- **Q-CEALSA-TYPO-DESPACHOS**: confirmar typo `liberar_stock_depachos_parciales`.
- **Q-BECO-AJUSTE-BYB**: por que BECOFARMA tiene `bodega_cliente_ajuste_byb`.
- **Q-K7-ML-MODELO**: que modelo de ML usa K7 para sugerir ubicacion.
- **Q-K7-BOD5-AMATITLAN-NOSAP**: por que BOD5 NO tiene `interface_SAP=True`.
- **Q-CEALSA-3200-ESTADOS**: por que tantos estados de producto en CEALSA.
- **Q-CEALSA-AUSENTES-7**: validar las 7 cols ausentes en `trans_pe_tipo`.
- **Q-BECO-PRODUCCION**: BECOFARMA tiene tipo `Orden de Produccion` activo.
- **Q-MAMPA-IDPRODESTADO-3**: por que default es 3 y no 1.
- **Q-MAMPA-IDINDICE-4**: significado de `IdIndiceRotacion=4` en MAMPA.
- **Q-CEALSA-IDACUERDO-1**: cruzar con `cealsa_vwacuerdocomercialenc`.
- **Q-MAMPA-BOD23-FALTANTES**: confirmar bodega 23 para faltantes.
- **Q-K7-BOD7-FACTURACION**: existe bod7 fisica o es virtual.
- **Q-VERIFICACION-CONSOL**: logica K7+BYB de verificacion consolidada.

### Deprecation confirmada
- `industria_motriz` (bodega) y `IDPRODUCTOPARAMETROA/B` (producto) son
  DEPRECATED. Diseñados para una venta unica de repuestos automotriz
  que nunca llego a operar. Reflejado en `02-bodega/README.md` y
  `04-producto/README.md`. NO priorizar en WebAPI, mantener por compatibilidad
  schema solamente.

### Capabilities nuevas identificadas
- `picking-by-voice` — BECOFARMA, K7 (parcial 4/6 bodegas), MAMPA (33/33).
- `recommend-location-ml` — K7 (6/6 bodegas), BYB (1/2 bodegas).
- `same-operator-pick-and-verify` — BECOFARMA (1/1), CEALSA (2/2).
- `verify-with-photo` (en pedido) — BECOFARMA, MAMPA.
- `assign-all-operators` — solo MAMPA.
- `fiscal-warehouse-segregated` — solo CEALSA.
- `dispatch-auto-from-hh` — solo BECOFARMA.
- `verify-consolidated` — K7 (5/6), BYB (2/2).
- `confirm-code-in-picking` — MAMPA bod 1, CEALSA (2/2).
- `dispatch-by-pallet-mixed` — solo MAMPA bod 1.

### Proximo: code-deep-flow
Una vez completados los flags (queda capa 4 producto + producto_estado),
arrancar el code-deep-flow: dado un parametro X, mapear como viaja por el
backend / BOF (VB.NET) / HH (Android) y que tablas afecta. Cruce de codigo
+ DB. Erik confirmo este orden el 28-abr-2026.

---

## Actualizacion 28-abr-2026 (Wave 5: cierre capa 4 producto)

### Nuevo archivo: `brain/heat-map-params/cross-cliente/04-producto.md`
Cierra el inventario de las 4 capas fundamentales con distribuciones
REALES de flags por cliente para `producto`, `producto_bodega`,
`producto_estado`, `producto_estado_ubic`.

### Hallazgos brutales nuevos

1. **Schema producto practicamente sin drift**: 60 cols en TODOS menos
   CEALSA con 59 (le falta `margen_impresion`). Es la tabla MAS
   estandarizada de las 4 capas que vimos.

2. **producto_bodega es pure join N:M sin parametros**: 9 cols identicas
   en los 5 clientes. NO tiene flags funcionales. WebAPI puede tratarlo
   como pure relation.

3. **MAMPA tiene 1.036.101 filas en producto_bodega**: 33 bodegas x
   31.397 productos = caso de presion para WebAPI. Endpoints deben
   tener paginacion agresiva.

4. **CASO CEALSA 3.200 ESTADOS RESUELTO**: 3.197 filas son scaffolding
   ruido — patron automatico que crea "Buen Estado" por cada
   IdPropietario nuevo. Insertadas entre 2022-01-27 y 2025-04-15 por
   user_agr=6. Solo 4 estados utiles (Buen Estado, Mal Estado, RH, HR).
   `propietario` ni siquiera existe como tabla — solo tablas relacionadas
   (`propietario_destinatario`, `propietario_reglas_enc`, etc).
   `producto.IdPropietario` distinct: 1 en 4 clientes, 3 en CEALSA.
   Filtrar siempre `producto_estado` por IdPropietario presente en
   producto. **Q-CEALSA-ORIGEN-PROP-3197**: investigar trigger / job en
   code-deep-flow.

5. **CAPABILITIES NUNCA IMPLEMENTADAS EN TOM** (False/0/NULL en TODOS
   los 5 clientes): `serializado`, `kit`, `materia_prima`,
   `temperatura_recepcion`, `temperatura_despacho`, `capturar_aniada`,
   `captura_arancel`, `es_hardware`, `tolerancia=0`, `ciclo_vida=0`,
   `IdCamara`, `IdPerfilSerializado`, `IdUnidadMedidaCobro`, `IdArancel`.
   Schema preparado para futuro que TOM nunca prendio. WebAPI puede
   ignorarlas en MVP, mantener nullable.

6. **MAMPA NO usa lote ni vencimiento en NINGUN producto**:
   `control_lote=False` y `control_vencimiento=False` en los 31.397.
   Coherente con zapateria. Modelo de inventario completamente distinto
   al resto. `IdTipoRotacion=1` (FIFO puro) en 99.6% — el resto FEFO.

7. **CEALSA practicamente NO controla vencimiento**: solo 24 productos
   (1.4%) con `control_vencimiento=True`. `control_lote=True` mixto
   (45%). Operacion seca distribuidor general.

8. **CEALSA es el UNICO con productos por peso**: 6 productos exactos
   con `control_peso=True` + `peso_recepcion=True` + `peso_despacho=True`
   (los 3 flags juntos). Probable carnes / quesos al peso.

9. **CEALSA es el UNICO con soft delete real**: 79 productos con
   `activo=False` (4.6%). Resto: 100% activo=True. WebAPI debe soportar
   el flag.

10. **CEALSA tiene IdSimbologia (codigo de barras) poblado en 47%**
    (798/1714) — el resto residual (5% o menos). MAMPA cero (usa
    etiqueta interna `IdTipoEtiqueta=12`).

11. **Cada cliente tiene IdTipoEtiqueta dedicado**: BECO=8, K7=10,
    MAMPA=12, BYB=2, CEALSA=2. NO hay default universal. WebAPI debe
    permitir custom templates por cliente.

12. **IdTipoRotacion=3 (FEFO) dominante** en BECO/K7/BYB. MAMPA=1 (FIFO).
    CEALSA=NULL en todos. Confirma que CEALSA delega rotacion al sistema.

13. **`genera_lp_old=True` en TODOS menos BYB**: BYB es el unico con
    drift (487 False / 136 True = 78% False). El resto: 100% True.
    Confirma modelo dual `genera_lp` (bodega) vs `genera_lp_old`
    (producto, legacy). Posible deprecation futura.

14. **Catalogo de estados con drift**:
    - BECO 10 estados, mapeo 1:1 a bodega ERP NAV
    - K7 18 estados granulares de calidad
    - MAMPA 19 estados de perecederos (carne / abarrotes / refri)
    - BYB 24 estados industriales con NO_DISPONIBLE_NAV / NAV_BD
    - CEALSA 4 estados utiles (catalogo MUY pobre)
    - `reservar_en_umbas` flag adicional ausente en K7 y CEALSA.

15. **producto_estado_ubic practicamente vacio en TODOS** (1-20 filas).
    NO es feature critica para WebAPI MVP.

### Nuevas hipotesis Q-* (capa producto)
- Q-RESERVAR-EN-UMBAS (significado UMBA y por que solo 3 clientes)
- Q-CEALSA-ORIGEN-PROP-3197 (trigger / job que crea Buen Estado)
- Q-CEALSA-RH-HR (significado de estados RH / HR en CEALSA)
- Q-CEALSA-IDTIPO-NULL (por que NULL para todos los productos)
- Q-BYB-NO-DISPONIBLE-NAV-BD (diferencia NAV vs NAV_BD)
- Q-MAMPA-MERMA-CARNE-FLUJO (estados 20 y 21 como flujo)
- Q-GENERA-LP-OLD-LEGADO (confirmar deprecation)

### Lecturas para WebAPI
1. Endpoints `/producto` con paginacion agresiva (caso MAMPA 31k).
2. Capability flags expanded en DTO para evitar consultas N+1 desde HH.
3. `producto_bodega` como pure relation, sin endpoint propio.
4. `producto_estado` con catalogo libre por cliente, NO estandarizar.
5. NO exponer DEPRECATED (`IDPRODUCTOPARAMETROA/B`, `industria_motriz`,
   `genera_lp_old` a confirmar).
6. Filtrar CEALSA `producto_estado` por IdPropietario presente en producto.
7. Soft delete via `activo` requerido (caso CEALSA).
8. Perfiles de operacion alterno: "calzado/talla-color" (MAMPA) y
   "distribuidor general" (CEALSA) ademas del estandar farma/gastro.

### Estado actualizado del arbol del brain
```
brain/heat-map-params/cross-cliente/
├── README.md                       APPLIED  (indice + conteos macro)
├── 01-i_nav_config_enc.md          APPLIED  (78 cols, taxonomia ERP)
├── 02-bodega.md                    APPLIED  (123 cols, capabilities)
├── 03-tipos-documento.md           APPLIED  (trans_oc_ti + trans_pe_tipo)
└── 04-producto.md                  APPLIED  (60 cols + estados resueltos)
```

### Inventario CERRADO
Las 4 capas fundamentales del nucleo de configuracion estan cubiertas.
Drift medido. Hipotesis abiertas registradas. **Listo para arrancar
code-deep-flow (Wave 6+)**: dado un parametro X, mapear como viaja por
backend / BOF VB.NET / HH Android y que tablas afecta. Cruce de codigo
+ DB. Erik confirmo el orden A1 -> B el 28-abr-2026.

---

## Wave 6 init — code-deep-flow bootstrap (28-abr-2026)

Erik confirmo arrancar B (code-deep-flow) tras cerrar A1 (capa producto).
Aprobacion del approach holistico → micro: WMS tiene los mismos
parametros para todas las empresas; el inventario debe partir del flujo
GENERAL y bajar a variantes por empresa solo donde haya divergencia
real.

### Acceso confirmado a fuentes de codigo (28-abr-2026)
- Azure DevOps `ejcalderon0892`:
  - Repo `TOMWMS_BOF` (376 MB) rama `dev_2028_merge` ← BOF + WSHHRN +
    WMSWebAPI + Syncs + Portal + DAL/Entity Core
  - Repo `TOMHH2025` (14 MB) rama `dev_2028_merge` ← HH Android Java
- API REST preferida sobre clone (PAT en `AZURE_DEVOPS_PAT`).
- Detalle en `agent-context/AZURE_ACCESS.md`.

### Nuevo folder: `brain/code-deep-flow/`
- `README.md` — metodo holistico → micro, plantilla de cada
  `traza-NNN-<param>.md` con 9 secciones (Resumen, BOF, WSHHRN,
  WMSWebAPI, HH, DB, ERP, Variantes por empresa, Q-* abiertas, Lecturas
  WebAPI nuevo).
- `00-mapa-de-cajas.md` — diagrama macro de toda la arquitectura como
  insumo previo a cualquier traza. Cubre repos, modulos
  (TOMIMSV4/WSHHRN/WMSWebAPI/etc), sub-topologia BOF y HH, relaciones
  cross-capa, hipotesis Q-* a nivel arquitectura.

### Hallazgos brutales del scan inicial de codigo

1. **WSHHRN no es solo el WebService de la HH, es ademas un router
   proxy a ERPs externos**. `TOMHHWS.asmx` es el unico endpoint SOAP
   para la HH, pero ademas hay 11 Web References cliente a ERPs:
   WSDevolucionVentaNAV, WSLotePedidoCompra, WSPaginaLotes,
   WSPedidosCompraNAV, WSRecepAlm, WSTransferenciaEnvio, wWSPicking,
   wsBYBNavCUWMS, wsBYBNavMovProd, wsBYBNavUInternas,
   wsBYBNavUbicarAlmacen, wsTransferenciaIngresoNAV. Las 4 wsBYBNav*
   son BYB-specific (NAV en linea, NO via sync).

2. **WMSWebAPI tiene 25 Controllers identificados** en .NET moderno
   con AutoMapper y JWT. Maestros (15), operativo (6), sync (2),
   auth (2). Convivencia activa con WSHHRN: la migracion REST esta
   en marcha pero parcial.

3. **HH tiene DOS clientes HTTP en paralelo**: `WebService.java`
   (SOAP a WSHHRN, legacy) y `ApiService.java` + `RetrofitClient.java`
   (REST a WMSWebAPI, nuevo). Cada pantalla puede usar uno u otro.
   Estado de migracion debe verificarse caso por caso en cada traza.

4. **TOMWMS_BOF es mono-solution con muchos modulos historicos**:
   ademas de los 3 grandes (TOMIMSV4, WSHHRN, WMSWebAPI) hay TMS,
   MES, MI3, IAService, GoCloud, GoCloudy, Perceptron, PlotWH,
   3 PrintServices, 2 versiones StockReservation (v2 y v3 conviven),
   DAL/Entity Core paralelo a AppGlobal legacy, etc. Cada uno
   pendiente de identificacion de uso real.

5. **Conn.ini por cliente en WSHHRN**: Conn.ini default,
   Conn_Becofarma.ini, Conn - Cumbre.ini. Confirmar si la conexion
   por cliente realmente vive en .ini o en config DB / WMSWebAPI
   appsettings.

### Hipotesis Q-* nuevas a nivel arquitectura

- Q-WSHHRN-AS-PROXY-BYB (4 Web References NAV en linea para BYB)
- Q-WMSWEBAPI-MIGRACION-MAPA (% migrado de SOAP a REST)
- Q-PERCEPTRON-USO-REAL (deployado en 5 clientes pero solo 2 lo prenden)
- Q-MI3-QUE-ES (recurrente en MI3 / CEALSAMI3 / SAPMI3PC / TestMI3)
- Q-WMS-EXE-CONFIG-EN-WSHHRN (BOF.config dentro de carpeta WebService)
- Q-CHATGPT-SERVICE (ChatGPTService.vb en WSHHRN — feature o experimento)
- Q-3-PRINT-SERVICES (PrintService / PrintsService / ServicioImpresion)
- Q-STOCK-RESERVATION-2-VS-3 (cual usa cada cliente)
- Q-TOMWMSUX-VS-WMSPORTAL (dos webs distintas)
- Q-DALCORE-VS-DAL-LEGACY (WebAPI usa Core, WSHHRN usa legacy, misma DB?)

### Pendiente eleccion Erik para `traza-001`

Candidatos top-pick segun criterio de mayor cobertura cross-capa +
drift conocido:
- A) `notificacion_voz` (bodega) — toca BOF + WSHHRN + HH (commands voz)
  + DB. Variantes claras: BECO 1/1, K7 4/6, MAMPA 33/33, BYB 0/2, CEALSA 0/2.
- B) `verificacion_consolidada` (bodega) — flujo packing critico K7+BYB.
- C) `genera_lp` vs `genera_lp_old` — modelo dual bodega+producto, BYB
  drift 78% en `genera_lp_old`, perfecto para validar approach.
- D) `control_lote` (producto) — drift TOTAL: MAMPA 0%, BECO 97%,
  CEALSA 45%. Caso simple para arrancar.

---

## Wave 6 — code-deep-flow — traza-001 License Plate (2026-04-28)

**Archivo**: `brain/code-deep-flow/traza-001-license-plate.md` (~600 líneas, 9 secciones)

### Hallazgos brutales

1. **WMSWebAPI no migra LP**. 25 controllers + 16 services REST cubren maestros, KPI, sync ERP, auth — **0 endpoints LP/picking/recepcion HH**. WMSWebAPI es backend del portal web, no reemplazo de WSHHRN.
2. **WSHHRN tiene 374 funciones públicas en un solo .asmx.vb (19.492 líneas)**. 21 son LP-específicas. Cuello de botella crítico para migración a .NET 10.
3. **El LP no es entidad propia en DB**. Es `nvarchar(100)` que viaja como `lic_plate` en stock/stock_rec/stock_res/stock_jornada/faltantes/trans_movimientos/trans_re_det + `lp_origen`/`lp_destino` en trans_movimiento_pallet + `barra_pallet` en trans_movimientos. Naming inconsistente.
4. **Modelo triple confirmado**: `i_nav_config_enc.genera_lp` (bodega) + `producto.genera_lp_old` (producto) + `producto_presentacion.genera_lp_auto` (presentación). El DAL Core trata `serializado`+`genera_lote`+`genera_lp_old`+`control_vencimiento` siempre como bloque consecutivo.
5. **Invariante de coherencia NO enforced en DB**. K7 tiene 25% del stock sin `lic_plate` poblado aunque `genera_lp=True` para todos. Drift histórico (stock viejo + ajustes manuales). El WebAPI nuevo debe enforzar el invariante en capa de servicios.
6. **BYB único drift cross-cliente**: 78% de productos con `genera_lp_old=False` aunque bodega tiene `genera_lp=True`. Único cliente con esta política.
7. **NAV expone correlativo LP**: `DynamicsNavInterface/WebReference` incluye `Get_Nuevo_Correlativo_LicensePlate(_S)` como operations consumidas DE NAV. Algunos clientes pueden obtener correlativo de NAV en línea, no de tabla local.
8. **Configuración de LP idéntica cross-cliente**: `configuracion_barra_pallet` tiene LongLP=7 en los 5, pero datos reales muestran LP de 16 chars en K7/BYB. Inconsistencia config vs realidad.
9. **HH muestra/oculta UI según los 3 flags**: `frm_recepcion_datos.java` línea 247 declara variables locales `PGenera_lp`, `PTiene_Ctrl_Peso`, `PTiene_Ctrl_Temp`, `PTiene_PorSeries`, `PTiene_Pres` — la matriz completa de capabilities. Cada `P*` controla un campo opcional.
10. **Convivencia código muerto**: `frm_recepcion_datos_original.java` junto al `frm_recepcion_datos.java` actual; `producto_presentacion1` junto a `producto_presentacion`; tres `srvProducto*` en TestMI3.

### Issues de seguridad descubiertos durante el pase (no parte del flujo LP)

- **Q-SEC-OPENAI-KEY-LEAK**: `WSHHRN/ChatGPTService.vb` línea 9 — API key OpenAI **hardcodeada en código fuente**, comprometida desde push a Azure DevOps. Acción: rotar key + mover a appsettings con secret manager + auditar git history para purgar.
- **Q-SEC-CONNINI-CREDS**: `WSHHRN/Conn.ini`, `Conn_Becofarma.ini`, `Conn - Cumbre.ini` almacenan credenciales SQL Server con usuario `sa` y password en claro, pusheado al repo. Acción: mover a Azure Key Vault o variables de entorno por host, eliminar de git history.

### Q-* arquitectura abiertas (17 total — ver traza para tabla completa)

- Q-LP-OPERADOR-VS-USUARIO, Q-LP-LONG-DEFAULT, Q-LP-LONG-VS-DATOS-REALES, Q-LP-S-VARIANTE
- Q-LP-CORRELATIVO-NAV, Q-LP-EN-K7-DRIFT-25PCT, Q-LP-BYB-PRODS-SIN-LP
- Q-PRESENTACION1-MUERTA, Q-RECEPCION-BOF-FLUJO, Q-HH-RECEPCION-DOS-VERSIONES
- Q-MI3-QUE-ES, Q-WMSWEBAPI-MIGRACION-MAPA, Q-CONNINI-SELECCION
- Q-LP-NAMING-DB, Q-LP-FALTANTES-PARA-QUE
- Q-SEC-OPENAI-KEY-LEAK, Q-SEC-CONNINI-CREDS

### Próximos pasos

- Erik resuelve las Q-* críticas (sobre todo Q-LP-LONG-VS-DATOS-REALES, Q-LP-BYB-PRODS-SIN-LP, Q-WMSWEBAPI-MIGRACION-MAPA).
- traza-002 control_lote + control_vencimiento (siguiente acordada).
- Issues de seguridad: Erik decide rotación de credenciales y plan de purga del git history.

### Wave 6.1 update (2026-04-28) — Erik resuelve 2 Q-* + nuevas

**Q-* resueltas**:
- ~~Q-LP-EN-K7-DRIFT-25PCT~~ → **resuelta parcial**. Erik: explosión consume LP. Datos: 269/1186 stocks sin LP tienen IdPresentacion=NULL (consistente con explosión a unidad básica). 917/1186 tienen presentación con genera_lp_auto=True pero sin LP (drift residual real).
- ~~Q-LP-CORRELATIVO-NAV~~ → **resuelta**. NAV-en-línea es BYB-only. WSHHRN actúa como puente porque NAV expone web services. Resto de clientes usa interfaces ad-hoc por ERP. WMSWebAPI Sync/* es la capa de generalización emergente.

**Q-* nuevas derivadas**:
- Q-LP-EXPLOSION-COMO-OPERA: ¿qué SP/función ejecuta la explosión y limpia lic_plate?
- Q-LP-917-DRIFT-RESIDUAL: origen de los 917 stocks K7 con drift legítimo
- Q-LP-CICLO-VIDA: modelar formalmente Active → Consumed → Voided del LP

**Principios de dominio confirmados**:
1. LP = identificador efímero atado a estado físico de agrupación. Explosión/despacho/desconsolidación lo consumen.
2. No hay "integración ERP universal". Cada cliente tiene adaptador ad-hoc. WMSWebAPI Sync/* es la abstracción emergente.
3. Invariante de coherencia es responsabilidad de capa de servicios, no del modelo de datos.
4. Modelo dual `genera_lp` (bodega) + `genera_lp_old` (producto) = migración interrupta histórica. WebAPI nuevo puede unificar en `RequiresLP = bod AND prod`.
5. NAV-en-línea via SOAP es puente BYB-específico, forzado por arquitectura de NAV.

**Arquitectura objetivo WebAPI .NET 10** (sketch):
- Domain universal (LicensePlate con ciclo de vida, Stock, Producto, Capabilities)
- Application Services con lógica común (LpIssuance, LpConsumption, StockMovement con invariante)
- ERP Integration pluggable: IErpAdapter + NavErpAdapter (BYB) + SapErpAdapter (K7) + Ims4mb (BECO/BYB-sec/CEALSA) + Mampa + Cumbre

---

## DISCLAIMER GLOBAL DE RAMAS (Wave 6.1 — 2026-04-28)

**El brain documenta `dev_2028_merge` (BOF + HH) salvo indicación explícita.**

| Cliente | Rama producción | Estado |
|---|---|---|
| BECO, K7, BYB, CEALSA | `dev_2023_estable` | Producción estable |
| MAMPA | `dev_2028_merge` | Pruebas integrales |
| Cumbre | `dev_2028_Cumbre` (HH) | Rama dedicada |
| MHS | (B2B vía WMSWebAPI) | Nuevo cliente con dev propio |

**Diferencias 2023 vs 2028 confirmadas (BOF)**:
- **Solo 2028**: `WMS.DALCore`, `WMS.EntityCore`, `WMS.AppGlobalCore`, `WMS.StockReservation2/3`, `reservastockfrommi3`, `GoCloud`, `GoCloudy`, `InstallerSW`, `.github`, `.cursorrules`, meta-docs (`AGENTS.md`, `CLAUDE.md`, `CONVENTIONS.md`, `PARCHES_APLICADOS.md`)
- **Existen en ambas, hashes distintos**: `WMSWebAPI` (NO greenfield 2028 — está desde 2023), `WSHHRN/TOMHHWS.asmx.vb` (340 funciones en 2023 → 369 en 2028, +29 públicas, +1 LP endpoint), `WSHHRN/ChatGPTService.vb` (issue OpenAI key existe en 2023 también — severidad sube)

**Diferencias 2023 vs 2028 confirmadas (HH)**:
- Estructura top-level **idéntica**
- `clsBeProducto.java` con `Genera_lp` = **idéntico** ambas
- `ApiService.java` REST = **idéntico** (la capa REST de HH no es feature 2028)
- `frm_recepcion_datos.java`: 10448 → 11481 líneas (+1033, refactor interno)

**Endpoint LP único nuevo en 2028 BOF**:
- `Existe_Lp_By_Licencia_And_IdBodega_JSON(pLic_Plate, pIdBodega)` ← versión JSON del endpoint SOAP existente. **Patrón emergente 2028: SOAP → JSON.**

**Detalle completo**: ver `agent-context/RAMAS_Y_CLIENTES.md`

**Q-* derivadas (Wave 6.1)**:
- Q-MIGRACION-2023-A-2028: orden, cronograma, breaking changes, scripts de migración DB
- Q-DALCORE-PROPOSITO: ¿reemplaza WMS.DAL legacy? ¿coexiste? ¿lo invoca WSHHRN o WebAPI?
- Q-MHS-COMO-CLIENTE: scope WebAPI con MHS, fecha go-live, qué maestros escribe, qué transacciones lee
- Q-WMS5-VACIO: ¿qué fue/iba a ser TOMWMS5? Repo existe pero vacío
- Q-HH-RAMAS-25: política de ramas HH — consolidación, qué ramas por cliente siguen activas
- Q-MERCOPAN-MERCOSAL: ramas con nombres de cliente fuera de la lista activa — ¿clientes históricos? ¿prospectos?
- Q-CUMBRE-RAMA-DEDICADA: ¿por qué Cumbre tiene rama HH propia y no comparte `dev_2028_merge`?
- Q-DEV2025-PROPOSITO: ¿qué contiene `dev_2025`? ¿paso intermedio entre 2023 y 2028?
- Q-MASTER-PROPOSITO: `master` es default en ambos repos — ¿en qué difiere de `dev_2023_estable`?
- Q-SOAP-A-JSON-2028: ¿es política consciente convertir SOAP → JSON en 2028? ¿se planea convertir los 340+ endpoints?
- Q-SEC-OPENAI-KEY-LEAK: severidad sube — el archivo está en 2023 también (más tiempo expuesto). **Rotar de inmediato**.


---

## Wave 6.1 update 2 (2026-04-28) — Erik resuelve Q-DALCORE-PROPOSITO con historia completa

**Q-DALCORE-PROPOSITO** → **resuelta**. Documento dedicado: `code-deep-flow/02-portal-y-dms.md` (~340 líneas).

**TL;DR de la resolución**:
- DALCore + EntityCore + AppGlobalCore son **duplicación obligada** de WMS.DAL/WMS.Entity legacy porque las DLLs VB.NET Framework no son compatibles con .NET Core.
- La duplicación se justifica como **oportunidad de reingeniería gradual** (cleanup en el port).
- Consumidores principales: **WMSWebAPI** (canal B2B-only, MHS = primer caso) + **DMS** (probable, pendiente verificar) + futuros desarrollos web.
- **NO reemplaza** a WMS.DAL legacy — coexisten. WSHHRN, BOF WinForms, MI3 siguen con la capa legacy.
- **Visión a futuro** (Erik): "el siguiente paso es llevar a la web lo que hoy se ejecuta en Windows Forms" — DALCore es preparación para esto.

**Hallazgos colaterales documentados en `02-portal-y-dms.md`**:

1. **DMS = "Data Management System"** — herramienta server-side de Efren. Está en `TOMWMS_BOF/DMS/`. EXE parametrizado (horario/días/frecuencia) que aprovecha la infra del license-server. Replica data on-prem → cloud fila por fila (NO bulk insert) por principio Erik de "control total para debug".

2. **Portal CEALSA** — origen del DMS. CEALSA = 3PL guatemalteco regulado por SAT/SIB/aseguradora. Necesidad: exponer inventario de propietarios "pagantes" (~10 de N) a internet sin replicar 10+ GB. Solución: filtrar por propietario (reducción 80x) + replicar transaccional manteniendo estructura ("como arriba es abajo").

3. **Generador de código (Efren / Erik)** — la "primera automatización" del WMS. Se pega a la BD y genera por cada tabla: `clsBe<Tabla>` (Entity autogen 100%) + `clsLn<Tabla>` (CRUD base autogen) + `clsLn<Tabla>.partial` (negocio adhoc manual). Patrón universal del WMS. Erik admite drift: hay clases LN con código manual en el archivo "autogenerado".

4. **Modelo multi-tenant 3PL** — propietarios. Estados aceptables varían por propietario ("tropicalización"). 6 tablas validadas en CEALSA QAS: `propietarios`, `propietario_bodega`, `propietario_destinatario`, `propietario_reglas_enc/det`, `producto_estado`, `producto_estado_ubic`.

5. **stock_jornada validado por SQL** — tabla con 25+ columnas que replica TODA la traza transaccional del stock (incluye `IdRecepcionEnc/Det`, `IdPedidoEnc`, `IdPickingEnc`, `IdDespachoEnc`, `lote`, `lic_plate`, `serial`, `IdPropietarioBodega`, `fecha_vence`). Materializa el principio "como arriba es abajo". **Activo en CEALSA y MAMPA (21.883 filas)**. Inactivo (tabla existe vacía) en BECO/K7/BYB. ¿Por qué MAMPA? — abierta.

6. **CEALSAMI3** = módulo de sync CEALSA con su ERP. Ubicación: `TOMWMS_BOF/CEALSAMI3/CEALSASYNC.sln`. **Anomalía detectada**: clases con prefijo `Nav` (`clsSyncNavProducto`, `clsSyncNavCategoriasProducto`, `clsSyncNavGruposProducto`, `clsSyncNavTablaConversion`) — contradice "NAV es solo BYB". Hipótesis principal: copy-paste de un BYBSync original sin renombrar. Q-NAV-PREFIJO-CEALSA abierta.

7. **TOMWeb** — repo separado en Azure DevOps. Es **ASP.NET clásico + PHP** (xampp, .htaccess, login, en/es). Distinto al portal CEALSA. Probablemente portal corporativo / tienda de licencias. Q-TOMWEB-PROPOSITO abierta.

8. **CEALSA repo en Azure DevOps**: existe pero **vacío** (0 MB, sin ramas) — igual que `TOMWMS5`. ¿Iba a ser el portal? ¿el DMS específico? ¿se canceló? Q-CEALSA-REPO abierta.

**Q-* nuevas abiertas en `02-portal-y-dms.md` (15)**:
- DMS: Q-DMS-USA-DALCORE, Q-DMS-DESTINO-CLOUD, Q-DMS-PROPIETARIO-FILTER
- Portal: Q-PORTAL-STACK, Q-PORTAL-AUTH, Q-PORTAL-MULTITENANCY-DECISION
- Generador: Q-GENERADOR-UBICACION, Q-GENERADOR-INPUTS, Q-LN-DRIFT-AUDIT
- DALCore: Q-DALCORE-PARIDAD, Q-DALCORE-CONSUMERS, Q-DALCORE-COMPORTAMIENTO
- stock_jornada: Q-STOCK-JORNADA-PROCESO, Q-STOCK-JORNADA-CONSUMER, Q-STOCK-JORNADA-DESFASE, Q-STOCK-JORNADA-MAMPA
- Web migration: Q-WEB-BOF-STACK, Q-WEB-BOF-TIMELINE, Q-GENERADOR-WEB-VARIANTE
- Otros: Q-NAV-PREFIJO-CEALSA, Q-TOMWEB-PROPOSITO, Q-CEALSA-REPO, Q-PROPIETARIO-ESTADO-MODELO, Q-PROPIETARIO-AGNOSTICO

**Principios Erik confirmados (consolidados)**:
1. Control total > performance (no bulk insert, no ORM, fila por fila debuggeable)
2. "Como es arriba es abajo" (replicación estructural en cloud-target, no flat)
3. Duplicación obligada como oportunidad (cleanup en el port .NET Framework → .NET Core)
4. Generación de código como base de mantenibilidad (patrón BE + LN-base + LN-partial)
5. Tropicalización por propietario (multi-tenant 3PL via reglas + estados-por-propietario)
6. Reingeniería gradual con coexistencia (DAL legacy + DALCore conviven)
7. Infra existente como apalancamiento (DMS aprovecha license-server)


---

## Wave 6.1 update 3 (2026-04-28) — CONCEPT_MAP creado

**NUEVO**: `_index/CONCEPT_MAP.md` (~430 líneas) — vista temática de las ~85 Q-* abiertas agrupadas en 15 dominios, con prioridad, método de resolución y dependencias entre dominios.

**Resumen agrupado**:
- A. WMS Core (LP, lote, vencimiento, peso, serializado, IdTipoRotacion) — 15 Q-*
- B. Ramas y migración 2023→2028 — 8 Q-*
- C. DALCore/EntityCore/AppGlobalCore — 5 Q-* (1 resuelta)
- D. Portal CEALSA / DMS / replicación cloud — 10 Q-*
- E. Multi-tenancy 3PL — 12 Q-*
- F. stock_jornada (regulatorio) — 4 Q-*
- G. Integraciones (MI3, NAV, MercaERP, WSHHRN, WMSWebAPI) — 11 Q-*
- H. Generador de código — 4 Q-*
- I. Verificación / capabilities — 9 Q-*
- J. Reservas (Reservation 2 vs 3) — 2 Q-*
- K. Recepción/pedido/despacho/ciclo — 4 Q-*
- L. Cliente-específicas (BECO/BYB/K7/MAMPA) — 22 Q-* agrupadas
- M. Web BOF (futuro) — 2 Q-*
- N. Seguridad (TRANSVERSAL CRÍTICA) — 3 Q-* (1 crítica)
- O. Otros técnicos — 3 Q-*

**Mapa de dependencias** entre dominios documentado en CONCEPT_MAP.md.

**Candidatos a próxima traza evaluados** (5 opciones):
1. control_lote + control_vencimiento (recomendada)
2. stock_jornada cierre regulatorio
3. cealsasync + NAV-prefijo-mystery
4. capability-flags por cliente
5. Wave de seguridad (no-traza, ACCIÓN)

**Wave 6.2 quick-wins propuesta** — 8 Q-* baratas resolubles en 1-2 turnos solo con SQL/GREP.

**Métricas brain Wave 6.1 cierre**:
- Q-* totales: ~85 (sin placeholders)
- Q-* resueltas: 1
- Q-* alta prioridad abiertas: 11
- Q-* críticas: 1 (Q-SEC-OPENAI-KEY-LEAK)
- Líneas brain total: ~2.620
- Archivos: 5 principales


---

## Wave 6.2 (2026-04-28) — Quick wins cerrada (7 Q-* resueltas, +5 Q-* derivadas)

**Doc**: `_index/WAVE-6.2-QUICK-WINS.md` (~250 lineas)

**Resueltas**:
- Q-LP-LONG-VS-DATOS-REALES → BYB avg 19 chars (NAV correlativo); resto 6-9 chars
- Q-CEALSA-AUSENTES-7 → en realidad **37** propietarios huerfanos (no 7) de 3.197 totales
- Q-DALCORE-PARIDAD → DALCore al **22%** del legacy (256/1162 archivos)
- Q-DEV2025-PROPOSITO + Q-MASTER-PROPOSITO → alias de `dev_2023_estable` (mismo commit `1f5cc2c4`)
- Q-LN-DRIFT-AUDIT → convencion es `_partial.vb` (underscore), no `.partial.vb`. Solo **10% de las LN tienen partial** -> drift 90%
- Q-PROPIETARIO-AGNOSTICO → no-3PL usan **1 propietario default**; CEALSA unico 3PL real (3.197 prop)

**Reformulada**:
- Q-LP-LONG-DEFAULT → no hay columna especifica en `producto_parametros`. Limite es por `varchar(N)` del schema o convencion cliente

**Q-* nuevas derivadas** (+5):
1. Q-GENERADOR-ABANDONO (alta) - 90% drift en clases base
2. Q-LP-CORRELATIVO-NAV-FORMATO (media) - estructura LP BYB
3. Q-PORTAL-AUTH-CREDENCIALES-EN-PROPIETARIOS (alta) - cols `codigo_acceso` + `clave_acceso` en `propietarios`
4. Q-LP-DATA-DIRTY-MIN (baja) - 8 filas BECO LP="0"
5. Q-RAMA-MASTER-DEV2025-DUPLICADAS (baja) - politica de alias

**Metricas post-wave**:
- Q-* resueltas: 8/85 (9.4%)
- Q-* netas abiertas: ~82
- Brain en proyecto Replit (movido de /tmp/wms-brain a ./wms-brain): persistente para Carolina


---

## Wave 7 (2026-04-29) — Holding IDEALSA + implosión + capabilities flag

**Detonante**: Erik conectó dos BDs nuevas en EC2 (`IMS4MB_MERHONSA_PRD` y `IMS4MB_MERCOPAN_PRD`) — ambas filiales del holding IDEALSA (Honduras + Panamá). Pidió recorrido paralelo + detección de gaps. Adicionalmente reveló la lógica de **implosión / merge LP en cambio de ubicación 2028**.

**Docs nuevos**:
- `agent-context/HOLDING_IDEALSA.md` (~280 líneas) — análisis paralelo MERHONSA vs MERCOPAN, schema diff, capabilities `i_nav_config_enc`
- `code-deep-flow/03-implosion-y-merge-lp.md` (~410 líneas) — traza profunda de las 3 capas de implosión (BOF, HH, parámetro auto) + merge LP 2028 + ciclo de vida completo del LP

**Q-* RESUELTAS en esta wave (4 importantes)**:
- ✅ **Q-LP-WHEN-DESTROYED** — un LP "muere" en 5 caminos: despacho completo, implosión BOF manual, implosión HH Cumbre, cambio ubicación 2028 con LP destino preexistente (auto si flag ON), anulación de recepción. Persiste histórico en `trans_movimientos.lic_plate`.
- ✅ **Q-LP-MERGE-EN-DESTINO** — 2028 unifica el flujo via `frmCambioUbicacion.vb` reescrito (+113% líneas). Detecta LP destino existente y mergea automáticamente si `i_nav_config_enc.implosion_automatica=True`.
- ✅ **Q-CAPABILITY-FLAG** — la tabla maestra de capabilities **ES** `i_nav_config_enc`. Tiene 50+ flags por bodega/propietario que controlan: generación de LP, implosión auto, explosión auto, control_lote, control_vencimiento, integración NAV, integración SAP, políticas de despacho, reabasto, NC, bonificaciones.
- ✅ **Q-CONTROL-LOTE-TABLA** — descartada: `control_lote` y `control_vencimiento` NO son tablas, son columnas (bit) en `i_nav_config_enc`.

**Hallazgos brutales nuevos**:
- 🔥 **WMS soporta NAV + SAP simultáneamente como ERPs** (`interface_sap` flag). Antes asumido NAV-only.
- 🔥 **`frmImplosion.vb` SÍ existe en BOF** (1332 líneas, sin cambios entre 2023 y 2028). Erik creía que no. Hipótesis: oculto por permisos en clientes nuevos, visible en Cumbre.
- 🔥 **MERCOPAN tiene rol "cocinero"** (`StockCocinero`, `stock_BK_Cocinero`) — sugiere preparación de mezclas/comidas en almacén. Único entre los clientes mapeados.
- 🔥 **Holding IDEALSA confirmado**: 98% schema común MERHONSA↔MERCOPAN (315 de ~320 tablas). Diferencias menores tienen patrón identificable (regulatorio Panamá, recovery Honduras).
- 🔥 **MERCOPAN ya tiene 323K movimientos** (en producción), MERHONSA solo 0 movimientos pero ya 16K detalles de tareas HH (arranque operativo en curso).

**Q-* nuevas derivadas (+10)** — agregadas a `CUESTIONARIO_CAROLINA.md` bloque 11:
- Q-MERGE-LP-LOG-PATRON, Q-IMPLOSION-BOF-VISIBILIDAD, Q-CLAVAUD-MEANING
- Q-SAP-CLIENTES, Q-UMB-CONCEPT, Q-LP-ZOMBIE
- Q-IDEALSA-MASTER-DATA, Q-IDEALSA-OTROS-PAISES, Q-MERHONSA-PARADOJA-LP
- Q-COCINERO-ROLE-PANAMA, Q-SCHEMA-PRODUCTOS-MERHONSA

**Métricas post-Wave 7**:
- Q-* totales: ~95 (+10)
- Q-* resueltas: 12/95 (Wave 6.1: 1, Wave 6.2: 7, Wave 7: 4)
- Q-* alta prioridad abiertas: ~10
- Q-* críticas: 1 (Q-SEC-OPENAI-KEY-LEAK, sin cambio)
- Líneas brain total: ~3.700
- Archivos brain: 9 principales

**Próximo recomendado**:
1. Resolver Q-SCHEMA-PRODUCTOS-MERHONSA (descubrir schema real, < 30 min)
2. Pattern del log f(y)→f(z) en MERCOPAN (tiene los 323K movimientos para hacer estadística)
3. Traza-002 control_lote+control_vencimiento ahora con la realidad de que son flags por bodega
4. Mapa completo de los 50 flags de `i_nav_config_enc` con interpretación cliente-por-cliente

---

## Wave 8 (2026-04-29) — Estrategia Clavaud + MI3 + algoritmo de reserva 2028

**Detonante**: Erik contó la anécdota de Marcelo Clavaud (gerente logística IDEALSA Panamá) y reveló que **MI3 = Módulo de Integración con Terceros** (eufemismo interno para la interface). Esto destrabó 3 Q-* y permitió mapear el algoritmo de reserva completo.

**Doc nuevo**:
- `code-deep-flow/04-mi3-y-reserva-clavaud.md` (~480 líneas) — 7 capas: caso negocio, flag y parámetro, modelo de datos, algoritmo `Reserva_Stock`, ranking LINQ, patrón 3PL, proyecto MI3 + CEALSAMI3

**Q-* RESUELTAS (4)**:
- ✅ **Q-CLAVAUD-MEANING** — apellido del gerente de logística cliente IDEALSA Panamá. Estrategia anti-vaciamiento de picking codificada como flag `i_nav_config_enc.conservar_zona_picking_clavaud` (bit, default 0, migration 2022-11-07 commit `EJC202211071706`).
- ✅ **Q-MI3-IDENTIDAD** — MI3 es un proyecto WCF/SOAP completo en `MI3/` con services para Bodega, Cliente, Direcciones, Documentos, Barras_Pallet. Es el endpoint que expone el WMS al ERP.
- ✅ **Q-UMB-CONCEPT** — UMB = Unidad de Medida Básica. Confirmado en código: `vCantidadSolicitadaUMBas = pBePedidoDet.Cantidad * BePres.Factor`.
- ✅ **Q-PROPIETARIO-AGNOSTICO** (refinada) — branching por `empresas.Operador_logistico`. 3PL (CEALSA): config por (IdBodega, IdEmpresa). 1PL/2PL: config por (IdBodega, IdPropietario).

**Hallazgos brutales nuevos**:
- 🔥 **El algoritmo de reserva 2028 está completamente reescrito**. `clsLnStock_res_Partial.vb` pasó de ~600 a **4374 líneas**. Tiene **13+ funciones de reserva** (con variantes para NAV+BYB, lista, específico, reemplazo automático, consolidado).
- 🔥 **El ranking se hace en MEMORIA con LINQ**, no en SQL. Líneas 4564-4736 tienen el `Select Case` por `IdTipoRotacion`.
- 🔥 **`IdTipoRotacion` vive en 3 tablas** (cascada de precedencia probable): `producto.IdTipoRotacion`, `bodega_ubicacion.IdTipoRotacion`, `i_nav_config_enc.IdTipoRotacion`.
- 🔥 **`tipo_rotacion` tiene 4 valores**: 1=FIFO, 2=LIFO, 3=FEFO, **4=UPSR** (acrónimo aún sin definir — Q-* abierta).
- 🔥 **Existe función `Reserva_Stock_NAV_BYB`** específica para BECO+NAV (la única función de reserva nombrada por cliente).
- 🔥 **Existe carpeta `CEALSAMI3/`** — variante específica de MI3 para CEALSA (app de sync standalone, no WCF). Contiene `dsUbicSug` (motor de ubicación sugerida).
- 🔥 **`clsLnTrans_picking_det_Partial.vb` 1925 líneas modificadas en 2028** — algoritmo de picking también reescrito masivamente.

**Modelo de datos confirmado**:
- `bodega_ubicacion`: 38 cols, MERCOPAN tiene 2.903 ubicaciones en 7 niveles (0-6), nivel 0=muelle, nivel 1=picking
- `producto_presentacion`: cols `factor`, `EsPallet`, `IdPresentacionPallet`, `CamasPorTarima`, `CajasPorCama` permiten calcular Z pallets desde N cajas
- `tipo_rotacion`: catálogo {FIFO, LIFO, FEFO, UPSR}

**Q-* nuevas derivadas (+7)** — agregadas a `CUESTIONARIO_CAROLINA.md` bloque 12:
- Q-UPSR-MEANING (media)
- Q-ROTACION-PRECEDENCIA (media)
- Q-CLAVAUD-THRESHOLD (alta)
- Q-MI3-VS-CEALSAMI3 (media)
- Q-DSUBICSUG-ALGORITMO (alta)
- Q-RESERVA-MULTIPLE-VARIANTES (media)
- Q-REEMPLAZO-AUTO (alta)

**Métricas post-Wave 8**:
- Q-* totales: ~102 (+7)
- Q-* resueltas: 16/102 (15.7%) — Wave 6.1: 1, Wave 6.2: 7, Wave 7: 4, Wave 8: 4
- Q-* alta prioridad abiertas: ~12
- Q-* críticas: 1 (Q-SEC-OPENAI-KEY-LEAK, sin cambio)
- Líneas brain total: ~4.180
- Archivos brain: 10 principales

**Próximo recomendado**:
1. Resolver Q-UPSR-MEANING + Q-ROTACION-PRECEDENCIA con un grep + lectura puntual (< 30 min)
2. Inspeccionar `dsUbicSug.xsd` para entender el algoritmo de ubicación sugerida
3. Mapear las 13+ funciones de `clsLnStock_res_Partial.vb` (matriz: cuándo se usa cada una, por cliente)
4. Algo que Erik priorice

---

## Wave 9 (2026-04-29) — Casos naturales de reserva + nuevo diario `naked-erik-anatomy`

**Detonante**: Erik pidió mapear matriz "función de reserva × cliente × caso de uso" después de mencionar que **los 4 módulos más complejos del WMS son**: algoritmo de reserva, ubicación sugerida, reemplazo en HH durante picking, verificación. Reveló además 5 parámetros operativos nuevos. Pidió crear un **diario `naked-erik-anatomy`** versionado con tono técnico-poético-sarcástico.

**Nuevos folders del brain**:
- 🆕 `wms-test-natural-cases/` — 7 docs creados (00-INDEX + casos 01-07)
- 🆕 `naked-erik-anatomy/` — 2 docs (000-prólogo + 001 primer entry)

**Hallazgos brutales**:
- 🔥 **`clsLnStock_res_Partial.vb` tiene >26.680 líneas** (no 4.374 como estimaba). Tres adapters específicos por canal: `Reserva_Stock_From_Reabasto` (línea 9856), `Reserva_Stock_From_MI3` (línea 18192), `Reserva_Stock_From_SAP` (línea 26680).
- 🔥 **`producto_estado` tiene 14 estados granulares** (Conforme, Cuarentena, No Encontrado, Avería de Importación, No conforme, etc) con flags `utilizable` + `dañado` + `tolerancia_dias_vencimiento`.
- 🔥 **`cliente` tabla tiene 9 flags operativos** (no estaban mapeados): `IdUbicacionAbastecerCon`, `IdUbicacionManufactura`, `realiza_manufactura`, `despachar_lotes_completos`, `control_ultimo_lote`, `control_calidad`, `es_bodega_recepcion`, `es_bodega_traslado`, `es_proveedor`.
- 🔥 **`cliente_tiempos` es N×N×N** (cliente × familia × clasificación → días tolerados). Tiene `Dias_Local` y `Dias_Exterior` distintos.
- 🔥 **MERHONSA tiene typo histórico**: dos columnas `explosion_automatica_nivel_max` y `explosio_automatica_nivel_max` (sin 'n') conviven en schema. Deuda técnica visible.
- 🔥 **`Reemplazo_Automatico` NO es WMS-driven**: siempre se llama desde HH (TOMHHWS.asmx + frmCantidadreemplazo). Es operador-driven con asistencia automática.
- 🔥 **`Lote_Numerico` aparece en 5 tablas** (recepción + despacho + barras pallet + interfaces NAV) — sistema completo de lote correlativo transversal.

**Q-* RESUELTAS en Wave 9 (5)**:
- ✅ **Q-EXPLOSION-EXISTE** — confirmado: `explosion_automatica` + `explosion_automatica_desde_ubicacion_picking` + `explosion_automatica_nivel_max` en `i_nav_config_enc`
- ✅ **Q-RESTRICCION-UBICACION-CLIENTE** — `cliente.IdUbicacionAbastecerCon` (existe en código y schema)
- ✅ **Q-LOTE-NUM-EXISTE** — confirmado: `cliente.control_ultimo_lote` + `trans_re_det_lote_num` + `trans_despacho_det_lote_num`
- ✅ **Q-TOLERANCIA-MULTI-NIVEL** — confirmada cascada: `cliente_tiempos` > `producto.tolerancia` > `producto_estado.tolerancia_dias_vencimiento` > `i_nav_config_enc.dias_vida_defecto_perecederos`
- ✅ **Q-FROM-CHANNEL-FUNCTIONS** — 3 adapters confirmados: `From_MI3`, `From_SAP`, `From_Reabasto`

**Q-* nuevas (+10)** — (sin renumerar el cuestionario formal aún, pendiente próxima wave):
- Q-CLAVAUD-FALLBACK, Q-EXPLOSION-NIVEL-MAX-DEFAULT, Q-MERHONSA-DOBLE-COLUMNA-EXPLOSION
- Q-RESTRICCION-FALLBACK, Q-LOTE-NUM-SALTO, Q-LOTE-NUM-MTIPO
- Q-TOLERANCIA-PRECEDENCIA, Q-TOLERANCIA-DIAS-LOCAL-VS-EXTERIOR, Q-DAÑADO-SE-DESPACHA
- Q-FROM-MI3-DIFF, Q-FROM-SAP-DIFF, Q-FROM-REABASTO-DIFF
- Q-NAV-BYB-WHY, Q-REEMPLAZO-WHO-DECIDES

**Métricas post-Wave 9**:
- Q-* totales: ~115 (+13)
- Q-* resueltas: 21/115 (18.3%) — Wave 6.1: 1, 6.2: 7, 7: 4, 8: 4, 9: 5
- Q-* alta prioridad abiertas: ~18
- Q-* críticas: 1 (Q-SEC-OPENAI-KEY-LEAK, sin cambio)
- Líneas brain total: ~6.500 (+2.300 por casos naturales + diario)
- Archivos brain: **19** (10 antes + 7 casos naturales + 2 diario)

**Próximo recomendado**:
1. Inspeccionar el cuerpo de `Reserva_Stock_From_MI3`, `_From_SAP`, `_From_Reabasto` (entender qué los hace distintos del core)
2. Mapear `dsUbicSug.xsd` (motor de ubicación sugerida CEALSAMI3) — Erik dice que es uno de los 4 módulos más complejos
3. Caso 10 — reemplazo en HH durante picking (flujo end-to-end con `frmCantidadreemplazo`)
4. Otra anécdota nueva de Erik para nuevo entry del diario
