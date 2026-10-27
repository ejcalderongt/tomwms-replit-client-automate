"""Local BM25 candidate retrieval with explicit scope filtering.

#EJC20261027_BRAIN_RETRIEVAL
"""
import argparse
import collections
import json
import math
import re
from pathlib import Path
from audit import CONFIG
from resolve import fold

STOPWORDS = set('a al algo ante asi como con contra cual cuales cuando de del donde el ella en entre era es esa ese esta este esto fue ha hay la las le lo los mas me mi no nos o para pero por porque que quien se si sin sobre su sus un una uno unos unas y ya'.split())


def tokenize(text):
    return [t for t in re.findall(r'[\w]+', fold(text)) if t not in STOPWORDS and len(t) > 1]


class BM25:
    def __init__(self, documents, k1=1.5, b=.75):
        self.documents = documents
        self.k1, self.b = k1, b
        self.terms = [collections.Counter(tokenize(d['haystack'])) for d in documents]
        self.lengths = [sum(c.values()) for c in self.terms]
        self.average = sum(self.lengths) / max(1, len(self.lengths))
        self.postings = collections.defaultdict(list)
        for index, counts in enumerate(self.terms):
            for term, frequency in counts.items():
                self.postings[term].append((index, frequency))

    def search(self, query, top_k=5, client=None, branch=None):
        scores = collections.defaultdict(float)
        for term in set(tokenize(query)):
            posting = self.postings.get(term, [])
            idf = math.log(1 + (len(self.documents) - len(posting) + .5) / (len(posting) + .5))
            for index, frequency in posting:
                denominator = frequency + self.k1 * (1 - self.b + self.b * self.lengths[index] / (self.average or 1))
                scores[index] += idf * frequency * (self.k1 + 1) / denominator
        grouped = {}
        for index, score in sorted(scores.items(), key=lambda pair: (-pair[1], self.documents[pair[0]]['path'])):
            doc = self.documents[index]
            scope = doc.get('metadata', {})
            if client and fold(client) not in {fold(c) for c in scope.get('clients', [])}:
                continue
            if branch and fold(branch) not in {fold(b) for b in scope.get('branches', [])}:
                continue
            if doc['id'] not in grouped:
                grouped[doc['id']] = {'id': doc['id'], 'path': doc['path'], 'score': score,
                                      'metadata': scope, 'alternative_paths': [],
                                      'knowledge_status': doc.get('knowledge_status', 'unknown')}
            else:
                grouped[doc['id']]['alternative_paths'].append(doc['path'])
        return list(grouped.values())[:top_k]


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument('query')
    parser.add_argument('--client')
    parser.add_argument('--branch')
    parser.add_argument('--top-k', type=int, default=5)
    parser.add_argument('--root', type=Path, default=CONFIG.parents[2])
    args = parser.parse_args()
    from evaluate import load_documents
    catalog = json.loads((args.root / 'analysis/brain-improvement/source-catalog.json').read_text(encoding='utf-8'))
    index = BM25(load_documents(args.root, catalog))
    print(json.dumps({'method': 'bm25', 'scope_policy': 'strict-declared; unknown scope excluded when filtered',
                      'results': index.search(args.query, args.top_k, args.client, args.branch)},
                     ensure_ascii=True, indent=2))


if __name__ == '__main__':
    main()
