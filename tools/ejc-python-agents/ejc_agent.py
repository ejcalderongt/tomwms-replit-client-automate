from __future__ import annotations

import argparse
import datetime as dt
import re
import subprocess
import sys
from dataclasses import dataclass, field
from pathlib import Path
from typing import Iterable


@dataclass
class AgentConfig:
    name: str = "ejc-python-agent"
    version: str = "0.1"
    mode: str = "draft"
    code_roots: list[str] = field(default_factory=list)
    guardrails: list[str] = field(default_factory=list)
    data_seed_tables: list[str] = field(default_factory=list)
    ops_seed_targets: list[str] = field(default_factory=list)
    default_terms: list[str] = field(default_factory=list)


def find_repo_root(start: Path) -> Path:
    current = start.resolve()
    for candidate in [current, *current.parents]:
        if (candidate / "tools" / "ejc-python-agents").exists():
            return candidate
    raise FileNotFoundError("No se encontro tools/ejc-python-agents.")


def _strip_scalar(value: str) -> str:
    value = value.strip()
    if value.startswith("-"):
        value = value[1:].strip()
    if (value.startswith('"') and value.endswith('"')) or (
        value.startswith("'") and value.endswith("'")
    ):
        value = value[1:-1]
    return value


def _list_after(lines: list[str], index: int, parent_indent: int) -> list[str]:
    items: list[str] = []
    for raw in lines[index + 1 :]:
        if not raw.strip() or raw.lstrip().startswith("#"):
            continue
        indent = len(raw) - len(raw.lstrip(" "))
        if indent <= parent_indent:
            break
        stripped = raw.strip()
        if stripped.startswith("- "):
            items.append(_strip_scalar(stripped))
    return items


def load_agent_config(path: Path) -> AgentConfig:
    lines = path.read_text(encoding="utf-8-sig").splitlines()
    cfg = AgentConfig()
    section = ""
    subsection = ""

    for i, raw in enumerate(lines):
        stripped = raw.strip()
        if not stripped or stripped.startswith("#"):
            continue

        indent = len(raw) - len(raw.lstrip(" "))

        if indent == 0 and stripped.endswith(":"):
            section = stripped[:-1]
            subsection = ""
            continue

        if indent == 2 and stripped.endswith(":"):
            subsection = stripped[:-1]
            continue

        if stripped == "code_roots:" and section == "repo":
            cfg.code_roots = _list_after(lines, i, 2)
            continue
        if stripped == "guardrails:" and section == "repo":
            cfg.guardrails = _list_after(lines, i, 2)
            continue
        if stripped == "seed_tables:" and section == "lanes" and subsection == "data":
            cfg.data_seed_tables = _list_after(lines, i, 4)
            continue
        if stripped == "seed_targets:" and section == "lanes" and subsection == "operativity":
            cfg.ops_seed_targets = _list_after(lines, i, 4)
            continue
        if stripped == "default_terms:" and section == "search":
            cfg.default_terms = _list_after(lines, i, 2)
            continue

        if ":" in stripped and not stripped.startswith("- "):
            key, value = stripped.split(":", 1)
            key = key.strip()
            value = _strip_scalar(value)
            if section == "agent":
                if key == "name":
                    cfg.name = value
                elif key == "version":
                    cfg.version = value
                elif key == "mode":
                    cfg.mode = value
            continue

    return cfg


def run_rg(repo_root: Path, terms: Iterable[str], code_roots: list[str], limit: int = 120) -> list[str]:
    out: list[str] = []
    for term in terms:
        if not term.strip():
            continue
        for root in code_roots:
            try:
                cmd = ["rg", "-n", "-S", term, str(repo_root / root)]
                proc = subprocess.run(cmd, capture_output=True, text=True, check=False)
                if proc.stdout.strip():
                    lines = [ln for ln in proc.stdout.splitlines() if ln.strip()]
                    out.extend(lines[: max(1, limit // max(len(terms), 1))])
            except Exception:
                continue
        if len(out) >= limit:
            break
    return out[:limit]


def extract_ids(text: str) -> list[str]:
    found = re.findall(r"\b\d{4,}\b", text)
    # preserve order, unique
    seen: set[str] = set()
    ids: list[str] = []
    for value in found:
        if value not in seen:
            seen.add(value)
            ids.append(value)
    return ids


def build_sql_suggestions(ids: list[str], cfg: AgentConfig) -> list[str]:
    seed_doc = ids[0] if ids else "<IdDocumento>"
    seed_no_doc = ids[1] if len(ids) > 1 else "<No_Documento>"
    suggestions = [
        (
            "-- Encabezado OC\n"
            "SELECT e.IdOrdenCompraEnc,e.No_Documento,e.Referencia,e.IdEstadoOC,es.Nombre AS Estado,\n"
            "       e.IdTipoIngresoOC,ti.Nombre AS TipoIngreso,e.Enviado_A_ERP\n"
            "FROM trans_oc_enc e\n"
            "LEFT JOIN trans_oc_estado es ON es.IdEstadoOC=e.IdEstadoOC\n"
            "LEFT JOIN trans_oc_ti ti ON ti.IdTipoIngresoOC=e.IdTipoIngresoOC\n"
            f"WHERE e.IdOrdenCompraEnc={seed_doc} OR e.No_Documento='{seed_no_doc}';"
        ),
        (
            "-- Detalle OC\n"
            "SELECT d.IdOrdenCompraEnc,d.No_Linea,d.codigo_producto,d.cantidad,d.cantidad_recibida,\n"
            "       (d.cantidad-d.cantidad_recibida) AS pendiente\n"
            "FROM trans_oc_det d\n"
            f"WHERE d.IdOrdenCompraEnc={seed_doc}\n"
            "ORDER BY d.No_Linea;"
        ),
        (
            "-- Recepciones asociadas\n"
            "SELECT TOP 50 *\n"
            "FROM trans_re_enc\n"
            f"WHERE IdOrdenCompraEnc={seed_doc}\n"
            "ORDER BY IdRecepcionEnc DESC;"
        ),
    ]
    if cfg.data_seed_tables:
        suggestions.append("-- Tablas seed data lane: " + ", ".join(cfg.data_seed_tables))
    return suggestions


def summarize_hits(hits: list[str]) -> list[str]:
    if not hits:
        return ["Sin coincidencias directas en codebase para los terminos."]
    grouped: dict[str, int] = {}
    for line in hits:
        file_part = line.split(":", 1)[0]
        grouped[file_part] = grouped.get(file_part, 0) + 1
    ranked = sorted(grouped.items(), key=lambda x: (-x[1], x[0]))
    return [f"{path} ({count})" for path, count in ranked[:20]]


def write_report(
    repo_root: Path,
    case_text: str,
    evidence_text: str,
    cfg: AgentConfig,
    ids: list[str],
    data_hits: list[str],
    ops_hits: list[str],
    sql_suggestions: list[str],
) -> Path:
    out_dir = repo_root / "tools" / "ejc-python-agents" / "out"
    out_dir.mkdir(parents=True, exist_ok=True)
    output = out_dir / "current-case-report.md"

    now = dt.datetime.now().astimezone().isoformat(timespec="seconds")
    lines: list[str] = []
    lines.append("# EJC Python Agent - Case Report")
    lines.append("")
    lines.append(f"- generated_at: {now}")
    lines.append(f"- agent: {cfg.name} v{cfg.version} ({cfg.mode})")
    lines.append(f"- case: {case_text}")
    lines.append("")

    lines.append("## 1) Data Lane")
    lines.append("- IDs detectados: " + (", ".join(ids) if ids else "ninguno"))
    lines.append("- Top coincidencias de codigo relacionadas:")
    for item in summarize_hits(data_hits):
        lines.append(f"  - {item}")
    lines.append("")
    lines.append("### SQL sugerido (solo lectura)")
    for sql in sql_suggestions:
        lines.append("```sql")
        lines.append(sql)
        lines.append("```")

    lines.append("")
    lines.append("## 2) Operativity Lane")
    lines.append("- Top coincidencias de flujo/UI:")
    for item in summarize_hits(ops_hits):
        lines.append(f"  - {item}")
    lines.append("- Objetivos seed operativos:")
    for target in cfg.ops_seed_targets:
        lines.append(f"  - {target}")

    lines.append("")
    lines.append("## 3) Cruce datos vs operatividad")
    lines.append(
        "- Validar si el estado en BD habilita/deshabilita visualmente accion esperada (ej: bandera No Enviado)."
    )
    lines.append(
        "- Confirmar si la accion existe en codigo y si esta enlazada en el ribbon/menu activo del formulario."
    )
    lines.append("- Confirmar precondiciones para cerrar flujo (enviado ERP, pendientes=0, estado actual).")

    lines.append("")
    lines.append("## 4) Guardrails")
    for rule in cfg.guardrails:
        lines.append(f"- {rule}")

    if evidence_text.strip():
        lines.append("")
        lines.append("## 5) Evidencia de entrada (extracto)")
        excerpt = evidence_text.strip()
        if len(excerpt) > 2500:
            excerpt = excerpt[:2500] + "\n...[truncado]"
        lines.append("```text")
        lines.append(excerpt)
        lines.append("```")

    output.write_text("\n".join(lines) + "\n", encoding="utf-8")
    return output


def main() -> int:
    parser = argparse.ArgumentParser(description="EJC Python Agent - Data/Operativity cross analyzer")
    sub = parser.add_subparsers(dest="command", required=True)

    inspect = sub.add_parser("inspect", help="Analiza caso y genera reporte cruzado")
    inspect.add_argument("--case", required=True, help="Descripcion corta del caso")
    inspect.add_argument("--refs", default="", help="Referencias separadas por coma")
    inspect.add_argument("--evidence-file", default="", help="Ruta a evidencia de texto")

    args = parser.parse_args()
    repo_root = find_repo_root(Path.cwd())
    cfg = load_agent_config(repo_root / "tools" / "ejc-python-agents" / "agents" / "ejc-python-agent.yml")

    case_text = args.case.strip()
    refs_text = args.refs.strip()
    evidence_text = ""
    if args.evidence_file:
        fp = Path(args.evidence_file)
        if not fp.is_absolute():
            fp = repo_root / fp
        if fp.exists():
            evidence_text = fp.read_text(encoding="utf-8-sig", errors="replace")

    merged_text = " ".join([case_text, refs_text, evidence_text])
    ids = extract_ids(merged_text)
    if refs_text:
        ids.extend([s.strip() for s in refs_text.split(",") if s.strip()])
        # unique keep order
        uniq: list[str] = []
        seen: set[str] = set()
        for value in ids:
            if value not in seen:
                uniq.append(value)
                seen.add(value)
        ids = uniq

    data_terms = list(cfg.default_terms) + ids + ["trans_oc_enc", "trans_oc_det", "Enviado_A_ERP"]
    ops_terms = list(cfg.default_terms) + ["mnuEstadoEnviadoAERP", "mnuCerrarPedidoCompra", "cmdBackorder"]

    data_hits = run_rg(repo_root, data_terms, cfg.code_roots)
    ops_hits = run_rg(repo_root, ops_terms, cfg.code_roots)
    sql_suggestions = build_sql_suggestions(ids, cfg)

    report = write_report(
        repo_root=repo_root,
        case_text=case_text,
        evidence_text=evidence_text,
        cfg=cfg,
        ids=ids,
        data_hits=data_hits,
        ops_hits=ops_hits,
        sql_suggestions=sql_suggestions,
    )
    print(f"Reporte generado: {report}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
