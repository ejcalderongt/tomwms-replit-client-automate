"""Build/query a provenance-bearing possible-dependency graph.

Syntax linkage is not semantic binding or proof of runtime reachability.
#EJC20261027_BRAIN_FUNCTIONAL_GRAPH
"""
import argparse
import collections
import json
import subprocess
from pathlib import Path
from audit import digest


def revision(root):
    def git(*args):
        return subprocess.run(['git', '-C', str(root), *args], check=True,
                              capture_output=True, text=True).stdout.strip()
    return {'root': str(root), 'commit': git('rev-parse', 'HEAD'),
            'branch': git('branch', '--show-current'),
            'dirty': bool(git('status', '--porcelain')),
            'scope': 'local checkout; deployment and runtime state unverified'}


def validate_sources(facts):
    root = Path(facts['repo_root'])
    for item in facts['files'] + facts.get('configuration_sources', []):
        path = (root / item['path']).resolve()
        if not path.is_relative_to(root.resolve()) or digest(path) != item['sha256']:
            raise ValueError('Stale or invalid syntax source: ' + item['path'])
    if facts.get('input_manifest'):
        manifest = facts['input_manifest']
        if digest(Path(manifest['path'])) != manifest['sha256']:
            raise ValueError('SQL input manifest changed')
    if facts.get('inventory_policy'):
        from sql_inventory import discover, POLICY
        if facts['inventory_policy'] != POLICY or set(discover(root)['files']) != {f['path'] for f in facts['files']}:
            raise ValueError('SQL inventory changed; re-export source facts')


def build(vb, java, sql):
    for facts in [vb, java, sql]:
        validate_sources(facts)
    nodes, edges, unresolved = {}, [], []
    snapshots = {'bof': revision(Path(vb['repo_root'])), 'hh': revision(Path(java['repo_root'])),
                 'dba': revision(Path(sql['repo_root']))}
    index = collections.defaultdict(list)
    endpoints = collections.defaultdict(list)
    for language, repo, facts in [('vb', 'bof', vb), ('java', 'hh', java)]:
        for method in facts['methods']:
            key = language + ':' + method['id']
            node = {k: method[k] for k in ('name', 'type', 'path', 'sha256', 'line_start', 'line_end', 'syntax_errors')}
            node.update(id=key, kind='method', language=language, repo=repo,
                        source_state='syntax-observed; not runtime-validated')
            nodes[key] = node
            index[(language, method['type'].casefold(), method['name'].casefold())].append(key)
            if language == 'vb' and method.get('web_method'):
                endpoints[method['name'].casefold()].append(key)

    def edge(source, target, relation, repo, path, sha, line, confidence):
        edges.append({'source': source, 'target': target, 'relation': relation, 'confidence': confidence,
                      'evidence': {'repo': repo, 'path': path, 'sha256': sha, 'line': line}})

    for language, repo, facts in [('vb', 'bof', vb), ('java', 'hh', java)]:
        for method in facts['methods']:
            source = language + ':' + method['id']
            if method['syntax_errors']:
                unresolved.append({'source': source, 'issue': 'file-has-syntax-errors'})
                continue
            for call in method['calls']:
                qualifier = call.get('qualifier')
                if qualifier is None or qualifier.casefold() in {'me', 'myclass', 'this'}:
                    owner = method['type']
                else:
                    owner = qualifier
                candidates = index.get((language, owner.casefold(), call['name'].casefold()), [])
                candidates = [key for key in candidates if not nodes[key]['syntax_errors']]
                if not candidates:
                    # Framework, instance dispatch, arrays and dynamic calls stay unresolved.
                    continue
                confidence = 'syntax-linked' if len(candidates) == 1 else 'ambiguous-overload-or-type'
                for target in candidates:
                    edge(source, target, 'possible-call', repo, method['path'], method['sha256'], call['line'], confidence)
            for route in method.get('ws_endpoints', []):
                candidates = [key for key in endpoints.get(route['name'].casefold(), []) if not nodes[key]['syntax_errors']]
                if not candidates:
                    unresolved.append({'source': source, 'issue': 'literal-ws-route-not-found', 'endpoint': route['name']})
                for target in candidates:
                    edge(source, target, 'literal-ws-route', repo, method['path'], method['sha256'], route['line'],
                         'syntax-linked' if len(candidates) == 1 else 'ambiguous-endpoint')

    definitions = {}
    for file in sql['files']:
        if file['syntax_errors']:
            unresolved.append({'repo': 'dba', 'path': file['path'], 'issue': 'sql-parse-errors'})
            continue
        for definition in file['definitions']:
            key = 'sql:' + definition['name'].casefold()
            nodes.setdefault(key, {'id': key, 'name': definition['name'], 'kind': 'sql-' + definition['kind'],
                                   'repo': 'dba', 'definitions': [], 'source_state': 'repository-DDL; alterations are targets, not reconstructed schema; deployment-unverified'})
            nodes[key]['definitions'].append({'path': file['path'], 'sha256': file['sha256'],
                                             'line': definition['line_start']})
            definitions.setdefault(definition['name'].casefold(), key)

    def sql_target(name):
        normalized = name.replace('[', '').replace(']', '').casefold()
        if normalized in definitions:
            return definitions[normalized], 'exact-script-object'
        if '.' not in normalized:
            matches = [key for name, key in definitions.items() if name.split('.')[-1] == normalized]
            if len(matches) == 1:
                return matches[0], 'unique-script-name; default-schema-unverified'
        key = 'sql-unresolved:' + normalized
        nodes.setdefault(key, {'id': key, 'name': name, 'kind': 'sql-object-unresolved',
                               'source_state': 'reference-only; schema/type/existence-unverified'})
        return key, 'unresolved-sql-definition'

    for method in vb['methods']:
        for reference in method['sql_references']:
            if method['syntax_errors']:
                continue
            target, resolution = sql_target(reference['name'])
            edge('vb:' + method['id'], target, 'sql-literal-reference', 'bof', method['path'], method['sha256'],
                 reference['line'], 'candidate/' + resolution)
    for file in sql['files']:
        if file['syntax_errors']:
            continue
        for reference in file['references']:
            if reference['temporary']:
                continue
            owners = [d for d in file['definitions'] if d['offset'] <= reference['offset'] < d['offset'] + d['length']]
            if len(owners) != 1:
                unresolved.append({'repo': 'dba', 'path': file['path'], 'issue': 'sql-reference-owner-ambiguous'})
                continue
            source = definitions[owners[0]['name'].casefold()]
            target, resolution = sql_target(reference['name'])
            edge(source, target, 'sql-' + reference['relation'], 'dba', file['path'], file['sha256'],
                 reference['line'], 'syntax-linked/' + resolution)
    alternative_sql = {n['id'] for n in nodes.values() if len(n.get('definitions', [])) > 1}
    for relation in edges:
        if relation['source'] in alternative_sql or relation['target'] in alternative_sql:
            relation['confidence'] += '/multiple-DDL-locations; active-version-unverified'
    return {'version': 1, 'scope': 'Possible dependencies in inspected source; not exhaustive and not a runtime execution graph',
            'repositories': snapshots, 'nodes': list(nodes.values()), 'edges': edges, 'unresolved': unresolved,
            'source_snapshots': {repo: {'repo_root': facts['repo_root'], 'files': facts['files'],
                                      'configuration_sources': facts.get('configuration_sources', []),
                                      'input_manifest': facts.get('input_manifest'),
                                      'inventory_policy': facts.get('inventory_policy'),
                                      'source_root': facts.get('source_root')}
                                 for repo, facts in [('bof', vb), ('hh', java), ('dba', sql)]},
            'coverage': {'vb_files': len(vb['files']), 'java_files': len(java['files']), 'sql_scripts': len(sql['files']),
                         'method_nodes': sum(n['kind'] == 'method' for n in nodes.values()),
                         'edges': len(edges), 'unresolved_reported': len(unresolved),
                         'unresolved_instance_or_framework_calls': 'not enumerated; no false completeness claim'}}


def neighbors(graph, identifier, direction='dependencies', depth=2):
    if graph.get('rule_input') and digest(Path(graph['rule_input']['path'])) != graph['rule_input']['sha256']:
        raise ValueError('Rule inputs changed; rebuild graph')
    for repo, facts in graph.get('source_snapshots', {}).items():
        validate_sources(facts)
        if facts.get('source_root'):
            root = Path(facts['repo_root'])
            actual = {p.relative_to(root).as_posix() for p in (root / facts['source_root']).rglob('*.java')}
            if actual != {f['path'] for f in facts['files']}:
                raise ValueError('Source inventory changed; rebuild graph: ' + repo)
    nodes = {n['id']: n for n in graph['nodes']}
    selected = [n['id'] for n in graph['nodes'] if n['id'] == identifier or n.get('name', '').casefold() == identifier.casefold()
                or (n.get('type', '') + '.' + n.get('name', '')).casefold() == identifier.casefold()]
    adjacency = collections.defaultdict(list)
    for edge in graph['edges']:
        adjacency[edge['source'] if direction == 'dependencies' else edge['target']].append(edge)
    seen, found = set(selected), []
    frontier = selected
    for _ in range(depth):
        next_frontier = []
        for key in frontier:
            for edge in adjacency[key]:
                target = edge['target'] if direction == 'dependencies' else edge['source']
                found.append(edge)
                if target not in seen:
                    seen.add(target)
                    next_frontier.append(target)
        frontier = next_frontier
    return {'matched': selected, 'nodes': [nodes[key] for key in sorted(seen)], 'edges': found,
            'warning': graph['scope']}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    commands = parser.add_subparsers(dest='command', required=True)
    create = commands.add_parser('build')
    for name in ('vb', 'java', 'sql', 'output'):
        create.add_argument('--' + name, required=True, type=Path)
    create.add_argument('--rules', type=Path, default=Path(__file__).with_name('functional-rules.json'))
    query = commands.add_parser('query')
    query.add_argument('graph', type=Path)
    query.add_argument('symbol')
    query.add_argument('--direction', choices=['dependencies', 'impact'], default='dependencies')
    query.add_argument('--depth', type=int, choices=range(1, 7), default=2)
    args = parser.parse_args()
    if args.command == 'build':
        result = build(*[json.loads(getattr(args, name).read_text(encoding='utf-8')) for name in ('vb', 'java', 'sql')])
        if args.rules.is_file():
            from graph_rules import attach_rules
            result = attach_rules(result, json.loads(args.rules.read_text(encoding='utf-8')))
            result['rule_input'] = {'path': str(args.rules.resolve()), 'sha256': digest(args.rules)}
        args.output.parent.mkdir(parents=True, exist_ok=True)
        args.output.write_text(json.dumps(result, ensure_ascii=False, indent=2), encoding='utf-8')
        print(json.dumps(result['coverage']))
    else:
        result = neighbors(json.loads(args.graph.read_text(encoding='utf-8')), args.symbol, args.direction, args.depth)
        print(json.dumps(result, ensure_ascii=True, indent=2))


if __name__ == '__main__':
    main()
