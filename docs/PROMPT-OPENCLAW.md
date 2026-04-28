# PROMPT-OPENCLAW.md — Implementar `WmsBrainClient` con Open Claw

> **Para qué**: pasarle este prompt a Open Claw / Claude Desktop / Claude
> Code para que implemente el módulo PowerShell `WmsBrainClient`
> respetando el ecosistema brain existente. Está pensado para una
> sesión de **build mode** (no plan mode), arrancando desde cero en una
> máquina Windows con PowerShell 5.1+ o 7+.

---

## Cómo usar este prompt

1. Abrí Open Claw (o Claude Desktop / Code) en una sesión nueva.
2. Tené el repo de exchange clonado en al menos 3 worktrees (ver §0).
3. Pegá el bloque "PROMPT" de la §1 como primer mensaje.
4. Adjuntá: `PROTOCOL.md`, `SPEC.md`, `CMDLETS.md`, `INDEX.md`,
   `EXTENSION-V2-PROPOSAL.md`, `ALIASES.md`, `examples/*`,
   `templates/*`, `questions/Q-001..Q-008` + `MIGRATION-NOTE.md`.
   Adicionalmente, **adjuntá** o asegurate que pueda leer:
   - `scripts/brain_bridge.mjs` (rama `main` del exchange).
   - `scripts/apply_bundle.mjs` (rama `main`).
   - `scripts/hello_sync.mjs` (rama `main`).
   - `brain/BRIDGE.md` (rama `wms-brain` del exchange).
   - `brain/agent-context/AGENTS.md` (rama `wms-brain`).

5. Dejalo correr. Esperá las preguntas finales antes de aceptar el PR.

---

## 0. Pre-requisitos en la máquina

Antes del PROMPT, en la terminal Windows:

```powershell
# Crear los 3 clones (worktrees) del repo de exchange en ramas distintas
$base = 'C:\tomwms'
git clone https://github.com/ejcalderongt/tomwms-replit-client-automate.git "$base\exchange-main"
git -C "$base\exchange-main" checkout main

git clone https://github.com/ejcalderongt/tomwms-replit-client-automate.git "$base\exchange-brain"
git -C "$base\exchange-brain" checkout wms-brain

git clone https://github.com/ejcalderongt/tomwms-replit-client-automate.git "$base\brain-client"
git -C "$base\brain-client" checkout wms-brain-client

# Setear las vars de entorno (sesion actual)
$env:WMS_BRAIN_EXCHANGE_REPO_MAIN  = "$base\exchange-main"
$env:WMS_BRAIN_EXCHANGE_REPO_BRAIN = "$base\exchange-brain"
$env:WMS_BRAIN_CLIENT_REPO         = "$base\brain-client"
$env:WMS_BRAIN_AUTHOR_INIT         = "EJC"
$env:WMS_BRAIN_DEFAULT_PROFILE     = "K7-PRD"
# Heredadas de AGENTS.md:
$env:WMS_KILLIOS_DB_HOST     = "52.41.114.122,1437"
$env:WMS_KILLIOS_DB_USER     = "wmsuser"
$env:WMS_KILLIOS_DB_PASSWORD = "<el mismo que usas en SSMS>"
$env:BRAIN_BASE_URL          = "https://a5b1339e-a6ee-4eb8-bed1-c2bf8103da79-00-2i7pdom0xaba6.janeway.replit.dev/api/brain"
$env:BRAIN_IMPORT_TOKEN      = "<pegar valor del panel Secrets de Replit>"
$env:AZURE_DEVOPS_PAT        = "<solo si vas a clonar repos VS internos>"

# Tener Node 20+
node --version    # debe imprimir v20.x o superior

# Tener SqlServer module (Invoke-Sqlcmd)
Install-Module -Name SqlServer -Scope CurrentUser    # una sola vez
```

---

## 1. PROMPT (pegar literal a Open Claw)

```
Sos Open Claw. Vas a implementar el modulo PowerShell `WmsBrainClient`
v0.2.0 para Erik Calderon (PrograX24, slug `ejc`).

REGLAS DURAS (no negociables):

1. **NO reinventar el bridge**. Existen ya en la rama `main` del repo
   de exchange:
   - `scripts/brain_bridge.mjs` (SCHEMA_VERSION="1", productor/consumidor de eventos).
   - `scripts/apply_bundle.mjs` (aplica bundles, opcional --brain-message).
   - `scripts/hello_sync.mjs` (handshake productor/consumidor).
   - `scripts/brain-up.ps1` (bootstrap del cliente).

   Cada cmdlet PowerShell que toque el bridge **delega via `node ... .mjs ...`**
   y postprocesa la salida. NO duplicas la logica del bridge en PS.

2. **NO ejecutar writes contra K7-PRD / BB-PRD / C9-QAS**. Son las 3
   BDs productivas en `52.41.114.122,1437`. El cmdlet `Invoke-WmsBrainQuery`
   tiene safety SELECT-only; cualquier intento de DML = exit 7.

3. **NO usar ORM / migraciones / db:push** ni nada que genere
   `ALTER TABLE`. SQL plano via `Invoke-Sqlcmd`. Las 3 BDs son productivas
   de clientes reales; cualquier write destruye datos.

4. **NO mezclar HH (Java) con VB en un mismo cambio**. Es contrato del
   proyecto WMS, ver `wms-brain/brain/agent-context/AGENTS.md` §2.

5. **NO loggear `$env:WMS_KILLIOS_DB_PASSWORD` ni `$env:BRAIN_IMPORT_TOKEN`**.
   Pasarlos por referencia (`$env:VAR`), nunca el valor literal.

6. **NO commitear automaticamente**. Erik los revisa y los hace el. El
   modulo escribe drafts; Erik decide.

7. **Espanol rioplatense, sin emojis, en logs y outputs**.

CONTEXTO QUE TENES QUE LEER ANTES DE ESCRIBIR UNA LINEA DE CODIGO:

FASE 0 — LECTURA OBLIGATORIA (en este orden):
  a. `wms-brain-client/INDEX.md`              (panorama de la rama)
  b. `wms-brain-client/SPEC.md`               (identidad, dependencias hard, perfiles SQL)
  c. `wms-brain-client/PROTOCOL.md`           (CRITICO: contrato con el bridge real)
  d. `wms-brain-client/CMDLETS.md`            (catalogo de cmdlets)
  e. `wms-brain-client/EXTENSION-V2-PROPOSAL.md` (que tipos faltan, por que)
  f. `wms-brain-client/ALIASES.md`            (codenames K7/BB/C9/...)
  g. `wms-brain-client/questions/MIGRATION-NOTE.md` (formato de las cards)
  h. `exchange-main/scripts/brain_bridge.mjs` (contrato real del bridge — leelo entero, ~500 lineas)
  i. `exchange-main/scripts/apply_bundle.mjs` (que flags acepta)
  j. `exchange-main/scripts/hello_sync.mjs`   (que devuelve el handshake)
  k. `exchange-brain/brain/BRIDGE.md`         (doctrina del bridge)
  l. `exchange-brain/brain/agent-context/AGENTS.md` (reglas duras del proyecto)

CHECKPOINT FASE 0: cuando termines de leer, devolveme un resumen de:
  - cuantos tipos validos define VALID_TYPES en brain_bridge.mjs
  - cuantos estados validos define VALID_STATUSES
  - cual es la firma exacta de la funcion que valida un evento
  - si hay alguna inconsistencia entre PROTOCOL.md, BRIDGE.md y el .mjs real
  - cuantos cmdlets tiene CMDLETS.md y como se agrupan en categorias

NO sigas a la FASE 1 hasta que confirme tu resumen.

---

FASE 1 — ESTRUCTURA DEL MODULO POWERSHELL:

Estructura de archivos esperada (en `$env:WMS_BRAIN_CLIENT_REPO`):

  src/
    WmsBrainClient.psd1                     (manifest)
    WmsBrainClient.psm1                     (loader: dot-sources Public/Private/*)
    Public/                                 (cmdlets exportados, 1 archivo cada uno)
      Invoke-WmsBrainHello.ps1
      Invoke-WmsBrainBootstrap.ps1
      Test-WmsBrainEnvironment.ps1
      Invoke-WmsBrainApplyBundle.ps1
      Get-WmsBrainBundleHistory.ps1
      New-WmsBrainEvent.ps1
      Invoke-WmsBrainNotify.ps1
      Get-WmsBrainEventQueue.ps1
      Get-WmsBrainEvent.ps1
      Get-WmsBrainConnectionString.ps1
      Invoke-WmsBrainQuery.ps1
      Test-WmsBrainConnection.ps1
      Get-WmsBrainSuiteList.ps1
      Invoke-WmsBrainSuite.ps1
      Invoke-WmsBrainScenario.ps1
      Get-WmsBrainQuestion.ps1
      Show-WmsBrainQuestion.ps1
      Invoke-WmsBrainQuestion.ps1
      New-WmsBrainQuestionEvent.ps1
      Submit-WmsBrainAnswer.ps1
      Show-WmsBrainStatus.ps1
    Private/                                (helpers no exportados)
      _BrainEventSchema.ps1                 (constantes + validador del shape)
      _NodeRunner.ps1                       (wrapper de node con captura stdout/stderr)
      _SqlSafety.ps1                        (regex anti-DML)
      _IdGenerator.ps1                      (YYYYMMDD-HHMM-INIT con anti-colision)
      _GitHelpers.ps1                       (branch check, working tree clean)
      _ProfileResolver.ps1                  (K7-PRD/BB-PRD/C9-QAS/LOCAL_DEV)
      _Logger.ps1                           (formato [WmsBrainClient] [<cmdlet>] [<lvl>])
  tests/
    Pester/                                 (tests con Pester 5)
      Invoke-WmsBrainHello.Tests.ps1
      ...
  scripts/
    install.ps1                             (instala el modulo en CurrentUser)
  README.md                                 (apunta al README.md de la rama)

CHECKPOINT FASE 1: muestrame el manifest (.psd1) y el loader (.psm1)
con la lista de FunctionsToExport derivada del directorio Public/.

---

FASE 2 — IMPLEMENTACION POR GRUPOS (en orden, no saltees):

  Grupo A — Bootstrap y handshake (3 cmdlets):
    Invoke-WmsBrainHello, Invoke-WmsBrainBootstrap, Test-WmsBrainEnvironment.
    Test exhaustivo de Test-WmsBrainEnvironment es CRITICO porque es la
    puerta de entrada al modulo.

  Grupo B — Conexion SQL (3 cmdlets):
    Get-WmsBrainConnectionString, Invoke-WmsBrainQuery, Test-WmsBrainConnection.
    El safety SELECT-only de Invoke-WmsBrainQuery debe tener tests que
    cubran: INSERT, UPDATE, DELETE, MERGE, TRUNCATE, DROP, ALTER, CREATE,
    GRANT, REVOKE, EXEC sp_, EXEC xp_. Todos exit 7.

  Grupo C — Eventos del bridge (4 cmdlets):
    New-WmsBrainEvent, Invoke-WmsBrainNotify, Get-WmsBrainEventQueue,
    Get-WmsBrainEvent.
    Validar que New-WmsBrainEvent emite un .json que pasa por el
    validador interno de brain_bridge.mjs (probarlo invocando el .mjs).

  Grupo D — Bundles (2 cmdlets):
    Invoke-WmsBrainApplyBundle, Get-WmsBrainBundleHistory.
    Mapear todos los flags de apply_bundle.mjs 1:1.

  Grupo E — Question cards (5 cmdlets):
    Get-WmsBrainQuestion, Show-WmsBrainQuestion, Invoke-WmsBrainQuestion,
    New-WmsBrainQuestionEvent, Submit-WmsBrainAnswer.
    Aca se implementa el WORKAROUND del PROTOCOL.md §5 (emitir
    `directive` con tags=["question","Q-NNN"] mientras schema_version="2"
    no este aprobado).

  Grupo F — Suites y scenarios (3 cmdlets):
    Get-WmsBrainSuiteList, Invoke-WmsBrainSuite, Invoke-WmsBrainScenario.
    Recordar: scenarios son READ-ONLY en este cliente, no aplican
    setup.sql con writes.

  Grupo G — UX y status (1 cmdlet):
    Show-WmsBrainStatus.

CHECKPOINT POR GRUPO: despues de cada grupo, mostrame:
  - los .ps1 escritos (codigo completo, no resumen).
  - los tests Pester que pasan.
  - un ejemplo de invocacion exitosa contra el ambiente real.

---

FASE 3 — TESTS Y QA:

  - Cobertura minima 80% por cmdlet via Pester.
  - Smoke test end-to-end:
      1. Test-WmsBrainEnvironment (debe pasar antes de seguir).
      2. Invoke-WmsBrainHello -Rol consumidor.
      3. Test-WmsBrainConnection -Profile K7-PRD.
      4. Get-WmsBrainQuestion -Status pending.
      5. Show-WmsBrainQuestion -Id Q-001.
      6. Invoke-WmsBrainQuestion -Id Q-001 -DryRun (no toca SQL real).
      7. Invoke-WmsBrainQuestion -Id Q-001 (corre las 3 queries de Q-001 contra BB-PRD, guarda CSVs).
      8. New-WmsBrainQuestionEvent -QuestionId Q-001  (workaround: directive+tags).
      9. Invoke-WmsBrainNotify -FromEventFile <path>.
     10. Get-WmsBrainEventQueue -Status pending  (debe aparecer el evento).

  Si algun smoke test falla, no avances al siguiente grupo hasta
  resolverlo.

---

FASE 4 — DOCUMENTACION INLINE:

  Cada cmdlet con comment-based help completo:
    .SYNOPSIS, .DESCRIPTION, .PARAMETER, .EXAMPLE x3, .NOTES.

  Validar que `Get-Help <cmdlet> -Full` rinda un help util.

---

FASE 5 — INSTALACION Y EMPAQUETADO:

  scripts/install.ps1 que:
    1. Copia src/* a `$HOME\Documents\PowerShell\Modules\WmsBrainClient\<version>\`.
    2. Importa el modulo y corre Test-WmsBrainEnvironment.
    3. Si todo OK, imprime banner "WmsBrainClient v0.2.0 listo. Probá: Invoke-WmsBrainHello -Rol consumidor".

CHECKPOINT FINAL: PR (en rama wms-brain-client) con:
  - src/ completo
  - tests/ con Pester pasando
  - scripts/install.ps1
  - README.md actualizado con cmdlets implementados
  - CHANGELOG.md con bumps desde 0.1.0 → 0.2.0

PEDIME APROBACION ANTES DE ABRIR EL PR. Erik revisa todo antes de
mergear nada.

---

Si en cualquier momento tenes dudas, NO inventes. Preguntame. Las
dudas tipicas:
  - "que pasa si SCHEMA_VERSION del .mjs no es 1?" → devolver exit 5
    desde Test-WmsBrainEnvironment con mensaje claro.
  - "como manejo el caso de working tree sucio en exchange-brain?"
    → exit 4 desde Invoke-WmsBrainNotify, no intentar limpiar.
  - "puedo cachear connection strings?" → no, recompone cada vez con
    `$env:WMS_KILLIOS_DB_PASSWORD` actual (la pass puede rotar).
  - "que verbo PowerShell uso para X?" → siempre verbo aprobado de
    Get-Verb. Si no hay match exacto, preferir Invoke- sobre inventar.

Arranca por la FASE 0. Avisa cuando termines la lectura.
```

---

## 2. Qué esperar de la sesión

Tiempo estimado: **3–5 horas** de Open Claw activo, con checkpoints
intermedios donde Erik valida el resultado de cada fase antes de
avanzar.

Resultado esperado:

- Módulo PowerShell instalable, ~22 cmdlets, ~3000 LOC PS + ~1500 LOC tests.
- Smoke test end-to-end pasando contra K7-PRD/BB-PRD/C9-QAS reales.
- Q-001 a Q-008 ejecutables desde la primera invocación.
- Workaround `directive`+`tags` operativo (mientras se aprueba V2).
- CHANGELOG, install script, comment-based help para cada cmdlet.

---

## 3. Anti-patrones a vigilar

| Anti-patrón                                                 | Corrección                                                  |
|-------------------------------------------------------------|--------------------------------------------------------------|
| Reimplementar la cola de eventos en PowerShell.             | Delegar a `brain_bridge.mjs` siempre.                        |
| Cachear `$env:WMS_KILLIOS_DB_PASSWORD` en variable PS.      | Resolver en runtime cada vez.                                |
| Usar `Write-Host` para output programático.                 | Usar `Write-Output` (pipeable). `Write-Host` solo para banners. |
| Hardcodear paths en lugar de leer `$env:WMS_BRAIN_*_REPO`.  | Leer siempre del entorno; default seguro si no está seteado. |
| Emitir eventos con `type` no en `VALID_TYPES`.              | Validar contra el `.mjs` antes de notify.                    |
| Correr `git push` sin confirmación del usuario.             | `Invoke-WmsBrainNotify` confirma antes de push (default).    |
| Mezclar logica HH (Java) con VB en el mismo cmdlet.         | Cada cmdlet toca un solo dominio.                            |
| Generar SQL dinámico con string concat (riesgo SQL injection). | Usar parámetros de `Invoke-Sqlcmd` (`-Variable`).         |
| Llamar al `.mjs` con paths relativos.                       | Usar paths absolutos resueltos desde `$env:WMS_BRAIN_*_REPO`. |
| Imprimir el contenido del `brain_event.json` con la pass.   | Sanitizar antes de cualquier `Write-*`.                      |

---

## 4. Versionado de este prompt

- **2026-04-27**: redactado por agente Replit en ciclo-10. Alineado al
  bridge real (`SCHEMA_VERSION="1"`). Asume 4 ramas orphan
  (`main`/`wms-brain`/`wms-brain-client`/`wms-db-brain`).
- Si bumpea el `SCHEMA_VERSION` del bridge (post `EXTENSION-V2-PROPOSAL`),
  actualizar §0 vars de entorno y §1 FASE 0 lectura para que apunte a
  la versión nueva, y borrar el WORKAROUND del Grupo E.
