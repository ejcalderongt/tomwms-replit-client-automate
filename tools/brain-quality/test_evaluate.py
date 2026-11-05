import unittest
from evaluate import retrieval_metrics, viewer_baseline


class EvaluationTests(unittest.TestCase):
    def test_metrics_reward_rank_and_partial_evidence(self):
        values = retrieval_metrics(['a', 'b'], [{'id': 'noise'}, {'id': 'b'}])
        self.assertEqual(values, {'hit@5': 1, 'mrr@5': .5, 'evidence-recall@5': .5})

    def test_baseline_matches_all_terms_and_deduplicates(self):
        docs = [{'id': 'a', 'path': 'first', 'haystack': 'stock reserva'},
                {'id': 'a', 'path': 'copy', 'haystack': 'stock reserva'},
                {'id': 'b', 'path': 'partial', 'haystack': 'stock'}]
        self.assertEqual(viewer_baseline(docs, 'stock reserva'), [{'id': 'a', 'path': 'first'}])
        self.assertEqual(viewer_baseline(docs, 'stock reserva missing'), [])


if __name__ == '__main__':
    unittest.main()
