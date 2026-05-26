from __future__ import annotations

import argparse
import datetime as dt
import hashlib
import json
import os
import re
import sys
import textwrap
import urllib.error
import urllib.request
from dataclasses import dataclass, field
from pathlib import Path
from typing import Iterable


PROMPT_VERSION = "wms-context-router-v1"
DEFAULT_LM_STUDIO_URL = "http://127.0.0.1:1234/v1"


@dataclass
class RouteEntry:
    name: str
    file: str = ""
    triggers: list[str] = field(default_factory=list)
    also_load: list[str] = field(default_factory=list)
    score: int = 0
    matched: list[str] = field(default_factory=list)
    kind: str = "domain"


def strip_yaml_scalar(value: str) -> str:
    value = value.strip()
    if value.startswith("-"):
        value = value[1:].strip()
    if (value.startswith('"') and value.endswith('"')) or (
        value.startswith("'") and value.endswith("'")
    ):
        value = value[1:-1]
    return value


def find_repo_root(start: Path) -> Path:
    current = start.resolve()
    for candidate in [current, *current.parents]:
        if (candidate / "brain" / "agents" / "_index.yml").exists():
            return candidate
    raise FileNotFoundError("No se encontro brain/agents/_index.yml en los padres.")


def parse_list_after(lines: list[str], index: int, parent_indent: int) -> list[str]:
    items: list[str] = []
    for raw in lines[index + 1 :]:
        if not raw.strip() or raw.lstrip().startswith("#"):
            continue
        indent = len(raw) - len(raw.lstrip(" "))
        if indent <= parent_indent:
            break
        stripped = raw.strip()
        if stripped.startswith("- "):
            items.append(strip_yaml_scalar(stripped))
    return items


def parse_index(index_path: Path) -> tuple[list[str], dict[str, RouteEntry], dict[str, RouteEntry]]:
    lines = index_path.read_text(encoding="utf-8-sig").splitlines()
    always_load: list[str] = []
    domains: dict[str, RouteEntry] = {}
    clients: dict[str, RouteEntry] = {}

    for i, raw in enumerate(lines):
        if raw.strip() == "always_load:":
            always_load = parse_list_after(lines, i, 0)
            break

    section = ""
    current: RouteEntry | None = None
    current_key = ""

    for i, raw in enumerate(lines):
        stripped = raw.strip()
        if not stripped or stripped.startswith("#"):
            continue

        indent = len(raw) - len(raw.lstrip(" "))

        if indent == 0:
            section = stripped[:-1] if stripped.endswith(":") else ""
            current = None
            current_key = ""
            continue

        if section not in {"domains", "clients"}:
            continue

        if indent == 2 and stripped.endswith(":"):
            name = stripped[:-1]
            current = RouteEntry(name=name, kind="client" if section == "clients" else "domain")
            if section == "domains":
                domains[name] = current
            else:
                clients[name] = current
            current_key = ""
            continue

        if current is None:
            continue

        if indent == 4 and ":" in stripped:
            key, value = stripped.split(":", 1)
            current_key = key.strip()
            value = value.strip()
            if current_key == "file" and value:
                current.file = strip_yaml_scalar(value)
            elif current_key in {"triggers", "also_load"}:
                items = parse_list_after(lines, i, 4)
                if current_key == "triggers":
                    current.triggers = items
                else:
                    current.also_load = items
            continue

    return always_load, domains, clients


def score_entry(task: str, entry: RouteEntry) -> RouteEntry:
    lower_task = task.lower()
    matched = []
    for trigger in entry.triggers:
        normalized = trigger.lower()
        if normalized and normalized in lower_task:
            matched.append(trigger)
    entry.matched = matched
    entry.score = len(matched)
    return entry


def unique_keep_order(values: Iterable[str]) -> list[str]:
    seen: set[str] = set()
    result: list[str] = []
    for value in values:
        normalized = value.replace("\\", "/")
        if normalized and normalized not in seen:
            seen.add(normalized)
            result.append(normalized)
    return result


def select_context(repo_root: Path, task: str) -> tuple[list[str], list[RouteEntry]]:
    always_load, domains, clients = parse_index(repo_root / "brain" / "agents" / "_index.yml")

    selected_entries: list[RouteEntry] = []
    selected_files: list[str] = list(always_load)

    for entry in domains.values():
        scored = score_entry(task, entry)
        if scored.score > 0:
            selected_entries.append(scored)
            selected_files.append(scored.file)
            selected_files.extend(scored.also_load)

    for entry in clients.values():
        scored = score_entry(task, entry)
        if scored.score > 0:
            selected_entries.append(scored)
            selected_files.append(scored.file)

    if any(entry.name in {"reserva", "webapi", "performance"} for entry in selected_entries):
        selected_files.append("codex-context-mi3-di-estatus.yml")

    selected_entries.sort(key=lambda item: (-item.score, item.kind, item.name))
    return unique_keep_order(selected_files), selected_entries


def read_source(repo_root: Path, rel_path: str, max_chars: int) -> str:
    path = repo_root / rel_path
    if not path.exists():
        return f"[NO ENCONTRADO] {rel_path}\n"

    text = path.read_text(encoding="utf-8-sig", errors="replace")
    text = redact_sensitive(text)

    if len(text) <= max_chars:
        return text

    head = text[:max_chars]
    return head + f"\n\n[TRUNCADO: {rel_path} tenia {len(text)} caracteres]\n"


def redact_sensitive(text: str) -> str:
    patterns = [
        (r"(WMS_KILLIOS_DB_PASSWORD\s*=\s*)(.+)", r"\1[REDACTED]"),
        (r"(BRAIN_IMPORT_TOKEN\s*=\s*)(.+)", r"\1[REDACTED]"),
        (r"(Password\s*=\s*)([^;\r\n]+)", r"\1[REDACTED]"),
        (r"(?im)^(\s*definition\s*:\s*).*$", r"\1[REDACTED_SQL_DEFINITION]"),
    ]
    output = text
    for pattern, replacement in patterns:
        output = re.sub(pattern, replacement, output)
    return output


def discover_model(base_url: str, timeout: int) -> str | None:
    try:
        request = urllib.request.Request(f"{base_url.rstrip('/')}/models")
        with urllib.request.urlopen(request, timeout=timeout) as response:
            payload = json.loads(response.read().decode("utf-8"))
        data = payload.get("data") or []
        if data:
            return data[0].get("id")
    except Exception:
        return None
    return None


def call_lm_studio(
    base_url: str,
    model: str | None,
    task: str,
    source_bundle: str,
    timeout: int,
) -> tuple[str | None, str]:
    model_id = model or discover_model(base_url, timeout=timeout)
    if not model_id:
        return None, "LM Studio no respondio en /models o no hay modelo cargado."

    system_prompt = textwrap.dedent(
        """
        Sos un compresor local de contexto para el proyecto WMS de Erik.
        Tu trabajo es reducir tokens para Codex, no resolver el bug.
        No inventes archivos, simbolos ni reglas. No incluyas secretos.
        No expongas definiciones SQL completas ni contenido marcado como definition.
        Responde en espanol, compacto, con maximo 900 palabras.

        Formato:
        1. Ruta de contexto
        2. Senales relevantes
        3. Riesgos / guardrails
        4. Archivos o simbolos para leer despues
        5. Brain queries sugeridas
        6. Checklist de entrada
        """
    ).strip()

    user_prompt = f"Tarea:\n{task}\n\nContexto seleccionado:\n{source_bundle}"
    payload = {
        "model": model_id,
        "messages": [
            {"role": "system", "content": system_prompt},
            {"role": "user", "content": user_prompt},
        ],
        "temperature": 0.1,
        "max_tokens": 1400,
    }

    data = json.dumps(payload).encode("utf-8")
    request = urllib.request.Request(
        f"{base_url.rstrip('/')}/chat/completions",
        data=data,
        headers={"Content-Type": "application/json"},
        method="POST",
    )

    try:
        with urllib.request.urlopen(request, timeout=timeout) as response:
            response_payload = json.loads(response.read().decode("utf-8"))
        content = response_payload["choices"][0]["message"]["content"].strip()
        return content, f"LM Studio OK ({model_id})"
    except urllib.error.URLError as exc:
        return None, f"LM Studio no disponible: {exc}"
    except Exception as exc:
        return None, f"Respuesta inesperada de LM Studio: {exc}"


def cache_key(task: str, selected_files: list[str], source_bundle: str, model: str | None) -> str:
    material = "\n".join([PROMPT_VERSION, task, model or "", *selected_files, source_bundle])
    return hashlib.sha256(material.encode("utf-8")).hexdigest()


def render_fallback(selected_files: list[str], selected_entries: list[RouteEntry]) -> str:
    matched_lines = []
    for entry in selected_entries:
        joined = ", ".join(entry.matched)
        matched_lines.append(f"- {entry.kind}:{entry.name} -> {joined}")

    brain_lines = [
        "- GET $env:BRAIN_BASE_URL/health",
        "- GET $env:BRAIN_BASE_URL/search?q=<simbolo>&kind=<tipo>",
        "- GET $env:BRAIN_BASE_URL/impact?symbol=<simbolo>&depth=2",
    ]

    return "\n".join(
        [
            "## Ruta de contexto",
            *[f"- {path}" for path in selected_files],
            "",
            "## Triggers detectados",
            *(matched_lines or ["- Sin triggers especificos; usar coordinator.yml como entrada."]),
            "",
            "## Brain queries sugeridas",
            *brain_lines,
            "",
            "## Guardrails",
            "- No tocar Reference.vb.",
            "- No mezclar HH Java con VB/Core en la misma fase.",
            "- DB productiva KILLIOS solo SELECT.",
            "- No exponer secretos ni definition SQL.",
        ]
    )


def build_source_bundle(repo_root: Path, selected_files: list[str], max_total_chars: int) -> str:
    if not selected_files:
        return ""

    per_file = max(2500, max_total_chars // max(len(selected_files), 1))
    chunks = []
    for rel_path in selected_files:
        text = read_source(repo_root, rel_path, per_file)
        chunks.append(f"\n--- FILE: {rel_path} ---\n{text}")

    bundle = "\n".join(chunks)
    if len(bundle) > max_total_chars:
        bundle = bundle[:max_total_chars] + f"\n\n[TRUNCADO: bundle max_total_chars={max_total_chars}]\n"
    return bundle


def write_brief(
    repo_root: Path,
    task: str,
    selected_files: list[str],
    selected_entries: list[RouteEntry],
    llm_status: str,
    body: str,
) -> Path:
    out_dir = repo_root / "tools" / "wms-context" / "out"
    out_dir.mkdir(parents=True, exist_ok=True)
    output_path = out_dir / "current-brief.md"

    now = dt.datetime.now().astimezone().isoformat(timespec="seconds")
    matched = []
    for entry in selected_entries:
        matched.append(f"- {entry.kind}:{entry.name} score={entry.score} triggers={', '.join(entry.matched)}")

    content = "\n".join(
        [
            "# WMS Context Brief",
            "",
            f"- generated_at: {now}",
            f"- prompt_version: {PROMPT_VERSION}",
            f"- lm_studio: {llm_status}",
            f"- task: {task}",
            "",
            "## Selected Files",
            *[f"- {path}" for path in selected_files],
            "",
            "## Matched Routes",
            *(matched or ["- Sin rutas especificas detectadas."]),
            "",
            "## Compact Brief",
            body.strip(),
            "",
        ]
    )

    output_path.write_text(content, encoding="utf-8")
    return output_path


def main() -> int:
    parser = argparse.ArgumentParser(description="WMS context router with optional LM Studio compression.")
    parser.add_argument("task", help="Descripcion breve de la tarea.")
    parser.add_argument("--no-llm", action="store_true", help="No llamar LM Studio.")
    parser.add_argument("--refresh", action="store_true", help="Ignorar cache.")
    parser.add_argument("--model", default=os.getenv("LM_STUDIO_MODEL"), help="Modelo LM Studio.")
    parser.add_argument(
        "--lm-studio-url",
        default=os.getenv("LM_STUDIO_BASE_URL", DEFAULT_LM_STUDIO_URL),
        help="Base URL OpenAI-compatible de LM Studio.",
    )
    parser.add_argument("--max-source-chars", type=int, default=24000)
    parser.add_argument("--timeout", type=int, default=120)
    args = parser.parse_args()

    repo_root = find_repo_root(Path.cwd())
    selected_files, selected_entries = select_context(repo_root, args.task)
    source_bundle = build_source_bundle(repo_root, selected_files, args.max_source_chars)

    cache_dir = repo_root / "tools" / "wms-context" / "cache"
    cache_dir.mkdir(parents=True, exist_ok=True)
    key = cache_key(args.task, selected_files, source_bundle, args.model)
    cache_path = cache_dir / f"{key}.json"

    llm_status = "LLM omitida (--no-llm)"
    body = render_fallback(selected_files, selected_entries)

    if not args.no_llm:
        cached = None
        if cache_path.exists() and not args.refresh:
            cached = json.loads(cache_path.read_text(encoding="utf-8"))

        if cached:
            llm_status = cached.get("status", "LM Studio cache")
            body = cached.get("body", body)
        else:
            llm_body, status = call_lm_studio(
                args.lm_studio_url,
                args.model,
                args.task,
                source_bundle,
                timeout=args.timeout,
            )
            llm_status = status
            if llm_body:
                body = redact_sensitive(llm_body)
            cache_path.write_text(
                json.dumps({"status": llm_status, "body": body}, ensure_ascii=False, indent=2),
                encoding="utf-8",
            )

    output_path = write_brief(repo_root, args.task, selected_files, selected_entries, llm_status, body)
    print(f"Brief generado: {output_path}")
    print(f"LM Studio: {llm_status}")
    print("Archivos seleccionados:")
    for path in selected_files:
        print(f" - {path}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
