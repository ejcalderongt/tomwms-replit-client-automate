#!/usr/bin/env python3
"""TOMWMS Brain — SQL catalog extractor.

Connects to a SQL Server database (read-only), extracts the catalog
(objects + dependencies + module definitions), writes a single JSON
payload, and optionally POSTs it to the Brain import endpoint.

Usage examples:
    # Extract only, write to file
    python extract_sql_catalog.py \\
        --server "52.41.114.122,1437" \\
        --database TOMWMS_KILLIOS_PRD \\
        --user wmsuser \\
        --output ./tomwms_killios_prd.json

    # Extract and upload directly to Replit Brain
    python extract_sql_catalog.py \\
        --server "52.41.114.122,1437" \\
        --database TOMWMS_KILLIOS_PRD \\
        --user wmsuser \\
        --upload https://<replit>.replit.dev/api/brain/import/sql-catalog

Password is read from the WMS_KILLIOS_DB_PASSWORD env var
(never passed on command line).

Requires: pyodbc (and an ODBC driver for SQL Server installed locally).
    pip install pyodbc requests
"""
from __future__ import annotations

import argparse
import datetime as dt
import json
import os
import sys
from pathlib import Path

EXTRACTOR_VERSION = "1.0.0"


def load_sql_file() -> str:
    here = Path(__file__).parent
    return (here / "extract.sql").read_text(encoding="utf-8")


def split_statements(sql: str) -> list[str]:
    """Split the extract.sql file into the 3 individual SELECT blocks.

    A block is kept only if it contains a real (non-commented) line that
    starts with SELECT. Without this guard, the file's header comment block
    is also kept because the word "SELECT" appears inside a `-- Permissions
    required: SELECT on sys.objects` comment, which then gets executed as
    SQL and produces garbage rows. (Bug found by EJC during first run.)
    """
    parts: list[str] = []
    current: list[str] = []
    for line in sql.splitlines():
        stripped = line.strip()
        if stripped.startswith("-- 1.") or stripped.startswith("-- 2.") or stripped.startswith("-- 3."):
            if current:
                parts.append("\n".join(current).strip())
                current = []
        current.append(line)
    if current:
        parts.append("\n".join(current).strip())

    def has_real_select(block: str) -> bool:
        for line in block.splitlines():
            s = line.strip()
            if not s or s.startswith("--"):
                continue
            if s.upper().startswith("SELECT"):
                return True
        return False

    return [p for p in parts if has_real_select(p)]


def fetch_rows(cursor, sql: str) -> list[dict]:
    cursor.execute(sql)
    cols = [c[0] for c in cursor.description]
    return [dict(zip(cols, row)) for row in cursor.fetchall()]


def normalize_objects(rows: list[dict]) -> list[dict]:
    out = []
    for r in rows:
        out.append({
            "schema": r["schema_name"],
            "name": r["object_name"],
            "kind": r["kind"],
            "object_id": int(r["object_id"]),
            "create_date": r.get("create_date"),
            "modify_date": r.get("modify_date"),
            "definition_length": int(r["definition_length"]) if r.get("definition_length") is not None else None,
            "row_count": int(r["row_count"]) if r.get("row_count") is not None else None,
        })
    return out


def normalize_dependencies(rows: list[dict]) -> list[dict]:
    out = []
    for r in rows:
        out.append({
            "from_schema": r["from_schema"],
            "from_name": r["from_name"],
            "to_schema": r.get("to_schema"),
            "to_name": r["to_name"],
            "to_kind_hint": r.get("to_kind_hint"),
            "is_ambiguous": bool(r.get("is_ambiguous", False)),
        })
    return out


def normalize_modules(rows: list[dict]) -> list[dict]:
    out = []
    for r in rows:
        defn = r.get("definition") or ""
        out.append({
            "schema": r["schema_name"],
            "name": r["object_name"],
            "definition": defn,
        })
    return out


def build_payload(server: str, database: str, objects, deps, modules) -> dict:
    return {
        "database": database,
        "server": server,
        "extracted_at": dt.datetime.now(dt.timezone.utc).isoformat(),
        "extractor_version": EXTRACTOR_VERSION,
        "objects": objects,
        "dependencies": deps,
        "modules": modules,
    }


def upload(url: str, payload: dict, token: str | None) -> None:
    import requests

    headers = {}
    if token:
        headers["X-Brain-Token"] = token
    print(f"Uploading to {url} ...", flush=True)
    resp = requests.post(url, json=payload, headers=headers, timeout=300)
    print(f"HTTP {resp.status_code}")
    print(resp.text[:2000])
    resp.raise_for_status()


def main() -> int:
    ap = argparse.ArgumentParser(description=__doc__)
    ap.add_argument("--server", required=True, help="SQL Server host[,port]")
    ap.add_argument("--database", required=True)
    ap.add_argument("--user", required=True)
    ap.add_argument("--driver", default="ODBC Driver 17 for SQL Server")
    ap.add_argument("--output", help="Path to write the JSON payload (optional if --upload given)")
    ap.add_argument("--upload", help="POST the payload to this URL (e.g. Replit endpoint)")
    ap.add_argument("--include-modules", action="store_true", default=True,
                    help="Include full SQL definitions of procs/views/funcs (default: yes)")
    ap.add_argument("--no-modules", dest="include_modules", action="store_false")
    args = ap.parse_args()

    if not args.output and not args.upload:
        print("Provide at least --output or --upload", file=sys.stderr)
        return 2

    password = os.environ.get("WMS_KILLIOS_DB_PASSWORD")
    if not password:
        print("WMS_KILLIOS_DB_PASSWORD env var is required", file=sys.stderr)
        return 2

    try:
        import pyodbc  # type: ignore
    except ImportError:
        print("pyodbc is not installed. Run: pip install pyodbc requests", file=sys.stderr)
        return 2

    conn_str = (
        f"DRIVER={{{args.driver}}};"
        f"SERVER={args.server};"
        f"DATABASE={args.database};"
        f"UID={args.user};"
        f"PWD={password};"
        f"TrustServerCertificate=yes;"
        f"Encrypt=yes;"
    )
    print(f"Connecting to {args.database}@{args.server} ...", flush=True)
    conn = pyodbc.connect(conn_str, readonly=True)
    cur = conn.cursor()

    sql_blocks = split_statements(load_sql_file())
    if len(sql_blocks) < 2:
        print("extract.sql malformed", file=sys.stderr)
        return 2

    print("Fetching sys.objects ...", flush=True)
    objects = normalize_objects(fetch_rows(cur, sql_blocks[0]))
    print(f"  {len(objects)} objects")

    print("Fetching sys.sql_expression_dependencies ...", flush=True)
    deps = normalize_dependencies(fetch_rows(cur, sql_blocks[1]))
    print(f"  {len(deps)} dependencies")

    modules: list[dict] = []
    if args.include_modules and len(sql_blocks) >= 3:
        print("Fetching sys.sql_modules definitions ...", flush=True)
        modules = normalize_modules(fetch_rows(cur, sql_blocks[2]))
        print(f"  {len(modules)} module definitions")

    cur.close()
    conn.close()

    payload = build_payload(args.server, args.database, objects, deps, modules)

    if args.output:
        Path(args.output).write_text(
            json.dumps(payload, indent=2, default=str), encoding="utf-8"
        )
        size_mb = Path(args.output).stat().st_size / (1024 * 1024)
        print(f"Wrote {args.output} ({size_mb:.2f} MB)")

    if args.upload:
        token = os.environ.get("BRAIN_IMPORT_TOKEN")
        if not token:
            print(
                "WARNING: BRAIN_IMPORT_TOKEN env var is not set; "
                "the upload will likely be rejected (401/503).",
                file=sys.stderr,
            )
        upload(args.upload, payload, token)

    print("Done.")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
