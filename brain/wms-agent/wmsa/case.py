"""Manejo de casos: intake estructurado + escalado a Replit.

Un caso es un JSON con campos del CASE_INTAKE_TEMPLATE. Se guarda en
~/.wmsa/cases/<id>.json. El comando `escalate` lo renderiza como Markdown
listo para pegar en el chat de Replit.
"""

from __future__ import annotations

import json
import re
from dataclasses import dataclass, field, asdict
from datetime import datetime
from pathlib import Path
from typing import Optional

CASES_DIR = Path.home() / ".wmsa" / "cases"

CASE_TYPES = (
    "data-discrepancy",
    "hh-bug",
    "vb-exception",
    "sql-perf",
    "feature-request",
    "otro",
)
SEVERITIES = ("bloqueante", "alta", "media", "baja")


@dataclass
class Case:
    id: str
    type: str
    severity: str
    client_db: str
    reporter: str
    reported_at: str
    symptom: str
    sku: str = ""
    bodega: str = ""
    ubicacion: str = ""
    lote_idstock: str = ""
    documento: str = ""
    usuario_op: str = ""
    terminal_hh: str = ""
    last_good_at: str = ""
    first_bad_at: str = ""
    suspect_op: str = ""
    repro: str = ""
    repro_steps: list[str] = field(default_factory=list)
    already_tried: str = ""
    hypothesis: str = ""
    attachments: list[str] = field(default_factory=list)
    access_needed: list[str] = field(default_factory=list)
    expected_result: str = ""
    findings: list[dict] = field(default_factory=list)  # logs de investigación local
    created_at: str = ""

    def path(self) -> Path:
        return CASES_DIR / f"{self.id}.json"

    def save(self) -> None:
        CASES_DIR.mkdir(parents=True, exist_ok=True)
        self.path().write_text(
            json.dumps(asdict(self), indent=2, ensure_ascii=False),
            encoding="utf-8",
        )

    @classmethod
    def load(cls, case_id: str) -> "Case":
        p = CASES_DIR / f"{case_id}.json"
        if not p.exists():
            raise FileNotFoundError(f"Caso {case_id} no encontrado en {CASES_DIR}")
        data = json.loads(p.read_text(encoding="utf-8"))
        return cls(**data)

    @classmethod
    def list_all(cls) -> list["Case"]:
        if not CASES_DIR.exists():
            return []
        out: list[Case] = []
        for p in sorted(CASES_DIR.glob("*.json")):
            try:
                out.append(cls(**json.loads(p.read_text(encoding="utf-8"))))
            except Exception:
                continue
        return out

    def add_finding(self, source: str, summary: str, data: dict | None = None) -> None:
        self.findings.append(
            {
                "source": source,
                "summary": summary,
                "data": data or {},
                "at": datetime.now().isoformat(timespec="seconds"),
            }
        )

    def to_markdown(self) -> str:
        """Render listo para pegar en chat de Replit (formato CASE_INTAKE)."""
        lines = [
            f"# Caso {self.id}",
            "",
            "## Identificación",
            f"- Tipo: `{self.type}`",
            f"- Severidad: `{self.severity}`",
            f"- Cliente / BD: `{self.client_db}`",
            f"- Reportado por: {self.reporter}",
            f"- Fecha del reporte: {self.reported_at}",
            "",
            "## Síntoma observable",
            f"> {self.symptom}",
            "",
            "## Datos puntuales",
            f"- SKU / IdProducto: {self.sku or '_n/d_'}",
            f"- Bodega / IdBodega: {self.bodega or '_n/d_'}",
            f"- Ubicación: {self.ubicacion or '_n/d_'}",
            f"- IdLote / IdStock: {self.lote_idstock or '_n/d_'}",
            f"- Documento: {self.documento or '_n/d_'}",
            f"- Usuario operó: {self.usuario_op or '_n/d_'}",
            f"- Terminal HH: {self.terminal_hh or '_n/d_'}",
            "",
            "## Ventana temporal",
            f"- Última vez bien: {self.last_good_at or '_n/d_'}",
            f"- Primera vez mal: {self.first_bad_at or '_n/d_'}",
            f"- Operación sospechosa: {self.suspect_op or '_n/d_'}",
            "",
            "## Reproducibilidad",
            f"- Estado: {self.repro or '_n/d_'}",
        ]
        if self.repro_steps:
            lines.append("- Pasos:")
            for i, s in enumerate(self.repro_steps, 1):
                lines.append(f"  {i}. {s}")
        lines += [
            "",
            "## Lo que ya se intentó",
            self.already_tried or "_n/d_",
            "",
            "## Hipótesis",
            self.hypothesis or "_n/d_",
            "",
            "## Acceso necesario",
        ]
        for a in self.access_needed or ["_n/d_"]:
            lines.append(f"- {a}")
        lines += [
            "",
            "## Resultado esperado",
            self.expected_result or "_n/d_",
        ]
        if self.findings:
            lines += ["", "## Hallazgos locales (auto)", ""]
            for f in self.findings:
                lines.append(f"### [{f['at']}] {f['source']}")
                lines.append(f["summary"])
                if f.get("data"):
                    lines.append("```json")
                    lines.append(json.dumps(f["data"], indent=2, ensure_ascii=False)[:2000])
                    lines.append("```")
                lines.append("")
        if self.attachments:
            lines += ["", "## Adjuntos referenciados", ""]
            for a in self.attachments:
                lines.append(f"- {a}")
        return "\n".join(lines)


def new_case_id(reporter_initials: str = "") -> str:
    """Genera ID humanamente legible: <YYYYMMDD>-<HHMM>-<inits>."""
    now = datetime.now()
    base = now.strftime("%Y%m%d-%H%M")
    initials = re.sub(r"[^A-Za-z0-9]", "", reporter_initials).upper()[:4]
    return f"{base}-{initials}" if initials else base
