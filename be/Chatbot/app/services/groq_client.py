import httpx
from typing import List, Dict, Optional

from app.llm_providers import LLMConfig, DEFAULT


class LLMClient:
    """Client để gọi LLM API (Groq, vLLM, hoặc OpenAI-compatible API)."""
    
    def __init__(self, provider: LLMConfig = None):
        """Khởi tạo LLM client với provider từ llm_providers."""
        if provider is None:
            provider = DEFAULT
        
        self.provider = provider
        self.api_url = provider.base_url
        self.model = provider.model
        self.headers = provider.headers
        print(f"LLMClient khởi tạo với provider: {provider.name} ({provider.base_url})")
    
    async def chat_completion(
        self,
        messages: List[Dict[str, str]],
        temperature: float = 0.7,
        max_tokens: int = 2048,
        stream: bool = False
    ) -> str:
        """Gọi LLM API để sinh phản hồi."""
        payload = {
            "model": self.model,
            "messages": messages,
            "temperature": temperature,
            "max_tokens": max_tokens,
            "stream": stream
        }
        
        async with httpx.AsyncClient(timeout=60.0) as client:
            response = await client.post(self.api_url, headers=self.headers, json=payload)
            response.raise_for_status()
            result = response.json()
            return result["choices"][0]["message"]["content"]
    
    def build_rag_prompt(
        self,
        query: str,
        context: List[str],
        conversation_history: Optional[List[Dict]] = None,
        student_info: Optional[Dict] = None
    ) -> List[Dict[str, str]]:
        """Xây dựng prompt cho RAG với context và thông tin sinh viên."""
        student_section = ""
        if student_info:
            student_section = f"""
Thông tin sinh viên đang trò chuyện:
- Họ tên: {student_info.get('ho_ten', 'Không rõ')}
- Mã sinh viên: {student_info.get('ma_sinh_vien', 'Không rõ')}
- Ngày sinh: {student_info.get('ngay_sinh', 'Không rõ')}
- Khóa học: {student_info.get('khoa_hoc', 'Không rõ')}
- Chuyên ngành: {student_info.get('chuyen_nganh', 'Không rõ')}
- Học kỳ hiện tại: {student_info.get('hoc_ky_hien_tai', 'Không rõ')}
- Tình trạng: {student_info.get('tinh_trang', 'Không rõ')}
"""
        
        system_prompt = f"""Bạn là trợ lý AI hỗ trợ sinh viên của Trường Đại học D.University. 
{student_section}
Nhiệm vụ của bạn là:
1. Trả lời các câu hỏi về thông tin học tập, điểm số, chương trình đào tạo
2. Đưa ra định hướng, lời khuyên cho học kỳ sắp tới
3. Giải thích các quy định học vụ

Quy tắc trả lời:
- Luôn trả lời bằng tiếng Việt
- Xưng hô thân thiện, gọi sinh viên bằng tên
- Dựa vào context để trả lời chính xác
- Nếu không có thông tin, nói rõ là không có

Quy tắc định dạng:
- Trả lời theo định dạng Markdown
- Sử dụng **in đậm** cho thông tin quan trọng
- Sử dụng danh sách khi liệt kê
- Sử dụng bảng khi hiển thị điểm

Context thông tin:
---
{{context}}
---"""
        
        context_text = "\n\n".join(context)
        messages = [{"role": "system", "content": system_prompt.format(context=context_text)}]
        
        if conversation_history:
            for msg in conversation_history[-6:]:
                messages.append(msg)
        
        messages.append({"role": "user", "content": query})
        return messages
    
    def build_orientation_prompt(
        self,
        student_info: str,
        current_grades: str,
        next_semester_curriculum: str
    ) -> List[Dict[str, str]]:
        """Xây dựng prompt để đưa ra định hướng học tập."""
        system_prompt = f"""Bạn là cố vấn học tập của Trường Đại học D.University.
Dựa vào thông tin sinh viên, kết quả học tập và chương trình học kỳ tới,
hãy đưa ra định hướng và lời khuyên cụ thể.

Phân tích cần bao gồm:
1. Đánh giá tình hình học tập hiện tại
2. Các môn cần cải thiện
3. Chiến lược học tập cho kỳ tới
4. Lời khuyên về cân bằng học tập

Thông tin sinh viên:
{student_info}

Kết quả học tập các kỳ trước:
{current_grades}

Chương trình học kỳ tới:
{next_semester_curriculum}
"""
        
        return [
            {"role": "system", "content": system_prompt},
            {"role": "user", "content": "Hãy phân tích và đưa ra định hướng học tập cho tôi."}
        ]
