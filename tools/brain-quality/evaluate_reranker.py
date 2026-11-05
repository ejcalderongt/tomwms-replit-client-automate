"""Evaluate optional second-stage ranking against the frozen incident suite.

#EJC20261027_BRAIN_RERANK_EVAL
"""
import argparse
import json
import statistics
import time
from pathlib import Path
from audit import CONFIG, digest
from evaluate import load_documents, retrieval_metrics
from retrieve import BM25
from rerank import CrossEncoderReranker, RerankedIndex


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--root', type=Path, default=CONFIG.parents[2])
    parser.add_argument('--runtime', type=Path, required=True)
    parser.add_argument('--semantic-runtime', type=Path)
    parser.add_argument('--output', type=Path, required=True)
    args = parser.parse_args()
    catalog_path = args.root / 'analysis/brain-improvement/source-catalog.json'
    suite_path = Path(__file__).with_name('eval-cases.json')
    catalog = json.loads(catalog_path.read_text(encoding='utf-8'))
    suite = json.loads(suite_path.read_text(encoding='utf-8'))
    for case in suite['cases']:
        for source in case['oracle_sources']:
            if digest(args.root / source['path']) != source['sha256']:
                raise ValueError('Oracle source changed: ' + case['id'])
    start = time.perf_counter()
    documents = load_documents(args.root, catalog)
    retriever = BM25(documents)
    semantic = None
    method = 'bm25-rerank'
    if args.semantic_runtime:
        from semantic import SemanticIndex, HybridIndex
        semantic = SemanticIndex(documents, args.semantic_runtime)
        retriever = HybridIndex(retriever, semantic)
        method = 'hybrid-rerank'
    reranker = CrossEncoderReranker(documents, args.runtime)
    index = RerankedIndex(retriever, reranker)
    preparation_ms = (time.perf_counter() - start) * 1000
    results = []
    for case in suite['cases']:
        start = time.perf_counter()
        found = index.search(case['query'])
        elapsed = (time.perf_counter() - start) * 1000
        results.append({'case_id': case['id'], 'split': case['split'], 'results': found,
                        'retrieval': retrieval_metrics(case['expected_sources'], found),
                        'latency_ms': elapsed, 'answer_evaluation_status': 'not-run; no generated answer'})
        print(json.dumps({'completed_case': case['id'], 'latency_ms': elapsed}), flush=True)
    metrics = ('hit@5', 'mrr@5', 'evidence-recall@5')
    latencies = sorted(r['latency_ms'] for r in results)
    report = {'version': 1, 'method': method + '/document-adapter',
              'catalog_sha256': digest(catalog_path), 'suite_sha256': digest(suite_path),
              'implementation_sha256': digest(Path(__file__)),
              'reranker': {'identity': reranker.identity, 'cache_hits': reranker.cache_hits,
                           'cache_misses': reranker.cache_misses},
              'semantic': semantic.identity if semantic else None,
              'preparation_ms': preparation_ms,
              'aggregate': {m: statistics.mean(r['retrieval'][m] for r in results) for m in metrics},
              'by_split': {split: {'cases': sum(r['split'] == split for r in results),
                                  **{m: statistics.mean(r['retrieval'][m] for r in results if r['split'] == split)
                                     for m in metrics}} for split in sorted({r['split'] for r in results})},
              'latency_ms': {'median': statistics.median(latencies),
                             'p95': latencies[max(0, int(len(latencies) * .95 + .999) - 1)]},
              'cost': {'external_model_calls': 0, 'model_tokens': None, 'monetary_total': None,
                       'reason': 'Local model inference; hardware costs and token totals not measured'},
              'limitations': ['No LLM answers; retrieval scoring does not prove diagnosis quality',
                              'Literal candidate passages selected by lexical coverage; long-document coverage is limited'],
              'cases': results}
    reranker.close()
    if semantic:
        semantic.close()
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(report, ensure_ascii=False, indent=2), encoding='utf-8')
    print(json.dumps({'method': method, 'aggregate': report['aggregate'], 'latency_ms': report['latency_ms']}))


if __name__ == '__main__':
    main()
