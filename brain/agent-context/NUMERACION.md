# NUMERACION.md — Prefijos de IDs en el brain

> Que prefijo usar para que tipo de cosa, cual es el numero que sigue, y
> donde mirar para no chocar.

**Ultima actualizacion**: 2026-04-30 (post CP-016 #AG29042026). Validar el
"siguiente libre" cada vez que vayas a crear un ID nuevo (no asumir que
esta tabla esta al dia).

---

## 1. Tabla maestra

| Prefijo | Para que | Donde vive | Siguiente libre (al 2026-04-30) | Como verificar |
|---|---|---|---|---|
| `ADR-NNN` | Architecture Decision Records | `brain/decisions/NNN-*.md` | **006** | `ls brain/decisions/` |
| `CP-NNN` | Casos Practicos debuggeados (bug encontrado + reproducido + cita evidencia) — tambien admite features incorporados que requieren validacion cross-cliente | `brain/debuged-cases/CP-NNN-*/` o `brain/debuged-cases/CP-NNN.md` | **017** | `ls brain/debuged-cases/` (en GitHub rama wms-brain, NO en workspace local) |
| `C-NNN` | Colas de investigacion pendientes (preguntas operativas, no del WMS) | `brain/colas-pendientes.md` (lista plana, no archivos) | **020** | leer el archivo |
| `Q-NOMBRE` | Preguntas abiertas al WMS / cliente (no numericas, naming descriptivo) | `RAMAS_Y_CLIENTES.md` y otros | n/a (naming libre) | `rg "^- \*\*Q-" brain/` |
| `H##` (`HNN`) | Hallazgos / hipotesis del agente (con timestamp) | `brain/_proposals/YYYYMMDD-HHMM-HNN-*.md` | siguiente libre del dia | `ls brain/_proposals/ \| tail` |
| `P-NN` | Preguntas estructuradas dentro de un ciclo / wave | `brain/wms-specific-process-flow/preguntas-ciclo-N.md` | depende del ciclo | leer el archivo |
| `Wave-N` | Ola / ciclo de scan o analisis (sesion completa de aprendizaje) | nombre informal en commits y `_proposals/` | **7** (la ultima fue Wave 6.2) | leer `brain/_index/INDEX.md` y commits |
| `LLR_CASO_#X` | Rama lateral de un `CASO_#X` del motor de reserva | `casos-observados/LLR_CASO_#X.md` | depende de cuantas LLR salieron del CASO | `ls casos-observados/` |
| `CASO_#X` | Caso del motor de reserva (numerados por valor literal en `trans_pe_det_log_reserva.Caso_Reserva`) | `casos-observados/CASO_#X.md` | n/a (los numeros vienen del WMS, no los inventas vos) | mirar el log de reservas |

---

## 2. Reglas de naming

### 2.1 ADR-NNN

- Formato: `NNN-titulo-en-kebab-case.md` (3 digitos con padding).
- Ejemplo: `006-formato-yaml-clientes.md`.
- Contenido obligatorio: contexto, decision, consecuencias, alternativas
  consideradas, fecha, firma (Erik o agente que propone).
- Nunca renumerar un ADR existente. Si una decision se revierte, escribir un
  ADR posterior que la deroga (citar el numero del derogado).

### 2.2 CP-NNN

- **Dos formatos validos**:
  - **Archivo plano** `CP-NNN.md` para casos chicos. Asi son CP-001 a CP-012.
  - **Subcarpeta** `CP-NNN-bug-titulo-corto/` con al menos:
    - `INDEX.md` (descripcion del caso, vinculos)
    - `EVIDENCIA*.md` (queries, conteos, citas del log)
    - `REPORTE*.md` (reporte tecnico o ejecutivo)
    - `PLAYBOOK-FIX.md` (si aplica)
    Asi son CP-013 (`CP-013-killios-wms164/`) y CP-015
    (`CP-015-bug-danado-picking-transversal/`).
- Ejemplo carpeta: `CP-013-killios-wms164/`.
- Ejemplo archivo plano: `CP-005.md`.
- **Numerar de corrido** independientemente de cliente o severidad.
- **No reusar numeros**: si CP-014 esta deprecated/incorporado en otro,
  marcar el viejo con prefijo `[DEPRECATED]` en su INDEX y referencia al
  reemplazo, no eliminar el numero.

### 2.3 C-NNN

- Formato: bullet en `brain/colas-pendientes.md` con sintaxis:
  ```
  - [C-NNN] (YYYY-MM-DD) titulo corto
    - Detalle:
    - Bloqueado por:
    - Quien lo abre:
  ```
- Estado se marca con `[ ]` o `[x]` al inicio.
- Cuando se cierra: mover entrada a `brain/_processed/colas-cerradas.md`.

### 2.4 Q-NOMBRE

- Para preguntas abiertas con dueño identificable (cliente, Erik, equipo).
- Naming descriptivo, no numerico: `Q-MIGRACION-2023-A-2028`,
  `Q-MERCOPAN-MERCOSAL`, `Q-DALCORE-PROPOSITO`.
- Vive donde tiene sentido (en general en seccion "Q-* abiertas" del archivo
  donde se origino la pregunta).

### 2.5 H## (Hallazgos / hipotesis)

- Formato del filename: `YYYYMMDD-HHMM-HNN-titulo-corto.md`.
- Ejemplo: `20260428-1900-H01-tras-wms-reservastock-muerto.md`.
- `NN` se reinicia cada dia (H01, H02... del dia 28-04, despues H01 del 29-04).
- Vive en `brain/_proposals/`. Cuando un hallazgo se confirma, se promueve
  a `brain/learnings/` o a un `CP-NNN`. Si se descarta, se mueve a
  `brain/_processed/`.

### 2.6 P-NN (Preguntas de ciclo)

- Numeradas dentro de un archivo de preguntas de un ciclo concreto.
- Ejemplo: `P-15` en `brain/wms-specific-process-flow/preguntas-ciclo-7.md`.
- No tienen alcance global; viven dentro del archivo del ciclo.

### 2.7 Wave-N

- Una "wave" es una sesion completa de scan / analisis cross-repo o
  cross-BD. No tiene formato de archivo dedicado: aparece en commits
  (`Wave 6: scan exhaustivo TOMHH2025...`) y en `_inbox/` o `_proposals/`.
- Conteo conocido al 2026-04-30: Wave 1 a 6.2 (la 6.2 son quick wins).
  Proxima sera Wave 7.

### 2.8 CASO_#X y LLR_CASO_#X

- **No los inventas vos**. El numero viene del valor literal de
  `trans_pe_det_log_reserva.Caso_Reserva` en el WMS productivo.
- LLR (Linea Lateral de Reserva) son sub-ramas: `LLR_CASO_#28` salio del
  `CASO_#20`, `LLR_CASO_#29` del `CASO_#23`, `LLR_CASO_#31` del `CASO_#24`.
- Documentados en `casos-observados/`. Hoy son **25 casos principales + 3
  LLR**, total 28.

---

## 3. Reglas anti-conflicto

1. **Antes de crear un ID nuevo**, hacer `ls` o `rg` del prefijo en el subdir
   correspondiente. Esta tabla puede estar desactualizada.
2. **No reusar numeros nunca**, ni siquiera si el ADR/CP fue eliminado.
   Si CP-007 se descarta, queda como "DESCARTADO" en el archivo, no se
   recicla el numero.
3. **No saltar numeros** sin razon. Si tenes ADR-005 y queres ir directo a
   ADR-010, documentar por que (en general nunca tenes razon valida).
4. **No mezclar prefijos** en un mismo archivo. Si una decision arquitectonica
   sale de un debuggeo, hacer `CP-NNN` con la evidencia y `ADR-MMM` con la
   decision, y citarse entre si.

---

## 4. Estado de numeracion al 2026-04-30 (snapshot)

```
ADR libres:    ADR-006+         (existen 003, 004, 005 - ver brain/decisions/)
CP libres:     CP-017+          (existen CP-001.md a CP-014/, CP-015/ y CP-016/ creados este turno)
C libres:      C-020+           (C-001 a C-005 en GitHub; C-006 a C-011 agregadas con traza-002; C-012 a C-019 agregadas con CP-016)
H libres:      por dia. Para hoy 2026-04-30: H01+
Wave libre:    Wave 7+          (Wave 6.2 fue la ultima)
```

### Snapshot CP en GitHub rama wms-brain (al 2026-04-30)

Archivos planos: `CP-001.md` ... `CP-012.md`, `CP-013.md` (este ademas tiene
carpeta de evidencia abajo).

Carpetas:
- `CP-013-killios-wms164/` (caso Killios WMS164, 16 archivos + 2 subdirs)
- `CP-014-bug-danado-picking-transversal/` (3 archivos, RENOMBRADO en
  workspace local a CP-015 — ver bullet abajo)
- `CP-015-bug-danado-picking-transversal/` (NUEVO 2026-04-30 turno previo;
  reemplaza CP-014 con trace de codigo agregado)
- `CP-016-feature-AG29042026-validacion-implosion-rack/` (NUEVO 2026-04-30
  este turno; feature COLABORATIVO Erik+Marcela+Abigail incorporado en
  `dev_2028_merge`, requiere validacion cross-cliente antes de PRD)

> **Nota historica CP-014 -> CP-015**: el caso fue creado el 2026-04-30 con
> numero CP-014 sin verificar la lista de CPs existentes en GitHub (donde
> CP-013 ya estaba ocupado por el caso WMS164). Al traer al workspace y
> agregar el trace de codigo (`code-deep-flow/traza-002-danado-picking.md`),
> se renumera a CP-015 (siguiente libre real). El CP-014 viejo en GitHub
> queda como historico hasta que se consolide en proximo sync.

> **Nota CP-016 (este turno 2026-04-30)**: incorpora el feature
> `#AG29042026` (validacion previa de implosion + orquestador unificado
> cambio estado/ubicacion HH). Aclarando: no es un BUG sino un FEATURE ya
> incorporado en codigo (`dev_2028_merge`) que requiere VALIDACION
> cross-cliente. Se documenta en `debuged-cases/` porque no hay todavia
> una carpeta `incorporated-features/` y el flujo (analisis -> documentacion
> -> casos golden -> roll-out) es identico al de un debug case.
