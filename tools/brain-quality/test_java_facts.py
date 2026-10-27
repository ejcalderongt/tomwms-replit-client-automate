import importlib.util
import tempfile
import unittest
from pathlib import Path
from java_facts import export


@unittest.skipUnless(importlib.util.find_spec('tree_sitter_java'), 'Install requirements-graph.txt')
class JavaFactsTests(unittest.TestCase):
    def test_literal_routes_exclude_comments_strings_and_dynamic_names(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            path = root / 'app/src/main/java/C.java'
            path.parent.mkdir(parents=True)
            path.write_text('''class C {
 void load(String dynamic) {
   // callMethod("FakeComment");
   String text = "callMethod(\\"FakeString\\")";
   callMethodJsonPost("ActualEndpoint", "pId", 1);
   callMethod(dynamic);
 }
}''', encoding='utf-8')
            facts = export(root, 'app/src/main/java')
            self.assertEqual(facts['files'][0]['syntax_errors'], 0)
            routes = facts['methods'][0]['ws_endpoints']
            self.assertEqual([r['name'] for r in routes], ['ActualEndpoint'])
            self.assertEqual(routes[0]['line'], 5)


if __name__ == '__main__':
    unittest.main()
