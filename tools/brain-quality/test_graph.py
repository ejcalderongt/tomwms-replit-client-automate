import copy
import json
import subprocess
import tempfile
import unittest
from pathlib import Path
from audit import digest
from graph import build, neighbors


class GraphTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name)
        subprocess.run(['git', 'init', '-q', str(self.root)], check=True)
        subprocess.run(['git', '-C', str(self.root), '-c', 'user.name=Fixture',
                        '-c', 'user.email=fixture@example.invalid', 'commit', '--allow-empty', '-qm', 'fixture'],
                       check=True, capture_output=True)
        for name in ['A.vb', 'C.java', 'schema.sql']:
            (self.root / name).write_text('fixture', encoding='utf-8')
        def method(name, owner, path, key, **extra):
            return {'id': key, 'name': name, 'type': owner, 'path': path,
                    'sha256': digest(self.root / path), 'line_start': 1, 'line_end': 2,
                    'syntax_errors': 0, 'calls': [], 'sql_references': [], **extra}
        self.vb = {'repo_root': str(self.root), 'files': [{'path': 'A.vb', 'sha256': digest(self.root / 'A.vb')}],
                   'methods': [method('Endpoint', 'WS', 'A.vb', 'a', web_method=True,
                                      calls=[{'name': 'Get', 'qualifier': 'DAL', 'line': 1}]),
                               method('Get', 'DAL', 'A.vb', 'b'), method('Get', 'DAL', 'A.vb', 'c')]}
        self.java = {'repo_root': str(self.root), 'files': [{'path': 'C.java', 'sha256': digest(self.root / 'C.java')}],
                     'methods': [method('load', 'Screen', 'C.java', 'j', ws_endpoints=[{'name': 'Endpoint', 'line': 1}])]}
        self.sql = {'repo_root': str(self.root), 'files': [{'path': 'schema.sql', 'sha256': digest(self.root / 'schema.sql'),
                                                          'syntax_errors': [], 'definitions': [], 'references': []}]}

    def test_routes_overloads_and_reverse_impact(self):
        graph = build(self.vb, self.java, self.sql)
        route = next(e for e in graph['edges'] if e['relation'] == 'literal-ws-route')
        self.assertEqual((route['source'], route['target']), ('java:j', 'vb:a'))
        ambiguous = [e for e in graph['edges'] if e['relation'] == 'possible-call']
        self.assertEqual(len(ambiguous), 2)
        self.assertTrue(all(e['confidence'] == 'ambiguous-overload-or-type' for e in ambiguous))
        impact = neighbors(graph, 'vb:b', 'impact', 2)
        self.assertEqual({n['id'] for n in impact['nodes']}, {'vb:b', 'vb:a', 'java:j'})
        self.vb['methods'][0]['web_method'] = False
        self.assertFalse(any(e['relation'] == 'literal-ws-route' for e in build(self.vb, self.java, self.sql)['edges']))

    def test_changed_source_rejects_graph_generation(self):
        graph = build(self.vb, self.java, self.sql)
        (self.root / 'C.java').write_text('changed', encoding='utf-8')
        with self.assertRaisesRegex(ValueError, 'Stale'):
            build(self.vb, self.java, self.sql)
        with self.assertRaisesRegex(ValueError, 'Stale'):
            neighbors(graph, 'vb:b', 'impact', 2)


if __name__ == '__main__':
    unittest.main()
