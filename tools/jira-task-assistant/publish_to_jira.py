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

TEAM = {
    "dt solutions": "61d3d6500586a20069465469",
    "dts": "61d3d6500586a20069465469",
    "yo": "61d3d6500586a20069465469",
    "erik calderon": "557058:0239b0a8-451b-48a5-ac22-a06111f5b8a5",
    "erik": "557058:0239b0a8-451b-48a5-ac22-a06111f5b8a5",
    "roberto melgar": "5ba9121d08ff0878b1b8ac80",
    "roberto": "5ba9121d08ff0878b1b8ac80",
    "axel palala": "5ba911e4b9665979c5587b41",
    "axel": "5ba911e4b9665979c5587b41",
    "jaroslav pospichal": "5ba911ff165d986a7c5294ae",
    "jaroslav": "5ba911ff165d986a7c5294ae",
    "carolina fuentes": "557058:88d97c4b-ebd7-46b2-b194-74de23f1613d",
    "carolina": "557058:88d97c4b-ebd7-46b2-b194-74de23f1613d",
    "efren gustavo buch": "61d4671fe67ea2006bcb5963",
    "efren": "61d4671fe67ea2006bcb5963",
    "anderly teleguario": "61d4671ce67ea2006bcb5940",
    "anderly": "61d4671ce67ea2006bcb5940",
    "kelvyn magzul": "61d4671b7aa7ac0070296608",
    "kelvyn": "61d4671b7aa7ac0070296608",
    "jonatan santiago": "712020:f1b7f989-706d-47b1-8d74-8105f428a512",
    "jonatan": "712020:f1b7f989-706d-47b1-8d74-8105f428a512",
    "marcela álvarez": "712020:ef404ed6-74ac-4ee5-8f1f-883880e49f44",
    "marcela": "712020:ef404ed6-74ac-4ee5-8f1f-883880e49f44",
    "maría lorena gonzález": "712020:cd1a2391-b582-4b08-91f0-e18d0423dd7d",
    "lorena": "712020:cd1a2391-b582-4b08-91f0-e18d0423dd7d",
    "juan carlos correa": "712020:bd63e11f-74e4-49b4-a9a5-00f1b7a6c4d2",
    "juan carlos": "712020:bd63e11f-74e4-49b4-a9a5-00f1b7a6c4d2",
    "melvin cojti": "712020:0f298b96-ecde-434e-9a74-e8da1eb3e5f2",
    "melvin": "712020:0f298b96-ecde-434e-9a74-e8da1eb3e5f2",
    "melvyn": "63496653db32d9ce175dc139",
    "martin n. ocampo": "712020:1408b1ac-beed-4a9e-98b0-250d36dd8e37",
    "martin": "712020:1408b1ac-beed-4a9e-98b0-250d36dd8e37",
    "abigail gaytan": "712020:5d0afccf-1b47-4cc7-ac27-39671948667a",
    "abigail": "712020:5d0afccf-1b47-4cc7-ac27-39671948667a",
}


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


def resolve_persona(jira_url: str, headers: dict[str, str], persona: str) -> str:
    key = persona.strip().lower()
    if key in TEAM:
        return TEAM[key]
    r = requests.get(
        f"{jira_url}{API3_PATH}/user/search",
        headers=headers,
        params={"query": persona, "maxResults": 5},
        timeout=30,
    )
    r.raise_for_status()
    users = r.json()
    if users:
        return users[0]["accountId"]
    raise RuntimeError(f"No se encontro usuario en Jira: {persona}")


def assign_issue(jira_url: str, headers: dict[str, str], issue_key: str, account_id: str) -> None:
    r = requests.put(
        f"{jira_url}{API3_PATH}/issue/{issue_key}/assignee",
        headers=headers,
        data=json.dumps({"accountId": account_id}),
        timeout=30,
    )
    r.raise_for_status()


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
        }
    }
    r = requests.post(f"{jira_url}{API3_PATH}/issue", headers=headers, data=json.dumps(payload), timeout=30)
    r.raise_for_status()
    key = r.json()["key"]
    assign_issue(jira_url, headers, key, account_id)
    return key


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
    parser.add_argument("--assignee", default="erik", help="Alias humano para asignacion (ej: erik, dts, axel)")
    args = parser.parse_args()

    jira_url, headers = auth_headers()
    account_id = resolve_persona(jira_url, headers, args.assignee)
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
