import hashlib
import subprocess
import tempfile
import unittest
from pathlib import Path
from answer_check import citation_issues


class MultiRootEvidenceTests(unittest.TestCase):
    def test_historical_blob_and_explicit_root_allowlist(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            brain, repo = root / 'brain', root / 'repo'
            brain.mkdir()
            repo.mkdir()
            subprocess.run(['git', 'init', '-q', str(repo)], check=True)
            source = repo / 'source.md'
            source.write_text('old\n', encoding='utf-8-sig')
            raw = source.read_bytes()
            subprocess.run(['git', '-C', str(repo), '-c', 'core.autocrlf=false', 'add', 'source.md'], check=True)
            subprocess.run(['git', '-C', str(repo), '-c', 'user.name=Fixture', '-c', 'user.email=fixture@example.invalid',
                            'commit', '-qm', 'fixture'], check=True)
            commit = subprocess.check_output(['git', '-C', str(repo), 'rev-parse', 'HEAD'], text=True).strip()
            source.write_text('new\n', encoding='utf-8-sig')
            citation = {'repo_root': str(repo), 'path': 'source.md', 'git_ref': commit,
                        'sha256': hashlib.sha256(raw).hexdigest(), 'start': 0, 'end': 3, 'quote': 'old'}
            self.assertEqual(citation_issues(brain, citation, [repo]), [])
            self.assertIn('source-root-not-allowed', citation_issues(brain, citation))
            del citation['git_ref']
            self.assertIn('source-hash-mismatch', citation_issues(brain, citation, [repo]))


if __name__ == '__main__':
    unittest.main()
