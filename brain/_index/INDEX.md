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
