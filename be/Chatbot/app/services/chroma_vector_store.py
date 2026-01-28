import os
from typing import List, Dict, Any, Tuple, Optional
import chromadb
from chromadb.config import Settings

from app.services.embedding import EmbeddingService


class ChromaVectorStore:
    """Vector store sử dụng ChromaDB để lưu trữ và tìm kiếm embedding."""
    
    def __init__(
        self, 
        embedding_service: EmbeddingService,
        persist_directory: str = "./data/chroma_db",
        collection_name: str = "student_data"
    ):
        """Khởi tạo ChromaDB vector store."""
        self.embedding_service = embedding_service
        self.persist_directory = persist_directory
        self.collection_name = collection_name
        
        os.makedirs(persist_directory, exist_ok=True)
        self.client = chromadb.PersistentClient(path=persist_directory)
        self.collection = self.client.get_or_create_collection(
            name=collection_name,
            metadata={"hnsw:space": "cosine"}
        )
        
        print(f"ChromaDB đã khởi tạo tại: {persist_directory}")
        print(f"Collection '{collection_name}' có {self.collection.count()} documents")
    
    def add_documents(self, chunks: List[Dict[str, Any]], batch_size: int = 100) -> None:
        """Thêm các chunk vào ChromaDB."""
        total_chunks = len(chunks)
        print(f"Đang thêm {total_chunks} chunks vào ChromaDB...")
        
        for i in range(0, total_chunks, batch_size):
            batch = chunks[i:i + batch_size]
            texts = [chunk["content"] for chunk in batch]
            embeddings = self.embedding_service.embed_texts(texts).tolist()
            ids = [f"chunk_{i + j}" for j in range(len(batch))]
            
            metadatas = []
            for chunk in batch:
                metadata = {
                    "type": chunk.get("type", "unknown"),
                    "source": chunk.get("source", ""),
                }
                if "hoc_ky" in chunk:
                    metadata["hoc_ky"] = str(chunk["hoc_ky"])
                if "ma_mon" in chunk:
                    metadata["ma_mon"] = chunk["ma_mon"]
                if "loai" in chunk:
                    metadata["loai"] = chunk["loai"]
                metadatas.append(metadata)
            
            self.collection.add(ids=ids, embeddings=embeddings, documents=texts, metadatas=metadatas)
            print(f"  Đã thêm {min(i + batch_size, total_chunks)}/{total_chunks} chunks...")
        
        print(f"Hoàn thành! Collection có {self.collection.count()} documents")
    
    def search(
        self, 
        query: str, 
        top_k: int = 5,
        where: Optional[Dict] = None,
        where_document: Optional[Dict] = None
    ) -> List[Tuple[Dict[str, Any], float]]:
        """Tìm kiếm các chunk tương tự với query."""
        query_embedding = self.embedding_service.embed_text(query).tolist()
        
        results = self.collection.query(
            query_embeddings=[query_embedding],
            n_results=top_k,
            where=where,
            where_document=where_document,
            include=["documents", "metadatas", "distances"]
        )
        
        output = []
        if results and results["documents"] and len(results["documents"]) > 0:
            for i, doc in enumerate(results["documents"][0]):
                chunk = {
                    "content": doc,
                    "metadata": results["metadatas"][0][i] if results["metadatas"] else {}
                }
                distance = results["distances"][0][i] if results["distances"] else 0
                similarity = 1 - distance
                output.append((chunk, similarity))
        
        return output
    
    def delete_all(self) -> None:
        """Xóa tất cả documents trong collection."""
        self.client.delete_collection(self.collection_name)
        self.collection = self.client.get_or_create_collection(
            name=self.collection_name,
            metadata={"hnsw:space": "cosine"}
        )
        print(f"Đã xóa tất cả documents trong collection '{self.collection_name}'")
    
    def delete_by_ids(self, ids: List[str]) -> None:
        """Xóa documents theo danh sách ID."""
        self.collection.delete(ids=ids)
        print(f"Đã xóa {len(ids)} documents")
    
    def update_document(self, doc_id: str, content: str, metadata: Optional[Dict] = None) -> None:
        """Cập nhật một document theo ID."""
        embedding = self.embedding_service.embed_text(content).tolist()
        update_kwargs = {"ids": [doc_id], "documents": [content], "embeddings": [embedding]}
        if metadata:
            update_kwargs["metadatas"] = [metadata]
        self.collection.update(**update_kwargs)
        print(f"Đã cập nhật document '{doc_id}'")
    
    def count(self) -> int:
        """Trả về số lượng documents trong collection."""
        return self.collection.count()
    
    def get_all_ids(self) -> List[str]:
        """Lấy tất cả IDs trong collection."""
        result = self.collection.get(include=[])
        return result["ids"] if result else []


def initialize_chroma_vector_store(
    persist_directory: str,
    embedding_service: EmbeddingService,
    collection_name: str = "student_data",
    force_rebuild: bool = False
) -> ChromaVectorStore:
    """Khởi tạo ChromaDB vector store."""
    store = ChromaVectorStore(
        embedding_service=embedding_service,
        persist_directory=persist_directory,
        collection_name=collection_name
    )
    
    if store.count() > 0 and not force_rebuild:
        print(f"Sử dụng ChromaDB có sẵn với {store.count()} documents")
        return store
    
    if force_rebuild and store.count() > 0:
        store.delete_all()
    
    print(f"ChromaDB sẵn sàng tại: {persist_directory}")
    return store
