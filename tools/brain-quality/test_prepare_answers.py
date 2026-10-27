import tempfile
import unittest
from pathlib import Path
from prepare_answers import prepare


class BlindingTests(unittest.TestCase):
    def test_only_retrieved_evidence_is_exposed_and_spans_are_exact(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            (root / 'retrieved.md').write_text('A source about stock_res.', encoding='utf-8')
            report = {'method': 'bm25', 'catalog_sha256': 'corpus', 'suite_sha256': 'suite',
                      'cases': [{'case_id': 'one', 'results': [{'id': 'retrieved', 'path': 'retrieved.md'}]}]}
            suite = {'cases': [{'id': 'one', 'query': 'stock_res', 'client': None, 'branch': None,
                                 'expected_sources': ['SECRET_ORACLE'], 'expected_concepts': ['SECRET_CONCEPT']} ]}
            packet = prepare(root, report, suite)
            self.assertNotIn('SECRET_ORACLE', str(packet))
            self.assertNotIn('SECRET_CONCEPT', str(packet))
            citation = packet['packets'][0]['retrieved_evidence'][0]
            text = (root / citation['path']).read_text(encoding='utf-8')
            self.assertEqual(citation['quote'], text[citation['start']:citation['end']])


if __name__ == '__main__':
    unittest.main()
