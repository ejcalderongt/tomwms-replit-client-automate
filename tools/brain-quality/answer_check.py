"""Check source-grounded answer records without pretending to judge entailment.

Source hashes and literal spans are mechanical checks. Support, diagnosis and
dependency quality require explicit reviewer adjudication.
#EJC20261027_BRAIN_ANSWER_EVAL
"""
import argparse
import hashlib
import json
from pathlib import Path
from audit import CONFIG, digest
from evidence import allowed_roots, citation_bytes, decoded


def citation_issues(root, citation, source_roots=()):
    if not isinstance(citation, dict):
        return ['citation-must-be-object']
    try:
        raw = citation_bytes(root, citation, source_roots)
    except ValueError as error:
        return [str(error)]
    if hashlib.sha256(raw).hexdigest() != citation.get('sha256'):
        return ['source-hash-mismatch']
    start, end = citation.get('start'), citation.get('end')
    if type(start) is not int or type(end) is not int or start < 0 or end <= start:
        return ['invalid-span']
    try:
        text = decoded(raw)
    except UnicodeDecodeError:
        return ['unsupported-source-encoding']
    if end > len(text) or text[start:end] != citation.get('quote'):
        return ['quote-span-mismatch']
    return []


def check_answer(root, case, answer, source_roots=()):
    issues = []
    if answer.get('case_id') != case['id']:
        issues.append('case-id-mismatch')
    claims = answer.get('claims', [])
    if not isinstance(claims, list):
        return {'case_id': case['id'], 'issues': issues + ['claims-must-be-array'], 'metrics': None}
    unlisted = answer.get('unlisted_material_claims', [])
    if unlisted:
        issues.append('unlisted-material-claims')
    claim_ids = [c.get('id') for c in claims]
    if None in claim_ids or len(set(claim_ids)) != len(claim_ids):
        issues.append('claim-ids-missing-or-duplicated')
    checked, reviewed, unsupported = 0, 0, 0
    for claim in claims:
        if claim.get('kind') not in {'fact', 'hypothesis', 'decision'}:
            issues.append(str(claim.get('id')) + ':invalid-kind')
        citations = claim.get('citations', [])
        source_issues = []
        if not citations:
            source_issues.append('citations-required')
        for citation in citations:
            source_issues.extend(citation_issues(root, citation, source_roots))
        if not source_issues:
            checked += 1
        issues.extend(str(claim.get('id')) + ':' + problem for problem in source_issues)
        review = claim.get('review', {})
        if review.get('support') in {'supported', 'unsupported', 'contradicted'}:
            if not review.get('reviewer') or not review.get('rationale'):
                issues.append(str(claim.get('id')) + ':incomplete-adjudication')
            elif source_issues and review['support'] == 'supported':
                issues.append(str(claim.get('id')) + ':support-requires-valid-citations')
            else:
                reviewed += 1
                unsupported += review['support'] != 'supported'
    # An answer must not claim unrestricted applicability to another client/branch.
    scope = answer.get('scope', {})
    for key in ('client', 'branch'):
        if case.get(key) is not None and scope.get(key) != case[key]:
            issues.append('scope-mismatch:' + key)
        if case.get(key) is None and scope.get(key) is not None:
            issues.append('unsupported-scope-inference:' + key)
    quality = {}
    for key in ('diagnosis', 'dependencies', 'scope_correctness'):
        review = answer.get('evaluation', {}).get(key, {})
        valid = (type(review.get('score')) is int and 0 <= review['score'] <= 2
                 and review.get('reviewer') and review.get('rationale'))
        quality[key] = review['score'] if valid else None
    abstention = answer.get('abstention_review', {})
    abstention_reviewed = (type(abstention.get('appropriate')) is bool and bool(abstention.get('reviewer'))
                           and bool(abstention.get('rationale')))
    return {'case_id': case['id'], 'issues': issues,
            'claim_count': len(claims), 'claim_reviews_complete': reviewed == len(claims) if claims else abstention_reviewed,
            'unlisted_material_claim_count': len(unlisted),
            'metrics': {'citation_integrity': checked / len(claims) if claims else None,
                        'adjudication_coverage': reviewed / len(claims) if claims else None,
                        'unsupported_claim_rate': unsupported / reviewed if reviewed else None,
                        **quality},
            'limitations': 'Exact source matches prove citation integrity, not semantic support; reviewer labels must be independently audited.'}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('answers', type=Path)
    parser.add_argument('--root', type=Path, default=CONFIG.parents[2])
    parser.add_argument('--output', type=Path, required=True)
    args = parser.parse_args()
    suite = json.loads(Path(__file__).with_name('eval-cases.json').read_text(encoding='utf-8'))
    cases = {c['id']: c for c in suite['cases']}
    answers = json.loads(args.answers.read_text(encoding='utf-8'))
    ids = [a.get('case_id') for a in answers]
    if len(ids) != len(set(ids)) or any(key not in cases for key in ids):
        parser.error('Duplicate or unknown answer case IDs')
    results = [check_answer(args.root, cases[a['case_id']], a, allowed_roots(args.root)) for a in answers]
    complete = len(results) == len(cases) and all(r.get('claim_reviews_complete') and r['metrics']
                      and all(r['metrics'][key] is not None for key in ('diagnosis', 'dependencies', 'scope_correctness'))
                      for r in results)
    report = {'evaluated': len(results), 'missing_cases': sorted(set(cases) - set(ids)),
              'cases': results, 'complete': complete, 'evaluation_complete': complete,
              'answer_integrity_passed': not any(r['issues'] for r in results),
              'completion_scope': 'All answers adjudicated; does not mean all answers are correct'}
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(report, ensure_ascii=False, indent=2), encoding='utf-8')
    print(json.dumps({'evaluated': report['evaluated'], 'missing': len(report['missing_cases']), 'complete': report['complete']}))


if __name__ == '__main__':
    main()
