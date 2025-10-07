// @utils/format.ts

/**
 * Chuyển chuỗi có dấu về không dấu, chữ thường và loại bỏ khoảng trắng.
 * Ví dụ: "Phòng Hành Chính" -> "phong-hanh-chinh"
 */
export const toKebabCase = (str: string): string => {
  if (!str) return '';
  return str
    .normalize('NFD') // Tách các ký tự có dấu
    .replace(/[\u0300-\u036f]/g, '') 
    .toLowerCase()
    .trim()
    .replace(/\s+/g, '-'); 
};


export const generateUsername = (hoDem: string | null, ten: string | null): string => {
  if (!hoDem || !ten) return '';

  const cleanedTen = toKebabCase(ten);
  // Lấy chữ cái đầu của từng từ trong Họ Đệm
  const hoDemParts = hoDem.split(' ').filter(p => p.trim() !== '');
  const vietTatHoDem = hoDemParts.map(part => toKebabCase(part).charAt(0)).join('');

  return `${cleanedTen}${vietTatHoDem}`;
};

export const generateChucVuAbbreviation = (name: string | null): string => {
  if (!name) return '';

  // Chỉ lấy ký tự đầu của mỗi từ, chuyển thành chữ thường, không dấu
  const parts = name.split(' ').filter(p => p.trim() !== '');
  const abbreviation = parts.map(part => toKebabCase(part).charAt(0)).join('');

  // Quy tắc tùy chỉnh: nếu chỉ có 1 ký tự, có thể thêm quy tắc riêng
  if (abbreviation.length === 1 && name.toLowerCase().includes('giám đốc')) {
      return 'gd'; // Trường hợp đặc biệt
  }

  return abbreviation;
};

/**
 Format: tên + viết tắt họ + "@" + viết tắt chức vụ + ".huce.edu.vn"
 Ví dụ: "thanhpd@gd.huce.edu.vn"
 */
export const generateHuceEmail = (hoDem: string | null, ten: string | null, tenChucVu: string | null): string => {
  const username = generateUsername(hoDem, ten);
  
  // Lấy viết tắt chức vụ (ví dụ: Giám đốc -> gd)
  const chucVuAbbr = generateChucVuAbbreviation(tenChucVu);
  
  if (!username || !chucVuAbbr) {
    return '';
  }

  return `${username}@${chucVuAbbr}.huce.edu.vn`;
};