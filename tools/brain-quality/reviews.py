"""Apply hash-bound source reconciliation decisions. #EJC20261027_BRAIN_REVIEW"""
import json
from pathlib import Path

REVIEW_PATH = Path(__file__).with_name('source-reviews.json')


def apply_reviews(catalog, reviews):
    lookup = {r['id']: r for r in reviews}
    applied = 0
    for entry in catalog['entries']:
        review = lookup.get(entry['id'])
        if not review:
            continue
        actual = {s['path']: s['sha256'] for s in entry['sources']}
        if actual != review['source_hashes']:
            entry['review_issue'] = 'source-changed-since-review'
            continue
        if entry['resolution'] != 'unresolved':
            entry['review_issue'] = 'review-no-longer-applicable'
            continue
        if review['action'] not in {'retain-alternatives', 'exclude-derived-index', 'prefer-reviewed-source'}:
            raise ValueError('Unsupported review action')
        if review['action'] == 'prefer-reviewed-source':
            if review.get('selected_path') not in actual:
                raise ValueError('Reviewed source selection is missing')
            entry.update(resolution='reviewed-selected', selected=review['selected_path'],
                         retrieval_policy='selected', knowledge_status='legacy-unvalidated', review=review)
            applied += 1
            continue
        entry.update(resolution='reviewed-alternatives', selected=None,
                     retrieval_policy='exclude-derived' if review['action'] == 'exclude-derived-index'
                     else 'return-all-with-conflict',
                     knowledge_status='disputed', review=review)
        applied += 1
    catalog['summary']['unresolved'] = sum(e['resolution'] == 'unresolved' for e in catalog['entries'])
    catalog['summary']['reviewed-alternatives'] = sum(e['resolution'] == 'reviewed-alternatives' for e in catalog['entries'])
    catalog['summary']['reviewed-selected'] = sum(e['resolution'] == 'reviewed-selected' for e in catalog['entries'])
    return catalog


def load_reviews():
    return json.loads(REVIEW_PATH.read_text(encoding='utf-8')) if REVIEW_PATH.is_file() else []
