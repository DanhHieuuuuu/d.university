from typing import List, Dict, Optional


class QueryRewriter:
    """Viết lại câu hỏi dựa trên ngữ cảnh hội thoại để tìm kiếm context tốt hơn."""
    
    def __init__(self, llm_client: Optional[object] = None):
        """Khởi tạo Query Rewriter với LLM client."""
        self.llm_client = llm_client
    
    async def rewrite_query(
        self,
        current_query: str,
        conversation_history: List[Dict[str, str]],
        max_context_messages: int = 4
    ) -> str:
        """Viết lại câu hỏi dựa trên lịch sử hội thoại."""
        if not conversation_history:
            return current_query
        
        recent_history = conversation_history[-max_context_messages:]
        needs_rewrite = self._needs_rewrite(current_query, recent_history)
        
        if not needs_rewrite:
            return current_query
        
        rewritten = await self._llm_rewrite(current_query, recent_history)
        return rewritten
    
    def _needs_rewrite(self, query: str, history: List[Dict[str, str]]) -> bool:
        """Kiểm tra xem câu hỏi có cần viết lại không."""
        if not history:
            return False
        
        query_lower = query.lower().strip()
        
        if len(query_lower) < 20:
            return True
        
        follow_up_indicators = [
            "còn", "thế", "vậy", "thì sao", "như vậy", "như thế",
            "cái đó", "món đó", "nó", "họ", "điều đó",
            "ở trên", "bên trên", "vừa nói", "vừa hỏi",
            "tiếp", "nữa", "thêm"
        ]
        
        for indicator in follow_up_indicators:
            if indicator in query_lower:
                return True
        
        start_words = ["còn ", "và ", "thế ", "vậy ", "rồi ", "sau đó "]
        for word in start_words:
            if query_lower.startswith(word):
                return True
        
        return False
    
    async def _llm_rewrite(self, query: str, history: List[Dict[str, str]]) -> str:
        """Sử dụng LLM để viết lại câu hỏi."""
        history_text = ""
        for msg in history:
            role = "Người dùng" if msg["role"] == "user" else "Trợ lý"
            history_text += f"{role}: {msg['content']}\n"
        
        system_prompt = """Bạn là trợ lý giúp viết lại câu hỏi.
Viết lại câu hỏi hiện tại thành câu hỏi độc lập, đầy đủ ngữ cảnh.

Quy tắc:
1. Giữ nguyên ý nghĩa câu hỏi gốc
2. Thêm ngữ cảnh từ lịch sử hội thoại nếu cần
3. CHỈ trả về câu hỏi đã viết lại, không giải thích
"""
        
        user_prompt = f"""Lịch sử hội thoại:
{history_text}

Câu hỏi hiện tại: {query}

Câu hỏi đã viết lại:"""
        
        messages = [
            {"role": "system", "content": system_prompt},
            {"role": "user", "content": user_prompt}
        ]
        
        try:
            rewritten = await self.llm_client.chat_completion(messages, temperature=0.1, max_tokens=256)
            rewritten = rewritten.strip().strip('"').strip("'")
            
            if not rewritten or len(rewritten) > len(query) * 3:
                return query
            
            return rewritten
        except Exception as e:
            print(f"Query rewrite error: {e}")
            return query
