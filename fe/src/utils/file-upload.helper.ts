import { FileService } from '@services/file.service';

/**
 * Helper function để upload file với folder path
 * @param file File cần upload
 * @param moduleName Tên module (vd: 'auth', 'hrm', 'student')
 * @param subFolder Sub folder bên trong module (vd: 'anh_dai_dien', 'ho_so')
 * @param userId ID người dùng (optional)
 * @returns Upload response với file URL
 *
 * @example
 * // Upload avatar của user
 * const result = await uploadFileWithPath(file, 'auth', 'anh_dai_dien', 'khoa.pd');
 * // File sẽ được lưu tại: auth/anh_dai_dien/khoa.pd/{uuid}.{ext}
 *
 * @example
 * // Upload file vào thư mục mặc định
 * const result = await uploadFileWithPath(file);
 * // File sẽ được lưu tại thư mục gốc
 */
export const uploadFileWithPath = async (file: File, moduleName?: string, subFolder?: string, userId?: string) => {
  let folderPath: string | undefined;

  if (moduleName) {
    const parts = [moduleName];
    if (subFolder) parts.push(subFolder);
    if (userId) parts.push(userId);
    folderPath = parts.join('/');
  }

  return FileService.uploadFile(file, folderPath);
};

/**
 * Helper function để upload nhiều file với folder path
 * @param files Danh sách file cần upload
 * @param moduleName Tên module (vd: 'auth', 'hrm', 'student')
 * @param subFolder Sub folder bên trong module (vd: 'tai_lieu', 'bao_cao')
 * @param userId ID người dùng (optional)
 * @returns Upload response với danh sách file URLs
 *
 * @example
 * // Upload nhiều tài liệu học tập
 * const result = await uploadMultipleFilesWithPath(files, 'student', 'tai_lieu', 'SV001');
 * // Files sẽ được lưu tại: student/tai_lieu/SV001/{uuid}.{ext}
 */
export const uploadMultipleFilesWithPath = async (
  files: File[],
  moduleName?: string,
  subFolder?: string,
  userId?: string
) => {
  let folderPath: string | undefined;

  if (moduleName) {
    const parts = [moduleName];
    if (subFolder) parts.push(subFolder);
    if (userId) parts.push(userId);
    folderPath = parts.join('/');
  }

  return FileService.uploadMultipleFiles(files, folderPath);
};

/**
 * Helper function để tạo FormData cho file service
 * @param name Tên file
 * @param file File object
 * @param applicationField Đường dẫn folder (sẽ được sử dụng như folderPath trong backend)
 * @param description Mô tả file (optional)
 * @returns FormData object
 *
 * @example
 * // Tạo FormData với application field
 * const formData = createFileFormData('Avatar', file, 'auth/anh_dai_dien/khoa.pd', 'Ảnh đại diện');
 * await FileService.create(formData);
 */
export const createFileFormData = (
  name: string,
  file: File,
  applicationField?: string,
  description?: string
): FormData => {
  const formData = new FormData();
  formData.append('Name', name);
  formData.append('File', file);

  if (applicationField) {
    formData.append('ApplicationField', applicationField);
  }

  if (description) {
    formData.append('Description', description);
  }

  return formData;
};

/**
 * Helper function để tạo FormData cho update file service
 * @param id ID của file cần update
 * @param name Tên file
 * @param file File object (optional - nếu muốn thay đổi file)
 * @param applicationField Đường dẫn folder (optional)
 * @param description Mô tả file (optional)
 * @returns FormData object
 */
export const updateFileFormData = (
  id: number,
  name: string,
  file?: File,
  applicationField?: string,
  description?: string
): FormData => {
  const formData = new FormData();
  formData.append('Id', id.toString());
  formData.append('Name', name);

  if (file) {
    formData.append('File', file);
  }

  if (applicationField) {
    formData.append('ApplicationField', applicationField);
  }

  if (description) {
    formData.append('Description', description);
  }

  return formData;
};
