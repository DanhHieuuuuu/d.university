import httpx
from typing import List, Dict, Optional

from app.llm_providers import LLMConfig, get_provider, DEFAULT

class LLMClient:
    """
    Client de goi LLM API (Groq, vLLM, hoac bat ky OpenAI-compatible API nao).
    """
    
    def __init__(self, provider: LLMConfig = None):
        """
        Khoi tao LLM client.
        
        Args:
            provider: Cau hinh LLM tu llm_providers. 
                      Neu None, su dung DEFAULT (tu .env LLM_PROVIDER)
        
        Cach su dung:
            from app.llm_providers import GROQ, VLLM
            client = LLMClient(GROQ)  # Su dung Groq
            client = LLMClient(VLLM)  # Su dung vLLM local
            client = LLMClient()      # Su dung DEFAULT tu .env
        """
        if provider is None:
            provider = DEFAULT
        
        self.provider = provider
        self.api_url = provider.base_url
        self.model = provider.model
        self.headers = provider.headers
        print(f"LLMClient khoi tao voi provider: {provider.name} ({provider.base_url})")
    
    async def chat_completion(
        self,
        messages: List[Dict[str, str]],
        temperature: float = 0.7,
        max_tokens: int = 2048,
        stream: bool = False
    ) -> str:
        """
        Goi Groq API de sinh phan hoi.
        
        Args:
            messages: Danh sach messages theo format OpenAI
            temperature: Do sang tao (0-1)
            max_tokens: So token toi da trong phan hoi
            stream: Co stream response hay khong
            
        Returns:
            Phan hoi tu LLM
        """
        payload = {
            "model": self.model,
            "messages": messages,
            "temperature": temperature,
            "max_tokens": max_tokens,
            "stream": stream
        }
        
        async with httpx.AsyncClient(timeout=60.0) as client:
            response = await client.post(
                self.api_url,
                headers=self.headers,
                json=payload
            )
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
        """
        Xay dung prompt cho RAG.
        
        Args:
            query: Cau hoi cua nguoi dung
            context: Danh sach context tu vector store
            conversation_history: Lich su hoi thoai (neu co)
            student_info: Thong tin sinh vien (ten, ma SV, khoa, nganh...)
            
        Returns:
            Danh sach messages de gui cho LLM
        """
        # Xay dung phan thong tin sinh vien
        student_section = ""
        if student_info:
            student_section = f"""
Thông tin sinh viên đang trò chuyện:
- Họ tên: {student_info.get('ho_ten', 'Không rõ')}
- Mã sinh viên: {student_info.get('ma_sinh_vien', 'Không rõ')}
- Ngày sinh: {student_info.get('ngay_sinh', 'Không rõ')}
- Khóa học: {student_info.get('khoa_hoc', 'Không rõ')}
- Học kỳ hiện tại: {student_info.get('hoc_ky_hien_tai', 'Không rõ')}
- Tình trạng: {student_info.get('tinh_trang', 'Không rõ')}
"""
        
        system_prompt = f"""Bạn là trợ lý AI hỗ trợ sinh viên của Trường Đại học D.University. 
{student_section}
Nhiệm vụ của bạn là:
1. Trả lời các câu hỏi về thông tin học tập, điểm số, chương trình đào tạo của sinh viên
2. Đưa ra những định hướng, lời khuyên cho học kỳ sắp tới dựa trên kết quả học tập hiện tại
3. Giải thích các quy định học vụ khi được hỏi

Quy tắc trả lời:
- Luôn trả lời bằng tiếng Việt
- Xưng hô thân thiện, gọi sinh viên bằng tên (ví dụ: "Chào An", "An ơi")
- Dựa vào thông tin được cung cấp trong context để trả lời chính xác
- Nếu không có thông tin trong context, hãy nói rõ là bạn không có thông tin đó
- Khi đưa ra định hướng, hãy dựa vào điểm số và chương trình học để đề xuất phù hợp
- Thể hiện sự thân thiện và hỗ trợ sinh viên

Quy tắc định dạng (QUAN TRỌNG):
- Trả lời theo định dạng **Markdown** để dễ đọc
- Sử dụng **in đậm** cho thông tin quan trọng
- Sử dụng danh sách (- hoặc 1. 2. 3.) khi liệt kê nhiều mục
- Sử dụng bảng Markdown khi hiển thị điểm số nhiều môn
- Sử dụng heading (## hoặc ###) để phân chia các phần nếu câu trả lời dài
- Sử dụng > để trích dẫn hoặc nhấn mạnh lời khuyên quan trọng

Context thông tin:
---
{{context}}
---"""
        
        context_text = "\n\n".join(context)
        
        messages = [
            {"role": "system", "content": system_prompt.format(context=context_text)}
        ]
        
        # Them lich su hoi thoai neu co
        if conversation_history:
            for msg in conversation_history[-6:]:  # Chi lay 6 tin nhan gan nhat
                messages.append(msg)
        
        # Them cau hoi hien tai
        messages.append({"role": "user", "content": query})
        
        return messages
    
    def build_orientation_prompt(
        self,
        student_info: str,
        current_grades: str,
        next_semester_curriculum: str
    ) -> List[Dict[str, str]]:
        """
        Xay dung prompt de dua ra dinh huong hoc tap.
        
        Args:
            student_info: Thong tin sinh vien
            current_grades: Diem cac ky truoc
            next_semester_curriculum: Chuong trinh khung ky toi
            
        Returns:
            Danh sach messages de gui cho LLM
        """
        system_prompt = """Bạn là cố vấn học tập của Trường Đại học D.University.
Dựa vào thông tin sinh viên, kết quả học tập các kỳ trước và chương trình học kỳ tới,
hãy đưa ra những định hướng và lời khuyên cụ thể cho sinh viên.

Phân tích cần bao gồm:
1. Đánh giá tình hình học tập hiện tại (điểm mạnh, điểm yếu)
2. Các môn cần chú ý cải thiện
3. Chiến lược học tập cho kỳ tới
4. Các môn nên ưu tiên đăng ký (nếu có tự chọn)
5. Lời khuyên về cân bằng học tập

Thông tin sinh viên:
{student_info}

Kết quả học tập các kỳ trước:
{current_grades}

Chương trình học kỳ tới:
{next_semester_curriculum}
"""
        
        return [
            {
                "role": "system", 
                "content": system_prompt.format(
                    student_info=student_info,
                    current_grades=current_grades,
                    next_semester_curriculum=next_semester_curriculum
                )
            },
            {
                "role": "user",
                "content": "Hãy phân tích và đưa ra định hướng học tập cho tôi."
            }
        ]
