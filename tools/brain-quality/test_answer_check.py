import tempfile
import unittest
from pathlib import Path
from audit import digest
from answer_check import citation_issues, check_answer


class AnswerChecks(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name)
        source = self.root / 'source.md'
        source.write_text('stock_res is reserved quantity', encoding='utf-8')
        self.citation = {'path': 'source.md', 'sha256': digest(source), 'start': 0,
                         'end': 9, 'quote': 'stock_res'}

    def test_fabricated_quote_and_changed_source_fail(self):
        self.assertEqual(citation_issues(self.root, self.citation), [])
        self.citation['quote'] = 'invented'
        self.assertIn('quote-span-mismatch', citation_issues(self.root, self.citation))
        self.citation['sha256'] = 'wrong'
        self.assertIn('source-hash-mismatch', citation_issues(self.root, self.citation))

    def test_citation_integrity_does_not_invent_support_score(self):
        case = {'id': 'one', 'client': 'MHS', 'branch': None}
        answer = {'case_id': 'one', 'scope': {'client': 'MHS', 'branch': None},
                  'claims': [{'id': 'claim', 'kind': 'fact', 'citations': [self.citation]}]}
        report = check_answer(self.root, case, answer)
        self.assertEqual(report['metrics']['citation_integrity'], 1)
        self.assertIsNone(report['metrics']['unsupported_claim_rate'])
        self.assertIsNone(report['metrics']['diagnosis'])
        answer['scope']['client'] = 'Killios'
        self.assertIn('scope-mismatch:client', check_answer(self.root, case, answer)['issues'])

    def test_path_escape_fails(self):
        self.citation['path'] = '../private.md'
        self.assertIn('source-outside-root', citation_issues(self.root, self.citation))


if __name__ == '__main__':
    unittest.main()
