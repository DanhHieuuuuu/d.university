import axios from 'axios';
import { getItem } from '@utils/token-storage';

const getImageFromS3 = async (fileName: string, bustCache: boolean = false): Promise<Blob> => {
  try {
    const { accessToken } = getItem();

    const params: any = { fileName };
    // Thêm timestamp để bypass cache khi cần
    if (bustCache) {
      params.t = Date.now();
    }

    const res = await axios.get(`${process.env.NEXT_PUBLIC_AUTH_API_URL}s3-test/download`, {
      params,
      headers: {
        Authorization: `Bearer ${accessToken}`,
        accept: '*/*'
      },
      responseType: 'blob'
    });

    return Promise.resolve(res.data);
  } catch (err) {
    console.error('Error fetching image from S3:', err);
    return Promise.reject(err);
  }
};

const updateUserImage = async (maNhanSu: string, file: File): Promise<Blob> => {
  try {
    const { accessToken } = getItem();

    const formData = new FormData();
    formData.append('File', file);

    const res = await axios.put(`${process.env.NEXT_PUBLIC_AUTH_API_URL}user/update-image-user`, formData, {
      params: { MaNhanSu: maNhanSu },
      headers: {
        Authorization: `Bearer ${accessToken}`,
        'Content-Type': 'multipart/form-data',
        accept: '*/*'
      },
      responseType: 'blob'
    });

    return Promise.resolve(res.data);
  } catch (err) {
    console.error('Error updating user image:', err);
    return Promise.reject(err);
  }
};

export const ImageService = {
  getImageFromS3,
  updateUserImage
};
