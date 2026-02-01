from typing import List, Dict, Any, Optional, Tuple

from app.services.chroma_vector_store import ChromaVectorStore
from app.services.groq_client import LLMClient
from app.services.query_rewriter import QueryRewriter


class RAGPipeline:
    """RAG Pipeline kết hợp retrieval và generation, hỗ trợ query rewriting."""
    
    def __init__(self, vector_store: ChromaVectorStore, llm_client: Optional[LLMClient] = None, top_k: int = 5):
        """Khởi tạo RAG pipeline.
        
        Args:
            vector_store: ChromaDB vector store
            llm_client: LLM client (có thể None, sẽ được set sau từ request)
            top_k: Số lượng kết quả tìm kiếm tối đa
        """
        self.vector_store = vector_store
        self.llm_client = llm_client
        self.top_k = top_k
        self.query_rewriter = QueryRewriter(llm_client) if llm_client else None
        self.student_info = None
    
    async def query(
        self,
        question: str,
        conversation_history: Optional[List[Dict]] = None,
        use_query_rewriting: bool = True
    ) -> Tuple[str, List[str], Optional[str]]:
        """Xử lý câu hỏi và trả về phản hồi từ LLM."""
        if not self.llm_client:
            raise ValueError("LLM client chưa được cấu hình. Vui lòng truyền llm_config trong request.")
        
        rewritten_query = None
        search_query = question
        
        if use_query_rewriting and conversation_history and self.query_rewriter:
            rewritten_query = await self.query_rewriter.rewrite_query(
                current_query=question,
                conversation_history=conversation_history
            )
            if rewritten_query and rewritten_query != question:
                search_query = rewritten_query
                print(f"[Query Rewrite] '{question}' -> '{rewritten_query}'")
        
        search_results = self.vector_store.search(search_query, top_k=self.top_k)
        relevant_results = [(chunk, score) for chunk, score in search_results if score > 0.3]
        contexts = [chunk["content"] for chunk, _ in relevant_results]
        
        messages = self.llm_client.build_rag_prompt(
            query=question,
            context=contexts,
            conversation_history=conversation_history,
            student_info=self.student_info
        )
        
        response = await self.llm_client.chat_completion(messages)
        return response, contexts, rewritten_query if rewritten_query != question else None
    
    async def get_study_orientation(self) -> str:
        """Lấy định hướng học tập cho sinh viên dựa trên dữ liệu hiện có."""
        if not self.llm_client:
            raise ValueError("LLM client chưa được cấu hình. Vui lòng truyền llm_config trong request.")
        
        student_results = self.vector_store.search("thông tin sinh viên", top_k=1)
        student_info = student_results[0][0]["content"] if student_results else ""
        
        grades_results = self.vector_store.search("điểm học kỳ kết quả học tập", top_k=3)
        grades_info = "\n\n".join([r[0]["content"] for r in grades_results])
        
        current_semester = 3
        next_semester_results = self.vector_store.search(
            f"chương trình khung học kỳ {current_semester}", top_k=1
        )
        next_curriculum = next_semester_results[0][0]["content"] if next_semester_results else ""
        
        messages = self.llm_client.build_orientation_prompt(
            student_info=student_info,
            current_grades=grades_info,
            next_semester_curriculum=next_curriculum
        )
        
        response = await self.llm_client.chat_completion(messages, temperature=0.5)
        return response
    
    def get_student_summary(self) -> Dict[str, Any]:
        """Lấy tóm tắt thông tin sinh viên."""
        summary = {}
        
        student_results = self.vector_store.search("thông tin sinh viên mã sinh viên", top_k=1)
        if student_results:
            summary["thong_tin_sinh_vien"] = student_results[0][0]["content"]
        
        academic_results = self.vector_store.search("GPA xếp loại học lực tình trạng", top_k=1)
        if academic_results:
            summary["tinh_trang_hoc_vu"] = academic_results[0][0]["content"]
        
        latest_grades = self.vector_store.search("kết quả học tập điểm trung bình", top_k=2)
        if latest_grades:
            summary["diem_cac_ky"] = [r[0]["content"] for r in latest_grades]
        
        return summary
