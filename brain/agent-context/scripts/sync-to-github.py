#!/usr/bin/env python3
"""
sync-to-github.py — Sincroniza /home/runner/workspace/wms-brain/ a la rama
wms-brain del repo ejcalderongt/tomwms-replit-client-automate via API REST
de GitHub.

Por que via API y no `git push`: el sandbox de Replit bloquea operaciones
git destructivas en /home/runner/workspace/.git/. Ver
brain/agent-context/REPLIT_AGENT.md seccion 2.1 y GITHUB_SYNC.md para el
detalle completo.

Uso minimo:
    cd /home/runner/workspace/wms-brain
    python brain/agent-context/scripts/sync-to-github.py \
        --message "feat: nueva traza de licensing en BOF VB.NET"

Reanudable: si el script explota a mitad de los blobs (timeout, rate
limit), volver a correrlo con los mismos argumentos. El cache en
/tmp/wms-brain-blobs-cache.json salta los blobs ya hechos.
"""

import argparse
import base64
import json
import os
import random
import sys
import time
import urllib.error
import urllib.request
from concurrent.futures import ThreadPoolExecutor, as_completed
from datetime import date

REPO = "ejcalderongt/tomwms-replit-client-automate"
BRANCH = "wms-brain"
API_BASE = f"https://api.github.com/repos/{REPO}"


def gh_request(method, url, body=None, max_retries=8, timeout=120):
    """HTTP request con retry exponencial para 403/429/5xx."""
    token = os.environ["GITHUB_TOKEN"]
    data = json.dumps(body).encode() if body is not None else None
    last_err = None
    for attempt in range(max_retries):
        try:
            req = urllib.request.Request(
                url,
                data=data,
                method=method,
                headers={
                    "Authorization": f"token {token}",
                    "Accept": "application/vnd.github+json",
                    "Content-Type": "application/json",
                    "User-Agent": "wms-brain-sync",
                },
            )
            with urllib.request.urlopen(req, timeout=timeout) as resp:
                return json.loads(resp.read())
        except urllib.error.HTTPError as e:
            body_err = e.read().decode()[:300]
            last_err = f"HTTP {e.code}: {body_err}"
            if e.code in (403, 429, 502, 503, 504):
                wait = min((2 ** attempt) + random.random(), 30)
                time.sleep(wait)
                continue
            raise RuntimeError(last_err)
        except Exception as e:
            last_err = str(e)
            time.sleep(2 + random.random())
            continue
    raise RuntimeError(f"max_retries reached: {last_err}")


def list_files(root, exclude_dirs=(".git",)):
    files = []
    for dirpath, dirnames, filenames in os.walk(root):
        dirnames[:] = [d for d in dirnames if d not in exclude_dirs]
        for fn in filenames:
            full = os.path.join(dirpath, fn)
            rel = os.path.relpath(full, root)
            files.append(rel)
    return files


def make_blob(path, cache):
    if path in cache:
        return path, cache[path]
    with open(path, "rb") as f:
        content = base64.b64encode(f.read()).decode()
    blob = gh_request(
        "POST", f"{API_BASE}/git/blobs",
        {"content": content, "encoding": "base64"},
    )
    return path, blob["sha"]


def main():
    ap = argparse.ArgumentParser(description=__doc__)
    ap.add_argument("--message", required=True,
                    help="Commit message (ASCII puro, sin tildes ni emojis).")
    ap.add_argument("--backup-name", default=None,
                    help=f"Backup branch name (default: {BRANCH}-pre-sync-YYYY-MM-DD).")
    ap.add_argument("--cache-file", default="/tmp/wms-brain-blobs-cache.json",
                    help="Path al cache de blobs (default: /tmp/wms-brain-blobs-cache.json).")
    ap.add_argument("--threads", type=int, default=4,
                    help="Threads para crear blobs (default 4, max recomendado 6).")
    ap.add_argument("--dry-run", action="store_true",
                    help="No empuja nada, solo lista lo que haria.")
    ap.add_argument("--force-base-tree", default=None,
                    help="SHA de tree explicito a usar como base_tree (default: tree del HEAD actual).")
    args = ap.parse_args()

    backup_name = args.backup_name or f"{BRANCH}-pre-sync-{date.today().isoformat()}"

    # Verificar que estamos en la raiz del brain
    cwd = os.getcwd()
    if not os.path.exists(os.path.join(cwd, "brain")) or not os.path.exists(os.path.join(cwd, "README.md")):
        print(f"ERROR: corre el script desde la raiz del brain. CWD actual: {cwd}", file=sys.stderr)
        print(f"Esperado: /home/runner/workspace/wms-brain/", file=sys.stderr)
        sys.exit(2)

    # 1. HEAD actual
    print(f"==> Consultando HEAD actual de {BRANCH}...")
    ref = gh_request("GET", f"{API_BASE}/git/refs/heads/{BRANCH}")
    parent_sha = ref["object"]["sha"]
    parent_commit = gh_request("GET", f"{API_BASE}/git/commits/{parent_sha}")
    base_tree_sha = args.force_base_tree or parent_commit["tree"]["sha"]
    print(f"    parent: {parent_sha[:12]}  base_tree: {base_tree_sha[:12]}")

    # 2. Backup branch
    if not args.dry_run:
        print(f"==> Creando backup branch {backup_name} -> {parent_sha[:12]}...")
        try:
            gh_request("POST", f"{API_BASE}/git/refs",
                       {"ref": f"refs/heads/{backup_name}", "sha": parent_sha})
            print(f"    backup creado.")
        except RuntimeError as e:
            if "already exists" in str(e).lower() or "422" in str(e):
                print(f"    backup ya existia (OK).")
            else:
                raise

    # 3. Listar archivos
    files = sorted(list_files(cwd))
    print(f"==> Archivos a sincronizar: {len(files)}")

    if args.dry_run:
        print(f"--- DRY RUN ---")
        for f in files[:20]:
            print(f"    {f}")
        if len(files) > 20:
            print(f"    ... y {len(files) - 20} mas.")
        print(f"--- FIN DRY RUN ---")
        return

    # 4. Cache de blobs
    cache = {}
    if os.path.exists(args.cache_file):
        try:
            with open(args.cache_file) as f:
                cache = json.load(f)
            print(f"    cache cargado: {len(cache)} blobs ya hechos.")
        except Exception:
            print(f"    cache corrupto, empezando de cero.")
            cache = {}

    todo = [p for p in files if p not in cache]
    print(f"==> Creando blobs ({args.threads} threads, {len(todo)} faltan)...")

    last_save = time.time()
    done = 0
    with ThreadPoolExecutor(max_workers=args.threads) as ex:
        futures = {ex.submit(make_blob, p, cache): p for p in todo}
        for fut in as_completed(futures):
            try:
                path, sha = fut.result()
                cache[path] = sha
                done += 1
                if done % 25 == 0:
                    print(f"    {done}/{len(todo)}  (cache total {len(cache)})")
                if time.time() - last_save > 20:
                    with open(args.cache_file, "w") as f:
                        json.dump(cache, f)
                    last_save = time.time()
            except Exception as e:
                print(f"    ERROR en blob: {e}", file=sys.stderr)
                with open(args.cache_file, "w") as f:
                    json.dump(cache, f)
                raise

    with open(args.cache_file, "w") as f:
        json.dump(cache, f)
    print(f"    blobs OK: {len(cache)} total.")

    # 5. Tree
    tree_items = [
        {"path": p, "mode": "100644", "type": "blob", "sha": sha}
        for p, sha in sorted(cache.items())
        if p in files
    ]
    print(f"==> Creando tree con base_tree + {len(tree_items)} items...")
    tree = gh_request("POST", f"{API_BASE}/git/trees",
                      {"base_tree": base_tree_sha, "tree": tree_items})
    print(f"    tree: {tree['sha'][:12]}")

    # 6. Commit
    print(f"==> Creando commit...")
    commit = gh_request("POST", f"{API_BASE}/git/commits", {
        "message": args.message,
        "tree": tree["sha"],
        "parents": [parent_sha],
    })
    print(f"    commit: {commit['sha'][:12]}")

    # 7. Update ref
    print(f"==> Actualizando ref {BRANCH} -> {commit['sha'][:12]}...")
    gh_request("PATCH", f"{API_BASE}/git/refs/heads/{BRANCH}",
               {"sha": commit["sha"], "force": False})

    print()
    print(f"OK: {BRANCH} -> {commit['sha']}")
    print(f"    https://github.com/{REPO}/commit/{commit['sha']}")
    print(f"    Backup: https://github.com/{REPO}/tree/{backup_name}")


if __name__ == "__main__":
    main()
