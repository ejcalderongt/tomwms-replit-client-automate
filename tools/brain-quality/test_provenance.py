import hashlib
import json
import tempfile
import unittest
from pathlib import Path
from provenance import validate_claim, CONFIG


class ProvenanceTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name)
        path = self.root / 'source.md'
        path.write_text('Evidence', encoding='utf-8')
        self.config = json.loads(CONFIG.read_text(encoding='utf-8'))
        self.claim = {'id': 'case', 'kind': 'hypothesis', 'statement': 'Requires verification',
                      'clients': ['MAMPA'], 'branches': ['dev_2028_merge'],
                      'validated_at': None, 'status': 'draft',
                      'sources': [{'path': 'source.md', 'commit': None,
                                   'sha256': hashlib.sha256(path.read_bytes()).hexdigest()}]}

    def test_draft_is_valid_without_invented_commit(self):
        self.assertEqual(validate_claim(self.root, self.claim, self.config), [])

    def test_changed_evidence_fails(self):
        (self.root / 'source.md').write_text('Changed', encoding='utf-8')
        self.assertIn('source:0:hash-mismatch', validate_claim(self.root, self.claim, self.config))

    def test_unjustified_promotion_and_missing_scope_fail(self):
        self.claim.update(status='verified', clients=[])
        issues = validate_claim(self.root, self.claim, self.config)
        self.assertIn('explicit-scope-required:clients', issues)
        self.assertIn('verified-requires-validation-date', issues)
        self.assertIn('hypothesis-cannot-be-verified-fact', issues)
        self.assertIn('verified-requires-validation-evidence', issues)
        self.assertIn('source:0:verified-source-must-be-committed', issues)

    def test_traversal_rejected(self):
        self.claim['sources'][0]['path'] = '../private.md'
        self.assertIn('source:0:path-outside-root', validate_claim(self.root, self.claim, self.config))

    def test_plausible_but_nonexistent_commit_rejected(self):
        self.claim.update(kind='fact', status='verified', validated_at='2026-10-27',
                          validation_evidence=['independent check'])
        self.claim['sources'][0].update(commit='a' * 40, content_state='committed')
        self.assertIn('source:0:commit-content-mismatch', validate_claim(self.root, self.claim, self.config))


if __name__ == '__main__':
    unittest.main()
