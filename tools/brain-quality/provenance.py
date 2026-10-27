"""Validate claims against current source bytes; no source bodies in output.

#EJC20261027_BRAIN_PROVENANCE
"""
import argparse
import hashlib
import json
import re
import subprocess
from datetime import datetime
from pathlib import Path
from audit import CONFIG


def validate_claim(root, claim, config):
    issues = []
    if not isinstance(claim, dict):
        return ['claim-must-be-object']
    for key in config['claim_required_fields']:
        if key not in claim:
            issues.append('missing:' + key)
    if claim.get('kind') not in config['claim_kinds']:
        issues.append('invalid-kind')
    if claim.get('status') not in config['claim_statuses']:
        issues.append('invalid-status')
    for field in ('id', 'statement'):
        if not isinstance(claim.get(field), str) or not claim[field].strip():
            issues.append('empty:' + field)
    for field in ('clients', 'branches'):
        if not isinstance(claim.get(field), list) or not claim[field] or any(
                not isinstance(v, str) or not v.strip() for v in claim[field]):
            issues.append('explicit-scope-required:' + field)
    verified = claim.get('status') == 'verified'
    date = claim.get('validated_at')
    if date:
        try:
            datetime.fromisoformat(date)
        except (ValueError, TypeError):
            issues.append('invalid-validation-date')
    elif verified:
        issues.append('verified-requires-validation-date')
    if verified and claim.get('kind') == 'hypothesis':
        issues.append('hypothesis-cannot-be-verified-fact')
    if verified and not claim.get('validation_evidence'):
        issues.append('verified-requires-validation-evidence')
    sources = claim.get('sources')
    if not isinstance(sources, list) or not sources:
        return issues + ['sources-required']
    for index, source in enumerate(sources):
        prefix = f'source:{index}:'
        if not isinstance(source, dict):
            issues.append(prefix + 'must-be-object')
            continue
        for key in config['source_required_fields']:
            if key not in source:
                issues.append(prefix + 'missing:' + key)
        raw = source.get('path')
        if not isinstance(raw, str) or not raw:
            issues.append(prefix + 'invalid-path')
            continue
        path = (root / raw).resolve()
        if Path(raw).is_absolute() or not path.is_relative_to(root.resolve()):
            issues.append(prefix + 'path-outside-root')
            continue
        if 'agent-context' in path.relative_to(root.resolve()).parts:
            issues.append(prefix + 'restricted-context')
            continue
        if not path.is_file():
            issues.append(prefix + 'missing-file')
        elif hashlib.sha256(path.read_bytes()).hexdigest() != source.get('sha256'):
            issues.append(prefix + 'hash-mismatch')
        commit = source.get('commit')
        if commit is not None and (not isinstance(commit, str) or not re.fullmatch(r'[0-9a-f]{40}', commit)):
            issues.append(prefix + 'invalid-commit')
        if verified and (commit is None or source.get('content_state') != 'committed'):
            issues.append(prefix + 'verified-source-must-be-committed')
        elif verified and isinstance(commit, str) and re.fullmatch(r'[0-9a-f]{40}', commit):
            # A plausible SHA is not proof that this file existed in that commit.
            relative = path.relative_to(root.resolve()).as_posix()
            result = subprocess.run(['git', '-C', str(root), 'show', f'{commit}:{relative}'],
                                    capture_output=True)
            if result.returncode or hashlib.sha256(result.stdout).hexdigest() != source.get('sha256'):
                issues.append(prefix + 'commit-content-mismatch')
    return issues


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('claims', type=Path)
    parser.add_argument('--root', type=Path, default=CONFIG.parents[2])
    args = parser.parse_args()
    config = json.loads(CONFIG.read_text(encoding='utf-8'))
    claims = json.loads(args.claims.read_text(encoding='utf-8'))
    results = [{'id': c.get('id'), 'issues': validate_claim(args.root, c, config)} for c in claims]
    print(json.dumps({'claims': len(results), 'invalid': sum(bool(c['issues']) for c in results),
                      'results': results}, ensure_ascii=True, indent=2))
    raise SystemExit(1 if any(c['issues'] for c in results) else 0)


if __name__ == '__main__':
    main()
