"""
FastAPI Server cho Chatbot tu van sinh vien.
Su dung RAG pipeline voi Groq LLM.
Ho tro conversation memory de nho lich su hoi thoai.
Luu tru lich su vao file JSON de khong mat khi restart.
"""

import os
import uuid
from contextlib import asynccontextmanager
from fastapi import FastAPI, HTTPException
from fastapi.middleware.cors import CORSMiddleware

from app.config import config
from app.models.schemas import (
    ChatRequest, ChatResponse, HealthResponse,
    ClearSessionRequest, SessionInfoResponse
)
from app.services.embedding import EmbeddingService
from app.services.vector_store import initialize_vector_store
from app.services.groq_client import GroqClient
from app.services.rag import RAGPipeline
from app.services.conversation_memory import ConversationMemory

# Global instances
rag_pipeline: RAGPipeline = None
conversation_memory: ConversationMemory = None


@asynccontextmanager
async def lifespan(app: FastAPI):
    """Khoi tao cac service khi server start."""
    global rag_pipeline, conversation_memory
    
    print("=" * 50)
    print("Khởi động Chatbot Server...")
    print("=" * 50)
    
    # Kiem tra API key
    if not config.GROQ_API_KEY:
        print("CANH BAO: GROQ_API_KEY chua duoc cau hinh!")
        print("Vui long tao file .env va them GROQ_API_KEY")
    
    # Khoi tao conversation memory voi file storage
    print("\n[1/5] Khởi tạo Conversation Memory...")
    data_dir = os.path.dirname(config.DATA_PATH)
    storage_path = os.path.join(data_dir, "conversation_history.json")
    conversation_memory = ConversationMemory(storage_path=storage_path)
    print(f"Lưu trữ lịch sử tại: {storage_path}")
    
    # Khoi tao embedding service
    print("\n[2/5] Khởi tạo Embedding Service...")
    embedding_service = EmbeddingService(config.EMBEDDING_MODEL)
    
    # Khoi tao vector store
    print("\n[3/5] Khởi tạo Vector Store...")
    vector_store = initialize_vector_store(
        data_path=config.DATA_PATH,
        vector_store_path=config.VECTOR_STORE_PATH,
        embedding_service=embedding_service
    )
    
    # Khoi tao Groq client
    print("\n[4/5] Khởi tạo Groq Client...")
    groq_client = GroqClient(
        api_key=config.GROQ_API_KEY,
        model=config.GROQ_MODEL
    )
    
    # Khoi tao RAG pipeline
    print("\n[5/5] Khởi tạo RAG Pipeline...")
    rag_pipeline = RAGPipeline(
        vector_store=vector_store,
        groq_client=groq_client,
        top_k=config.TOP_K_RESULTS,
        data_path=config.DATA_PATH  # Truyen duong dan file JSON de doc thong tin sinh vien
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
    - **session_id**: ID session de theo doi lich su (auto-generated neu khong truyen)
    
    Server se tu dong:
    1. Lay lich su hoi thoai tu session_id
    2. Viet lai cau hoi neu la follow-up question
    3. Luu cap hoi-dap vao lich su
    """
    if not config.GROQ_API_KEY:
        raise HTTPException(
            status_code=500,
            detail="GROQ_API_KEY chưa được cấu hình. Vui lòng kiểm tra file .env"
        )
    
    try:
        # Tao hoac lay session_id
        session_id = request.session_id or str(uuid.uuid4())
        
        # Lay lich su hoi thoai tu memory (uu tien hon request.conversation_history)
        history = conversation_memory.get_history(session_id)
        if not history and request.conversation_history:
            # Fallback: dung conversation_history tu request neu chua co trong memory
            history = request.conversation_history
        
        # Goi RAG pipeline voi query rewriting
        response, contexts, rewritten_query = await rag_pipeline.query(
            question=request.message,
            conversation_history=history,
            use_query_rewriting=True
        )
        
        # Luu cap hoi-dap vao memory
        conversation_memory.add_exchange(
            session_id=session_id,
            user_message=request.message,
            assistant_message=response
        )
        
        return ChatResponse(
            response=response,
            context_used=contexts,
            session_id=session_id,
            rewritten_query=rewritten_query
        )
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@app.post("/api/session/clear")
async def clear_session(request: ClearSessionRequest):
    """
    Xoa lich su hoi thoai cua mot session.
    Goi khi nguoi dung muon bat dau cuoc hoi thoai moi.
    """
    conversation_memory.clear_session(request.session_id)
    return {"message": "Đã xóa session thành công", "session_id": request.session_id}


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
    Goi khi du lieu JSON thay doi.
    """
    global rag_pipeline
    
    try:
        embedding_service = EmbeddingService(config.EMBEDDING_MODEL)
        vector_store = initialize_vector_store(
            data_path=config.DATA_PATH,
            vector_store_path=config.VECTOR_STORE_PATH,
            embedding_service=embedding_service,
            force_rebuild=True
        )
        
        groq_client = GroqClient(
            api_key=config.GROQ_API_KEY,
            model=config.GROQ_MODEL
        )
        
        rag_pipeline = RAGPipeline(
            vector_store=vector_store,
            groq_client=groq_client,
            top_k=config.TOP_K_RESULTS,
            data_path=config.DATA_PATH
        )
        
        return {"message": "Đã rebuild index thành công"}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


if __name__ == "__main__":
    import uvicorn
    uvicorn.run("app.main:app", host="0.0.0.0", port=8000, reload=True)
