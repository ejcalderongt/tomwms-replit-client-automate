# WmsBrainClient (PowerShell)

Cliente operativo del ecosistema **TOMWMS Brain**. Wrappea los `.mjs` del repo
de exchange (`brain_bridge.mjs`, `apply_bundle.mjs`, `hello_sync.mjs`) y la
conexion read-only a las BDs productivas (K7-PRD, BB-PRD, C9-QAS).

> Modulo: `WmsBrainClient` v0.2.0
> Schema bridge esperado: `"1"` (alineado al `SCHEMA_VERSION` del
> `brain_bridge.mjs` actual).
> Branch: `wms-brain-client` del repo
> [`tomwms-replit-client-automate`](https://github.com/ejcalderongt/tomwms-replit-client-automate).

## Instalacion

```powershell
# Desde el clon de wms-brain-client:
cd clients\wms-brain-client
.\scripts\install.ps1 -Force -InstallDependencies
```

Esto copia `src\*` a:

```
$HOME\Documents\PowerShell\Modules\WmsBrainClient\0.2.0\
```

E instala las dependencias `powershell-yaml` y `SqlServer` (Install-Module en
CurrentUser scope).

## Importar el modulo

Despues de correr `scripts\install.ps1`, el modulo queda en el path estandar
de PowerShell, asi que basta con:

```powershell
Import-Module WmsBrainClient
Get-Module WmsBrainClient | Select-Object Name, Version, ExportedCommands
```

Para forzar una version especifica (ej. recien instalada vs cacheada):

```powershell
Import-Module WmsBrainClient -RequiredVersion 0.2.0 -Force
```

Si no quieres pasar por el instalador, tambien podes importar directo desde
el clon (util para desarrollo):

```powershell
Import-Module .\src\WmsBrainClient.psd1 -Force
```

Verifica que cargo bien con un comando rapido:

```powershell
Test-WmsBrainEnvironment
```

## Variables de entorno

| Var                              | Obligatoria | Detalle                                                     |
|----------------------------------|-------------|-------------------------------------------------------------|
| `WMS_BRAIN_EXCHANGE_REPO_MAIN`   | si          | path al clon del exchange en rama `main`                    |
| `WMS_BRAIN_EXCHANGE_REPO_BRAIN`  | si          | path al clon del exchange en rama `wms-brain`               |
| `WMS_BRAIN_CLIENT_REPO`          | si          | path al clon de `wms-brain-client`                          |
| `WMS_KILLIOS_DB_PASSWORD`        | si          | password de `wmsuser` para K7/BB/C9 PRD/QAS                 |
| `WMS_BRAIN_DEFAULT_PROFILE`      | no          | perfil default para `Invoke-WmsBrainQuery`. Default K7-PRD  |
| `WMS_BRAIN_AUTHOR_INIT`          | no          | iniciales para id de eventos. Default EJC                   |
| `BRAIN_BASE_URL`                 | no          | base URL del API brain (solo `Test-WmsBrainConnection`)     |
| `BRAIN_IMPORT_TOKEN`             | no          | bearer token opcional                                       |
| `AZURE_DEVOPS_PAT`               | no          | token de Azure DevOps                                       |

> **Nunca** se loggea el contenido de `WMS_KILLIOS_DB_PASSWORD`,
> `BRAIN_IMPORT_TOKEN` ni `AZURE_DEVOPS_PAT`. El sanitizador interno los
> reemplaza por placeholders.

## Comandos implementados (23)

### Bootstrap

- `Invoke-WmsBrainHello -Rol consumidor -WmsRepo C:\src\TOMIMSV4`
  Handshake operativo (delega a `hello_sync.mjs`).
- `Invoke-WmsBrainBootstrap`
  Corre `brain-up.ps1` (instala/actualiza modulo, valida vars).
- `Test-WmsBrainEnvironment [-Strict]`
  Reporta repos, paths, scripts, node, modulo SqlServer, vars y match de
  `SCHEMA_VERSION`.

### Bundles

- `Invoke-WmsBrainApplyBundle -Latest -Repo C:\src\TOMIMSV4 -BrainMessage "v23 OK"`
  Delega 1:1 a `apply_bundle.mjs`. Mapea todos los flags incluyendo
  `--brain-*` para que despues podas notificar.
- `Get-WmsBrainBundleHistory -Last 10`
  Tabla de bundles aplicados (lee `entregables_ajuste/*/v*_bundle/apply_log.json`).

### Eventos del bridge (schema_version "1")

- `New-WmsBrainEvent -Type apply_succeeded -Source apply_bundle -Message "..." -Bundle v23 -CommitSha abc1234`
  Genera `<id>.json` valido a disco (UTF-8 sin BOM).
- `Invoke-WmsBrainNotify -FromEventFile $evt.Path`
  Encola via `brain_bridge.mjs notify`. Pre-flight: rama `wms-brain` y working
  tree limpio. Acepta tanto eventos completos (con `id`, `status` v1) como
  drafts emitidos por `apply_bundle.mjs --brain-message` (sin `id` y/o
  `status=draft`); el bridge hidrata los campos faltantes durante notify.
  Default: `git push origin wms-brain` despues.
- `Get-WmsBrainEventQueue [-Status pending]`
- `Get-WmsBrainEvent -Id 20260427-1845-EJC`

### SQL read-only

- `Get-WmsBrainConnectionString -Profile BB-PRD [-AsHashtable]`
- `Invoke-WmsBrainQuery -Profile BB-PRD -Query "SELECT TOP 10 * FROM ..."`
  Refusa cualquier DML/DDL antes de mandarse al server (exit 7).
- `Test-WmsBrainConnection -Profile K7-PRD [-IncludeBrainApi]`

### Suites y scenarios

- `Get-WmsBrainSuiteList`
  Tabla de `suites/` y `scenarios/` del clon `wms-brain`.
- `Invoke-WmsBrainSuite -Name outbox-health -Profile BB-PRD`
- `Invoke-WmsBrainScenario -Name doble-despacho -Profile BB-PRD`
  En este cliente los scenarios son **READ-ONLY**: `setup.sql` no se ejecuta;
  solo `detection.sql`, `expectations.sql` y `queries/*.sql`.

### Question cards (workaround mientras schema v2 no este aprobado)

- `Get-WmsBrainQuestion [-Status pending] [-Codename BB] [-Priority high] [-Tag outbox]`
- `Show-WmsBrainQuestion -Id Q-001`
- `Invoke-WmsBrainQuestion -Id Q-001 [-Profile BB-PRD]`
  Corre los `suggestedQueries[*].sql`, escribe CSVs en
  `$env:TEMP\WmsBrainClient\drafts\<Id>\` y prepara draft de answer card.
- `New-WmsBrainQuestionEvent -QuestionId Q-001`
  Workaround: emite `brain_event.json` de tipo `directive` con
  `tags=["question","Q-NNN",...]` (no `question_request` aun).
- `New-WmsBrainAnswerEvent -QuestionId Q-001 -AnswerId A-001 -AnswerFile A-001-q-001.md -Verdict confirmed -Confidence high`
  Workaround: re-emite el evento de respuesta como `directive` con
  `tags=["answer","A-NNN","Q-NNN"]` (util si `Submit-WmsBrainAnswer` ya
  promovio el .md y solo hace falta notificar de nuevo). Migra a
  `question_answer` cuando se acepte schema v2.
- `New-WmsBrainLearningEvent -Title "NavSync corre cada 2 min" -Scope BB -SourceQuestionId Q-001`
  Workaround: empuja un learning propuesto al brain como `directive` con
  `tags=["learning","L-...","<scope>",...]`. Acepta opcionalmente
  `-LearningCardPath` si tenes un .md pre-armado. Migra a
  `learning_proposed` cuando se acepte schema v2.
- `Submit-WmsBrainAnswer -QuestionId Q-001 -Verdict confirmed -Confidence high`
  Promueve el draft a `answers/A-NNN-<slug>.md` y opcionalmente notifica al
  brain (usa internamente la misma shape que `New-WmsBrainAnswerEvent`).

### UX

- `Show-WmsBrainStatus` (alias `wmsbc`)
  Banner de estado (version, schema match, repos, pendientes).

## Flujos tipicos

### Aplicar un bundle y notificar al brain

```powershell
$r = Invoke-WmsBrainApplyBundle -Latest -Repo C:\src\TOMIMSV4 `
    -BrainMessage "v23 fix ajuste borrador" `
    -BrainModules "frmAjusteStock,ajuste_rules" `
    -BrainTags "validation,ajuste,v23"

if ($r.BrainEventPath) {
    Invoke-WmsBrainNotify -FromEventFile $r.BrainEventPath
}
```

### Responder una question card

```powershell
Get-WmsBrainQuestion -Status pending | Format-Table

# Ver detalle
Show-WmsBrainQuestion -Id Q-001

# Correr las queries sugeridas
Invoke-WmsBrainQuestion -Id Q-001 -Profile BB-PRD

# (editar el draft en $env:TEMP\WmsBrainClient\drafts\Q-001\answer.md)

Submit-WmsBrainAnswer -QuestionId Q-001 -Verdict confirmed -Confidence high
```

### Verificar entorno antes de operar

```powershell
Test-WmsBrainEnvironment -Strict
Show-WmsBrainStatus
```

## Schema v2 (futuro)

Cuando se acepte el bump del `brain_bridge.mjs` a `SCHEMA_VERSION="2"` (ver
`EXTENSION-V2-PROPOSAL.md` en este repo), `New-WmsBrainQuestionEvent` pasara a
emitir `question_request` nativo y `Submit-WmsBrainAnswer` emitira
`question_answer` con status `answered`. La firma publica de los cmdlets se
mantiene; solo cambia el `type` interno del JSON.

## Codigos de salida

| Codigo | Significado                                      |
|--------|--------------------------------------------------|
| 0      | OK                                               |
| 2      | Argumento o path invalido                        |
| 3      | Variable de entorno faltante                     |
| 4      | Estado git invalido (rama incorrecta o dirty)    |
| 5      | Mismatch de schema_version                       |
| 6      | Error de conexion SQL                            |
| 7      | Intento de DML/DDL detectado                     |
| 8      | Error heredado de subprocess (.mjs / Invoke-Sqlcmd) |

## Tests

Pester en `tests/Pester/` (14 archivos de tests):

```powershell
Invoke-Pester ./tests/Pester
```

Cobertura por area:

| Archivo                              | Cubre                                                               |
|--------------------------------------|---------------------------------------------------------------------|
| `SqlSafety.Tests.ps1`                | Regex anti-DML/DDL, comentarios y string literals.                  |
| `IdGenerator.Tests.ps1`              | Formato YYYYMMDD-HHMM-INIT y bump de minuto.                        |
| `EventSchema.Tests.ps1`              | Validador de shape, enums type/status/source.                       |
| `ProfileResolver.Tests.ps1`          | Perfiles K7/BB/C9/LOCAL_DEV y password en runtime.                  |
| `NewWmsBrainEvent.Tests.ps1`         | Emision de evento valido + colision de id + host.                   |
| `AnswerLearningEvent.Tests.ps1`      | `New-WmsBrainAnswerEvent` y `New-WmsBrainLearningEvent` end-to-end. |
| `Logger.Tests.ps1`                   | Sanitizado de secretos en logs y niveles validos.                   |
| `QuestionCardParser.Tests.ps1`       | Parseo de front-matter YAML y resolucion de questions/.             |
| `EventQueueParse.Tests.ps1`          | Surface de `Get-WmsBrainEventQueue` y `Get-WmsBrainEvent`.          |
| `BridgeOutputParser.Tests.ps1`       | Parseo behavioral de `brain_bridge.mjs list` y `hello_sync.mjs`.    |
| `NotifyDraft.Tests.ps1`              | `-AllowDraft` para drafts de `apply_bundle.mjs` y gate de notify.   |
| `ProcessRunner.Tests.ps1`            | Quoting Win32 + regresion: runners no usan `ArgumentList` (PS 5.1). |
| `PublicSurface.Tests.ps1`            | Cmdlets exportados, `SupportsShouldProcess`, alias, help.           |
| `ModuleLoad.Tests.ps1`               | `Test-ModuleManifest`, import, comment-based help completa.         |

**No** ejercita SQL, git ni `node` en vivo (eso debe correrse contra
K7/BB/C9 desde Windows).

## Convenciones

- Verbos PowerShell aprobados (`Get-Verb`).
- Cmdlets de escritura: `[CmdletBinding(SupportsShouldProcess=$true)]` →
  `-WhatIf` y `-Confirm` automaticos.
- Output: PSCustomObject pipeable.
- Logs estructurados a `Write-Information` / stderr.
- Encoding: UTF-8 sin BOM.
