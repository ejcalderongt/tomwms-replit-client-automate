# NUMERACION.md — Prefijos de IDs en el brain

> Que prefijo usar para que tipo de cosa, cual es el numero que sigue, y
> donde mirar para no chocar.

**Ultima actualizacion**: 2026-04-30 (post taxonomia nueva, ver
`agent-context/TAXONOMIA.md`: customer-open-cases / customer-closed-cases /
wms-known-issues / wms-incorporated-features). Validar el "siguiente libre"
cada vez que vayas a crear un ID nuevo (no asumir que esta tabla esta al dia).

---

## 1. Tabla maestra

| Prefijo | Para que | Donde vive | Siguiente libre (al 2026-04-30) | Como verificar |
|---|---|---|---|---|
| `ADR-NNN` | Architecture Decision Records | `brain/decisions/NNN-*.md` | **006** | `ls brain/decisions/` |
| `CP-NNN` | Caso de un cliente especifico (sintoma reportado u observado en una instalacion) | `brain/customer-open-cases/CP-NNN-cliente-tema/` o `brain/customer-closed-cases/CP-NNN-cliente-tema/` (legacy: `brain/debuged-cases/CP-NNN.md` para CP-001..CP-012 sin migrar) | **017** | `ls brain/customer-open-cases/ brain/customer-closed-cases/ brain/debuged-cases/` |
| `BUG-NNN` | Bug del producto TOMWMS (cuando se confirma que un caso de cliente afecta a >=1 cliente, el caso anchor se mantiene como CP) | `brain/wms-known-issues/BUG-NNN-titulo-corto/` | **002** | `ls brain/wms-known-issues/` |
| `FEAT-NNN` | Feature ya incorporado en codigo, requiere validacion cross-cliente antes de PRD | `brain/wms-incorporated-features/FEAT-NNN-titulo-corto/` | **002** | `ls brain/wms-incorporated-features/` |
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

- **CP es siempre un caso de un CLIENTE especifico** (sintoma reportado u observado).
- **Dos formatos validos**:
  - **Archivo plano** `CP-NNN.md` para casos chicos. Asi son CP-001 a CP-012 (legacy).
  - **Subcarpeta** `CP-NNN-cliente-tema/` con al menos:
    - `INDEX.md` (descripcion del caso, vinculos)
    - `EVIDENCIA*.md` (queries, conteos, citas del log)
    - `REPORTE*.md` (reporte tecnico o ejecutivo)
    - `PLAYBOOK-FIX.md` (si aplica)
    Asi es CP-013 (`customer-open-cases/CP-013-killios-wms164/`).
- Ejemplo carpeta: `customer-open-cases/CP-013-killios-wms164/`.
- Ejemplo archivo plano: `debuged-cases/CP-005.md` (legacy).
- **Numerar de corrido** independientemente de cliente o severidad.
- **No reusar numeros nunca**, ni siquiera si el CP fue deprecado.
- **No renombrar un CP cuando se materializa un BUG**. La convencion es
  "mantener hasta que se materializa": el CP es la ocurrencia del cliente,
  el BUG es la raiz del producto. Un BUG cita N CPs como anclas.
- **Promocion entre carpetas**:
  - Caso nuevo abierto -> `customer-open-cases/CP-NNN-cliente-tema/`.
  - Cuando cumple Definition of Done (fix mergeado + validacion post-fix
    documentada) -> mover a `customer-closed-cases/CP-NNN-cliente-tema/` y
    agregar `RESOLUCION.md`.
- Ver detalle de la taxonomia y flujos en `agent-context/TAXONOMIA.md`.

### 2.3 BUG-NNN

- **BUG es del PRODUCTO**, no de un cliente. Aparece cuando un CP de
  cliente se confirma que afecta a >=1 cliente.
- Vive en: `brain/wms-known-issues/BUG-NNN-titulo-corto/`.
- Naming: `BUG-NNN-titulo-corto-en-kebab-case`.
  Ejemplo: `BUG-001-danado-picking-no-resta-inventario`.
- Archivos minimos:
  - `INDEX.md` (descripcion del bug, severidad, prioridad, estado del fix).
  - `CLIENTES-AFECTADOS.md` (matriz cliente x volumen x severidad x estado).
  - `CASOS-RELACIONADOS.md` (links a los CP-NNN que evidencian el bug).
- **Definition of Done para cerrar un BUG**:
  1. El fix esta mergeado en la rama productiva de TODOS los clientes
     afectados.
  2. Cada cliente afectado tiene validacion post-fix (SQL o golden test)
     que confirma no-reproduccion.
- Cuando se cierra: mover a `wms-known-issues-resolved/` (folder a crear).
- Ver detalle en `agent-context/TAXONOMIA.md`.

### 2.4 FEAT-NNN

- **FEAT es un feature ya incorporado en codigo** que requiere validacion
  cross-cliente antes de promover a PRD.
- Vive en: `brain/wms-incorporated-features/FEAT-NNN-titulo-corto/`.
- Naming: `FEAT-NNN-titulo-corto-en-kebab-case`.
  Ejemplo: `FEAT-001-validacion-implosion-rack`.
- Archivos minimos:
  - `INDEX.md` (descripcion, autoria con tags `'#XX...`, mecanica del codigo).
  - opcionalmente `RIESGOS-CROSS-CLIENTE.md`, `PLAN-VALIDACION.md`,
    `ARCHIVOS-MODIFICADOS.md`.
- **Definition of Done para cerrar un FEAT**:
  1. Validado en QA con golden tests.
  2. Promovido a PRD en >=1 cliente sin regresion.
  3. Riesgos cross-cliente identificados resueltos o aceptados.
- Cuando se cierra: mover a `wms-features-released/` o agregarlo a un
  changelog del producto.

### 2.5 C-NNN

- Formato: bullet en `brain/colas-pendientes.md` con sintaxis:
  ```
  - [C-NNN] (YYYY-MM-DD) titulo corto
    - Detalle:
    - Bloqueado por:
    - Quien lo abre:
  ```
- Estado se marca con `[ ]` o `[x]` al inicio.
- Cuando se cierra: mover entrada a `brain/_processed/colas-cerradas.md`.

### 2.6 Q-NOMBRE

- Para preguntas abiertas con dueño identificable (cliente, Erik, equipo).
- Naming descriptivo, no numerico: `Q-MIGRACION-2023-A-2028`,
  `Q-MERCOPAN-MERCOSAL`, `Q-DALCORE-PROPOSITO`.
- Vive donde tiene sentido (en general en seccion "Q-* abiertas" del archivo
  donde se origino la pregunta).

### 2.7 H## (Hallazgos / hipotesis)

- Formato del filename: `YYYYMMDD-HHMM-HNN-titulo-corto.md`.
- Ejemplo: `20260428-1900-H01-tras-wms-reservastock-muerto.md`.
- `NN` se reinicia cada dia (H01, H02... del dia 28-04, despues H01 del 29-04).
- Vive en `brain/_proposals/`. Cuando un hallazgo se confirma, se promueve
  a `brain/learnings/` o a un `CP-NNN`. Si se descarta, se mueve a
  `brain/_processed/`.

### 2.8 P-NN (Preguntas de ciclo)

- Numeradas dentro de un archivo de preguntas de un ciclo concreto.
- Ejemplo: `P-15` en `brain/wms-specific-process-flow/preguntas-ciclo-7.md`.
- No tienen alcance global; viven dentro del archivo del ciclo.

### 2.9 Wave-N

- Una "wave" es una sesion completa de scan / analisis cross-repo o
  cross-BD. No tiene formato de archivo dedicado: aparece en commits
  (`Wave 6: scan exhaustivo TOMHH2025...`) y en `_inbox/` o `_proposals/`.
- Conteo conocido al 2026-04-30: Wave 1 a 6.2 (la 6.2 son quick wins).
  Proxima sera Wave 7.

### 2.10 CASO_#X y LLR_CASO_#X

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
ADR libres:    ADR-006+   (existen 003, 004, 005 - ver brain/decisions/)
CP libres:     CP-017+    (CP-013 movido a customer-open-cases/, CP-015 deprecada, CP-016 renombrado a FEAT-001)
BUG libres:    BUG-002+   (existe BUG-001 — primer fine-tune issue de Erik)
FEAT libres:   FEAT-002+  (existe FEAT-001 — antes era CP-016)
C libres:      C-020+     (C-001 a C-005 en GitHub; C-006 a C-011 agregadas con traza-002; C-012 a C-019 agregadas con CP-016/FEAT-001)
H libres:      por dia. Para hoy 2026-04-30: H01+
Wave libre:    Wave 7+    (Wave 6.2 fue la ultima)
```

### Snapshot post-migracion 2026-04-30

**`brain/customer-open-cases/`** (casos de cliente abiertos):
- `CP-013-killios-wms164/` (caso Killios WMS164, 16 archivos + outputs/, OPEN, ancla del BUG-001)

**`brain/customer-closed-cases/`** (casos de cliente cerrados): vacia
todavia.

**`brain/wms-known-issues/`** (bugs del producto):
- `BUG-001-danado-picking-no-resta-inventario/` (3 archivos: INDEX, CLIENTES-AFECTADOS, CASOS-RELACIONADOS)

**`brain/wms-incorporated-features/`** (features incorporados pendientes de validacion):
- `FEAT-001-validacion-implosion-rack/` (1 archivo: INDEX, antes era CP-016)

**`brain/debuged-cases/`** (LEGACY, pendiente de migracion):
- Archivos planos: `CP-001.md` ... `CP-012.md` — pendientes de revision uno por uno.
- `CP-013.md` — duplicado historico del CP-013 anclado en customer-open-cases (ver nota).
- `CP-014-bug-danado-picking-transversal/` — DEPRECATED (en GitHub solo).
- `CP-015-bug-danado-picking-transversal/` — DEPRECATED, header con [DEPRECATED -> ver BUG-001].
- `CP-016-feature-AG29042026-validacion-implosion-rack/` — vacia (movido contenido a FEAT-001).
- `00-INDEX.md`, `CONVENCION.yml` — apuntan a la nueva taxonomia.

> **Nota CP-013 .md vs carpeta**: en GitHub conviven `CP-013.md` (archivo plano,
> 1.4 KB, primer reporte) y `CP-013-killios-wms164/` (carpeta completa). El
> archivo plano es legacy pre-carpeta. La carpeta es la fuente de verdad y
> es la que se movio a `customer-open-cases/`.

> **Nota historica CP-014 -> CP-015 -> BUG-001**: el caso CP-014 fue
> creado el 2026-04-30 con numeracion mal asignada (CP-013 ya ocupado en
> GitHub). Se renumero a CP-015. Despues, con la nueva taxonomia (este
> turno), el "transversal" queda absorbido por `BUG-001` porque
> realmente nunca fue un caso de cliente sino una descripcion del bug
> del producto. CP-015 mantiene su carpeta en debuged-cases con header
> DEPRECATED.

> **Nota CP-016 -> FEAT-001**: el feature `#AG29042026` (validacion previa
> de implosion + orquestador unificado HH) era CP-016. Con la taxonomia
> nueva, los features incorporados pendientes de validacion viven en
> `wms-incorporated-features/`. Se renombro a `FEAT-001-validacion-implosion-rack/`.
> El INDEX.md mantiene su contenido (24 KB, mecanica completa) con un
> header chico de "renombrado de CP-016".

> **Pendiente CP-001 a CP-012**: revisar cada uno y reclasificar.
> Probablemente la mayoria a `customer-closed-cases/`. Hacer cuando
> Erik tenga tiempo.
