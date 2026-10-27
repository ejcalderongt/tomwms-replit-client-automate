"""Attach scoped source-reviewed rule candidates; never infer deployment.

#EJC20261027_BRAIN_RULES
"""
from pathlib import Path
from audit import digest


def attach_rules(graph, rules):
    by_id = {node['id']: node for node in graph['nodes']}
    attached = 0
    for rule in rules:
        evidence_issues = []
        for source in rule['sources']:
            snapshot = graph['repositories'].get(source['repo'])
            if not snapshot:
                evidence_issues.append('unknown-repository')
                continue
            root = Path(snapshot['root']).resolve()
            path = (root / source['path']).resolve()
            if not path.is_relative_to(root) or not path.is_file() or digest(path) != source['sha256']:
                evidence_issues.append('source-changed-or-missing')
                continue
            text = path.read_text(encoding='utf-8-sig')
            if source['quote'] not in text:
                evidence_issues.append('literal-evidence-missing')
            if snapshot['commit'] != source['checkout_commit'] or snapshot['branch'] != source['checkout_branch']:
                evidence_issues.append('repository-scope-changed')
        targets = []
        for selector in rule['targets']:
            found = [node for node in graph['nodes'] if node['id'] == selector or
                     (node.get('type', '') + '.' + node.get('name', '')).casefold() == selector.casefold()]
            if len(found) != 1:
                evidence_issues.append('target-missing-or-ambiguous:' + selector)
            targets.extend(found)
        if evidence_issues:
            graph['unresolved'].append({'rule': rule['id'], 'issue': 'rule-not-attached', 'details': evidence_issues})
            continue
        key = 'rule:' + rule['id']
        graph['nodes'].append({'id': key, 'kind': 'functional-rule', 'name': rule['id'],
                               'statement': rule['statement'], 'claim_kind': rule['kind'],
                               'status': rule['status'], 'scope': rule['scope'],
                               'source_state': 'literal-source-checked; semantic review and runtime validation pending',
                               'sources': rule['sources']})
        for node in targets:
            graph['edges'].append({'source': node['id'], 'target': key, 'relation': 'has-rule-candidate',
                                   'confidence': 'source-matched; not runtime-verified',
                                   'evidence': rule['sources'][0]})
        attached += 1
    graph['coverage']['functional_rule_nodes'] = attached
    graph['coverage']['edges'] = len(graph['edges'])
    graph['coverage']['unresolved_reported'] = len(graph['unresolved'])
    return graph
