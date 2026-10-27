import tempfile
import unittest
from pathlib import Path
from resolve import matches, resolve


class ResolveTests(unittest.TestCase):
    def test_boundaries_accents(self):
        self.assertTrue(matches('Recepción HH', 'recepcion'))
        self.assertFalse(matches('respuesta espacial', 'SP'))
        self.assertTrue(matches('revisar SP de reserva', 'SP'))

    def test_union_missing_targets_conflicts_and_client(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            (root / 'old.yml').write_text('domains:\n- archivo: brain/agents/domain-mampa.yml\n  triggers:\n    keywords: [MAMPA]\nclients:\n  archivos:\n  - slug: mampa\n    archivo: brain/client-index/mampa.yml\n')
            (root / 'new.yml').write_text('domains:\n  bof:\n    file: brain/agents/domain-bof.yml\n    triggers: [MAMPA]\n    also_load: [brain/agents/missing.yml]\n')
            def entry(name, paths, conflict=False):
                return {'id': name, 'sources': [{'path': p} for p in paths],
                        'resolution': 'unresolved' if conflict else 'unique',
                        'retrieval_policy': 'return-all-with-conflict' if conflict else 'selected',
                        'selected': None if conflict else paths[0],
                        'knowledge_status': 'legacy-unvalidated'}
            catalog = {'entries': [entry('agents/_index.yml', ['old.yml', 'new.yml'], True),
                                   entry('agents/coordinator.yml', ['coord.yml']),
                                   entry('agents/domain-mampa.yml', ['mampa.yml']),
                                   entry('agents/domain-bof.yml', ['bof-a.yml', 'bof-b.yml'], True),
                                   entry('client-index/mampa.yml', ['client.yml'])]}
            result = resolve(root, catalog, 'MAMPA interface')
            found = {r['id']: r for r in result['results']}
            self.assertIn('agents/domain-mampa.yml', found)
            self.assertIn('client-index/mampa.yml', found)
            self.assertEqual(len(found['agents/domain-bof.yml']['sources']), 2)
            self.assertEqual(result['issues'], [{'path': 'agents/missing.yml', 'issue': 'missing-target'}])


if __name__ == '__main__':
    unittest.main()
