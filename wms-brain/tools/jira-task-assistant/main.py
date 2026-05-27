#!/usr/bin/env python3
from __future__ import annotations

import argparse
import json
import re
import subprocess
from dataclasses import dataclass, asdict
from datetime import datetime
from pathlib import Path
from typing import Iterable


HASH_RE = re.compile(r"\b[0-9a-f]{7,40}\b", re.IGNORECASE)
MERGE_WORD_RE = re.compile(r"\bmerge\b", re.IGNORECASE)


@dataclass
class CommitItem:
    repo: str
    hash: str
    date: str
    author: str
    email: str
    subject: str


@dataclass
class CandidateTask:
    commit_hash: str
    repo: str
    date: str
    title: str
    tipo: str
    horas_sugeridas: float
    epica_sugerida: str
    motivo: str


def run_git_log(repo_path: Path, since: str, until: str, author_hint: str | None) -> list[CommitItem]:
    cmd = [
        "git",
        "-C",
        str(repo_path),
        "log",
        "--since",
        since,
        "--until",
        until,
        "--date=short",
        "--pretty=format:%H|%ad|%an|%ae|%s",
    ]
    out = subprocess.check_output(cmd, text=True, encoding="utf-8", errors="replace")
    items: list[CommitItem] = []
    for line in out.splitlines():
        parts = line.split("|", 4)
        if len(parts) != 5:
            continue
        h, d, an, ae, s = parts
        if author_hint:
            ah = author_hint.lower()
            if ah not in an.lower() and ah not in ae.lower():
                continue
        items.append(
            CommitItem(
                repo=repo_path.name,
                hash=h,
                date=d,
                author=an,
                email=ae,
                subject=s.strip(),
            )
        )
    return items


def read_text_if_exists(path: Path) -> str:
    return path.read_text(encoding="utf-8", errors="replace") if path.exists() else ""


def extract_documented_hashes(texts: Iterable[str]) -> set[str]:
    found: set[str] = set()
    for t in texts:
        for h in HASH_RE.findall(t):
            found.add(h.lower())
    return found


def classify_commit(subject: str) -> tuple[str, float, str]:
    s = subject.lower()
    if any(k in s for k in ["fix", "error", "bug", "correcci", "evita", "guard"]):
        return "Bug", 2.5, "WMS-2026"
    if any(k in s for k in ["optimiza", "mejora", "readmodel", "cache", "rendimiento", "performance"]):
        return "Mejora", 3.0, "WMS-73"
    if any(k in s for k in ["identity", "refactor", "scale", "arquitect", "tool"]):
        return "Técnica", 4.0, "WMS-2026"
    return "Mejora", 2.0, "WMS-73"


def is_noise_commit(subject: str) -> bool:
    s = subject.strip().lower()
    if MERGE_WORD_RE.search(s):
        return True
    if s in {"sync", "merge", "comit local.", "fix dal", "syn ajnust"}:
        return True
    return False


def to_candidate(c: CommitItem) -> CandidateTask:
    tipo, horas, epica = classify_commit(c.subject)
    return CandidateTask(
        commit_hash=c.hash[:8],
        repo=c.repo,
        date=c.date,
        title=c.subject,
        tipo=tipo,
        horas_sugeridas=horas,
        epica_sugerida=epica,
        motivo="Commit no encontrado en BITACORA_JIRA/LOG_DIARIO (por hash).",
    )


def write_outputs(out_dir: Path, commits: list[CommitItem], candidates: list[CandidateTask], documented_hashes: set[str]) -> None:
    out_dir.mkdir(parents=True, exist_ok=True)
    payload = {
        "generated_at": datetime.now().isoformat(timespec="seconds"),
        "total_commits": len(commits),
        "documented_hashes_found": len(documented_hashes),
        "unclassified_count": len(candidates),
        "unclassified": [asdict(x) for x in candidates],
    }
    (out_dir / "jira_payload.json").write_text(
        json.dumps(payload, ensure_ascii=False, indent=2),
        encoding="utf-8",
    )

    lines = [
        "# Candidatos Jira (Draft)",
        "",
        f"- Total commits analizados: **{len(commits)}**",
        f"- Commits no clasificados: **{len(candidates)}**",
        "",
        "## Tareas sugeridas",
        "",
    ]
    for i, c in enumerate(candidates, start=1):
        lines.extend(
            [
                f"{i}. `{c.commit_hash}` [{c.repo}] {c.title}",
                f"   - Tipo: {c.tipo}",
                f"   - Épica sugerida: {c.epica_sugerida}",
                f"   - Horas sugeridas: {c.horas_sugeridas}",
                "",
            ]
        )
    (out_dir / "jira_candidates.md").write_text("\n".join(lines), encoding="utf-8")


def main() -> int:
    parser = argparse.ArgumentParser(description="Cruce Git vs bitácoras para candidatos Jira.")
    parser.add_argument("--since", required=True, help='Fecha inicial, ej: "2026-05-19 00:00"')
    parser.add_argument("--until", required=True, help='Fecha final, ej: "2026-05-26 23:59"')
    parser.add_argument("--author-hint", default="ejcalderon", help="Filtro opcional de autor (nombre/correo)")
    parser.add_argument("--repo", action="append", dest="repos", help="Ruta repo a incluir (repetible)")
    parser.add_argument("--bitacora", required=True, help="Ruta BITACORA_JIRA.md")
    parser.add_argument("--log-diario", required=True, help="Ruta LOG_DIARIO.md")
    parser.add_argument("--out-dir", required=True, help="Directorio de salida")
    args = parser.parse_args()

    repos = [Path(r) for r in (args.repos or [])]
    if not repos:
        raise SystemExit("Debes indicar al menos un --repo")

    all_commits: list[CommitItem] = []
    for rp in repos:
        all_commits.extend(run_git_log(rp, args.since, args.until, args.author_hint))

    bitacora_text = read_text_if_exists(Path(args.bitacora))
    log_text = read_text_if_exists(Path(args.log_diario))
    documented = extract_documented_hashes([bitacora_text, log_text])

    candidates: list[CandidateTask] = []
    for c in all_commits:
        h = c.hash.lower()
        h8 = c.hash[:8].lower()
        if is_noise_commit(c.subject):
            continue
        if h in documented or h8 in documented:
            continue
        candidates.append(to_candidate(c))

    write_outputs(Path(args.out_dir), all_commits, candidates, documented)
    print(f"OK: {len(candidates)} commits no clasificados. Ver salida en: {args.out_dir}")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
