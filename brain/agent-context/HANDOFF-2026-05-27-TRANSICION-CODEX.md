---
id: HANDOFF-2026-05-27-TRANSICION-CODEX
tipo: handoff
estado: vigente
titulo: Handoff transicion gobernanza del brain a Codex (agente local)
fecha: 2026-05-27
from: mary-jane (agente del Replit workspace)
to: codex (curador del brain) + agente local de Codex
ramas: [main, dev_2026_mampa, dev_2028_merge]
clientes: [killios, mampa, becofarma, byb, cealsa, mercopan, merhonsa]
tags: [handoff, gobernanza, brain, codex, agente-local, transicion]
---

# Handoff — Transicion gobernanza del brain a Codex

Codex, te dejo un mapa denso de lo que hay armado, las decisiones tomadas, y como
conviene preparar el contexto para que tu agente local consuma el brain sin
saturarse. Esto **complementa** lo que ya empezaste; no reescribe nada de tu lado.

---

## 1. Reparto de roles (acordado con EJC)

| Rol | Responsabilidad | Surface |
|---|---|---|
| **Codex** | Curador del brain, gobernanza, agente local de performance | `wms-brain/brain/`, `wms-brain/scripts/`, tu agente local |
| **Mary Jane** (yo, agente del Replit workspace) | Lead operativo: fixes BOF/HH, queries a produccion, snapshots, atlas | Repos Azure DevOps (TOMWMS_BOF, TOMHH2025), BD productiva SQL Server, replit workspace |
| **EJC** | Owner tecnico, unico autorizado a aprobar commit/push | Todo |

Tu agente local y yo somos **canales paralelos** sobre el mismo brain.
Coordinamos via el bridge (`brain/BRIDGE.md`, schema v2). Vos sos el dueno del
esquema y del flujo de curacion; yo soy quien lo alimenta con material operativo
nuevo (handoffs de cliente, fixes en produccion, snapshots de BD).

---

## 2. Estado del brain hoy — lo que NO debes rehacer

### 2.1 Bridge ya consolidado en schema v2 (commit BRIDGE.md changelog)

- `brain/BRIDGE.md` documenta el flujo: `_inbox/<id>.json` → `analyze` →
  `_proposals/<id>.md` → edicion manual de .md → `apply` → `_processed/`.
- Schema v2 (2026-04-27, ciclo-11) agrego 3 tipos para investigacion SQL
  (`question_request`, `question_answer`, `learning_proposed`) y el estado
  terminal `answered`.
- `scripts/brain_bridge.mjs` con `notify | list | show | analyze | apply | skip`.
- Politica fundamental: **el bridge nunca edita el brain automaticamente**.

### 2.2 Estructura del brain (lo importante para el agente local)

```
brain/
├── BRIDGE.md                        ← protocolo de actualizacion (LEER PRIMERO)
├── replit.md                        ← 9 reglas vinculantes + identidad EJC
├── way-of-thinking.md
├── README.md
├── _index/                          ← ATLAS cliente-aware
├── _inbox/<id>.json                 ← cola pending
├── _proposals/<id>.md               ← propuestas heuristicas
├── _processed/<id>.json             ← procesados
├── agent-context/                   ← contexto duradero para agentes (este handoff vive aqui)
├── architecture/
├── brain-map/
├── clients/<cliente>/
├── code-changes/{BOF,HH}/PATTERNS-*.md
├── code-deep-flow/DIFF-2023-VS-2028-*.md
├── colas-pendientes.md
├── _conventions/
├── customer-{open,closed}-cases/
├── data-deep-dive/<cliente>/snapshot-*.md
├── ddl-funcional/
├── debuged-cases/
├── decisions/
├── entities/
├── fingerprint/
├── heat-map-params/
├── learnings/L-NNN-*.md
├── naked-erik-anatomy/
├── outputs/
├── _proposals/                     (duplicado del de arriba, mismo dir)
├── release-notes/
├── sendero-producto/
├── skills/
├── sql-catalog/
├── tasks-historicas/
├── test-scenarios/
├── wms-agent/                      ← tu CLI (wmsa)
├── wms-brain-client/
├── wms-incorporated-features/
├── wms-known-issues/
├── wms-specific-process-flow/
└── wms-test-natural-cases/
```

### 2.3 Sync state — commits nuevos no reflejados en Janeway

Janeway runId=56 indexo TOMWMS_BOF `a599caf` (`dev_2028_merge`) y TOMHH2025
`284c7da9`. Cualquier query semantica que haga tu agente contra Janeway esta
desfasada respecto a estos 3 commits nuevos:

| Commit | Repo | Rama | Que hace |
|---|---|---|---|
| `aef063e` | TOMWMS_BOF | `dev_2026_mampa` | Fix casing bulkcopy (cliente MAMPA) |
| `895d843` | TOMWMS_BOF | `dev_2028_merge` | Port del fix de casing bulkcopy |
| `7511135` | TOMWMS_BOF | `dev_2026_mampa` | Perf 3-en-1 sobre `Cargar_Datos_Comparativos` (aplicar inventario) |

Antes de razonar sobre codigo, **trigger un re-index de Janeway** o avisame y te
genero un delta-patch para el brain con los hunks de los 3 commits.

---

## 3. Reglas vinculantes que tu agente DEBE respetar

Estan en `brain/replit.md` §4 (version completa alli). Las 9 reglas que NO se
discuten:

1. **Migracion XML→JSON: oportunista**. Legacy estable no se migra; funcionalidad
   nueva → JSON.
2. **Patron JSON estandar: Forma A** — wrapper `{data, error}` + `JavaScriptSerializer` +
   status 200/500.
3. **Mantener `n`** — eliminar `.replace("n","n")` en `WebService.java:352`. La HH
   procesa UTF-8.
4. **NO commit/push automatico sin permiso explicito de EJC**. Tu agente debe
   parar antes del `git push` y pedir luz verde.
5. **NO mezclar** cambios HH (Android) y backend (VB.NET) en el mismo commit.
6. **`Cantidad` en UMBAS** para familias `stock`, `movimientos`, `stock_res` (HH y
   BOF). En `trans_picking_ubic` la logica es presentacion si existe, UMBAS si no.
   Ver `code-changes/HH/PATTERNS-UMBAS.md` y handoffs `2026-05-20-hh-*`.
7. **Archivos de operacion local de agentes** (`AGENTS.md`, `CLAUDE.md`,
   `.cursorrules`, `.codex/`, etc.) **NO viajan con codigo WMS**. Branch separado
   `prograx-local-codex-backup-recovery` en el repo de respaldo.
8. **Capas WMSWebAPI (.NET Core)**: `DALCore` → `EntityCore` → `Services` →
   `Controller` (con Forma A). No mezclar responsabilidades. Ver
   `code-changes/BOF/PATTERNS-WMSWEBAPI-LAYERS.md`.
9. **Estatus OC MI3** se resuelve por `trans_oc_enc.Referencia`, NO por
   `IdOrdenCompraEnc`. Modelo: `trans_oc_enc → trans_re_oc → tarea_hh` con
   `IdTipoTarea=1`. Ver `code-changes/BOF/PATTERNS-OC-MI3.md`.

Si tu agente local viola alguna de estas reglas en una sugerencia, EJC lo va a
frenar manualmente — pero mejor que tu prompt del agente las cargue en la system
instruction inicial.

---

## 4. Como digerir el brain sin saturar contexto

Esto es lo que aprendi operando como agente sobre el brain. Pasalo a tu agente
local para evitar context bloat:

### 4.1 Patron "indice + topic file"

El brain esta creciendo y ya no entra entero en una ventana de contexto. La regla
operativa:

- **`_index/ATLAS.md`** se carga siempre al inicio. Es el TOC cliente-aware. Si
  pesa mas de ~600 lineas, partilo por seccion y deja solo los pointers en ATLAS.
- **Topic files** se cargan **bajo demanda** segun la query del usuario. Cada
  topic file debe tener frontmatter:
  ```yaml
  ---
  name: <titulo corto>
  description: <una linea — que cubre, para decidir si cargarlo>
  clientes: [becofarma, killios, mampa, ...]
  ramas: [dev_2026_mampa, dev_2028_merge]
  tags: [bulkcopy, inventory, performance]
  ---
  ```
  Esto permite que el agente seleccione que cargar via grep sobre el frontmatter,
  no leyendo el body.

### 4.2 Priorizacion por "frecuencia × especificidad"

Para un agente local con presupuesto de tokens limitado, ordene asi que meter en
system prompt vs que dejar para retrieval:

| Tier | Que va | Donde |
|---|---|---|
| **0 — siempre cargado** | Las 9 reglas vinculantes + identidad de roles + bridge protocol + tabla de equipo/iniciales commits | System prompt fijo |
| **1 — cargar por triage** | ATLAS.md + lista de cliente activo + lista de SPs/tablas tocadas en el ultimo commit | Primer turno de cada conversacion |
| **2 — retrieval bajo demanda** | Snapshots por cliente, DIFFs 2023↔2028, flags-callsites, handoffs historicos, patterns | Grep + read on-demand |
| **3 — solo si EJC lo pide** | Raw JSONs de snapshots, dumps de queries, logs de Janeway | Nunca proactivo |

### 4.3 Anti-patrones que vi consumir contexto inutilmente

- **Leer frmInventario.vb entero (10K lineas)** cuando solo necesitas una funcion.
  Siempre `rg -n "Sub <nombre>"` primero, despues `read offset/limit`.
- **Hay dos copias** de varios forms (ej. `TOMIMSV4/Transacciones/Inventario/frmInventario.vb`
  9555 lineas vs `TOMIMSV4/TOMIMSV4/Transacciones/Inventario/frmInventario.vb`
  10184 lineas). **La activa segun `.vbproj` es la segunda**. La primera es
  orphan y NO compila. Documentalo en `code-changes/BOF/PATTERNS-FILE-DUPLICATES.md`
  para que tu agente no pierda 30 min editando el archivo equivocado (me paso hoy).
- **Repetir `git ls-remote` cada turno** para verificar credenciales. Cachealo
  en memoria de la sesion.
- **Cargar todos los snapshots cliente** cuando el ticket es de un solo cliente.
  Filtrar por cliente activo desde el primer turno.

### 4.4 Convenciones de commit (no inventes nuevas)

| Autor | Formato |
|---|---|
| EJC | `#EJCRP <tipo>(<area>): <msg>` |
| GT | `#GT_DDMMAAAA:` |
| AG | `#AGDDMMAAAA` |
| MA | `#MA DDMMAAAA` o `#MADDMMAAAA` |
| AT | `#AT DDMMAAAA` |
| MECR | `#MECR DDMMAAAA` |

Si tu agente sugiere commits, usa `#EJCRP` (porque es EJC quien autoriza). Tipos
que uso: `fix`, `perf`, `feat`, `refactor`, `chore`, `docs`. Area entre
parentesis del modulo afectado (ej. `inventario`, `picking`, `oc-mi3`, `bulkcopy`).

### 4.5 Tags inline en codigo

Convencion que ya esta en uso para trazar cambios en codigo:
```
'#EJC20260523_<MODULO>_<TIPO>: <descripcion de una linea>
```
Ej: `#EJC20260523_INSERTBATCH_TRACE`,
`#EJC20260523_CARGAR_DATOS_COMPARATIVOS_PERF_A`. Permite grep historico de toda
intervencion por fecha+modulo. Tu agente deberia agregar estos tags **siempre**
cuando modifica codigo.

---

## 5. Trabajo operativo reciente que tu agente debe conocer

Para que vos puedas indexarlo en el brain. Son los 3 commits nuevos no
reflejados en Janeway runId=56:

### 5.1 `aef063e` — TOMWMS_BOF `dev_2026_mampa` (fix bulkcopy MAMPA)
- **Sintoma**: `SqlBulkCopy.WriteToServer` fallaba en `trans_inv_stock_prod`
  durante import de inventario en MAMPA con error de columna no encontrada
  aunque la collation SQL es CI.
- **Raiz**: `SqlBulkCopy.ColumnMappings.Add(source, dest)` valida `dest`
  **case-sensitive a nivel interno** del driver, independiente de la collation
  de SQL Server. 5 mismatches: `idproducto/idProducto`,
  `idpresentacion/idPresentacion`, `idunidadmedida/idUnidadMedida`,
  `Lic_plate/lic_plate`, `idproductotallacolor/IdProductoTallaColor`.
- **Fix**: `clsInsertBatch.Execute` normaliza casing destino usando
  `InsertBatchTrace_ColumnasDestino` + Dictionary CI.
- **Patron a documentar**: `code-changes/BOF/PATTERNS-SQLBULKCOPY-CASING.md`
  (lo puedo escribir yo si queres).

### 5.2 `895d843` — TOMWMS_BOF `dev_2028_merge` (port del 5.1)
- Misma raiz, distinta forma porque las dos ramas tienen versiones muy
  diferentes de `clsInsert.vb`. En `dev_2026_mampa` existe el modulo de trace
  completo (`InsertBatchTrace_*`); en `dev_2028_merge` solo existe la variante
  schema-safe (`#EJC20260523_INSERTBATCH_SCHEMA_SAFE`).
- **Adaptacion**: refactor de `GetDestinationColumns` para que devuelva
  `Dictionary<string,string>` CI (en lugar de `HashSet<string>`) — value es el
  casing exacto de `INFORMATION_SCHEMA.COLUMNS`; el bucle del bulk usa
  `TryGetValue` y pasa el casing real como destino.
- **Leccion para el agente local**: cuando portas un fix entre ramas con drift
  de codigo, NUNCA hagas cherry-pick literal — releé la estructura destino,
  adapta la idea. El cherry-pick hubiera arrastrado funciones huerfanas.

### 5.3 `7511135` — TOMWMS_BOF `dev_2026_mampa` (perf aplicar inventario)
- **Sintoma reportado por EJC**: `Cargar_Datos_Comparativos` esta
  "extremadamente pesado" al finalizar aplicar inventario.
- **Diagnostico**: archivo real
  `TOMIMSV4/TOMIMSV4/Transacciones/Inventario/frmInventario.vb` (10184 lineas,
  no la orphan de 9555). La funcion vive en linea 894-962, se invoca en linea
  1154 dentro de `Listar_Datos_De_Inventario` como paso 12 de 17 envueltos en
  **una sola transaccion SQL**.
- **3 fixes aplicados (tag `#EJC20260523_CARGAR_DATOS_COMPARATIVOS_PERF_*`)**:
  - **A**: eliminar copia field-by-field (`For Each` clonaba 17 propiedades
    por fila → `glistaInv = ListaConteos`).
  - **B**: `gviewComparativo.OptionsView.BestFitMaxRowCount = 100` antes del
    `BestFitColumns()`. DevExpress recorria todas las filas para calcular
    ancho.
  - **C**: `Commit_Transaction` movido **antes** de los refrescos de pantalla.
    Bloque `Cargar_Datos_Comparativos` + `Cargar_Conteos_Operador` +
    `Carga_Regularizacion` + `Cargar_KPI_Ubicaciones` + bloque `Inicial`
    envuelto en nueva `clsTransRefresh` con catch local que NO propaga.
- **Pendiente — Fix D** (no aplicado, requiere autorizacion): refactor del SP
  `Get_All_By_Comparacion_Inventario` para que reciba `@IdPropietario`,
  `@IdProducto`, `@IdTramo`, `@IdUbicacion` opcionales y filtre server-side, en
  vez de traer todo y filtrar con `.FindAll` en cliente.
- **Pendiente — port a `dev_2028_merge`**: EJC quiere validar en MAMPA antes.

### 5.4 Lecciones meta del ultimo dia

- **Bug arquitectural recurrente en BOF**: rutinas de "aplicar X" mezclan
  operaciones de escritura con SELECTs cosmeticos de refresh dentro de la misma
  transaccion gigante. `Cargar_Datos_Comparativos` es solo el caso de hoy;
  sospecho que `frmRegularizarInventario`, `frmPickingSugerido` y `frmRecepcion`
  tienen el mismo patron. Vale la pena un sweep proactivo. Documentacion
  sugerida: `code-changes/BOF/PATTERNS-INV-APLICAR-PERF.md`.

---

## 6. Como escribir a Mary Jane (bridge protocol)

El protocolo esta en `brain/BRIDGE.md` (schema v2). Resumen para tu agente:

- **Inbox unico**: `brain/_inbox/<id>.json` (no hay inbox direccional; el envelope
  identifica `source` y el destinatario se infiere del `type`).
- **Para hablarme** (mary-jane, agente Replit): emiti `type=directive` con
  `source=codex-local-agent`, `context.message` legible + `context.tags`
  apropiados. Yo voy a leerlo al inicio de la proxima sesion via
  `brain_bridge.mjs list`.
- **`BRIDGE-LOG`**: cada `apply`/`skip` queda registrado en el `history` del
  evento + commit de git con la `--note` o `--reason`.
- **Latencia esperada**: asincrona. Mary Jane responde cuando EJC abre sesion en
  el replit; tu agente cuando corre.
- **Conflicto de jurisdiccion**: si vos curas un patterns y yo lo modifico
  operativamente, tu version gana (sos curador). Mandame request en `_inbox/`
  con el diff que queres y lo aplico.

---

## 7. Trabajo complementario que sugiero para tu agente local

No invadiendo tu surface — solo cosas que **a vos te conviene priorizar** porque
a mi me cuesta hacerlas desde el replit:

1. **Auto-indexacion incremental del brain**: cada vez que llegue un handoff
   nuevo, parsear frontmatter y actualizar `_index/ATLAS.md` automaticamente. Yo
   no tengo eficiencia para mantener el ATLAS sincronizado a mano.
2. **Deteccion de "patterns repetidos" en handoffs**: si veo 3 handoffs
   distintos con el mismo sintoma (ej. "transaccion larga + grid refresh
   dentro"), tu agente deberia sugerir crear un `PATTERNS-*.md` consolidando.
3. **Re-index post-commit a Janeway**: hoy Janeway esta atras en 3 commits. Tu
   agente puede disparar el re-index y dejarme un `acked` en el bridge.
4. **Validacion de las 9 reglas en cada commit propuesto**: linter del brain.
   Si yo (o cualquiera) propongo un commit que viola regla #4 (push automatico)
   o #5 (mezcla HH+BOF), tu agente lo flaggea antes de que EJC tenga que vetar
   manualmente.
5. **Resumen semanal a EJC**: digest de que se commiteo, que patterns nuevos
   hay, que snapshots cambiaron significativamente.

Lo que **NO** deberias replicar de mi lado:
- Conexion directa a la BD productiva (yo ya tengo `tedious` + secreto
  `WMS_KILLIOS_DB_PASSWORD`).
- Push a Azure DevOps (yo tengo `AZURE_DEVOPS_PAT` cacheado).
- Edicion operativa de codigo BOF/HH (mi surface — yo lo hago, vos lo registras
  en el brain).

---

## 8. Pregunta concreta para vos

Cuando tu agente local este operativo, ¿queres que **a partir de ahi** yo deje
de escribir directamente a `wms-brain/brain/` y solo te mande handoffs por el
bridge, y vos los procesas e indexas? Eso te da control total del esquema y
elimina la posibilidad de que yo introduzca drift en la estructura del brain.

Si la respuesta es si, ya queda como nueva regla del bridge protocol. Avisame y
lo agrego al `BRIDGE.md` y al `replit.md` como regla #10.

---

## 9. Acuse de recibo sugerido

Cuando tu agente lea este handoff, emiti en `_inbox/`:

```json
{
  "id": "<YYYYMMDD-HHMM>-CODEX-ACK-HANDOFF-TRANSICION",
  "schema_version": "1",
  "type": "directive",
  "source": "codex-local-agent",
  "host": "<tu-host>",
  "context": {
    "message": "Acuse del handoff HANDOFF-2026-05-27-TRANSICION-CODEX. Agente local cargado con las 9 reglas y la tier-list de contexto. Respuestas a las 5 sugerencias y a la pregunta de §8 en el body.",
    "tags": ["handoff", "ack", "codex-local-agent"]
  },
  "status": "pending"
}
```

Asi cierro el ciclo y se que tu agente esta vivo y procesando.
