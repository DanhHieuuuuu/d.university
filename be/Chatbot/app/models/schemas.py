from pydantic import BaseModel
from typing import List, Optional


class ChatRequest(BaseModel):
    """Request model cho endpoint chat."""
    message: str
    conversation_history: Optional[List[dict]] = []


class ChatResponse(BaseModel):
    """Response model cho endpoint chat."""
    response: str
    context_used: List[str]
    rewritten_query: Optional[str] = None


class HealthResponse(BaseModel):
    """Response model cho health check."""
    status: str
    message: str


class ChatWithMssvRequest(BaseModel):
    """Request model cho endpoint chat với mssv."""
    message: str
    mssv: str
    conversation_history: Optional[List[dict]] = []


class StudentDataItem(BaseModel):
    """Model cho một sinh viên trong request sync."""
    mssv: str
    data: dict


class SyncStudentsRequest(BaseModel):
    """Request model cho endpoint sync sinh viên."""
    students: List[StudentDataItem]


class SyncStudentsResponse(BaseModel):
    """Response model cho endpoint sync sinh viên."""
    success: bool
    message: str
    total_students: int
    total_chunks: int
