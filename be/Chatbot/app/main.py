import os
import uuid
from contextlib import asynccontextmanager
from fastapi import FastAPI, HTTPException
from fastapi.middleware.cors import CORSMiddleware

from app.config import config
from app.models.schemas import (
    ChatRequest, ChatResponse, HealthResponse,
    ClearSessionRequest, SessionInfoResponse,
    ChatWithMssvRequest, SyncStudentsRequest, SyncStudentsResponse
)
from app.services.embedding import EmbeddingService
from app.services.chroma_vector_store import initialize_chroma_vector_store
from app.services.groq_client import LLMClient
from app.services.rag import RAGPipeline
from app.services.conversation_memory import ConversationMemory
from app.services.student_data_service import StudentDataService

# Global instances
rag_pipeline: RAGPipeline = None
conversation_memory: ConversationMemory = None
student_data_service: StudentDataService = None
embedding_service_global: EmbeddingService = None


@asynccontextmanager
async def lifespan(app: FastAPI):
    """Khoi tao cac service khi server start."""
    global rag_pipeline, conversation_memory, student_data_service, embedding_service_global
    
    print("=" * 50)
    print("Khoi dong Chatbot Server...")
    print("=" * 50)
    
    # Kiem tra API key
    if not config.GROQ_API_KEY:
        print("CANH BAO: GROQ_API_KEY chua duoc cau hinh!")
        print("Vui long tao file .env va them GROQ_API_KEY")
    
    # Khoi tao conversation memory voi file storage
    print("\n[1/6] Khoi tao Conversation Memory...")
    data_dir = os.path.dirname(config.DATA_PATH)
    storage_path = os.path.join(data_dir, "conversation_history.json")
    conversation_memory = ConversationMemory(storage_path=storage_path)
    print(f"Luu tru lich su tai: {storage_path}")
    
    # Khoi tao embedding service
    print("\n[2/6] Khoi tao Embedding Service...")
    embedding_service = EmbeddingService(config.EMBEDDING_MODEL)
    embedding_service_global = embedding_service
    
    # Khoi tao vector store (ChromaDB)
    print("\n[3/6] Khoi tao ChromaDB Vector Store...")
    vector_store = initialize_chroma_vector_store(
        data_path=config.DATA_PATH,
        persist_directory=config.CHROMA_PERSIST_DIR,
        embedding_service=embedding_service,
        collection_name=config.CHROMA_COLLECTION_NAME
    )
    
    # Khoi tao LLM client
    print("\n[4/6] Khoi tao LLM Client...")
    llm_client = LLMClient()
    
    # Khoi tao RAG pipeline
    print("\n[5/6] Khoi tao RAG Pipeline...")
    rag_pipeline = RAGPipeline(
        vector_store=vector_store,
        llm_client=llm_client,
        top_k=config.TOP_K_RESULTS,
        data_path=config.DATA_PATH
    )
    
    # Khoi tao StudentDataService
    print("\n[6/6] Khoi tao Student Data Service...")
    student_data_service = StudentDataService(
        embedding_service=embedding_service,
        persist_directory=config.CHROMA_PERSIST_DIR,
        collection_name=config.CHROMA_COLLECTION_NAME
    )
    
    print("\n" + "=" * 50)
    print("Server da san sang!")
    print("=" * 50 + "\n")
    
    yield
    
    print("Shutting down...")


# Khoi tao FastAPI app
app = FastAPI(
    title="D.University Chatbot API",
    description="API Chatbot tư vấn sinh viên sử dụng RAG với Groq LLM",
    version="1.0.0",
    lifespan=lifespan
)

# CORS middleware
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)


@app.get("/", response_model=HealthResponse)
async def root():
    """Health check endpoint."""
    return HealthResponse(
        status="ok",
        message="D.University Chatbot API đang hoạt động"
    )


@app.get("/health", response_model=HealthResponse)
async def health_check():
    """Health check endpoint."""
    return HealthResponse(
        status="ok",
        message="Server hoạt động bình thường"
    )


@app.post("/api/chat", response_model=ChatResponse)
async def chat(request: ChatRequest):
    """
    Endpoint chat voi chatbot.
    
    - **message**: Cau hoi cua sinh vien
    - **conversation_history**: Lich su hoi thoai tu client (optional)
    
    Server se:
    1. Viet lai cau hoi neu la follow-up question
    2. Tim kiem context lien quan
    3. Tra ve phan hoi tu LLM
    """
    if not config.GROQ_API_KEY:
        raise HTTPException(
            status_code=500,
            detail="GROQ_API_KEY chưa được cấu hình. Vui lòng kiểm tra file .env"
        )
    
    try:
        # Lay lich su hoi thoai tu request
        history = request.conversation_history or []
        
        # Goi RAG pipeline voi query rewriting
        response, contexts, rewritten_query = await rag_pipeline.query(
            question=request.message,
            conversation_history=history,
            use_query_rewriting=True
        )
        
        return ChatResponse(
            response=response,
            context_used=contexts,
            rewritten_query=rewritten_query
        )
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@app.post("/api/session/clear")
async def clear_session(request: ClearSessionRequest):
    """
    Xoa lich su hoi thoai cua mot session (POST method).
    Goi khi nguoi dung muon bat dau cuoc hoi thoai moi.
    """
    conversation_memory.clear_session(request.session_id)
    return {"message": "Đã xóa session thành công", "session_id": request.session_id}


@app.delete("/api/session/{session_id}")
async def delete_session(session_id: str):
    """
    Xoa mot session theo ID (DELETE method - RESTful).
    """
    # Kiem tra session ton tai
    info = conversation_memory.get_session_info(session_id)
    if not info:
        raise HTTPException(status_code=404, detail="Session không tồn tại")
    
    conversation_memory.clear_session(session_id)
    return {"message": "Đã xóa session thành công", "session_id": session_id}


@app.get("/api/sessions")
async def get_all_sessions():
    """
    Lay danh sach tat ca cac session da luu.
    Tra ve thong tin tom tat cua tung session.
    """
    sessions = conversation_memory.get_all_sessions()
    return {
        "total": len(sessions),
        "sessions": sessions
    }


@app.get("/api/session/{session_id}")
async def get_session_info(session_id: str):
    """
    Lay thong tin cua mot session (khong bao gom lich su hoi thoai).
    """
    info = conversation_memory.get_session_info(session_id)
    if not info:
        raise HTTPException(status_code=404, detail="Session không tồn tại")
    
    return {
        "session_id": session_id,
        "title": info.get("title", ""),
        "message_count": info["message_count"],
        "created_at": info["created_at"],
        "last_access": info["last_access"]
    }


@app.get("/api/session/{session_id}/history")
async def get_session_history(session_id: str):
    """
    Lay lich su hoi thoai day du cua mot session.
    Bao gom thong tin session va tat ca tin nhan.
    """
    session_data = conversation_memory.get_session_with_history(session_id)
    if not session_data:
        raise HTTPException(status_code=404, detail="Session không tồn tại")
    
    return session_data


@app.put("/api/session/{session_id}/rename")
async def rename_session(session_id: str, title: str):
    """
    Doi ten (tieu de) cua mot session.
    
    Query params:
        title: Tieu de moi cho session
    """
    success = conversation_memory.rename_session(session_id, title)
    if not success:
        raise HTTPException(status_code=404, detail="Session không tồn tại")
    
    return {"message": "Đã đổi tên session thành công", "session_id": session_id, "title": title}


@app.get("/api/orientation")
async def get_orientation():
    """
    Endpoint lay dinh huong hoc tap cho sinh vien.
    Phan tich diem so va dua ra loi khuyen cho ky toi.
    """
    if not config.GROQ_API_KEY:
        raise HTTPException(
            status_code=500,
            detail="GROQ_API_KEY chưa được cấu hình. Vui lòng kiểm tra file .env"
        )
    
    try:
        orientation = await rag_pipeline.get_study_orientation()
        return {"orientation": orientation}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@app.get("/api/student/summary")
async def get_student_summary():
    """
    Endpoint lay tom tat thong tin sinh vien.
    """
    try:
        summary = rag_pipeline.get_student_summary()
        return {"summary": summary}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@app.post("/api/rebuild-index")
async def rebuild_index():
    """
    Endpoint rebuild vector store index.
    Gọi khi dữ liệu JSON thay đổi.
    """
    global rag_pipeline
    
    try:
        embedding_service = EmbeddingService(config.EMBEDDING_MODEL)
        vector_store = initialize_chroma_vector_store(
            data_path=config.DATA_PATH,
            persist_directory=config.CHROMA_PERSIST_DIR,
            embedding_service=embedding_service,
            collection_name=config.CHROMA_COLLECTION_NAME,
            force_rebuild=True
        )
        
        llm_client = LLMClient()
        
        rag_pipeline = RAGPipeline(
            vector_store=vector_store,
            llm_client=llm_client,
            top_k=config.TOP_K_RESULTS,
            data_path=config.DATA_PATH
        )
        
        return {"message": "Đã rebuild ChromaDB index thành công"}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@app.post("/api/sync-students", response_model=SyncStudentsResponse)
async def sync_students(request: SyncStudentsRequest):
    """Sync dữ liệu sinh viên vào ChromaDB."""
    try:
        students_data = [{"mssv": s.mssv, "data": s.data} for s in request.students]
        result = student_data_service.sync_students(students_data)
        return SyncStudentsResponse(
            success=result["success"],
            message=result["message"],
            total_students=result["total_students"],
            total_chunks=result["total_chunks"]
        )
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@app.post("/api/chat-with-mssv", response_model=ChatResponse)
async def chat_with_mssv(request: ChatWithMssvRequest):
    """Chat với filter theo mssv."""
    if not config.GROQ_API_KEY:
        raise HTTPException(status_code=500, detail="GROQ_API_KEY chưa được cấu hình")
    
    try:
        history = request.conversation_history or []
        mssv = request.mssv
        
        # Lấy thông tin sinh viên
        student_info = None
        student_info_result = student_data_service.get_student_info(mssv)
        if student_info_result:
            content = student_info_result.get("content", "")
            student_info = {"ma_sinh_vien": mssv}
            for line in content.split("\n"):
                if "Họ tên:" in line:
                    student_info["ho_ten"] = line.split(":")[-1].strip()
        
        # Tìm kiếm context
        search_results = student_data_service.search_by_mssv(
            query=request.message, mssv=mssv, top_k=config.TOP_K_RESULTS
        )
        
        relevant_results = [(chunk, score) for chunk, score in search_results if score > 0.1]
        contexts = [chunk["content"] for chunk, _ in relevant_results]
        
        # Fallback: lấy tất cả thông tin sinh viên
        if not contexts and student_info:
            all_data = student_data_service.search_by_mssv(
                query=f"thông tin sinh viên {mssv}", mssv=mssv, top_k=10
            )
            contexts = [chunk["content"] for chunk, _ in all_data]
        
        if not contexts:
            return ChatResponse(
                response=f"Không tìm thấy thông tin sinh viên {mssv}",
                context_used=[], rewritten_query=None
            )
        
        llm_client = LLMClient()
        messages = llm_client.build_rag_prompt(
            query=request.message, context=contexts,
            conversation_history=history, student_info=student_info
        )
        response = await llm_client.chat_completion(messages)
        
        return ChatResponse(response=response, context_used=contexts, rewritten_query=None)
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@app.get("/api/student/{mssv}/info")
async def get_student_info_by_mssv(mssv: str):
    """Lấy thông tin sinh viên theo mssv."""
    try:
        results = student_data_service.search_by_mssv(
            query=f"thông tin sinh viên {mssv}", mssv=mssv, top_k=10
        )
        if not results:
            raise HTTPException(status_code=404, detail=f"Không tìm thấy sinh viên {mssv}")
        return {
            "mssv": mssv,
            "data": [{"content": r[0]["content"], "type": r[0].get("metadata", {}).get("type", ""), "score": r[1]} for r in results]
        }
    except HTTPException:
        raise
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@app.get("/api/debug/chroma-stats")
async def get_chroma_stats():
    """Debug: Xem thống kê ChromaDB."""
    try:
        count = student_data_service.vector_store.count()
        sample = student_data_service.vector_store.collection.get(limit=5, include=["metadatas", "documents"])
        all_meta = student_data_service.vector_store.collection.get(include=["metadatas"])
        unique_mssv = set(m.get("mssv") for m in all_meta["metadatas"] if m and "mssv" in m)
        return {
            "total_documents": count,
            "unique_students": list(unique_mssv),
            "sample_documents": [
                {"id": sample["ids"][i], "metadata": sample["metadatas"][i], "content_preview": sample["documents"][i][:200]}
                for i in range(len(sample["ids"]))
            ] if sample["ids"] else []
        }
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


if __name__ == "__main__":
    import uvicorn
    uvicorn.run("app.main:app", host="0.0.0.0", port=8000, reload=True)
