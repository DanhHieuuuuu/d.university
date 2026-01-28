"""
Service để quản lý dữ liệu sinh viên trong ChromaDB.
Hỗ trợ sync từ .NET API và truy vấn theo mssv.
"""
import json
from typing import List, Dict, Any, Optional
from app.services.embedding import EmbeddingService
from app.services.chroma_vector_store import ChromaVectorStore


class StudentDataService:
    """Service quản lý dữ liệu sinh viên trong ChromaDB."""
    
    def __init__(
        self,
        embedding_service: EmbeddingService,
        persist_directory: str,
        collection_name: str = "student_data"
    ):
        self.embedding_service = embedding_service
        self.persist_directory = persist_directory
        self.collection_name = collection_name
        self.vector_store = ChromaVectorStore(
            embedding_service=embedding_service,
            persist_directory=persist_directory,
            collection_name=collection_name
        )
    
    def _convert_student_to_chunks(self, mssv: str, data: Dict[str, Any]) -> List[Dict[str, Any]]:
        """
        Chuyển đổi thông tin sinh viên thành các chunks để lưu vào ChromaDB.
        Hỗ trợ cả camelCase và snake_case từ .NET API.
        """
        chunks = []
        
        # Helper function để lấy giá trị với cả 2 naming convention
        def get_val(obj, camel_key, snake_key=None, default=''):
            if not obj:
                return default
            if snake_key is None:
                # Auto convert camelCase to snake_case
                snake_key = ''.join(['_' + c.lower() if c.isupper() else c for c in camel_key]).lstrip('_')
            return obj.get(camel_key, obj.get(snake_key, default))
        
        def get_obj(obj, camel_key, snake_key=None):
            if not obj:
                return {}
            if snake_key is None:
                snake_key = ''.join(['_' + c.lower() if c.isupper() else c for c in camel_key]).lstrip('_')
            return obj.get(camel_key, obj.get(snake_key, {}))
        
        def get_list(obj, camel_key, snake_key=None):
            if not obj:
                return []
            if snake_key is None:
                snake_key = ''.join(['_' + c.lower() if c.isupper() else c for c in camel_key]).lstrip('_')
            return obj.get(camel_key, obj.get(snake_key, []))
        
        # 1. Thông tin sinh viên cơ bản
        sinh_vien = get_obj(data, "sinhVien", "sinh_vien")
        if sinh_vien:
            content = f"""Thông tin sinh viên:
- Mã sinh viên: {get_val(sinh_vien, 'maSinhVien', 'ma_sinh_vien', mssv)}
- Họ tên: {get_val(sinh_vien, 'hoTen', 'ho_ten')}
- Ngày sinh: {get_val(sinh_vien, 'ngaySinh', 'ngay_sinh')}
- Giới tính: {get_val(sinh_vien, 'gioiTinh', 'gioi_tinh')}
- Email: {get_val(sinh_vien, 'email')}
- Số điện thoại: {get_val(sinh_vien, 'soDienThoai', 'so_dien_thoai')}
- Địa chỉ: {get_val(sinh_vien, 'diaChi', 'dia_chi')}
- Khóa học: {get_val(sinh_vien, 'khoaHoc', 'khoa_hoc')}
- Học kỳ hiện tại: {get_val(sinh_vien, 'hocKyHienTai', 'hoc_ky_hien_tai')}
- Tình trạng: {get_val(sinh_vien, 'tinhTrang', 'tinh_trang')}"""
            chunks.append({
                "content": content,
                "type": "sinh_vien",
                "mssv": mssv
            })
        
        # 2. Thông tin khoa
        khoa = get_obj(data, "khoa")
        if khoa:
            content = f"""Thông tin khoa của sinh viên {mssv}:
- Mã khoa: {get_val(khoa, 'maKhoa', 'ma_khoa')}
- Tên khoa: {get_val(khoa, 'tenKhoa', 'ten_khoa')}
- Mô tả: {get_val(khoa, 'moTa', 'mo_ta')}"""
            chunks.append({
                "content": content,
                "type": "khoa",
                "mssv": mssv
            })
        
        # 3. Thông tin ngành
        nganh = get_obj(data, "nganh")
        if nganh:
            content = f"""Thông tin ngành học của sinh viên {mssv}:
- Mã ngành: {get_val(nganh, 'maNganh', 'ma_nganh')}
- Tên ngành: {get_val(nganh, 'tenNganh', 'ten_nganh')}
- Số tín chỉ tối thiểu: {get_val(nganh, 'soTinChiToiThieu', 'so_tin_chi_toi_thieu')}
- Thời gian đào tạo: {get_val(nganh, 'thoiGianDaoTao', 'thoi_gian_dao_tao')}
- Mô tả: {get_val(nganh, 'moTa', 'mo_ta')}"""
            chunks.append({
                "content": content,
                "type": "nganh",
                "mssv": mssv
            })
        
        # 4. Chuyên ngành
        chuyen_nganh = get_obj(data, "chuyenNganh", "chuyen_nganh")
        if chuyen_nganh:
            content = f"""Thông tin chuyên ngành của sinh viên {mssv}:
- Mã chuyên ngành: {get_val(chuyen_nganh, 'maChuyenNganh', 'ma_chuyen_nganh')}
- Tên chuyên ngành: {get_val(chuyen_nganh, 'tenChuyenNganh', 'ten_chuyen_nganh')}
- Mô tả: {get_val(chuyen_nganh, 'moTa', 'mo_ta')}"""
            chunks.append({
                "content": content,
                "type": "chuyen_nganh",
                "mssv": mssv
            })
        
        # 5. Chương trình khung
        ctk = get_list(data, "chuongTrinhKhung", "chuong_trinh_khung")
        for ky in ctk:
            hoc_ky = get_val(ky, "hocKy", "hoc_ky")
            mon_hoc_list = get_list(ky, "monHoc", "mon_hoc")
            if mon_hoc_list:
                mon_hoc_str = "\n".join([
                    f"  + {get_val(mh, 'maMon', 'ma_mon')}: {get_val(mh, 'tenMon', 'ten_mon')} ({get_val(mh, 'soTinChi', 'so_tin_chi')} tín chỉ, {get_val(mh, 'loai')})"
                    for mh in mon_hoc_list
                ])
                content = f"""Chương trình khung học kỳ {hoc_ky} của sinh viên {mssv}:
{mon_hoc_str}"""
                chunks.append({
                    "content": content,
                    "type": "chuong_trinh_khung",
                    "mssv": mssv,
                    "hoc_ky": str(hoc_ky)
                })
        
        # 6. Điểm các kỳ
        diem_cac_ky = get_list(data, "diemCacKy", "diem_cac_ky")
        for ky in diem_cac_ky:
            hoc_ky = get_val(ky, "hocKy", "hoc_ky")
            nam_hoc = get_val(ky, "namHoc", "nam_hoc")
            diem_mon = get_list(ky, "diemMon", "diem_mon")
            
            if diem_mon:
                diem_str = "\n".join([
                    f"  + {get_val(dm, 'maMon', 'ma_mon')}: {get_val(dm, 'tenMon', 'ten_mon')} - Điểm QT: {get_val(dm, 'diemQuaTrinh', 'diem_qua_trinh')}, Điểm CK: {get_val(dm, 'diemCuoiKy', 'diem_cuoi_ky')}, Tổng kết: {get_val(dm, 'diemTongKet', 'diem_tong_ket')} ({get_val(dm, 'diemChu', 'diem_chu')})"
                    for dm in diem_mon
                ])
                content = f"""Kết quả học tập học kỳ {hoc_ky} năm học {nam_hoc} của sinh viên {mssv}:
{diem_str}
- Điểm trung bình học kỳ: {get_val(ky, 'diemTrungBinhHocKy', 'diem_trung_binh_hoc_ky')}
- Điểm trung bình tích lũy: {get_val(ky, 'diemTrungBinhTichLuy', 'diem_trung_binh_tich_luy')}
- Số tín chỉ đạt: {get_val(ky, 'soTinChiDat', 'so_tin_chi_dat')}
- Số tín chỉ tích lũy: {get_val(ky, 'soTinChiTichLuy', 'so_tin_chi_tich_luy')}
- Xếp loại học kỳ: {get_val(ky, 'xepLoaiHocKy', 'xep_loai_hoc_ky')}"""
                chunks.append({
                    "content": content,
                    "type": "diem",
                    "mssv": mssv,
                    "hoc_ky": str(hoc_ky),
                    "nam_hoc": nam_hoc
                })
        
        # 7. Thông tin học vụ
        hoc_vu = get_obj(data, "thongTinHocVu", "thong_tin_hoc_vu")
        if hoc_vu:
            content = f"""Thông tin học vụ của sinh viên {mssv}:
- GPA hiện tại: {get_val(hoc_vu, 'gpaHienTai', 'gpa_hien_tai')}
- Xếp loại học lực: {get_val(hoc_vu, 'xepLoaiHocLuc', 'xep_loai_hoc_luc')}
- Số môn nợ: {get_val(hoc_vu, 'soMonNo', 'so_mon_no')}
- Cảnh báo học vụ: {'Có' if hoc_vu.get('canhBaoHocVu', hoc_vu.get('canh_bao_hoc_vu', False)) else 'Không'}"""
            chunks.append({
                "content": content,
                "type": "hoc_vu",
                "mssv": mssv
            })
        
        return chunks
    
    def sync_students(self, students: List[Dict[str, Any]], clear_all: bool = True) -> Dict[str, Any]:
        """
        Sync danh sách sinh viên vào ChromaDB.
        
        Args:
            students: List[{"mssv": str, "data": dict}]
            clear_all: True để xóa toàn bộ dữ liệu cũ trước khi sync
            
        Returns:
            Dict với thông tin kết quả sync
        """
        # Xóa toàn bộ dữ liệu cũ nếu clear_all = True
        if clear_all:
            print("Đang xóa toàn bộ dữ liệu cũ trong ChromaDB...")
            self.vector_store.delete_all()
            print("Đã xóa xong dữ liệu cũ")
        
        all_chunks = []
        
        for student in students:
            mssv = student.get("mssv", "")
            data = student.get("data", {})
            
            if mssv and data:
                # Tạo chunks mới
                chunks = self._convert_student_to_chunks(mssv, data)
                all_chunks.extend(chunks)
        
        # Thêm tất cả chunks vào ChromaDB
        if all_chunks:
            self._add_chunks_to_chroma(all_chunks)
        
        return {
            "success": True,
            "message": f"Đã xóa dữ liệu cũ và sync {len(students)} sinh viên với {len(all_chunks)} chunks",
            "total_students": len(students),
            "total_chunks": len(all_chunks)
        }
    
    def _delete_student_data(self, mssv: str):
        """Xóa tất cả dữ liệu của một sinh viên."""
        try:
            # Lấy tất cả IDs của sinh viên này
            results = self.vector_store.collection.get(
                where={"mssv": mssv},
                include=[]
            )
            if results and results["ids"]:
                self.vector_store.collection.delete(ids=results["ids"])
                print(f"Đã xóa {len(results['ids'])} documents của sinh viên {mssv}")
        except Exception as e:
            print(f"Lỗi khi xóa dữ liệu sinh viên {mssv}: {e}")
    
    def _add_chunks_to_chroma(self, chunks: List[Dict[str, Any]]):
        """Thêm chunks vào ChromaDB với metadata."""
        import uuid
        
        texts = [chunk["content"] for chunk in chunks]
        embeddings = self.embedding_service.embed_texts(texts).tolist()
        
        ids = [f"{chunk.get('mssv', 'unknown')}_{chunk.get('type', 'unknown')}_{uuid.uuid4().hex[:8]}" for chunk in chunks]
        
        metadatas = []
        for chunk in chunks:
            metadata = {
                "type": chunk.get("type", "unknown"),
                "mssv": chunk.get("mssv", ""),
            }
            if "hoc_ky" in chunk:
                metadata["hoc_ky"] = chunk["hoc_ky"]
            if "nam_hoc" in chunk:
                metadata["nam_hoc"] = chunk["nam_hoc"]
            metadatas.append(metadata)
        
        self.vector_store.collection.add(
            ids=ids,
            embeddings=embeddings,
            documents=texts,
            metadatas=metadatas
        )
        
        print(f"Đã thêm {len(chunks)} chunks vào ChromaDB")
    
    def search_by_mssv(
        self,
        query: str,
        mssv: str,
        top_k: int = 5
    ) -> List[Dict[str, Any]]:
        """
        Tìm kiếm thông tin liên quan đến một sinh viên cụ thể.
        
        Args:
            query: Câu hỏi
            mssv: Mã số sinh viên
            top_k: Số lượng kết quả
            
        Returns:
            Danh sách kết quả
        """
        return self.vector_store.search(
            query=query,
            top_k=top_k,
            where={"mssv": mssv}
        )
    
    def get_student_info(self, mssv: str) -> Optional[Dict[str, Any]]:
        """
        Lấy thông tin cơ bản của sinh viên theo mssv.
        """
        results = self.vector_store.search(
            query=f"thông tin sinh viên {mssv}",
            top_k=1,
            where={"$and": [{"mssv": mssv}, {"type": "sinh_vien"}]}
        )
        if results:
            return results[0][0]
        return None
