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
