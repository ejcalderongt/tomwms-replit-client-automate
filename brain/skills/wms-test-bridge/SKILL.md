---
name: wms-test-bridge
description: Operar el Bridge WMS↔Brain para automatizar pruebas multi-cliente. Cubre los tres comandos de wmsa (learn-config / make-payload / run-test), el contrato del controller en el BOF, los formatos YAML de escenarios y las reglas duras para evitar tocar producción.
---

# Skill: wms-test-bridge

> **Cuándo aplica**: cualquier tarea relacionada con (a) aprender la
> configuración de un cliente WMS, (b) generar payloads de prueba, (c)
> ejecutar pruebas contra un sandbox del WMS, o (d) interpretar/resultados
> y diff entre clientes.
>
> **No aplica para**: pruebas unitarias dentro del código BOF (eso es
> xUnit/MSTest aparte). Esta skill es para pruebas **end-to-end multi-cliente**.

Lectura previa obligatoria:
- `wms-brain/decisions/004-bridge-wms-test-automation.md` (ADR).
- `wms-brain/entities/modules/reservation/README.md` (módulo flagship).
- `wms-brain/entities/rules/rule-08-killios-prod-solo-lectura.md`.

---

## 1. Los tres comandos del CLI `wmsa`

### 1.1 `wmsa learn-config`

```bash
wmsa learn-config \
  --client <slug>                # ej: killios, becofarma, byb, cealsa, idealsa, cumbre, mampa
  --conn "<connection-string>"   # SQL Server, READ-ONLY
  --window-days 90               # ventana para análisis de patrones operativos
  --output brain/clients/<slug>/ # destino de la ficha aprendida
  [--baseline brain/clients/_baseline/]   # opcional, para diff
  [--dry-run]                    # imprime, no escribe
```

**Qué extrae** (cada extracción es un `SELECT`):

1. **Flags maestros** — los 88 bits catalogados:
   - `bodega` (~57 flags).
   - `producto` (17 flags).
   - `cliente` (9 flags).
   - `ajuste_tipo` (5 flags, incluye typo `momdifica_vencimiento`).
2. **Config global** — `i_nav_config_enc` completo (69 campos), incluidos
   los flags MI3 (`explosion_automatica_nivel_max`, `usar_zona_picking`,
   `paquete_completo_*`, etc.).
3. **Catálogo activo**:
   - Conteos: productos, bodegas, ubicaciones, áreas, tramos.
   - `propietario_bodega` activos.
4. **Distribución de stock** (sobre `stock`):
   - Histograma por estado (`IdEstado`).
   - Con/sin lote, con/sin vencimiento.
   - Tipos de ubicación (picking vs almacenamiento).
5. **Patrones operativos** (últimos N días):
   - Top 10 errores en `log_error_wms` (clasificados por handler/módulo).
   - Tasa de uso por tipo de pedido (`trans_pe_enc.tipo`).
   - Distribución de invocaciones a `Insertar_Stock_Res_MI3` desde
     `trans_pe_det_log_reserva`.
6. **Particularidades del schema**:
   - Triggers presentes que no están en baseline.
   - Vistas/SPs custom (delta vs catálogo `wms-db-brain`).
   - Columnas extra en tablas conocidas.

**Output estructurado** en `brain/clients/<slug>/`:

```
README.md               <- ficha humana (1-2 KB)
learn-snapshot.json     <- crudo (versionable, idempotente)
flags-activos.md        <- qué flags están bit=1 vs catálogo
deltas-vs-baseline.md   <- diferencias schema vs baseline
perfil-operativo.md     <- patrones de los últimos N días
escenarios-cubiertos.md <- escenarios del catálogo aplicables a este cliente
```

**Idempotencia**: misma BD + mismo timestamp ⇒ mismo output (orden estable
de keys, redondeo determinístico).

**Sensibilidad**: NUNCA loguear connection string ni password.
Cumple `rule-07-nunca-loguear-secrets.md`.

### 1.2 `wmsa make-payload`

```bash
wmsa make-payload \
  --scenario <ID>                # ej: RES-001, PIC-001, DES-001
  --client <slug>                # debe existir brain/clients/<slug>/
  --output /tmp/payloads/<slug>-<ID>-<seq>.json
  [--seed 42]                    # determinismo en resolución de placeholders
```

**Qué hace**:

1. Lee `brain/test-scenarios/<modulo>/<ID>.yaml` (ver §3 — formato YAML).
2. Lee `brain/clients/<slug>/learn-snapshot.json` (ficha del cliente).
3. Verifica `required_config` del escenario — si el cliente no cumple,
   aborta con código `EREQ` ("este escenario no aplica para este cliente").
4. Resuelve placeholders `{...}` con datos reales del cliente:
   - `{producto con control_lote=1, stock>10}` → SELECT real al snapshot.
   - `{ubicacion picking activa}` → idem.
   - `{propietario con propietario_bodega activo}` → idem.
5. Calcula hash de la config (`config_hash` en payload).
6. Genera assertions concretas a partir de las plantillas del escenario.

**Output** (JSON):

```json
{
  "scenario": "RES-001",
  "client": "killios",
  "config_hash": "sha256:abc123...",
  "generated_at": "2026-04-28T10:00:00Z",
  "preconditions": [
    {"action": "clear_stock", "where": {"propietario_id": 42}},
    {"action": "insert_stock", "values": {...}}
  ],
  "action": {
    "endpoint": "/api/test-bridge/sandbox/reserve",
    "method": "POST",
    "body": {...}
  },
  "assertions": [
    {"check": "row_count", "table": "stock_res", "where": {"estado": 1}, "expected": 1},
    {"check": "no_error", "table": "log_error_wms", "since": "@action_ts"}
  ]
}
```

### 1.3 `wmsa run-test`

```bash
wmsa run-test \
  --payload /tmp/payloads/<archivo>.json
  --mode {dry-run|replay|canary}   # default: dry-run
  --bridge-url <url>               # ej: https://bof-qa.empresa/api/test-bridge
  --token-env BRIDGE_TOKEN         # nombre del env var con el token
  [--report-dir brain/test-runs/<fecha>/]
```

**Modos**:

| Modo | Toca BD | Aislamiento | Cuándo usar |
|---|---|---|---|
| `dry-run` | No | Brain simula con su modelo | Validar coherencia del payload |
| `replay` | Sí, BD QA | `BEGIN TRAN`/`ROLLBACK` | Smoke tests, CI |
| `canary` | Sí, BD prod, propietario sentinel | `BEGIN TRAN`/`ROLLBACK` | Validación pre-rollout |

**Output**:

- `brain/test-runs/<fecha>/<run-id>.json` — resultado estructurado.
- `brain/test-runs/<fecha>/<run-id>.md` — reporte humano.
- Asiento en `brain_bridge_log` del WMS (vía `POST /api/test-bridge/audit/event`).

**Códigos de salida**:
- `0` — todas las assertions pasaron.
- `1` — al menos una assertion falló.
- `2` — payload inválido o config drift detectado.
- `3` — error de conexión al bridge.
- `4` — el cliente no cumple `required_config` del escenario.

---

## 2. Contrato del controller `/api/test-bridge/...` (BOF)

> Este controller vive **dentro del webservice del BOF** (proyecto VB.NET).
> Comparte runtime con el WS pero tiene namespace, auth y log propios.

### 2.1 Endpoints read-only (siempre seguros)

| Verbo | Path | Descripción |
|---|---|---|
| `GET` | `/api/test-bridge/health` | Versión BOF, BD activa, modo (prod/qa/sandbox), timestamp |
| `GET` | `/api/test-bridge/config/snapshot` | `i_nav_config_enc` + flags maestros (insumo de `learn-config`) |
| `GET` | `/api/test-bridge/stock/profile?propietarioId={id}` | Histograma stock por propietario |
| `GET` | `/api/test-bridge/log-errors/recent?since={iso}&module={...}` | Tail clasificado de `log_error_wms` |
| `GET` | `/api/test-bridge/handlers/inventory` | Lista de handlers MI3 disponibles (cuando exista motor nuevo) |
| `POST` | `/api/test-bridge/inspect/pedido` | Body `{pedido_id}` → estado completo + cruce con `stock_res` |
| `POST` | `/api/test-bridge/explain/reservation` | Body `{pedido_id}` → traza paso a paso de MI3 (lee `trans_pe_det_log_reserva`) |

### 2.2 Endpoints sandbox (transacción con `ROLLBACK` siempre)

| Verbo | Path | Descripción |
|---|---|---|
| `POST` | `/api/test-bridge/sandbox/reserve` | Ejecuta MI3 en `BEGIN TRAN/ROLLBACK`, retorna delta esperado |
| `POST` | `/api/test-bridge/sandbox/dispatch` | Idem para despacho |
| `POST` | `/api/test-bridge/sandbox/adjustment` | Idem para ajuste |

### 2.3 Endpoints de auditoría

| Verbo | Path | Descripción |
|---|---|---|
| `GET` | `/api/test-bridge/audit/runs?since={iso}` | Historial de invocaciones del Brain |
| `POST` | `/api/test-bridge/audit/event` | Brain reporta resultado de un test |

### 2.4 Auth y headers

Todos los endpoints requieren:
- `Authorization: Bearer ${BRIDGE_TOKEN}` (token rotativo, distinto del token operacional).
- `X-Bridge-Client: <slug>` (identifica qué cliente atiende esta llamada).
- `X-Agent-Name: brain` (auditoría).

Endpoints `/sandbox/*` además requieren:
- `X-Bridge-Sandbox: true` (acuse explícito de modo sandbox).
- Feature flag `enable_sandbox=true` en la BD (default `false`).

### 2.5 Tabla `brain_bridge_log` (BD WMS)

```sql
CREATE TABLE brain_bridge_log (
  id              BIGINT IDENTITY PRIMARY KEY,
  ts              DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
  endpoint        NVARCHAR(200) NOT NULL,
  method          NVARCHAR(10) NOT NULL,
  bridge_client   NVARCHAR(50) NOT NULL,
  agent_name      NVARCHAR(50) NULL,
  payload_hash    CHAR(64) NULL,           -- sha256 del body
  status_code     INT NOT NULL,
  duration_ms     INT NOT NULL,
  result_summary  NVARCHAR(500) NULL,
  ip_origin       NVARCHAR(45) NULL
);
CREATE INDEX IX_brain_bridge_log_ts ON brain_bridge_log(ts);
CREATE INDEX IX_brain_bridge_log_endpoint ON brain_bridge_log(endpoint, ts);
```

> Tabla **append-only**. Nunca se borra. Limpieza por partición/archive job
> (no por DELETE).

---

## 3. Formato YAML de escenarios

Convención: `brain/test-scenarios/<modulo>/<ID>-<slug>.yaml`.

### 3.1 Esquema canónico

```yaml
id: RES-001                        # único en todo el catálogo
title: Reserva FEFO simple, una bodega
module: reservation                # reservation | picking | despacho | ajustes | recepcion | ...
priority: P0                       # P0=crítico, P1=alto, P2=medio, P3=bajo
legacy_ref:                        # opcional, si mapea a un Ejecuta_QA_CASO_*
  class: clsLnStock_res
  function: Ejecuta_QA_CASO_1_IDEAL_20231002011101

description: >
  Hay cajas en almacenamiento que vencen primero vs cajas en zona de picking.
  El motor MI3 debe reservar desde almacenamiento respetando FEFO.

required_config:                   # filtro: si el cliente no cumple, no aplica
  i_nav_config_enc.usar_fefo: 1
  i_nav_config_enc.usar_zona_picking: 1
  producto.control_vencimiento: 1

setup:                             # qué sembrar antes de la acción
  - clear_stock_for_propietario: "{propietario sentinel}"
  - insert_stock:
      bodega: "{bodega activa}"
      producto: "47022"            # producto sentinel del CASO 1
      ubicacion: "{ubicacion no-picking activa}"
      lote: "CASO_USO1_MI3_AUTOMATE"
      vencimiento: "2026-01-01"
      cantidad: "{1 * factor presentacion default}"
  - insert_stock:
      bodega: "{bodega activa}"
      producto: "47022"
      ubicacion: "{ubicacion picking activa}"
      lote: "CASO_USO1_MI3_AUTOMATE"
      vencimiento: "2026-01-02"    # vence después
      cantidad: "{1 * factor presentacion default}"

action:
  endpoint: /api/test-bridge/sandbox/reserve
  method: POST
  body:
    pedido:
      tipo: traslado_cliente
      bodega_origen: "{bodega activa}.codigo"
      cliente_destino: "{cliente sentinel}.codigo"
      lineas:
        - producto: "47022"
          cantidad: 1

expected:
  - check: row_count
    table: stock_res
    where:
      estado: 1
      idstock: "@stock_almacenaje.id"     # referencia al stock sembrado en setup
    value: 1
    note: "Debe reservar la caja del almacenaje (vence antes), no la de picking"

  - check: row_count
    table: stock_res
    where:
      estado: 1
      idstock: "@stock_picking.id"
    value: 0
    note: "La caja de picking no debe estar reservada"

  - check: no_error
    table: log_error_wms
    since: "@action_ts"

cleanup:                           # opcional, default: rollback elimina todo
  on_replay: rollback              # rollback | keep | custom
  on_canary: rollback              # idem

tags:
  - fefo
  - mi3
  - reservation
  - golden-path

owner: brain                       # quién mantiene este escenario
```

### 3.2 Convenciones de placeholders

| Placeholder | Resuelto por |
|---|---|
| `{bodega activa}` | `learn-snapshot.json` → `bodegas[0]` con `Activo=1` |
| `{ubicacion picking activa}` | `bodega_ubicacion` con `ubicacion_picking=1` y `Activo=1` |
| `{ubicacion no-picking activa}` | `bodega_ubicacion` con `ubicacion_picking=0` y `Activo=1` |
| `{propietario sentinel}` | `clients/<slug>/sentinels.md` define cuál |
| `{cliente sentinel}` | idem |
| `{producto con control_lote=1, stock>10}` | query estructurada al snapshot |
| `@stock_almacenaje.id` | id del stock sembrado en `setup` (para cross-ref en assertions) |
| `@action_ts` | timestamp de inicio de la acción |

### 3.3 Tipos de assertions (`expected[].check`)

| Check | Descripción |
|---|---|
| `row_count` | Cuenta filas en una tabla con un `where` |
| `row_exists` | Existe al menos una fila |
| `row_absent` | No existe ninguna fila |
| `column_value` | Una columna tiene cierto valor |
| `no_error` | No hay nuevas filas en `log_error_wms` desde `@action_ts` |
| `state_transition` | Una entidad transicionó de estado X a Y |
| `numeric_range` | Una agregación cae en un rango |
| `custom_sql` | Query libre que retorna una sola fila/columna |

---

## 4. Reglas duras de la skill

1. **Killios PRD READ-ONLY**: ningún `wmsa learn-config` ni `wmsa run-test --mode replay`
   se ejecuta contra Killios PRD. Sólo `dry-run` o `canary` con propietario
   sentinel y `ROLLBACK` siempre. Cumple `rule-08`.
2. **Nunca commitear secretos**: connection strings, tokens, passwords nunca
   van al repo. Siempre por env vars. Cumple `rule-07`.
3. **Idempotencia**: cada comando debe producir output determinístico para
   el mismo input. `--seed` controla el RNG.
4. **Auditoría**: cada `run-test` deja asiento en `brain/test-runs/` y en
   `brain_bridge_log`. Sin excepciones.
5. **Hash de config en payload**: si la config del cliente cambia entre
   `make-payload` y `run-test`, el `run-test` aborta con `EDRIFT`.
6. **Sin `commit` en sandbox**: el controller jamás expone endpoints que
   commiteen. Si hace falta validar persistencia, se hace en QA con BD copia.
7. **Cobertura declarada por bundle**: cada bundle al BOF en
   `entregables_ajuste/<fecha>/<v##_bundle>/MANIFEST.json` debe declarar
   `coverage: ["RES-001", "RES-003", ...]`.
8. **Legacy es la verdad**: los `Ejecuta_QA_CASO_*` de `clsLnStock_res`
   son **fuente canónica** validada por Erik. Cuando hay duda, ese código
   gana sobre cualquier escenario YAML.

---

## 5. Flujo de trabajo recomendado

### 5.1 Onboarding de un cliente nuevo

```bash
# 1. Aprender la config
wmsa learn-config --client becofarma --conn "Server=...;Database=BECO_PRD;..."

# 2. Validar fichas generadas
ls brain/clients/becofarma/

# 3. Ver qué escenarios aplican
cat brain/clients/becofarma/escenarios-cubiertos.md

# 4. Generar payloads de los aplicables
for sc in $(cat brain/clients/becofarma/escenarios-cubiertos.md | grep '^- ' | awk '{print $2}'); do
  wmsa make-payload --scenario $sc --client becofarma --output /tmp/payloads/becofarma-$sc.json
done

# 5. Dry-run primero
for p in /tmp/payloads/becofarma-*.json; do
  wmsa run-test --payload $p --mode dry-run
done
```

### 5.2 Validación pre-bundle

```bash
# 1. Antes de empaquetar el bundle, listar escenarios afectados
# (declarado en MANIFEST.json del bundle)
COVERAGE=$(jq -r '.coverage[]' entregables_ajuste/2026-04-28/v24_bundle/MANIFEST.json)

# 2. Ejecutar replay contra QA para todos los clientes aprendidos
for client in killios becofarma byb cealsa idealsa; do
  for sc in $COVERAGE; do
    wmsa make-payload --scenario $sc --client $client --output /tmp/p.json
    wmsa run-test --payload /tmp/p.json --mode replay --bridge-url $BOF_QA_URL
  done
done

# 3. Si todos pasan, marcar bundle como ready
```

### 5.3 Investigación de incidente

```bash
# 1. Erik reporta: "MI3 dejó pedido 12345 sin reservar en Killios"
# 2. Brain inspecciona
curl -X POST -H "Authorization: Bearer $BRIDGE_TOKEN" \
  -H "X-Bridge-Client: killios" \
  -H "X-Agent-Name: brain" \
  $BOF_PROD_URL/api/test-bridge/explain/reservation \
  -d '{"pedido_id": 12345}'

# 3. Si la traza muestra anomalía, generar payload reproductor
wmsa make-payload --scenario RES-004 --client killios \
  --override-input pedido_id=12345 \
  --output /tmp/repro.json

# 4. Dry-run para confirmar diagnosis
wmsa run-test --payload /tmp/repro.json --mode dry-run

# 5. Si reproduce, abrir caso en _inbox/ con el payload adjunto
```

---

## 6. Formato de reporte (`brain/test-runs/<fecha>/<run-id>.md`)

```markdown
# Run report: <run-id>

- **Scenario**: RES-001
- **Client**: killios
- **Mode**: replay
- **Bridge URL**: https://bof-qa.empresa
- **Started**: 2026-04-28T10:30:00Z
- **Duration**: 3.4s
- **Result**: PASS | FAIL | DRIFT | ERROR
- **Bundle context**: v24_bundle (si aplica)

## Preconditions
- ✓ clear_stock_for_propietario(42) — 0 rows affected (limpio previo)
- ✓ insert_stock(producto=47022, ubicacion=12, vence=2026-01-01) — id=99001
- ✓ insert_stock(producto=47022, ubicacion=15, vence=2026-01-02) — id=99002

## Action
POST /api/test-bridge/sandbox/reserve
Status: 200, duration 1.2s

## Assertions
| # | Check | Expected | Got | Result |
|---|---|---|---|---|
| 1 | row_count(stock_res, idstock=99001, estado=1) | 1 | 1 | ✓ |
| 2 | row_count(stock_res, idstock=99002, estado=1) | 0 | 0 | ✓ |
| 3 | no_error(log_error_wms since 10:30:00) | true | true | ✓ |

## Diff config (vs último learn-config)
- (sin cambios)

## Logs
[fragmento de trans_pe_det_log_reserva durante la sandbox]
```

---

## 7. Referencias

- ADR: `wms-brain/decisions/004-bridge-wms-test-automation.md`.
- Catálogo de escenarios: `wms-brain/test-scenarios/README.md`.
- Familia legacy: `wms-brain/test-scenarios/reservation/legacy-clsLnStock_res/README.md`.
- Módulo reservation: `wms-brain/entities/modules/reservation/README.md`.
- Catálogo SQL: `wms-db-brain/db-brain/README.md`.
- Filosofía multi-cliente: `wms-db-brain/db-brain/parametrizacion/README.md`.
- Reglas duras del agente: `wms-brain/entities/rules/`.
