import os
from contextlib import asynccontextmanager
from fastapi import FastAPI, HTTPException
from fastapi.middleware.cors import CORSMiddleware

from app.config import config
from app.models.schemas import (
    ChatRequest, ChatResponse, HealthResponse,
    ChatWithMssvRequest, SyncStudentsRequest, SyncStudentsResponse
)
from app.services.embedding import EmbeddingService
from app.services.chroma_vector_store import initialize_chroma_vector_store
from app.services.groq_client import LLMClient
from app.services.rag import RAGPipeline
from app.services.student_data_service import StudentDataService

rag_pipeline: RAGPipeline = None
student_data_service: StudentDataService = None
embedding_service_global: EmbeddingService = None


@asynccontextmanager
async def lifespan(app: FastAPI):
    """Khởi tạo các service khi server khởi động."""
    global rag_pipeline, student_data_service, embedding_service_global
    
    print("=" * 50)
    print("Khởi động Chatbot Server...")
    print("=" * 50)
    
    if not config.GROQ_API_KEY:
        print("CẢNH BÁO: GROQ_API_KEY chưa được cấu hình!")
    
    print("\n[1/5] Khởi tạo Embedding Service...")
    embedding_service = EmbeddingService(config.EMBEDDING_MODEL)
    embedding_service_global = embedding_service
    
    print("\n[2/5] Khởi tạo ChromaDB Vector Store...")
    vector_store = initialize_chroma_vector_store(
        persist_directory=config.CHROMA_PERSIST_DIR,
        embedding_service=embedding_service,
        collection_name=config.CHROMA_COLLECTION_NAME
    )
    
    print("\n[3/5] Khởi tạo LLM Client...")
    llm_client = LLMClient()
    
    print("\n[4/5] Khởi tạo RAG Pipeline...")
    rag_pipeline = RAGPipeline(
        vector_store=vector_store,
        llm_client=llm_client,
        top_k=config.TOP_K_RESULTS
    )
    
    print("\n[5/5] Khởi tạo Student Data Service...")
    student_data_service = StudentDataService(
        embedding_service=embedding_service,
        persist_directory=config.CHROMA_PERSIST_DIR,
        collection_name=config.CHROMA_COLLECTION_NAME
    )
    
    print("\n" + "=" * 50)
    print("Server đã sẵn sàng!")
    print("=" * 50 + "\n")
    
    yield
    print("Đang tắt server...")


app = FastAPI(
    title="D.University Chatbot API",
    description="API Chatbot tư vấn sinh viên sử dụng RAG với Groq LLM",
    version="1.0.0",
    lifespan=lifespan
)

app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)


@app.get("/", response_model=HealthResponse)
async def root():
    """Kiểm tra API đang hoạt động."""
    return HealthResponse(status="ok", message="D.University Chatbot API đang hoạt động")


@app.get("/health", response_model=HealthResponse)
async def health_check():
    """Kiểm tra tình trạng server."""
    return HealthResponse(status="ok", message="Server hoạt động bình thường")


@app.post("/api/chat", response_model=ChatResponse)
async def chat(request: ChatRequest):
    """Chat với chatbot, hỗ trợ query rewriting và lịch sử hội thoại."""
    if not config.GROQ_API_KEY:
        raise HTTPException(status_code=500, detail="GROQ_API_KEY chưa được cấu hình")
    
    try:
        history = request.conversation_history or []
        response, contexts, rewritten_query = await rag_pipeline.query(
            question=request.message,
            conversation_history=history,
            use_query_rewriting=True
        )
        return ChatResponse(response=response, context_used=contexts, rewritten_query=rewritten_query)
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@app.get("/api/orientation")
async def get_orientation():
    """Lấy định hướng học tập cho sinh viên dựa trên điểm số."""
    if not config.GROQ_API_KEY:
        raise HTTPException(status_code=500, detail="GROQ_API_KEY chưa được cấu hình")
    
    try:
        orientation = await rag_pipeline.get_study_orientation()
        return {"orientation": orientation}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@app.get("/api/student/summary")
async def get_student_summary():
    """Lấy tóm tắt thông tin sinh viên."""
    try:
        summary = rag_pipeline.get_student_summary()
        return {"summary": summary}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@app.post("/api/rebuild-index")
async def rebuild_index():
    """Xây dựng lại ChromaDB index."""
    global rag_pipeline
    
    try:
        embedding_service = EmbeddingService(config.EMBEDDING_MODEL)
        vector_store = initialize_chroma_vector_store(
            persist_directory=config.CHROMA_PERSIST_DIR,
            embedding_service=embedding_service,
            collection_name=config.CHROMA_COLLECTION_NAME,
            force_rebuild=True
        )
        llm_client = LLMClient()
        rag_pipeline = RAGPipeline(
            vector_store=vector_store,
            llm_client=llm_client,
            top_k=config.TOP_K_RESULTS
        )
        return {"message": "Đã rebuild ChromaDB index thành công"}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@app.post("/api/sync-students", response_model=SyncStudentsResponse)
async def sync_students(request: SyncStudentsRequest):
    """Đồng bộ dữ liệu sinh viên từ .NET API vào ChromaDB."""
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
    """Chat với filter theo mã số sinh viên."""
    if not config.GROQ_API_KEY:
        raise HTTPException(status_code=500, detail="GROQ_API_KEY chưa được cấu hình")
    
    try:
        history = request.conversation_history or []
        mssv = request.mssv
        
        student_info = None
        student_info_result = student_data_service.get_student_info(mssv)
        if student_info_result:
            content = student_info_result.get("content", "")
            student_info = {"ma_sinh_vien": mssv}
            for line in content.split("\n"):
                if "Họ tên:" in line:
                    student_info["ho_ten"] = line.split(":")[-1].strip()
        
        search_results = student_data_service.search_by_mssv(
            query=request.message, mssv=mssv, top_k=config.TOP_K_RESULTS
        )
        
        relevant_results = [(chunk, score) for chunk, score in search_results if score > 0.1]
        contexts = [chunk["content"] for chunk, _ in relevant_results]
        
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
    """Lấy thông tin sinh viên theo mã số sinh viên."""
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
    """Xem thống kê ChromaDB (debug)."""
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
