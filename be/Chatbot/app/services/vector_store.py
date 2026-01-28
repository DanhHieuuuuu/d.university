import os
from typing import List, Dict, Any, Tuple
import numpy as np
import faiss

from app.services.embedding import EmbeddingService


class VectorStore:
    """
    Vector store su dung FAISS de luu tru va tim kiem embedding.
    """
    
    def __init__(self, embedding_service: EmbeddingService):
        """
        Khoi tao vector store.
        
        Args:
            embedding_service: Service de tao embedding
        """
        self.embedding_service = embedding_service
        self.index = None
        self.chunks = []
        self.embeddings = None
    
    def build_index(self, chunks: List[Dict[str, Any]]) -> None:
        """
        Xay dung FAISS index tu cac chunk du lieu.
        
        Args:
            chunks: Danh sach cac chunk du lieu
        """
        self.chunks = chunks
        
        # Tao embedding cho tat ca cac chunk
        texts = [chunk["content"] for chunk in chunks]
        print(f"Đang tạo embedding cho {len(texts)} chunks...")
        self.embeddings = self.embedding_service.embed_texts(texts)
        
        # Xay dung FAISS index (su dung L2 distance)
        dimension = self.embeddings.shape[1]
        self.index = faiss.IndexFlatIP(dimension)
        
        # Normalize embeddings truoc khi them vao index
        faiss.normalize_L2(self.embeddings)
        self.index.add(self.embeddings)
        
        print(f"Đã xây dựng index với {self.index.ntotal} vectors")
    
    def search(self, query: str, top_k: int = 5) -> List[Tuple[Dict[str, Any], float]]:
        """
        Tim kiem cac chunk tuong tu voi query.
        
        Args:
            query: Cau hoi can tim kiem
            top_k: So luong ket qua tra ve
            
        Returns:
            Danh sach tuple (chunk, score) sap xep theo do tuong dong giam dan
        """
        if self.index is None:
            raise ValueError("Index chưa được xây dựng. Gọi build_index() trước.")
        
        # Tao embedding cho query
        query_embedding = self.embedding_service.embed_text(query)
        query_embedding = query_embedding.reshape(1, -1).astype('float32')
        faiss.normalize_L2(query_embedding)
        
        # Tim kiem trong index
        scores, indices = self.index.search(query_embedding, top_k)
        
        # Tra ve ket qua
        results = []
        for i, idx in enumerate(indices[0]):
            if idx != -1:
                results.append((self.chunks[idx], float(scores[0][i])))
        
        return results
    
    def count(self) -> int:
        """Tra ve so luong vectors trong index."""
        return self.index.ntotal if self.index else 0
