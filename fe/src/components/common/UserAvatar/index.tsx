'use client';

import { useState, useRef } from 'react';
import { Avatar, Button, message } from 'antd';
import { UserOutlined, EditOutlined } from '@ant-design/icons';
import { useUserAvatar } from '@hooks/useUserAvatar';
import { FileService } from '@services/file.service';
import { useSelector } from 'react-redux';
import { RootState } from '@redux/store';

interface UserAvatarProps {
  imageLink?: string | null;
  maNhanSu?: string | null;
  tenNhanSu?: string | null;
  size?: number | 'small' | 'default' | 'large';
  editable?: boolean;
  showEditButton?: boolean;
  onUploadSuccess?: (blob: Blob, fileId: number) => void;
  onUploadError?: (error: any) => void;
  className?: string;
}

const UserAvatar = ({
  imageLink,
  maNhanSu,
  tenNhanSu,
  size = 'large',
  editable = false,
  showEditButton = false,
  onUploadSuccess,
  onUploadError,
  className = ''
}: UserAvatarProps) => {
  const [uploadingAvatar, setUploadingAvatar] = useState(false);
  const fileInputRef = useRef<HTMLInputElement>(null);
  const { user } = useSelector((state: RootState) => state.authState);

  const { avatarUrl, updateAvatar, isDefaultAvatar, currentFileId } = useUserAvatar(imageLink);

  const handleAvatarClick = () => {
    if (editable) {
      fileInputRef.current?.click();
    }
  };

  const validateFile = (file: File): { valid: boolean; error?: string } => {
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
  };

  const handleFileChange = async (event: React.ChangeEvent<HTMLInputElement>) => {
    const file = event.target.files?.[0];
    if (!file || !maNhanSu) return;

    const validation = validateFile(file);
    if (!validation.valid) {
      message.error(validation.error);
      return;
    }

    setUploadingAvatar(true);
    try {
      // Gọi API update-image-user
      const imageBlob = await FileService.updateUserImage(maNhanSu, file);

      // Cập nhật avatar với blob mới
      updateAvatar(imageBlob);

      message.success('Cập nhật ảnh đại diện thành công!');

      if (onUploadSuccess) {
        // Trigger callback để cập nhật Redux store với cache busting
        onUploadSuccess(imageBlob, Date.now());
      }
    } catch (error) {
      console.error('Avatar upload error:', error);
      message.error('Không thể tải lên ảnh đại diện!');

      if (onUploadError) {
        onUploadError(error);
      }
    } finally {
      setUploadingAvatar(false);
      if (fileInputRef.current) {
        fileInputRef.current.value = '';
      }
    }
  };

  return (
    <div className={`user-avatar-wrapper ${className}`}>
      <Avatar
        size={size}
        src={avatarUrl || undefined}
        icon={!avatarUrl && <UserOutlined />}
        className={editable ? 'cursor-pointer' : ''}
        onClick={handleAvatarClick}
      />

      <input ref={fileInputRef} type="file" accept="image/*" onChange={handleFileChange} style={{ display: 'none' }} />

      {showEditButton && (
        <Button
          type="default"
          icon={<EditOutlined />}
          onClick={handleAvatarClick}
          loading={uploadingAvatar}
          disabled={!maNhanSu}
          className="mt-2"
        >
          Đổi ảnh đại diện
        </Button>
      )}
    </div>
  );
};

export default UserAvatar;
