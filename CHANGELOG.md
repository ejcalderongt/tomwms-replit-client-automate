# Changelog — WmsBrainClient

Todos los cambios relevantes del modulo PowerShell `WmsBrainClient`.
Formato: [Keep a Changelog](https://keepachangelog.com/es-ES/1.1.0/),
versionado semantico.

## [0.2.0] - 2026-04-27

### Added
- Estructura de modulo formal: `src/WmsBrainClient.psd1` + `.psm1` con loader
  que dot-sourcea `Private/_*.ps1` (helpers internos) y `Public/*.ps1` (cmdlets
  exportados).
- 9 helpers privados: `_BrainEventSchema`, `_NodeRunner`, `_SqlSafety`,
  `_IdGenerator`, `_GitHelpers`, `_ProfileResolver`, `_QuestionCardParser`,
  `_BridgeOutputParser`, `_Logger`.
- 23 cmdlets publicos:
  - **Bootstrap**: `Invoke-WmsBrainHello`, `Invoke-WmsBrainBootstrap`,
    `Test-WmsBrainEnvironment`.
  - **Bundles**: `Invoke-WmsBrainApplyBundle`, `Get-WmsBrainBundleHistory`.
  - **Eventos**: `New-WmsBrainEvent`, `Invoke-WmsBrainNotify`,
    `Get-WmsBrainEventQueue`, `Get-WmsBrainEvent`.
  - **Eventos extendidos (workaround v1, futuros nativos en v2)**:
    `New-WmsBrainQuestionEvent`, `New-WmsBrainAnswerEvent`,
    `New-WmsBrainLearningEvent`. Mapean hoy a `directive` con tags
    `["question",...]` / `["answer",...]` / `["learning",...]`.
  - **SQL read-only**: `Get-WmsBrainConnectionString`, `Invoke-WmsBrainQuery`,
    `Test-WmsBrainConnection`.
  - **Suites/scenarios**: `Get-WmsBrainSuiteList`, `Invoke-WmsBrainSuite`,
    `Invoke-WmsBrainScenario`.
  - **Question cards**: `Get-WmsBrainQuestion`, `Show-WmsBrainQuestion`,
    `Invoke-WmsBrainQuestion`, `Submit-WmsBrainAnswer`.
  - **UX**: `Show-WmsBrainStatus`.
- Safety SELECT-only para queries (regex anti-DML/DDL): refusa INSERT, UPDATE,
  DELETE, MERGE, TRUNCATE, DROP, ALTER, CREATE, GRANT, REVOKE, EXEC, EXECUTE,
  sp_, xp_ fuera de comentarios y string literals. Exit code 7 si pega.
- 4 perfiles de conexion conocidos: `K7-PRD`, `BB-PRD`, `C9-QAS`, `LOCAL_DEV`.
  Las 3 productivas comparten `52.41.114.122,1437` con usuario `wmsuser` y
  `$env:WMS_KILLIOS_DB_PASSWORD`. La pass nunca se cachea ni se loggea.
- Validador de schema_version contra `brain_bridge.mjs` real.
- Workaround para Q-NNN/A-NNN/L-* como brain_event tipo `directive` con
  `tags=["question","Q-NNN",...]` / `["answer","A-NNN","Q-NNN"]` /
  `["learning","L-...","<scope>",...]` (mientras schema v2 no este aprobado).
- Instalador: `scripts/install.ps1` (copia a
  `$HOME\Documents\PowerShell\Modules\WmsBrainClient\<version>\`).
- Suite Pester (sin SQL/git/node en vivo): SQL safety, schema, id generator,
  perfiles, evento, eventos answer/learning, logger, parser de cards,
  surface publica (cmdlets exportados + SupportsShouldProcess + help) y
  manifest/load.

### Changed
- Alineado al `brain_bridge.mjs` real (`SCHEMA_VERSION = "1"`). El cliente lee
  esa constante en runtime para detectar mismatch contra el esperado del
  modulo.
- Logs estructurados `[WmsBrainClient] [<cmdlet>] [<level>] <msg>`. Sanitiza
  WMS_KILLIOS_DB_PASSWORD / BRAIN_IMPORT_TOKEN / AZURE_DEVOPS_PAT antes de
  imprimir cualquier mensaje.

### Notes
- Schema v2 (con `question_request`, `question_answer`, `learning_proposed`,
  status `answered`) queda documentado en `docs/EXTENSION-V2-PROPOSAL.md`.
  Cuando se acepte el bump del bridge, `New-WmsBrainQuestionEvent`,
  `New-WmsBrainAnswerEvent`, `New-WmsBrainLearningEvent` y
  `Submit-WmsBrainAnswer` migran a tipos nativos. Hasta entonces, todo va por
  el workaround `directive` y se mantiene compat hacia adelante.
- `Test-WmsBrainEnvironment -Strict` lanza terminating error en lugar de
  `exit 3`, para no matar la sesion del host del llamador desde adentro
  del modulo. El codigo logico 3 sigue documentado en el mensaje del error.
- Parsers del bridge extraidos a helper privado `_BridgeOutputParser` y
  hechos tolerantes a los formatos reales:
  * `brain_bridge.mjs list`: acepta JSON puro (--json), tokens key=value
    (`id  type=...  status=...  message="..."`) y formato tabular legacy
    (`id status type summary`). Filtra headers/separadores.
  * `hello_sync.mjs`: acepta tanto el formato actual
    (`OK  rama=...  head=...  bundles=...`) como el verboso legacy
    (`Rama:`, `HEAD:`, `Bundles encontrados:`). Soporta sinonimos en
    ingles (`branch`, `last_produced`, `pending`).
  Hay tests Pester behavioral con stdout mockeado en
  `BridgeOutputParser.Tests.ps1`.
- `New-WmsBrainQuestionEvent` ahora invoca `$PSCmdlet.ShouldProcess(...)`
  para que `-WhatIf` y `-Confirm` funcionen como el resto de los cmdlets
  de escritura. Pasa `-Confirm:$false` al `New-WmsBrainEvent` interno
  para no doblar el prompt.
- `Test-WmsBrainEnvironment -Strict` ahora promueve mismatch de branch
  (main, wms-brain, wms-brain-client) a `ERROR` cuando se invoca con
  `-Strict`, para gating operacional duro. Sin `-Strict` queda como
  `WARN` para permitir inspeccion sin abortar.
- `Resolve-WmsBrainSqlcmdParams`: removido el seteo espurio de
  `MaxBinaryLength=0` (no era un row-limit; `Invoke-Sqlcmd` no expone
  `-MaxRows` nativo). El limite de filas se mantiene client-side via
  `Select-Object -First $MaxRows` en `Invoke-WmsBrainQuery`.
- `Invoke-WmsBrainNotify` ahora interopera con los drafts que emite
  `apply_bundle.mjs --brain-message` (sin `id`, con `status='draft'`).
  Detecta el draft via `Test-WmsBrainEventIsDraft` y aplica
  `Test-WmsBrainEventShape -AllowDraft` (id/created_at/history opcionales,
  status='draft' permitido), respetando que el bridge hidrata los campos
  faltantes durante el notify. Eventos completos siguen pasando por la
  validacion estricta v1. Test Pester nuevo en
  `NotifyDraft.Tests.ps1`.
- `Test-WmsBrainEventShape` ahora valida explicitamente que el campo
  `schema_version` del evento, si esta presente, coincida con el
  `-SchemaVersion` solicitado (rechaza un evento marcado v2 cuando se pide
  v1, etc.). Bajo `-AllowDraft` el campo puede faltar (lo hidrata el
  bridge), pero si esta presente sigue siendo validado. Tests Pester en
  `EventSchema.Tests.ps1`.
- `_NodeRunner.ps1` y `_GitHelpers.ps1` ya no usan
  `ProcessStartInfo.ArgumentList.Add(...)` (API exclusiva de .NET Core 2.1+,
  no presente en .NET Framework 4.x sobre el que corre Windows PowerShell
  5.1). Reemplazado por `ProcessStartInfo.Arguments` (string) con quoting
  Win32-correcto via el nuevo helper privado
  `ConvertTo-WmsBrainProcessArgString` (CommandLineToArgvW: comillas para
  args con espacio/tab/comillas, escape `\"` para comillas internas y
  duplicado de backslashes contra cierre de quote). Esto restaura
  compatibilidad real PS 5.1 + 7 declarada en el manifest. Tests Pester
  nuevos en `ProcessRunner.Tests.ps1` (6 sobre el quoting + 4 de regresion
  asegurando que los runners no vuelvan a referenciar `ArgumentList`).

## [0.1.0] - 2026-04-20

### Added
- Esqueleto inicial sin cmdlets implementados, solo manifest y placeholder.
