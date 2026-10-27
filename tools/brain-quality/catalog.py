"""Build a lossless source catalog; never silently choose divergent knowledge.

#EJC20261027_BRAIN_CANONICAL
"""
import argparse
import difflib
import hashlib
import json
import re
from pathlib import Path
from audit import CONFIG, audit
from metadata import declared_metadata


def normalized_body(text):
    text = text.lstrip("\ufeff").replace("\r\n", "\n")
    text = re.sub(r"\A---\n.*?\n---(?:\n|$)", "", text, count=1, flags=re.S)
    return "\n".join(line.rstrip() for line in text.strip().splitlines())


def additive_superset(root, copies):
    """Select only a sequence-preserving extension, never a rewrite."""
    bodies = {c['path']: normalized_body((root / c['path']).read_text(
        encoding='utf-8-sig')).splitlines() for c in copies}
    for candidate in sorted(copies, key=lambda c: (-len(bodies[c['path']]), c['path'])):
        target = bodies[candidate['path']]
        if all(all(op in {'equal', 'insert'} for op, *_ in difflib.SequenceMatcher(
                None, body, target, autojunk=False).get_opcodes()) for body in bodies.values()):
            return candidate
    return None


def build_catalog(root, report):
    grouped = {}
    for row in report["documents"]:
        grouped.setdefault(row["relative"], []).append(row)
    entries = []
    for relative, copies in sorted(grouped.items()):
        normalized = {hashlib.sha256(normalized_body((root / c["path"]).read_text(
            encoding="utf-8-sig")).encode()).hexdigest() for c in copies}
        if len(copies) == 1:
            resolution = "unique"
        elif len({c["sha256"] for c in copies}) == 1:
            resolution = "byte-identical"
        elif len(normalized) == 1:
            resolution = "body-equivalent"
        else:
            resolution = "unresolved"
        selected = min(copies, key=lambda c: (len(Path(c["path"]).parts), c["path"]))
        if resolution == 'unresolved':
            superset = additive_superset(root, copies)
            if superset:
                selected = superset
                resolution = 'additive-superset'
        historical = relative.startswith(("agents/bridge/", "_inbox/", "_processed/")) or relative == "BRIDGE.md"
        entries.append({"id": relative, "resolution": resolution,
                        "selected": None if resolution == "unresolved" else selected["path"],
                        "retrieval_policy": "historical-only" if historical else (
                            "return-all-with-conflict" if resolution == "unresolved" else "selected"),
                        "knowledge_status": "legacy-unvalidated",
                        "selection_evidence": 'ordered-line-containment; not functional validation' if resolution == 'additive-superset' else None,
                        "sources": [{"path": c["path"], "sha256": c["sha256"],
                                     "commit": None,
                                     "content_state": "worktree-snapshot-not-attributed",
                                     "metadata": declared_metadata(root / c['path']),
                                     "checkout_commit": report["commit"],
                                     "checkout_dirty": report["dirty"]} for c in copies]})
    return {"version": 1, "generated_at": report["generated_at"],
            "warning": "Checkout commit identifies the baseline, not validation of uncommitted file content; hashes identify current bytes.",
            "summary": {kind: sum(e["resolution"] == kind for e in entries)
                        for kind in ("unique", "byte-identical", "body-equivalent", "additive-superset", "unresolved")},
            "entries": entries}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--root", type=Path, default=CONFIG.parents[2])
    parser.add_argument("--output", required=True, type=Path)
    args = parser.parse_args()
    root = args.root.resolve()
    report = audit(root, json.loads(CONFIG.read_text(encoding="utf-8")))
    catalog = build_catalog(root, report)
    from reviews import apply_reviews, load_reviews
    catalog = apply_reviews(catalog, load_reviews())
    sources = [s for e in catalog['entries'] for s in e['sources']]
    catalog['metadata_coverage'] = {
        'sources': len(sources),
        'with_provenance_and_explicit_unknowns': len(sources),
        'declared_client_and_branch': sum(s['metadata']['scope_state'] == 'declared' for s in sources),
        'metadata_parse_issues': sum(bool(s['metadata']['issues']) for s in sources),
        'independently_validated': 0}
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(catalog, ensure_ascii=False, indent=2), encoding="utf-8")
    print(json.dumps(catalog["summary"]))


if __name__ == "__main__":
    main()
