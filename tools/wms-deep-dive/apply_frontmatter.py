#!/usr/bin/env python3
"""
F6 — Aplicar frontmatter mínimo (id/tipo/estado/clientes/ramas/modulo/tags) a
los archivos .md del brain que no tienen frontmatter, RESPETANDO carpetas con
protocolos propios.

Uso:
    python3 tools/wms-deep-dive/apply_frontmatter.py            # dry-run
    python3 tools/wms-deep-dive/apply_frontmatter.py --apply    # escribe
"""
import re
import sys
import json
from pathlib import Path
from collections import defaultdict

ROOT = Path(__file__).resolve().parent.parent.parent  # wms-brain/
BRAIN = ROOT / "brain"

# Patrones (regex sobre rel_path POSIX) a EXCLUIR — formatos propios o protocolos
EXCLUDE_REGEX = [
    re.compile(r"^wms-brain-client/"),               # protocolo Q/A/L versionado
    re.compile(r"^_conventions/"),                   # convenciones con formato propio
    re.compile(r"^skills/"),                         # skills Replit + sub-references propios
    re.compile(r"^tasks-historicas/"),               # task-N formato propio
    re.compile(r"^outputs/"),                        # outputs generados con formato propio
    re.compile(r"^architecture/adr/ADR-"),           # ADRs con formato propio
    re.compile(r"^learnings/L-"),                    # learnings protocolVersion
]

# Mapeo carpeta top-level → tipo
DIR_TO_TIPO = {
    "agent-context": "agent-context",
    "architecture": "architecture",
    "brain-map": "brain-map",
    "clients": "client",
    "code-deep-flow": "code-deep-flow",
    "customer-closed-cases": "cp-closed",
    "customer-open-cases": "cp-open",
    "ddl-funcional": "ddl-funcional",
    "debuged-cases": "debuged-case",
    "decisions": "decision",
    "entities": "entity",
    "fingerprint": "fingerprint",
    "heat-map-params": "heat-map-params",
    "_inbox": "inbox",
    "_index": "index",
    "learnings": "learning",
    "naked-erik-anatomy": "naked-erik",
    "_proposals": "proposal",
    "sendero-producto": "sendero-producto",
    "skills": "skill-doc",
    "sql-catalog": "sql-catalog",
    "test-scenarios": "test-scenario",
    "wms-agent": "wms-agent",
    "wms-incorporated-features": "feat",
    "wms-known-issues": "known-issue",
    "wms-specific-process-flow": "process-flow",
    "wms-test-natural-cases": "wms-test-natural-case",
    "outputs": "output",
    "_processed": "processed",
}

# Aliases de cliente (filename normalizado → cliente)
CLIENT_ALIASES = {
    "BYB": "byb", "BB": "byb",
    "CEALSA": "cealsa",
    "MAMPA": "mampa",
    "MERCOPAN": "mercopan",
    "MERHONSA": "merhonsa",
    "KILLIOS": "killios", "KILLIOS_2026": "killios", "K7": "killios",
    "IDEAL": "idealsa", "IDEALSA": "idealsa",
    "BECOFARMA": "becofarma",
    "CUMBRE": "cumbre",
}

# Ramas a buscar en contenido — solo nombres específicos no ambiguos.
# (Excluyo "byb" y "cealsa" sueltos: chocan con nombres de cliente.)
BRANCH_PATTERNS = [
    (re.compile(r"\bdev_2023(?:_estable)?\b", re.IGNORECASE), "dev_2023_estable"),
    (re.compile(r"\bdev_2028(?:_merge)?\b", re.IGNORECASE), "dev_2028_merge"),
    (re.compile(r"\bdev_2025\b", re.IGNORECASE), "dev_2025"),
    (re.compile(r"\bdev_102022\b", re.IGNORECASE), "dev_102022"),
    (re.compile(r"\b240Cealsa\b"), "240Cealsa"),
    (re.compile(r"\b240byb\b"), "240byb"),
    (re.compile(r"\b240ejc\b"), "240ejc"),
    # 240 sólo si habla de rama mobile explícitamente: "rama 240" o "branch 240"
    (re.compile(r"\b(?:rama|branch|HH)\s+240\b", re.IGNORECASE), "240"),
]

# Módulos a inferir por path o filename
MODULE_KEYWORDS = {
    "reservation": ["reservation", "reserva", "stock_res", "mi3"],
    "picking": ["picking"],
    "despacho": ["despacho"],
    "recepcion": ["recepcion", "receipt"],
    "ajuste": ["ajuste"],
    "inventario": ["inventario", "ciclico", "inventory"],
    "traslado": ["traslado", "transfer"],
    "pedido": ["pedido"],
    "implosion": ["implosion", "implode"],
    "explosion": ["explosion", "explode"],
    "license-plate": ["license_plate", "license-plate", "lp_"],
    "putaway": ["putaway", "ubicar", "ubicacion"],
}


def is_excluded(rel_path: str) -> bool:
    for rx in EXCLUDE_REGEX:
        if rx.search(rel_path):
            return True
    return False


def derive_id(rel_path: str) -> str:
    """ID = stem del filename, MAYÚSCULAS si parece código (tiene dígitos o
    guión-prefijo conocido), sino el stem tal cual."""
    p = Path(rel_path)
    stem = p.stem
    # Casos conocidos: ADR-NN, FEAT-NN, BUG-NN, Q-NN, A-NN, L-NN, CP-NN, RES-NN
    if re.match(r"^(ADR|FEAT|BUG|Q|A|L|CP|RES|REC|DESP|PROC|TR|TRAS|BUG|H\d+)[-_]", stem, re.IGNORECASE):
        return stem
    # Mantener formato original (puede ser CASO-01-1_IDEAL_..., 00-mapa-de-cajas, etc.)
    return stem


def derive_tipo(rel_path: str) -> str:
    parts = rel_path.split("/")
    # Archivo en raíz de brain/ → documentación general
    if len(parts) == 1:
        return "documentation"
    top = parts[0]
    return DIR_TO_TIPO.get(top, top)


def derive_clientes(rel_path: str, content: str) -> list:
    found = set()
    # Path: /por-cliente/<X>/ o /clients/<X>/ o /<x>/<file>
    parts = rel_path.split("/")
    for i, seg in enumerate(parts[:-1]):
        if seg in ("por-cliente", "clients") and i + 1 < len(parts):
            cand = parts[i + 1]
            # quitar .md si es el filename siguiente
            cand = cand.replace(".md", "")
            cand_up = cand.upper()
            if cand_up in CLIENT_ALIASES:
                found.add(CLIENT_ALIASES[cand_up])
            elif cand.lower() in (v.lower() for v in CLIENT_ALIASES.values()):
                found.add(cand.lower())
    # Filename
    stem = Path(rel_path).stem.upper()
    for alias, cli in CLIENT_ALIASES.items():
        if re.search(rf"(?<![A-Z]){alias}(?![A-Z])", stem):
            found.add(cli)
    # Cross-cliente: no asignar
    if "cross-cliente" in rel_path.lower():
        return []
    return sorted(found)


def derive_ramas(content: str) -> list:
    found = set()
    sample = content[:30000]  # primeras 30 KB
    for rx, label in BRANCH_PATTERNS:
        if rx.search(sample):
            found.add(label)
    # Si tiene dev_2028_merge no agregar 240 también a menos que sea explícito
    return sorted(found)


def derive_modulo(rel_path: str, content: str) -> list:
    found = set()
    rp_low = rel_path.lower()
    # Path-based
    for mod, kws in MODULE_KEYWORDS.items():
        for kw in kws:
            if f"/{kw}/" in rp_low or f"/{kw}." in rp_low or rp_low.endswith(f"/{kw}.md"):
                found.add(mod)
                break
    return sorted(found)


def derive_tags(rel_path: str, tipo: str, clientes: list, ramas: list, modulos: list) -> list:
    tags = [tipo]
    for c in clientes:
        tags.append(f"cliente/{c}")
    for m in modulos:
        tags.append(f"modulo/{m}")
    return tags


def derive_titulo(content: str) -> str:
    """Primer H1 (línea que arranca con `# `)."""
    for line in content.split("\n", 50):
        if line.startswith("# "):
            return line[2:].strip()
    return ""


def build_frontmatter(rel_path: str, content: str) -> dict:
    fm = {
        "id": derive_id(rel_path),
        "tipo": derive_tipo(rel_path),
        "estado": "vigente",
    }
    titulo = derive_titulo(content)
    if titulo and titulo != fm["id"]:
        fm["titulo"] = titulo
    clientes = derive_clientes(rel_path, content)
    if clientes:
        fm["clientes"] = clientes
    ramas = derive_ramas(content)
    if ramas:
        fm["ramas"] = ramas
    modulos = derive_modulo(rel_path, content)
    if modulos:
        fm["modulo"] = modulos
    tags = derive_tags(rel_path, fm["tipo"], clientes, ramas, modulos)
    if tags:
        fm["tags"] = tags
    return fm


def fm_to_yaml(fm: dict) -> str:
    """Serialización YAML manual minimalista (sin dependencia)."""
    lines = ["---"]
    for k, v in fm.items():
        if isinstance(v, list):
            if not v:
                continue
            inner = ", ".join(v)
            lines.append(f"{k}: [{inner}]")
        elif isinstance(v, str):
            # Strings con caracteres especiales: comillas
            if any(c in v for c in [":", "#", "{", "}", "[", "]", "&", "*", "!", "|", ">", "'", '"', "%", "@", "`"]):
                vesc = v.replace('"', '\\"')
                lines.append(f'{k}: "{vesc}"')
            else:
                lines.append(f"{k}: {v}")
        else:
            lines.append(f"{k}: {v}")
    lines.append("---")
    return "\n".join(lines) + "\n"


def main():
    apply = "--apply" in sys.argv

    todos = sorted(BRAIN.rglob("*.md"))
    sin_fm = []
    excluded = []
    already = []

    for p in todos:
        rel = str(p.relative_to(BRAIN))
        if is_excluded(rel):
            excluded.append(rel)
            continue
        try:
            with open(p, encoding="utf-8") as f:
                first = f.readline().rstrip("\n")
        except Exception:
            continue
        if first == "---":
            already.append(rel)
            continue
        sin_fm.append(rel)

    print(f"Total .md: {len(todos)}")
    print(f"  ya con frontmatter: {len(already)}")
    print(f"  excluidos por carpeta/patrón: {len(excluded)}")
    print(f"  candidatos a aplicar FM: {len(sin_fm)}")

    plan = []
    by_tipo = defaultdict(int)
    by_clientes = defaultdict(int)
    by_ramas = defaultdict(int)

    for rel in sin_fm:
        full = BRAIN / rel
        try:
            content = full.read_text(encoding="utf-8")
        except UnicodeDecodeError:
            content = full.read_text(encoding="latin-1")
        fm = build_frontmatter(rel, content)
        plan.append((rel, fm))
        by_tipo[fm["tipo"]] += 1
        for c in fm.get("clientes", []):
            by_clientes[c] += 1
        for r in fm.get("ramas", []):
            by_ramas[r] += 1

    print("\n=== Distribución por tipo ===")
    for t, n in sorted(by_tipo.items(), key=lambda x: -x[1]):
        print(f"  {n:>4}  {t}")
    print("\n=== Inferencia de cliente ===")
    for c, n in sorted(by_clientes.items(), key=lambda x: -x[1]):
        print(f"  {n:>4}  {c}")
    print("\n=== Inferencia de rama ===")
    for r, n in sorted(by_ramas.items(), key=lambda x: -x[1]):
        print(f"  {n:>4}  {r}")

    # Reporte detallado a JSON
    report = {
        "candidates": len(sin_fm),
        "excluded": len(excluded),
        "by_tipo": dict(by_tipo),
        "by_clientes": dict(by_clientes),
        "by_ramas": dict(by_ramas),
        "plan": [{"path": p, "fm": fm} for p, fm in plan],
        "excluded_files": excluded,
    }
    report_path = ROOT / "tools" / "wms-deep-dive" / "frontmatter_plan.json"
    report_path.write_text(json.dumps(report, indent=2, ensure_ascii=False, default=list), encoding="utf-8")
    print(f"\nPlan completo en: {report_path.relative_to(ROOT)}")

    if not apply:
        print("\n=== DRY-RUN ===")
        print("Para aplicar: python3 tools/wms-deep-dive/apply_frontmatter.py --apply")
        # Mostrar 5 muestras
        print("\n=== Muestras ===")
        for rel, fm in plan[:5]:
            print(f"\n--- {rel} ---")
            print(fm_to_yaml(fm).rstrip())
        return

    # APPLY
    print("\n=== APLICANDO ===")
    written = 0
    for rel, fm in plan:
        full = BRAIN / rel
        try:
            content = full.read_text(encoding="utf-8")
        except UnicodeDecodeError:
            content = full.read_text(encoding="latin-1")
        # Idempotencia: si ya empieza con --- (raro, pero por las dudas)
        if content.startswith("---\n"):
            continue
        new = fm_to_yaml(fm) + "\n" + content
        full.write_text(new, encoding="utf-8")
        written += 1
    print(f"Escritos: {written} archivos")


if __name__ == "__main__":
    main()
