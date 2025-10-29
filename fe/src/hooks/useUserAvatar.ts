import { useState, useEffect, useCallback } from 'react';
import { ImageService } from '@services/image.service';

const DEFAULT_AVATAR = 'anh_vo_danh.jpg';

export const useUserAvatar = (imageLink?: string | null) => {
  const [avatarUrl, setAvatarUrl] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<Error | null>(null);
  const [refreshTrigger, setRefreshTrigger] = useState(0);

  const refresh = useCallback(() => {
    setRefreshTrigger((prev) => prev + 1);
  }, []);

  const updateAvatar = useCallback(
    (blob: Blob) => {
      // Revoke URL cũ nếu có
      if (avatarUrl) {
        URL.revokeObjectURL(avatarUrl);
      }
      // Tạo URL mới từ blob
      const url = URL.createObjectURL(blob);
      setAvatarUrl(url);
    },
    [avatarUrl]
  );

  useEffect(() => {
    let objectUrl: string | null = null;

    const fetchAvatar = async () => {
      try {
        setLoading(true);
        setError(null);

        // Sử dụng tên file từ imageLink, nếu null thì dùng ảnh mặc định
        const fileName = imageLink || DEFAULT_AVATAR;

        // Nếu refreshTrigger > 0, bypass cache để lấy ảnh mới
        const bustCache = refreshTrigger > 0;
        const blob = await ImageService.getImageFromS3(fileName, bustCache);
        const url = URL.createObjectURL(blob);
        objectUrl = url;
        setAvatarUrl(url);
      } catch (err) {
        console.error('Error loading avatar:', err);
        setError(err as Error);
        setAvatarUrl(null);
      } finally {
        setLoading(false);
      }
    };

    fetchAvatar();

    // Cleanup function để revoke object URL
    return () => {
      if (objectUrl) {
        URL.revokeObjectURL(objectUrl);
      }
    };
  }, [imageLink, refreshTrigger]);

  return { avatarUrl, loading, error, refresh, updateAvatar };
};
