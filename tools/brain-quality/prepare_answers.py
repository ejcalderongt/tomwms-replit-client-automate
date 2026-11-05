"""Prepare blinded evidence packets from a retrieval report, never gold sources.

Gold concepts, expected source paths and evaluation rubrics are not exposed
to the answerer. #EJC20261027_BRAIN_BLINDED_PACKETS
"""
import argparse
import json
from pathlib import Path
from audit import CONFIG, digest
from rerank import candidate_passage


def prepare(root, report, suite, top_k=2):
    by_case = {c['id']: c for c in suite['cases']}
    packets = []
    for result in report['cases']:
        case = by_case[result['case_id']]
        evidence = []
        for hit in result['results'][:top_k]:
            path = root / hit['path']
            if not path.resolve().is_relative_to(root.resolve()) or 'agent-context' in path.parts:
                raise ValueError('Invalid evidence path')
            text = path.read_text(encoding='utf-8-sig')
            passage = candidate_passage(text, case['query'])
            start = text.find(passage)
            evidence.append({'document_id': hit['id'], 'path': hit['path'], 'sha256': digest(path),
                             'start': start, 'end': start + len(passage), 'quote': passage,
                             'knowledge_status': hit.get('knowledge_status', 'legacy-unvalidated')})
        packets.append({'case_id': case['id'], 'query': case['query'],
                        'scope': {'client': case['client'], 'branch': case['branch']},
                        'instructions': 'Answer only from these retrieved passages. Cite exact spans, distinguish historical evidence from current verified behavior, and abstain when evidence is insufficient.',
                        'retrieved_evidence': evidence})
    return {'retrieval_method': report['method'], 'catalog_sha256': report['catalog_sha256'],
            'suite_sha256': report['suite_sha256'], 'gold_oracle_visible': False, 'packets': packets}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('report', type=Path)
    parser.add_argument('--root', type=Path, default=CONFIG.parents[2])
    parser.add_argument('--output', type=Path, required=True)
    args = parser.parse_args()
    suite = json.loads(Path(__file__).with_name('eval-cases.json').read_text(encoding='utf-8'))
    report = json.loads(args.report.read_text(encoding='utf-8'))
    if report['suite_sha256'] != digest(Path(__file__).with_name('eval-cases.json')):
        parser.error('Suite changed since retrieval evaluation')
    catalog_path = args.root / 'analysis/brain-improvement/source-catalog.json'
    if report['catalog_sha256'] != digest(catalog_path):
        parser.error('Catalog changed since retrieval evaluation')
    # Validate source bytes against the catalog before creating excerpts.
    from evaluate import load_documents
    load_documents(args.root, json.loads(catalog_path.read_text(encoding='utf-8')))
    packets = prepare(args.root, report, suite)
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(packets, ensure_ascii=False, indent=2), encoding='utf-8')
    print(json.dumps({'packets': len(packets['packets']), 'gold_oracle_visible': False}))


if __name__ == '__main__':
    main()
