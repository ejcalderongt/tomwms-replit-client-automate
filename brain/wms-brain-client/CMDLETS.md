# wms-brain-client — Catalogo de cmdlets

> **Status**: Draft v1 (2026-04-27).
> Convencion: PowerShell verbos aprobados (Get-Verb).
> Modulo: `WmsBrainClient`. Alias del modulo: `wmsbc`.

## Tabla resumen

| Cmdlet | Categoria | Lectura/Escritura | Resumen |
|---|---|:---:|---|
| `Initialize-WmsBrain` | Bootstrap | E | Instala todo y configura por primera vez |
| `Set-WmsBrainConfig` | Bootstrap | E | Modifica configuracion (operador, perfiles SQL, token) |
| `Test-WmsBrainConnection` | Bootstrap | L | Verifica acceso a SQL y a brain |
| `Show-WmsBrainStatus` | Bootstrap | L | Resumen del estado actual del cliente |
| `Sync-WmsBrain` | Brain | L+E | git pull / push contra el repo brain |
| `Get-WmsBrainQuestion` | Question | L | Lista question cards (con filtros) |
| `Show-WmsBrainQuestion` | Question | L | Muestra una question en detalle |
| `Invoke-WmsBrainQuestion` | Question | L | Ejecuta queries asociadas y prepara draft |
| `Submit-WmsBrainAnswer` | Question | E | Confirma y commitea answer al repo |
| `Get-WmsBrainScenario` | Scenario | L | Lista escenarios disponibles |
| `Show-WmsBrainScenario` | Scenario | L | Detalle de un escenario |
| `Send-WmsBrainSeed` | Scenario | E | Aplica setup.sql del escenario al SQL local (con safety) |
| `Test-WmsBrainScenario` | Scenario | L | Valida que el seed funciono (corre expectations.sql) |
| `Remove-WmsBrainSeed` | Scenario | E | Aplica teardown.sql del escenario |
| `Get-WmsBrainAnalysisSuite` | Analysis | L | Lista suites de diagnostico |
| `Invoke-WmsBrainAnalysis` | Analysis | L | Corre suite contra perfil SQL |
| `Compare-WmsBrainSchema` | Schema | L | Diff de schema vs baseline en repo |
| `Save-WmsBrainSchemaSnapshot` | Schema | E | Guarda snapshot al repo (solo si hay diff) |
| `Submit-WmsBrainLearning` | Learning | E | Modo libre — submit hallazgo manual |
| `Show-WmsBrainHistory` | History | L | Historial local de operaciones |
| `Undo-WmsBrainOperation` | History | E | Rollback de una operacion previa |
| `Start-WmsBrainInteractive` | UI | L+E | Lanza el menu REPL |

## Detalle por cmdlet

### `Initialize-WmsBrain`

```powershell
Initialize-WmsBrain
  [-RepoPath <string>]              # default: $HOME\src\wms-brain
  [-Operator <string>]              # slug, ej. ejc
  [-SkipDependencyInstall]          # si ya tenes todo, salta instalaciones
  [-NonInteractive]                 # falla en vez de preguntar (para automation)
```

Idempotente: se puede correr varias veces, valida cada paso.

Salida: tabla con cada paso del bootstrap y su status.

### `Set-WmsBrainConfig`

```powershell
Set-WmsBrainConfig
  [-Slug <string>]                  # cambia operator slug
  [-Email <string>]
  [-AddSqlProfile <hashtable>]      # @{Name='K7-PRD';Server='...';Database='...';Auth='sql';Codename='K7'}
  [-RemoveSqlProfile <string>]
  [-DefaultSqlProfile <string>]
  [-RefreshGitHubToken]
  [-AddAlias <string> -RealName <string>]   # alias K7 -> 'Killios' (NO se commitea)
  [-Show]                           # imprime config actual (sin secrets)
```

### `Test-WmsBrainConnection`

```powershell
Test-WmsBrainConnection
  [-Profile <string>]               # default: profile activo
  [-Brain]                          # incluye test contra GitHub
  [-Verbose]
```

Salida:

```
[OK]   SQL profile K7-PRD     server=<SERVER> db=TOMWMS_KILLIOS_PRD   latency=23ms
[OK]   GitHub auth                                                     scopes=repo
[OK]   Repo wms-brain                                                  branch=wms-brain HEAD=233a07d2
[OK]   Aliases local                                                   12 entradas
```

### `Show-WmsBrainStatus`

Imprime banner con: version del cliente, slug operador, perfil SQL activo,
ultimo sync, pending count, last operation.

### `Sync-WmsBrain`

```powershell
Sync-WmsBrain
  [-PullOnly]
  [-PushOnly]
  [-Force]                          # ignora dirty working tree (no recomendado)
```

Hace `git fetch origin wms-brain`, fast-forward si limpio, si hay drafts
locales pendientes los commitea (con confirmacion) y `git push`.

### `Get-WmsBrainQuestion`

```powershell
Get-WmsBrainQuestion
  [-Status <string>]                # pending | in-progress | answered | closed (default: pending)
  [-Codename <string>]              # filtra por target (K7, BB, ...)
  [-Tag <string[]>]
  [-Priority <string>]              # low | medium | high | critical
  [-Mine]                           # solo las que vos respondiste/abriste
```

Salida tabla:

```
Id      Title                                     Priority  Tags                  Targets
------  ----------------------------------------  --------  --------------------  ----------
Q-007   Cadencia real de NavSync en BB            medium    outbox,navsync,BB     BB-PRD
Q-008   Decimales SAP en cantidades               high      sap,decimales,K7      K7-PRD
Q-009   INGRESOS pendientes en BB outbox          critical  outbox,bug,BB         BB-PRD
```

### `Show-WmsBrainQuestion`

```powershell
Show-WmsBrainQuestion -Id <string>
  [-NoQueries]                      # esconde el SQL sugerido
  [-Markdown]                       # imprime en MD raw
```

### `Invoke-WmsBrainQuestion`

```powershell
Invoke-WmsBrainQuestion
  -Id <string>
  [-Profile <string>]               # default: el target principal de la question
  [-OnlyQueries <string[]>]         # subset de queries (default: todas)
  [-MaxRows <int>]                  # default: 10000
  [-DryRun]                         # NO ejecuta, solo imprime SQL y plan
  [-OpenDraft]                      # abre el draft en editor configurado
```

Salida:
- ejecuta queries con `Invoke-Sqlcmd` o equivalente.
- guarda resultados en `%APPDATA%\WmsBrainClient\drafts\Q-007\results\`.
- genera draft de answer card en `%APPDATA%\WmsBrainClient\drafts\Q-007\answer.md`.
- imprime tabla resumen + sugiere `Submit-WmsBrainAnswer -Id Q-007`.

### `Submit-WmsBrainAnswer`

```powershell
Submit-WmsBrainAnswer
  -Id <string>
  [-Verdict <string>]               # confirmed | partial | inconclusive | rejected | error
  [-Confidence <string>]            # low | medium | high
  [-EditNotes]                      # abre editor para escribir freeform notes
  [-NoCommit]                       # solo mueve a learnings/answered/, no commitea
  [-Force]                          # skipea confirmacion final
```

Flow:
1. Lee draft.
2. Pide verdict + confidence si no se pasaron.
3. Abre editor para freeform notes (default).
4. Sanitiza (codenames, hashes, etc).
5. Mueve answer card a path final.
6. Commitea con mensaje convencional.
7. `git push`.

### `Get-WmsBrainScenario` / `Show-WmsBrainScenario`

Scenarios viven en `scenarios/` del repo brain. Ejemplo:

```
Get-WmsBrainScenario

Name                              Description
--------------------------------  --------------------------------------------------
doble-despacho                    Reproduce caso BUG-001 con 2 viajes parciales
bug-p16b-race-condition           Reproduce race condition entre transferencia y entrega SAP
stock-rezervado-fantasma          Reserva sin pedido asociado
```

### `Send-WmsBrainSeed`

```powershell
Send-WmsBrainSeed
  -Scenario <string>
  -Profile <string>                 # SOLO perfiles con codename != *-PRD por safety
  [-Force]                          # bypassa restriccion de PRD (loggea y exige confirmacion doble)
  [-DryRun]
  [-OpId <string>]                  # asignar id de operacion (auto si no se pasa)
```

Flow:
1. Valida que el perfil NO sea PRD (a menos que `-Force`).
2. Snapshot de las tablas que el setup.sql va a tocar.
3. Pide confirmacion (`Y/N`).
4. Ejecuta setup.sql en transaccion.
5. Loggea en historial con OpId.
6. Imprime `Test-WmsBrainScenario -Scenario X` para validar.
7. Imprime `Undo-WmsBrainOperation -Id <OpId>` para rollback.

### `Test-WmsBrainScenario`

Read-only. Corre `expectations.sql` y reporta pass/fail por aserto.

### `Remove-WmsBrainSeed`

Wrapper de `Undo-WmsBrainOperation` para el caso de seed.

### `Get-WmsBrainAnalysisSuite` / `Invoke-WmsBrainAnalysis`

```powershell
Get-WmsBrainAnalysisSuite

Name                       Queries  Description
-------------------------  -------  --------------------------------------------------
outbox-health              8        Diagnostica i_nav_transacciones_out
state-machine-pedido       12       Verifica integridad de estados de pedido
stock-integrity            6        Stock fisico vs reservado vs comprometido
bug-p16b-detector          5        Encuentra pedidos despachados con estado != Despachado

Invoke-WmsBrainAnalysis
  -Suite <string>
  -Profile <string>
  [-OutputJson <path>]
  [-OutputMarkdown <path>]
  [-OnlyQueries <string[]>]
  [-Verbose]
```

Salida: tabla resumen con cada query y status. Output detallado en archivos.

### `Compare-WmsBrainSchema`

```powershell
Compare-WmsBrainSchema
  -Profile <string>
  [-Against <string>]               # path del baseline (default: ultimo snapshot del codename)
  [-Format <string>]                # diff | summary | full
```

### `Save-WmsBrainSchemaSnapshot`

```powershell
Save-WmsBrainSchemaSnapshot
  -Profile <string>
  [-Force]                          # commitea aun si no hay diff
  [-Tag <string>]                   # tag opcional (ej. "post-migracion-v2.3")
```

Flow: extrae DDL via `mssql-scripter` o equivalente, compara con ultimo
snapshot, si hay diff genera archivo y commitea.

### `Submit-WmsBrainLearning`

```powershell
Submit-WmsBrainLearning
  [-Title <string>]
  [-Tags <string[]>]
  [-Profile <string>]               # opcional, da contexto codename
  [-Verdict <string>]               # new-finding | refinement | correction | question
  [-OpenEditor]                     # abre editor para escribir el cuerpo
  [-FromDraft <path>]               # toma cuerpo de un MD existente
```

### `Show-WmsBrainHistory`

```powershell
Show-WmsBrainHistory
  [-Last <int>]                     # default: 20
  [-Cmdlet <string>]                # filtra por cmdlet
  [-Operation <string>]             # filtra por OpId
  [-Format <string>]                # table | json | detailed
```

### `Undo-WmsBrainOperation`

```powershell
Undo-WmsBrainOperation
  -Id <string>                      # OpId de la operacion a revertir
  [-Force]                          # skip confirmacion
  [-DryRun]
```

Flow: lee snapshot pre-operacion, valida que las tablas no tienen otra
operacion encima sin reconciliar, restaura, loggea undo en history.

### `Start-WmsBrainInteractive`

Lanza el menu REPL descrito en SPEC seccion 5.2.

```powershell
Start-WmsBrainInteractive
  [-NoBanner]
  [-Profile <string>]
```

Alias: `wmsbc` (sin parametros).

## Verbos aprobados utilizados

Get, Show, Set, Initialize, Test, Sync, Invoke, Submit, Send, Remove,
Compare, Save, Start, Undo. Todos en `Get-Verb`.

## Convenciones

- TODO cmdlet de escritura soporta `-WhatIf` y `-Confirm` automaticamente
  via `[CmdletBinding(SupportsShouldProcess=$true,ConfirmImpact='High')]`.
- TODO cmdlet retorna PSCustomObject pipeable (no print-only).
- TODO cmdlet de escritura genera entrada en historial.
- Verbose y Debug streams se respetan en todos los cmdlets.
- Errores se emiten via `Write-Error` (terminating con `-ErrorAction Stop`).
