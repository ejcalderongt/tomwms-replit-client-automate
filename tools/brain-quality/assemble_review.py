"""Unblind frozen reviews, preserve first-pass results and summarize corrections.

#EJC20261027_BRAIN_REVIEW_RESULTS
"""
import argparse
import copy
import json
import statistics
from pathlib import Path
from audit import CONFIG, digest
from answer_check import check_answer
from evidence import allowed_roots


def apply_judgements(answers, mapping, judgements, method):
    source = {a['case_id']: a for a in answers}
    lookup = {(j['case_id'], j['variant']): j for j in judgements}
    if len(lookup) != len(judgements):
        raise ValueError('Duplicate adjudications')
    output = []
    for key in mapping['mapping']:
        if key['method'] != method:
            continue
        row = copy.deepcopy(source[key['case_id']])
        judgement = lookup[(key['case_id'], key['variant'])]
        claims = {claim['id']: claim for claim in judgement['claims']}
        if set(claims) != set(key['claim_ids']):
            raise ValueError('Judged claim IDs differ from frozen answer')
        by_original = {claim['id']: claim for claim in row['claims']}
        for blind_id, original_id in key['claim_ids'].items():
            by_original[original_id]['review'] = {k: v for k, v in claims[blind_id].items() if k != 'id'}
        row['evaluation'] = judgement['evaluation']
        row['unlisted_material_claims'] = judgement.get('unlisted_material_claims', [])
        if judgement.get('abstention_review'):
            row['abstention_review'] = judgement['abstention_review']
        output.append(row)
    return output


def summarize(root, cases, answers):
    checks = [check_answer(root, cases[a['case_id']], a, allowed_roots(root)) for a in answers]
    if any(not c.get('claim_reviews_complete') or not c['metrics'] for c in checks):
        raise ValueError('Incomplete adjudication')
    claims = [claim for answer in answers for claim in answer['claims']]
    unlisted = [claim for answer in answers for claim in answer.get('unlisted_material_claims', [])]
    unsupported = sum(c['review']['support'] != 'supported' for c in claims)
    unsupported_unlisted = sum(c.get('support') != 'supported' for c in unlisted)
    metrics = ('diagnosis', 'dependencies', 'scope_correctness')
    return {'cases': len(answers), 'listed_claims': len(claims), 'unlisted_assertions': len(unlisted),
            'unsupported_listed': unsupported, 'unsupported_unlisted': unsupported_unlisted,
            'unsupported_assertion_rate': (unsupported + unsupported_unlisted) / max(1, len(claims) + len(unlisted)),
            'scores_0_to_2': {m: statistics.mean(c['metrics'][m] for c in checks) for m in metrics},
            'by_split': {split: {m: statistics.mean(c['metrics'][m] for c in checks
                                                   if cases[c['case_id']]['split'] == split) for m in metrics}
                         for split in sorted({cases[c['case_id']]['split'] for c in checks})},
            'case_checks': checks}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--root', type=Path, default=CONFIG.parents[2])
    args = parser.parse_args()
    directory = args.root / 'analysis/brain-improvement'
    def read(name):
        return json.loads((directory / name).read_text(encoding='utf-8'))
    baseline = read('baseline-answers.json')
    coordinated = read('coordinated-answers.json')
    corrected = read('coordinated-answers-corrected.json')
    mapping = read('private-review-mapping.json')
    corrected_mapping = read('private-corrected-review-mapping.json')
    for value, filename in [(mapping['input_sha256']['baseline'], 'baseline-answers.json'),
                            (mapping['input_sha256']['coordinated'], 'coordinated-answers.json'),
                            (corrected_mapping['input_sha256']['coordinated'], 'coordinated-answers-corrected.json')]:
        if value != digest(directory / filename):
            raise ValueError('Frozen input changed: ' + filename)
    judgements = read('blind-adjudications.json')
    corrections = read('correction-adjudications.json')
    changed = {a['case_id'] for a, b in zip(coordinated, corrected) if a != b}
    if changed != {'WMS-EVAL-19', 'WMS-EVAL-22'}:
        raise ValueError('Unexpected correction scope')
    selected = {(m['case_id'], m['variant']) for m in corrected_mapping['mapping']
                if m['method'] == 'coordinated' and m['case_id'] in changed}
    # Preserve baseline ratings and original first-pass records. Only changed
    # coordinated answers receive post-feedback judgments, explicitly labelled.
    replacement = {(j['case_id'], j['variant']): j for j in corrections if (j['case_id'], j['variant']) in selected}
    if set(replacement) != selected:
        raise ValueError('Correction reviews incomplete')
    post_review = [replacement.get((j['case_id'], j['variant']), j) for j in judgements]
    methods = {
        'baseline': apply_judgements(baseline, mapping, judgements, 'baseline'),
        'coordinated_first_pass': apply_judgements(coordinated, mapping, judgements, 'coordinated'),
        'coordinated_after_review': apply_judgements(corrected, corrected_mapping, post_review, 'coordinated')}
    suite_path = Path(__file__).with_name('eval-cases.json')
    cases = {c['id']: c for c in json.loads(suite_path.read_text(encoding='utf-8'))['cases']}
    results = {}
    for name, answers in methods.items():
        (directory / (name + '-reviewed.json')).write_text(json.dumps(answers, ensure_ascii=False, indent=2), encoding='utf-8')
        results[name] = summarize(args.root, cases, answers)
    baseline_scores = results['baseline']
    proposed = results['coordinated_after_review']
    nonregressing = all(proposed['by_split'][split][m] >= baseline_scores['by_split'][split][m]
                        for split in baseline_scores['by_split'] for m in ('diagnosis', 'dependencies', 'scope_correctness'))
    report = {'version': 1, 'suite_sha256': digest(suite_path), 'results': results,
              'selective_specialist_mode_supported_in_sample': nonregressing and
              proposed['unsupported_assertion_rate'] <= baseline_scores['unsupported_assertion_rate'],
              'baseline_review_repeated_during_correction': 'Repeated control annotations are retained separately; the frozen baseline ratings remain authoritative for the primary comparison.',
              'limitations': ['One same-model independent-context judge, not human or cross-model validation',
                             'Corrected results are post-feedback, not a fresh held-out experiment',
                             'Retrieval, specialist evidence and synthesis changed together; no causal attribution to agent count alone',
                             'Full LLM latency and monetary/token costs are unavailable; artifact-only timings are not reasoning latency',
                             'Results apply to 25 documented cases and do not prove current deployment behavior']}
    (directory / 'answer-comparison.json').write_text(json.dumps(report, ensure_ascii=False, indent=2), encoding='utf-8')
    print(json.dumps({'results': {name: {k: v for k, v in result.items() if k != 'case_checks'}
                                  for name, result in results.items()},
                      'selective_supported': report['selective_specialist_mode_supported_in_sample']}))


if __name__ == '__main__':
    main()
