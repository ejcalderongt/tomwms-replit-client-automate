import hashlib
import json
import subprocess
import tempfile
import unittest
from pathlib import Path

DLL = Path(__file__).with_name('ast-exporter') / 'bin/Debug/net10.0/AstExporter.dll'


@unittest.skipUnless(DLL.is_file(), 'Build AstExporter.csproj before parser integration tests')
class AstExporterTests(unittest.TestCase):
    def test_compile_membership_and_real_syntax_not_comments_or_strings(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            source = root / 'C.vb'
            source.write_text('''Public Class C
  Public Shared Function A() As Integer
    ' B() is a comment, not an invocation
    Dim label As String = "B()"
    Return B()
  End Function
  Private Shared Function B() As Integer
    Return 1
  End Function
End Class
''', encoding='utf-8-sig')
            (root / 'orphan.vb').write_text('Public Class Orphan\nEnd Class', encoding='utf-8')
            (root / 'sample.vbproj').write_text('<Project><ItemGroup><Compile Include="C.vb" /></ItemGroup></Project>')
            output = root / 'facts.json'
            subprocess.run(['dotnet', str(DLL.resolve()), str(root), str(output), 'sample.vbproj'],
                           check=True, capture_output=True, timeout=30)
            facts = json.loads(output.read_text(encoding='utf-8'))
            self.assertEqual([f['path'] for f in facts['files']], ['C.vb'])
            self.assertEqual(facts['files'][0]['sha256'], hashlib.sha256(source.read_bytes()).hexdigest())
            self.assertEqual(facts['files'][0]['syntax_errors'], 0)
            method = next(m for m in facts['methods'] if m['name'] == 'A')
            self.assertEqual([c['name'] for c in method['calls']], ['B'])
            self.assertEqual(method['calls'][0]['line'], 5)
            self.assertEqual(method['type'], 'C')


if __name__ == '__main__':
    unittest.main()
