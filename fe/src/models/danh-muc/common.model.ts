export type IViewDanToc = {
  id: number;
  tenDanToc: string;
};

export type IViewGioiTinh = {
  id: number;
  tenGioiTinh: string;
};

export type IViewLoaiHopDong = {
  id: number;
  maLoaiHopDong: string;
  tenLoaiHopDong: string;
  idBieuMau: number | null;
};

export type IViewLoaiPhongBan = {
  id: number;
  tenLoaiPhongBan: string;
};

export type IViewQuanHeGiaDinh = {
  id: number;
  maQuanHe: string;
  tenQuanHe: string;
};

export type IViewQuocTich = {
  id: number;
  tenQuocGia: string;
  stt: number | null;
};

export type IViewTonGiao = {
  id: number;
  tenTonGiao: string;
};
