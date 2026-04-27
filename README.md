# WmsBrainClient (PowerShell)

[![wms-brain-client smoke](https://github.com/ejcalderongt/tomwms-replit-client-automate/actions/workflows/wms-brain-client-smoke.yml/badge.svg)](https://github.com/ejcalderongt/tomwms-replit-client-automate/actions/workflows/wms-brain-client-smoke.yml)

Cliente operativo del ecosistema **TOMWMS Brain**. Wrappea los `.mjs` del repo
de exchange (`brain_bridge.mjs`, `apply_bundle.mjs`, `hello_sync.mjs`) y la
conexion read-only a las BDs productivas (K7-PRD, BB-PRD, C9-QAS).

> Modulo: `WmsBrainClient` v0.2.0
> Schema bridge esperado: `"2"` (vivo en `brain_bridge.mjs` desde 2026-04-27,
> Task #21). Los eventos schema v1 siguen siendo validos por aditividad.
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
CurrentUser scope) **pinneadas a versiones exactas** para que dos personas que
corran el instalador en dias distintos terminen con el mismo build (misma
idea que el pin de Pester en CI, ver seccion "Tests").

### Versiones pinneadas

| Modulo            | Version pinneada | Definida en                                 |
|-------------------|------------------|---------------------------------------------|
| `powershell-yaml` | `0.4.7`          | `scripts/install.ps1` (`$PinnedDependencyVersions`) |
| `SqlServer`       | `22.3.0`         | `scripts/install.ps1` (`$PinnedDependencyVersions`) |

`scripts/install.ps1` instala estos modulos con
`Install-Module -Name <dep> -RequiredVersion <ver> -Scope CurrentUser` y
considera la dependencia satisfecha solo si `Get-Module -ListAvailable`
encuentra **esa misma version** instalada (no acepta una version mayor
silenciosamente).

Para bumpear alguna de estas versiones a futuro:

1. `Find-Module -Name <dep>` en PSGallery para ver las versiones disponibles.
2. Editar el hashtable `$PinnedDependencyVersions` al principio del bloque
   de dependencias en `clients/wms-brain-client/scripts/install.ps1`.
3. Actualizar la tabla de arriba.
4. En una maquina limpia: `.\scripts\install.ps1 -Force -InstallDependencies`
   y luego `.\scripts\Run-SmokeTest.ps1` para confirmar que sigue verde.

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

## Quick Start

Si es la primera vez que usas el modulo (o si simplemente queres un dashboard
rapido de que esta seteado y que falta), corre:

```powershell
Show-WmsBrainQuickStart
```

Imprime un dashboard one-shot con cinco bloques:

1. **Header** — version del modulo, schema esperado y schema detectado
   en `brain_bridge.mjs`.
2. **Pre-flight** — resumen de `Test-WmsBrainEnvironment` (OK / WARN /
   ERROR por cada chequeo).
3. **Variables de entorno** — tabla con las 10 vars relevantes y su
   estado actual (`OK` / `MISSING` / `SET` con redaccion para
   secretos).
4. **Proximos pasos** — comandos sugeridos en orden segun lo que
   detecte que falta.
5. **Ejemplos copy-paste** — 4-5 comandos para empezar a usar el
   modulo.

Si te falta seterar variables criticas, podes hacerlo de forma interactiva
desde el mismo cmdlet:

```powershell
# Pide via Read-Host las criticas faltantes y las setea para la sesion.
Show-WmsBrainQuickStart -SetMissing

# Igual que arriba, pero ademas persiste en el scope User las que NO son
# secretos. Los secretos (DB password, tokens) NUNCA se persisten en disco
# aunque pases -Persist; solo quedan en la sesion actual.
Show-WmsBrainQuickStart -SetMissing -Persist
```

Cuando ya conoces el modulo y solo queres un nudge corto:

```powershell
Show-WmsBrainQuickStart -Compact
```

El cmdlet tambien devuelve un objeto consumible desde scripts
(`ModuleVersion`, `ExpectedSchemaVersion`, `DetectedSchemaVersion`,
`EnvSummary`, `NextSteps`).

Para ayuda enriquecida con ejemplos de cualquier cmdlet:

```powershell
Get-Help Show-WmsBrainQuickStart -Full
Get-Help New-WmsBrainQuestionEvent -Examples
Get-Help Submit-WmsBrainAnswer -Full
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

## Comandos implementados (24)

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

### Eventos del bridge (schema v1 + v2 aditivo)

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

### Question cards (schema v2 nativo)

- `Get-WmsBrainQuestion [-Status pending] [-Codename BB] [-Priority high] [-Tag outbox]`
- `Show-WmsBrainQuestion -Id Q-001`
- `Invoke-WmsBrainQuestion -Id Q-001 [-Profile BB-PRD]`
  Corre los `suggestedQueries[*].sql`, escribe CSVs en
  `$env:TEMP\WmsBrainClient\drafts\<Id>\` y prepara draft de answer card.
- `New-WmsBrainQuestionEvent -QuestionId Q-001 [-LegacyDirective]`
  Emite `brain_event.json` de tipo `question_request` schema v2 (con
  `ref.question_id`, `ref.question_card_path`, `context.targets[]` y
  `context.expected_outputs[]`). Si el bridge esta en v1 o se pasa
  `-LegacyDirective` / `WMS_BRAIN_FORCE_V1`, mantiene compat emitiendo
  `directive` + `tags=["question","Q-NNN",...]`.
- `New-WmsBrainAnswerEvent -QuestionId Q-001 -AnswerId A-001 -AnswerFile A-001-q-001.md -Verdict confirmed -Confidence high [-LegacyDirective]`
  Re-emite el evento de respuesta como `question_answer` schema v2
  (`status="answered"`, `ref.answers_question_id`, `ref.answer_card_path`,
  `context.verdict`/`context.confidence`). Util si `Submit-WmsBrainAnswer`
  ya promovio el .md y solo hace falta notificar de nuevo. Compat legacy
  `directive`+`tags=["answer","A-NNN","Q-NNN"]` si el bridge esta en v1.
- `New-WmsBrainLearningEvent -Title "NavSync corre cada 2 min" -Scope BB -SourceQuestionId Q-001 [-LegacyDirective]`
  Empuja un learning propuesto al brain como `learning_proposed` schema v2
  (con `ref.learning_id`, `ref.source_question_id` y `context.scope`).
  Acepta opcionalmente `-LearningCardPath` si tenes un .md pre-armado.
  Compat legacy `directive`+`tags=["learning","L-...","<scope>",...]` si
  el bridge esta en v1.
- `Submit-WmsBrainAnswer -QuestionId Q-001 -Verdict confirmed -Confidence high [-LegacyDirective]`
  Promueve el draft a `answers/A-NNN-<slug>.md` y opcionalmente notifica al
  brain emitiendo `question_answer` schema v2 (con `status="answered"`); el
  bridge marca automaticamente el `question_request` referenciado a
  `status="answered"` cuando entra el evento. Usa internamente la misma
  shape que `New-WmsBrainAnswerEvent`.

### UX

- `Show-WmsBrainStatus` (alias `wmsbc`)
  Banner de estado (version, schema match, repos, pendientes).
- `Show-WmsBrainQuickStart [-SetMissing] [-Persist] [-Compact]`
  Dashboard de inicio rapido: header + pre-flight + variables de entorno
  con su estado + proximos pasos sugeridos + ejemplos copy-paste. Con
  `-SetMissing` abre prompts interactivos para setear las variables
  criticas faltantes (los secretos via `Read-Host -AsSecureString` y
  jamas se persisten en disco aunque se pase `-Persist`). Ver "Quick
  Start" mas arriba para el flujo recomendado.

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

## Schema v2 (vigente desde 2026-04-27)

El bump del `brain_bridge.mjs` a `SCHEMA_VERSION="2"` esta vivo en produccion
desde 2026-04-27 (Task #21 — ver `EXTENSION-V2-PROPOSAL.md` para la spec
historica). `New-WmsBrainQuestionEvent` emite `question_request` nativo y
`Submit-WmsBrainAnswer` emite `question_answer` con `status="answered"`
cuando el bridge esta en v2 (deteccion automatica via
`Get-WmsBrainEffectiveSchemaVersion`). La firma publica de los cmdlets se
mantiene; solo cambia el `type` interno del JSON.

Los eventos schema v1 siguen siendo validos: la extension es **aditiva**.
Para forzar emision v1 puntual contra exchanges en versiones viejas del
bridge, los cmdlets aceptan el switch `-LegacyDirective` y respetan la
variable de entorno `WMS_BRAIN_FORCE_V1`.

## Publicar nueva version

La rama orphan `wms-brain-client` del repo
[`tomwms-replit-client-automate`](https://github.com/ejcalderongt/tomwms-replit-client-automate)
se publica desde el workspace de Replit con el script
`scripts/publish-wms-brain-client.mjs`. El script lee la version del manifest
(`src/WmsBrainClient.psd1`), arma un commit orphan con el contenido actual
de `clients/wms-brain-client/`, fuerza-actualiza la rama y crea/mueve el tag
`vX.Y.Z`. Verifica al final que el tree remoto contenga exactamente los mismos
archivos que el directorio local.

```bash
# Desde la raiz del workspace, con el conector "github" de Replit conectado:

# 1) Bump de version en src/WmsBrainClient.psd1 (ej. 0.2.0 -> 0.2.1) y
#    actualizar CHANGELOG.md cerrando la seccion ## [0.2.1] - YYYY-MM-DD.

# 2) Dry-run para revisar version detectada, archivos y mensaje:
node scripts/publish-wms-brain-client.mjs --dry-run

# 3) Publicacion real (lee la seccion del CHANGELOG como mensaje de commit/tag):
node scripts/publish-wms-brain-client.mjs --yes

# Opciones utiles:
#   --message "msg corto"   -> override del mensaje (en vez del CHANGELOG)
#   --no-tag                -> publica la rama pero no toca tags
#   --branch <nombre>       -> publicar a otra rama (default: wms-brain-client)
#   --repo owner/name       -> repo destino distinto
```

Auth: usa el conector `github` de Replit por default (sin PAT). Si corres el
script fuera de Replit (ej. CI Linux), exporta `GITHUB_TOKEN` con scope `repo`
y el script lo usa como fallback automatico. Ejecuta `node
scripts/publish-wms-brain-client.mjs --help` para ver todas las opciones.

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

> **Version de Pester en CI.** El workflow
> [`wms-brain-client-smoke.yml`](../../.github/workflows/wms-brain-client-smoke.yml)
> instala Pester pinneado a una version exacta (hoy: **5.7.1**, definida en
> `env.PESTER_VERSION` del job) y se la pasa al smoke script via
> `PESTER_REQUIRED_VERSION`, asi `Run-SmokeTest.ps1` hace
> `Import-Module Pester -RequiredVersion <pinned>` y dos corridas del mismo
> commit dan exactamente el mismo resultado. Para bumpear: editar
> `env.PESTER_VERSION` en el job del workflow y actualizar este parrafo.
> En desarrollo local la var no esta seteada y el script acepta cualquier
> Pester >= 5.0.0 (`Import-Module Pester -MinimumVersion 5.0.0`).

> **Smoke test 1-comando.** Si lo unico que queres es validar que el modulo
> carga y los tests pasan en una maquina nueva, corre:
>
> ```powershell
> .\scripts\Run-SmokeTest.ps1
> ```
>
> Eso ejecuta `Test-ModuleManifest`, `Import-Module + Get-Command` y
> `Invoke-Pester` en orden, y reescribe `docs/SMOKE-TEST-REPORT.md` con
> el resultado. Devuelve `exit 0` si paso, `exit 1` si fallo.

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

### Corrida nocturna y alerta de rotura

El workflow `.github/workflows/wms-brain-client-smoke.yml` corre en cron diario
a las **06:00 UTC (~00:00 America/Guatemala)**. Si esa corrida `schedule`
falla, el job abre/comenta automaticamente un **issue de GitHub** en el repo
[`tomwms-replit-client-automate`](https://github.com/ejcalderongt/tomwms-replit-client-automate)
con la label `nightly-smoke-failure` y lo **asigna** al owner del modulo
(default `@ejcalderongt`), para que llegue como notificacion directa de
GitHub sin depender de que alguien revise la pestaña Actions. El issue
incluye:

- Link directo al run que fallo.
- Commit del run roto.
- Ultimo commit conocido bueno (sacado del run verde mas reciente del mismo
  workflow) y link a ese run.

La logica es **idempotente**: si ya hay un issue abierto con esa label, en vez
de duplicar se agrega un comentario nuevo y se re-asegura que siga asignado
al owner. Cuando la siguiente corrida nocturna vuelve a verde, el mismo
workflow comenta "volvio a verde" y cierra el/los issue(s) abiertos con esa
label, asi no se acumulan.

> **Donde mirar:** la pestaña **Issues** del repo filtrando por label
> `nightly-smoke-failure`, mas las notificaciones de GitHub del owner
> asignado (mail / mobile), **mas el ping al canal de chat** (ver abajo).
>
> Para cambiar/ampliar los asignados sin tocar el workflow, definir la
> variable de repositorio (Settings -> Secrets and variables -> Actions ->
> Variables) `NIGHTLY_SMOKE_OWNERS` con una lista comma-separated de
> usernames de GitHub (ej. `ejcalderongt,otra-persona`).

#### Ping a Slack o Teams

Inmediatamente despues de abrir/comentar el issue, el workflow tambien
intenta postear al canal del equipo para bajar el tiempo de deteccion de
horas a minutos (issue + chat, no solo issue). Lee dos secrets opcionales
de GitHub Actions (Settings -> Secrets and variables -> Actions -> Secrets):

- `SLACK_WEBHOOK_URL` — Slack Incoming Webhook. Payload simple `{ text }`.
- `TEAMS_WEBHOOK_URL` — Teams Incoming Webhook **legacy** (MessageCard).
  Si el canal usa Power Automate / Workflows en vez del conector legacy,
  el payload no encaja y hay que ajustar el step a Adaptive Cards.

Solo hace falta configurar **uno** de los dos. Si **ninguno** esta seteado,
el step loguea un `::notice::` y sigue de largo (no rompe el job; el issue
de GitHub queda igual). Si el webhook responde error, se loguea un
`::warning::` pero tampoco rompe el job — el issue es la fuente
autoritativa de la alerta y no queremos que un canal caido enmascare otro
problema.

El mensaje incluye link al run que fallo, link al issue recien
abierto/comentado, y el commit roto. Igual que el issue, **solo** se
dispara en la corrida `schedule` (no en push/PR), para no spamear durante
desarrollo normal. El dry-run manual con `simulate_nightly` (ver mas
abajo) crea/cierra issues marcados `[SIMULACION]` pero **no** dispara el
ping al chat, asi probar la alerta nocturna no spamea el canal del equipo.

Cuando la siguiente corrida nocturna vuelve a verde y el step de cierre
efectivamente cierra >= 1 issue, se manda un segundo ping `volvio a verde`
al mismo canal con la lista de issues cerrados. Si la nocturna pasa verde
sin que hubiera issues abiertos (caso normal), no se manda nada.

#### Probar la alerta nocturna en seco

La logica de notificacion (abrir issue, deduplicar, cerrar al volver a
verde) solo se ejercita cuando el cron realmente se rompe. Para no
enterarte de un bug en los steps de notificacion (permisos faltantes,
shape del payload, label inexistente, etc.) la primera vez que se rompa
de verdad, el `workflow_dispatch` acepta el input `simulate_nightly` para
hacer un dry-run desde la pestaña **Actions**:

| `simulate_nightly` | Que hace                                                                                                                                                                                                |
|--------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `none` (default)   | Corrida normal. No toca issues.                                                                                                                                                                         |
| `failure`          | Fuerza un fallo temprano y dispara `Open or update nightly failure issue` como si fuera `schedule`. Crea/comenta un issue con la label `nightly-smoke-failure` marcando titulo y body con `[SIMULACION]`. |
| `success`          | Corre el smoke normal y, si pasa, dispara `Close nightly failure issue on green` para cerrar el issue de prueba creado por la corrida anterior.                                                          |

Receta para validar de punta a punta:

1. **Actions -> wms-brain-client smoke -> Run workflow**, elegir
   `simulate_nightly = failure`. Verificar en **Issues** que aparece uno
   nuevo con label `nightly-smoke-failure`, prefijo `[SIMULACION]` en el
   titulo, link al run, y el "ultimo commit conocido bueno" resuelto
   (no `(desconocido)` ni `(lookup fallo: 403 ...)`).
2. Repetir el paso 1 con la misma opcion. Verificar que **no** se crea
   un issue nuevo: solo se agrega un comentario al existente con el
   header `Otra corrida de SIMULACION`. Esto valida la deduplicacion.
3. Disparar otra vez con `simulate_nightly = success`. Verificar que el
   issue se cierra automaticamente con el comentario "Cierre simulado".
   Por seguridad, este modo **solo cierra issues cuyo titulo arranca con
   `[SIMULACION]`**, asi una prueba no puede cerrar por error un issue de
   rotura real en curso ni enmascararlo con un comentario simulado.
4. Si quedaron issues abiertos por algun fallo de la prueba misma,
   cerrarlos a mano (label `nightly-smoke-failure`).

Las corridas con `simulate_nightly = failure` quedan rojas en Actions
(es lo que se busca), por lo que conviene avisar antes de dispararlas
para no asustar al equipo cuando vean el badge en rojo brevemente.

## Convenciones

- Verbos PowerShell aprobados (`Get-Verb`).
- Cmdlets de escritura: `[CmdletBinding(SupportsShouldProcess=$true)]` →
  `-WhatIf` y `-Confirm` automaticos.
- Output: PSCustomObject pipeable.
- Logs estructurados a `Write-Information` / stderr.
- Encoding: UTF-8 sin BOM.
