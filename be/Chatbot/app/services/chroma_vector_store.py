"""
ChromaDB Vector Store - Thay thế FAISS với nhiều tính năng hơn.
Hỗ trợ persistence, metadata filtering, và incremental updates.
"""
import os
from typing import List, Dict, Any, Tuple, Optional
import chromadb
from chromadb.config import Settings

from app.services.embedding import EmbeddingService
from app.services.chunking import load_and_chunk_data


class ChromaVectorStore:
    """
    Vector store sử dụng ChromaDB để lưu trữ và tìm kiếm embedding.
    Ưu điểm so với FAISS:
    - Tự động persistence
    - Hỗ trợ metadata filtering
    - Dễ dàng thêm/xóa/cập nhật document
    """
    
    def __init__(
        self, 
        embedding_service: EmbeddingService,
        persist_directory: str = "./data/chroma_db",
        collection_name: str = "student_data"
    ):
        """
        Khởi tạo ChromaDB vector store.
        
        Args:
            embedding_service: Service để tạo embedding
            persist_directory: Thư mục lưu trữ ChromaDB
            collection_name: Tên collection
        """
        self.embedding_service = embedding_service
        self.persist_directory = persist_directory
        self.collection_name = collection_name
        
        # Đảm bảo thư mục tồn tại
        os.makedirs(persist_directory, exist_ok=True)
        
        # Khởi tạo ChromaDB client với persistence
        self.client = chromadb.PersistentClient(path=persist_directory)
        
        # Lấy hoặc tạo collection
        self.collection = self.client.get_or_create_collection(
            name=collection_name,
            metadata={"hnsw:space": "cosine"}  # Sử dụng cosine similarity
        )
        
        print(f"ChromaDB đã khởi tạo tại: {persist_directory}")
        print(f"Collection '{collection_name}' có {self.collection.count()} documents")
    
    def add_documents(self, chunks: List[Dict[str, Any]], batch_size: int = 100) -> None:
        """
        Thêm các chunk vào ChromaDB.
        
        Args:
            chunks: Danh sách các chunk dữ liệu
            batch_size: Số lượng chunk xử lý mỗi batch
        """
        total_chunks = len(chunks)
        print(f"Đang thêm {total_chunks} chunks vào ChromaDB...")
        
        for i in range(0, total_chunks, batch_size):
            batch = chunks[i:i + batch_size]
            
            # Tạo embedding cho batch
            texts = [chunk["content"] for chunk in batch]
            embeddings = self.embedding_service.embed_texts(texts).tolist()
            
            # Tạo IDs và metadata
            ids = [f"chunk_{i + j}" for j in range(len(batch))]
            metadatas = []
            for chunk in batch:
                metadata = {
                    "type": chunk.get("type", "unknown"),
                    "source": chunk.get("source", ""),
                }
                # Thêm các metadata khác nếu có
                if "hoc_ky" in chunk:
                    metadata["hoc_ky"] = str(chunk["hoc_ky"])
                if "ma_mon" in chunk:
                    metadata["ma_mon"] = chunk["ma_mon"]
                if "loai" in chunk:
                    metadata["loai"] = chunk["loai"]
                metadatas.append(metadata)
            
            # Thêm vào collection
            self.collection.add(
                ids=ids,
                embeddings=embeddings,
                documents=texts,
                metadatas=metadatas
            )
            
            processed = min(i + batch_size, total_chunks)
            print(f"  Đã thêm {processed}/{total_chunks} chunks...")
        
        print(f"Hoàn thành! Collection có {self.collection.count()} documents")
    
    def search(
        self, 
        query: str, 
        top_k: int = 5,
        where: Optional[Dict] = None,
        where_document: Optional[Dict] = None
    ) -> List[Tuple[Dict[str, Any], float]]:
        """
        Tìm kiếm các chunk tương tự với query.
        
        Args:
            query: Câu hỏi cần tìm kiếm
            top_k: Số lượng kết quả trả về
            where: Filter theo metadata (vd: {"type": "diem"})
            where_document: Filter theo nội dung document
            
        Returns:
            Danh sách tuple (chunk, score) sắp xếp theo độ tương đồng giảm dần
        """
        # Tạo embedding cho query
        query_embedding = self.embedding_service.embed_text(query).tolist()
        
        # Tìm kiếm trong ChromaDB
        results = self.collection.query(
            query_embeddings=[query_embedding],
            n_results=top_k,
            where=where,
            where_document=where_document,
            include=["documents", "metadatas", "distances"]
        )
        
        # Chuyển đổi kết quả
        output = []
        if results and results["documents"] and len(results["documents"]) > 0:
            for i, doc in enumerate(results["documents"][0]):
                chunk = {
                    "content": doc,
                    "metadata": results["metadatas"][0][i] if results["metadatas"] else {}
                }
                # ChromaDB trả về distance, chuyển thành similarity score
                # Cosine distance: similarity = 1 - distance
                distance = results["distances"][0][i] if results["distances"] else 0
                similarity = 1 - distance
                output.append((chunk, similarity))
        
        return output
    
    def delete_all(self) -> None:
        """Xóa tất cả documents trong collection."""
        # Xóa collection và tạo lại
        self.client.delete_collection(self.collection_name)
        self.collection = self.client.get_or_create_collection(
            name=self.collection_name,
            metadata={"hnsw:space": "cosine"}
        )
        print(f"Đã xóa tất cả documents trong collection '{self.collection_name}'")
    
    def delete_by_ids(self, ids: List[str]) -> None:
        """
        Xóa documents theo ID.
        
        Args:
            ids: Danh sách ID cần xóa
        """
        self.collection.delete(ids=ids)
        print(f"Đã xóa {len(ids)} documents")
    
    def update_document(
        self, 
        doc_id: str, 
        content: str, 
        metadata: Optional[Dict] = None
    ) -> None:
        """
        Cập nhật một document.
        
        Args:
            doc_id: ID của document
            content: Nội dung mới
            metadata: Metadata mới (optional)
        """
        embedding = self.embedding_service.embed_text(content).tolist()
        
        update_kwargs = {
            "ids": [doc_id],
            "documents": [content],
            "embeddings": [embedding]
        }
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
    data_path: str,
    persist_directory: str,
    embedding_service: EmbeddingService,
    collection_name: str = "student_data",
    force_rebuild: bool = False
) -> ChromaVectorStore:
    """
    Khởi tạo ChromaDB vector store từ dữ liệu JSON.
    
    Args:
        data_path: Đường dẫn đến file JSON
        persist_directory: Đường dẫn để lưu ChromaDB
        embedding_service: Service để tạo embedding
        collection_name: Tên collection
        force_rebuild: True để xây dựng lại index dù đã tồn tại
        
    Returns:
        ChromaVectorStore đã được khởi tạo
    """
    store = ChromaVectorStore(
        embedding_service=embedding_service,
        persist_directory=persist_directory,
        collection_name=collection_name
    )
    
    # Nếu đã có data và không force rebuild
    if store.count() > 0 and not force_rebuild:
        print(f"Sử dụng ChromaDB có sẵn với {store.count()} documents")
        return store
    
    # Xây dựng mới
    print(f"Đang xây dựng ChromaDB từ: {data_path}")
    
    if force_rebuild and store.count() > 0:
        store.delete_all()
    
    chunks = load_and_chunk_data(data_path)
    store.add_documents(chunks)
    
    return store
