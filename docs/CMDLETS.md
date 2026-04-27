# CMDLETS.md — WmsBrainClient PowerShell

> **Convención**: PowerShell verbos aprobados (`Get-Verb`).
> Módulo: `WmsBrainClient`. Alias del módulo: `wmsbc`.
> **Filosofía**: cada cmdlet o **delega** a un `.mjs` del ecosistema brain
> existente, o **wrappea** `Invoke-Sqlcmd` con safety. No reinventa el bridge.

---

## 0. Mapa cmdlet → script real delegado

| Cmdlet                            | Delega a                                        | Modo |
|-----------------------------------|-------------------------------------------------|------|
| `Invoke-WmsBrainHello`            | `node scripts/hello_sync.mjs`                   | wrap |
| `Invoke-WmsBrainBootstrap`        | `.\scripts\brain-up.ps1`                        | wrap |
| `Invoke-WmsBrainApplyBundle`      | `node scripts/apply_bundle.mjs`                 | wrap |
| `Invoke-WmsBrainNotify`           | `node scripts/brain_bridge.mjs notify`          | wrap |
| `Get-WmsBrainEventQueue`          | `node scripts/brain_bridge.mjs list`            | wrap |
| `Get-WmsBrainEvent`               | `node scripts/brain_bridge.mjs show`            | wrap |
| `New-WmsBrainEvent`               | (puro PS, escribe `brain_event.json` válido)    | nativo |
| `New-WmsBrainQuestionEvent`       | (puro PS, escribe evento `directive` con tags Q-NNN — ver §6) | nativo |
| `Get-WmsBrainConnectionString`    | (puro PS, perfil → connection string)           | nativo |
| `Invoke-WmsBrainQuery`            | `Invoke-Sqlcmd` con safety SELECT-only          | wrap |
| `Invoke-WmsBrainQuestion`         | corre `suggestedQueries` de Q-NNN vía `Invoke-WmsBrainQuery` | nativo |
| `Test-WmsBrainEnvironment`        | (puro PS, valida vars + paths)                  | nativo |
| `Test-WmsBrainConnection`         | (puro PS, ping SQL + permisos)                  | nativo |
| `Invoke-WmsBrainSuite`            | `wms-brain/suites/<name>/run.sql` vía Invoke-WmsBrainQuery | nativo |
| `Invoke-WmsBrainScenario`         | `wms-brain/scenarios/<name>/setup.sql` (read-only por contrato) | nativo |
| `Get-WmsBrainBundleHistory`       | parsea `entregables_ajuste/*/v*/apply_log.json` | nativo |
| `Get-WmsBrainQuestion`            | enumera `questions/*.md` del repo               | nativo |
| `Show-WmsBrainStatus`             | (puro PS, banner de estado)                     | nativo |

---

## 1. Bootstrap y handshake

### `Invoke-WmsBrainHello`

```powershell
Invoke-WmsBrainHello
  -Rol <consumidor|productor>
  [-ExchangeRepo <path>]                 # default: $env:WMS_BRAIN_EXCHANGE_REPO_MAIN
  [-WmsRepo <path>]                      # obligatorio si Rol=consumidor
  [-NoPull]
  [-Quiet]
```

**Delega a**: `node $ExchangeRepo\scripts\hello_sync.mjs --rol $Rol --exchange-repo $ExchangeRepo [--wms-repo $WmsRepo] [--no-pull] [--quiet]`.

**Salida**: PSCustomObject con `{Rol, ExchangeBranch, ExchangeHead, BundlesEncontrados, UltimoProducido, UltimoAplicado, Pendiente}`. Imprime el banner ASCII "Hello Erik" y exit 0 si todo OK.

### `Invoke-WmsBrainBootstrap`

```powershell
Invoke-WmsBrainBootstrap
  [-ScriptPath <path>]                   # default: $env:WMS_BRAIN_EXCHANGE_REPO_MAIN\scripts\brain-up.ps1
  [-Force]
```

**Delega a**: `& $ScriptPath` (el `.ps1` ya existente; instala módulo, cargo aliases, valida `$env:WMS_KILLIOS_DB_PASSWORD`).

### `Test-WmsBrainEnvironment`

```powershell
Test-WmsBrainEnvironment
  [-Strict]                              # exit 3 si falta alguna var
```

Valida (en orden):

1. `$env:WMS_BRAIN_EXCHANGE_REPO_MAIN` apunta a un clon existente, en rama `main`.
2. `$env:WMS_BRAIN_EXCHANGE_REPO_BRAIN` apunta a un clon en rama `wms-brain`.
3. `$env:WMS_BRAIN_CLIENT_REPO` apunta a un clon en rama `wms-brain-client`.
4. Existen los `.mjs`: `brain_bridge.mjs`, `apply_bundle.mjs`, `hello_sync.mjs`.
5. `node --version` >= 20.
6. `Get-Command Invoke-Sqlcmd` existe.
7. Vars heredadas: `WMS_KILLIOS_DB_HOST`, `WMS_KILLIOS_DB_USER`, `WMS_KILLIOS_DB_PASSWORD`, `BRAIN_BASE_URL`, `BRAIN_IMPORT_TOKEN`, `AZURE_DEVOPS_PAT`.
8. Coincidencia de schema: lee `SCHEMA_VERSION` constante en `brain_bridge.mjs` y compara con el esperado del cliente.

**Salida**:

```
[OK]   ExchangeRepo MAIN     C:\tomwms-exchange-main         branch=main
[OK]   ExchangeRepo BRAIN    C:\tomwms-exchange-brain        branch=wms-brain
[OK]   ClientRepo            C:\tomwms-brain-client          branch=wms-brain-client
[OK]   brain_bridge.mjs       SCHEMA_VERSION=1
[OK]   apply_bundle.mjs       (con --brain-message)
[OK]   hello_sync.mjs
[OK]   node v20.11.0
[OK]   Invoke-Sqlcmd          (SqlServer module 22.x)
[OK]   $env:WMS_KILLIOS_DB_PASSWORD   (set, no-print)
[WARN] $env:BRAIN_IMPORT_TOKEN        (not set, opcional para read-only)
```

---

## 2. Aplicación de bundles

### `Invoke-WmsBrainApplyBundle`

```powershell
Invoke-WmsBrainApplyBundle
  [-Latest]
  [-Bundle <path>]                       # mutuamente excluyente con -Latest
  -Repo <path>                           # path al repo VS (TOMWMS_BOF)
  [-RamaDestino <string>]
  [-DryRun]
  [-Yes]
  [-BundlesRoot <path>]
  [-BrainMessage <string>]
  [-BrainModules <string>]               # csv
  [-BrainTags <string>]                  # csv
```

**Delega a**: `node $main\scripts\apply_bundle.mjs` con todos los flags mapeados 1:1.

**Salida**: PSCustomObject con `{Result, Branch, CommitSha, MarkerHits, ApplyLogPath, BrainEventPath}`. Si pasaste `-BrainMessage`, devuelve el `BrainEventPath` listo para `Invoke-WmsBrainNotify`.

### `Get-WmsBrainBundleHistory`

```powershell
Get-WmsBrainBundleHistory
  [-Repo <path>]                         # default: $env:WMS_BRAIN_EXCHANGE_REPO_MAIN
  [-Last <int>]                          # default: 10
  [-Status <OK|FAIL|PENDING>]
```

Lee `entregables_ajuste/*/v*_bundle/apply_log.json` y devuelve tabla.

---

## 3. Eventos del bridge (schema_version "1" vigente)

### `New-WmsBrainEvent`

```powershell
New-WmsBrainEvent
  -Type <apply_succeeded|apply_failed|skill_update|directive|merge_completed|external_change>
  -Source <openclaw|replit|manual|apply_bundle>
  -Message <string>
  [-Modules <string[]>]
  [-Tags <string[]>]
  [-Bundle <string>]
  [-CommitSha <string>]
  [-RamaDestino <string>]
  [-FilesChanged <string[]>]
  [-Marker <string>]
  [-Author <string>]                     # default: $env:WMS_BRAIN_AUTHOR_INIT (default EJC)
  [-OutputDir <path>]                    # default: $env:TEMP\WmsBrainEvents\
```

**Comportamiento**:

1. Genera `id` con formato `YYYYMMDD-HHMM-AUTHOR` (regenera con `+1min` si colisiona).
2. Construye PSCustomObject con shape exacto del bridge (`schema_version="1"`, `created_at` con offset local, `status="pending"`, `analysis=null`, `proposal=null`, `decision=null`, `history=[{at,action="notify",by=AUTHOR}]`).
3. Serializa a JSON con indent 2, encoding UTF-8 sin BOM.
4. Escribe a `$OutputDir\<id>.json`.

**Salida**: `[PSCustomObject]@{ Id=...; Path=...; Event=... }`.

### `Invoke-WmsBrainNotify`

```powershell
Invoke-WmsBrainNotify
  [-ExchangeRepo <path>]                 # default: $env:WMS_BRAIN_EXCHANGE_REPO_BRAIN
  -FromEventFile <path>
  [-NoPush]
```

**Delega a**: `node $main\scripts\brain_bridge.mjs notify --exchange-repo $ExchangeRepo --from-event-file $FromEventFile`.

**Pre-flight**:

1. Verifica `git -C $ExchangeRepo branch --show-current` == `wms-brain`. Si no, exit 4.
2. Verifica working tree limpio en `$ExchangeRepo`. Si no, exit 4.
3. Lee el `.json` del evento y valida schema mínimo (`id`, `type`, `source`).

**Post**: si el bridge salió OK, opcionalmente `git -C $ExchangeRepo push origin wms-brain` (a menos que `-NoPush`).

### `Get-WmsBrainEventQueue`

```powershell
Get-WmsBrainEventQueue
  [-ExchangeRepo <path>]
  [-Status <pending|analyzed|proposed|applied|skipped|all>]   # default: pending
  [-Type <string>]                       # filtro client-side
  [-Tag <string>]
```

**Delega a**: `node ... brain_bridge.mjs list --exchange-repo ...` y postprocesa la salida JSON.

### `Get-WmsBrainEvent`

```powershell
Get-WmsBrainEvent
  [-ExchangeRepo <path>]
  -Id <string>
```

**Delega a**: `node ... brain_bridge.mjs show --exchange-repo ... --id $Id`.

---

## 4. Conexión y queries SQL (read-only)

### `Get-WmsBrainConnectionString`

```powershell
Get-WmsBrainConnectionString
  [-Profile <K7-PRD|BB-PRD|C9-QAS|LOCAL_DEV>]   # default: $env:WMS_BRAIN_DEFAULT_PROFILE
  [-AsHashtable]
```

Devuelve string de conexión completo (con `$env:WMS_KILLIOS_DB_PASSWORD` resuelto en runtime, **nunca** lo loggea).

### `Invoke-WmsBrainQuery`

```powershell
Invoke-WmsBrainQuery
  [-Profile <string>]
  -Query <string>                        # SQL crudo
  [-MaxRows <int>]                       # default: 10000
  [-QueryTimeout <int>]                  # default: 60
  [-AsCsv <path>]
```

**Safety**:

1. Antes de ejecutar, parsea el SQL con regex anti-DML: refusa si encuentra `\b(INSERT|UPDATE|DELETE|MERGE|TRUNCATE|DROP|ALTER|CREATE|GRANT|REVOKE|EXEC|EXECUTE|sp_|xp_)\b` fuera de comentarios. Exit 7 si detecta intento de DML.
2. Si pasa, ejecuta `Invoke-Sqlcmd` con `MaxRows` enforced.
3. Si `-AsCsv`, exporta a CSV con `Export-Csv -NoTypeInformation -Encoding UTF8`.

**Salida**: array de PSCustomObject (igual que `Invoke-Sqlcmd`).

### `Test-WmsBrainConnection`

```powershell
Test-WmsBrainConnection
  [-Profile <string>]
  [-IncludeBrainApi]                     # tambien testea $env:BRAIN_BASE_URL/health
```

Ejecuta `SELECT @@VERSION, USER_NAME(), DB_NAME()` y reporta latencia + perms.

---

## 5. Suites y scenarios

### `Get-WmsBrainSuiteList`

```powershell
Get-WmsBrainSuiteList
  [-BrainRepo <path>]                    # default: $env:WMS_BRAIN_EXCHANGE_REPO_BRAIN
```

Enumera `suites/*/README.md` y `scenarios/*/README.md` del clon en rama `wms-brain`.

**Salida**:

```
Type      Name                       Steps  Description
--------  -------------------------  -----  --------------------------------------------------
suite     outbox-health              5      Salud del outbox HH→VB→SQL
suite     state-machine-pedido       7      Trazabilidad estado del pedido (S1-S7)
suite     bug-p16b-detector          5      Detector del bug P16B (B1-B5)
scenario  doble-despacho             -      Reproduce doble despacho del mismo lote
```

### `Invoke-WmsBrainSuite`

```powershell
Invoke-WmsBrainSuite
  -Name <string>                         # ej. state-machine-pedido
  [-Profile <string>]
  [-Step <string>]                       # ej. S3 (corre solo ese)
  [-OutputDir <path>]
```

Lee `wms-brain/suites/<name>/<step>.sql`, los corre vía `Invoke-WmsBrainQuery`, escribe resultados a `$OutputDir\<name>\<step>.csv` y un `summary.md` con conteos.

### `Invoke-WmsBrainScenario`

```powershell
Invoke-WmsBrainScenario
  -Name <string>
  [-Profile <string>]
  [-OutputDir <path>]
```

**Importante**: en este cliente los scenarios son **read-only**. No aplican `setup.sql` con writes; ejecutan únicamente las queries de detección/expectations descritas en `wms-brain/scenarios/<name>/`.

---

## 6. Question cards (workaround §5 del PROTOCOL)

### `Get-WmsBrainQuestion`

```powershell
Get-WmsBrainQuestion
  [-Status <pending|in-progress|answered|closed|all>]
  [-Codename <string>]
  [-Priority <low|medium|high|critical>]
  [-Tag <string[]>]
```

Lee `questions/*.md` de la rama `wms-brain-client` (este repo) y parsea YAML front-matter.

**Salida**:

```
Id      Title                                              Priority  Codename  Tags
------  -------------------------------------------------  --------  --------  ----------------------
Q-001   Cadencia real del job que procesa SALIDAS en BB    medium    BB        outbox,navsync,BB
Q-003   Por que BB tiene 110k INGRESOS pendientes (PEND-12) critical BB        outbox,bandera-roja
```

### `Show-WmsBrainQuestion`

```powershell
Show-WmsBrainQuestion -Id <string> [-NoQueries] [-Markdown]
```

### `Invoke-WmsBrainQuestion`

```powershell
Invoke-WmsBrainQuestion
  -Id <string>
  [-Profile <string>]                    # default: target principal de la card
  [-OnlyQueries <string[]>]
  [-MaxRows <int>]
  [-DryRun]
```

Ejecuta cada `suggestedQueries[*].sql` vía `Invoke-WmsBrainQuery`. Guarda outputs en `$env:TEMP\WmsBrainClient\drafts\<Id>\<query-id>.csv` y genera draft de answer card.

### `New-WmsBrainQuestionEvent`

```powershell
New-WmsBrainQuestionEvent
  -QuestionId <string>                   # ej. Q-001
  [-Source <string>]                     # default: openclaw
  [-Author <string>]
  [-OutputDir <path>]
```

**Workaround mientras schema_version "2" no esté aprobado**: lee la card `questions/Q-NNN-*.md`, extrae `title`, `tags`, `targets`, y construye un `New-WmsBrainEvent` con:

- `-Type directive`
- `-Tags @("question", $QuestionId, ...tags-de-la-card...)`
- `-Modules` derivado de los `targets[*].codename`
- `-Message "<title>. Ver wms-brain-client/questions/<file>.md"`

Cuando se acepte v2, este cmdlet pasará a emitir `-Type question_request` nativo. Ver `EXTENSION-V2-PROPOSAL.md`.

### `Submit-WmsBrainAnswer`

```powershell
Submit-WmsBrainAnswer
  -QuestionId <string>
  -Verdict <confirmed|partial|inconclusive|rejected|error>
  -Confidence <low|medium|high>
  [-DraftPath <path>]                    # default: $env:TEMP\WmsBrainClient\drafts\<Id>\answer.md
  [-EditNotes]
  [-NoNotify]                            # solo escribe el .md, no encola brain_event
```

Flujo:

1. Lee draft local.
2. Sanitiza (codenames, hashes, no-leak de password).
3. Escribe answer card final en `answers/A-NNN-<slug>.md` del repo `wms-brain-client`.
4. (Si no `-NoNotify`) crea `brain_event.json` con `-Type directive`, `-Tags @("answer","A-NNN","Q-NNN")` y notifica.

---

## 7. Lado productor — referencia (NO se usa desde PowerShell)

Estos comandos viven del lado Replit (agente), no del lado consumidor. Documentados para que Erik los conozca:

```bash
# En el clon del exchange en rama wms-brain (Replit):
node scripts/brain_bridge.mjs list    --exchange-repo /tmp/exchange-rw
node scripts/brain_bridge.mjs show    --exchange-repo /tmp/exchange-rw --id 20260427-1845-EJC
node scripts/brain_bridge.mjs analyze --exchange-repo /tmp/exchange-rw --id 20260427-1845-EJC
# (humano edita los .md del brain)
node scripts/brain_bridge.mjs apply   --exchange-repo /tmp/exchange-rw --id 20260427-1845-EJC \
  --note "Actualizada SKILL §6: regla X ya no aplica" --by EJC
node scripts/brain_bridge.mjs skip    --exchange-repo /tmp/exchange-rw --id 20260427-1845-EJC \
  --reason "Solo refactor cosmetico, brain no se entera"
```

---

## 8. Convenciones generales

- Todo cmdlet de escritura: `[CmdletBinding(SupportsShouldProcess=$true,ConfirmImpact='High')]` → `-WhatIf` y `-Confirm` automáticos.
- Todo cmdlet retorna PSCustomObject pipeable.
- Errores: `Write-Error` (terminating con `-ErrorAction Stop`).
- Logs estructurados: `[WmsBrainClient] [<cmdlet>] [<level>] <msg>` a `$VerbosePreference`.
- **Nunca** loggear `$env:WMS_KILLIOS_DB_PASSWORD` ni `$env:BRAIN_IMPORT_TOKEN`.
- Encoding de outputs: UTF-8 sin BOM, salvo VB que requiere UTF-8 con BOM (no aplica acá).

---

## 9. Verbos aprobados utilizados

`Get`, `Show`, `Set`, `New`, `Test`, `Invoke`, `Submit`, `Send`, `Remove`, `Compare`, `Save`, `Start`. Todos en `Get-Verb`.
