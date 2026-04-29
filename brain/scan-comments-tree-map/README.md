# scan-comments-tree-map — Rescate de comentarios firmados con timestamp

> **Premisa de Erik**: el equipo (EJC + GT + AG + MA + AT + MECR + CF) firma comentarios en el código con un patrón propio:
>
> ```vbnet
> '#EJC20220224_0326AM: Identificar si se debe registrar en NAV el documento para BYB.
> ```
>
> Esos comentarios son **anclas de conocimiento**: contienen contexto que el código por sí solo no transmite (regla de negocio, decisión histórica, fix con justificación, alerta operativa). Mezclados con ese oro hay mucho ruido (TODOs sueltos, comentarios obsoletos, etiquetas de prueba, comentarios que repiten la línea siguiente).
>
> **Objetivo**: escanear el repo, detectar todos los comentarios que **siguen el patrón firmado**, separarlos en señal vs ruido con una rúbrica explícita, y promover los útiles al brain como `learnings/L-*` o como entradas del cuestionario.

Esta carpeta contiene la herramienta + las reglas de detección y scoring. La herramienta es **standalone**, se corre desde la línea de comandos contra un clon local de cualquier repo del WMS (`TOMWMS_BOF`, `TOMHH2025`, `TOMWMS_DBA`).

---

## Estado

| Atributo | Valor |
|---|---|
| Versión | 0.1.0 (beta) — `brain.learning.increase(beta)` |
| Lenguajes que escanea | VB.NET (`'`), Java (`//`, `/* */`), C# (`//`, `/* */`), SQL (`--`, `/* */`), Python (`#`), TypeScript/JavaScript (`//`, `/* */`) |
| Output principal | 5 archivos: `tree-map.json`, `by-author.md`, `by-client.md`, `useful-shortlist.md`, `noise-discarded.md` |
| Cómo se promueve | Editor humano elige del shortlist y crea `learnings/L-NNN-<slug>.md` o agrega al cuestionario |

---

## Cómo correr

```bash
# desde la raíz del clon del brain
cd brain/scan-comments-tree-map

# requisitos: python 3.10+, pyyaml. Sin dependencias C nativas.
pip install pyyaml

# escaneo completo de un repo
python scan.py --repo /path/to/TOMWMS_BOF --out outputs/2026-04-29-bof

# escaneo solo de una carpeta
python scan.py --repo /path/to/TOMWMS_BOF/WSHHRN --out outputs/2026-04-29-wshhrn

# dry-run con sample inline (valida regex sin necesidad del repo)
python scan.py --dry-run

# escaneo con threshold custom (default es score>=3 para shortlist)
python scan.py --repo /path/to/TOMHH2025 --out outputs/2026-04-29-hh --useful-threshold 4
```

### Flags

| Flag | Default | Para qué |
|---|---|---|
| `--repo PATH` | required | Carpeta raíz a escanear (recursivo) |
| `--out DIR` | `./outputs/<timestamp>` | Donde escribir los 5 outputs |
| `--useful-threshold N` | 3 | Score mínimo para considerar "útil" (ver SCORING.md) |
| `--include-no-pattern` | false | Si se activa, también lista comentarios con autor pero sin formato de fecha |
| `--dry-run` | false | Corre regex contra sample interno y sale (sin tocar repo) |
| `--max-context-lines` | 5 | Cuántas líneas alrededor del comentario capturar para contexto |

---

## Outputs explicados

Cada corrida produce 5 archivos en `outputs/<timestamp>/`:

### 1. `tree-map.json`

Estructura jerárquica completa: `repo/carpeta/archivo/[matches]`. Es la fuente de verdad cruda.

```json
{
  "scan_metadata": {
    "repo": "/path/to/TOMWMS_BOF",
    "scanned_at": "2026-04-29T14:32:00",
    "files_scanned": 4283,
    "files_with_matches": 612,
    "total_matches": 2741,
    "useful_count": 487,
    "noise_count": 2254
  },
  "tree": {
    "WSHHRN": {
      "TOMHHWS.asmx.vb": [
        {
          "line": 15734,
          "author": "EJC",
          "author_full": "Erik Calderón",
          "date": "2022-02-24",
          "time": "03:26 AM",
          "body": "Identificar si se debe registrar en NAV el documento para BYB.",
          "score": 8,
          "score_breakdown": {
            "client_mention": ["BYB"],
            "erp_mention": ["NAV"],
            "rule_keyword": ["Identificar"],
            "body_length": 60
          },
          "context_before": ["..."],
          "context_after": ["..."]
        }
      ]
    }
  }
}
```

### 2. `by-author.md`

Conteo y muestras por integrante del equipo. Útil para:
- Ver quién documenta más
- Identificar autores no registrados (siglas nuevas a agregar a `config/authors.yml`)
- Detectar drift (un autor que dejó de comentar de cierta forma)

### 3. `by-client.md`

Comentarios agrupados por cliente mencionado: BYB, BECOFARMA, MAMPA, KILLIOS, CEALSA, CUMBRE, IDEALSA, MERHONSA, MERCOPAN, INELAC. Es **gold para Wave 1+ del brain**: cada bucket es un mini-fingerprint de "qué cosas se han codificado específicamente para este cliente".

### 4. `useful-shortlist.md`

Top N comentarios con `score >= threshold`. Cada uno con:
- Cita exacta + ubicación
- Score breakdown (qué señales subieron el score)
- **Slot vacío para promoción**: campo "promote_to" con sugerencia (`learning/L-XXX`, `Q-XXX`, `case-XXX`) — el editor humano marca con qué se queda

Este es el **input directo a `brain.learning.increase(beta)`**. Carolina (o Erik) recorre, marca y se generan los archivos del brain.

### 5. `noise-discarded.md`

Lo que el scoring tiró abajo, con la razón. Crítico para **no perder señal por una mala rúbrica**: si Erik ve que algo importante quedó acá, ajustamos la rúbrica (`SCORING.md` + `config/boost-keywords.yml`) y volvemos a correr.

---

## Workflow de promoción al brain (`brain.learning.increase(beta)`)

```
1. correr scan.py contra TOMWMS_BOF
        │
        ▼
2. abrir useful-shortlist.md
        │
        ▼
3. para cada comentario con score alto:
   ┌─ si describe regla de negocio confirmada → crear learnings/L-NNN-<slug>.md
   ├─ si plantea pregunta → agregar a CUESTIONARIO_CAROLINA.md como Q-*
   ├─ si describe decisión arquitectónica → crear decisions/<NNN>-<slug>.md
   └─ si es bug/anomalía → abrir _proposals/YYYYMMDD-HHMM-<H_ID>-<slug>.md
        │
        ▼
4. revisar noise-discarded.md spot-check (5%)
   ├─ si hay falsos negativos → ajustar SCORING.md
   └─ commit + nueva corrida
        │
        ▼
5. commit al brain con bump de versión beta → 0.1.1
```

---

## Lo que NO hace (todavía)

- **No edita el repo del WMS**. Es READ-ONLY contra el código fuente. Para reformatear o limpiar comentarios obsoletos hay que abrir bundle aparte (regla 01 del brain: no push automático).
- **No rastrea el git blame**. Si querés saber quién tocó esa línea después del comentario, eso lo hace `git log -L`. Acá solo se confía en las iniciales del autor que firmó.
- **No deduplica entre archivos por similitud semántica**. Solo dedup exacto. Comentarios casi-idénticos pero parafraseados aparecen 2 veces.
- **No corre como pre-commit hook**. Es un escaneo bajo demanda, no un linter activo.
- **No alcanza comentarios sin firma del patrón**. Eso es deliberado: la premisa es rescatar lo que el equipo **se tomó el trabajo de firmar**. Comentarios anónimos van a otro flujo.

---

## Roadmap (post-beta)

- v0.2: integrar `git blame` para enriquecer cada match con el commit que tocó esa línea
- v0.3: detección semántica de duplicados (mismo concepto, distinta redacción) con embeddings locales
- v0.4: emitir directamente drafts de `L-NNN-<slug>.md` con front-matter listo, en vez de shortlist plano
- v0.5: pre-commit hook que **bloquee** comentarios firmados con score 0 (forza calidad mínima en nuevos)

---

## Archivos de esta carpeta

| Archivo | Propósito |
|---|---|
| `README.md` | Este archivo |
| `PATTERN.md` | Regex documentado, ejemplos positivos/negativos, variantes detectadas |
| `SCORING.md` | Rúbrica de utilidad: señales, puntajes, threshold |
| `scan.py` | Script principal Python |
| `config/authors.yml` | Iniciales conocidas → nombre completo + rol |
| `config/boost-keywords.yml` | Palabras que suben el score (clientes, ERPs, reglas) |
| `config/ignorelist.yml` | Patrones que descartan comentarios (TODO, prueba, etc.) |
| `outputs/.gitkeep` | Las corridas reales no se versionan; el directorio sí |

---

## Cross-refs

- Convenciones de comentarios del equipo: [`brain/_conventions/`](../_conventions/) (a expandir si esto consolida)
- Reglas duras del agente: [`entities/rules/`](../entities/rules/)
- Cuestionario para Carolina: [`agent-context/CUESTIONARIO_CAROLINA.md`](../agent-context/CUESTIONARIO_CAROLINA.md)
- Cómo se cargan los learnings al brain: [`learnings/`](../learnings/)
- Discovery tree del brain: [`_index/DISCOVERY_TREE.md`](../_index/DISCOVERY_TREE.md)
