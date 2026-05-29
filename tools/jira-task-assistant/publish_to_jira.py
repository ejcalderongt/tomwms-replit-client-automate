#!/usr/bin/env python3
from __future__ import annotations

import argparse
import base64
import json
import os
from pathlib import Path
from typing import Any

import requests

BOARD_ID = 3
PROJECT = "WMS"
AGILE_PATH = "/rest/agile/1.0"
API3_PATH = "/rest/api/3"


def adf_text(text: str) -> dict[str, Any]:
    return {
        "type": "doc",
        "version": 1,
        "content": [{"type": "paragraph", "content": [{"type": "text", "text": text[:32000]}]}],
    }


def auth_headers() -> tuple[str, dict[str, str]]:
    jira_url = os.environ["JIRA_URL"].rstrip("/")
    email = os.environ["JIRA_EMAIL"]
    token = os.environ["JIRA_TOKEN"]
    basic = base64.b64encode(f"{email}:{token}".encode("utf-8")).decode("ascii")
    return jira_url, {
        "Authorization": f"Basic {basic}",
        "Accept": "application/json",
        "Content-Type": "application/json",
    }


def get_active_sprint(jira_url: str, headers: dict[str, str]) -> dict[str, Any]:
    r = requests.get(f"{jira_url}{AGILE_PATH}/board/{BOARD_ID}/sprint", headers=headers, params={"state": "active"}, timeout=30)
    r.raise_for_status()
    values = r.json().get("values", [])
    if not values:
        raise RuntimeError("No hay sprint activo en el board 3.")
    return values[0]


def should_skip(title: str) -> bool:
    s = title.lower()
    noisy = ["merge", "sync", "log diario", "jira log", "arbol de trabajo", "llm_studio"]
    return any(k in s for k in noisy)


def create_issue(jira_url: str, headers: dict[str, str], account_id: str, candidate: dict[str, Any]) -> str:
    raw_title = candidate["title"][:200].strip()
    has_ejc_tag = "#ejc" in raw_title.lower()
    summary = raw_title if has_ejc_tag else f"#EJCAM {raw_title}"
    desc = (
        f"Generado desde jira-task-assistant.\n\n"
        f"Repo: {candidate['repo']}\n"
        f"Commit: {candidate['commit_hash']}\n"
        f"Fecha: {candidate['date']}\n"
        f"Tipo sugerido: {candidate.get('tipo','Mejora')}\n"
        f"Horas sugeridas: {candidate.get('horas_sugeridas',2.0)}\n"
        f"Motivo: {candidate.get('motivo','')}\n"
    )
    payload = {
        "fields": {
            "project": {"key": PROJECT},
            "summary": summary,
            "description": adf_text(desc),
            "issuetype": {"id": "10012"},
            "assignee": {"accountId": account_id},
        }
    }
    r = requests.post(f"{jira_url}{API3_PATH}/issue", headers=headers, data=json.dumps(payload), timeout=30)
    r.raise_for_status()
    return r.json()["key"]


def move_issue_to_sprint(jira_url: str, headers: dict[str, str], sprint_id: int, key: str) -> None:
    payload = {"issues": [key]}
    r = requests.post(f"{jira_url}{AGILE_PATH}/sprint/{sprint_id}/issue", headers=headers, data=json.dumps(payload), timeout=30)
    r.raise_for_status()


def assign_issue_to_epic(jira_url: str, headers: dict[str, str], epic_key: str, key: str) -> None:
    payload = {"issues": [key]}
    r = requests.post(f"{jira_url}{AGILE_PATH}/epic/{epic_key}/issue", headers=headers, data=json.dumps(payload), timeout=30)
    if r.status_code >= 300:
        return


def main() -> int:
    parser = argparse.ArgumentParser(description="Publica candidatos Jira desde jira_payload.json")
    parser.add_argument("--payload", required=True)
    parser.add_argument("--max", type=int, default=15)
    args = parser.parse_args()

    jira_url, headers = auth_headers()
    account_id = os.environ["JIRA_ACCOUNT_ID"]
    active = get_active_sprint(jira_url, headers)

    data = json.loads(Path(args.payload).read_text(encoding="utf-8"))
    candidates: list[dict[str, Any]] = data.get("unclassified", [])

    created: list[str] = []
    processed = 0
    seen_titles: set[str] = set()
    for c in candidates:
        if processed >= args.max:
            break
        title = c.get("title", "").strip()
        if not title or should_skip(title):
            continue
        sig = title.lower()
        if sig in seen_titles:
            continue
        seen_titles.add(sig)
        key = create_issue(jira_url, headers, account_id, c)
        move_issue_to_sprint(jira_url, headers, int(active["id"]), key)
        epic = c.get("epica_sugerida")
        if epic:
            assign_issue_to_epic(jira_url, headers, epic, key)
        created.append(key)
        processed += 1

    print(json.dumps({"sprint": {"id": active["id"], "name": active["name"]}, "created": created}, ensure_ascii=False))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
