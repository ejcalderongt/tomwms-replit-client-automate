"""Compare retrieval reports only when their corpus, suite and cases agree.

#EJC20261027_BRAIN_COMPARISON
"""
import argparse
import json
from pathlib import Path


def compare(baseline, candidate):
    for key in ('catalog_sha256', 'suite_sha256'):
        if not baseline.get(key) or baseline.get(key) != candidate.get(key):
            raise ValueError('Incomparable report: ' + key)
    original = {case['case_id']: case for case in baseline['cases']}
    proposed = {case['case_id']: case for case in candidate['cases']}
    if len(original) != len(baseline['cases']) or len(proposed) != len(candidate['cases']):
        raise ValueError('Duplicate case IDs')
    if set(original) != set(proposed):
        raise ValueError('Case sets differ')
    if any(original[key]['split'] != proposed[key]['split'] for key in original):
        raise ValueError('Case split changed')
    paired = []
    for key in sorted(original):
        paired.append({'case_id': key, 'split': original[key]['split'],
                       'delta': {metric: proposed[key]['retrieval'][metric] - original[key]['retrieval'][metric]
                                 for metric in ('hit@5', 'mrr@5', 'evidence-recall@5')}})
    groups = {}
    for split in sorted({row['split'] for row in paired} | {'all'}):
        rows = [row for row in paired if split == 'all' or row['split'] == split]
        groups[split] = {metric: sum(row['delta'][metric] for row in rows) / len(rows)
                         for metric in ('hit@5', 'mrr@5', 'evidence-recall@5')}
    nonregressing = all(values['hit@5'] >= -1e-12 and values['evidence-recall@5'] >= -1e-12
                        and values['mrr@5'] >= -1e-12 for values in groups.values())
    improved = any(value > 1e-12 for value in groups['all'].values())
    return {'baseline': baseline['method'], 'candidate': candidate['method'],
            'deltas': groups, 'paired_cases': paired,
            'retrieval_candidate_supported': nonregressing and improved,
            'generation_quality_proven': False,
            'decision_scope': 'retrieval only; excludes diagnosis, scope correctness and unsupported claims',
            'candidate_latency_ms': candidate.get('latency_ms'),
            'candidate_cost': candidate.get('cost')}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('baseline', type=Path)
    parser.add_argument('candidate', type=Path)
    parser.add_argument('--output', required=True, type=Path)
    args = parser.parse_args()
    report = compare(json.loads(args.baseline.read_text(encoding='utf-8')),
                     json.loads(args.candidate.read_text(encoding='utf-8')))
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(report, indent=2), encoding='utf-8')
    print(json.dumps({key: value for key, value in report.items() if key != 'paired_cases'}))


if __name__ == '__main__':
    main()
