"""
Module quan ly lich su hoi thoai (Conversation Memory).
Luu tru va truy xuat lich su theo session_id.
Ho tro luu tru persistent vao file JSON.
"""

import json
import os
import threading
from typing import List, Dict, Optional
from datetime import datetime, timedelta
from collections import OrderedDict


class ConversationMemory:
    """
    Luu tru lich su hoi thoai trong memory va file JSON.
    Su dung LRU cache de giai phong session cu.
    Tu dong luu vao file khi co thay doi.
    """
    
    def __init__(
        self, 
        storage_path: Optional[str] = None,
        max_sessions: int = 1000, 
        max_history_per_session: int = 20, 
        session_ttl_hours: int = 24 * 7  # 7 ngay
    ):
        """
        Khoi tao conversation memory.
        
        Args:
            storage_path: Duong dan file JSON de luu tru persistent
            max_sessions: So luong session toi da luu tru
            max_history_per_session: So luong tin nhan toi da moi session
            session_ttl_hours: Thoi gian song cua session (gio)
        """
        self.storage_path = storage_path
        self.max_sessions = max_sessions
        self.max_history = max_history_per_session
        self.session_ttl = timedelta(hours=session_ttl_hours)
        
        # OrderedDict de thuc hien LRU
        self._sessions: OrderedDict[str, Dict] = OrderedDict()
        self._lock = threading.Lock()
        
        # Tai du lieu tu file neu co
        if storage_path:
            self._load_from_file()
    
    def _load_from_file(self):
        """Tai lich su tu file JSON."""
        if not self.storage_path or not os.path.exists(self.storage_path):
            return
        
        try:
            with open(self.storage_path, 'r', encoding='utf-8') as f:
                data = json.load(f)
            
            for session_id, session_data in data.get('sessions', {}).items():
                # Chuyen chuoi datetime thanh datetime object
                self._sessions[session_id] = {
                    "history": session_data.get("history", []),
                    "created_at": datetime.fromisoformat(session_data["created_at"]),
                    "last_access": datetime.fromisoformat(session_data["last_access"]),
                    "title": session_data.get("title", "")
                }
            
            print(f"Da tai {len(self._sessions)} session tu file: {self.storage_path}")
            
            # Xoa cac session het han
            self._cleanup_expired()
            
        except Exception as e:
            print(f"Loi khi tai lich su tu file: {e}")
    
    def _save_to_file(self):
        """Luu lich su vao file JSON."""
        if not self.storage_path:
            return
        
        try:
            # Tao thu muc neu chua ton tai
            os.makedirs(os.path.dirname(self.storage_path), exist_ok=True)
            
            # Chuyen datetime thanh chuoi ISO
            data = {
                "sessions": {},
                "last_updated": datetime.now().isoformat()
            }
            
            for session_id, session_data in self._sessions.items():
                data["sessions"][session_id] = {
                    "history": session_data["history"],
                    "created_at": session_data["created_at"].isoformat(),
                    "last_access": session_data["last_access"].isoformat(),
                    "title": session_data.get("title", "")
                }
            
            with open(self.storage_path, 'w', encoding='utf-8') as f:
                json.dump(data, f, ensure_ascii=False, indent=2)
                
        except Exception as e:
            print(f"Loi khi luu lich su vao file: {e}")
    
    def _cleanup_expired(self):
        """Xoa cac session het han."""
        now = datetime.now()
        expired = [
            sid for sid, data in self._sessions.items()
            if now - data["last_access"] > self.session_ttl
        ]
        for sid in expired:
            del self._sessions[sid]
        
        if expired:
            self._save_to_file()
    
    def _ensure_capacity(self):
        """Dam bao khong vuot qua so luong session toi da."""
        while len(self._sessions) >= self.max_sessions:
            self._sessions.popitem(last=False)  # Xoa session cu nhat
    
    def _generate_title(self, first_message: str) -> str:
        """Tao tieu de cho session tu tin nhan dau tien."""
        # Lay 50 ky tu dau tien lam tieu de
        title = first_message[:50]
        if len(first_message) > 50:
            title += "..."
        return title
    
    def get_history(self, session_id: str) -> List[Dict[str, str]]:
        """
        Lay lich su hoi thoai cua mot session.
        
        Args:
            session_id: ID cua session
            
        Returns:
            Danh sach tin nhan theo format [{"role": "user/assistant", "content": "..."}]
        """
        with self._lock:
            if session_id not in self._sessions:
                return []
            
            # Cap nhat thoi gian truy cap va di chuyen len dau
            self._sessions[session_id]["last_access"] = datetime.now()
            self._sessions.move_to_end(session_id)
            
            return self._sessions[session_id]["history"].copy()
    
    def add_message(self, session_id: str, role: str, content: str):
        """
        Them mot tin nhan vao lich su.
        
        Args:
            session_id: ID cua session
            role: "user" hoac "assistant"
            content: Noi dung tin nhan
        """
        with self._lock:
            self._cleanup_expired()
            
            if session_id not in self._sessions:
                self._ensure_capacity()
                self._sessions[session_id] = {
                    "history": [],
                    "created_at": datetime.now(),
                    "last_access": datetime.now(),
                    "title": ""
                }
            
            session = self._sessions[session_id]
            session["history"].append({"role": role, "content": content})
            session["last_access"] = datetime.now()
            
            # Tao tieu de tu tin nhan dau tien cua user
            if not session["title"] and role == "user":
                session["title"] = self._generate_title(content)
            
            # Giu lai chi max_history tin nhan
            if len(session["history"]) > self.max_history:
                session["history"] = session["history"][-self.max_history:]
            
            # Di chuyen len dau (most recently used)
            self._sessions.move_to_end(session_id)
            
            # Luu vao file
            self._save_to_file()
    
    def add_exchange(self, session_id: str, user_message: str, assistant_message: str):
        """
        Them mot cap hoi-dap vao lich su.
        
        Args:
            session_id: ID cua session
            user_message: Cau hoi cua nguoi dung
            assistant_message: Phan hoi cua assistant
        """
        self.add_message(session_id, "user", user_message)
        self.add_message(session_id, "assistant", assistant_message)
    
    def clear_session(self, session_id: str):
        """Xoa lich su cua mot session."""
        with self._lock:
            if session_id in self._sessions:
                del self._sessions[session_id]
                self._save_to_file()
    
    def get_session_info(self, session_id: str) -> Optional[Dict]:
        """Lay thong tin cua session."""
        with self._lock:
            if session_id not in self._sessions:
                return None
            
            session = self._sessions[session_id]
            return {
                "message_count": len(session["history"]),
                "created_at": session["created_at"].isoformat(),
                "last_access": session["last_access"].isoformat(),
                "title": session.get("title", "")
            }
    
    def get_all_sessions(self) -> List[Dict]:
        """
        Lay danh sach tat ca cac session.
        
        Returns:
            Danh sach cac session voi thong tin tom tat
        """
        with self._lock:
            sessions = []
            for session_id, session_data in reversed(self._sessions.items()):
                sessions.append({
                    "session_id": session_id,
                    "title": session_data.get("title", ""),
                    "message_count": len(session_data["history"]),
                    "created_at": session_data["created_at"].isoformat(),
                    "last_access": session_data["last_access"].isoformat()
                })
            return sessions
    
    def get_session_with_history(self, session_id: str) -> Optional[Dict]:
        """
        Lay thong tin day du cua session bao gom lich su hoi thoai.
        
        Args:
            session_id: ID cua session
            
        Returns:
            Dict chua thong tin session va lich su hoi thoai
        """
        with self._lock:
            if session_id not in self._sessions:
                return None
            
            session = self._sessions[session_id]
            return {
                "session_id": session_id,
                "title": session.get("title", ""),
                "message_count": len(session["history"]),
                "created_at": session["created_at"].isoformat(),
                "last_access": session["last_access"].isoformat(),
                "history": session["history"].copy()
            }
    
    def rename_session(self, session_id: str, new_title: str) -> bool:
        """
        Doi ten (tieu de) cua session.
        
        Args:
            session_id: ID cua session
            new_title: Tieu de moi
            
        Returns:
            True neu thanh cong, False neu session khong ton tai
        """
        with self._lock:
            if session_id not in self._sessions:
                return False
            
            self._sessions[session_id]["title"] = new_title
            self._save_to_file()
            return True


# Ham helper de khoi tao voi duong dan mac dinh
def create_conversation_memory(data_dir: str = None) -> ConversationMemory:
    """
    Tao ConversationMemory voi duong dan luu tru mac dinh.
    
    Args:
        data_dir: Thu muc data (neu None se dung thu muc mac dinh)
        
    Returns:
        ConversationMemory instance
    """
    if data_dir is None:
        # Su dung thu muc data mac dinh
        current_dir = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))
        data_dir = os.path.join(current_dir, "..", "data")
    
    storage_path = os.path.join(data_dir, "conversation_history.json")
    
    return ConversationMemory(storage_path=storage_path)


# Global instance - se duoc khoi tao trong main.py
conversation_memory: ConversationMemory = None
