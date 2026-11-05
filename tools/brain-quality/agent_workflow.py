"""Plan bounded specialist work and validate independently produced findings.

This CLI does not spawn agents or authorize external actions.
#EJC20261027_BRAIN_AGENT_WORKFLOW
"""
import argparse
import json
from pathlib import Path
from audit import CONFIG, digest
from answer_check import citation_issues
from evidence import allowed_roots

CONTRACT = Path(__file__).with_name('specialist-contracts.json')


def validate_findings(root, packets, findings):
    expected = {p['case_id']: p['scope'] for p in packets['packets']}
    issues, seen, citations = [], set(), 0
    for row in findings:
        case_id = row.get('case_id')
        if case_id not in expected or case_id in seen:
            issues.append({'case_id': case_id, 'issue': 'unknown-or-duplicate-case'})
            continue
        seen.add(case_id)
        if row.get('requested_scope') != expected[case_id]:
            issues.append({'case_id': case_id, 'issue': 'requested-scope-changed'})
        for finding in row.get('findings', []):
            if finding.get('kind') not in {'fact', 'hypothesis'} or not finding.get('statement'):
                issues.append({'case_id': case_id, 'issue': 'invalid-finding'})
            if not finding.get('citations'):
                issues.append({'case_id': case_id, 'finding': finding.get('id'), 'issue': 'citations-missing'})
            for citation in finding.get('citations', []):
                citations += 1
                for issue in citation_issues(root, citation, allowed_roots(root)):
                    issues.append({'case_id': case_id, 'finding': finding.get('id'), 'issue': issue})
    return {'covered': len(seen), 'expected': len(expected), 'missing_cases': sorted(set(expected) - seen),
            'citations_checked': citations, 'issues': issues,
            'source_integrity_passed': not issues and seen == set(expected),
            'semantic_support_reviewed': False}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    commands = parser.add_subparsers(dest='command', required=True)
    plan = commands.add_parser('plan')
    plan.add_argument('query')
    plan.add_argument('--client')
    plan.add_argument('--branch')
    plan.add_argument('--output', required=True, type=Path)
    validate = commands.add_parser('validate')
    validate.add_argument('findings', type=Path)
    validate.add_argument('--packets', required=True, type=Path)
    validate.add_argument('--output', required=True, type=Path)
    parser.add_argument('--root', type=Path, default=CONFIG.parents[2])
    args = parser.parse_args()
    if args.command == 'plan':
        contract = json.loads(CONTRACT.read_text(encoding='utf-8'))
        result = {'query': args.query, 'scope': {'client': args.client, 'branch': args.branch},
                  'state': 'planned', 'contract_sha256': digest(CONTRACT), 'contract': contract,
                  'instruction': 'Use isolated contexts for specialists; coordinator sees their findings, verifier sees anonymized answer pairs and review evidence. Check authorization before dispatch.'}
    else:
        result = validate_findings(args.root, json.loads(args.packets.read_text(encoding='utf-8')),
                                   json.loads(args.findings.read_text(encoding='utf-8')))
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(result, ensure_ascii=False, indent=2), encoding='utf-8')
    print(json.dumps({k: v for k, v in result.items() if k not in {'contract', 'issues'}}))


if __name__ == '__main__':
    main()
