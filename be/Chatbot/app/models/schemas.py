from pydantic import BaseModel
from typing import List, Optional
import uuid


class ChatRequest(BaseModel):
    """Request model cho chat endpoint."""
    message: str
    conversation_history: Optional[List[dict]] = []  # Lich su hoi thoai tu client


class ChatResponse(BaseModel):
    """Response model cho chat endpoint."""
    response: str
    context_used: List[str]
    rewritten_query: Optional[str] = None  # Cau hoi da duoc viet lai (neu co)


class ClearSessionRequest(BaseModel):
    """Request model de xoa session."""
    session_id: str


class SessionInfoResponse(BaseModel):
    """Response model cho thong tin session."""
    session_id: str
    title: Optional[str] = ""
    message_count: int
    created_at: str
    last_access: str

    
class HealthResponse(BaseModel):
    status: str
    message: str


class ChatWithMssvRequest(BaseModel):
    message: str
    mssv: str
    conversation_history: Optional[List[dict]] = []


class StudentDataItem(BaseModel):
    mssv: str
    data: dict


class SyncStudentsRequest(BaseModel):
    students: List[StudentDataItem]


class SyncStudentsResponse(BaseModel):
    success: bool
    message: str
    total_students: int
    total_chunks: int
