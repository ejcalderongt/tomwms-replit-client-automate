import unittest
from rerank import candidate_passage, RerankedIndex


class RerankerTests(unittest.TestCase):
    def test_passage_is_literal_evidence_and_finds_late_identifier(self):
        body = 'old irrelevant context ' * 200 + 'cliente_bodega missing active relation'
        passage = candidate_passage(body, 'cliente_bodega relación', size=250)
        self.assertIn('cliente_bodega', passage)
        self.assertIn(passage, body)
        self.assertLessEqual(len(passage), 250)

    def test_scope_passed_to_candidate_retriever(self):
        class Retriever:
            def search(self, query, k, client, branch):
                self.arguments = query, k, client, branch
                return []
        class Reranker:
            def rerank(self, query, candidates, k):
                return candidates
        retriever = Retriever()
        self.assertEqual(RerankedIndex(retriever, Reranker()).search('q', client='MHS', branch='dev'), [])
        self.assertEqual(retriever.arguments, ('q', 20, 'MHS', 'dev'))


if __name__ == '__main__':
    unittest.main()
