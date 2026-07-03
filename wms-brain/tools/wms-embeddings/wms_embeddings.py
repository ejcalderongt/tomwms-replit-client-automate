from __future__ import annotations

import argparse
import datetime as dt
import hashlib
import json
import math
import os
import re
import time
import sys
from dataclasses import dataclass
from http.server import BaseHTTPRequestHandler, ThreadingHTTPServer
from pathlib import Path
from typing import Iterable

import urllib.request


DEFAULT_BASE_URL = "http://127.0.0.1:1234/v1"
PROMPT_VERSION = "wms-embeddings-federated-v2"
SUPPORTED_CODE_SUFFIXES = {
    ".vb",
    ".cs",
    ".java",
    ".sql",
    ".js",
    ".ts",
    ".json",
    ".xml",
    ".config",
    ".aspx",
    ".asmx",
    ".xaml",
    ".py",
    ".md",
    ".markdown",
    ".yml",
    ".yaml",
}


@dataclass
class Chunk:
    path: str
    file_type: str
    title: str
    text: str
    tags: list[str]
    project: str
    root: str
    language: str
    priority: int = 0


def find_repo_root(start: Path) -> Path:
    current = start.resolve()
    for candidate in [current, *current.parents]:
        if (candidate / "tools").exists():
            return candidate
    raise FileNotFoundError("No se encontro la raiz del repo.")


def discover_model(base_url: str, timeout: int) -> str | None:
    try:
        req = urllib.request.Request(f"{base_url.rstrip('/')}/models")
        with urllib.request.urlopen(req, timeout=timeout) as resp:
            payload = json.loads(resp.read().decode("utf-8"))
        data = payload.get("data") or []
        if data:
            return data[0].get("id")
    except Exception:
        return None
    return None


def openai_embeddings(base_url: str, model: str, inputs: list[str], timeout: int) -> list[list[float]]:
    payload = {"model": model, "input": inputs}
    data = json.dumps(payload).encode("utf-8")
    req = urllib.request.Request(
        f"{base_url.rstrip('/')}/embeddings",
        data=data,
        headers={"Content-Type": "application/json"},
        method="POST",
    )
    with urllib.request.urlopen(req, timeout=timeout) as resp:
        payload = json.loads(resp.read().decode("utf-8"))
    items = payload.get("data") or []
    return [item["embedding"] for item in items]


def openai_embeddings_batched(base_url: str, model: str, inputs: list[str], timeout: int, batch_size: int) -> list[list[float]]:
    embeddings: list[list[float]] = []
    for start in range(0, len(inputs), batch_size):
        batch = inputs[start : start + batch_size]
        batch_embeddings = openai_embeddings(base_url, model, batch, timeout)
        embeddings.extend(batch_embeddings)
    return embeddings


def normalize_text(text: str) -> str:
    return text.replace("\r\n", "\n").replace("\r", "\n")


def redact_sensitive(text: str) -> str:
    patterns = [
        (r"(WMS_EC2_DB_PASSWORD\s*=\s*)(.+)", r"\1[REDACTED]"),
        (r"(BRAIN_IMPORT_TOKEN\s*=\s*)(.+)", r"\1[REDACTED]"),
        (r"(Password\s*=\s*)([^;\n]+)", r"\1[REDACTED]"),
        (r"(?im)^(\s*definition\s*:\s*).*$", r"\1[REDACTED_SQL_DEFINITION]"),
    ]
    output = text
    for pattern, replacement in patterns:
        output = re.sub(pattern, replacement, output)
    return output


def parse_scalar(value: str) -> str:
    value = value.strip()
    if (value.startswith('"') and value.endswith('"')) or (value.startswith("'") and value.endswith("'")):
        return value[1:-1]
    return value


def load_federation_manifest(repo_root: Path) -> list[dict]:
    manifest = repo_root / "tools" / "wms-federation.yml"
    if not manifest.exists():
        return [
            {
                "name": "workspace",
                "path": str(repo_root),
                "include": ["brain", "tools"],
                "exclude": [".git", ".vs", "bin", "obj", "node_modules", "packages"],
            }
        ]

    projects: list[dict] = []
    current: dict[str, object] | None = None
    active_list: str | None = None

    for raw in manifest.read_text(encoding="utf-8-sig").splitlines():
        if not raw.strip() or raw.strip().startswith("#"):
            continue
        indent = len(raw) - len(raw.lstrip(" "))
        stripped = raw.strip()

        if indent == 2 and stripped.startswith("- "):
            current = {}
            projects.append(current)
            active_list = None
            tail = stripped[2:]
            if ":" in tail:
                key, value = tail.split(":", 1)
                current[key.strip()] = parse_scalar(value)
            continue

        if current is None:
            continue

        if indent == 4 and ":" in stripped and not stripped.startswith("- "):
            key, value = stripped.split(":", 1)
            key = key.strip()
            value = value.strip()
            if value:
                current[key] = parse_scalar(value)
                active_list = None
            else:
                current[key] = []
                active_list = key
            continue

        if indent == 6 and stripped.startswith("- ") and active_list:
            current.setdefault(active_list, [])
            current[active_list].append(parse_scalar(stripped[2:]))

    return projects


def project_priority(project: dict) -> int:
    try:
        return int(project.get("priority", 0))
    except Exception:
        return 0


def infer_tags(path: Path, title: str, text_lines: Iterable[str]) -> list[str]:
    joined = " ".join(text_lines).lower()
    stem = path.stem.lower()
    tags = {stem, title.lower()}
    for token in [
        "bof",
        "hh",
        "sql",
        "mi3",
        "picking",
        "recepcion",
        "verificacion",
        "inventario",
        "reserva",
        "packing",
        "portal",
        "webapi",
        "brain",
        "context",
        "agent",
    ]:
        if token in stem or token in joined or token in path.as_posix().lower():
            tags.add(token)
    return sorted(tags)


def should_include_relative(rel: Path, includes: list[str], excludes: set[str]) -> bool:
    if any(part in excludes for part in rel.parts):
        return False
    if not includes:
        return True
    rel_posix = rel.as_posix()
    return any(rel_posix.startswith(inc.rstrip("/")) or inc in rel.parts for inc in includes)


def classify_language(path: Path) -> str:
    return {
        ".vb": "vb",
        ".cs": "csharp",
        ".java": "java",
        ".sql": "sql",
        ".js": "javascript",
        ".ts": "typescript",
        ".json": "json",
        ".xml": "xml",
        ".config": "xml",
        ".aspx": "aspnet",
        ".asmx": "aspnet",
        ".xaml": "xaml",
        ".py": "python",
        ".md": "markdown",
        ".markdown": "markdown",
        ".yml": "yaml",
        ".yaml": "yaml",
    }.get(path.suffix.lower(), "text")


def chunk_markdown(path: Path, text: str, project: str, root: str, priority: int) -> list[Chunk]:
    chunks: list[Chunk] = []
    lines = normalize_text(text).splitlines()
    current_title = path.name
    current: list[str] = []

    def flush() -> None:
        nonlocal current
        body = "\n".join(current).strip()
        if body:
            chunks.append(Chunk(str(path), "md", current_title, body, infer_tags(path, current_title, current), project, root, "markdown", priority))
        current = []

    for line in lines:
        if line.startswith("#"):
            flush()
            current_title = line.lstrip("#").strip() or path.name
            continue
        if not line.strip() and current and current[-1].strip() == "":
            flush()
            continue
        current.append(line)
    flush()
    return chunks


def chunk_yaml(path: Path, text: str, project: str, root: str, priority: int) -> list[Chunk]:
    chunks: list[Chunk] = []
    lines = normalize_text(text).splitlines()
    current_title = path.name
    current: list[str] = []

    def flush() -> None:
        nonlocal current
        body = "\n".join(current).strip()
        if body:
            chunks.append(Chunk(str(path), "yml", current_title, body, infer_tags(path, current_title, current), project, root, "yaml", priority))
        current = []

    for line in lines:
        if re.match(r"^[A-Za-z0-9_-]+:\s*$", line):
            flush()
            current_title = line[:-1].strip()
            current.append(line)
            continue
        current.append(line)
    flush()
    return chunks


def chunk_code(path: Path, text: str, project: str, root: str, priority: int) -> list[Chunk]:
    lines = normalize_text(text).splitlines()
    chunks: list[Chunk] = []
    current: list[str] = []
    current_title = path.name
    markers = (
        "Public ",
        "Private ",
        "Protected ",
        "Friend ",
        "Class ",
        "Module ",
        "Function ",
        "Sub ",
        "CREATE ",
        "ALTER ",
        "SELECT ",
        "INSERT ",
        "UPDATE ",
        "DELETE ",
        "package ",
        "public class ",
        "class ",
        "interface ",
    )

    def flush() -> None:
        nonlocal current
        body = "\n".join(current).strip()
        if body:
            chunks.append(Chunk(str(path), "code", current_title, body, infer_tags(path, current_title, current), project, root, classify_language(path), priority))
        current = []

    for line in lines:
        stripped = line.strip()
        if stripped.startswith(markers):
            flush()
            current_title = stripped[:120]
        current.append(line)
    flush()
    return chunks


def load_chunks(repo_root: Path) -> list[Chunk]:
    chunks: list[Chunk] = []
    for project in load_federation_manifest(repo_root):
        project_name = str(project.get("name", "unknown"))
        project_root = Path(str(project.get("path", repo_root)))
        priority = project_priority(project)
        includes = [str(item) for item in project.get("include") or []]
        excludes = {str(item) for item in project.get("exclude") or []}
        if not project_root.exists():
            continue

        for path in project_root.rglob("*"):
            if not path.is_file():
                continue
            rel = path.relative_to(project_root)
            if not should_include_relative(rel, includes, excludes):
                continue
            if path.suffix.lower() not in SUPPORTED_CODE_SUFFIXES and path.name.lower() not in {"readme", "license"}:
                continue
            try:
                text = path.read_text(encoding="utf-8-sig", errors="replace")
            except Exception:
                continue
            text = redact_sensitive(text)
            if path.suffix.lower() in {".md", ".markdown"}:
                chunks.extend(chunk_markdown(rel, text, project_name, str(project_root), priority))
            elif path.suffix.lower() in {".yml", ".yaml"}:
                chunks.extend(chunk_yaml(rel, text, project_name, str(project_root), priority))
            else:
                chunks.extend(chunk_code(rel, text, project_name, str(project_root), priority))
    return chunks


def cosine_similarity(a: list[float], b: list[float]) -> float:
    dot = sum(x * y for x, y in zip(a, b))
    na = math.sqrt(sum(x * x for x in a))
    nb = math.sqrt(sum(y * y for y in b))
    if na == 0 or nb == 0:
        return 0.0
    return dot / (na * nb)


def chunk_digest(chunk: Chunk) -> str:
    material = "\n".join([PROMPT_VERSION, chunk.project, chunk.root, chunk.path, chunk.file_type, chunk.title, ",".join(chunk.tags), chunk.text])
    return hashlib.sha256(material.encode("utf-8")).hexdigest()


def load_existing_rows(repo_root: Path) -> list[dict]:
    chunks_path = repo_root / "tools" / "wms-embeddings" / "out" / "chunks.jsonl"
    if not chunks_path.exists():
        return []
    rows: list[dict] = []
    for line in chunks_path.read_text(encoding="utf-8").splitlines():
        if line.strip():
            rows.append(json.loads(line))
    return rows


def load_state(repo_root: Path) -> dict:
    state_path = repo_root / "tools" / "wms-embeddings" / "out" / "state.json"
    if not state_path.exists():
        return {}
    try:
        return json.loads(state_path.read_text(encoding="utf-8"))
    except Exception:
        return {}


def write_state(repo_root: Path, state: dict) -> None:
    out_dir = repo_root / "tools" / "wms-embeddings" / "out"
    out_dir.mkdir(parents=True, exist_ok=True)
    (out_dir / "state.json").write_text(json.dumps(state, ensure_ascii=False, indent=2), encoding="utf-8")


def load_index(repo_root: Path) -> list[dict]:
    return load_existing_rows(repo_root)


def load_index_with_embeddings(repo_root: Path) -> list[dict]:
    return [row for row in load_existing_rows(repo_root) if row.get("embedding")]


def write_index_rows(repo_root: Path, rows: list[dict], model: str, base_url: str, incremental: bool) -> Path:
    out_dir = repo_root / "tools" / "wms-embeddings" / "out"
    out_dir.mkdir(parents=True, exist_ok=True)
    chunks_path = out_dir / "chunks.jsonl"
    index_path = out_dir / "index.json"

    with chunks_path.open("w", encoding="utf-8") as fh:
        for idx, row in enumerate(rows):
            row["chunk_id"] = idx
            fh.write(json.dumps(row, ensure_ascii=False) + "\n")

    index_path.write_text(
        json.dumps(
            {
                "generated_at": dt.datetime.now().astimezone().isoformat(timespec="seconds"),
                "prompt_version": PROMPT_VERSION,
                "model": model,
                "base_url": base_url,
                "chunks": len(rows),
                "incremental": incremental,
            },
            ensure_ascii=False,
            indent=2,
        ),
        encoding="utf-8",
    )
    return index_path


def write_chunks_rows(repo_root: Path, rows: list[dict]) -> None:
    out_dir = repo_root / "tools" / "wms-embeddings" / "out"
    out_dir.mkdir(parents=True, exist_ok=True)
    chunks_path = out_dir / "chunks.jsonl"
    with chunks_path.open("w", encoding="utf-8") as fh:
        for idx, row in enumerate(rows):
            row["chunk_id"] = idx
            fh.write(json.dumps(row, ensure_ascii=False) + "\n")


def index_chunks(repo_root: Path, base_url: str, model: str, timeout: int, incremental: bool, batch_size: int = 8) -> tuple[Path, int]:
    out_dir = repo_root / "tools" / "wms-embeddings" / "out"
    out_dir.mkdir(parents=True, exist_ok=True)
    previous = {row.get("digest"): row for row in load_existing_rows(repo_root)} if incremental else {}
    state = load_state(repo_root) if incremental else {}
    previous_files = state.get("files") or {}
    chunks = load_chunks(repo_root)

    rows: list[dict] = []
    pending_texts: list[str] = []
    pending_rows: list[dict] = []
    current_files: dict[str, dict] = {}

    for chunk in chunks:
        digest = chunk_digest(chunk)
        file_key = f"{chunk.project}|{chunk.root}|{chunk.path}"
        current_files[file_key] = {"digest": digest, "language": chunk.language, "priority": chunk.priority}
        row = {
            "digest": digest,
            "path": chunk.path,
            "file_type": chunk.file_type,
            "title": chunk.title,
            "tags": chunk.tags,
            "text": chunk.text,
            "project": chunk.project,
            "root": chunk.root,
            "language": chunk.language,
            "priority": chunk.priority,
        }
        cached = previous.get(digest)
        if incremental and previous_files.get(file_key, {}).get("digest") == digest and cached and cached.get("embedding"):
            row["embedding"] = cached["embedding"]
            rows.append(row)
            continue
        pending_texts.append(f"{chunk.project}\n{chunk.title}\n{chunk.path}\n{chunk.language}\n{', '.join(chunk.tags)}\n\n{chunk.text}")
        pending_rows.append(row)
        rows.append(row)

    if pending_texts:
        for start in range(0, len(pending_texts), batch_size):
            batch_texts = pending_texts[start : start + batch_size]
            batch_rows = pending_rows[start : start + batch_size]
            embeddings = openai_embeddings(base_url, model, batch_texts, timeout)
            for row, embedding in zip(batch_rows, embeddings):
                row["embedding"] = embedding
            write_chunks_rows(repo_root, rows)
            write_state(
                repo_root,
                {
                    "generated_at": dt.datetime.now().astimezone().isoformat(timespec="seconds"),
                    "model": model,
                    "base_url": base_url,
                    "chunks": len(rows),
                    "incremental": incremental,
                    "cache_hits": len(rows) - len(pending_rows[: start + len(batch_rows)]),
                    "cache_misses": len(pending_rows[: start + len(batch_rows)]),
                    "projects": sorted({row["project"] for row in rows}),
                    "files": current_files,
                },
            )

    index_path = write_index_rows(repo_root, rows, model, base_url, incremental)
    write_state(
        repo_root,
        {
            "generated_at": dt.datetime.now().astimezone().isoformat(timespec="seconds"),
            "model": model,
            "base_url": base_url,
            "chunks": len(rows),
            "incremental": incremental,
            "cache_hits": len(rows) - len(pending_rows),
            "cache_misses": len(pending_rows),
            "projects": sorted({row["project"] for row in rows}),
            "files": current_files,
        },
    )
    return index_path, len(rows)


def should_reindex(repo_root: Path) -> bool:
    state = load_state(repo_root)
    if not state:
        return True
    chunks_path = repo_root / "tools" / "wms-embeddings" / "out" / "chunks.jsonl"
    if not chunks_path.exists():
        return True
    return False


def search(rows: list[dict], query_embedding: list[float], top_k: int) -> list[dict]:
    scored = []
    for row in rows:
        score = cosine_similarity(query_embedding, row.get("embedding") or [])
        boosted_score = score + (float(row.get("priority", 0)) / 1000.0)
        scored.append({**row, "score": score, "boosted_score": boosted_score})
    scored.sort(key=lambda item: item.get("boosted_score", item["score"]), reverse=True)
    return scored[:top_k]


def search_local(repo_root: Path, query: str, top_k: int, base_url: str, model: str | None, timeout: int) -> list[dict]:
    rows = load_index_with_embeddings(repo_root)
    if not rows:
        return []
    resolved_model = model or discover_model(base_url, timeout)
    if not resolved_model:
        return []
    q_embedding = openai_embeddings(base_url, resolved_model, [query], timeout)[0]
    return search(rows, q_embedding, top_k)


def render_markdown(results: list[dict], query: str) -> str:
    lines = [f"# WMS Semantic Search", "", f"- query: {query}", ""]
    for item in results:
        lines.extend(
            [
                f"## {item['project']} :: {item['path']}",
                f"- score: {item['score']:.4f}",
                f"- title: {item['title']}",
                f"- language: {item.get('language', 'text')}",
                f"- tags: {', '.join(item.get('tags') or [])}",
                "",
                "```text",
                item["text"][:2500],
                "```",
                "",
            ]
        )
    return "\n".join(lines)


def escape_html(text: str) -> str:
    return (
        text.replace("&", "&amp;")
        .replace("<", "&lt;")
        .replace(">", "&gt;")
        .replace('"', "&quot;")
    )


def render_html(rows: list[dict], query: str) -> str:
    payload = json.dumps(rows, ensure_ascii=False)
    return f"""<!doctype html>
<html lang="es">
<head>
<meta charset="utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1" />
<title>WMS Federated Search</title>
<style>
body {{ font-family: system-ui, sans-serif; margin: 0; background: linear-gradient(180deg, #020617, #0f172a); color: #e2e8f0; }}
header {{ padding: 24px; border-bottom: 1px solid #1e293b; background: rgba(15, 23, 42, 0.95); position: sticky; top: 0; backdrop-filter: blur(10px); }}
main {{ padding: 24px; display: grid; gap: 16px; max-width: 1200px; margin: 0 auto; }}
.toolbar {{ display: grid; gap: 8px; grid-template-columns: 1fr auto; }}
input {{ width: 100%; padding: 14px 16px; border-radius: 12px; border: 1px solid #334155; background: #0b1220; color: #e2e8f0; }}
button {{ padding: 14px 18px; border: 0; border-radius: 12px; background: #2563eb; color: white; cursor: pointer; }}
.card {{ background: rgba(15, 23, 42, 0.9); border: 1px solid #1e293b; border-radius: 16px; padding: 16px; }}
.meta {{ color: #94a3b8; font-size: 12px; }}
.tags span {{ display: inline-block; margin: 4px 6px 0 0; padding: 2px 8px; border-radius: 999px; background: #1d4ed8; color: white; font-size: 12px; }}
pre {{ white-space: pre-wrap; word-break: break-word; background: #020617; padding: 12px; border-radius: 12px; overflow-x: auto; }}
h1,h2 {{ margin: 0 0 8px 0; }}
.hint {{ color: #94a3b8; font-size: 13px; }}
.pill {{ display:inline-block; margin-right:8px; padding:4px 10px; border-radius:999px; background:#111827; border:1px solid #334155; cursor:pointer; }}
</style>
</head>
<body>
<header>
  <h1>WMS Federated Search</h1>
  <div class="toolbar">
    <input id="q" value="{escape_html(query)}" placeholder="Buscar en todo el brain federado..." />
    <button onclick="runSearch()">Buscar</button>
  </div>
  <div class="hint">Busca entre proyectos, brain, documentos y código fuente indexado.</div>
</header>
<main>
  <section id="stats" class="hint"></section>
  <section id="filters"></section>
  <section id="results"></section>
</main>
<script>
const DATA = {payload};
function esc(s) {{
  return String(s).replaceAll('&','&amp;').replaceAll('<','&lt;').replaceAll('>','&gt;');
}}
function unique(arr) {{
  return [...new Set(arr)];
}}
function render(list) {{
  document.getElementById('stats').textContent = `${{list.length}} chunks visibles de ${{DATA.length}}`;
  const projects = unique(DATA.map(x => x.project || 'unknown'));
  document.getElementById('filters').innerHTML = projects.map(p => '<span class="pill" onclick="filterProject(\\'' + esc(p) + '\\')">' + esc(p) + '</span>').join('');
  const target = document.getElementById('results');
  target.innerHTML = list.map(item =>
    '<article class="card">' +
      '<div class="meta">' + esc(item.project || 'unknown') + ' · ' + esc(item.language || 'text') + ' · score ' + Number(item.score || 0).toFixed(4) + '</div>' +
      '<h2>' + esc(item.title) + '</h2>' +
      '<div class="meta">' + esc(item.path) + '</div>' +
      '<div class="tags">' + (item.tags || []).slice(0, 10).map(tag => '<span>' + esc(tag) + '</span>').join('') + '</div>' +
      '<pre>' + esc((item.text || '').slice(0, 3000)) + '</pre>' +
    '</article>'
  ).join('');
}}
function runSearch() {{
  const q = document.getElementById('q').value.trim().toLowerCase();
  const terms = q ? q.split(/\\s+/).filter(Boolean) : [];
  const list = DATA.filter(item => {{
    const hay = `${{item.project}} ${{item.path}} ${{item.title}} ${{(item.tags || []).join(' ')}} ${{item.text}} ${{item.language}}`.toLowerCase();
    return terms.every(t => hay.includes(t));
  }});
  render(list.slice(0, 100));
}}
function filterProject(project) {{
  const list = DATA.filter(item => (item.project || 'unknown') === project);
  render(list.slice(0, 100));
}}
document.getElementById('q').addEventListener('keydown', e => {{ if (e.key === 'Enter') runSearch(); }});
render(DATA.slice(0, 50));
</script>
</body>
</html>"""


def watch_loop(repo_root: Path, base_url: str, model: str | None, timeout: int, incremental: bool, interval: int) -> int:
    resolved_model = model or discover_model(base_url, timeout)
    if not resolved_model:
        print("No se pudo detectar modelo de embeddings en /models.")
        return 1
    last_sig = None
    print("Watcher activo. Ctrl+C para salir.")
    try:
        while True:
            chunks = load_chunks(repo_root)
            sig_material = "\n".join(sorted(chunk_digest(chunk) for chunk in chunks[:2000]))
            sig = hashlib.sha256(sig_material.encode("utf-8")).hexdigest()
            if sig != last_sig:
                index_path, count = index_chunks(repo_root, base_url, resolved_model, timeout, incremental)
                print(f"[{dt.datetime.now().strftime('%H:%M:%S')}] reindexado: {count} chunks -> {index_path}")
                last_sig = sig
            time.sleep(interval)
    except KeyboardInterrupt:
        return 0


def main() -> int:
    parser = argparse.ArgumentParser(description="WMS federated embeddings indexer")
    sub = parser.add_subparsers(dest="cmd", required=True)

    p_index = sub.add_parser("index")
    p_index.add_argument("--base-url", default=os.getenv("EMBEDDINGS_BASE_URL", DEFAULT_BASE_URL))
    p_index.add_argument("--model", default=os.getenv("EMBEDDINGS_MODEL"))
    p_index.add_argument("--timeout", type=int, default=120)
    p_index.add_argument("--batch-size", type=int, default=8)
    p_index.add_argument("--incremental", action="store_true", help="Reusar embeddings ya calculados por digest.")

    p_query = sub.add_parser("query")
    p_query.add_argument("query")
    p_query.add_argument("--top-k", type=int, default=5)
    p_query.add_argument("--base-url", default=os.getenv("EMBEDDINGS_BASE_URL", DEFAULT_BASE_URL))
    p_query.add_argument("--model", default=os.getenv("EMBEDDINGS_MODEL"))
    p_query.add_argument("--timeout", type=int, default=120)

    sub.add_parser("export-html")
    p_serve = sub.add_parser("serve-html")
    p_serve.add_argument("--host", default="127.0.0.1")
    p_serve.add_argument("--port", type=int, default=8787)

    p_watch = sub.add_parser("watch")
    p_watch.add_argument("--base-url", default=os.getenv("EMBEDDINGS_BASE_URL", DEFAULT_BASE_URL))
    p_watch.add_argument("--model", default=os.getenv("EMBEDDINGS_MODEL"))
    p_watch.add_argument("--timeout", type=int, default=120)
    p_watch.add_argument("--incremental", action="store_true", help="Reusar embeddings ya calculados por digest.")
    p_watch.add_argument("--interval", type=int, default=20)

    args = parser.parse_args()
    repo_root = find_repo_root(Path.cwd())
    out_dir = repo_root / "tools" / "wms-embeddings" / "out"

    if args.cmd == "index":
        model = args.model or discover_model(args.base_url, args.timeout)
        if not model:
            print("No se pudo detectar modelo de embeddings en /models.")
            return 1
        index_path, count = index_chunks(repo_root, args.base_url, model, args.timeout, args.incremental, args.batch_size)
        print(f"Index generado: {index_path}")
        print(f"Chunks: {count}")
        return 0

    if args.cmd == "query":
        results = search_local(repo_root, args.query, args.top_k, args.base_url, args.model, args.timeout)
        md = render_markdown(results, args.query)
        out_dir.mkdir(parents=True, exist_ok=True)
        (out_dir / "search-results.md").write_text(md, encoding="utf-8")
        print(md)
        return 0

    if args.cmd == "export-html":
        rows = load_index(repo_root)
        if not rows:
            print("No hay indice. Ejecuta primero index.")
            return 1
        html = render_html(rows, "WMS semantic search")
        out_dir.mkdir(parents=True, exist_ok=True)
        (out_dir / "search-results.html").write_text(html, encoding="utf-8")
        print(f"HTML generado: {out_dir / 'search-results.html'}")
        return 0

    if args.cmd == "serve-html":
        out_dir.mkdir(parents=True, exist_ok=True)
        html_path = out_dir / "search-results.html"
        if not html_path.exists():
            html_path.write_text(render_html(load_index(repo_root), "WMS semantic search"), encoding="utf-8")

        class Handler(BaseHTTPRequestHandler):
            def do_GET(self) -> None:
                if self.path in {"/", "/index.html"}:
                    content = html_path.read_text(encoding="utf-8")
                    self.send_response(200)
                    self.send_header("Content-Type", "text/html; charset=utf-8")
                    self.end_headers()
                    self.wfile.write(content.encode("utf-8"))
                    return
                self.send_response(404)
                self.end_headers()

            def log_message(self, format: str, *args) -> None:
                return

        server = ThreadingHTTPServer((args.host, args.port), Handler)
        print(f"Servidor HTML en http://{args.host}:{args.port}")
        try:
            server.serve_forever()
        except KeyboardInterrupt:
            server.shutdown()
        return 0

    if args.cmd == "watch":
        return watch_loop(repo_root, args.base_url, args.model, args.timeout, args.incremental, args.interval)

    return 1


if __name__ == "__main__":
    sys.exit(main())
