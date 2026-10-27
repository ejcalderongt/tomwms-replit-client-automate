"""Read only explicitly allowed current files or immutable Git blobs.

#EJC20261027_BRAIN_EVIDENCE
"""
import json
import os
import re
import subprocess
from pathlib import Path


def allowed_roots(brain_root):
    config = json.loads(Path(__file__).with_name('source-roots.json').read_text(encoding='utf-8'))
    roots = [brain_root.resolve()]
    for value in config['roots'].values():
        path = Path(os.path.expandvars(value))
        roots.append((path if path.is_absolute() else brain_root / path).resolve())
    return roots


def citation_bytes(brain_root, citation, source_roots=()):
    selected = Path(citation.get('repo_root', brain_root)).resolve()
    if selected not in {brain_root.resolve(), *(Path(p).resolve() for p in source_roots)}:
        raise ValueError('source-root-not-allowed')
    relative = citation.get('path')
    if not isinstance(relative, str) or not relative:
        raise ValueError('missing-source-path')
    path = (selected / relative).resolve()
    if Path(relative).is_absolute() or not path.is_relative_to(selected):
        raise ValueError('source-outside-root')
    if any(part.casefold() in {'.git', 'agent-context'} for part in Path(relative).parts):
        raise ValueError('restricted-source')
    ref = citation.get('git_ref')
    if ref is not None:
        if not isinstance(ref, str) or not ref:
            raise ValueError('invalid-git-reference')
        commit = subprocess.run(['git', '-C', str(selected), 'rev-parse', '--verify', '--end-of-options',
                                 ref + '^{commit}'], capture_output=True, check=False)
        sha = commit.stdout.decode('ascii', errors='replace').strip()
        if commit.returncode or not re.fullmatch(r'[0-9a-f]{40}', sha):
            raise ValueError('invalid-git-reference')
        blob = subprocess.run(['git', '-C', str(selected), 'show', sha + ':' + Path(relative).as_posix()],
                              capture_output=True, check=False)
        if blob.returncode:
            raise ValueError('missing-git-source')
        return blob.stdout
    if not path.is_file():
        raise ValueError('missing-source')
    return path.read_bytes()


def decoded(raw):
    return raw.decode('utf-8-sig').replace('\r\n', '\n').replace('\r', '\n')
