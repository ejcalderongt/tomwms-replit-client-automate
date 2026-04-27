# tomwms-replit-client-automate — rama `openclaw-control-ui`

> **MVP del control de bootstrap del brain**: scripts PowerShell que dejan
> el ambiente de Erik listo para que el agente Replit opere con el cerebro
> compartido (`wms-brain`). Esta rama se llama `openclaw-control-ui` por
> razones históricas (originalmente iba a tener una UI Avalonia para
> OpenClaw); hoy es un MVP por línea de comandos.
# tomwms-replit-client-automate — rama `main`

> **Repositorio de intercambio** entre las máquinas Windows de Erik (PrograX24 / TOMWMS)
> y los agentes Replit que lo asisten. Esta rama (`main`) contiene **bundles operativos**,
> **scripts de sincronización** y el **bridge** entre Erik y el agente.
>
> No contiene código del WMS. Sólo:
> 1. Scripts PowerShell y Node de export / restore / set-config.
> 2. Bundles versionados de cambios listos para aplicar al BOF (`TOMWMS_BOF`) o al HH (`TOMHH2025`).
> 3. Manifiestos de estado y reglas de sincronización.

Snapshot: 2026-04-27. Mantenedor: agente Replit por encargo de Erik Calderón.

---

## Mapa de ramas del repositorio

Este repo (`ejcalderongt/tomwms-replit-client-automate`) tiene **cuatro ramas
huérfanas** sin merges entre sí. Cada una con un rol distinto:

| Rama | Propósito | README |
|---|---|---|
| `main` | Repo de intercambio: bundles, scripts, bridge | `README.md` |
| **`openclaw-control-ui`** (esta) | MVP de bootstrap/control del brain (`brain-up.ps1`) | `README.md` + `TOOLS.md` |
| `wms-brain` | **Cerebro** del agente: doctrina, módulos, decisiones, skills, ADRs | `brain/README.md` |
| `wms-db-brain` | **Cerebro de BD**: catálogo SQL extraído de Killios PRD (621 objetos) | `db-brain/README.md` |

> Las cuatro son **orphan branches**. Para clonar sólo ésta:
> `git clone --single-branch --branch openclaw-control-ui <url>`.

---

## Para qué sirve esta rama

Cuando Erik se sienta en su máquina Windows y va a abrir un caso con el
agente Replit, lo primero que hace es:

```powershell
.\scripts\brain-up.ps1
```

Esto sincroniza el repo de intercambio (`main`) y el repo del brain (`wms-brain`),
valida que no haya basura local (working tree sucio, ramas con divergencia,
secretos en claro), y deja un snapshot del estado en `state/brain-up.json`.

Después, para preguntar al agente algo concreto:

```powershell
.\scripts\brain-query.ps1 -Question "analiza si el idstock 1965 está reservado"
```

Es decir: **esta rama es el "panel de control" mínimo viable** para que Erik
arranque, valide y consulte sin tener que recordar 12 comandos.
Este repo (`ejcalderongt/tomwms-replit-client-automate`) es **multi-cabeza**:
cada rama tiene un propósito y una vida independiente. No hay merges entre ellas.

| Rama | Propósito | README |
|---|---|---|
| **`main`** (esta) | Repo de intercambio: bundles, scripts, bridge | `README.md` |
| `openclaw-control-ui` | MVP del control de bootstrap del brain (`brain-up.ps1`) | `README.md` + `TOOLS.md` |
| `wms-brain` | **Cerebro** del agente: doctrina, módulos, decisiones, skills, ADRs | `brain/README.md` |
| `wms-db-brain` | **Cerebro de BD**: catálogo SQL extraído de Killios PRD (621 objetos) | `db-brain/README.md` |

> Las cuatro ramas son **orphan branches** (sin historia compartida). Para clonar
> sólo una, usá `git clone --single-branch --branch <rama> <url>`.

---

## Para qué sirve esta rama (`main`)

Esta es la **superficie operativa** del intercambio. Cuando el agente Replit
necesita entregar un cambio a Erik para que lo aplique en su BOF o en el HH,
lo hace publicando un **bundle** acá. Erik (en su Windows) corre los scripts
para validar y aplicar el bundle a su solución VB.NET o Android.

Flujo típico:

```
Agente Replit                     Repo intercambio (rama main)               Erik (Windows)
-----------------                 -------------------------------            ---------------------
 1. Genera patch + manifest  -->  entregables_ajuste/<fecha>/<bundle>/
                                                                        <-- 2. git pull
                                                                            3. .\scripts\Apply-Bundle.ps1
                                                                            4. Compila / prueba
                                                                            5. Si ok: commit en TOMWMS_BOF
                                                                               (Azure DevOps)
```

El agente **nunca empuja directamente** a `TOMWMS_BOF` ni a `TOMHH2025`.
Esa regla está documentada en `wms-brain` → `brain/entities/rules/rule-01-no-push-automatico-wms.md`.

---

## Estructura de la rama

```
.
├── README.md                          <- este archivo
├── INSTALL.md                         <- comandos de uso (brain-up, brain-query)
├── TOOLS.md                           <- notas locales del operador
│
├── config/
│   └── SYNC_RULES.md                  <- reglas heredadas del repo de intercambio
│
├── manifests/
│   └── environment.json               <- snapshot del ambiente declarado
│
├── scripts/
│   ├── brain-up.ps1                   <- bootstrap completo del ambiente brain
│   ├── brain-query.ps1                <- punto de entrada para preguntas al agente
│   ├── openclaw-export.ps1            <- (heredado) export de estado
│   ├── openclaw-restore.ps1           <- (heredado) restore de estado
│   └── openclaw-set-config.ps1        <- (heredado) set config con -DryRun
│
└── skills/
    └── README.md                      <- placeholder para skills locales
```

### Scripts clave

#### `scripts/brain-up.ps1` (2.7 KB)

**El comando más importante de esta rama.** Hace lo siguiente, en orden:

1. **Sync exchange** (`C:\tomwms-exchange`):
   - `git fetch && git pull --ff-only` en `main`.
   - Verifica que no haya archivos modificados sin commitear.
2. **Sync brain** (`C:\tomwms-brain`):
   - `git fetch && git pull --ff-only` en `wms-brain`.
   - Idem, verifica working tree limpio.
3. **Sync db-brain** (`C:\tomwms-db-brain`, opcional):
   - Idem para `wms-db-brain` si está clonado.
4. **Validación**:
   - Sin secretos en claro en cambios pendientes.
   - Sin commits divergentes con `origin/<rama>`.
   - Última extracción de Killios no está caducada (>72h dispara warning).
5. **Snapshot**:
   - Escribe `state/brain-up.json` con timestamp, SHAs de cada repo, validaciones
     pasadas y warnings.

Flags útiles:
- `-PromptSql`: pide credenciales SQL Server interactivamente para validar
  acceso a Killios (por defecto sólo valida el catálogo cacheado).
- `-Verbose`: muestra cada step.
- `-SkipDbBrain`: omite la rama `wms-db-brain` (útil si todavía no está
  clonada localmente).

Ejemplo:

```powershell
.\scripts\brain-up.ps1                   # sync silencioso completo
.\scripts\brain-up.ps1 -PromptSql        # con validacion de acceso SQL
.\scripts\brain-up.ps1 -Verbose          # muestra cada paso
```

Salida típica:

```
[brain-up] exchange@main:    9430734 (clean)
[brain-up] wms-brain:        a336e27 (clean)
[brain-up] wms-db-brain:     131e611 (clean)
[brain-up] killios snapshot: 2026-04-27T01:29Z (fresh)
[brain-up] state -> state/brain-up.json
[brain-up] OK
```

#### `scripts/brain-query.ps1` (368 B)

Wrapper minimalista para mandar preguntas al agente. Lee el último
`state/brain-up.json`, arma un payload con la pregunta + contexto del estado
y lo deja en `_inbox/` del repo de brain para que el bridge lo recoja.

Ejemplo:

```powershell
.\scripts\brain-query.ps1 -Question "explicame por que stock_res quedo huerfano para idstock 1965"
```

Lo que hace internamente:
1. Verifica que `brain-up.ps1` corrió en las últimas 4 horas.
2. Genera un evento JSON con `timestamp`, `question`, `state-snapshot`.
3. Lo escribe en `<wms-brain>/brain/_inbox/<ulid>.json`.
4. (Opcional) hace `git commit -m "case: <ulid>"` y `git push`.

#### Scripts heredados (`openclaw-*.ps1`)

Son **copia exacta** de los de la rama `main`. Se mantienen acá por
conveniencia: cuando Erik sólo tiene esta rama clonada, igual puede hacer
exports y restores sin tener que clonar `main`.

---

## Estado persistente

#### `state/brain-up.json`

Lo escribe `brain-up.ps1` en cada corrida. Esquema:

```json
{
  "ts": "2026-04-27T07:42:11Z",
  "exchange": { "branch": "main",          "sha": "9430734...", "clean": true },
  "brain":    { "branch": "wms-brain",     "sha": "a336e27...", "clean": true },
  "dbBrain":  { "branch": "wms-db-brain",  "sha": "131e611...", "clean": true },
  "killios":  { "lastExtract": "2026-04-27T01:29:47Z", "ageH": 6.2, "stale": false },
  "warnings": [],
  "ok": true
}
```

**Importante**: este archivo es local de Erik, no se versiona acá. Está en
`.gitignore` (carpeta `state/`).

---

## Convenciones operativas (`TOOLS.md`)

Resumido del `TOOLS.md` actual:

- **Antes de operar**: siempre `brain-up.ps1` (handshake `hello sync`).
- **Si el sync está bien**: el agente responde `Hello Erik` + ASCII art para
  confirmar que está listo.
- **Para el MVP de control**: usar **esta rama** (`openclaw-control-ui`).
- **Paths de referencia en la máquina de Erik**:
  - Exchange repo: `C:\tomwms-exchange` (rama `main`).
  - Brain repo: `C:\tomwms-brain` (rama `wms-brain`).
  - DB-brain repo: `C:\tomwms-db-brain` (rama `wms-db-brain`).
- **Branches involucradas**:
  - `main` = repo de intercambio, bundles, contrato operativo.
  - `wms-brain` = conocimiento compartido del agente.
  - `openclaw-control-ui` = MVP de bootstrap/control/brain-up.
├── INSTALL.md                         <- comandos PowerShell de bootstrap
│
├── config/
│   └── SYNC_RULES.md                  <- reglas de sync (qué se versiona, qué no)
│
├── manifests/
│   └── environment.json               <- estado declarado del ambiente Erik
│
├── scripts/
│   ├── openclaw-export.ps1            <- exporta estado actual desde la máquina Erik
│   ├── openclaw-restore.ps1           <- restaura estado en otra máquina
│   ├── openclaw-set-config.ps1        <- aplica config (con -DryRun)
│   ├── Apply-Bundle.ps1               <- aplica un bundle de entregables_ajuste/
│   ├── apply_bundle.mjs               <- runner Node del Apply-Bundle (lógica núcleo)
│   ├── brain_bridge.mjs               <- bridge agente <-> brain (eventos _inbox)
│   └── hello_sync.mjs                 <- handshake "hello sync" con el agente
│
├── entregables_ajuste/                <- bundles versionados listos para aplicar
│   ├── AGENTS.md                      <- protocolo de entrega de bundles
│   └── <fecha>/<bundle_id>/
│       ├── MANIFEST.json              <- metadata del bundle (qué toca, qué requiere)
│       ├── README.md                  <- descripción humana del cambio
│       └── patches/*.patch            <- patches `git apply` ready
│
└── skills/
    └── README.md                      <- skills locales (placeholder por ahora)
```

### Detalle archivo por archivo

#### Scripts PowerShell (estación Erik)

- **`openclaw-export.ps1`** (973 B): exporta el estado actual de la máquina
  Windows de Erik (manifiestos, config, snapshots) a `manifests/environment.json`.
  No toca código fuente del BOF/HH; sólo configuración de entorno.

- **`openclaw-restore.ps1`** (526 B): restaura el estado declarado en
  `manifests/environment.json` a la máquina actual. Pensado para bootstrap
  de una segunda máquina (laptop secundaria, VM, etc.).

- **`openclaw-set-config.ps1`** (590 B): aplica cambios de configuración
  con flag `-DryRun` para simular antes de tocar nada. **Siempre correr con
  `-DryRun` primero** (regla de oro).

- **`Apply-Bundle.ps1`** (3.0 KB): wrapper PowerShell que delega al runner
  Node `apply_bundle.mjs`. Recibe la ruta de un bundle de `entregables_ajuste/`
  y lo aplica al checkout local del BOF (`C:\TOMWMS_BOF`) o HH (`C:\TOMHH2025`).

#### Runners Node (lógica de bridge y bundles)

- **`apply_bundle.mjs`** (20.5 KB): núcleo del aplicador de bundles. Lee
  `MANIFEST.json`, valida pre-requisitos (rama esperada, archivos tocados,
  ausencia de cambios sin commitear), aplica los `.patch` con `git apply
  --3way`, y deja un reporte de qué se cambió. Si falla, hace rollback del
  índice.

- **`brain_bridge.mjs`** (21.5 KB): bridge entre el agente Replit y el cerebro
  (`wms-brain` rama). Lee eventos pendientes en `brain/_inbox/*.json`, los
  procesa y mueve los resueltos a `brain/_processed/`. Emite propuestas a
  `brain/_proposals/` cuando detecta patrones nuevos.

- **`hello_sync.mjs`** (8.0 KB): handshake protocolar antes de cualquier
  operación. Verifica que el repo de intercambio y el repo de brain estén
  alineados (mismas SHAs esperadas, sin trabajo pendiente). Si falla, el
  agente debe abortar y pedir intervención humana. Regla:
  `wms-brain` → `brain/entities/rules/rule-10-hello-sync-antes-de-operar.md`.

#### Configuración y manifiestos

- **`config/SYNC_RULES.md`** (203 B): reglas duras del sync.
  - **No** versionar secretos en claro.
  - **No** versionar binarios compilados del BOF/HH.
  - **Si** versionar bundles, manifiestos y scripts.

- **`manifests/environment.json`** (187 B): snapshot del entorno declarado.
  Lo escribe `openclaw-export.ps1` y lo lee `openclaw-restore.ps1`.

#### Bundles de entrega

- **`entregables_ajuste/AGENTS.md`** (12.2 KB): protocolo completo de cómo el
  agente debe estructurar un bundle. Define:
  - Naming: `<fecha>/<v##_bundle>/`.
  - Contenido obligatorio: `MANIFEST.json`, `README.md`, `patches/`.
  - Esquema del `MANIFEST.json` (target repo, rama esperada, archivos tocados,
    chequeos pre/post, autor agente, timestamp).
  - Convenciones de naming de patches (`0001-...`, `0002-...`).
  - Política de rollback.

- **`entregables_ajuste/<fecha>/<bundle>/`**: cada bundle es **inmutable** una
  vez publicado. Si hay que corregir, se publica un bundle nuevo con número
  posterior. Ejemplo actual: `2026-04-25/v23_bundle/`.

---

## Flujo recomendado (operativo)

### Para Erik (estación Windows)

```powershell
# 1. Sincronizar repo de intercambio
cd C:\tomwms-exchange
git pull

# 2. Verificar handshake con agente
node scripts/hello_sync.mjs

# 3. Aplicar bundle pendiente
.\scripts\Apply-Bundle.ps1 -BundlePath entregables_ajuste\2026-04-25\v23_bundle

# 4. Si todo compila: commit en el repo de destino (BOF o HH)
cd C:\TOMWMS_BOF
git status
git commit -am "Aplica v23_bundle: <descripcion>"
git push origin dev_2028_merge
```

### Para el agente Replit

```bash
# 1. Generar bundle desde patch listo
node scripts/apply_bundle.mjs --pack \
  --target tomwms-bof \
  --branch dev_2028_merge \
  --patches /tmp/work/*.patch \
  --out entregables_ajuste/$(date +%Y-%m-%d)/v24_bundle

# 2. Commit + push a esta rama (main)
git add entregables_ajuste/
git commit -m "v24_bundle: <descripcion>"
git push origin main

# 3. Notificar a Erik por canal acordado (no acá)
```

---

## Reglas de oro de esta rama

1. **No tocar el código del WMS desde acá.** Esta rama es sólo control.
2. **`brain-up.ps1` es idempotente.** Correrlo dos veces no rompe nada.
3. **Si `brain-up.ps1` da warnings, leerlos antes de ignorar.** Generalmente
   indican: working tree sucio, snapshot Killios caducado, divergencia con
   `origin`.
4. **`brain-query.ps1` no responde directo.** Sólo encola el caso para el
   agente. La respuesta llega por el canal acordado (chat Replit).
5. **No commitear `state/`.** Es snapshot local.

---

## Cross-refs a otras ramas

- **Para entender qué bundles vendrán por el repo de intercambio** →
  `main` → `entregables_ajuste/`.
- **Para entender qué sabe el agente sobre el WMS** → `wms-brain` →
  `brain/README.md`.
- **Para consultar el catálogo SQL de Killios** → `wms-db-brain` →
  `db-brain/README.md`.
- **Bridge formal de eventos** → `wms-brain` → `brain/BRIDGE.md`.

---

## Estado actual (snapshot 2026-04-27)

- 15 archivos versionados.
- Scripts: 5 PowerShell (2 propios MVP + 3 heredados de `main`).
- Última actualización del flujo: `brain-up.ps1` v1.x (sync triple repo +
  validación Killios snapshot).

---

## Roadmap futuro (no comprometido)

- UI gráfica Avalonia para reemplazar `brain-query.ps1` con un chat panel.
- Integración del `brain-up` con un service Windows que corra cada N minutos.
- Tray icon para mostrar estado del último `brain-up.json` (verde / amarillo
  / rojo).
- Reemplazar PowerShell por binarios .NET 8 self-contained (single file).

> Estos puntos no están comprometidos; la rama sigue siendo MVP en línea de
> comandos hasta nuevo aviso.
1. **No secretos en claro.** Ni en scripts, ni en manifests, ni en bundles.
   Los secretos viven en el ambiente del runner (Erik o Replit).
2. **No código del BOF/HH versionado.** Sólo patches dentro de bundles.
3. **No mergear ramas.** Esta es `main`, las otras tres son orphan.
4. **Bundles inmutables.** Una vez publicado, no se reescribe; se publica
   un sucesor con número incrementado.
5. **Hello sync antes de operar.** Si el handshake falla, abortar.
6. **DryRun antes que apply.** Especialmente en config.

---

## Convenciones de commits en esta rama

- **Bundle nuevo**: `v##_bundle: <descripcion-corta>`.
- **Script update**: `scripts: <que-cambio>`.
- **Manifest update**: `manifests: <que-cambio>`.
- **Doc update**: `docs: <que-cambio>`.
- Cuerpo del commit: explicar **por qué**, no sólo **qué**.
- Idioma: español rioplatense, sin emojis.

---

## Cross-refs a otras ramas

- **Para entender el por qué de los bundles** → `wms-brain`:
  - `brain/entities/decisions/` (decisiones operativas).
  - `brain/decisions/` (ADRs estratégicos como el 003-mi3-reescrito).
- **Para entender el modelo de datos que tocan los bundles** → `wms-db-brain`:
  - `db-brain/tables/`, `db-brain/views/`, `db-brain/sps/`.
- **Para entender el ciclo de vida del brain (cómo nace un evento)** →
  `wms-brain` → `brain/BRIDGE.md`.
- **Para el MVP de bootstrap del brain** → `openclaw-control-ui` →
  `scripts/brain-up.ps1`.

---

## Estado actual (snapshot 2026-04-27)

- 24 archivos versionados.
- Último bundle: `entregables_ajuste/2026-04-25/v23_bundle/` (5.3 KB de patch
  sobre `frm-eliminar-ajuste-rules-borrador-y-lista-memoria`).
- Scripts: 7 (3 PowerShell estación + 3 Node runners + 1 PowerShell wrapper).
- Bridge activo: `brain_bridge.mjs` lee `_inbox/` de la rama `wms-brain`.
