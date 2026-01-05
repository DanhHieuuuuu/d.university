import { useState, useEffect, useCallback } from 'react';
import { FileService } from '@services/file.service';

export const useUserAvatar = (imageLink?: string | null) => {
  const [avatarUrl, setAvatarUrl] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<Error | null>(null);
  const [refreshTrigger, setRefreshTrigger] = useState(0);
  const [isDefaultAvatar, setIsDefaultAvatar] = useState(false);
  const [currentFileId, setCurrentFileId] = useState<number | null>(null);

  const refresh = useCallback(() => {
    setRefreshTrigger((prev) => prev + 1);
  }, []);

  const updateAvatar = useCallback(
    (blob: Blob, fileId?: number) => {
      // Revoke URL cũ nếu có
      if (avatarUrl) {
        URL.revokeObjectURL(avatarUrl);
      }
      // Tạo URL mới từ blob
      const url = URL.createObjectURL(blob);
      setAvatarUrl(url);
      setIsDefaultAvatar(false);
      if (fileId !== undefined) {
        setCurrentFileId(fileId);
      }
    },
    [avatarUrl]
  );

  useEffect(() => {
    let objectUrl: string | null = null;

    const fetchAvatar = async () => {
      // Nếu không có imageLink, không cần fetch, sử dụng icon mặc định từ Ant Design
      if (!imageLink) {
        setAvatarUrl(null);
        setIsDefaultAvatar(true);
        setLoading(false);
        return;
      }

      try {
        setLoading(true);
        setError(null);

        // Parse imageLink thành fileId
        const fileId = parseInt(imageLink);
        if (isNaN(fileId)) {
          console.warn('Invalid imageLink, using default icon');
          setAvatarUrl(null);
          setIsDefaultAvatar(true);
          setLoading(false);
          return;
        }

        // Lấy thông tin file từ API
        const fileInfo = await FileService.getFileById(fileId);
        if (!fileInfo || !fileInfo.link) {
          console.warn('File info not found, using default icon');
          setAvatarUrl(null);
          setIsDefaultAvatar(true);
          setLoading(false);
          return;
        }

        setCurrentFileId(fileId);

        // Download ảnh từ S3
        const bustCache = refreshTrigger > 0;
        const blob = await FileService.downloadFile(fileInfo.link, bustCache);
        const url = URL.createObjectURL(blob);
        objectUrl = url;
        setAvatarUrl(url);
        setIsDefaultAvatar(false);
      } catch (err) {
        console.error('Error loading avatar, using default icon:', err);
        setError(err as Error);
        setAvatarUrl(null);
        setIsDefaultAvatar(true);
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

  return { avatarUrl, loading, error, refresh, updateAvatar, isDefaultAvatar, currentFileId };
};
