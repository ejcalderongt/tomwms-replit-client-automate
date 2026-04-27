# Changelog — WmsBrainClient

Todos los cambios relevantes del modulo PowerShell `WmsBrainClient`.
Formato: [Keep a Changelog](https://keepachangelog.com/es-ES/1.1.0/),
versionado semantico.

## [Unreleased]

### Added
- **Cmdlet nuevo `Initialize-WmsBrainConfig`** — wizard interactivo que
  escribe un config local en `$env:USERPROFILE\.wmsbrain\config.json`
  con los defaults del operador para conectar a BD y explorar el brain.
  Pregunta paso a paso por: profile productivo (K7-PRD / BB-PRD /
  C9-QAS), paths a los 3 repos orphan (auto-detecta
  `C:\Tools\tomwms-replit-client-*`), credenciales SQL (host, user,
  opcionalmente password), y BRAIN_BASE_URL. Switches:
  * `-IncludePassword` — pregunta el password de SQL y lo persiste en
    el config **cifrado con DPAPI** (`ConvertFrom-SecureString`, scope
    CurrentUser+Machine — solo descifrable por el mismo usuario en la
    misma maquina). Sin este switch el config queda sin password.
  * `-SetEnv` — al final persiste env vars User scope para que los
    demas cmdlets las lean (sin password como env var: si esta cifrado
    en el config, hidrata `$env:WMS_KILLIOS_DB_PASSWORD` solo para esta
    sesion).
  * `-NonInteractive` — sin prompts, usa defaults; util para CI o
    bootstraping silencioso (combinar con `-Force` y `-SetEnv`).
  * `-Force` — sobreescribe config existente sin confirmacion.
  * `-Path` — destino alternativo (default `~/.wmsbrain/config.json`).

  Devuelve un `PSCustomObject` con `Path`, `Created`, `Config`,
  `AppliedEnvVars`. Si existe config previo, sus valores se usan como
  defaults del wizard (solo respondes lo que querés cambiar).

- **Banner ASCII + tagline rotativos** en `Show-WmsBrainQuickStart` y
  `Initialize-WmsBrainConfig`. La figura ASCII rota por dia del año
  (7 figuras: cerebro, racks de warehouse, gauge OK/OK, terminal HH,
  robot, signal bars, pipeline `MAIN<->BRAIN<->CLIENT`); la frase rota
  por minuto del dia (12 taglines operativos). Determinista pero
  variable, sin emojis.

- **Helper privado `_Config.ps1`** con funciones nuevas:
  * `Get-WmsBrainConfigPath` — resuelve la ruta del config local.
  * `Get-WmsBrainLocalConfig` — lee y parsea el JSON; devuelve `$null`
    si no existe.
  * `Save-WmsBrainLocalConfig` — escribe el config (`-Force` para
    sobreescribir).
  * `ConvertTo-WmsBrainEncryptedString` / `ConvertFrom-WmsBrainEncryptedString`
    — wrappers DPAPI sobre `ConvertFrom-SecureString` /
    `ConvertTo-SecureString`.
  * `Get-WmsBrainAsciiArt` / `Get-WmsBrainTagline` — devuelven la
    figura/frase del momento (parametro `-Index` opcional para forzar
    una eleccion en tests).

  Sube `FunctionsToExport` de 24 a 25 funciones.

- **Cmdlet nuevo `Show-WmsBrainQuickStart`** — guia interactiva de inicio
  rapido. Imprime un dashboard one-shot con: header (version del modulo,
  schema esperado vs detectado), pre-flight (resumen de
  `Test-WmsBrainEnvironment`), tabla de variables de entorno relevantes
  con su estado (OK / MISSING / SET con redaccion para secretos),
  proximos pasos sugeridos en orden segun lo que falte, y 4-5 ejemplos
  copy-paste para empezar a usar el modulo. Switches:
  * `-SetMissing` — abre prompts `Read-Host` para cada variable critica
    faltante y la setea para la sesion (los secretos via
    `-AsSecureString`).
  * `-Persist` — junto con `-SetMissing`, ademas persiste cada var en
    el scope `User`. Las variables marcadas como secretas (DB password,
    tokens) NUNCA se persisten en disco aunque se pase `-Persist`,
    quedan solo para la sesion actual.
  * `-Compact` — solo header + proximos pasos, omite pre-flight,
    variables y ejemplos. Util cuando ya conoces el modulo.

  Devuelve un `PSCustomObject` con `ModuleVersion`,
  `ExpectedSchemaVersion`, `DetectedSchemaVersion`, `EnvSummary`
  (hashtable nombre->estado) y `NextSteps` (array de strings sugeridos),
  asi puede consumirse desde scripts. El catalogo de variables relevantes
  esta declarado al inicio de la funcion y es facil de extender.

  Tambien se enriquecio el bloque `Get-Help` (`.EXAMPLE`, `.OUTPUTS`,
  `.LINK`) de los cmdlets de mayor uso al arrancar:
  `Show-WmsBrainStatus`, `Test-WmsBrainEnvironment`,
  `New-WmsBrainQuestionEvent`, `Submit-WmsBrainAnswer`.

  Sube `FunctionsToExport` de 23 a 24 funciones.

### Changed
- `scripts/install.ps1` ahora **pinea** las dependencias `powershell-yaml`
  (`0.4.7`) y `SqlServer` (`22.3.0`) via `Install-Module -RequiredVersion`,
  para que dos personas que corran el instalador en dias distintos terminen
  con el mismo build (misma idea que el pin de Pester en CI). Las versiones
  viven en `$PinnedDependencyVersions` arriba del bloque de dependencias y
  estan documentadas en `README.md` ("Versiones pinneadas"), junto con las
  instrucciones para bumpearlas.
- **Bridge bump a `SCHEMA_VERSION="2"`** en `scripts/brain_bridge.mjs`
  (rama `main` del exchange, Task #21, 2026-04-27). El bridge ahora
  procesa end-to-end los tipos `question_request` / `question_answer` /
  `learning_proposed` y el status terminal `answered`, con dispatcher
  `analyze` por tipo y side-effect que flipea automaticamente el
  `question_request` referenciado a `status="answered"` cuando se
  procesa su `question_answer`. Validacion en `notify` que rechaza
  `schema_version="1"` con tipos v2-only. Cobertura end-to-end en
  `scripts/test_brain_bridge_v2.mjs`. Consecuencia operativa: el cliente
  PowerShell detecta `SCHEMA_VERSION="2"` via
  `Get-WmsBrainBridgeSchemaVersion` y emite los tipos nativos sin
  override; el workaround `directive`+`tags` documentado en
  `docs/PROTOCOL.md` §5 quedo marcado como **OBSOLETO**, y la propuesta
  formal `docs/EXTENSION-V2-PROPOSAL.md` §7 quedo cerrada en su
  totalidad. Los eventos schema v1 siguen siendo validos (cambio
  aditivo, sin migracion de eventos viejos).

### Added
- Soporte nativo para schema v2 del bridge en los **cuatro** cmdlets de
  eventos extendidos (Tasks #13 + #20). Cuando el bridge expone
  `SCHEMA_VERSION="2"` se emite:
  * `type=question_request` (desde `New-WmsBrainQuestionEvent`) con
    `schema_version="2"`, `ref.question_id`, `ref.question_card_path`,
    `ref.rama_repo='wms-brain-client'`, `context.targets[]` y
    `context.expected_outputs[]` (en vez de `directive` +
    `tags=["question",Q-NNN,...]`).
  * `type=question_answer` (desde `Submit-WmsBrainAnswer` y
    `New-WmsBrainAnswerEvent`) con `schema_version="2"`,
    `status="answered"` (terminal), `ref.answers_question_id`,
    `ref.answer_card_path`, `context.verdict`, `context.confidence` (en
    vez de `directive` + `tags=["answer",A-NNN,Q-NNN]`).
  * `type=learning_proposed` (desde `New-WmsBrainLearningEvent`) con
    `schema_version="2"`, `status="pending"`, `ref.learning_id`,
    `ref.learning_card_path` (si se pasó card),
    `ref.source_question_id` (si se pasó `-SourceQuestionId`) y
    `context.scope` (en vez de `directive` +
    `tags=["learning",L-NNN,scope,...]`).
- Helper privado `Get-WmsBrainEffectiveSchemaVersion` que resuelve la
  versión efectiva en este orden: `-LegacyDirective` → fuerza `'1'`;
  `$env:WMS_BRAIN_FORCE_V1` truthy (`1`/`true`/`yes`/`on`,
  case-insensitive) → fuerza `'1'`; lectura de `SCHEMA_VERSION` en
  `$env:WMS_BRAIN_EXCHANGE_REPO_MAIN\scripts\brain_bridge.mjs`; fallback
  `'1'` si no hay bridge accesible. Garantiza que sin bridge confirmado
  el cliente sigue en comportamiento legacy.
- Switch `-LegacyDirective` en los cuatro cmdlets de eventos extendidos
  (`New-WmsBrainQuestionEvent`, `Submit-WmsBrainAnswer`,
  `New-WmsBrainAnswerEvent`, `New-WmsBrainLearningEvent`) para forzar
  emisión legacy v1 puntual sin tener que setear/limpiar la variable de
  entorno global. Los cuatro cmdlets ahora devuelven `SchemaVersion` y
  `EmittedType` en el resultado para auditoría.
- Parámetros nuevos en `New-WmsBrainEvent` necesarios para sostener v2
  sin romper consumidores v1: `-SchemaVersion '1'|'2'` (default `'1'`),
  `-Status` (default `'pending'`, habilita `'answered'`), `-RefExtra` y
  `-ContextExtra` (hashtables que se mergean sobre el shape base).
  La `[ValidateSet]` de `-Type` se extendió con
  `question_request`/`question_answer`/`learning_proposed`; la validación
  cruzada (type vs schema_version) la sigue haciendo
  `Test-WmsBrainEventShape`.
- Tests Pester nuevos/extendidos en `tests/Pester/SchemaV2.Tests.ps1`
  cubriendo: helper de detección (5 ramas), `New-WmsBrainEvent` con v2
  (incluyendo rechazo de combinaciones inválidas),
  `New-WmsBrainQuestionEvent` rama v1 default + rama v2 con bridge
  mockeado + override `-LegacyDirective` + override `WMS_BRAIN_FORCE_V1`,
  `Submit-WmsBrainAnswer` rama v1 default + rama v2 con bridge mockeado
  + override `-LegacyDirective`, `New-WmsBrainAnswerEvent` rama v1
  default + rama v2 (`question_answer`/`status="answered"`) + override
  `-LegacyDirective` + override `WMS_BRAIN_FORCE_V1`, y
  `New-WmsBrainLearningEvent` rama v1 default + rama v2 con card +
  `-SourceQuestionId` + rama v2 minimal sin card/source + override
  `-LegacyDirective` + override `WMS_BRAIN_FORCE_V1` (Task #20).
- `tests/Pester/AnswerLearningEvent.Tests.ps1` ahora snapshotea y limpia
  `WMS_BRAIN_FORCE_V1` y `WMS_BRAIN_EXCHANGE_REPO_MAIN` en el setup,
  para que las aserciones legacy v1 no queden flakeando si el host del
  runner ya tuviera esas variables apuntando a un bridge v2.

### Changed
- `docs/EXTENSION-V2-PROPOSAL.md` actualiza §8 documentando que el
  cliente PowerShell ya implementa la emisión v2 en los cuatro cmdlets
  de eventos extendidos (Tasks #13 + #20). §8.5 marca como migrados
  `New-WmsBrainAnswerEvent` y `New-WmsBrainLearningEvent`; resta el bump
  del bridge en `scripts/brain_bridge.mjs` rama `main` para que tome
  efecto en producción y limpiar el workaround del PROTOCOL.md §5.

### Fixed
- `Show-WmsBrainQuickStart` ya no falla con
  `ParameterArgumentValidationErrorEmptyStringNotAllowed` al pasar
  arrays con strings vacios (`''`) a `Write-WmsBrainBanner -Lines`.
  Reemplazados los 11 separadores visuales por un solo espacio (`' '`)
  asi nunca disparan la validacion del helper. Visualmente identico.

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
