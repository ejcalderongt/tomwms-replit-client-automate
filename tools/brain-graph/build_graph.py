#!/usr/bin/env python3
"""
build_graph.py v2 — Parser del brain markdown -> graph.json para Cytoscape.js

Extrae nodos y aristas. A diferencia de v1, ademas de los links markdown
explicitos, infiere aristas IMPLICITAS desde el frontmatter YAML:

- `materializa_bug: BUG-001`        -> arista al BUG por id
- `casos_relacionados: [CP-013]`    -> aristas a CPs por id
- `relacionado_con: [BUG-001, ...]` -> aristas genericas (resuelve por id o path)
- `bugs_relacionados: [...]`        -> idem a BUGs
- `feats_relacionados: [...]`       -> idem a FEATs
- `clientes: [killios, mercopan]`   -> aristas a nodos virtuales _virtual/cliente/<n>
- `cliente: killios` (string)       -> idem
- `modulo: trans_picking_ubic`      -> arista a nodo virtual _virtual/modulo/<n>
- `ramas_afectadas: [dev_2023_...]` -> aristas a nodos virtuales _virtual/rama/<n>
- `autores: [erik, marcela]`        -> aristas a nodos virtuales _virtual/autor/<n>

Los nodos virtuales centralizan el grafo: si BUG-001 y FEAT-001 ambos
tienen `cliente: killios` en frontmatter, ambos se conectan al mismo
nodo _virtual/cliente/killios — lo que produce el clustering tipo "Accounts"
del grafo de Obsidian.

Output: graph.json en el mismo folder.
"""
import json
import re
import sys
from datetime import datetime, timezone
from pathlib import Path

BRAIN_ROOT = Path(__file__).resolve().parent.parent.parent / "brain"
OUT_JSON = Path(__file__).resolve().parent / "graph.json"

LINK_MD_RE = re.compile(r"\[([^\]\n]{1,300}?)\]\(([^)\s]{1,400}?)\)")
WIKI_LINK_RE = re.compile(r"\[\[([^\]\n]{1,200}?)\]\]")
TAG_RE = re.compile(r"(?:^|[\s(\[])#([A-Za-z][\w/-]{1,60})\b")
FRONTMATTER_RE = re.compile(r"\A---\n(.*?)\n---\n", re.DOTALL)
HEADING_LINE_RE = re.compile(r"^#{1,6}\s")
ID_RE = re.compile(r"\b(BUG-\d{3}|FEAT-\d{3}|CP-\d{3}|ADR-\d{3})\b")


def detect_type(path: Path) -> str:
    s = str(path).replace("\\", "/")
    name = path.name
    if "/wms-known-issues/" in s and "BUG-" in s:
        return "bug"
    if "/wms-incorporated-features/" in s and "FEAT-" in s:
        return "feat"
    if "/customer-open-cases/" in s:
        return "cp-open"
    if "/customer-closed-cases/" in s:
        return "cp-closed"
    if "/debuged-cases/" in s and ("CP-" in name or "/CP-" in s):
        return "cp-legacy"
    if "/decisions/" in s:
        return "adr"
    if "/_proposals/" in s:
        return "proposal"
    if "/_processed/" in s:
        return "processed"
    if "/code-deep-flow/" in s:
        return "trace"
    if "/learnings/" in s:
        return "learning"
    if "/agent-context/" in s:
        return "agent-context"
    if "/data-seek-strategy/" in s:
        return "data-strategy"
    if "/wms-specific-process-flow/" in s:
        return "wms-flow"
    if "/casos-observados/" in s:
        return "caso-reserva"
    if "/clients/" in s or "/client-index/" in s:
        return "client"
    if "/wms-db-brain/" in s:
        return "db-brain"
    if "/_index/" in s:
        return "index"
    if "/_inbox/" in s:
        return "inbox"
    if "/heat-map-params/" in s:
        return "heat-map"
    if "/wms-brain-client/" in s:
        return "brain-client"
    if "/test-scenarios/" in s or "/wms-test-natural-cases/" in s:
        return "test-scenario"
    if "/outputs/" in s:
        return "output"
    if "/queries/" in s:
        return "query"
    return "other"


def parse_frontmatter(content: str):
    m = FRONTMATTER_RE.match(content)
    if not m:
        return {}, content
    raw = m.group(1)
    fm = {}
    current_key = None
    for line in raw.split("\n"):
        if not line.strip():
            current_key = None
            continue
        if not line.startswith((" ", "-", "\t")) and ":" in line:
            k, _, v = line.partition(":")
            k = k.strip()
            v = v.strip()
            if v.startswith("[") and v.endswith("]"):
                items = [x.strip().strip("\"'") for x in v[1:-1].split(",") if x.strip()]
                fm[k] = items
                current_key = None
            elif v == "":
                fm[k] = []
                current_key = k
            else:
                fm[k] = v.strip("\"'")
                current_key = None
        elif line.lstrip().startswith("-") and current_key is not None:
            item = line.lstrip().lstrip("-").strip().strip("\"'")
            if not isinstance(fm.get(current_key), list):
                fm[current_key] = []
            fm[current_key].append(item)
    return fm, content[m.end():]


def extract_tags(body: str):
    tags = set()
    in_code_fence = False
    for line in body.split("\n"):
        # Toggle code fence
        if line.lstrip().startswith("```"):
            in_code_fence = not in_code_fence
            continue
        if in_code_fence:
            continue
        if HEADING_LINE_RE.match(line):
            continue
        if line.startswith(("    ", "\t")):
            continue
        for m in TAG_RE.finditer(line):
            t = m.group(1)
            if t and not t.isdigit() and len(t) > 1:
                tags.add(t)
    return tags


def resolve_link(target: str, current_file: Path, brain_root: Path):
    if not target or target.startswith(("http://", "https://", "mailto:", "ftp://")):
        return None, False
    target = target.split("#", 1)[0].split("?", 1)[0].strip().strip("<>`")
    if not target:
        return None, False
    if target.startswith("/"):
        candidate = (brain_root.parent / target.lstrip("/")).resolve()
    else:
        candidate = (current_file.parent / target).resolve()
    candidates_to_try = [candidate]
    if not candidate.suffix:
        candidates_to_try.append(candidate.with_name(candidate.name + ".md"))
    for c in candidates_to_try:
        if c.exists():
            try:
                rel = c.relative_to(brain_root)
                return str(rel).replace("\\", "/"), False
            except ValueError:
                return None, False
    try:
        rel_broken = candidate.relative_to(brain_root)
        return str(rel_broken).replace("\\", "/"), True
    except ValueError:
        return None, False


def slugify(s: str) -> str:
    return re.sub(r"[^a-z0-9]+", "-", s.lower()).strip("-")


def find_node_by_id(nodes: dict, target_id: str):
    """Busca un nodo cuyo frontmatter `id` coincida, o cuyo path contenga el id."""
    target_id = target_id.strip().strip("\"'")
    if not target_id:
        return None
    target_lower = target_id.lower()
    for rel, n in nodes.items():
        if str(n.get("id_fm", "")).lower() == target_lower:
            return rel
    for rel in nodes.keys():
        if target_lower in rel.lower():
            return rel
    return None


def add_virtual_node(virtual_nodes: dict, kind: str, name: str):
    """Crea (o reusa) un nodo virtual del tipo dado. Devuelve el id (path-like)."""
    name = str(name).strip().strip("\"'")
    if not name:
        return None
    slug = slugify(name)
    if not slug:
        return None
    vid = f"_virtual/{kind}/{slug}"
    if vid not in virtual_nodes:
        virtual_nodes[vid] = {
            "id": vid,
            "label": name,
            "tipo": f"v-{kind}",
            "tags": [],
            "clientes": [],
            "size_bytes": 0,
            "frontmatter_keys": [],
            "estado": "",
            "severidad": "",
            "in_degree": 0,
            "out_degree": 0,
            "is_virtual": True,
            "virtual_kind": kind,
        }
    return vid


def main():
    if not BRAIN_ROOT.exists():
        print(f"ERROR: brain root no existe: {BRAIN_ROOT}", file=sys.stderr)
        sys.exit(1)

    files = []
    for pat in ("*.md", "*.yml", "*.yaml"):
        files.extend(BRAIN_ROOT.rglob(pat))
    files = sorted(set(files))

    nodes = {}
    edges = []
    broken = []
    virtual_nodes = {}

    # Pasada 1: cargar todos los nodos con su frontmatter parseado
    for f in files:
        rel = str(f.relative_to(BRAIN_ROOT)).replace("\\", "/")
        try:
            content = f.read_text(encoding="utf-8", errors="replace")
        except Exception:
            content = ""
        fm, body = parse_frontmatter(content)
        tipo = (fm.get("tipo") or fm.get("type") or detect_type(f)).strip()

        tags = set()
        fm_tags = fm.get("tags")
        if isinstance(fm_tags, list):
            tags.update(t.strip() for t in fm_tags if t.strip())
        elif isinstance(fm_tags, str):
            tags.update(t.strip() for t in fm_tags.split(",") if t.strip())
        tags.update(extract_tags(body))

        clientes = []
        fm_cli = fm.get("clientes") or fm.get("cliente")
        if isinstance(fm_cli, list):
            clientes = [c.strip() for c in fm_cli if c.strip()]
        elif isinstance(fm_cli, str):
            clientes = [c.strip() for c in fm_cli.split(",") if c.strip()]

        nodes[rel] = {
            "id": rel,
            "label": f.stem,
            "tipo": tipo,
            "tags": sorted(tags),
            "clientes": clientes,
            "size_bytes": f.stat().st_size,
            "frontmatter_keys": sorted(fm.keys()),
            "estado": fm.get("estado", ""),
            "severidad": fm.get("severidad", ""),
            "id_fm": fm.get("id", ""),
            "frontmatter": fm,
            "_body": body,
            "_path": f,
            "in_degree": 0,
            "out_degree": 0,
        }

    # Pasada 2: extraer aristas explicitas (links markdown) y wiki-links
    for rel, n in list(nodes.items()):
        body = n["_body"]
        f = n["_path"]
        for m in LINK_MD_RE.finditer(body):
            label = m.group(1)
            target = m.group(2)
            resolved, is_broken = resolve_link(target, f, BRAIN_ROOT)
            if resolved is None:
                continue
            if is_broken:
                broken.append({"from": rel, "target_raw": target, "target_resolved": resolved})
                continue
            edges.append({
                "source": rel,
                "target": resolved,
                "label": label[:50].replace("`", ""),
                "kind": "md-link",
            })
        for m in WIKI_LINK_RE.finditer(body):
            raw = m.group(1).split("|")[0].strip()
            base = raw.lower()
            target_rel = None
            for cand_rel, cand in nodes.items():
                if cand["label"].lower() == base or cand_rel.split("/")[-1].lower() == base + ".md":
                    target_rel = cand_rel
                    break
            if target_rel:
                edges.append({
                    "source": rel,
                    "target": target_rel,
                    "label": raw[:50],
                    "kind": "wiki-link",
                })

    # Pasada 3: aristas IMPLICITAS desde frontmatter
    fm_link_keys = {
        "materializa_bug": "bug",
        "materializa_bugs": "bug",
        "bug_raiz": "bug",
        "bug_relacionado": "bug",
        "bugs_relacionados": "bug",
        "casos_relacionados": "case",
        "caso_anchor": "case",
        "casos_anchor": "case",
        "relacionado_con": "rel",
        "relacionados": "rel",
        "feats_relacionados": "feat",
        "feat_relacionado": "feat",
        "trazas_relacionadas": "trace",
        "traza_relacionada": "trace",
        "adrs_relacionados": "adr",
    }
    fm_virtual_keys = {
        # frontmatter key -> kind del nodo virtual
        "cliente": "cliente",
        "clientes": "cliente",
        "modulo": "modulo",
        "modulos": "modulo",
        "rama": "rama",
        "ramas": "rama",
        "ramas_afectadas": "rama",
        "autor": "autor",
        "autores": "autor",
    }

    for rel, n in nodes.items():
        fm = n["frontmatter"]
        if not fm:
            continue

        # 3.a: links a otros archivos por id
        for key, edge_kind in fm_link_keys.items():
            val = fm.get(key)
            if not val:
                continue
            targets = val if isinstance(val, list) else [val]
            for t in targets:
                t = str(t).strip().strip("\"'")
                if not t:
                    continue
                target_rel = find_node_by_id(nodes, t)
                if target_rel and target_rel != rel:
                    edges.append({
                        "source": rel,
                        "target": target_rel,
                        "label": f"{key}",
                        "kind": f"fm-{edge_kind}",
                    })

        # 3.b: nodos virtuales (cliente, modulo, rama, autor)
        for key, vkind in fm_virtual_keys.items():
            val = fm.get(key)
            if not val:
                continue
            targets = val if isinstance(val, list) else [val]
            for t in targets:
                vid = add_virtual_node(virtual_nodes, vkind, t)
                if vid:
                    edges.append({
                        "source": rel,
                        "target": vid,
                        "label": f"{key}:{t}",
                        "kind": f"v-{vkind}",
                    })

    # Pasada 4: tambien inferir aristas desde menciones a IDs en el cuerpo
    # (solo para nodos tipo bug/feat/cp/cp-legacy/trace que tienen IDs en su nombre)
    # Esto hace que un texto que diga "ver BUG-001" cree una arista al BUG-001
    body_id_edges = 0
    for rel, n in nodes.items():
        body = n["_body"]
        if not body or len(body) > 200000:
            continue
        own_id = n.get("id_fm") or ""
        for m in ID_RE.finditer(body):
            mentioned = m.group(1)
            if mentioned == own_id:
                continue
            target_rel = find_node_by_id(nodes, mentioned)
            if target_rel and target_rel != rel:
                # evitar duplicar si ya existe arista por link explicito o frontmatter
                edges.append({
                    "source": rel,
                    "target": target_rel,
                    "label": mentioned,
                    "kind": "body-mention",
                })
                body_id_edges += 1

    # Mergear nodos virtuales
    nodes.update(virtual_nodes)

    # Limpiar campos auxiliares antes de serializar
    for n in nodes.values():
        n.pop("_body", None)
        n.pop("_path", None)
        n.pop("frontmatter", None)

    # Calcular grados
    for e in edges:
        if e["source"] in nodes:
            nodes[e["source"]]["out_degree"] += 1
        if e["target"] in nodes:
            nodes[e["target"]]["in_degree"] += 1

    # Estadisticas
    type_counts = {}
    edge_kind_counts = {}
    for n in nodes.values():
        type_counts[n["tipo"]] = type_counts.get(n["tipo"], 0) + 1
    for e in edges:
        edge_kind_counts[e["kind"]] = edge_kind_counts.get(e["kind"], 0) + 1

    sorted_by_degree = sorted(
        nodes.values(),
        key=lambda n: -(n["in_degree"] + n["out_degree"]),
    )
    top20 = [
        {
            "id": n["id"],
            "label": n["label"],
            "tipo": n["tipo"],
            "in": n["in_degree"],
            "out": n["out_degree"],
            "total": n["in_degree"] + n["out_degree"],
        }
        for n in sorted_by_degree[:20]
    ]

    orphans = [n["id"] for n in nodes.values() if (n["in_degree"] + n["out_degree"]) == 0]
    tag_counts = {}
    for n in nodes.values():
        for t in n["tags"]:
            tag_counts[t] = tag_counts.get(t, 0) + 1
    top_tags = sorted(tag_counts.items(), key=lambda x: -x[1])[:30]

    out = {
        "nodes": list(nodes.values()),
        "edges": edges,
        "meta": {
            "generated_at": datetime.now(timezone.utc).isoformat(timespec="seconds"),
            "brain_root": str(BRAIN_ROOT),
            "total_nodes": len(nodes),
            "total_edges": len(edges),
            "type_counts": type_counts,
            "edge_kind_counts": edge_kind_counts,
            "top20_by_degree": top20,
            "orphan_count": len(orphans),
            "broken_link_count": len(broken),
            "virtual_node_count": len(virtual_nodes),
            "body_mention_edges": body_id_edges,
            "top_tags": top_tags,
        },
        "diagnostics": {
            "orphans": orphans,
            "broken_links": broken[:200],
        },
    }

    OUT_JSON.write_text(json.dumps(out, indent=2, ensure_ascii=False), encoding="utf-8")

    print(f"OK. Output -> {OUT_JSON}")
    print()
    print(f"=== ESTADISTICAS ===")
    print(f"Total nodos:       {len(nodes)}  (de los cuales {len(virtual_nodes)} son virtuales)")
    print(f"Total aristas:     {len(edges)}")
    print(f"Nodos huerfanos:   {len(orphans)}")
    print(f"Links rotos:       {len(broken)}")
    print(f"Body mention edges:{body_id_edges}  (menciones de BUG-NNN/CP-NNN/etc en el texto)")
    print()
    print(f"=== ARISTAS POR TIPO ===")
    for k, c in sorted(edge_kind_counts.items(), key=lambda x: -x[1]):
        print(f"  {c:5d}  {k}")
    print()
    print(f"=== CONTEO POR TIPO DE NODO ===")
    for t, c in sorted(type_counts.items(), key=lambda x: -x[1]):
        print(f"  {c:5d}  {t}")
    print()
    print(f"=== TOP 20 NODOS MAS CONECTADOS ===")
    for n in top20:
        print(f"  in={n['in']:3d} out={n['out']:3d} total={n['total']:3d}  [{n['tipo']:14s}]  {n['id']}")


if __name__ == "__main__":
    main()
