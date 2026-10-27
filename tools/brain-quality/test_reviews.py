import unittest
from reviews import apply_reviews


class ReviewTests(unittest.TestCase):
    def fixture(self):
        return {'summary': {}, 'entries': [{'id': 'rule', 'resolution': 'unresolved',
                'sources': [{'path': 'a', 'sha256': 'one'}, {'path': 'b', 'sha256': 'two'}]}]}

    def test_review_keeps_both_sources_and_disputed_status(self):
        review = {'id': 'rule', 'action': 'retain-alternatives',
                  'source_hashes': {'a': 'one', 'b': 'two'}, 'reason': 'Different scope'}
        result = apply_reviews(self.fixture(), [review])
        entry = result['entries'][0]
        self.assertEqual(entry['knowledge_status'], 'disputed')
        self.assertIsNone(entry['selected'])
        self.assertEqual(len(entry['sources']), 2)
        self.assertEqual(result['summary']['reviewed-alternatives'], 1)

    def test_changed_source_invalidates_review(self):
        review = {'id': 'rule', 'action': 'retain-alternatives',
                  'source_hashes': {'a': 'old', 'b': 'two'}}
        result = apply_reviews(self.fixture(), [review])
        self.assertEqual(result['entries'][0]['resolution'], 'unresolved')
        self.assertEqual(result['entries'][0]['review_issue'], 'source-changed-since-review')

    def test_selected_source_preserves_alternatives_without_validating_business(self):
        review = {'id': 'rule', 'action': 'prefer-reviewed-source',
                  'selected_path': 'b', 'source_hashes': {'a': 'one', 'b': 'two'},
                  'reason': 'Runtime integration verified'}
        entry = apply_reviews(self.fixture(), [review])['entries'][0]
        self.assertEqual(entry['resolution'], 'reviewed-selected')
        self.assertEqual(entry['selected'], 'b')
        self.assertEqual(entry['knowledge_status'], 'legacy-unvalidated')
        self.assertEqual(len(entry['sources']), 2)


if __name__ == '__main__':
    unittest.main()
