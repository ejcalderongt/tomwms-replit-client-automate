"""Measure uncached inference and warm-cache latency with both CPU and wall time.

No existing cache entries are deleted. #EJC20261027_BRAIN_TIMING
"""
import argparse
import json
import time
import uuid
from pathlib import Path
from audit import CONFIG, digest
from evaluate import load_documents
from retrieve import BM25
from rerank import CrossEncoderReranker, RerankedIndex


def measure(action):
    wall, cpu = time.perf_counter(), time.process_time()
    result = action()
    wall_s, cpu_s = time.perf_counter() - wall, time.process_time() - cpu
    return result, {'wall_ms': wall_s * 1000, 'process_cpu_ms': cpu_s * 1000,
                    'elapsed_outlier': wall_s > max(10, cpu_s * 10),
                    'timing_note': 'Process CPU includes native inference threads; wall time includes scheduling and host interruptions.'}


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--root', type=Path, default=CONFIG.parents[2])
    parser.add_argument('--runtime', type=Path, required=True)
    parser.add_argument('--output', type=Path, required=True)
    args = parser.parse_args()
    catalog_path = args.root / 'analysis/brain-improvement/source-catalog.json'
    suite_path = Path(__file__).with_name('eval-cases.json')
    catalog = json.loads(catalog_path.read_text(encoding='utf-8'))
    suite = json.loads(suite_path.read_text(encoding='utf-8'))
    documents = load_documents(args.root, catalog)
    lexical = BM25(documents)
    reranker, preparation = measure(lambda: CrossEncoderReranker(documents, args.runtime))
    # A fresh namespace measures real inference without destroying the shared cache.
    reranker.identity['profiling_namespace'] = str(uuid.uuid4())
    index = RerankedIndex(lexical, reranker)
    selected = ['WMS-EVAL-01', 'WMS-EVAL-11', 'WMS-EVAL-21']
    cases = {c['id']: c for c in suite['cases']}
    rows = []
    try:
        for name in selected:
            case = cases[name]
            if case['split'] != 'development':
                raise ValueError('Timing sample must not change holdout use')
            _, lexical_time = measure(lambda: lexical.search(case['query']))
            for temperature in ['uncached', 'warm-cache']:
                hits, misses = reranker.cache_hits, reranker.cache_misses
                found, timing = measure(lambda: index.search(case['query']))
                row = {'case_id': name, 'mode': temperature, 'lexical': lexical_time,
                       'reranking': timing, 'cache_hits': reranker.cache_hits - hits,
                       'cache_misses': reranker.cache_misses - misses,
                       'result_ids': [r['id'] for r in found]}
                rows.append(row)
                print(json.dumps(row), flush=True)
    finally:
        reranker.close()
    report = {'model': reranker.identity, 'model_preparation': preparation,
              'catalog_sha256': digest(catalog_path), 'suite_sha256': digest(suite_path),
              'sample_policy': 'Three fixed development queries; timing only, no retrieval tuning',
              'hardware_cost': None, 'external_model_calls': 0, 'rows': rows}
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(report, indent=2), encoding='utf-8')


if __name__ == '__main__':
    main()
