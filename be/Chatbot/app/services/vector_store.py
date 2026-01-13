import os
import json
import pickle
from typing import List, Dict, Any, Tuple
import numpy as np
import faiss

from app.services.embedding import EmbeddingService
from app.services.chunking import load_and_chunk_data

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
        self.index = faiss.IndexFlatIP(dimension)  # Inner Product (cosine similarity sau khi normalize)
        
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
            if idx != -1:  # -1 nghia la khong tim thay
                results.append((self.chunks[idx], float(scores[0][i])))
        
        return results
    
    def save(self, directory: str) -> None:
        """
        Luu vector store ra file.
        
        Args:
            directory: Thu muc de luu
        """
        os.makedirs(directory, exist_ok=True)
        
        # Luu FAISS index
        faiss.write_index(self.index, os.path.join(directory, "index.faiss"))
        
        # Luu chunks va embeddings
        with open(os.path.join(directory, "chunks.json"), 'w', encoding='utf-8') as f:
            json.dump(self.chunks, f, ensure_ascii=False, indent=2)
        
        np.save(os.path.join(directory, "embeddings.npy"), self.embeddings)
        
        print(f"Đã lưu vector store tại: {directory}")
    
    def load(self, directory: str) -> bool:
        """
        Tai vector store tu file.
        
        Args:
            directory: Thu muc chua file
            
        Returns:
            True neu tai thanh cong, False neu khong tim thay file
        """
        index_path = os.path.join(directory, "index.faiss")
        chunks_path = os.path.join(directory, "chunks.json")
        embeddings_path = os.path.join(directory, "embeddings.npy")
        
        if not all(os.path.exists(p) for p in [index_path, chunks_path, embeddings_path]):
            return False
        
        self.index = faiss.read_index(index_path)
        
        with open(chunks_path, 'r', encoding='utf-8') as f:
            self.chunks = json.load(f)
        
        self.embeddings = np.load(embeddings_path)
        
        print(f"Đã tải vector store từ: {directory}")
        return True


def initialize_vector_store(
    data_path: str,
    vector_store_path: str,
    embedding_service: EmbeddingService,
    force_rebuild: bool = False
) -> VectorStore:
    """
    Khoi tao vector store tu du lieu JSON.
    Neu da co index thi tai lai, neu khong thi xay dung moi.
    
    Args:
        data_path: Duong dan den file JSON
        vector_store_path: Duong dan de luu/tai vector store
        embedding_service: Service de tao embedding
        force_rebuild: True de xay dung lai index du da ton tai
        
    Returns:
        VectorStore da duoc khoi tao
    """
    store = VectorStore(embedding_service)
    
    if not force_rebuild and store.load(vector_store_path):
        return store
    
    # Xay dung moi
    print(f"Đang xây dựng vector store từ: {data_path}")
    chunks = load_and_chunk_data(data_path)
    store.build_index(chunks)
    store.save(vector_store_path)
    
    return store
