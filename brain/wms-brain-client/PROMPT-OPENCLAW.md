# Prompt para Open Claw (Claude Desktop / Claude Code) — Implementacion de wms-brain-client

> **Como usar**: copiar TODO el bloque de abajo y pegarlo como primer
> mensaje en una sesion nueva de Claude Desktop o Claude Code, con el
> filesystem apuntando al folder donde vas a desarrollar el modulo
> PowerShell. Si Open Claw te lo pide, autoriza acceso al filesystem y
> a internet (necesita clonar el repo brain).
>
> **Modelo recomendado**: Claude Sonnet 4.5 o superior (necesita razonar
> sobre arquitectura, no solo codear).
>
> **Tiempo estimado de la primera implementacion (MVP)**: 3-6 horas
> con Claude trabajando en modo agentic.

---

## ===== INICIO DEL PROMPT (copiar todo lo de abajo) =====

Hola Claude. Soy Erik Calderon (slug: `ejc`), desarrollador VB.NET de
PrograX24 a cargo del producto **TOMWMS** (Brain WMS dual-head: BackOffice
.NET + Handheld Android). Voy a pedirte que implementes un cliente
PowerShell llamado **`WmsBrainClient`** segun una especificacion que ya
diseñamos. Vas a trabajar en mi maquina Windows con MCP/filesystem
access.

## Quien sos en este proyecto

Sos el implementador. Tu trabajo es:
1. Leer la spec completa que esta en mi repo brain (te paso link abajo).
2. Generar el modulo PowerShell completo segun la spec.
3. Probarlo contra mi SQL local.
4. Iterar hasta que el MVP funcione end-to-end.

NO sos el arquitecto. La spec ya esta cerrada. Si encontras una
ambiguedad o un error, **preguntame antes de improvisar**.

## Stack tecnologico (NO negociable)

- PowerShell 7+ (LTS), modulo nativo `WmsBrainClient.psd1`.
- `SqlServer` module de Microsoft (`Invoke-Sqlcmd`).
- GitHub CLI (`gh`) para auth, `git.exe` para operaciones.
- Almacenamiento local en `%APPDATA%\WmsBrainClient\`.
- NO usar .NET console aparte. NO usar C# aparte. TODO en PowerShell puro.
- NO usar ORM. NO usar migraciones. NO usar Drizzle, EF, ni nada similar.
  El cliente solo ejecuta SQL plano via Invoke-Sqlcmd.

## Donde esta la spec

Repo: `https://github.com/ejcalderongt/tomwms-replit-client-automate`
Rama: `wms-brain`
Carpeta: `brain/wms-brain-client/`

Antes de escribir UNA SOLA LINEA de codigo, lee y resume internamente
estos 5 archivos (en este orden):

1. `brain/wms-brain-client/SPEC.md` — arquitectura, principios, bootstrap, identidad.
2. `brain/wms-brain-client/ALIASES.md` — mapeo cliente <-> codename + politica de seguridad.
3. `brain/wms-brain-client/PROTOCOL.md` — formato exacto de question/answer/learning cards.
4. `brain/wms-brain-client/CMDLETS.md` — catalogo completo de cmdlets a implementar.
5. `brain/wms-brain-client/examples/Q-001-cadencia-navsync.md` — ejemplo de question card real.

Tambien lee (para entender el contexto WMS):

- `brain/wms-specific-process-flow/state-machine-pedido.md` — modelo de dominio del WMS.
- `brain/wms-specific-process-flow/interfaces-erp-por-cliente.md` — modalidades de integracion.
- `brain/wms-specific-process-flow/bug-report-p16b.md` — caso real que sirve para tests.

Despues de leer todo, hazme un resumen de **5-10 lineas** confirmando
que entendiste:
- Que es el modulo y su proposito.
- La arquitectura general (cliente local + repo brain + flujo de cards).
- Los principios de safety (read-only default, confirmacion, snapshots, rollback).
- El catalogo de cmdlets que vas a implementar.
- Tu plan de fases (MVP vs roadmap).

NO empieces a codear hasta que yo apruebe tu resumen.

## Mi configuracion local

- Windows 11 Pro.
- PowerShell 7.4.x ya instalado.
- SQL Server local en `localhost\SQLEXPRESS`, base `TOMWMS_KILLIOS_LOCAL_DEV`
  (replica de schema productivo, **datos sinteticos**).
- Tengo Git, GitHub CLI y Visual Studio 2022.
- PAT de GitHub almacenado en credentials manager (te indico cuando lo
  necesites).
- VS Code y Notepad++ disponibles como editores externos.

## Plan de fases que te pido

### Fase 0 — Bootstrap del proyecto (30 min)
- Crear estructura de carpetas del modulo segun SPEC seccion 3.2.
- Generar `WmsBrainClient.psd1` con manifest minimal (Version 0.1.0,
  Author=ejc, exports vacios).
- Generar `WmsBrainClient.psm1` con dot-source de Public/Private (vacios).
- Hacer Pester install y crear primer test smoke (Import-Module funciona).
- Inicializar git local con `.gitignore` razonable para PowerShell.

### Fase 1 — Bootstrap maximalista (1 hora)
Implementar PRIMERO:
- `Initialize-WmsBrain` (sin instalar realmente lo que no falta — verifica
  primero, instala solo si necesario).
- `Set-WmsBrainConfig` (completo).
- `Test-WmsBrainConnection` (SQL + GitHub).
- `Show-WmsBrainStatus`.
- `Sync-WmsBrain` (PullOnly como primer paso).

Test end-to-end: correr `Initialize-WmsBrain` desde cero, configurar,
sincronizar, mostrar status. Sin tocar question cards todavia.

### Fase 2 — Question/Answer flow (1.5 horas)
- `Get-WmsBrainQuestion`.
- `Show-WmsBrainQuestion`.
- `Invoke-WmsBrainQuestion` (read-only, SOLO `SELECT`).
- `Submit-WmsBrainAnswer` (sanitizacion + commit + push).

Test end-to-end: correr el ejemplo Q-001 de punta a punta. Yo te paso
el SQL local con datos para probar.

### Fase 3 — Modo interactivo (45 min)
- `Start-WmsBrainInteractive` con menu basico.
- Alias `wmsbc`.
- Loop de menu, lectura de input, llamada a cmdlets de Fase 1 y 2.
- Output con `Format-Table`, colores, paginacion.

### Fase 4 — Analysis suites (1 hora)
- `Get-WmsBrainAnalysisSuite`, `Invoke-WmsBrainAnalysis`.
- Necesito que crees al menos 1 suite real en el repo brain
  (`suites/outbox-health/`) basada en mi script
  `/tmp/dbq/scripts/14-outbox-marca-envio.cjs` (preguntale a Erik si
  necesitas el contenido).

### Fase 5 — Safety (1 hora)
- `Show-WmsBrainHistory`, `Undo-WmsBrainOperation`.
- Modulo Private de snapshots (BACKUP de tablas).
- Modulo Private de logging estructurado JSON.
- Confirmacion interactiva con `ShouldProcess`.

### Roadmap pos-MVP (no en este sprint)
- Scenarios y `Send-WmsBrainSeed`.
- `Compare-WmsBrainSchema` y `Save-WmsBrainSchemaSnapshot`.
- `Submit-WmsBrainLearning` modo libre.
- Modo headless para servers cliente.

## Reglas de oro

1. **No improvisar la spec**. Si encontras algo no cubierto, preguntame.
2. **No cambiar el formato de cards**. PROTOCOL.md es ley.
3. **No hacer SQL de escritura** en MVP. Toda Fase 1-4 es read-only.
4. **Testear cada cmdlet con Pester** antes de pasar al siguiente.
5. **Loggear en `%APPDATA%\WmsBrainClient\logs\` cada operacion**.
6. **Usar verbos PowerShell aprobados** (Get-Verb). Si dudas, preguntame.
7. **Sanitizar antes de commit**: codenames, IPs, usuarios SQL — todo
   reemplazado por placeholders.
8. **No subir secrets al repo brain**. Token GitHub, password SQL,
   `aliases.local.json` — NUNCA al repo.
9. **Pedir confirmacion** en TODA operacion que escribe (incluyendo el
   commit de answer cards — siempre ofrece preview).
10. **Documentar** cada cmdlet con comment-based help PowerShell estandar
    (`<# .SYNOPSIS .DESCRIPTION .PARAMETER .EXAMPLE #>`).

## Como me reportas progreso

Despues de cada fase:
- Resumen de lo implementado (3-5 bullets).
- Tests Pester corridos y resultado.
- Demo de un cmdlet (output real, no fake).
- Que sigue (la siguiente fase).
- Bloqueos o preguntas para mi.

## Ambiente que necesito que crees

```
C:\Users\ejc\src\WmsBrainClient\         # tu proyecto (este es el modulo)
C:\Users\ejc\src\wms-brain\              # repo brain clonado (ya existe o lo clonas)
%APPDATA%\WmsBrainClient\                # storage del cliente cuando se ejecute
  config.json
  aliases.local.json
  history\
  snapshots\
  logs\
  drafts\
```

## Como pruebo lo que vas implementando

Tengo SQL Server local con copia parcial de Killios productivo (datos
sinteticos donde hay PII). Te paso connection string cuando llegues a
Fase 1. Tambien te paso un PAT GitHub con scope `repo` cuando llegues a
Fase 1.

## Que NO espero de vos en este sprint

- NO esperes que generes el codigo de las **interfaces .vbproj** del WMS
  (NavSync, SAPSYNC*). Eso es harina de otro costal y vive en otro repo.
- NO esperes que repliques el BackOffice .NET. El cliente es solo
  herramienta de exploracion.
- NO esperes deploy a servidores cliente todavia. Eso es post-MVP.

## Pregunta inicial

Antes de arrancar Fase 0, hazme estas 5 preguntas (una a la vez) para
asegurarte de tener todo:

1. ¿Tienes acceso al repo brain (PAT de GitHub) o lo configuro yo
   manualmente?
2. ¿Confirmo que prefieres SQL Server con Windows Auth para el perfil
   `LOCAL_DEV` o necesitas SQL Auth?
3. ¿Que editor uso para abrir drafts (`code`, `notepad++`, `notepad`)?
4. ¿Quieres que el modulo soporte PS 5.1 (Windows PowerShell legado)
   ademas de PS 7+, o solo PS 7+?
5. ¿Quieres que use Pester v5 (moderno) o Pester v3 (legado)?

Cuando me respondas, arranca Fase 0.

## ===== FIN DEL PROMPT =====

---

## Notas adicionales para Erik (quien usa este prompt)

- **Antes de pegar el prompt**, asegurate de haber subido al repo brain
  los 5 docs (SPEC, ALIASES, PROTOCOL, CMDLETS, ejemplo Q-001) y este
  PROMPT-OPENCLAW.md mismo, para que Open Claw los pueda leer.
- **Configura MCP de filesystem** con acceso a `C:\Users\ejc\src\` antes
  de pegar el prompt.
- **Configura MCP de GitHub** o credentials helper para que Open Claw
  pueda hacer git operations.
- Si Open Claw se desvia de la spec, recordale leer otra vez el SPEC.md
  con un mensaje corto: "Lee `brain/wms-brain-client/SPEC.md` seccion X
  y ajusta tu propuesta".
- Si Open Claw quiere usar otro stack (.NET, Node), pegale el bloque de
  "Stack tecnologico" arriba — es no-negociable.
