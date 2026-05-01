---
id: GITHUB_SYNC
tipo: agent-context
estado: vigente
titulo: GITHUB_SYNC.md — Sincronizar el brain a GitHub via API
tags: [agent-context]
---

# GITHUB_SYNC.md — Sincronizar el brain a GitHub via API

> Patron canonico para sincronizar `/home/runner/workspace/wms-brain/` a la
> rama `wms-brain` del repo `ejcalderongt/tomwms-replit-client-automate`.
>
> **Por que via API y no `git push`**: el sandbox de Replit bloquea
> operaciones git destructivas en `/home/runner/workspace/.git/`. Ver
> `REPLIT_AGENT.md` seccion 2.1 para el detalle. La API REST de GitHub
> resuelve sin tocar `.git/config.lock`.

**Ultima validacion**: 2026-04-30, sync completo de 512 archivos / 19 MB en
~6 minutos (con 4 threads + cache + retry).

---

## 1. Cuando sincronizar

| Situacion | Que hacer |
|---|---|
| Acabas de cerrar trabajo significativo en el brain (>3 archivos editados) | **SI sincronizar** antes de cerrar el turno. |
| Solo lectura, sin ediciones | NO. Sincronizar pisa la rama con commit vacio. |
| Cambio cosmetico aislado en un solo archivo | OK sincronizar, pero podes esperar al siguiente turno y agruparlo. |
| Erik te lo pide explicitamente | SI, sin chequear nada mas. |
| Estas en medio de una investigacion y el resultado es parcial | NO. Anota el progreso en `_inbox/` y sincroniza cuando cierres el caso. |

---

## 2. Receta canonica (3 pasos)

### Paso 1 — Backup branch (siempre, no negociable)

Antes de tocar `wms-brain` en GitHub, crear un branch de backup apuntando al
HEAD actual. Naming: `wms-brain-pre-sync-YYYY-MM-DD` (si ya existe ese, le
agregas `-2`, `-3`, etc).

```python
import os, json, urllib.request

TOKEN = os.environ['GITHUB_TOKEN']
REPO = 'ejcalderongt/tomwms-replit-client-automate'

def gh(method, path, body=None):
    data = json.dumps(body).encode() if body else None
    r = urllib.request.Request(
        f'https://api.github.com/repos/{REPO}{path}',
        data=data, method=method,
        headers={'Authorization': f'token {TOKEN}',
                 'Accept': 'application/vnd.github+json',
                 'Content-Type': 'application/json',
                 'User-Agent': 'wms-brain-sync'})
    with urllib.request.urlopen(r, timeout=60) as resp:
        return json.loads(resp.read())

ref = gh('GET', '/git/refs/heads/wms-brain')
sha = ref['object']['sha']
gh('POST', '/git/refs',
   {'ref': 'refs/heads/wms-brain-pre-sync-2026-04-30',
    'sha': sha})
```

### Paso 2 — Crear blobs + tree + commit

Para cada archivo del workspace, crear un blob (POST `/git/blobs` con
`content` en base64). Despues construir un tree con `base_tree` =
HEAD-tree-actual de `wms-brain` (para preservar archivos que el workspace no
toco). Despues commit con UN parent = HEAD-actual. Despues PATCH del ref.

Ver `scripts/sync-to-github.py` para la implementacion completa.

### Paso 3 — Update ref + verificar

```python
gh('PATCH', '/git/refs/heads/wms-brain',
   {'sha': commit_sha, 'force': False})
```

`force: False` siempre. Si te dice "ref update fails because it's not a
fast-forward", **NO forzar**: investigar primero por que la rama avanzo
(quizas Erik o un agente local pushearon algo, y vos perdiste el sync).

Verificar visitando:
`https://github.com/ejcalderongt/tomwms-replit-client-automate/commit/<sha>`

---

## 3. Como NO romper

### 3.1 Rate limit (HTTP 403)

GitHub limita ~5000 requests/hora con PAT. Si pegas 16 threads simultaneos
contra `/git/blobs`, te tira 403 a los ~150 archivos. **Usar 4 threads
maximo** + retry exponencial (`(2**attempt) + random()` segundos) +
cache persistente en `/tmp/wms-brain-blobs-cache.json` para reanudar si
explota.

### 3.2 Timeout del bash (120s)

El comando bash tiene un cap de 120s. Para 500+ archivos con 4 threads,
necesitas ~3 invocaciones del script. **El cache de blobs es clave**: cada
re-run salta los ya hechos.

### 3.3 Encoding del commit message

Los commit messages deben ir en **ASCII puro** (sin tildes, sin n-tilde, sin
emojis). El push via API los acepta en UTF-8 pero algunos visualizadores se
rompen. Convencion: redactar el mensaje en castellano sin tildes.

Ejemplo correcto:
```
merge: incorporar brain del workspace (rico) sobre wms-brain GitHub
```

Ejemplo a evitar:
```
merge: incorporación del cerebro del espacio de trabajo
```

### 3.4 Forzar push (`force: True`)

**Solo en emergencia y avisando a Erik primero.** Caso unico aceptable:
limpiar una rama corrupta o resetear despues de un push erroneo. Si tenes
que forzar, **siempre** crear backup branch antes (regla del paso 1).

---

## 4. Drift (workspace vs GitHub)

El sync que escribimos es **unidireccional workspace -> GitHub**. NO baja
cambios de GitHub al workspace. Esto crea dos modos de drift posibles:

### 4.1 GitHub adelantado (alguien mas commiteo)

Sintoma: `gh('GET', '/git/refs/heads/wms-brain')['object']['sha']` no
coincide con lo que vos esperas.

Solucion:
1. **NO sincronizar** sin entender que cambio.
2. Bajar los archivos cambiados via API:
   `GET /contents/<path>?ref=wms-brain`
3. Reconciliar manualmente (compare con `diff` lo que tenes vs lo de GitHub).
4. Decidir caso por caso.

### 4.2 Workspace adelantado (vos editaste sin sincronizar)

Sintoma: vas a sincronizar y el numero de archivos del workspace no
coincide con lo que esperas (mas o menos).

Solucion:
1. `git --no-optional-locks status --short` en el workspace para ver
   archivos sin commitear (visibles solo si el `.git` del workspace los
   trackea, pero como el sandbox bloquea `git add`, en general el workspace
   no commitea).
2. `find /home/runner/workspace/wms-brain/ -newer <fecha-ultimo-sync>` para
   ver que cambio.
3. Sincronizar normal.

### 4.3 Caso real documentado

El 2026-04-30 paso esto: yo (agente Replit) trabaje 23 archivos en
`/tmp/wms-brain-fresh/` (NO en el workspace) y los sincronice a la rama
`wms-brain` de GitHub. La rama de GitHub quedo POBRE (solo esos 23
archivos), y el workspace local tenia 512 archivos del cerebro original
sin estar en GitHub.

Resolucion: hicimos un commit con `base_tree` = HEAD de GitHub (que tenia
los 23 archivos mios) + tree con los 512 del workspace. El resultado quedo
en GitHub con la union de ambos, sin perder nada. Backup branch
`wms-brain-pre-sync-2026-04-30` -> `185348790793` preservado por las dudas.

**Leccion**: trabajar siempre en `/home/runner/workspace/wms-brain/`,
nunca en `/tmp/`.

---

## 5. Script reusable

Vive en `brain/agent-context/scripts/sync-to-github.py`. Uso:

```bash
cd /home/runner/workspace/wms-brain && \
python brain/agent-context/scripts/sync-to-github.py \
    --message "feat: nueva traza de licensing en BOF VB.NET"
```

Flags utiles:
- `--message "..."` — mensaje del commit (obligatorio).
- `--backup-name "..."` — nombre del backup branch (default: `wms-brain-pre-sync-YYYY-MM-DD`).
- `--dry-run` — lista archivos a subir sin hacer el push.
- `--cache-file` — path del cache de blobs (default: `/tmp/wms-brain-blobs-cache.json`).
- `--threads N` — numero de threads para blobs (default 4, max recomendado 6).

El script crea el backup, sube todos los archivos, hace el commit y patch
del ref. Anuncia el commit hash al final.
