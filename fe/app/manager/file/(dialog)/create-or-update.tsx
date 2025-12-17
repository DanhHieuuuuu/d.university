import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { Form, FormProps, Input, Modal, Upload, UploadFile } from 'antd';
import { UploadOutlined } from '@ant-design/icons';
import type { UploadProps } from 'antd';

import { ICreateFile, IUpdateFile, IFile } from '@models/file/file.model';
import { FileService } from '@services/file.service';

type CreateFileModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  refreshData: () => void;
  isUpdate: boolean;
  selectedFile?: IFile | null;
};

const CreateFileModal: React.FC<CreateFileModalProps> = (props) => {
  const [form] = Form.useForm<ICreateFile>();
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);
  const [fileList, setFileList] = useState<UploadFile[]>([]);
  const [uploadedFile, setUploadedFile] = useState<File | null>(null);

  const title = props.isUpdate ? 'Cập nhật thông tin file' : 'Thêm file mới';
  const okText = props.isUpdate ? 'Lưu lại' : 'Thêm mới';

  useEffect(() => {
    if (props.isModalOpen && props.isUpdate && props.selectedFile) {
      form.setFieldsValue({
        name: props.selectedFile.name || '',
        description: props.selectedFile.description || '',
        applicationField: props.selectedFile.applicationField || ''
      });
      if (props.selectedFile.link) {
        setFileList([
          {
            uid: '-1',
            name: props.selectedFile.name || 'file',
            status: 'done',
            url: props.selectedFile.link
          }
        ]);
      }
    } else {
      form.resetFields();
      setFileList([]);
      setUploadedFile(null);
    }
  }, [props.isModalOpen, props.isUpdate, props.selectedFile]);

  const onCloseModal = () => {
    form.resetFields();
    setFileList([]);
    setUploadedFile(null);
    props.setIsModalOpen(false);
  };

  const uploadProps: UploadProps = {
    beforeUpload: (file) => {
      const isLt10M = file.size / 1024 / 1024 < 10;
      if (!isLt10M) {
        toast.error('File phải nhỏ hơn 10MB!');
        return false;
      }

      setUploadedFile(file);
      setFileList([file as any]);
      return false;
    },
    onRemove: () => {
      setFileList([]);
      setUploadedFile(null);
    },
    fileList
  };

  const handleSubmit: FormProps<ICreateFile>['onFinish'] = async (values) => {
    setIsSubmitting(true);
    try {
      const formData = new FormData();

      if (props.isUpdate && props.selectedFile?.id) {
        formData.append('Id', props.selectedFile.id.toString());
      }

      formData.append('Name', values.name);
      if (values.description) {
        formData.append('Description', values.description);
      }
      if (values.applicationField) {
        formData.append('ApplicationField', values.applicationField);
      }
      if (uploadedFile) {
        formData.append('File', uploadedFile);
      }

      if (props.isUpdate && props.selectedFile?.id) {
        await FileService.update(formData);
        toast.success('Cập nhật file thành công');
      } else {
        await FileService.create(formData);
        toast.success('Tạo file thành công');
      }

      props.refreshData();
      onCloseModal();
    } catch (err: any) {
      toast.error(err?.message || (props.isUpdate ? 'Không thể cập nhật file' : 'Không thể tạo file'));
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <Modal
      title={title}
      className="app-modal"
      width={700}
      open={props.isModalOpen}
      onCancel={onCloseModal}
      onOk={() => form.submit()}
      confirmLoading={isSubmitting}
      okText={okText}
      cancelText="Hủy"
      maskClosable={false}
    >
      <Form form={form} layout="vertical" onFinish={handleSubmit}>
        <Form.Item<ICreateFile>
          label="Tên file"
          name="name"
          rules={[
            { required: true, message: 'Vui lòng nhập tên file' },
            { max: 200, message: 'Tên không được vượt quá 200 ký tự' }
          ]}
        >
          <Input placeholder="Nhập tên file" />
        </Form.Item>

        <Form.Item<ICreateFile> label="Mô tả" name="description">
          <Input.TextArea rows={3} placeholder="Nhập mô tả (không bắt buộc)" />
        </Form.Item>

        <Form.Item<ICreateFile> label="Lĩnh vực ứng dụng" name="applicationField">
          <Input placeholder="Nhập lĩnh vực ứng dụng (không bắt buộc)" />
        </Form.Item>

        <Form.Item label="Upload file">
          <Upload {...uploadProps} maxCount={1}>
            <button type="button" className="ant-btn ant-btn-default">
              <UploadOutlined /> Chọn file
            </button>
          </Upload>
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default CreateFileModal;
