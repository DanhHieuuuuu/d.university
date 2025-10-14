const AVATAR_STORAGE_PREFIX = 'avatar_';

export const AvatarStorage = {
  saveAvatar: (maNhanSu: string, file: File): Promise<string> => {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      
      reader.onload = (e) => {
        const base64String = e.target?.result as string;
        try {
          localStorage.setItem(`${AVATAR_STORAGE_PREFIX}${maNhanSu}`, base64String);
          resolve(base64String);
        } catch (error) {
          reject(new Error('Không thể lưu ảnh đại diện'));
        }
      };
      
      reader.onerror = () => {
        reject(new Error('Không thể đọc file'));
      };
      
      reader.readAsDataURL(file);
    });
  },

  getAvatar: (maNhanSu: string): string | null => {
    try {
      return localStorage.getItem(`${AVATAR_STORAGE_PREFIX}${maNhanSu}`);
    } catch (error) {
      console.error('Error getting avatar:', error);
      return null;
    }
  },

  removeAvatar: (maNhanSu: string): void => {
    try {
      localStorage.removeItem(`${AVATAR_STORAGE_PREFIX}${maNhanSu}`);
    } catch (error) {
      console.error('Error removing avatar:', error);
    }
  },

  validateImageFile: (file: File): { valid: boolean; error?: string } => {
    const validTypes = ['image/jpeg', 'image/jpg', 'image/png', 'image/gif', 'image/webp'];
    const maxSize = 5 * 1024 * 1024; // 5MB

    if (!validTypes.includes(file.type)) {
      return {
        valid: false,
        error: 'Chỉ chấp nhận file ảnh định dạng JPG, PNG, GIF hoặc WebP'
      };
    }

    if (file.size > maxSize) {
      return {
        valid: false,
        error: 'Kích thước ảnh không được vượt quá 5MB'
      };
    }

    return { valid: true };
  }
};
