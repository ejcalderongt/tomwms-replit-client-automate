#!/usr/bin/env python3
"""
scan-comments-tree-map v0.1.0 (beta)

Escanea un repo del WMS, extrae comentarios firmados con el patrón del equipo
(<INICIALES><YYYYMMDD>_<HHMM>(AM|PM)?: <body>), aplica scoring de utilidad y
emite 5 outputs para promover los útiles al brain.

Premisa: ver brain/scan-comments-tree-map/README.md
Patrón: ver brain/scan-comments-tree-map/PATTERN.md
Scoring: ver brain/scan-comments-tree-map/SCORING.md

Uso rápido:
    python scan.py --dry-run                    # validar regex
    python scan.py --repo /path/to/TOMWMS_BOF   # escaneo completo
"""
from __future__ import annotations

import argparse
import hashlib
import json
import os
import re
import sys
from collections import Counter, defaultdict
from dataclasses import dataclass, field, asdict
from datetime import datetime
from pathlib import Path
from typing import Any

try:
    import yaml
except ImportError:
    print("ERROR: pyyaml no instalado. Correr: pip install pyyaml", file=sys.stderr)
    sys.exit(2)


VERSION = "0.1.0-beta"

# ---------------------------------------------------------------------------
# Sintaxis de comentarios por extensión
# ---------------------------------------------------------------------------

# Map ext -> (single_line_marker, block_open, block_close)
COMMENT_SYNTAX: dict[str, tuple[str | None, str | None, str | None]] = {
    ".vb": ("'", None, None),
    ".cs": ("//", "/*", "*/"),
    ".java": ("//", "/*", "*/"),
    ".kt": ("//", "/*", "*/"),
    ".sql": ("--", "/*", "*/"),
    ".py": ("#", None, None),
    ".ts": ("//", "/*", "*/"),
    ".tsx": ("//", "/*", "*/"),
    ".js": ("//", "/*", "*/"),
    ".jsx": ("//", "/*", "*/"),
    ".vbproj": (None, "<!--", "-->"),
    ".csproj": (None, "<!--", "-->"),
    ".xml": (None, "<!--", "-->"),
}

SCANNABLE_EXT = set(COMMENT_SYNTAX.keys())


# ---------------------------------------------------------------------------
# Regex master (ver PATTERN.md)
# ---------------------------------------------------------------------------

PATTERN = re.compile(
    r"""
    ^                                       # inicio de línea (ya recortada al body del comentario)
    \s*
    [#]?                                    # decorador '#' opcional
    \s*
    (?P<initials>[A-Z]{2,4})                # iniciales 2-4 mayúsculas
    \s*
    (?P<date>\d{8})                         # YYYYMMDD
    \s*[_\-]?\s*
    (?P<time>\d{3,4})                       # HHMM o HMM
    \s*
    (?P<period>[AaPp][Mm])?                 # AM/PM opcional
    \s*[:;\-]?\s*
    (?P<body>.+?)                           # cuerpo
    \s*$
    """,
    re.VERBOSE,
)


# ---------------------------------------------------------------------------
# Casos para --dry-run
# ---------------------------------------------------------------------------

DRY_RUN_POSITIVE = [
    "#EJC20220224_0326AM: Identificar si se debe registrar en NAV el documento para BYB.",
    "EJC20220224_0326AM: Identificar...",
    "EJC20220224_326AM: Identificar...",
    "EJC202202240326AM: Identificar...",
    "EJC20220224-0326-AM: Identificar...",
    "EJC20220224_0326: Identificar...",
    "EJC20220224_0326am: identificar...",
    "EJC20220224_0326AM Identificar...",
    "#EJC20220224_0326AM; Identificar...",
    "MA20231115_1430PM: Validar tope de cantidad antes de reservar.",
    "MECR20240301_0900AM: SP modificado para devolver lote_numerico correlativo.",
    "CF20250620_1645: cambia política reabasto cuando bodega es nivel 0.",
    "GT20230815_1130AM: refactor de Push_To_NAV unificado para BECO y BYB.",
    "AT20240202_0815AM: cuidado al tocar este flag, K7 lo usa con ML.",
    "AG20221231_2359PM: cierre de año, ajuste manual stock_res CEALSA.",
]

DRY_RUN_NEGATIVE = [
    "TODO: agregar validación",
    "EJC: arreglar después",
    "EJC2022 ver con Erik",
    "comentario suelto sin firma",
    "Var x = 10",
    "// pieces of code",
    "1234567890",
    "EJC2022022 0326AM hueco",
]


# ---------------------------------------------------------------------------
# Estructuras
# ---------------------------------------------------------------------------

@dataclass
class Match:
    repo: str
    relpath: str
    line: int
    raw: str
    initials: str
    author_full: str
    date_iso: str
    time_human: str
    body: str
    score: int = 0
    score_breakdown: dict[str, Any] = field(default_factory=dict)
    context_before: list[str] = field(default_factory=list)
    context_after: list[str] = field(default_factory=list)
    is_noise: bool = False
    noise_reason: str = ""

    def body_hash(self) -> str:
        return hashlib.sha1(self.body.lower().strip().encode()).hexdigest()[:12]


# ---------------------------------------------------------------------------
# Carga de configs
# ---------------------------------------------------------------------------

def load_yaml(path: Path) -> dict:
    if not path.exists():
        return {}
    with path.open(encoding="utf-8") as f:
        return yaml.safe_load(f) or {}


def load_configs(config_dir: Path):
    return {
        "authors": load_yaml(config_dir / "authors.yml"),
        "boost": load_yaml(config_dir / "boost-keywords.yml"),
        "ignore": load_yaml(config_dir / "ignorelist.yml"),
    }


# ---------------------------------------------------------------------------
# Extracción de comentarios por archivo
# ---------------------------------------------------------------------------

def extract_comment_text(line: str, ext: str) -> str | None:
    """
    Devuelve el cuerpo del comentario sin el marker, o None si la línea no es comentario.
    Maneja solo single-line markers. Block comments multilínea se manejan aparte.
    """
    syntax = COMMENT_SYNTAX.get(ext)
    if not syntax:
        return None
    sl_marker, _, _ = syntax
    if sl_marker is None:
        return None
    stripped = line.lstrip()
    if not stripped.startswith(sl_marker):
        return None
    body = stripped[len(sl_marker):].lstrip()
    return body


def extract_block_comments(content: str, ext: str) -> list[tuple[int, str]]:
    """
    Devuelve [(line_number, body_line)] para cada línea dentro de bloques /* */ o <!-- -->.
    """
    syntax = COMMENT_SYNTAX.get(ext)
    if not syntax:
        return []
    _, b_open, b_close = syntax
    if not b_open:
        return []
    out = []
    lines = content.splitlines()
    in_block = False
    for i, line in enumerate(lines, start=1):
        if not in_block:
            idx = line.find(b_open)
            if idx >= 0:
                in_block = True
                rest = line[idx + len(b_open):]
                end_idx = rest.find(b_close)
                if end_idx >= 0:
                    body = rest[:end_idx].strip()
                    if body:
                        out.append((i, body))
                    in_block = False
                else:
                    body = rest.strip()
                    if body:
                        out.append((i, body))
        else:
            end_idx = line.find(b_close)
            if end_idx >= 0:
                body = line[:end_idx].strip().lstrip("*").strip()
                if body:
                    out.append((i, body))
                in_block = False
            else:
                body = line.strip().lstrip("*").strip()
                if body:
                    out.append((i, body))
    return out


# ---------------------------------------------------------------------------
# Match + parsing
# ---------------------------------------------------------------------------

def try_match(comment_body: str) -> dict | None:
    m = PATTERN.match(comment_body)
    if not m:
        return None
    return m.groupdict()


def parse_date(date_str: str) -> str:
    try:
        d = datetime.strptime(date_str, "%Y%m%d")
        return d.strftime("%Y-%m-%d")
    except ValueError:
        return f"INVALID:{date_str}"


def parse_time(time_str: str, period: str | None) -> str:
    t = time_str.zfill(4)
    hh, mm = t[:2], t[2:]
    pp = (period or "").upper()
    return f"{hh}:{mm}" + (f" {pp}" if pp else "")


def author_full(initials: str, authors_cfg: dict) -> str:
    entry = authors_cfg.get(initials)
    if entry:
        if isinstance(entry, dict):
            return entry.get("name", initials)
        return str(entry)
    return f"<unknown:{initials}>"


# ---------------------------------------------------------------------------
# Scoring
# ---------------------------------------------------------------------------

def score_match(m: Match, boost: dict, ignore: dict) -> None:
    body = m.body
    body_lower = body.lower()
    score = 0
    breakdown: dict[str, Any] = {}

    noise_keywords = [k.lower() for k in (ignore.get("noise_keywords") or [])]
    body_short = body.strip().lower()
    for nk in noise_keywords:
        if nk == body_short or (
            len(nk) > 3 and nk in body_lower and len(body) <= len(nk) + 15
        ):
            m.is_noise = True
            m.noise_reason = f"matched ignorelist keyword: '{nk}'"
            m.score = 0
            breakdown["forced_noise"] = nk
            m.score_breakdown = breakdown
            return

    if re.fullmatch(r"[\W\d\s]+", body):
        m.is_noise = True
        m.noise_reason = "body is only symbols/numbers"
        m.score = 0
        m.score_breakdown = {"forced_noise": "non-alpha-only"}
        return

    clients = boost.get("clients") or []
    found_clients = []
    for c in clients:
        if re.search(rf"\b{re.escape(c)}\b", body, re.IGNORECASE):
            found_clients.append(c)
    if found_clients:
        pts = min(3 * len(found_clients), 6)
        score += pts
        breakdown["clients"] = {"found": found_clients, "pts": pts}

    erps = boost.get("erps") or []
    found_erps = []
    for e in erps:
        if re.search(rf"\b{re.escape(e)}\b", body, re.IGNORECASE):
            found_erps.append(e)
    if found_erps:
        pts = min(2 * len(found_erps), 4)
        score += pts
        breakdown["erps"] = {"found": found_erps, "pts": pts}

    rule_kw = boost.get("rule_keywords") or []
    found_rules = []
    for r in rule_kw:
        if re.search(rf"\b{re.escape(r)}\b", body, re.IGNORECASE):
            found_rules.append(r)
    if found_rules:
        pts = min(2 * len(found_rules), 4)
        score += pts
        breakdown["rule_keywords"] = {"found": found_rules, "pts": pts}

    fix_kw = boost.get("fix_keywords") or []
    found_fix = [f for f in fix_kw if re.search(rf"\b{re.escape(f)}\b", body, re.IGNORECASE)]
    if found_fix:
        pts = min(len(found_fix), 2)
        score += pts
        breakdown["fix_keywords"] = {"found": found_fix, "pts": pts}

    cond_kw = boost.get("condition_keywords") or []
    found_cond = [c for c in cond_kw if re.search(rf"\b{re.escape(c)}\b", body, re.IGNORECASE)]
    if found_cond:
        pts = min(len(found_cond), 2)
        score += pts
        breakdown["condition_keywords"] = {"found": found_cond, "pts": pts}

    tables = boost.get("wms_tables") or []
    found_tables = [t for t in tables if re.search(rf"\b{re.escape(t)}\b", body, re.IGNORECASE)]
    if found_tables:
        pts = min(2 * len(found_tables), 4)
        score += pts
        breakdown["wms_tables"] = {"found": found_tables, "pts": pts}

    funcs = boost.get("wms_functions") or []
    found_funcs = [f for f in funcs if re.search(rf"\b{re.escape(f)}\b", body, re.IGNORECASE)]
    if found_funcs:
        pts = min(2 * len(found_funcs), 4)
        score += pts
        breakdown["wms_functions"] = {"found": found_funcs, "pts": pts}

    blen = len(body)
    if blen >= 60:
        score += 1
        breakdown["body_len_60"] = 1
    if blen >= 120:
        score += 1
        breakdown["body_len_120"] = 1
    if blen <= 20:
        score -= 2
        breakdown["body_len_short"] = -2
    if blen <= 10:
        score -= 3
        breakdown["body_len_very_short"] = -3

    try:
        year = int(m.date_iso.split("-")[0])
        if year < 2023:
            score += 1
            breakdown["historic"] = 1
    except Exception:
        pass

    has_client = bool(found_clients)
    has_erp = bool(found_erps)
    has_rule_or_func = bool(found_rules or found_funcs or found_tables)
    if has_client and has_erp and has_rule_or_func:
        score += 2
        breakdown["cross_signal_bonus"] = 2

    score = max(0, min(score, 15))
    m.score = score
    m.score_breakdown = breakdown


# ---------------------------------------------------------------------------
# Walk + scan
# ---------------------------------------------------------------------------

def walk_and_scan(repo_root: Path, configs: dict, max_context: int = 5) -> list[Match]:
    matches: list[Match] = []
    files_scanned = 0

    for root, dirs, files in os.walk(repo_root):
        dirs[:] = [d for d in dirs if d not in {".git", "node_modules", "bin", "obj", "packages"}]
        for fname in files:
            ext = Path(fname).suffix.lower()
            if ext not in SCANNABLE_EXT:
                continue
            path = Path(root) / fname
            try:
                content = path.read_text(encoding="utf-8", errors="replace")
            except Exception as e:
                print(f"WARN: no pude leer {path}: {e}", file=sys.stderr)
                continue
            files_scanned += 1
            relpath = str(path.relative_to(repo_root))
            lines = content.splitlines()

            for i, line in enumerate(lines, start=1):
                body = extract_comment_text(line, ext)
                if body is None:
                    continue
                parsed = try_match(body)
                if parsed is None:
                    continue
                date_iso = parse_date(parsed["date"])
                if date_iso.startswith("INVALID"):
                    continue
                time_h = parse_time(parsed["time"], parsed.get("period"))
                m = Match(
                    repo=str(repo_root),
                    relpath=relpath,
                    line=i,
                    raw=line.rstrip(),
                    initials=parsed["initials"],
                    author_full=author_full(parsed["initials"], configs["authors"]),
                    date_iso=date_iso,
                    time_human=time_h,
                    body=parsed["body"].strip(),
                    context_before=[ln.rstrip() for ln in lines[max(0, i - 1 - max_context):i - 1]],
                    context_after=[ln.rstrip() for ln in lines[i:i + max_context]],
                )
                score_match(m, configs["boost"], configs["ignore"])
                matches.append(m)

            for line_num, block_body in extract_block_comments(content, ext):
                parsed = try_match(block_body)
                if parsed is None:
                    continue
                date_iso = parse_date(parsed["date"])
                if date_iso.startswith("INVALID"):
                    continue
                time_h = parse_time(parsed["time"], parsed.get("period"))
                m = Match(
                    repo=str(repo_root),
                    relpath=relpath,
                    line=line_num,
                    raw=block_body,
                    initials=parsed["initials"],
                    author_full=author_full(parsed["initials"], configs["authors"]),
                    date_iso=date_iso,
                    time_human=time_h,
                    body=parsed["body"].strip(),
                )
                score_match(m, configs["boost"], configs["ignore"])
                matches.append(m)

    body_counts = Counter(m.body_hash() for m in matches)
    for m in matches:
        if body_counts[m.body_hash()] > 1 and not m.is_noise:
            m.score = max(0, m.score - 1)
            m.score_breakdown["duplicate_penalty"] = -1

    print(f"scan: {files_scanned} files scanned, {len(matches)} matches found", file=sys.stderr)
    return matches


# ---------------------------------------------------------------------------
# Outputs
# ---------------------------------------------------------------------------

def write_outputs(matches: list[Match], out_dir: Path, repo_root: Path,
                  threshold: int, configs: dict) -> None:
    out_dir.mkdir(parents=True, exist_ok=True)
    useful = [m for m in matches if m.score >= threshold and not m.is_noise]
    noise = [m for m in matches if m.score < threshold or m.is_noise]
    useful.sort(key=lambda m: (-m.score, m.relpath, m.line))
    unknown_authors = sum(1 for m in matches if m.author_full.startswith("<unknown:"))

    tree: dict[str, Any] = {}
    for m in matches:
        parts = m.relpath.split(os.sep)
        node = tree
        for p in parts[:-1]:
            node = node.setdefault(p, {})
        node.setdefault(parts[-1], []).append({
            k: v for k, v in asdict(m).items() if k not in {"repo"}
        })

    metadata = {
        "scanner_version": VERSION,
        "repo": str(repo_root),
        "scanned_at": datetime.utcnow().isoformat() + "Z",
        "total_matches": len(matches),
        "useful_count": len(useful),
        "noise_count": len(noise),
        "useful_threshold": threshold,
        "unknown_authors": unknown_authors,
        "clients_mentioned_distinct": sorted({
            c for m in matches for c in (m.score_breakdown.get("clients", {}) or {}).get("found", [])
        }),
        "score_distribution": dict(Counter(m.score for m in matches)),
    }

    (out_dir / "tree-map.json").write_text(
        json.dumps({"scan_metadata": metadata, "tree": tree}, ensure_ascii=False, indent=2),
        encoding="utf-8",
    )

    by_author = defaultdict(list)
    for m in matches:
        by_author[m.initials].append(m)
    lines_a = ["# Comentarios firmados por autor", "", f"Generado: {metadata['scanned_at']}", ""]
    for initials in sorted(by_author.keys()):
        lst = by_author[initials]
        lst.sort(key=lambda x: -x.score)
        full = lst[0].author_full
        lines_a.append(f"## {initials} — {full}  ({len(lst)} comentarios)")
        lines_a.append("")
        for m in lst[:10]:
            lines_a.append(f"- `{m.relpath}:{m.line}` (score {m.score}, {m.date_iso} {m.time_human})")
            lines_a.append(f"  > {m.body}")
        if len(lst) > 10:
            lines_a.append(f"- … y {len(lst) - 10} más (ver tree-map.json)")
        lines_a.append("")
    (out_dir / "by-author.md").write_text("\n".join(lines_a), encoding="utf-8")

    by_client = defaultdict(list)
    for m in matches:
        for c in (m.score_breakdown.get("clients", {}) or {}).get("found", []):
            by_client[c.upper()].append(m)
    lines_c = ["# Comentarios agrupados por cliente mencionado", "",
               f"Generado: {metadata['scanned_at']}", ""]
    for client in sorted(by_client.keys()):
        lst = by_client[client]
        lst.sort(key=lambda x: -x.score)
        lines_c.append(f"## {client}  ({len(lst)} comentarios)")
        lines_c.append("")
        for m in lst[:15]:
            lines_c.append(f"- `{m.relpath}:{m.line}` por **{m.initials}** "
                           f"({m.date_iso}) score {m.score}")
            lines_c.append(f"  > {m.body}")
        if len(lst) > 15:
            lines_c.append(f"- … y {len(lst) - 15} más")
        lines_c.append("")
    (out_dir / "by-client.md").write_text("\n".join(lines_c), encoding="utf-8")

    lines_u = ["# Useful Shortlist — comentarios para promover al brain", "",
               f"Generado: {metadata['scanned_at']}",
               f"Threshold de score: {threshold}",
               f"Total: {len(useful)} comentarios útiles", "",
               "| score | archivo:linea | autor | fecha | promote_to | body |",
               "|---|---|---|---|---|---|"]
    for m in useful:
        star = "★ " if m.score >= 5 else ""
        body_safe = m.body.replace("|", "\\|")
        lines_u.append(
            f"| {star}{m.score} | `{m.relpath}:{m.line}` | {m.initials} | {m.date_iso} | _____ | {body_safe} |"
        )
    lines_u.append("")
    lines_u.append("## Detalle expandido (top 50)")
    lines_u.append("")
    for m in useful[:50]:
        lines_u.append(f"### `{m.relpath}:{m.line}` — score {m.score}")
        lines_u.append(f"- **autor**: {m.initials} ({m.author_full})")
        lines_u.append(f"- **fecha**: {m.date_iso} {m.time_human}")
        lines_u.append(f"- **body**: {m.body}")
        lines_u.append(f"- **score breakdown**: `{json.dumps(m.score_breakdown, ensure_ascii=False)}`")
        if m.context_before or m.context_after:
            lines_u.append("- **contexto**:")
            lines_u.append("  ```")
            for cl in m.context_before:
                lines_u.append(f"  {cl}")
            lines_u.append(f"  >>> {m.raw}")
            for cl in m.context_after:
                lines_u.append(f"  {cl}")
            lines_u.append("  ```")
        lines_u.append("")
    (out_dir / "useful-shortlist.md").write_text("\n".join(lines_u), encoding="utf-8")

    lines_n = ["# Noise discarded — para auditar falsos negativos", "",
               f"Generado: {metadata['scanned_at']}",
               f"Total descartados: {len(noise)}", "",
               "**Si encontrás algo útil acá, ajustá `config/boost-keywords.yml` o `config/ignorelist.yml` "
               "y volvé a correr.**", "",
               "| score | archivo:linea | autor | razón | body |",
               "|---|---|---|---|---|"]
    noise_sorted = sorted(noise, key=lambda x: (-x.score, x.relpath, x.line))
    for m in noise_sorted[:500]:
        reason = m.noise_reason or f"score {m.score} < {threshold}"
        body_safe = m.body.replace("|", "\\|")[:200]
        lines_n.append(f"| {m.score} | `{m.relpath}:{m.line}` | {m.initials} | {reason} | {body_safe} |")
    if len(noise_sorted) > 500:
        lines_n.append("")
        lines_n.append(f"_… y {len(noise_sorted) - 500} más (truncado a 500). Ver tree-map.json para todo._")
    (out_dir / "noise-discarded.md").write_text("\n".join(lines_n), encoding="utf-8")

    print(f"\n✓ Escritos 5 outputs en {out_dir}/", file=sys.stderr)
    print(f"   total_matches:  {metadata['total_matches']}", file=sys.stderr)
    print(f"   useful:         {metadata['useful_count']}  ({metadata['useful_count']*100//max(1,metadata['total_matches'])}%)", file=sys.stderr)
    print(f"   noise:          {metadata['noise_count']}  ({metadata['noise_count']*100//max(1,metadata['total_matches'])}%)", file=sys.stderr)
    print(f"   unknown_authors: {metadata['unknown_authors']}", file=sys.stderr)
    print(f"   clientes mencionados: {', '.join(metadata['clients_mentioned_distinct']) or '(ninguno)'}", file=sys.stderr)


# ---------------------------------------------------------------------------
# Dry-run
# ---------------------------------------------------------------------------

def run_dry() -> int:
    print(f"scan-comments-tree-map v{VERSION} — dry-run\n")
    fail = 0
    print("Casos POSITIVOS (deben matchear):")
    for case in DRY_RUN_POSITIVE:
        m = PATTERN.match(case)
        if m:
            print(f"  ✓  {case[:80]}")
        else:
            print(f"  ✗  FALLO: {case}")
            fail += 1
    print("\nCasos NEGATIVOS (NO deben matchear):")
    for case in DRY_RUN_NEGATIVE:
        m = PATTERN.match(case)
        if m is None:
            print(f"  ✓  {case[:80]}")
        else:
            print(f"  ✗  FALLO (matcheó cuando no debía): {case}")
            fail += 1
    print()
    if fail == 0:
        print(f"✓ {len(DRY_RUN_POSITIVE)}/{len(DRY_RUN_POSITIVE)} positivos matchearon")
        print(f"✓ {len(DRY_RUN_NEGATIVE)}/{len(DRY_RUN_NEGATIVE)} negativos no matchearon")
        print("Regex está sano. Listo para correr contra repo real.")
        return 0
    print(f"✗ {fail} casos fallaron. Revisar PATTERN en scan.py o ajustar PATTERN.md.")
    return 1


# ---------------------------------------------------------------------------
# Main
# ---------------------------------------------------------------------------

def main():
    ap = argparse.ArgumentParser(description="Scan firmas de comentarios del WMS")
    ap.add_argument("--repo", help="Path al clon del repo (TOMWMS_BOF, TOMHH2025, ...)")
    ap.add_argument("--out", help="Directorio de outputs (default: ./outputs/<timestamp>)")
    ap.add_argument("--useful-threshold", type=int, default=3,
                    help="Score mínimo para útil (default 3)")
    ap.add_argument("--max-context-lines", type=int, default=5)
    ap.add_argument("--dry-run", action="store_true", help="Validar regex contra sample interno")
    ap.add_argument("--include-no-pattern", action="store_true",
                    help="(reservado para v0.2) listar también comentarios con autor pero sin fecha")
    args = ap.parse_args()

    if args.dry_run:
        return run_dry()

    if not args.repo:
        ap.error("--repo es requerido (o usar --dry-run)")

    repo_root = Path(args.repo).resolve()
    if not repo_root.exists():
        print(f"ERROR: repo no existe: {repo_root}", file=sys.stderr)
        return 2

    here = Path(__file__).resolve().parent
    configs = load_configs(here / "config")

    out_dir = Path(args.out) if args.out else here / "outputs" / datetime.now().strftime("%Y%m%d-%H%M%S")
    print(f"scan-comments-tree-map v{VERSION}", file=sys.stderr)
    print(f"  repo:     {repo_root}", file=sys.stderr)
    print(f"  out:      {out_dir}", file=sys.stderr)
    print(f"  threshold: {args.useful_threshold}", file=sys.stderr)

    matches = walk_and_scan(repo_root, configs, args.max_context_lines)
    write_outputs(matches, out_dir, repo_root, args.useful_threshold, configs)
    return 0


if __name__ == "__main__":
    sys.exit(main())
