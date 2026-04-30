---
id: TAXONOMIA
tipo: agent-context
estado: vigente
relacionado_con: [NUMERACION, RAMAS_Y_CLIENTES]
tags: [agent-context/convencion, agent-context/taxonomia]
---
# TAXONOMIA — Donde vive cada cosa en el brain

> **Ultima actualizacion**: 2026-04-30 — creacion inicial post acuerdo con Erik
> sobre estructura tipo Microsoft (known issues, open/closed cases).
>
> **Aplicable a partir de**: BUG-001, CP-013 (movido), FEAT-001 (renombrado de CP-016).
> Los CPs anteriores (CP-001 a CP-012) viven todavia en `debuged-cases/` y se
> migran cuando se revisen uno por uno.

---

## 1. Las 4 categorias

```
brain/
  customer-open-cases/         <- casos por cliente, ABIERTOS
  customer-closed-cases/        <- casos por cliente, CERRADOS (Definition of Done cumplido)
  wms-known-issues/             <- bugs del producto (cuando un caso ya se sabe afecta a >=2 clientes)
  wms-incorporated-features/    <- features ya en codigo, requieren validacion cross-cliente
  debuged-cases/                <- LEGACY, contiene CP-001 a CP-012 sin migrar todavia
```

---

## 2. Decision tree: ¿donde lo pongo?

```
¿Es un caso de un cliente especifico?
  |
  +-- SI -> ¿Esta cerrado segun Definition of Done?
  |        |
  |        +-- SI  -> customer-closed-cases/CP-NNN-cliente-tema/
  |        +-- NO  -> customer-open-cases/CP-NNN-cliente-tema/
  |
  +-- NO -> ¿Es un bug del producto que afecta >=1 cliente?
           |
           +-- SI -> wms-known-issues/BUG-NNN-titulo-corto/
           |
           +-- NO -> ¿Es un feature ya incorporado en codigo que necesita validar?
                    |
                    +-- SI -> wms-incorporated-features/FEAT-NNN-titulo-corto/
                    |
                    +-- NO -> revisar si encaja en otra carpeta del brain
                              (decisions/, learnings/, _proposals/, etc.)
```

---

## 3. Convencion de prefijos

| Prefijo | Donde vive | Que materializa | Naming completo |
|:--:|---|---|---|
| `CP-NNN` | `customer-open-cases/` o `customer-closed-cases/` | Caso de un cliente especifico (sintoma observado por el cliente) | `CP-NNN-cliente-tema/` |
| `BUG-NNN` | `wms-known-issues/` | Bug del producto que ya se confirmo afecta >=1 cliente | `BUG-NNN-titulo-corto/` |
| `FEAT-NNN` | `wms-incorporated-features/` | Feature ya en codigo que necesita validacion cross-cliente antes de promover a PRD | `FEAT-NNN-titulo-corto/` |

**Regla central**: los CPs **no se renombran cuando se materializa un BUG**.
La convencion es de Erik: "mantener hasta que se materializa". Significa:

- Un caso de cliente nace como **CP-NNN** en `customer-open-cases/`.
- Si se demuestra que el caso es un bug del producto que afecta a otros
  clientes, se **crea un BUG-NNN** en `wms-known-issues/`. El CP NO se
  renombra. El BUG cita al CP como "caso anchor".
- El CP puede tener su propia evolucion (apertura → cierre individual)
  independiente del BUG. Un CP cerrado (cliente reconciliado) puede
  convivir con un BUG abierto (otros clientes aun no reconciliados).

---

## 4. Estructura interna de cada folder

### 4.1 `customer-open-cases/CP-NNN-cliente-tema/` y `customer-closed-cases/CP-NNN-cliente-tema/`

Archivos minimos:

```
INDEX.md                    <- descripcion del caso, links, estado
REPORTE.md                  <- reporte tecnico (que se observo, que se hizo)
EVIDENCIA*.md               <- queries SQL, conteos, snapshots, capturas
INFORME-EJECUTIVO.md        <- (opcional) reporte para el cliente
PLAYBOOK-FIX.md             <- (opcional) si el fix se diseña dentro del caso
outputs/                    <- (opcional) salidas crudas de queries/scripts
```

Cuando se cierra un caso (Definition of Done cumplido):

1. Mover el folder de `customer-open-cases/` a `customer-closed-cases/`.
2. Agregar `RESOLUCION.md` adentro con:
   - fecha de cierre
   - quien valido el cierre
   - SHA del commit del fix (si aplica)
   - link a la query/golden test que confirma no-reproduccion
   - lessons learned (1-2 parrafos)
3. Si materializo un BUG, asegurar que `wms-known-issues/BUG-NNN/CASOS-RELACIONADOS.md`
   refleja el cierre del CP.

### 4.2 `wms-known-issues/BUG-NNN-titulo-corto/`

Archivos minimos:

```
INDEX.md                    <- descripcion del bug, severidad, prioridad, estado del fix
CLIENTES-AFECTADOS.md       <- matriz cliente x volumen x severidad x estado de fix
CASOS-RELACIONADOS.md       <- links a CP-NNN que evidencian el bug
PLAYBOOK-FIX.md             <- (opcional) si el fix vive aca y no en el CP anchor
TIMELINE.md                 <- (opcional) historia del bug, cuando se detecto, cuando se intento fix
```

**Definition of Done para cerrar un BUG**:
- El fix esta mergeado en la rama productiva de TODOS los clientes
  afectados.
- Cada cliente tiene validacion post-fix (SQL o golden test) que
  confirma no-reproduccion.

Cuando se cierra un BUG: mover a un futuro `wms-known-issues-resolved/`
(crear cuando aparezca el primer cierre).

### 4.3 `wms-incorporated-features/FEAT-NNN-titulo-corto/`

Archivos minimos:

```
INDEX.md                       <- descripcion del feature, autoria, mecanica
RIESGOS-CROSS-CLIENTE.md       <- (opcional, si el feature tiene riesgos por cliente)
PLAN-VALIDACION.md             <- (opcional, casos golden por cliente)
ARCHIVOS-MODIFICADOS.md        <- (opcional, listado con SHAs y diffs)
```

**Definition of Done para cerrar un FEAT**:
- El feature esta validado en QA (golden tests pasados).
- El feature esta promovido a PRD en al menos un cliente.
- Si hay riesgos cross-cliente identificados, todos estan resueltos o
  documentados como conocidos.

Cuando se cierra un FEAT: mover a un futuro `wms-features-released/` (crear
cuando aparezca el primer cierre) o agregarlo a un changelog del producto.

---

## 5. Definition of Done unificada (criterio de Erik 2026-04-30)

Un caso/bug/feature esta **CERRADO** cuando se cumplen **ambas**
condiciones:

1. **Codigo aplicado**: el fix/feature esta mergeado en la rama
   productiva del cliente correspondiente (CP) o de TODOS los clientes
   afectados (BUG/FEAT).
2. **Validacion documentada**: existe evidencia ejecutable (query SQL,
   golden test, replay de casos historicos) que confirma que el
   sintoma no se reproduce.

Mientras una de las dos falte, el caso/bug/feature sigue OPEN.

---

## 6. Flujo de promocion entre carpetas

### 6.1 CP nuevo (cliente reporta)

```
[cliente reporta sintoma]
        |
        v
  customer-open-cases/CP-NNN-cliente-tema/
        |
        |--- (si se demuestra que afecta a otros clientes)
        |    crear/actualizar wms-known-issues/BUG-NNN/
        |    sin renombrar el CP
        |
        v
  (analisis, fix, validacion)
        |
        v
  customer-closed-cases/CP-NNN-cliente-tema/  + RESOLUCION.md
```

### 6.2 BUG nuevo (cuando aparece >=1 caso por cliente)

```
[CP-NNN existente que se demuestra afecta a otros clientes]
        |
        v
  wms-known-issues/BUG-NNN-titulo-corto/
        |
        v
  CP-NNN se mantiene en customer-open-cases/, ahora cita al BUG-NNN
        |
        v
  (fix, validacion en cada cliente afectado)
        |
        v
  cuando TODOS los clientes afectados reconciliaron:
  mover BUG-NNN a wms-known-issues-resolved/ (folder a crear)
```

### 6.3 FEAT incorporado

```
[devs hacen un commit con un feature, requiere validacion antes de PRD]
        |
        v
  wms-incorporated-features/FEAT-NNN-titulo-corto/
        |
        v
  (analisis, riesgos cross-cliente, casos golden, validacion en QA)
        |
        v
  cuando esta en PRD de >=1 cliente sin regresion:
  mover a wms-features-released/ (folder a crear) o documentar en changelog
```

---

## 7. Migracion historica al 2026-04-30

| ID viejo | Tipo viejo | Ubicacion vieja | Ubicacion nueva | Notas |
|---|---|---|---|---|
| CP-013 | caso Killios WMS164 | `debuged-cases/CP-013-killios-wms164/` | `customer-open-cases/CP-013-killios-wms164/` | OPEN; ancla del BUG-001 |
| CP-014 | duplicado historico GitHub | `debuged-cases/CP-014-bug-danado-picking-transversal/` (solo en GitHub) | DEPRECATED en GitHub | reemplazado por CP-015 → BUG-001 |
| CP-015 | vista transversal | `debuged-cases/CP-015-bug-danado-picking-transversal/` | (queda con header DEPRECATED) | absorbido por BUG-001 |
| CP-016 | feature AG29042026 | `debuged-cases/CP-016-feature-AG29042026-validacion-implosion-rack/` | `wms-incorporated-features/FEAT-001-validacion-implosion-rack/` | RENOMBRADO; el contenido se mantiene casi igual |
| CP-001 a CP-012 | varios (archivos planos) | `debuged-cases/CP-NNN.md` | PENDIENTES | revisar uno por uno y reclasificar |

**Plan de migracion CP-001 a CP-012**: hacer una tarea aparte cuando
Erik tenga tiempo. Cada uno se lee, se decide:
- ¿Es un caso de cliente todavia abierto? → `customer-open-cases/`
- ¿Es un caso de cliente cerrado historicamente? → `customer-closed-cases/`
- ¿Es algo que materializa un BUG nuevo del producto? → crear `BUG-NNN`
  + mantener referencia al CP como anchor.
- ¿Es algo arquitectonico, no de cliente? → revisar si va a
  `decisions/`, `learnings/`, etc.

---

## 8. Numeracion (referencia rapida — fuente unica `agent-context/NUMERACION.md`)

Al **2026-04-30**:
- CP libre: **CP-017+** (CP-013 movido; CP-014 deprecada GitHub; CP-015 deprecada; CP-016 renombrado FEAT-001)
- BUG libre: **BUG-002+** (existe BUG-001)
- FEAT libre: **FEAT-002+** (existe FEAT-001)

Reglas anti-conflicto:
- No reusar numeros nunca (incluso si se deprecada).
- Antes de crear un ID nuevo, hacer `ls` o `rg` del prefijo en el subdir
  correspondiente. Esta tabla puede estar desactualizada.

---

## 9. Que NO va en estas 4 carpetas

Para evitar overload de las nuevas carpetas, lo siguiente sigue en sus
ubicaciones historicas:

| Tipo de contenido | Donde vive |
|---|---|
| Decisiones arquitectonicas | `decisions/ADR-NNN-*.md` |
| Aprendizajes generales | `learnings/` |
| Hipotesis del agente sin confirmar | `_proposals/YYYYMMDD-HHMM-HNN-*.md` |
| Trace tecnico de codigo (cross-CP) | `code-deep-flow/traza-NNN-*.md` |
| Casos del motor de reserva | `casos-observados/CASO_#X.md` |
| Datos crudos compartidos | `data-seek-strategy/templates/outputs/` |
| Informacion por cliente (config, ramas, contactos) | `client-index/cliente.yml` o `clients/cliente.md` |
| Convenciones generales | `agent-context/` (este archivo) |

---

## 10. Cross-refs

- Convencion de IDs: `agent-context/NUMERACION.md`
- Como sincronizar con GitHub: `agent-context/GITHUB_SYNC.md`
- Convencion historica de CPs (legacy): `debuged-cases/CONVENCION.yml`
- Indice general del brain: `_index/INDEX.md`
