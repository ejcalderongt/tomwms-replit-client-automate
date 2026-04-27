# tomwms-replit-client-automate — rama `openclaw-control-ui`

> **MVP del control de bootstrap del brain**: scripts PowerShell que dejan
> el ambiente de Erik listo para que el agente Replit opere con el cerebro
> compartido (`wms-brain`). Esta rama se llama `openclaw-control-ui` por
> razones históricas (originalmente iba a tener una UI Avalonia para
> OpenClaw); hoy es un MVP por línea de comandos.

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
