"""Document-level lexical baseline and reproducible retrieval scoring.

The predicate matches the existing HTML viewer (all whitespace terms must
occur); this is not a measurement of the deployed Brain API or LLM reasoning.
#EJC20261027_BRAIN_BASELINE
"""
import argparse
import hashlib
import json
import statistics
import time
from pathlib import Path
from audit import CONFIG, digest


def load_documents(root, catalog):
    documents = []
    for entry in catalog['entries']:
        if entry['retrieval_policy'] in {'historical-only', 'exclude-derived'}:
            continue
        sources = entry['sources'] if entry['retrieval_policy'] == 'return-all-with-conflict' else [
            s for s in entry['sources'] if s['path'] == entry['selected']]
        for source in sources:
            path = root / source['path']
            if not path.is_file() or digest(path) != source['sha256']:
                raise ValueError('Stale corpus: ' + source['path'])
            documents.append({'id': entry['id'], 'path': source['path'],
                              'metadata': source.get('metadata', {}),
                              'knowledge_status': entry['knowledge_status'],
                              'haystack': (entry['id'] + '\n' + path.read_text(encoding='utf-8-sig')).lower()})
    return documents


def viewer_baseline(documents, query, top_k=5):
    terms = [t for t in query.strip().lower().split() if t]
    seen = set()
    results = []
    for document in documents:
        if document['id'] not in seen and all(term in document['haystack'] for term in terms):
            results.append({'id': document['id'], 'path': document['path']})
            seen.add(document['id'])
            if len(results) >= top_k:
                break
    return results


def retrieval_metrics(expected, results):
    ids = [r['id'] for r in results]
    ranks = [ids.index(e) + 1 for e in expected if e in ids]
    return {'hit@5': int(bool(ranks)), 'mrr@5': 1 / min(ranks) if ranks else 0,
            'evidence-recall@5': len(set(expected).intersection(ids)) / len(set(expected))}


def run(root, catalog, suite, method='viewer-and', runtime=None):
    started = time.perf_counter()
    documents = load_documents(root, catalog)
    semantic = None
    if method in {'bm25', 'semantic', 'hybrid'}:
        from retrieve import BM25
        index = BM25(documents)
    if method in {'semantic', 'hybrid'}:
        from semantic import SemanticIndex, HybridIndex
        if runtime is None:
            raise ValueError('Local runtime cache directory is required')
        semantic = SemanticIndex(documents, runtime)
        index = semantic if method == 'semantic' else HybridIndex(index, semantic)
    preparation_ms = (time.perf_counter() - started) * 1000
    available = {d['id'] for d in documents}
    results = []
    for case in suite['cases']:
        for source in case.get('oracle_sources', []):
            if digest(root / source['path']) != source['sha256']:
                raise ValueError('Oracle source changed; review case: ' + case['id'])
        if not set(case['expected_sources']).issubset(available):
            raise ValueError('Expected evidence missing: ' + case['id'])
        started = time.perf_counter()
        found = viewer_baseline(documents, case['query']) if method == 'viewer-and' else index.search(case['query'])
        elapsed = (time.perf_counter() - started) * 1000
        results.append({'case_id': case['id'], 'split': case['split'], 'results': found,
                        'retrieval': retrieval_metrics(case['expected_sources'], found),
                        'latency_ms': elapsed,
                        'answer_evaluation': {key: None for key in (
                            'scope_correctness', 'diagnosis', 'dependencies', 'unsupported_claim_rate')},
                        'answer_evaluation_status': 'not-run; no generated answer'})
    latencies = sorted(r['latency_ms'] for r in results)
    semantic_info = {'identity': semantic.identity, 'cache_hits': semantic.cache_hits,
                     'cache_misses': semantic.cache_misses, 'chunks': len(semantic.owners)} if semantic else None
    if semantic:
        semantic.close()
    return {'version': 1, 'method': method + '/document-adapter', 'semantic': semantic_info,
            'limitations': ['Not the deployed Brain API', 'Document corpus rather than original HTML chunk index',
                           'No LLM answers generated; retrieval quality does not establish reasoning quality',
                           'Case oracles describe documented incidents, not current runtime validation'],
            'corpus_documents': len(available), 'corpus_variants': len(documents),
            'preparation_ms': preparation_ms,
            'aggregate': {key: statistics.mean(r['retrieval'][key] for r in results)
                          for key in ('hit@5', 'mrr@5', 'evidence-recall@5')},
            'by_split': {split: {'cases': sum(r['split'] == split for r in results),
                                **{key: statistics.mean(r['retrieval'][key] for r in results if r['split'] == split)
                                   for key in ('hit@5', 'mrr@5', 'evidence-recall@5')}}
                         for split in sorted({r['split'] for r in results})},
            'latency_ms': {'median': statistics.median(latencies),
                           'p95': latencies[max(0, int(len(latencies) * .95 + .999) - 1)]},
            'cost': {'external_model_calls': 0, 'model_tokens': None if semantic else 0,
                     'monetary_total': None, 'reason': 'Local retrieval only; hardware costs not measured'},
            'cases': results}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--root', type=Path, default=CONFIG.parents[2])
    parser.add_argument('--output', required=True, type=Path)
    parser.add_argument('--method', choices=['viewer-and', 'bm25', 'semantic', 'hybrid'], default='viewer-and')
    parser.add_argument('--runtime', type=Path)
    args = parser.parse_args()
    catalog_path = args.root / 'analysis/brain-improvement/source-catalog.json'
    suite_path = Path(__file__).with_name('eval-cases.json')
    catalog = json.loads(catalog_path.read_text(encoding='utf-8'))
    suite = json.loads(suite_path.read_text(encoding='utf-8'))
    report = run(args.root, catalog, suite, args.method, args.runtime)
    report['catalog_sha256'] = digest(catalog_path)
    report['suite_sha256'] = digest(suite_path)
    report['implementation_sha256'] = digest(Path(__file__))
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(report, ensure_ascii=False, indent=2), encoding='utf-8')
    print(json.dumps({'cases': len(report['cases']), 'aggregate': report['aggregate'],
                      'latency_ms': report['latency_ms']}))


if __name__ == '__main__':
    main()
