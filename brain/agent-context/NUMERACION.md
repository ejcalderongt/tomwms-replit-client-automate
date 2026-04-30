# NUMERACION.md — Prefijos de IDs en el brain

> Que prefijo usar para que tipo de cosa, cual es el numero que sigue, y
> donde mirar para no chocar.

**Ultima actualizacion**: 2026-04-30. Validar el "siguiente libre" cada vez
que vayas a crear un ID nuevo (no asumir que esta tabla esta al dia).

---

## 1. Tabla maestra

| Prefijo | Para que | Donde vive | Siguiente libre (al 2026-04-30) | Como verificar |
|---|---|---|---|---|
| `ADR-NNN` | Architecture Decision Records | `brain/decisions/NNN-*.md` | **006** | `ls brain/decisions/` |
| `CP-NNN` | Casos Practicos debuggeados (bug encontrado + reproducido + cita evidencia) | `brain/debuged-cases/CP-NNN-*/` | **001** | `ls brain/debuged-cases/` |
| `C-NNN` | Colas de investigacion pendientes (preguntas operativas, no del WMS) | `brain/colas-pendientes.md` (lista plana, no archivos) | **001** | leer el archivo |
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

- Formato: subcarpeta `CP-NNN-bug-titulo-corto/` con al menos:
  - `README.md` (descripcion del caso)
  - `EVIDENCIA.md` (queries, conteos, citas del log)
  - `REPRODUCIR.md` (pasos exactos)
  - `FIX-PROPUESTO.md` (si aplica)
- Ejemplo: `CP-001-stock-negativo-tras-ajuste-ciclico/`.
- **Numerar de corrido** independientemente de cliente o severidad.

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
ADR libres:    ADR-001, 002, 006+   (existen 003, 004, 005)
CP libres:     CP-001+              (debuged-cases vacio en workspace local;
                                     en /tmp habia un intento CP-014 que se
                                     descarto por mala numeracion. Ver
                                     DRIFT_DETECTADO.md.)
C libres:      depende del estado actual de colas-pendientes.md (no en
               workspace local todavia, esta solo en GitHub rama wms-brain)
H libres:      por dia. Para hoy 2026-04-30: H01+
Wave libre:    Wave 7 (Wave 6.2 fue la ultima)
```

> Nota CP-014: el numero **no es valido**. La numeracion de CP arranca en
> CP-001. El "CP-014" del trabajo en `/tmp/wms-brain-fresh/` fue un error de
> mi parte (agente Replit) — pense que la numeracion ya iba alta, sin
> verificar. Cuando se traiga ese caso al brain canonico, se renumera a
> `CP-001-bug-danado-picking-transversal/`.
