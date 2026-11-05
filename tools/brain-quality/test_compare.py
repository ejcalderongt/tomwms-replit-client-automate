import copy
import unittest
from compare import compare


class ComparisonTests(unittest.TestCase):
    def fixture(self):
        return {'method': 'baseline', 'catalog_sha256': 'corpus', 'suite_sha256': 'suite',
                'cases': [{'case_id': name, 'split': split,
                           'retrieval': {'hit@5': .5, 'mrr@5': .5, 'evidence-recall@5': .5}}
                          for name, split in [('a', 'development'), ('b', 'holdout')]]}

    def test_incomparable_corpus_and_case_sets_fail(self):
        baseline = self.fixture()
        candidate = copy.deepcopy(baseline)
        candidate['catalog_sha256'] = 'different'
        with self.assertRaises(ValueError):
            compare(baseline, candidate)
        candidate = copy.deepcopy(baseline)
        candidate['cases'].pop()
        with self.assertRaises(ValueError):
            compare(baseline, candidate)

    def test_holdout_regression_blocks_retrieval_adoption(self):
        baseline = self.fixture()
        candidate = copy.deepcopy(baseline)
        candidate['cases'][0]['retrieval'].update({'hit@5': 1, 'mrr@5': 1, 'evidence-recall@5': 1})
        candidate['cases'][1]['retrieval']['mrr@5'] = .4
        self.assertFalse(compare(baseline, candidate)['retrieval_candidate_supported'])
        candidate['cases'][1]['retrieval']['mrr@5'] = .5
        report = compare(baseline, candidate)
        self.assertTrue(report['retrieval_candidate_supported'])
        self.assertFalse(report['generation_quality_proven'])


if __name__ == '__main__':
    unittest.main()
