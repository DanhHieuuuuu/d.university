// @utils/format.ts

export const toKebabCase = (str: string): string => {
  if (!str) return '';

  return str
    .normalize('NFD')
    .replace(/[\u0300-\u036f]/g, '')
    .replace(/đ/g, 'd')
    .replace(/Đ/g, 'D')
    .replace(/[ăâàáảãạ]/g, 'a')
    .replace(/[ĂÂÀÁẢÃẠ]/g, 'A')
    .replace(/[êèéẻẽẹ]/g, 'e')
    .replace(/[ÊÈÉẺẼẸ]/g, 'E')
    .replace(/[ôơòóỏõọ]/g, 'o')
    .replace(/[ÔƠÒÓỎÕỌ]/g, 'O')
    .replace(/[ưùúủũụ]/g, 'u')
    .replace(/[ƯÙÚỦŨỤ]/g, 'U')
    .replace(/[ìíỉĩị]/g, 'i')
    .replace(/[ÌÍỈĨỊ]/g, 'I')
    .replace(/[ỳýỷỹỵ]/g, 'y')
    .replace(/[ỲÝỶỸỴ]/g, 'Y')
    .toLowerCase()
    .trim()
    .replace(/\s+/g, '-');
};

export const generateUsername = (hoDem: string | null, ten: string | null): string => {
  if (!hoDem || !ten) return '';

  const cleanedTen = toKebabCase(ten);
  const hoDemParts = hoDem.split(' ').filter(Boolean);
  const vietTatHoDem = hoDemParts.map((part) => toKebabCase(part).charAt(0)).join('');

  return `${cleanedTen}${vietTatHoDem}`.toLowerCase();
};

export const generateChucVuAbbreviation = (name: string | null): string => {
  if (!name) return '';

  const parts = name.split(' ').filter(Boolean);
  const abbreviation = parts
    .map((part) => toKebabCase(part).charAt(0))
    .join('')
    .toLowerCase();

  if (abbreviation.length === 1 && name.toLowerCase().includes('giám đốc')) {
    return 'gd';
  }

  return abbreviation;
};

export const generateHuceEmail = (hoDem: string | null, ten: string | null, tenChucVu: string | null): string => {
  const username = generateUsername(hoDem, ten);
  const chucVuAbbr = generateChucVuAbbreviation(tenChucVu);

  if (!username || !chucVuAbbr) return '';

  return `${username}@${chucVuAbbr}.huce.edu.vn`.toLowerCase();
};

export const removeVietnameseTones = (str: string) => {
  return str
    .normalize("NFD")
    .replace(/[\u0300-\u036f]/g, "")
    .replace(/đ/g, "d")
    .replace(/Đ/g, "D")
    .toLowerCase();
};
