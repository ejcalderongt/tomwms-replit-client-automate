import importlib.util
import sys
import tempfile
import unittest
from pathlib import Path
from unittest.mock import patch

SOURCE = Path(__file__).resolve().parents[2] / 'wms-brain/tools/wms-embeddings/wms_embeddings.py'
spec = importlib.util.spec_from_file_location('wms_embeddings_cache_test', SOURCE)
emb = importlib.util.module_from_spec(spec)
sys.modules[spec.name] = emb
spec.loader.exec_module(emb)


class EmbeddingCacheTests(unittest.TestCase):
    def chunks(self):
        return [emb.Chunk('same.md', 'md', title, title, [], 'wms', 'root', 'markdown', 1)
                for title in ['first chunk', 'second chunk']]

    def test_multichunk_reuse_and_model_invalidation(self):
        with tempfile.TemporaryDirectory() as directory:
            root = Path(directory)
            with patch.object(emb, 'load_chunks', return_value=self.chunks()), patch.object(
                    emb, 'openai_embeddings', side_effect=lambda base, model, texts, timeout: [[1., 2.] for _ in texts]) as provider:
                emb.index_chunks(root, 'http://localhost/v1', 'model-a', 1, True)
                self.assertEqual(provider.call_count, 1)
                provider.reset_mock()
                emb.index_chunks(root, 'http://localhost/v1', 'model-a', 1, True)
                provider.assert_not_called()
                state = emb.load_state(root)
                self.assertEqual(state['cache_hits'], 2)
                self.assertEqual(len(next(iter(state['files'].values()))['digests']), 2)
                emb.index_chunks(root, 'http://localhost/v1', 'model-b', 1, True)
                self.assertEqual(provider.call_count, 1)

    def test_partial_batch_and_dimension_mismatch_rejected(self):
        with tempfile.TemporaryDirectory() as directory:
            with patch.object(emb, 'load_chunks', return_value=self.chunks()), patch.object(
                    emb, 'openai_embeddings', return_value=[[1., 2.]]):
                with self.assertRaisesRegex(ValueError, 'incomplete batch'):
                    emb.index_chunks(Path(directory), 'http://localhost/v1', 'a', 1, True)
        with self.assertRaises(ValueError):
            emb.cosine_similarity([1., 2.], [1.])


if __name__ == '__main__':
    unittest.main()
