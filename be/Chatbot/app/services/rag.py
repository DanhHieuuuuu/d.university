import json
import os
from typing import List, Dict, Any, Optional, Tuple

from app.services.vector_store import VectorStore
from app.services.groq_client import GroqClient
from app.services.query_rewriter import QueryRewriter


class RAGPipeline:
    """
    RAG Pipeline ket hop retrieval va generation.
    Ho tro conversation memory va query rewriting.
    """
    
    def __init__(
        self, 
        vector_store: VectorStore, 
        groq_client: GroqClient, 
        top_k: int = 5,
        data_path: Optional[str] = None
    ):
        """
        Khoi tao RAG pipeline.
        
        Args:
            vector_store: Vector store de truy xuat
            groq_client: Client de goi LLM
            top_k: So luong document lay ra tu vector store
            data_path: Duong dan file JSON chua du lieu sinh vien
        """
        self.vector_store = vector_store
        self.groq_client = groq_client
        self.top_k = top_k
        self.query_rewriter = QueryRewriter(groq_client)
        
        # Tai thong tin sinh vien tu file JSON
        self.student_info = None
        if data_path and os.path.exists(data_path):
            self._load_student_info(data_path)
    
    def _load_student_info(self, data_path: str):
        """Tai thong tin sinh vien tu file JSON."""
        try:
            with open(data_path, 'r', encoding='utf-8') as f:
                data = json.load(f)
                if 'sinh_vien' in data:
                    self.student_info = data['sinh_vien']
                    print(f"Da tai thong tin sinh vien: {self.student_info.get('ho_ten', 'Unknown')}")
        except Exception as e:
            print(f"Loi khi tai thong tin sinh vien: {e}")
    
    async def query(
        self,
        question: str,
        conversation_history: Optional[List[Dict]] = None,
        use_query_rewriting: bool = True
    ) -> Tuple[str, List[str], Optional[str]]:
        """
        Xu ly cau hoi va tra ve phan hoi.
        
        Args:
            question: Cau hoi cua nguoi dung
            conversation_history: Lich su hoi thoai
            use_query_rewriting: Co su dung query rewriting hay khong
            
        Returns:
            Tuple (phan_hoi, danh_sach_context_da_dung, cau_hoi_da_viet_lai)
        """
        rewritten_query = None
        search_query = question
        
        # Buoc 1: Viet lai cau hoi neu can (cho follow-up questions)
        if use_query_rewriting and conversation_history:
            rewritten_query = await self.query_rewriter.rewrite_query(
                current_query=question,
                conversation_history=conversation_history
            )
            # Chi dung cau hoi da viet lai neu khac cau goc
            if rewritten_query and rewritten_query != question:
                search_query = rewritten_query
                print(f"[Query Rewrite] '{question}' -> '{rewritten_query}'")
        
        # Buoc 2: Truy xuat context tu vector store (dung cau hoi da viet lai)
        search_results = self.vector_store.search(search_query, top_k=self.top_k)
        
        # Loc cac ket qua co diem tuong dong thap
        relevant_results = [(chunk, score) for chunk, score in search_results if score > 0.3]
        
        # Lay noi dung cac chunk
        contexts = [chunk["content"] for chunk, _ in relevant_results]
        
        # Buoc 3: Xay dung prompt (dung cau hoi goc de hoi thoai tu nhien)
        messages = self.groq_client.build_rag_prompt(
            query=question,  # Dung cau hoi goc
            context=contexts,
            conversation_history=conversation_history,
            student_info=self.student_info  # Truyen thong tin sinh vien
        )
        
        # Buoc 4: Goi LLM de sinh phan hoi
        response = await self.groq_client.chat_completion(messages)
        
        return response, contexts, rewritten_query if rewritten_query != question else None
    
    async def get_study_orientation(self) -> str:
        """
        Lay dinh huong hoc tap cho sinh vien dua tren du lieu hien co.
        
        Returns:
            Phan hoi dinh huong tu LLM
        """
        # Tim kiem thong tin sinh vien
        student_results = self.vector_store.search("thông tin sinh viên", top_k=1)
        student_info = student_results[0][0]["content"] if student_results else ""
        
        # Tim kiem diem cac ky
        grades_results = self.vector_store.search("điểm học kỳ kết quả học tập", top_k=3)
        grades_info = "\n\n".join([r[0]["content"] for r in grades_results])
        
        # Tim kiem chuong trinh ky toi (ky 3 -> tim ky 3)
        # Lay hoc ky hien tai tu thong tin sinh vien va tim ky tiep theo
        current_semester = 3  # Mac dinh
        next_semester_results = self.vector_store.search(
            f"chương trình khung học kỳ {current_semester}", 
            top_k=1
        )
        next_curriculum = next_semester_results[0][0]["content"] if next_semester_results else ""
        
        # Xay dung prompt va goi LLM
        messages = self.groq_client.build_orientation_prompt(
            student_info=student_info,
            current_grades=grades_info,
            next_semester_curriculum=next_curriculum
        )
        
        response = await self.groq_client.chat_completion(messages, temperature=0.5)
        return response
    
    def get_student_summary(self) -> Dict[str, Any]:
        """
        Lay tom tat thong tin sinh vien.
        
        Returns:
            Dict chua thong tin tom tat
        """
        summary = {}
        
        # Tim thong tin sinh vien
        student_results = self.vector_store.search("thông tin sinh viên mã sinh viên", top_k=1)
        if student_results:
            summary["thong_tin_sinh_vien"] = student_results[0][0]["content"]
        
        # Tim thong tin hoc vu
        academic_results = self.vector_store.search("GPA xếp loại học lực tình trạng", top_k=1)
        if academic_results:
            summary["tinh_trang_hoc_vu"] = academic_results[0][0]["content"]
        
        # Tim diem ky gan nhat
        latest_grades = self.vector_store.search("kết quả học tập điểm trung bình", top_k=2)
        if latest_grades:
            summary["diem_cac_ky"] = [r[0]["content"] for r in latest_grades]
        
        return summary
