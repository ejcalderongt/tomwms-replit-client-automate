"""CPU-local multilingual embeddings with artifact-bound SQLite vector cache.

Only model artifacts are downloaded. Corpus text is processed locally and is
not stored in the cache. #EJC20261027_BRAIN_SEMANTIC
"""
import hashlib
import importlib.metadata
import json
import sqlite3
from pathlib import Path

MODEL = 'sentence-transformers/paraphrase-multilingual-MiniLM-L12-v2'


def windows(text, size=1000, overlap=150):
    if overlap < 0 or overlap >= size:
        raise ValueError('Invalid overlap')
    for start in range(0, max(1, len(text)), size - overlap):
        yield text[start:start + size]
        if start + size >= len(text):
            break


def fingerprint(directory):
    files = sorted(p for p in directory.rglob('*') if p.is_file() and p.suffix in {'.onnx', '.json', '.txt'}
                   and '.cache' not in p.relative_to(directory).parts)
    if not any(p.suffix == '.onnx' for p in files):
        raise ValueError('No local model artifact found')
    digest = hashlib.sha256()
    for path in files:
        digest.update(path.relative_to(directory).as_posix().encode())
        with path.open('rb') as stream:
            for part in iter(lambda: stream.read(1024 * 1024), b''):
                digest.update(part)
    return digest.hexdigest()


class SemanticIndex:
    def __init__(self, documents, runtime, model_name=MODEL):
        import numpy as np
        from fastembed import TextEmbedding
        self.np = np
        self.documents = documents
        runtime.mkdir(parents=True, exist_ok=True)
        self.model = TextEmbedding(model_name=model_name, cache_dir=str(runtime / 'models'), threads=4,
                                   providers=['CPUExecutionProvider'])
        self.identity = {'model': model_name, 'artifact_sha256': fingerprint(runtime / 'models'),
                         'fastembed': importlib.metadata.version('fastembed'), 'chunk_version': 'tokens112-overlap16-v1'}
        from tokenizers import Tokenizer
        tokenizers = sorted((runtime / 'models').rglob('tokenizer.json'))
        if len(tokenizers) != 1:
            raise ValueError('Expected one tokenizer in this model-specific runtime')
        tokenizer = Tokenizer.from_file(str(tokenizers[0]))
        tokenizer.no_truncation()
        tokenizer.no_padding()
        self.connection = sqlite3.connect(runtime / 'vectors.sqlite3')
        self.connection.execute('CREATE TABLE IF NOT EXISTS vectors (key TEXT PRIMARY KEY, dimension INTEGER NOT NULL, value BLOB NOT NULL)')
        passages = []
        self.owners = []
        for index, document in enumerate(documents):
            text = document['haystack']
            offsets = tokenizer.encode(text, add_special_tokens=False).offsets
            for start in range(0, len(offsets), 96):
                end = min(start + 112, len(offsets))
                chunk = text[offsets[start][0]:offsets[end - 1][1]]
                passages.append(chunk)
                self.owners.append(index)
                if end == len(offsets):
                    break
        self.cache_hits = self.cache_misses = 0
        vectors = self.embed(passages, 'passage')
        self.matrix = np.asarray(vectors, dtype=np.float32)
        if not len(self.matrix) or self.matrix.ndim != 2 or not np.isfinite(self.matrix).all():
            raise ValueError('Invalid corpus vectors')
        self.matrix /= np.maximum(np.linalg.norm(self.matrix, axis=1, keepdims=True), 1e-12)

    def embed(self, texts, role):
        np = self.np
        vectors = [None] * len(texts)
        missing = []
        for index, text in enumerate(texts):
            key = hashlib.sha256((json.dumps(self.identity, sort_keys=True) + role + '\n' + text).encode()).hexdigest()
            row = self.connection.execute('SELECT dimension, value FROM vectors WHERE key=?', (key,)).fetchone()
            if row:
                vector = np.frombuffer(row[1], dtype=np.float32).copy()
                if len(vector) != row[0] or not np.isfinite(vector).all():
                    raise ValueError('Corrupt embedding cache')
                vectors[index] = vector
                self.cache_hits += 1
            else:
                missing.append((index, key, text))
        for start in range(0, len(missing), 32):
            batch = missing[start:start + 32]
            texts_to_embed = [item[2] for item in batch]
            generated = list(self.model.query_embed(texts_to_embed) if role == 'query' else
                             self.model.passage_embed(texts_to_embed, batch_size=8))
            if len(generated) != len(batch):
                raise ValueError('Incomplete embedding batch')
            with self.connection:
                for (index, key, _), vector in zip(batch, generated):
                    vector = np.asarray(vector, dtype=np.float32)
                    if vector.ndim != 1 or len(vector) == 0 or not np.isfinite(vector).all() or np.linalg.norm(vector) == 0:
                        raise ValueError('Invalid embedding vector')
                    self.connection.execute('INSERT OR REPLACE INTO vectors VALUES (?, ?, ?)',
                                            (key, len(vector), vector.tobytes()))
                    vectors[index] = vector
                    self.cache_misses += 1
            if role == 'passage':
                print(json.dumps({'embedded': min(start + 32, len(missing)), 'pending_total': len(missing)}), flush=True)
        return vectors

    def search(self, query, top_k=5, client=None, branch=None):
        from resolve import fold
        np = self.np
        vector = self.embed([query], 'query')[0]
        if len(vector) != self.matrix.shape[1]:
            raise ValueError('Query vector dimension differs from index')
        vector = vector / max(float(np.linalg.norm(vector)), 1e-12)
        scores = self.matrix @ vector
        grouped = {}
        for chunk_index in np.argsort(-scores):
            doc = self.documents[self.owners[chunk_index]]
            scope = doc.get('metadata', {})
            if client and fold(client) not in {fold(c) for c in scope.get('clients', [])}:
                continue
            if branch and fold(branch) not in {fold(b) for b in scope.get('branches', [])}:
                continue
            if doc['id'] not in grouped:
                grouped[doc['id']] = {'id': doc['id'], 'path': doc['path'], 'score': float(scores[chunk_index]),
                                      'metadata': scope, 'knowledge_status': doc.get('knowledge_status', 'unknown')}
        return list(grouped.values())[:top_k]

    def close(self):
        self.connection.close()


def fuse(lexical, semantic, top_k=5, constant=60):
    """Reciprocal-rank fusion without query-specific tuning."""
    results = {}
    for channel, items in [('lexical', lexical), ('semantic', semantic)]:
        for rank, item in enumerate(items, 1):
            row = results.setdefault(item['id'], {**item, 'score': 0, 'channels': {}})
            row['score'] += 1 / (constant + rank)
            row['channels'][channel] = {'rank': rank, 'score': item['score']}
    return sorted(results.values(), key=lambda row: (-row['score'], row['id']))[:top_k]


class HybridIndex:
    def __init__(self, lexical, semantic):
        self.lexical, self.semantic = lexical, semantic

    def search(self, query, top_k=5, client=None, branch=None):
        return fuse(self.lexical.search(query, 30, client, branch),
                    self.semantic.search(query, 30, client, branch), top_k)
