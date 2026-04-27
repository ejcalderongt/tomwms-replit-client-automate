# ADR 004 — Bridge WMS ↔ Brain para automatización de pruebas

- **Estado**: Propuesto.
- **Fecha**: 2026-04-27.
- **Autor**: Brain (Replit) por encargo de Erik Calderón.
- **Reemplaza**: nada.
- **Dependencias**: ADR `003-mi3-reescrito.md`, módulo `entities/modules/reservation/`,
  rama `wms-db-brain`.

---

## 1. Contexto

El TOM WMS es un producto **multi-cliente con código único**. La diferencia
entre clientes (Killios, Becofarma, BYB, Cealsa, IDEALSA, La Cumbre, MAMPA,
etc.) está embebida en **flags bit** de los maestros (`bodega`, `producto`,
`cliente`, `ajuste_tipo`) y en `i_nav_config_enc`. Esto produce un espacio
combinatorio enorme de comportamientos posibles que **hoy no se prueba de
forma sistemática**:

- No hay pruebas automatizadas para el BOF (VB.NET).
- No hay pruebas automatizadas para el HH (Android).
- No hay pruebas automatizadas para el webservice.
- Sí existe una clase de pruebas manuales: `clsLnStock_res`
  (`Ejecuta_QA_CASO_*_*`) con ~20 casos canónicos, **pero corren a mano y no
  hay reporte agregado**.

Cada bundle al BOF (vía `entregables_ajuste/`) podría romper algún cliente
sin que nos enteremos hasta que el operador lo reporta. El motor de reservas
MI3 es el caso más doloroso porque combina FEFO + zona picking + paquetes
completos + explosión automática y depende de docenas de flags.

A la vez, el agente Brain ya tiene mucho conocimiento estructurado:
- `wms-db-brain`: 621 objetos del catálogo Killios PRD.
- `wms-brain/entities/modules/reservation/`: 14 docs (~270 KB) del motor MI3.
- `wms-brain/sql-catalog/reservation-tables.md`: DDL crítico.
- 88 flags bit catalogados en `db-brain/parametrizacion/`.

Falta un **canal bidireccional** entre el WMS y el Brain que permita:
1. Que Brain conozca la config real de cada cliente.
2. Que Brain genere casos de prueba que respeten esa config.
3. Que Brain ejecute esos casos contra un sandbox del WMS y compare resultado.
4. Que cada bundle nuevo se valide contra el catálogo de casos antes de
   entregarse.

---

## 2. Decisión

Crear un **WMS Test Bridge**: la combinación de (a) un controller dentro
del webservice del BOF, (b) tres comandos en el CLI `wmsa`, y (c) un
catálogo versionado de escenarios en el Brain.

### 2.1 Componentes

```
┌─────────────────────────┐         ┌──────────────────────────────┐
│  Brain (rama wms-brain) │         │  BOF webservice               │
│                         │         │                                │
│  wmsa learn-config      │ ──────> │  /api/test-bridge/config/...  │
│  wmsa make-payload      │ ──────> │  /api/test-bridge/inspect/... │
│  wmsa run-test          │ ──────> │  /api/test-bridge/sandbox/... │
│                         │ <────── │  /api/test-bridge/audit       │
│  test-scenarios/*.yaml  │         │                                │
└─────────────────────────┘         └──────────────────────────────┘
            │                                     │
            ↓                                     ↓
   brain/clients/<cliente>/             brain_bridge_log (tabla nueva)
   (ficha aprendida)                    (auditoría inmutable)
```

### 2.2 Tres comandos en `wmsa`

#### `wmsa learn-config --client <cliente>`

Conecta a la BD del cliente (lectura), extrae:
- 88 flags bit catalogados, valores reales.
- `i_nav_config_enc` completo (69 campos).
- Distribución de stock (histogramas).
- Patrones operativos de los últimos N días (`log_error_wms`,
  `trans_pe_det_log_reserva`).
- Triggers/SPs/vistas custom no presentes en baseline.

Output: `brain/clients/<cliente>/`:
- `learn-snapshot.json` — datos crudos.
- `README.md` — ficha humana.
- `flags-activos.md` — flags vs catálogo.
- `deltas-vs-baseline.md` — qué difiere del schema canónico.
- `perfil-operativo.md` — patrones de uso.
- `escenarios-cubiertos.md` — qué casos del catálogo aplican.

Idempotente. Cada corrida produce commit limpio. Diff entre corridas dispara
warnings si una premisa de un escenario cambió.

#### `wmsa make-payload --scenario <ID> --client <cliente>`

Lee `brain/test-scenarios/<modulo>/<ID>.yaml` (declarativo) y la ficha del
cliente, resuelve placeholders (`{producto con control_lote=1, stock>10}`)
con datos reales, produce un payload JSON listo para ejecutar:

```json
{
  "scenario": "RES-001",
  "client": "killios",
  "config_hash": "sha256:...",
  "preconditions": [ ... ],
  "action": { "endpoint": "/api/test-bridge/sandbox/reserve", "body": {...} },
  "assertions": [ ... ]
}
```

#### `wmsa run-test --payload <archivo> --mode {dry-run|replay|canary}`

- `dry-run`: simula con la lógica del Brain, no toca el WMS.
- `replay`: ejecuta contra BD QA, transacción que termina en `ROLLBACK`.
- `canary`: ejecuta contra BD prod con propietario sentinel, también con `ROLLBACK`.

Output: reporte en `brain/test-runs/<fecha>/<run-id>.{json,md}` + asiento
en tabla `brain_bridge_log` del lado WMS.

### 2.3 WMS Test Bridge Controller

Controller nuevo en el webservice del BOF, namespace `/api/test-bridge/...`.
Auth y allowlist independientes de los tokens operacionales. Ver
`skills/wms-test-bridge/SKILL.md` para el contrato detallado.

### 2.4 Catálogo de escenarios

Versionado en `brain/test-scenarios/`. Estructura por módulo:

```
test-scenarios/
├── README.md                    índice + convenciones
├── reservation/
│   ├── RES-001 ... RES-005     5 escenarios iniciales (YAML declarativo)
│   └── legacy-clsLnStock_res/  inventario de los 20 CASOs legacy + docs
├── picking/
│   └── PIC-001 ...
├── despacho/
│   └── DES-001 ...
└── ajustes/
    └── INV-001 ...
```

Naming: `<MOD>-NNN-slug-corto.yaml` donde `<MOD>` es el prefijo del módulo
y `NNN` es número incremental dentro del módulo.

---

## 3. Opciones evaluadas

### Opción A — Hacer nada, seguir manual

Mantener `clsLnStock_res.Ejecuta_QA_CASO_*` como hoy: alguien los corre a
mano, sin reporte agregado, sin generación automática.
**Descartada**: no escala, los 20 casos solo cubren reservas, y no se
ejecutan en CI.

### Opción B — Tests xUnit en VB.NET dentro del repo BOF

Migrar los `Ejecuta_QA_CASO_*` a un proyecto de test xUnit y correrlos en CI.
**Descartada parcialmente**: sigue sin resolver la **parametrización por
cliente** (un test xUnit asume una sola config; el problema es que cada
cliente tiene config distinta). Útil como complemento, no como reemplazo.

### Opción C — Bridge WMS↔Brain (esta decisión)

Brain orquesta + WMS expone endpoints de test. Permite multi-cliente,
reuso del catálogo, generación automática y reporte centralizado.
**Elegida** porque resuelve el problema raíz (diversidad de configs) sin
duplicar el motor de reservas.

### Opción D — Microservicio de simulación independiente

Construir un simulador del WMS en .NET 8 que reproduzca la lógica.
**Descartada**: divergencia inevitable con el motor real, alto costo de
mantenimiento. La Pieza `dry-run` de Opción C cumple un rol similar pero
acotado y se calibra periódicamente contra `replay`.

---

## 4. Consecuencias

### 4.1 Positivas

- **Cobertura por cliente**: cada bundle se puede validar contra los N
  clientes aprendidos antes de entregarse.
- **Reuso del conocimiento existente**: aprovecha `clsLnStock_res.Ejecuta_QA_CASO_*`
  como fuente canónica de escenarios validados por Erik.
- **Auditoría completa**: cada corrida queda en `brain_bridge_log` (BD WMS)
  + `brain/test-runs/` (Brain).
- **Detección temprana de drift**: cambios de flags en clientes disparan
  warnings antes de que el operador los reporte.
- **Onboarding de nuevos programadores**: un dev puede correr
  `wmsa learn-config + wmsa run-test` para entender qué hace MI3 sin tocar
  prod.

### 4.2 Negativas / Costos

- **Trabajo en el BOF**: hay que escribir el controller (~5-10 archivos
  VB.NET), añadir la tabla `brain_bridge_log`, y exponerlo en el WS.
- **Disciplina del catálogo**: cada bundle nuevo debe declarar qué
  escenarios cubre/rompe (en `MANIFEST.json` del entregable).
- **Mantenimiento de la ficha de cliente**: cada cambio de config en un
  cliente requiere re-correr `learn-config` (idealmente automático nightly).
- **Dependencia del controller**: si el controller cae, el `replay/canary`
  no funciona (el `dry-run` sigue funcionando).
- **Riesgo MI3 + transacciones anidadas**: el motor legacy abre
  transacciones internas; el `ROLLBACK` externo del controller puede tener
  efectos sutiles. Validar con CASO 1 antes de generalizar.

### 4.3 Reglas que se introducen

- **rule-13** (nueva, pendiente): *"Todo bundle al BOF debe declarar en
  `MANIFEST.json` los escenarios del catálogo que toca, y el agente debe
  correr `wmsa run-test` antes de marcar el bundle como `ready`."*
- **rule-14** (nueva, pendiente): *"El controller `/api/test-bridge/...`
  nunca expone endpoints que escriban commiteado. Todo es read-only o
  sandbox con `ROLLBACK` final."*

---

## 5. Plan de rollout (8 sprints)

| Sprint | Entrega | Lado | Bloqueado por |
|---|---|---|---|
| **S1** | Este ADR + `skills/wms-test-bridge/SKILL.md` + 8 escenarios YAML iniciales + inventario `legacy-clsLnStock_res/README.md` | Brain | nada |
| **S2** | `wmsa learn-config` v0 (lee BD directo, no usa controller) | Brain | acceso BD QA Killios |
| **S3** | Documentación de los 20 CASOs legacy (`legacy-clsLnStock_res/CASO-NN-*.md`) a partir del código fuente del BOF | Brain | acceso código `clsLnStock_res.vb` |
| **S4** | Controller v0 en BOF: solo endpoints read-only + auth + log + tabla `brain_bridge_log` | BOF | bundle al BOF |
| **S5** | `wmsa run-test --mode dry-run` consumiendo controller (read-only) | Brain | S4 |
| **S6** | Controller v1: añade endpoints de sandbox (`POST /sandbox/*`) con `BEGIN TRAN/ROLLBACK` + feature flag | BOF | bundle al BOF |
| **S7** | `wmsa run-test --mode replay` contra QA real | Brain + ambiente | S6 + entorno QA |
| **S8** | Reporting + integración con `_proposals/` del bridge + cobertura por bundle | Brain | todo lo anterior |

> Cada sprint es **independiente y entregable**. No hace falta esperar al S8
> para tener valor: ya con S1+S2+S3 Brain puede generar payloads y razonar
> sobre escenarios sin tocar el BOF.

---

## 6. Métricas de éxito

- **Cobertura**: % de escenarios del catálogo cubiertos por al menos un
  cliente aprendido.
- **Latencia de detección de drift**: días desde que cambia un flag de
  cliente hasta que Brain emite el warning.
- **Precisión del dry-run**: % de coincidencia entre `dry-run` y `replay`
  para los mismos payloads (objetivo >90% al S8).
- **Hits de cobertura por bundle**: cada bundle al BOF debe ejecutar al
  menos 8 escenarios (los iniciales) antes de marcarse `ready`.

---

## 7. Riesgos abiertos y mitigaciones

| # | Riesgo | Mitigación |
|---|---|---|
| R1 | `BEGIN TRAN/ROLLBACK` rompe transacciones anidadas de MI3 | Validar con CASO 1 en S7. Si rompe, ejecutar contra **BD copia idéntica nightly** en lugar de transacción. |
| R2 | Drift entre `learn_config()` y ejecución | Hash de config en payload + validación al ejecutar. |
| R3 | Datos sintéticos vs reales | Marcar entidades **sentinel** por cliente (productos, ubicaciones, propietarios fijos) y mantenerlas estables. |
| R4 | Costo de mantenimiento del catálogo | Declarar cobertura en `MANIFEST.json` de cada bundle. Reportes mensuales de cobertura. |
| R5 | Brain ≠ WMS (lógica divergente en `dry-run`) | Calibración periódica `dry-run` vs `replay`. Si divergencia >10%, regenerar el modelo del dry-run desde el código. |
| R6 | Multi-tenant en el controller | Tokens y allowlists por cliente. Header `x-bridge-client` obligatorio. |
| R7 | El controller se vuelve un agujero de seguridad | Auth rotativa, allowlist, rate-limit, feature flag para sandbox, log de cada llamada, audit ext. |
| R8 | Los 20 CASOs legacy quedan desincronizados con el catálogo nuevo | El inventario `legacy-clsLnStock_res/README.md` mantiene mapeo bidireccional CASO↔escenario nuevo. |

---

## 8. Referencias

- `wms-brain/decisions/003-mi3-reescrito.md` (motor MI3 nuevo).
- `wms-brain/entities/modules/reservation/12-mi3-todos-roadmap.md` (TODOs del motor).
- `wms-brain/skills/wms-test-bridge/SKILL.md` (skill canónica del bridge).
- `wms-brain/test-scenarios/README.md` (índice del catálogo de escenarios).
- `wms-brain/test-scenarios/reservation/legacy-clsLnStock_res/README.md`
  (inventario de los 20 CASOs canónicos).
- `wms-db-brain/db-brain/parametrizacion/README.md` (filosofía multi-cliente).
- `wms-brain/entities/rules/rule-08-killios-prod-solo-lectura.md`
  (regla maestra: prod es READ-ONLY).

---

## 9. Cambios desde versiones previas

- **v0.1 (2026-04-27)**: versión inicial. Propuesta para validación de Erik.
