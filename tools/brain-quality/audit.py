"""Read-only corpus audit. #EJC20261027_BRAIN_GOVERNANCE

Only paths, hashes and aggregate counts leave the scanner; document bodies
and credential files are never included in its output.
"""
import argparse
import hashlib
import json
import subprocess
from datetime import datetime, timezone
from pathlib import Path

CONFIG = Path(__file__).with_name("workspace.json")
EXCLUDED = {"agent-context", ".git", "node_modules", "__pycache__"}


def digest(path):
    return hashlib.sha256(path.read_bytes()).hexdigest()


def audit(root, config):
    roots = [root / name for name in config["knowledge_roots"]]
    documents = []
    by_relative = {}
    for corpus in roots:
        if not corpus.is_dir():
            continue
        for path in sorted(corpus.rglob("*")):
            relative = path.relative_to(corpus)
            if (not path.is_file() or EXCLUDED.intersection(relative.parts)
                    or path.suffix.lower() not in {".md", ".yml", ".yaml"}):
                continue
            row = {"path": path.relative_to(root).as_posix(),
                   "relative": relative.as_posix(), "sha256": digest(path),
                   "bytes": path.stat().st_size}
            documents.append(row)
            by_relative.setdefault(row["relative"], []).append(row)
    duplicates = []
    for relative, rows in sorted(by_relative.items()):
        if len(rows) > 1:
            duplicates.append({"relative": relative,
                               "status": "identical" if len({r['sha256'] for r in rows}) == 1 else "review-required",
                               "copies": [r["path"] for r in rows]})
    revision = subprocess.run(["git", "-C", str(root), "rev-parse", "HEAD"],
                              capture_output=True, text=True, check=True).stdout.strip()
    status = subprocess.run(["git", "-C", str(root), "status", "--porcelain"],
                            capture_output=True, text=True, check=True).stdout
    return {"schema_version": 1, "generated_at": datetime.now(timezone.utc).isoformat(),
            "root": str(root), "commit": revision, "dirty": bool(status.strip()),
            "scope": "local checkout; credential context excluded; no runtime validation",
            "roots": [{"path": str(p), "exists": p.is_dir()} for p in roots],
            "summary": {"documents": len(documents), "unique_relative_paths": len(by_relative),
                        "identical_duplicates": sum(d["status"] == "identical" for d in duplicates),
                        "divergent_duplicates": sum(d["status"] == "review-required" for d in duplicates)},
            "duplicates": duplicates, "documents": documents}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", type=Path, default=CONFIG.parents[2])
    parser.add_argument("--output", type=Path, required=True)
    args = parser.parse_args()
    config = json.loads(CONFIG.read_text(encoding="utf-8"))
    report = audit(args.root.resolve(), config)
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(report, ensure_ascii=False, indent=2), encoding="utf-8")
    print(json.dumps(report["summary"]))


if __name__ == "__main__":
    main()
