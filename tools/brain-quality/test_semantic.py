import unittest
from semantic import windows, fuse


class SemanticTests(unittest.TestCase):
    def test_windows_cover_all_characters_with_bounded_overlap(self):
        text = '0123456789abcdefghij'
        chunks = list(windows(text, 10, 2))
        self.assertEqual(chunks, ['0123456789', '89abcdefgh', 'ghij'])
        self.assertEqual(''.join([chunks[0]] + [c[2:] for c in chunks[1:]]), text)
        with self.assertRaises(ValueError):
            list(windows(text, 10, 10))

    def test_fusion_rewards_independent_agreement(self):
        lexical = [{'id': 'a', 'score': 5}, {'id': 'b', 'score': 3}]
        dense = [{'id': 'b', 'score': .8}, {'id': 'c', 'score': .7}]
        rows = fuse(lexical, dense)
        self.assertEqual(rows[0]['id'], 'b')
        self.assertEqual(set(rows[0]['channels']), {'lexical', 'semantic'})
        self.assertEqual(len(rows), 3)


if __name__ == '__main__':
    unittest.main()
