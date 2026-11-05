import unittest
from retrieve import BM25, tokenize


class RetrievalTests(unittest.TestCase):
    def fixture(self):
        return BM25([
            {'id': 'one', 'path': 'a', 'haystack': 'stock stock cliente_bodega relación activa',
             'metadata': {'clients': ['MHS'], 'branches': ['dev_2028_merge']}},
            {'id': 'two', 'path': 'b', 'haystack': 'stock reserva picking',
             'metadata': {'clients': ['MAMPA'], 'branches': ['stable']}},
            {'id': 'unknown', 'path': 'c', 'haystack': 'cliente_bodega', 'metadata': {}}])

    def test_identifier_and_accent_tokens(self):
        self.assertIn('cliente_bodega', tokenize('¿Qué relación tiene cliente_bodega?'))
        self.assertIn('recepcion', tokenize('Recepción'))

    def test_retrieval_and_strict_scope(self):
        index = self.fixture()
        self.assertEqual(index.search('relación cliente_bodega')[0]['id'], 'one')
        self.assertEqual(index.search('cliente_bodega', client='MHS')[0]['id'], 'one')
        self.assertEqual(index.search('cliente_bodega', client='Killios'), [])
        self.assertEqual(index.search('stock', client='MHS', branch='stable'), [])
        self.assertEqual(index.search('stock', client='mhs', branch='dev_2028_merge')[0]['id'], 'one')


if __name__ == '__main__':
    unittest.main()
