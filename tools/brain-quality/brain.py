"""Single local entry point for evidence retrieval. #EJC20261027_BRAIN_CLI"""
import argparse
import json
from pathlib import Path
from audit import CONFIG
from evaluate import load_documents
from retrieve import BM25


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('query')
    parser.add_argument('--client')
    parser.add_argument('--branch')
    parser.add_argument('--method', choices=['bm25', 'bm25-rerank', 'semantic', 'hybrid', 'hybrid-rerank'], default='bm25')
    parser.add_argument('--runtime', type=Path)
    parser.add_argument('--reranker-runtime', type=Path)
    parser.add_argument('--top-k', type=int, default=5)
    parser.add_argument('--root', type=Path, default=CONFIG.parents[2])
    args = parser.parse_args()
    if args.top_k < 1 or args.top_k > 30:
        parser.error('top-k must be between 1 and 30')
    if args.method in {'semantic', 'hybrid', 'hybrid-rerank'} and not args.runtime:
        parser.error('semantic methods require --runtime with local model cache')
    if args.method in {'bm25-rerank', 'hybrid-rerank'} and not args.reranker_runtime:
        parser.error('reranking requires --reranker-runtime')
    catalog = json.loads((args.root / 'analysis/brain-improvement/source-catalog.json').read_text(encoding='utf-8'))
    documents = load_documents(args.root, catalog)
    index = BM25(documents)
    semantic = reranker = None
    try:
        if args.method in {'semantic', 'hybrid', 'hybrid-rerank'}:
            from semantic import SemanticIndex, HybridIndex
            semantic = SemanticIndex(documents, args.runtime)
            index = semantic if args.method == 'semantic' else HybridIndex(index, semantic)
        if args.method in {'bm25-rerank', 'hybrid-rerank'}:
            from rerank import CrossEncoderReranker, RerankedIndex
            reranker = CrossEncoderReranker(documents, args.reranker_runtime)
            index = RerankedIndex(index, reranker)
        results = index.search(args.query, args.top_k, args.client, args.branch)
        by_id = {entry['id']: entry for entry in catalog['entries']}
        for result in results:
            entry = by_id[result['id']]
            result['source_evidence'] = entry['sources']
            result['reconciliation'] = entry['resolution']
        print(json.dumps({'query': args.query, 'method': args.method,
                          'experimental': args.method not in {'bm25', 'bm25-rerank'},
                          'scope_policy': 'strict-declared; missing metadata does not imply applicability',
                          'warning': 'Sources are evidence candidates. Validate current code/data before treating historical claims as current facts.',
                          'results': results}, ensure_ascii=True, indent=2))
    finally:
        if semantic:
            semantic.close()
        if reranker:
            reranker.close()


if __name__ == '__main__':
    main()
