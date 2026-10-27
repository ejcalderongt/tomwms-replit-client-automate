"""Discover Git-visible SQL scripts, including legacy extensionless files.

#EJC20261027_BRAIN_SQL_INVENTORY
"""
import argparse
import json
import os
import subprocess
from pathlib import Path

POLICY = 'git-visible-sql-or-extensionless-max8MiB-v2'
EXCLUDED = {'.git', 'brain', 'bin', 'obj', 'node_modules', '__pycache__'}


def discover(root):
    result = subprocess.run(['git', '-C', str(root), 'ls-files', '--cached', '--others',
                             '--exclude-standard', '-z'], capture_output=True, check=True)
    selected, skipped, seen_paths = [], [], set()
    for name in sorted(set(result.stdout.decode('utf-8').split('\0')) - {''}):
        relative = Path(name)
        if EXCLUDED.intersection(relative.parts) or relative.name.startswith('.'):
            continue
        if relative.suffix.lower() not in {'', '.sql'} or relative.name.lower() in {'readme', 'license'}:
            continue
        path = (root / relative).resolve()
        identity = str(path).casefold() if os.name == 'nt' else str(path)
        if identity in seen_paths:
            skipped.append({'path': name, 'reason': 'case-insensitive-path-alias'})
            continue
        seen_paths.add(identity)
        if not path.is_relative_to(root.resolve()) or not path.is_file():
            skipped.append({'path': name, 'reason': 'missing-or-outside-root'})
        elif path.stat().st_size > 8 * 1024 * 1024:
            skipped.append({'path': name, 'reason': 'larger-than8MiB'})
        else:
            selected.append(relative.as_posix())
    return {'policy': POLICY, 'files': selected, 'skipped': skipped,
            'scope': 'Git-visible .sql and extensionless files; contents may be historical or not deployed'}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--root', type=Path, required=True)
    parser.add_argument('--output', type=Path, required=True)
    args = parser.parse_args()
    report = discover(args.root.resolve())
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(report, ensure_ascii=False, indent=2), encoding='utf-8')
    print(json.dumps({'sql_candidates': len(report['files']), 'skipped': len(report['skipped'])}))


if __name__ == '__main__':
    main()
