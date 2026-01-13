"""
Module chunking du lieu JSON thanh cac doan van ban nho hon.
Phu hop cho tieng Viet va du lieu co cau truc.
"""

import json
from typing import List, Dict, Any

class SemanticChunker:
    """
    Chunker theo ngu nghia - chia du lieu JSON thanh cac chunk co y nghia.
    Khong chia cat giua cac truong du lieu quan trong.
    """
    
    def __init__(self, chunk_size: int = 500, overlap: int = 50):
        self.chunk_size = chunk_size
        self.overlap = overlap
    
    def chunk_json_data(self, data: Dict[str, Any]) -> List[Dict[str, Any]]:
        """
        Chia du lieu JSON thanh cac chunk co ngu nghia.
        Moi chunk bao gom metadata va noi dung.
        """
        chunks = []
        
        # Chunk thong tin sinh vien
        if "sinh_vien" in data:
            chunks.append({
                "type": "thong_tin_sinh_vien",
                "content": self._format_student_info(data["sinh_vien"]),
                "metadata": {"section": "sinh_vien"}
            })
        
        # Chunk thong tin khoa
        if "khoa" in data:
            chunks.append({
                "type": "thong_tin_khoa",
                "content": self._format_faculty_info(data["khoa"]),
                "metadata": {"section": "khoa"}
            })
        
        # Chunk thong tin nganh
        if "nganh" in data:
            chunks.append({
                "type": "thong_tin_nganh",
                "content": self._format_major_info(data["nganh"]),
                "metadata": {"section": "nganh"}
            })
        
        # Chunk chuong trinh khung theo tung ky
        if "chuong_trinh_khung" in data:
            for semester in data["chuong_trinh_khung"]:
                chunks.append({
                    "type": "chuong_trinh_khung",
                    "content": self._format_curriculum(semester),
                    "metadata": {"section": "chuong_trinh_khung", "ky": semester.get("ky")}
                })
        
        # Chunk diem theo tung ky
        if "diem_cac_ky" in data:
            for semester_grades in data["diem_cac_ky"]:
                chunks.append({
                    "type": "diem_hoc_ky",
                    "content": self._format_grades(semester_grades),
                    "metadata": {"section": "diem", "ky": semester_grades.get("hoc_ky")}
                })
        
        # Chunk thong tin hoc vu
        if "thong_tin_hoc_vu" in data:
            chunks.append({
                "type": "thong_tin_hoc_vu",
                "content": self._format_academic_status(data["thong_tin_hoc_vu"]),
                "metadata": {"section": "hoc_vu"}
            })
        
        # Chunk quy dinh thang diem
        if "quy_dinh_thang_diem" in data:
            chunks.append({
                "type": "quy_dinh_thang_diem",
                "content": self._format_grading_rules(data["quy_dinh_thang_diem"]),
                "metadata": {"section": "quy_dinh"}
            })
        
        return chunks
    
    def _format_student_info(self, student: Dict) -> str:
        """Format thong tin sinh vien thanh van ban."""
        return f"""Thông tin sinh viên:
- Mã sinh viên: {student.get('ma_sinh_vien', 'N/A')}
- Họ tên: {student.get('ho_ten', 'N/A')}
- Ngày sinh: {student.get('ngay_sinh', 'N/A')}
- Giới tính: {student.get('gioi_tinh', 'N/A')}
- Email: {student.get('email', 'N/A')}
- Số điện thoại: {student.get('so_dien_thoai', 'N/A')}
- Địa chỉ: {student.get('dia_chi', 'N/A')}
- Khóa học: {student.get('khoa_hoc', 'N/A')}
- Học kỳ hiện tại: {student.get('hoc_ky_hien_tai', 'N/A')}
- Tình trạng: {student.get('tinh_trang', 'N/A')}"""
    
    def _format_faculty_info(self, faculty: Dict) -> str:
        """Format thong tin khoa thanh van ban."""
        return f"""Thông tin Khoa:
- Mã khoa: {faculty.get('ma_khoa', 'N/A')}
- Tên khoa: {faculty.get('ten_khoa', 'N/A')}
- Mô tả: {faculty.get('mo_ta', 'N/A')}"""
    
    def _format_major_info(self, major: Dict) -> str:
        """Format thong tin nganh thanh van ban."""
        return f"""Thông tin Ngành học:
- Mã ngành: {major.get('ma_nganh', 'N/A')}
- Tên ngành: {major.get('ten_nganh', 'N/A')}
- Số tín chỉ tối thiểu: {major.get('so_tin_chi_toi_thieu', 'N/A')}
- Thời gian đào tạo: {major.get('thoi_gian_dao_tao', 'N/A')}
- Mô tả: {major.get('mo_ta', 'N/A')}"""
    
    def _format_curriculum(self, semester: Dict) -> str:
        """Format chuong trinh khung cua mot ky thanh van ban."""
        ky = semester.get('ky', 'N/A')
        subjects = semester.get('mon_hoc', [])
        
        subjects_text = ""
        total_credits = 0
        for subject in subjects:
            credits = subject.get('so_tin_chi', 0)
            total_credits += credits
            subjects_text += f"\n  + {subject.get('ten_mon', 'N/A')} ({subject.get('ma_mon', 'N/A')}): {credits} tín chỉ - {subject.get('loai', 'N/A')}"
        
        return f"""Chương trình khung Học kỳ {ky}:
- Tổng số môn: {len(subjects)}
- Tổng số tín chỉ: {total_credits}
- Danh sách môn học:{subjects_text}"""
    
    def _format_grades(self, semester_grades: Dict) -> str:
        """Format diem cua mot ky thanh van ban."""
        ky = semester_grades.get('hoc_ky', 'N/A')
        nam_hoc = semester_grades.get('nam_hoc', 'N/A')
        subjects = semester_grades.get('diem_mon', [])
        
        grades_text = ""
        for subject in subjects:
            grades_text += f"\n  + {subject.get('ten_mon', 'N/A')}: Điểm tổng kết {subject.get('diem_tong_ket', 'N/A')} ({subject.get('diem_chu', 'N/A')}) - {subject.get('ket_qua', 'N/A')}"
        
        return f"""Kết quả học tập Học kỳ {ky} - Năm học {nam_hoc}:
- Điểm trung bình học kỳ: {semester_grades.get('diem_trung_binh_hoc_ky', 'N/A')}
- Điểm trung bình tích lũy: {semester_grades.get('diem_trung_binh_tich_luy', 'N/A')}
- Số tín chỉ đạt: {semester_grades.get('so_tin_chi_dat', 'N/A')}
- Số tín chỉ tích lũy: {semester_grades.get('so_tin_chi_tich_luy', 'N/A')}
- Xếp loại học kỳ: {semester_grades.get('xep_loai_hoc_ky', 'N/A')}
- Chi tiết điểm các môn:{grades_text}"""
    
    def _format_academic_status(self, status: Dict) -> str:
        """Format tinh trang hoc vu thanh van ban."""
        canh_bao = "Có" if status.get('canh_bao_hoc_vu', False) else "Không"
        return f"""Tình trạng học vụ hiện tại:
- GPA hiện tại: {status.get('gpa_hien_tai', 'N/A')}
- Xếp loại học lực: {status.get('xep_loai_hoc_luc', 'N/A')}
- Số môn nợ: {status.get('so_mon_no', 0)}
- Cảnh báo học vụ: {canh_bao}"""
    
    def _format_grading_rules(self, rules: Dict) -> str:
        """Format quy dinh thang diem thanh van ban."""
        rules_text = "Quy định thang điểm:\n"
        for grade, info in rules.items():
            rules_text += f"- {grade}: Từ {info.get('diem_so_min', 0)} đến {info.get('diem_so_max', 0)} điểm, hệ số 4: {info.get('he_so_4', 0)}\n"
        return rules_text


def load_and_chunk_data(file_path: str) -> List[Dict[str, Any]]:
    """Load du lieu JSON va chia thanh cac chunk."""
    with open(file_path, 'r', encoding='utf-8') as f:
        data = json.load(f)
    
    chunker = SemanticChunker()
    return chunker.chunk_json_data(data)
