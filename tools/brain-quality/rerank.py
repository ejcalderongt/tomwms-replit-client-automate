"""Optional CPU multilingual cross-encoder; adoption requires evaluation.

#EJC20261027_BRAIN_RERANK
"""
import hashlib
import json
import math
import sqlite3
from retrieve import tokenize
from semantic import fingerprint, windows

MODEL = 'BAAI/bge-reranker-v2-m3-int8'


def candidate_passage(text, query, size=1800):
    """Bound candidate text and preserve the exact source span (no summary)."""
    terms = set(tokenize(query))
    best, score = '', -1
    for passage in windows(text, size, 200):
        overlap = len(terms.intersection(tokenize(passage)))
        if overlap > score:
            best, score = passage, overlap
    return best


class CrossEncoderReranker:
    def __init__(self, documents, runtime):
        from fastembed.rerank.cross_encoder import TextCrossEncoder
        runtime.mkdir(parents=True, exist_ok=True)
        self.model = TextCrossEncoder(model_name=MODEL, cache_dir=str(runtime / 'models'),
                                      threads=4, providers=['CPUExecutionProvider'], cuda=False)
        self.identity = {'model': MODEL, 'artifact_sha256': fingerprint(runtime / 'models'),
                         'passage_version': 'literal-char1800-overlap200-max-lexical-coverage-v1'}
        self.documents = {d['path']: d for d in documents}
        self.connection = sqlite3.connect(runtime / 'scores.sqlite3')
        self.connection.execute('CREATE TABLE IF NOT EXISTS scores (key TEXT PRIMARY KEY, value REAL NOT NULL)')
        self.cache_hits = self.cache_misses = 0

    def rerank(self, query, candidates, top_k=5):
        pending, rows = [], []
        for candidate in candidates:
            passage = candidate_passage(self.documents[candidate['path']]['haystack'], query)
            key = hashlib.sha256((json.dumps(self.identity, sort_keys=True) + query + '\n' + passage).encode()).hexdigest()
            cached = self.connection.execute('SELECT value FROM scores WHERE key=?', (key,)).fetchone()
            row = {**candidate, 'retrieval_score': candidate['score']}
            rows.append(row)
            if cached is not None:
                row['score'] = cached[0]
                self.cache_hits += 1
            else:
                pending.append((row, key, passage))
        if pending:
            scores = list(self.model.rerank(query, [p[2] for p in pending], batch_size=4))
            if len(scores) != len(pending) or any(not math.isfinite(float(s)) for s in scores):
                raise ValueError('Invalid reranker score batch')
            with self.connection:
                for (row, key, _), score in zip(pending, scores):
                    row['score'] = float(score)
                    self.connection.execute('INSERT OR REPLACE INTO scores VALUES (?, ?)', (key, float(score)))
                    self.cache_misses += 1
        return sorted(rows, key=lambda row: (-row['score'], row['id']))[:top_k]

    def close(self):
        self.connection.close()


class RerankedIndex:
    def __init__(self, retrieval, reranker):
        self.retrieval, self.reranker = retrieval, reranker

    def search(self, query, top_k=5, client=None, branch=None):
        candidates = self.retrieval.search(query, 20, client, branch)
        return self.reranker.rerank(query, candidates, top_k)
