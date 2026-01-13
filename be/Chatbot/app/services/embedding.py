"""
Module embedding su dung sentence-transformers.
Su dung model da ngon ngu ho tro tieng Viet.
"""

from typing import List
import numpy as np
from sentence_transformers import SentenceTransformer

class EmbeddingService:
    """
    Service tao embedding vector cho van ban tieng Viet.
    Su dung model paraphrase-multilingual-MiniLM-L12-v2 ho tro tieng Viet tot.
    """
    
    def __init__(self, model_name: str = "sentence-transformers/paraphrase-multilingual-MiniLM-L12-v2"):
        """
        Khoi tao embedding service.
        
        Args:
            model_name: Ten model sentence-transformers. Mac dinh su dung
                       paraphrase-multilingual-MiniLM-L12-v2 ho tro tieng Viet.
        """
        print(f"Đang tải model embedding: {model_name}...")
        self.model = SentenceTransformer(model_name)
        self.embedding_dim = self.model.get_sentence_embedding_dimension()
        print(f"Đã tải model. Dimension: {self.embedding_dim}")
    
    def embed_text(self, text: str) -> np.ndarray:
        """
        Tao embedding vector cho mot doan van ban.
        
        Args:
            text: Van ban can tao embedding
            
        Returns:
            Numpy array chua embedding vector
        """
        return self.model.encode(text, convert_to_numpy=True)
    
    def embed_texts(self, texts: List[str]) -> np.ndarray:
        """
        Tao embedding vector cho nhieu doan van ban.
        
        Args:
            texts: Danh sach van ban can tao embedding
            
        Returns:
            Numpy array 2D chua cac embedding vector
        """
        return self.model.encode(texts, convert_to_numpy=True, show_progress_bar=True)
    
    def compute_similarity(self, query_embedding: np.ndarray, doc_embeddings: np.ndarray) -> np.ndarray:
        """
        Tinh do tuong dong cosine giua query va cac document.
        
        Args:
            query_embedding: Embedding cua cau hoi
            doc_embeddings: Embedding cua cac document
            
        Returns:
            Array chua diem tuong dong cua tung document
        """
        # Normalize vectors
        query_norm = query_embedding / np.linalg.norm(query_embedding)
        doc_norms = doc_embeddings / np.linalg.norm(doc_embeddings, axis=1, keepdims=True)
        
        # Cosine similarity
        similarities = np.dot(doc_norms, query_norm)
        return similarities
