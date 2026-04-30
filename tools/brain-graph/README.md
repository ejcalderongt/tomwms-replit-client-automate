# brain-graph — visualizador del cerebro tipo Obsidian

> **Que es**: parser del brain markdown + viewer Cytoscape.js que renderiza
> el grafo completo del brain en el navegador. Sirve para responder
> preguntas que la lista jerarquica de carpetas no responde:
>
> - ¿Que nodos son centrales (mas conectados)?
> - ¿Que CPs estan huerfanos (sin links de entrada/salida)?
> - ¿Que links estan rotos (apuntan a archivos que no existen)?
> - ¿Como se cluteriza el cerebro alrededor de Killios, dañado_picking,
>   trans_picking_ubic, etc.?
> - ¿Donde estan las "comunidades" implicitas del brain?

---

## Como correrlo

### 1. Generar el grafo (parser Python -> graph.json)

```bash
cd wms-brain/tools/brain-graph
python3 build_graph.py
```

Esto:
- Escanea `wms-brain/brain/**/*.md`, `**/*.yml`, `**/*.yaml`.
- Para cada archivo, extrae: tipo (por carpeta o frontmatter), tags
  inline (`#cliente/killios`, etc.), frontmatter YAML, links markdown
  `[text](path)`, wiki-links `[[name]]`.
- Resuelve cada link a un archivo real bajo brain/.
- Escribe `graph.json` (~varios MB).
- Imprime estadisticas: top 20 nodos mas conectados, conteo por tipo,
  links rotos, nodos huerfanos.

### 2. Ver el grafo en el navegador

Necesitas servir `viewer.html` y `graph.json` desde el mismo origen
(no funciona con `file://` por la politica de fetch).

Opcion A — Python http.server (mas simple):
```bash
cd wms-brain/tools/brain-graph
python3 -m http.server 8000
# Abrir http://localhost:8000/viewer.html
```

Opcion B — desde el preview de Replit:
- Si tenes un workflow corriendo `python3 -m http.server` en este folder,
  el preview te da el URL automaticamente.

Opcion C — Obsidian (alternativo, sin pasar por este viewer):
- Abrir `wms-brain/brain/` como Vault en Obsidian.
- Activar "Graph view" (icono o `Ctrl+G`).
- Ventaja: backlinks automaticos, busqueda por tag, plugin Dataview.
- Desventaja: requiere instalar Obsidian local.

---

## Que se ve en el viewer

- **Nodos** = archivos del brain. Color por tipo:
  - rojo `#e63946` = `bug` (wms-known-issues/BUG-NNN)
  - verde-azulado `#2a9d8f` = `feat` (wms-incorporated-features/FEAT-NNN)
  - naranja `#f4a261` = `cp-open` (customer-open-cases/CP-NNN)
  - oliva `#7c9885` = `cp-closed` (customer-closed-cases/CP-NNN)
  - gris `#9b9b9b` = `cp-legacy` (debuged-cases/CP-NNN — pendientes migrar)
  - violeta `#9b5de5` = `adr` (decisions/)
  - amarillo `#fcbf49` = `proposal` (_proposals/)
  - azul `#118ab2` = `trace` (code-deep-flow/)
  - verde claro `#06d6a0` = `learning` (learnings/)
  - rosa `#ef476f` = `agent-context` (agent-context/)
  - amarillo claro `#ffd166` = `data-strategy` (data-seek-strategy/)
  - verde claro 2 `#80b918` = `wms-flow` (wms-specific-process-flow/)
  - azul oscuro `#3a86ff` = `caso-reserva` (casos-observados/)
  - magenta `#ff006e` = `client` (clients/, client-index/)
  - violeta oscuro `#8338ec` = `db-brain` (wms-db-brain/)
  - naranja oscuro `#fb5607` = `index` (_index/)
  - gris claro `#bdbdbd` = `inbox` (_inbox/)
  - gris medio `#6c757d` = `other`
- **Tamaño del nodo** = proporcional a la suma de in_degree + out_degree
  (los mas conectados son mas grandes).
- **Aristas grises** = links markdown `[text](path)`.
- **Aristas azules** = wiki-links `[[name]]` (si los hubiera).

### Interaccion

- **Click en nodo** = highlight de el + sus vecinos (resto se opaca);
  panel inferior izquierdo muestra metadata (tipo, in/out, tags, clientes).
- **Click en leyenda** = filtra solo nodos de ese tipo.
- **Click en tag** = filtra solo nodos con ese tag.
- **Buscador** = filtra por id, label, tag, cliente, tipo.
- **Limpiar filtros** = vuelve a mostrar todo.
- **Centrar** = encaja todo el grafo en la pantalla.
- **Re-layout** = recalcula posiciones (util si quedo apretado).
- **Click en fondo** = limpia el highlight del nodo seleccionado.
- **Scroll** = zoom in/out.
- **Arrastrar nodo** = lo movés (el layout se rompe pero podes ver
  detalles).

---

## Como hacer rendir mas el grafo (frontmatter + tags)

El parser ya detecta links existentes, pero el grafo cluteriza mejor si
agregamos metadata estructurada a los archivos clave.

### Frontmatter sugerido por tipo

#### Bugs (`wms-known-issues/BUG-NNN/INDEX.md`)

```yaml
---
id: BUG-001
tipo: bug
estado: open                # open | mitigado | resolved
severidad: critica          # critica | alta | media | baja
prioridad: alta             # alta | media | baja
modulo: trans_picking_ubic
clientes: [killios, mercopan, byb]
ramas_afectadas: [dev_2023_estable, dev_2028_merge]
descubierto: 2026-04
tags: [bug/picking, bug/critico, modulo/inventario]
---
```

#### Casos de cliente (`customer-open-cases/CP-NNN/INDEX.md`)

```yaml
---
id: CP-013
tipo: cp-open
cliente: killios
producto: WMS164
estado: open
severidad: alta
materializa_bug: BUG-001     # link explicito al BUG raiz
descubierto: 2026-04
tags: [cliente/killios, producto/wms164, bug/picking]
---
```

#### Features (`wms-incorporated-features/FEAT-NNN/INDEX.md`)

```yaml
---
id: FEAT-001
tipo: feat
estado: pendiente-validacion  # pendiente-validacion | qa | promovido
autores: [erik, marcela, abigail]
modulo: trans_ubic_hh
ramas_afectadas: [dev_2028_merge]
incorporado: 2026-04-29
clientes_destino: [idealsa, merhonsa, mercopan]
tags: [feat/picking, modulo/hh, autor/colaborativo]
---
```

#### Trazas de codigo (`code-deep-flow/traza-NNN.md`)

```yaml
---
id: traza-002
tipo: trace
relacionado_con: [BUG-001, CP-013]
ramas: [dev_2023_estable, dev_2028_merge]
archivos_analizados: 10
modulo: trans_picking_ubic
tags: [trace/picking, modulo/bof-vbnet]
---
```

### Tags sugeridos (taxonomia inicial)

- **Por modulo del WMS**: `#modulo/picking`, `#modulo/inventario`,
  `#modulo/recepcion`, `#modulo/despacho`, `#modulo/hh`, `#modulo/bof-vbnet`
- **Por cliente**: `#cliente/killios`, `#cliente/mercopan`,
  `#cliente/byb`, `#cliente/becofarma`, `#cliente/cealsa`,
  `#cliente/mampa`, `#cliente/idealsa`, `#cliente/merhonsa`
- **Por rama**: `#rama/dev_2023_estable`, `#rama/dev_2028_merge`
- **Por categoria de bug**: `#bug/critico`, `#bug/picking`,
  `#bug/inventario`, `#bug/datos-sucios`, `#bug/hardcoded`
- **Por estado**: `#estado/open`, `#estado/mitigado`, `#estado/resolved`
- **Por wave**: `#wave/13`, `#wave/14`, etc.

Una vez agregado el frontmatter + tags a los archivos clave, re-correr
`python3 build_graph.py` y refrescar el viewer.

---

## Limitaciones conocidas

- **No hace backlinks automaticos** como Obsidian. Si CP-013 enlaza a
  BUG-001 pero BUG-001 no enlaza a CP-013, en este viewer el grafo
  muestra una arista direccional. Obsidian renderia ambas direcciones
  con su panel de "backlinks" automatico.
- **Layout cose-bilkent es deterministico pero pesado** para grafos de
  >2000 nodos. Si el brain crece mucho, considerar layouts alternativos
  (`cola`, `fcose`, `dagre` para DAGs).
- **No persiste posicion de nodos**. Cada re-layout recalcula desde cero.
  Si querés posiciones fijas, exportar coords y modificar el viewer.
- **Frontmatter parser super basico** (no es PyYAML). Soporta: strings,
  arrays inline `[a, b, c]`, listas YAML con `- item`. No soporta
  estructuras anidadas.
- **Tags inline excluyen lineas dentro de code blocks** (heuristica:
  4 espacios o tab al inicio). Tags dentro de bloques con triple
  backtick \`\`\` SI se cuentan — son falsos positivos. Si te molesta,
  podemos refinar el parser.

---

## Roadmap (futuras iteraciones)

- [ ] Agregar layout `fcose` (mas rapido en grafos grandes, similar look).
- [ ] Detectar comunidades (clustering Louvain / Leiden) y colorear por
      cluster en lugar de tipo.
- [ ] Exportar a SVG/PNG para incluir en reportes.
- [ ] Backlinks bidireccionales virtuales (cada arista A->B se muestra
      como tambien existiera B->A en gris).
- [ ] Filtros combinados (tipo AND tag AND cliente).
- [ ] Persistir posiciones de nodos en localStorage.
- [ ] Vista de "diff": que cambio del grafo entre dos commits.
- [ ] Endpoint en el api-server que sirva graph.json directo desde
      memoria, sin necesitar correr build_graph.py manual.
