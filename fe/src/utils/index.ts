import { format } from 'date-fns';
import { isAxiosError } from 'axios';
import { toast } from 'react-toastify';
import { IPermission } from '@models/permission';

export const formatDate = (date: Date | string | number, type: string = 'dd/MM/yyyy') => {
  try {
    return format(date, type);
  } catch (error) {
    // console.error(error)
    return '';
  }
};

export const formatDateCallApi = (date: Date | string | number, type: string = 'yyyy-MM-dd') => {
  if (date) {
    return formatDate(date, type);
  }
  return undefined;
};

export const formatCurrency = (amount: number | string | null | undefined): string => {
  if (amount === null || amount === undefined || !amount) return '';

  const numericAmount = typeof amount === 'string' ? parseFloat(amount) : amount;
  if (isNaN(numericAmount)) return '';

  return numericAmount.toLocaleString('vi-VN');
};

export const formatDateView = (date: Date | string | number, type: string = 'dd/MM/yyyy') => formatDate(date, type);

export const processApiMsgError = (err: any, msgError?: string | null) => {
  let msg = '';

  if (isAxiosError(err)) {
    // Kiểm tra xem có phản hồi từ server không
    if (err.response) {
      msg = err.response.data.message || msgError || 'Đã xảy ra lỗi, vui lòng thử lại.';
    } else if (err.request) {
      // Trường hợp request đã được gửi nhưng không nhận được phản hồi
      msg = 'Không thể kết nối tới server, vui lòng kiểm tra kết nối mạng.';
    } else {
      // Các lỗi khác trong quá trình tạo request
      msg = 'Đã xảy ra lỗi, vui lòng thử lại.';
    }
    // setError(msg)
    toast.error(msg);
  } else {
    // Lỗi không phải từ Axios
    msg = 'Đã xảy ra lỗi không xác định, vui lòng thử lại.';
    // setError(msg)
    toast.error(msg);
  }
};

export const _parseCssString = (cssString: string) => {
  return cssString.split(';').reduce((styles: any, rule) => {
    if (rule.trim() === '') return styles;
    const [property, value] = rule.split(':');
    if (property && value) {
      const jsProperty = property.trim().replace(/-([a-z])/g, (_, char) => char.toUpperCase()); // Convert to camelCase
      styles[jsProperty] = value.trim();
    }
    return styles;
  }, {});
};

export const _isGranted = (permissions: IPermission[], permission: string) => {
  if (!permissions || !permissions.length) {
    return false;
  }
  const permissionNames = permissions.map((x) => x.name);
  return permissionNames.includes(permission);
};

export const _toUpperUnderscore = (text: string): string => {
  if (!text) {
    return text;
  }
  return text
    .normalize('NFD') // separate letters from accents
    .replace(/[\u0300-\u036f]/g, '') // remove accents
    .replace(/đ/g, 'd') // lowercase đ
    .replace(/Đ/g, 'D') // uppercase Đ
    .toUpperCase()
    .replace(/\s+/g, '_'); // replace spaces with _
};
