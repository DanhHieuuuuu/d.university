import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { Form, FormProps, Input, Modal } from 'antd';
import { useAppDispatch, useAppSelector } from '@redux/hooks';

import { ICreateRole } from '@models/role';
import { clearSelectedRole, createRole, getDetailRole, resetStatusRole, updateRole } from '@redux/feature/roleConfigSlice';

type CreateRoleModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  refreshData: () => void;
  isUpdate: boolean;
};

const CreateRoleModal: React.FC<CreateRoleModalProps> = (props) => {
  const [form] = Form.useForm<ICreateRole>();
  const dispatch = useAppDispatch();
  const { selected, roleGroup } = useAppSelector((state) => state.roleConfigState);
  const [isSubmitting, setIsSubmitting] = useState<boolean>(false);

  const title = props.isUpdate ? 'Cập nhật thông tin nhóm quyền' : 'Thêm nhóm quyền mới';
  const okText = props.isUpdate ? 'Lưu lại' : 'Thêm mới';

  useEffect(() => {
    if (props.isModalOpen && props.isUpdate && selected.id) {
      dispatch(getDetailRole(selected.id))
        .unwrap()
        .then((res) => {
          form.setFieldsValue({
            name: res?.name || '',
            description: res?.description || ''
          });
        })
        .catch(() => toast.error('Không thể tải thông tin nhóm quyền'));
    } else {
      form.resetFields();
    }
  }, [props.isModalOpen, props.isUpdate]);

  const onCloseModal = () => {
    dispatch(clearSelectedRole());
    dispatch(resetStatusRole());
    form.resetFields();
    props.setIsModalOpen(false);
  };

  const handleSubmit: FormProps<ICreateRole>['onFinish'] = async (values) => {
    setIsSubmitting(true);
    try {
      if (props.isUpdate && selected.id) {
        const result = await dispatch(updateRole({ id: selected.id, ...values })).unwrap();
        if (result != undefined) {
          toast.success(result?.message || 'Cập nhật thành công');
        }
      } else {
        const result = await dispatch(createRole(values)).unwrap();
        if (result) {
          toast.success(result?.message || 'Tạo nhóm quyền thành công');
        }
      }
      props.refreshData();
      onCloseModal();
    } catch (err: any) {
      toast.error(err?.message || (props.isUpdate ? 'Không thể cập nhật nhóm quyền' : 'Không thể tạo nhóm quyền'));
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
        <Form.Item<ICreateRole>
          label="Tên nhóm quyền"
          name="name"
          rules={[
            { required: true, message: 'Vui lòng nhập tên nhóm quyền' },
            { max: 100, message: 'Tên không được vượt quá 100 ký tự' }
          ]}
        >
          <Input placeholder="Nhập tên nhóm quyền" />
        </Form.Item>

        <Form.Item<ICreateRole> label="Mô tả" name="description">
          <Input.TextArea rows={3} placeholder="Nhập mô tả (không bắt buộc)" />
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default CreateRoleModal;
