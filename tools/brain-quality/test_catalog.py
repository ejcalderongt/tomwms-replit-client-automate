import tempfile
import unittest
from pathlib import Path
from catalog import build_catalog, normalized_body, additive_superset


class CatalogTests(unittest.TestCase):
    def test_additive_selection_rejects_rewrite_and_reordering(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            (root / 'a.md').write_text('A\nB\n', encoding='utf-8')
            (root / 'b.md').write_text('A\nnew evidence\nB\n', encoding='utf-8')
            copies = [{'path': 'a.md'}, {'path': 'b.md'}]
            self.assertEqual(additive_superset(root, copies)['path'], 'b.md')
            (root / 'b.md').write_text('B\nA\n', encoding='utf-8')
            self.assertIsNone(additive_superset(root, copies))
            (root / 'b.md').write_text('A\nC\n', encoding='utf-8')
            self.assertIsNone(additive_superset(root, copies))

    def test_normalize_only_frontmatter_and_whitespace(self):
        self.assertEqual(normalized_body('---\nid: x\n---\n# Rule\ntext  \n'), '# Rule\ntext')
        self.assertNotEqual(normalized_body('quantity: base'), normalized_body('quantity: presentation'))

    def test_conflicts_preserved_and_equivalence_not_validation(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            rows = []
            for path, body, sha in [('brain/a.md', '---\nid: a\n---\nRule', '1'),
                                    ('copy/a.md', 'Rule', '2'),
                                    ('brain/b.md', 'base', '3'), ('copy/b.md', 'presentation', '4')]:
                full = root / path
                full.parent.mkdir(parents=True, exist_ok=True)
                full.write_text(body, encoding='utf-8')
                rows.append({'relative': Path(path).name, 'path': path, 'sha256': sha})
            result = build_catalog(root, {'documents': rows, 'commit': 'fixture',
                                          'dirty': True, 'generated_at': 'fixture'})
            equivalent, conflict = result['entries']
            self.assertEqual(equivalent['resolution'], 'body-equivalent')
            self.assertEqual(equivalent['knowledge_status'], 'legacy-unvalidated')
            self.assertIsNone(conflict['selected'])
            self.assertEqual(len(conflict['sources']), 2)
            self.assertEqual(conflict['retrieval_policy'], 'return-all-with-conflict')


if __name__ == '__main__':
    unittest.main()
