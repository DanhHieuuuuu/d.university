export type ICreateNhanSu = {
  maNhanSu?: string;
  hoDem?: string;
  ten?: string;
  
  thongTinGiaDinh?: ICreateNsQuanHe[]
}

export type ICreateNsQuanHe = {
  quanHe?: number;
  hoTen?: string;
  ngaySinh?: Date;
  queQuan?: string;
  quocTich?: number;
  soDienThoai?: string;
  ngheNghiep?: string;
  donViCongTac?: string;
}