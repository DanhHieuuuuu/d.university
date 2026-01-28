from typing import List
import numpy as np
from sentence_transformers import SentenceTransformer


class EmbeddingService:
    """Service tạo embedding vector cho văn bản tiếng Việt."""
    
    def __init__(self, model_name: str = "sentence-transformers/paraphrase-multilingual-MiniLM-L12-v2"):
        """Khởi tạo embedding service với model đa ngôn ngữ."""
        print(f"Đang tải model embedding: {model_name}...")
        self.model = SentenceTransformer(model_name)
        self.embedding_dim = self.model.get_sentence_embedding_dimension()
        print(f"Đã tải model. Dimension: {self.embedding_dim}")
    
    def embed_text(self, text: str) -> np.ndarray:
        """Tạo embedding vector cho một đoạn văn bản."""
        return self.model.encode(text, convert_to_numpy=True)
    
    def embed_texts(self, texts: List[str]) -> np.ndarray:
        """Tạo embedding vector cho nhiều đoạn văn bản."""
        return self.model.encode(texts, convert_to_numpy=True, show_progress_bar=True)
    
    def compute_similarity(self, query_embedding: np.ndarray, doc_embeddings: np.ndarray) -> np.ndarray:
        """Tính độ tương đồng cosine giữa query và các document."""
        query_norm = query_embedding / np.linalg.norm(query_embedding)
        doc_norms = doc_embeddings / np.linalg.norm(doc_embeddings, axis=1, keepdims=True)
        similarities = np.dot(doc_norms, query_norm)
        return similarities
