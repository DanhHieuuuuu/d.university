"""
Module Query Rewriter.
Viet lai cau hoi dua tren lich su hoi thoai de search vector hieu qua hon.
"""

from typing import List, Dict, Optional


class QueryRewriter:
    """
    Viet lai cau hoi dua tren ngu canh hoi thoai.
    Giup cau hoi theo sau (follow-up) co the tim kiem context tot hon.
    """
    
    def __init__(self, groq_client):
        """
        Khoi tao Query Rewriter.
        
        Args:
            groq_client: Groq client de goi LLM
        """
        self.groq_client = groq_client
    
    async def rewrite_query(
        self,
        current_query: str,
        conversation_history: List[Dict[str, str]],
        max_context_messages: int = 4
    ) -> str:
        """
        Viet lai cau hoi dua tren lich su hoi thoai.
        
        Vi du:
        - User: "Diem mon Toan cua toi la bao nhieu?"
        - Assistant: "Diem mon Toan cua ban la 8.5"
        - User: "Con mon Ly?"  <- Cau hoi nay can duoc viet lai
        - Rewritten: "Diem mon Vat Ly cua toi la bao nhieu?"
        
        Args:
            current_query: Cau hoi hien tai cua nguoi dung
            conversation_history: Lich su hoi thoai
            max_context_messages: So tin nhan gan nhat dung lam ngu canh
            
        Returns:
            Cau hoi da duoc viet lai (hoac giu nguyen neu khong can)
        """
        # Neu khong co lich su, tra ve nguyen cau hoi
        if not conversation_history:
            return current_query
        
        # Lay cac tin nhan gan nhat
        recent_history = conversation_history[-max_context_messages:]
        
        # Kiem tra xem cau hoi co can viet lai khong
        # Cac dau hieu: cau hoi ngan, dung dai tu chi dinh (do, nay, kia), hoi tiep
        needs_rewrite = self._needs_rewrite(current_query, recent_history)
        
        if not needs_rewrite:
            return current_query
        
        # Goi LLM de viet lai cau hoi
        rewritten = await self._llm_rewrite(current_query, recent_history)
        
        return rewritten
    
    def _needs_rewrite(self, query: str, history: List[Dict[str, str]]) -> bool:
        """
        Kiem tra xem cau hoi co can viet lai khong.
        
        Cac truong hop can viet lai:
        1. Cau hoi qua ngan (< 20 ky tu)
        2. Chua dai tu chi dinh: do, nay, no, ho, cai do, mon do, ...
        3. Cau hoi bat dau bang "Con", "The", "Va", "Nhu vay"
        """
        # Khong co lich su thi khong can viet lai
        if not history:
            return False
        
        query_lower = query.lower().strip()
        
        # Cau hoi ngan thuong la follow-up
        if len(query_lower) < 20:
            return True
        
        # Cac tu chi follow-up question
        follow_up_indicators = [
            "còn", "thế", "vậy", "thì sao", "như vậy", "như thế",
            "cái đó", "món đó", "nó", "họ", "điều đó",
            "ở trên", "bên trên", "vừa nói", "vừa hỏi",
            "tiếp", "nữa", "thêm"
        ]
        
        for indicator in follow_up_indicators:
            if indicator in query_lower:
                return True
        
        # Bat dau bang cac tu nhu "con", "va", "the"
        start_words = ["còn ", "và ", "thế ", "vậy ", "rồi ", "sau đó "]
        for word in start_words:
            if query_lower.startswith(word):
                return True
        
        return False
    
    async def _llm_rewrite(self, query: str, history: List[Dict[str, str]]) -> str:
        """
        Su dung LLM de viet lai cau hoi.
        """
        # Format lich su hoi thoai
        history_text = ""
        for msg in history:
            role = "Người dùng" if msg["role"] == "user" else "Trợ lý"
            history_text += f"{role}: {msg['content']}\n"
        
        system_prompt = """Bạn là một trợ lý giúp viết lại câu hỏi.
Nhiệm vụ: Viết lại câu hỏi hiện tại thành một câu hỏi độc lập, đầy đủ ngữ cảnh, 
dựa trên lịch sử hội thoại được cung cấp.

Quy tắc:
1. Giữ nguyên ý nghĩa của câu hỏi gốc
2. Thêm thông tin ngữ cảnh từ lịch sử hội thoại nếu cần
3. Câu hỏi mới phải độc lập, không cần đọc lịch sử cũng hiểu được
4. CHỈ trả về câu hỏi đã viết lại, KHÔNG giải thích hay thêm gì khác
5. Nếu câu hỏi đã đầy đủ, trả về nguyên bản

Ví dụ:
- Lịch sử: "Điểm môn Toán của tôi là bao nhiêu?" -> "Điểm Toán là 8.5"
- Câu hỏi: "Còn môn Lý?"
- Viết lại: "Điểm môn Vật Lý của tôi là bao nhiêu?"
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
            rewritten = await self.groq_client.chat_completion(
                messages, 
                temperature=0.1,  # Low temperature for consistent output
                max_tokens=256
            )
            
            # Lam sach ket qua
            rewritten = rewritten.strip().strip('"').strip("'")
            
            # Neu ket qua rong hoac qua dai, tra ve cau hoi goc
            if not rewritten or len(rewritten) > len(query) * 3:
                return query
            
            return rewritten
            
        except Exception as e:
            print(f"Query rewrite error: {e}")
            return query
