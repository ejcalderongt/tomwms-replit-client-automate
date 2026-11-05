"""Behavior checks for duplicate classification and sensitive-context exclusion."""
import subprocess
import tempfile
import unittest
from pathlib import Path
from audit import audit


class CorpusAuditTests(unittest.TestCase):
    def test_divergent_identical_unique_and_sensitive(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            subprocess.run(["git", "init", "-q", str(root)], check=True)
            subprocess.run(["git", "-C", str(root), "-c", "user.name=Fixture",
                            "-c", "user.email=fixture@example.invalid", "commit",
                            "--allow-empty", "-qm", "fixture"], check=True)
            for name, body in {"brain/same.md": "same", "copy/same.md": "same",
                               "brain/conflict.yml": "a", "copy/conflict.yml": "b",
                               "copy/unique.md": "unique",
                               "brain/agent-context/private.md": "SENSITIVE_SENTINEL"}.items():
                path = root / name
                path.parent.mkdir(parents=True, exist_ok=True)
                path.write_text(body, encoding="utf-8")
            report = audit(root, {"knowledge_roots": ["brain", "copy"]})
            self.assertEqual(report["summary"], {"documents": 5, "unique_relative_paths": 3,
                                                "identical_duplicates": 1, "divergent_duplicates": 1})
            self.assertNotIn("private.md", str(report))
            self.assertNotIn("SENSITIVE_SENTINEL", str(report))
            self.assertTrue(report["dirty"])


if __name__ == "__main__":
    unittest.main()
