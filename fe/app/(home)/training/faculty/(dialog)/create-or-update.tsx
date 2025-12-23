import { useEffect, useState } from 'react';
import { Button, Form, FormProps, Input, Modal, Switch } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreateKhoa, IUpdateKhoa } from '@models/dao-tao/khoa.model';
import { createKhoa, getKhoaById, updateKhoa } from '@redux/feature/dao-tao/khoaThunk';
import { clearSelectedKhoa, resetStatusKhoa } from '@redux/feature/dao-tao/daotaoSlice';
import { ReduxStatus } from '@redux/const';
import { toast } from 'react-toastify';

type FacultyModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
  refreshData: () => void;
};

const FacultyModal: React.FC<FacultyModalProps> = (props) => {
  const dispatch = useAppDispatch();
  const [form] = Form.useForm<ICreateKhoa>();
  const [title, setTitle] = useState<string>('');

  const { $selected, $create, $update } = useAppSelector((state) => state.daotaoState.khoa);

  const isSaving = $create.status === ReduxStatus.LOADING || $update.status === ReduxStatus.LOADING;

  useEffect(() => {
    if (props.isModalOpen) {
      if (props.isUpdate) setTitle('Chỉnh sửa khoa');
      else if (props.isView) setTitle('Chi tiết khoa');
      else setTitle('Thêm mới khoa');
    }
  }, [props.isModalOpen, props.isUpdate, props.isView]);

  useEffect(() => {
    if (props.isModalOpen && (props.isUpdate || props.isView) && $selected.id) {
      dispatch(getKhoaById($selected.id));
    }
  }, [props.isModalOpen, props.isUpdate, props.isView, $selected.id]);

  useEffect(() => {
    if ($selected.data) {
      form.setFieldsValue($selected.data);
    }
  }, [$selected.data]);

  useEffect(() => {
    if ($create.status === ReduxStatus.SUCCESS || $update.status === ReduxStatus.SUCCESS) {
      dispatch(resetStatusKhoa());
      dispatch(clearSelectedKhoa());
      form.resetFields();
      props.refreshData();
      props.setIsModalOpen(false);
    }
  }, [$create.status, $update.status]);

  const handleClose = () => {
    form.resetFields();
    dispatch(clearSelectedKhoa());
    props.setIsModalOpen(false);
  };

  const handleFinish: FormProps['onFinish'] = async (values: ICreateKhoa | IUpdateKhoa) => {
    try {
      if (props.isUpdate && $selected.id) {
        await dispatch(updateKhoa({ id: $selected.id, ...values })).unwrap();
        toast.success('Cập nhật thành công');
      } else {
        await dispatch(createKhoa(values)).unwrap();
        toast.success('Thêm mới thành công');
      }
    } catch (error) {
      toast.error('Đã xảy ra lỗi, vui lòng thử lại!');
    }
  };

  return (
    <Modal
      title={title}
      className="app-modal"
      width="60%"
      closable={{ 'aria-label': 'Custom Close Button' }}
      open={props.isModalOpen}
      onCancel={handleClose}
      footer={
        <>
          {!props.isView && (
            <Button
              loading={isSaving}
              onClick={form.submit}
              icon={props.isUpdate ? <SaveOutlined /> : <PlusOutlined />}
              type="primary"
            >
              {props.isUpdate ? 'Lưu' : 'Tạo'}
            </Button>
          )}
          <Button color="default" variant="filled" onClick={handleClose} icon={<CloseOutlined />}>
            Đóng
          </Button>
        </>
      }
    >
      <Form
        name="khoa"
        layout="vertical"
        form={form}
        onFinish={handleFinish}
        autoComplete="on"
        disabled={props.isView}
        labelCol={{ style: { fontWeight: 600 } }}
        initialValues={{ trangThai: true }}
      >
        <div className="grid grid-cols-2 gap-x-5">
          <Form.Item<ICreateKhoa>
            label="Mã khoa"
            name="maKhoa"
            rules={[{ required: true, message: 'Vui lòng nhập mã khoa' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item<ICreateKhoa>
            label="Tên khoa"
            name="tenKhoa"
            rules={[{ required: true, message: 'Vui lòng nhập tên khoa' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item<ICreateKhoa> label="Tên tiếng Anh" name="tenTiengAnh">
            <Input />
          </Form.Item>
          <Form.Item<ICreateKhoa> label="Viết tắt" name="vietTat">
            <Input />
          </Form.Item>
          <Form.Item<ICreateKhoa> label="Email" name="email">
            <Input type="email" />
          </Form.Item>
          <Form.Item<ICreateKhoa> label="Số điện thoại" name="sdt">
            <Input />
          </Form.Item>
          <Form.Item<ICreateKhoa> label="Địa chỉ" name="diaChi" className="col-span-2">
            <Input.TextArea rows={2} />
          </Form.Item>
          <Form.Item<ICreateKhoa> label="Trạng thái" name="trangThai" valuePropName="checked">
            <Switch checkedChildren="Hoạt động" unCheckedChildren="Ngừng" />
          </Form.Item>
        </div>
      </Form>
    </Modal>
  );
};

export default FacultyModal;
