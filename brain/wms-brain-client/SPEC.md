# wms-brain-client — Especificacion de arquitectura

> **Status**: Draft v1 (2026-04-27).
> **Owner**: ejc (Erik Calderon).
> **Implementacion**: PowerShell modulo `WmsBrainClient` (Windows / Windows Server).
> **Repo brain**: https://github.com/ejcalderongt/tomwms-replit-client-automate (rama `wms-brain`).

## 1. Vision

`wms-brain-client` es la **navaja suiza local** que cada developer del
equipo PrograX24 (y en el futuro, instalable en servidores de clientes)
ejecuta sobre su instancia SQL Server local o productiva para:

1. **Aprender** del WMS real (estructuras, datos, anomalias) y depositar
   los aprendizajes en el repo `wms-brain` de manera estructurada y versionada.
2. **Reproducir escenarios** complejos via seed data (ej. el bug
   "Despachado vuelve a Pendiente") sin necesidad de provocarlos a mano.
3. **Diagnosticar** una BD desconocida con suites predefinidas (ej.
   "outbox-health", "state-machine-pedido", "stock-integrity").
4. **Responder preguntas** que el brain o cualquier consumidor publique
   (modelo "question card" / "answer card" con confianza criptografica
   ligera basada en commits firmados).
5. **Mantener historial** de aprendizajes (criticos, monitoreados) y de
   schema (solo cuando hay drift, no continuo).

El cliente NO reemplaza al BackOffice ni al WMS. Es una herramienta de
exploracion, debug y memoria compartida del equipo.

## 2. Principios de diseno

### 2.1 Safety-first

Toda operacion que modifique datos en SQL local cumple las 5 reglas:

1. **Confirmacion explicita** del usuario antes de ejecutar (`-Confirm`
   por default; `-Force` para skip en automation, pero loggea el bypass).
2. **Log estructurado** de la operacion en
   `%APPDATA%\WmsBrainClient\history\<timestamp>.json` con: cmdlet, params,
   queries SQL ejecutadas, filas afectadas, codename del cliente, slug
   del operador, hash de los datos previos.
3. **Snapshot pre-operacion** de las tablas afectadas (BACPAC parcial o
   `SELECT INTO #backup_<opid>`) para permitir rollback.
4. **Capacidad de rollback** via `Undo-WmsBrainOperation -Id <opid>` que
   restaura el snapshot.
5. **Modo dry-run** disponible en TODO cmdlet de escritura
   (`-WhatIf` estilo PowerShell estandar).

### 2.2 Read-only por defecto

`Get-*`, `Show-*`, `Compare-*`, `Test-*`, `Invoke-*Analysis` son SIEMPRE
read-only. Cmdlets de escritura llevan verbo explicito: `Send-`, `Set-`,
`Submit-`, `Initialize-`, `Save-`, `Undo-`.

### 2.3 Confianza dentro del equipo, alias hacia afuera

Los developers del equipo se identifican con su slug real (`ejc`, etc).
Los **clientes** del WMS se identifican SOLO con codenames de 2 caracteres
(K7, BB, C9...). El mapping real esta en
`%APPDATA%\WmsBrainClient\aliases.local.json` y NUNCA se sube al repo.
Ver `ALIASES.md`.

### 2.4 Folders, no ramas

Todo el flujo de aprendizaje vive en `main`. Cada question/answer/learning
es un archivo nuevo en una carpeta. Nada de ramas por sesion.

Excepcion: cambios estructurales del SPEC del cliente sí pueden ir en
rama feature, mergeada manual.

### 2.5 Historial de schema con criterio anti-saturacion

- Snapshot de schema **solo cuando hay diff** detectado por
  `Compare-WmsBrainSchema`.
- Si no hay cambios respecto al ultimo snapshot, NO se commitea nada.
- Manual override: `Save-WmsBrainSchemaSnapshot -Force`.

### 2.6 Historial de aprendizaje con monitoreo activo

- Cada aprendizaje (answer card o submit-learning) genera un archivo en
  `learnings/answered/<slug>/<fecha>/<codename>-<id>-<titulo>.md`.
- El brain (consumidor humano o agente) consolida periodicamente
  multiples answers en docs estables en `brain/wms-specific-process-flow/`.
- Cuando se consolida, la answer card se mueve a `learnings/closed/` con
  un link al doc consolidado.
- Aprendizajes NUNCA se borran (solo se mueven a `closed/` o se anotan
  como "superseded").

### 2.7 Maximalismo en bootstrap, minimalismo en uso diario

- `Initialize-WmsBrain` instala TODO lo necesario sin asumir nada del
  ambiente: chocolatey si falta, .NET 8 si falta, PowerShell 7 si falta,
  modulos `SqlServer` y `PowerShellGet` si faltan, GitHub CLI si falta,
  configura git, clona el repo, pide configuracion inicial.
- Despues del bootstrap, el uso diario es 1 cmdlet a la vez.

## 3. Arquitectura del cliente local

### 3.1 Stack tecnologico

- **Lenguaje**: PowerShell 7+ (LTS).
- **Modulo**: `WmsBrainClient` con manifest `.psd1`.
- **Dependencias PS**: `SqlServer` (oficial de Microsoft, `Invoke-Sqlcmd`,
  `Invoke-SqlcmdReader`).
- **Git**: GitHub CLI (`gh`) o `git.exe` con credentials helper.
- **Storage local**: `%APPDATA%\WmsBrainClient\`.
- **Instalacion**: PowerShell Gallery cuando sea publico, repo Git por ahora.

### 3.2 Estructura del modulo

```
WmsBrainClient/
  WmsBrainClient.psd1                 # manifest (version, exports, deps)
  WmsBrainClient.psm1                 # entry point (dot-source Public/Private)
  Public/                             # cmdlets exportados (1 archivo c/u)
    Initialize-WmsBrain.ps1
    Set-WmsBrainConfig.ps1
    Test-WmsBrainConnection.ps1
    Sync-WmsBrain.ps1
    Get-WmsBrainQuestion.ps1
    Show-WmsBrainQuestion.ps1
    Invoke-WmsBrainQuestion.ps1
    Submit-WmsBrainAnswer.ps1
    Get-WmsBrainScenario.ps1
    Send-WmsBrainSeed.ps1
    Test-WmsBrainScenario.ps1
    Get-WmsBrainAnalysisSuite.ps1
    Invoke-WmsBrainAnalysis.ps1
    Compare-WmsBrainSchema.ps1
    Save-WmsBrainSchemaSnapshot.ps1
    Submit-WmsBrainLearning.ps1
    Show-WmsBrainHistory.ps1
    Undo-WmsBrainOperation.ps1
    Show-WmsBrainStatus.ps1
    Start-WmsBrainInteractive.ps1     # menu interactivo (REPL)
  Private/                            # internas
    Config.ps1                        # load/save config
    SqlExec.ps1                       # wrapper de Invoke-Sqlcmd con safety
    GitOps.ps1                        # pull/commit/push contra wms-brain
    Logging.ps1                       # log estructurado JSON
    Snapshot.ps1                      # backup/restore de tablas
    Confirm.ps1                       # prompts de confirmacion
    Aliases.ps1                       # resolver codename<->cliente
    QuestionCard.ps1                  # parser/serializer de cards
  Resources/                          # plantillas embebidas
    bootstrap/
      install-deps.ps1                # instala chocolatey, .NET, etc
    scenarios/                        # solo nombre, contenido vive en repo brain
    suites/
    templates/
      question-card.template.md
      answer-card.template.md
      learning-card.template.md
  en-US/
    about_WmsBrainClient.help.txt
```

### 3.3 Configuracion local

`%APPDATA%\WmsBrainClient\config.json`:

```json
{
  "version": 1,
  "operator": {
    "slug": "ejc",
    "displayName": "Erik Calderon",
    "email": "ejcalderon@..."
  },
  "github": {
    "repo": "ejcalderongt/tomwms-replit-client-automate",
    "branch": "wms-brain",
    "tokenStore": "windows-credential-manager",
    "tokenName": "wms-brain-pat"
  },
  "sql": {
    "default": {
      "server": "localhost\\SQLEXPRESS",
      "database": "TOMWMS_LOCAL_DEV",
      "auth": "windows",
      "codename": "K7"
    },
    "profiles": {
      "K7-PRD": { "server": "...", "database": "TOMWMS_KILLIOS_PRD", "auth": "sql", "userStore": "..." },
      "BB-PRD": { "server": "...", "database": "IMS4MB_BYB_PRD", "auth": "sql", "userStore": "..." }
    }
  },
  "paths": {
    "repo": "C:\\Users\\ejc\\src\\wms-brain",
    "history": "%APPDATA%\\WmsBrainClient\\history",
    "snapshots": "%APPDATA%\\WmsBrainClient\\snapshots",
    "logs": "%APPDATA%\\WmsBrainClient\\logs"
  },
  "safety": {
    "requireConfirmOnWrite": true,
    "alwaysSnapshot": true,
    "snapshotRetentionDays": 30,
    "logRetentionDays": 365
  },
  "ui": {
    "useColors": true,
    "interactiveDefault": true
  }
}
```

`%APPDATA%\WmsBrainClient\aliases.local.json` (privado, no commitear):

```json
{
  "K7": "Killios",
  "BB": "BYB",
  "C9": "CEALSA"
}
```

### 3.4 Token de GitHub

- Cada developer usa su propio PAT (Personal Access Token) con scope
  `repo`. Por seguridad personal y trazabilidad.
- El token se almacena en **Windows Credential Manager** (no en archivo
  plano). El cliente lo recupera via API de Windows.
- Si el operador prefiere un token compartido del equipo, se documenta
  en `config.json` como `"tokenName": "wms-brain-team-pat"` y se gestiona
  manualmente.

## 4. Arquitectura del repo brain

### 4.1 Estructura ampliada

```
brain/
  wms-specific-process-flow/              # docs consolidados (curated)
    state-machine-pedido.md
    interfaces-erp-por-cliente.md
    bug-report-p16b.md
    preguntas-pasada-7.md
    respuestas-tanda-2.md
    ...
  wms-brain-client/                       # documentacion del cliente
    SPEC.md                               # este archivo
    ALIASES.md
    PROTOCOL.md
    CMDLETS.md
    PROMPT-OPENCLAW.md
    examples/
      Q-001-cadencia-navsync.md           # question card de ejemplo
      A-001-cadencia-navsync-ejc.md       # answer card de ejemplo
learnings/                                # raw learnings (uncurated)
  pending/                                # questions abiertas
    Q-XXX-titulo-corto.md
  answered/                               # respuestas commiteadas por developers
    <slug>/
      <YYYY-MM-DD>/
        <CODENAME>-<Q-ID>-<titulo>.md     # answer card
        <CODENAME>-LEARN-<id>-<titulo>.md # learning libre
  closed/                                 # questions consolidadas
    Q-XXX-titulo-corto.md                 # con front-matter linkando al doc consolidado
schema-snapshots/                         # drift de schema (solo cuando cambia)
  <CODENAME>-<ENV>/
    <YYYY-MM-DD>_<full|diff>.sql
    <YYYY-MM-DD>_summary.md
scenarios/                                # seed data para reproducir bugs
  doble-despacho/
    setup.sql                             # crea las filas necesarias
    expectations.sql                      # queries que validan el setup
    teardown.sql                          # opcional, limpia
    README.md
  bug-p16b-race-condition/
    ...
suites/                                   # suites de queries de diagnostico
  outbox-health/
    queries/                              # cada query un archivo .sql
      O1-cols-outbox.sql
      O2-distribucion-enviado-tipo.sql
      ...
    suite.json                            # metadata: orden, filtros, descripcion
    README.md
  state-machine-pedido/
    ...
  stock-integrity/
    ...
```

### 4.2 Flujo end-to-end

```
┌─ brain (humano o agente) ──────────────────────────────────────┐
│  1. Identifica una pregunta no resuelta                        │
│  2. Crea Q-XXX en learnings/pending/ via PR o commit directo   │
│     (formato definido en PROTOCOL.md)                          │
│  3. (Opcional) define query/script SQL de referencia           │
└─────────────────┬──────────────────────────────────────────────┘
                  │ git push
                  ▼
┌─ wms-brain repo ───────────────────────────────────────────────┐
│  learnings/pending/Q-001.md                                    │
└─────────────────┬──────────────────────────────────────────────┘
                  │ git pull (Sync-WmsBrain)
                  ▼
┌─ wms-brain-client local (Erik) ────────────────────────────────┐
│  4. Get-WmsBrainQuestion lista pendings                        │
│  5. Show-WmsBrainQuestion -Id Q-001 muestra detalle            │
│  6. Invoke-WmsBrainQuestion -Id Q-001 -Profile K7-PRD          │
│     - corre query contra SQL                                   │
│     - captura resultado en memoria                             │
│     - genera answer card draft en %APPDATA%\...\drafts\        │
│  7. Submit-WmsBrainAnswer -Id Q-001                            │
│     - pide confirmacion al operador                            │
│     - mueve answer card a learnings/answered/ejc/<fecha>/      │
│     - commit + push al repo                                    │
└─────────────────┬──────────────────────────────────────────────┘
                  │ git push
                  ▼
┌─ wms-brain repo ───────────────────────────────────────────────┐
│  learnings/answered/ejc/2026-04-27/K7-Q-001-...md              │
└─────────────────┬──────────────────────────────────────────────┘
                  │ humano o agente lee
                  ▼
┌─ brain consolida ──────────────────────────────────────────────┐
│  8. Lee answer cards relacionadas con Q-001                    │
│  9. Sintetiza en doc estable (ej. interfaces-erp-por-cliente.md)│
│ 10. Mueve Q-001 de pending/ a closed/ con front-matter         │
│ 11. (Opcional) crea Q-002 derivada                             │
└────────────────────────────────────────────────────────────────┘
```

## 5. Modos de operacion del cliente

### 5.1 Modo cmdlet (uno a uno)

Para automation, scripts, CI/CD futuros.

```powershell
Sync-WmsBrain
Get-WmsBrainQuestion -Status Pending
Invoke-WmsBrainQuestion -Id Q-001 -Profile K7-PRD
Submit-WmsBrainAnswer -Id Q-001
```

### 5.2 Modo interactivo (REPL)

Para uso diario humano. `Start-WmsBrainInteractive` o alias `wmsbc`.

Menu principal:

```
=================================================================
  WmsBrainClient v1.0  |  ejc @ K7-LOCAL  |  brain: synced (2 pending)
=================================================================
  [1] Sincronizar con brain (pull/push)
  [2] Ver preguntas pendientes (2)
  [3] Ejecutar suite de analisis
  [4] Reproducir escenario (seed)
  [5] Comparar schema vs baseline
  [6] Submit aprendizaje libre
  [7] Ver historial de operaciones
  [8] Configuracion
  [9] Salir
-----------------------------------------------------------------
> 2
```

Flujo guiado paso a paso. Confirmaciones inline. Colores. Spinner para
operaciones largas. Output paginado con `Out-Host -Paging`.

### 5.3 Modo headless (server)

Cuando se instala en server de cliente para diagnostico programado.

```powershell
Invoke-WmsBrainAnalysis -Suite outbox-health -Profile K7-PRD -Force -OutputJson "C:\diag\$(Get-Date -Format yyyyMMdd).json"
```

## 6. Identidad y autenticacion

### 6.1 Identificacion del operador

- Slug se setea en config inicial (`Set-WmsBrainConfig -Slug ejc`).
- Se usa en:
  - Commits: `git config user.name` + email.
  - Paths: `learnings/answered/ejc/...`.
  - Header de cada answer card.

### 6.2 Autenticacion al brain

- PAT GitHub almacenado en Windows Credential Manager.
- Se valida con `gh auth status` o `git ls-remote` antes de operar.
- Refresh: `Set-WmsBrainConfig -RefreshToken`.

### 6.3 Identificacion del cliente operado

- Codename del cliente se setea en cada `Profile` SQL.
- Se valida que el operador conozca el mapping (mediante el archivo local
  `aliases.local.json`).
- En los commits aparece SOLO el codename.

## 7. Bootstrap maximalista

`Initialize-WmsBrain` ejecuta paso a paso (con barra de progreso, sin
bajar la guardia en seguridad):

```
[1/12] Verificando PowerShell 7+...                   OK (7.4.1)
[2/12] Verificando .NET 8 SDK...                      Faltante. Instalando via winget... OK
[3/12] Verificando modulo SqlServer...                Faltante. Install-Module -Name SqlServer -Scope CurrentUser... OK
[4/12] Verificando GitHub CLI...                      OK (2.45.0)
[5/12] Configurando git user.name/user.email...       Pediendo input... OK
[6/12] Configurando GitHub PAT...                     Pediendo input... OK (almacenado en Credential Manager)
[7/12] Probando acceso al repo wms-brain...           OK (autorizado)
[8/12] Eligiendo carpeta para repo local...           Default: C:\Users\ejc\src\wms-brain  [Enter para aceptar]
[9/12] Clonando repo...                               OK (rama wms-brain)
[10/12] Configurando perfil SQL local...              Pediendo server/db/auth... OK (probado)
[11/12] Asignando codename al perfil...               Pediendo codename (ej. K7)... OK
[12/12] Inicializando aliases.local.json...           OK (1 entrada)

[OK] wms-brain-client listo. Iniciando modo interactivo...
```

## 8. Versionado y compatibilidad

- El cliente versiona el modulo con SemVer.
- Cada question/answer/learning card lleva campo `protocolVersion` (entero).
- El cliente rechaza cards de protocolo superior al que conoce y sugiere
  upgrade.

## 9. Roadmap

### v1.0 (MVP)
- Bootstrap maximalista.
- Cmdlets read-only (`Get-*`, `Show-*`, `Invoke-*Analysis`, `Compare-*`).
- Sync con brain.
- Question/Answer flow basico.
- Modo interactivo.

### v1.1
- Cmdlets de escritura con safety (`Send-Seed`, rollback).
- Suites predefinidas (outbox-health, state-machine-pedido).

### v1.2
- Schema snapshots y diff.
- Submit-Learning libre.

### v2.0
- Despliegue en server cliente (modo headless + scheduling).
- API REST opcional para que brain lance jobs remotos firmados.
- Cifrado de respuestas con datos sensibles (subset de tablas).

## 10. Seguridad

- Token GitHub: Windows Credential Manager, NO en config.json.
- SQL: prefer Windows Auth; si SQL Auth, password en Credential Manager.
- Logs: pueden contener datos sensibles. Retencion 365 dias, NO se sube
  al repo.
- Snapshots de tablas: locales, NO se suben al repo.
- Codenames: nunca exponer mapping al repo publico.
- Operaciones de escritura: siempre audit log inmutable + rollback.

## 11. Out of scope

- NO reemplaza al BackOffice (no es UI de operador WMS).
- NO ejecuta migraciones (no toca `db:push`, no tiene Drizzle, no tiene EF).
- NO sincroniza datos entre clientes (cada SQL es soberano).
- NO hace push directo al ERP (NAV/SAP) — eso es responsabilidad de las
  interfaces del WMS (NavSync, SAPSYNC*).
