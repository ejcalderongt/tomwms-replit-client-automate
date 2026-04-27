# SPEC.md — WmsBrainClient (PowerShell module)

> **Cliente operativo del ecosistema TOMWMS Brain**. Wrappea los scripts
> `.mjs` existentes (`brain_bridge`, `apply_bundle`, `hello_sync`) y la
> conexión SQL Server al brain de las BDs productivas, expuesto como
> cmdlets PowerShell verbo-sustantivo.

---

## 1. Identidad

| Campo            | Valor                                                                |
|------------------|----------------------------------------------------------------------|
| Nombre del módulo| `WmsBrainClient`                                                     |
| Prefijo cmdlets  | `Wms` o `WmsBrain` (ver ALIASES.md)                                  |
| Versión          | `0.2.0` (esta pasada — alineada al bridge)                           |
| Autor            | Erik Calderón (PrograX24, init `EJC`)                                |
| Plataforma       | PowerShell 5.1+ (Windows) y PowerShell 7+ (cross-platform).          |
| Idioma de logs   | Español rioplatense, sin emojis.                                     |
| Rama del repo    | `wms-brain-client` (orphan, igual que `wms-brain` y `wms-db-brain`). |

---

## 2. Posicionamiento — qué soy y qué NO soy

### Soy

- Una **capa ergonómica PowerShell** sobre el ecosistema brain existente.
- Un wrapper sobre `Invoke-Sqlcmd` con perfiles de conexión + fail-fast
  si falta password.
- Un emisor de `brain_event.json` válidos según `SCHEMA_VERSION` vigente
  del bridge.
- Un runner de **suites** (state-machine-pedido, outbox-health,
  bug-p16b-detector) y **scenarios** (doble-despacho, etc.) que viven en
  la rama `wms-brain` y son SQL read-only por contrato.

### NO soy

- **No reemplazo el bridge.** Todo evento sale por `brain_bridge.mjs notify`.
- **No edito el brain.** Eso es manual del lado productor (Replit) tras
  `analyze`.
- **No modifico el WMS productivo.** Solo lectura contra K7-PRD / BB-PRD.
- **No mezclo HH con VB.** Eso es contrato del proyecto WMS, ver
  `wms-brain/brain/agent-context/AGENTS.md` §2.
- **No uso ORM, migraciones automáticas, ni cualquier herramienta que
  intente generar `ALTER TABLE` o `db:push`.** Las 3 BDs (K7-PRD, BB-PRD,
  C9-QAS) son productivas; cualquier write destruye datos del cliente.
  SQL plano vía `Invoke-Sqlcmd`, siempre `SELECT`.

---

## 3. Dependencias hard del ecosistema existente

El cliente requiere que el repo de exchange
(`ejcalderongt/tomwms-replit-client-automate`) esté clonado en
`$env:WMS_BRAIN_EXCHANGE_REPO` con **dos worktrees** (o dos clones):

| Rama clonada       | Path por defecto              | Para qué                                       |
|--------------------|-------------------------------|------------------------------------------------|
| `main`             | `C:\tomwms-exchange-main`     | provee `scripts/brain_bridge.mjs`, `apply_bundle.mjs`, `hello_sync.mjs`, `brain-up.ps1`. |
| `wms-brain`        | `C:\tomwms-exchange-brain`    | recibe `brain/_inbox/<id>.json` cuando el cliente notifica. |
| `wms-brain-client` | `C:\tomwms-brain-client`      | provee este módulo PowerShell + spec.          |
| `wms-db-brain`     | `C:\tomwms-exchange-db-brain` | catálogo SQL Killios (referencia, opcional).   |

### Versiones mínimas de los `.mjs`

- `brain_bridge.mjs`: `SCHEMA_VERSION="1"` o superior.
- `apply_bundle.mjs`: con soporte de `--brain-message` (commit donde se
  agregó esa flag o posterior).
- `hello_sync.mjs`: cualquier versión.

El cliente lee `brain_bridge.mjs` y, si detecta `SCHEMA_VERSION` distinto
del esperado, **avisa y refuse** emitir tipos §4 hasta que coincida.

---

## 4. Inventario de cmdlets

> Lista completa con firma + comportamiento en `CMDLETS.md`. Acá solo
> agrupación por área.

### 4.1 Bootstrap y handshake

- `Invoke-WmsBrainHello` → wrap de `node scripts/hello_sync.mjs`.
- `Invoke-WmsBrainBootstrap` → wrap de `.\scripts\brain-up.ps1`.
- `Test-WmsBrainEnvironment` → valida vars de entorno + paths de los 4 clones.

### 4.2 Aplicación de bundles

- `Invoke-WmsBrainApplyBundle` → wrap de `node scripts/apply_bundle.mjs`.
- `Get-WmsBrainBundleHistory` → parsea `entregables_ajuste/*/v*_bundle/apply_log.json`.

### 4.3 Eventos (brain bridge)

- `New-WmsBrainEvent` → construye un PSCustomObject + escribe `.json` válido.
- `Invoke-WmsBrainNotify` → wrap de `brain_bridge.mjs notify --from-event-file`.
- `Get-WmsBrainEventQueue` → wrap de `brain_bridge.mjs list`.
- `Get-WmsBrainEvent` → wrap de `brain_bridge.mjs show --id`.

### 4.4 Eventos extendidos (§4 del PROTOCOL — propuesta)

- `New-WmsBrainQuestionEvent` → emite `directive`+tags hoy, `question_request` cuando se acepte v2.
- `New-WmsBrainAnswerEvent` → idem para `question_answer`.
- `New-WmsBrainLearningEvent` → idem para `learning_proposed`.

### 4.5 Conexión y queries SQL (read-only)

- `Get-WmsBrainConnectionString` → resuelve perfil (K7-PRD/BB-PRD/C9-QAS/LOCAL_DEV).
- `Invoke-WmsBrainQuery` → wrap de `Invoke-Sqlcmd` con safety SELECT.
- `Test-WmsBrainConnection` → ping al server + permisos read.

### 4.6 Suites y scenarios

- `Invoke-WmsBrainSuite` → corre una suite por nombre (ej. `state-machine-pedido`).
- `Invoke-WmsBrainScenario` → corre un scenario por nombre (ej. `doble-despacho`).
- `Get-WmsBrainSuiteList` → lista suites/scenarios disponibles desde rama `wms-brain`.

### 4.7 Question cards

- `Get-WmsBrainQuestion` → lee Q-NNN desde rama `wms-brain` (vía git show o filesystem).
- `Submit-WmsBrainQuestion` → emite `brain_event.json` (workaround `directive` por ahora).

---

## 5. Variables de entorno

Hereda las definidas en `wms-brain/brain/agent-context/AGENTS.md` §
"Variables de entorno requeridas". Adicionalmente:

| Var                              | Descripción                                                      |
|----------------------------------|------------------------------------------------------------------|
| `WMS_BRAIN_EXCHANGE_REPO_MAIN`   | path al clon en rama `main` (provee los `.mjs`).                |
| `WMS_BRAIN_EXCHANGE_REPO_BRAIN`  | path al clon en rama `wms-brain` (recibe inbox).                |
| `WMS_BRAIN_CLIENT_REPO`          | path al clon en rama `wms-brain-client` (provee este módulo).   |
| `WMS_BRAIN_DEFAULT_PROFILE`      | uno de `K7-PRD`/`BB-PRD`/`C9-QAS`/`LOCAL_DEV`. Default: `K7-PRD`. |
| `WMS_BRAIN_AUTHOR_INIT`          | iniciales para el `id` (default: `EJC`).                         |

`Test-WmsBrainEnvironment` verifica las 5 + las heredadas, y falla con
mensaje específico para cada una que falte.

---

## 6. Perfiles de conexión

| Perfil      | Server              | Database              | Usuario   | Password var                  |
|-------------|---------------------|------------------------|-----------|-------------------------------|
| `K7-PRD`    | `52.41.114.122,1437`| `TOMWMS_KILLIOS_PRD`  | `wmsuser` | `WMS_KILLIOS_DB_PASSWORD`     |
| `BB-PRD`    | `52.41.114.122,1437`| `IMS4MB_BYB_PRD`      | `wmsuser` | `WMS_KILLIOS_DB_PASSWORD`     |
| `C9-QAS`    | `52.41.114.122,1437`| `IMS4MB_CEALSA_QAS`   | `wmsuser` | `WMS_KILLIOS_DB_PASSWORD`     |
| `LOCAL_DEV` | `localhost`         | `TOMWMS_DEV`          | (Trusted) | n/a                           |

> Las 3 PRD/QAS comparten host y credencial. Los 3 son **READ-ONLY estricto**
> desde este cliente. Cualquier intento de DML genera excepción local antes
> de mandar al server.

---

## 7. Suites incluidas (viven en `wms-brain`)

| Suite                       | Steps   | Para qué                                                   |
|-----------------------------|---------|------------------------------------------------------------|
| `outbox-health`             | 5       | salud del outbox HH→VB→SQL.                                |
| `state-machine-pedido`      | 7 (S1–S7) | trazabilidad del estado del pedido extremo a extremo.     |
| `bug-p16b-detector`         | 5 (B1–B5) | detector del bug P16B (doble apply de despacho).          |

**Scenarios**:

| Scenario           | Para qué                                                            |
|--------------------|---------------------------------------------------------------------|
| `doble-despacho`   | reproduce el caso clásico de doble despacho del mismo lote.         |

Detalles en `wms-brain/suites/<nombre>/README.md` y
`wms-brain/scenarios/<nombre>/README.md`.

---

## 8. Question cards incluidas (Q-001 a Q-008)

> Las cards viven en `questions/Q-NNN-<slug>.md` de este mismo repo (rama
> `wms-brain-client`). Cada una trae front-matter YAML con SQL sugerido
> que el cliente ejecuta vía `Invoke-WmsBrainQuestion`.
>
> **Formato actual**: `protocolVersion: 1` (legacy, propio del cliente).
> Se mantiene mientras `SCHEMA_VERSION="2"` del bridge no esté aprobado.
> Ver `MIGRATION-NOTE.md` en el directorio `questions/`.

| ID    | Slug                              | Codename | Prioridad | Tema                                                                |
|-------|-----------------------------------|----------|-----------|---------------------------------------------------------------------|
| Q-001 | `cadencia-navsync`                | BB       | medium    | Cadencia real del job que procesa SALIDAS en BB outbox.             |
| Q-002 | `decimales-sap-killios`           | K7       | high      | Decimales en cantidades enviadas a SAP B1 (K7).                     |
| Q-003 | `ingresos-byb-pendientes`         | BB       | critical  | Por qué BB tiene 110k INGRESOS pendientes en outbox (PEND-12).      |
| Q-004 | `log-error-wms`                   | K7,BB,C9 | medium    | Estructura y uso de `log_error_wms` (PEND-11).                      |
| Q-005 | `sapsync-dedicado-por-cliente`    | K7       | medium    | Granularidad de `IdRecepcionDet`/`IdDespachoDet` en outbox.         |
| Q-006 | `mi3-cadencia-trigger`            | ID,MH,MC | medium    | Cadencia y trigger de MI3 (clientes Aurora).                        |
| Q-007 | `granularidad-recepcion-despacho` | K7,BB    | medium    | Detalle vs encabezado en outbox (1:N por documento).                |
| Q-008 | `devoluciones-frente-nuevo`       | K7,BB    | medium    | Manejo de devoluciones en outbox (frente nuevo `IdPedidoEncDevol`). |

> **Bandera roja conocida** (Q-003): BB-PRD reporta `110,795 INGRESOS pendientes`
> en `i_nav_transacciones_out` con `enviado=0` y `tipo_transaccion='INGRESO'`.
> Solo 107 INGRESOS lograron salir alguna vez. Caso bloqueante para
> reconciliación con NAV.

---

## 9. Política de errores y exit codes

| Código | Significado                                                         |
|--------|---------------------------------------------------------------------|
| 0      | OK.                                                                 |
| 2      | Argumentos inválidos / faltantes.                                   |
| 3      | Variable de entorno requerida no seteada (mensaje específico cuál). |
| 4      | Working tree sucio en clon de exchange (notify aborta).             |
| 5      | Schema mismatch entre cliente y `brain_bridge.mjs`.                 |
| 6      | Conexión SQL fallida (timeout / login).                             |
| 7      | Intento de DML detectado (cliente refusó mandar al server).         |
| 8      | Heredado de `brain_bridge`/`apply_bundle` (propaga su exit).        |

Todos los errores van a stderr en el formato:
`[WmsBrainClient] [<cmdlet>] [<exit_code>] <mensaje>`.

---

## 10. Roadmap

- **0.2.0** (esta pasada): cliente alineado al bridge real, propuesta v2.
- **0.3.0**: aceptación de `SCHEMA_VERSION="2"` + tipos extendidos.
- **0.4.0**: integración con WikiHub (notificación humana de eventos applied).
- **0.5.0**: GUI `Show-WmsBrainQueue` (terminal-based, `Out-GridView`).

---

## 11. Cambios respecto a la pasada anterior (0.1.0)

- **Bridge real reconocido**: ya no se asume "no hay bridge"; se delega.
- **Schema unificado**: se adopta `id`/`schema_version`/`type`/`source`/`ref`/`context`/`status`/`history` del bridge.
- **Tipos extendidos como propuesta formal v2**, no como invento paralelo.
- **Workaround `directive`+`tags`** para Q-NNN mientras v2 no se acepte.
- **Cmdlets renombradas** para reflejar que son wrappers (prefijo `Invoke-` para los que delegan a `.mjs`).
- **Suites/scenarios viven en `wms-brain`**, no acá. Este módulo solo los corre.
