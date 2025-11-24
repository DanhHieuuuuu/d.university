import { useState, useEffect, useCallback } from 'react';
import { FileService } from '@services/file.service';

const DEFAULT_AVATAR_PATH = 'Anh_dai_dien/Anh_mac_dinh/203d0732-820a-4605-bfcc-79c4a4e69d80.jpg';

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
      try {
        setLoading(true);
        setError(null);

        let fileName = DEFAULT_AVATAR_PATH;
        let useDefault = true;

        // Nếu có imageLink, thử lấy thông tin file từ API
        if (imageLink) {
          try {
            const fileId = parseInt(imageLink);
            if (!isNaN(fileId)) {
              const fileInfo = await FileService.getFileById(fileId);
              if (fileInfo && fileInfo.link) {
                fileName = fileInfo.link;
                useDefault = false;
                setCurrentFileId(fileId);
              }
            }
          } catch (err) {
            console.warn('Could not fetch file info, using default avatar:', err);
          }
        }

        setIsDefaultAvatar(useDefault);

        // Lấy ảnh từ S3
        const bustCache = refreshTrigger > 0;
        const blob = await FileService.downloadFile(fileName, bustCache);
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

  return { avatarUrl, loading, error, refresh, updateAvatar, isDefaultAvatar, currentFileId };
};
