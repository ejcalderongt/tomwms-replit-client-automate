"""Resolve domain context across both historical router schemas.

No document content is printed. #EJC20261027_BRAIN_ROUTING
"""
import argparse
import json
import re
import unicodedata
from pathlib import Path
import yaml
from audit import CONFIG


def fold(value):
    return ''.join(c for c in unicodedata.normalize('NFKD', str(value).casefold())
                   if not unicodedata.combining(c))


def matches(query, trigger):
    return re.search(r'(?<!\w)' + re.escape(fold(trigger)) + r'(?!\w)', fold(query)) is not None


def router_entries(document):
    domains = document.get('domains', [])
    if isinstance(domains, list):
        for domain in domains:
            triggers = domain.get('triggers', {})
            if isinstance(triggers, dict):
                triggers = triggers.get('keywords', []) + triggers.get('paths', [])
            yield domain.get('archivo'), triggers, []
    elif isinstance(domains, dict):
        for domain in domains.values():
            yield domain.get('file'), domain.get('triggers', []), domain.get('also_load', [])
    clients = document.get('clients', {})
    if isinstance(clients, dict):
        for slug, client in clients.items():
            if isinstance(client, dict) and client.get('file'):
                yield client['file'], client.get('triggers', [slug]), []
        for client in clients.get('archivos', []):
            yield client.get('archivo'), [client.get('slug')], []


def resolve(root, catalog, query):
    by_id = {e['id']: e for e in catalog['entries']}
    wanted = {'agents/coordinator.yml': {'governance-entry'}}
    issues = []
    router = by_id.get('agents/_index.yml', {})
    for source in router.get('sources', []):
        path = root / source['path']
        try:
            document = yaml.safe_load(path.read_text(encoding='utf-8-sig'))
            if not isinstance(document, dict):
                raise ValueError('router must be a mapping')
            for target, triggers, also_load in router_entries(document):
                hits = [t for t in triggers if t and matches(query, t)]
                if not hits or not target:
                    continue
                for value in [target] + also_load:
                    logical = value.removeprefix('brain/')
                    wanted.setdefault(logical, set()).update(hits)
        except (yaml.YAMLError, ValueError) as error:
            issues.append({'path': source['path'], 'issue': type(error).__name__})
    results = []
    for logical, triggers in sorted(wanted.items()):
        entry = by_id.get(logical)
        if not entry:
            issues.append({'path': logical, 'issue': 'missing-target'})
            continue
        sources = entry['sources'] if entry['retrieval_policy'] == 'return-all-with-conflict' else [
            s for s in entry['sources'] if s['path'] == entry['selected']]
        results.append({'id': logical, 'matched': sorted(triggers),
                        'resolution': entry['resolution'],
                        'knowledge_status': entry['knowledge_status'],
                        'sources': sources})
    return {'query': query, 'governance': str(root / 'AGENTS.md'),
            'routing_semantics': 'union of both router schemas; conflicts retained',
            'results': results, 'issues': issues}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('query')
    parser.add_argument('--catalog', type=Path)
    parser.add_argument('--root', type=Path, default=CONFIG.parents[2])
    args = parser.parse_args()
    root = args.root.resolve()
    path = args.catalog or root / 'analysis/brain-improvement/source-catalog.json'
    catalog = json.loads(path.read_text(encoding='utf-8'))
    # Fail rather than route from a stale catalog after content changes.
    from audit import digest
    for entry in catalog['entries']:
        if entry['id'].startswith('agents/') or entry['id'].startswith('client-index/'):
            for source in entry['sources']:
                if not (root / source['path']).is_file() or digest(root / source['path']) != source['sha256']:
                    parser.error('Catalog stale; regenerate catalog.py before routing')
    print(json.dumps(resolve(root, catalog, args.query), ensure_ascii=True, indent=2))


if __name__ == '__main__':
    main()
