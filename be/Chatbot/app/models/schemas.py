from pydantic import BaseModel
from typing import List, Optional
import uuid


class ChatRequest(BaseModel):
    """Request model cho chat endpoint."""
    message: str
    session_id: Optional[str] = None  # ID session de theo doi lich su hoi thoai
    conversation_history: Optional[List[dict]] = []  # Backward compatible


class ChatResponse(BaseModel):
    """Response model cho chat endpoint."""
    response: str
    context_used: List[str]
    session_id: str  # Tra ve session_id de frontend dung cho cac request tiep theo
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
