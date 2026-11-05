import tempfile
import unittest
from pathlib import Path
from metadata import declared_metadata


class MetadataTests(unittest.TestCase):
    def read(self, body):
        with tempfile.TemporaryDirectory() as directory:
            path = Path(directory) / 'case.md'
            path.write_text(body, encoding='utf-8')
            return declared_metadata(path)

    def test_declared_scope_does_not_promote_validity(self):
        result = self.read('---\ncliente: MAMPA\nramas: [dev_2028_merge]\nestado: vigente\n---\nBody')
        self.assertEqual(result['clients'], ['MAMPA'])
        self.assertEqual(result['scope_state'], 'declared')
        self.assertIsNone(result['validated_at'])
        self.assertEqual(result['validation_state'], 'not-independently-validated')

    def test_no_inference_from_body_and_duplicate_keys_fail(self):
        self.assertEqual(self.read('MAMPA dev_2028_merge')['clients'], [])
        result = self.read('---\ncliente: MAMPA\ncliente: Killios\n---\nBody')
        self.assertEqual(result['clients'], [])
        self.assertEqual(result['issues'], ['invalid-or-duplicate-yaml'])


if __name__ == '__main__':
    unittest.main()
