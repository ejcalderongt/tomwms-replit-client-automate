import json
import subprocess
import tempfile
import unittest
from pathlib import Path

DLL = Path(__file__).with_name('ast-exporter') / 'bin/Debug/net10.0/AstExporter.dll'


@unittest.skipUnless(DLL.is_file(), 'Build AstExporter.csproj')
class SqlFactsTests(unittest.TestCase):
    def test_ddl_and_table_references_do_not_treat_comments_as_queries(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            (root / 'fixture.sql').write_text('''CREATE OR ALTER PROCEDURE dbo.P AS
BEGIN
 -- SELECT * FROM fake_comment;
 SELECT 'FROM fake_string' AS label FROM dbo.stock;
 INSERT INTO dbo.stock_res (id) VALUES (1);
END
GO
ALTER TABLE dbo.stock ADD Extra int NULL;
''', encoding='utf-8-sig')
            output = root / 'facts.json'
            subprocess.run(['dotnet', str(DLL.resolve()), '--sql', str(root), str(output), 'fixture.sql'],
                           check=True, capture_output=True, timeout=30)
            facts = json.loads(output.read_text(encoding='utf-8'))['files'][0]
            self.assertEqual(facts['syntax_errors'], [])
            self.assertEqual({(d['kind'], d['name']) for d in facts['definitions']},
                             {('procedure', 'dbo.P'), ('table-alter-target', 'dbo.stock')})
            self.assertEqual({r['name'] for r in facts['references']}, {'dbo.stock', 'dbo.stock_res'})
            self.assertTrue(any(r['relation'] == 'writes' and r['name'] == 'dbo.stock_res' for r in facts['references']))


if __name__ == '__main__':
    unittest.main()
