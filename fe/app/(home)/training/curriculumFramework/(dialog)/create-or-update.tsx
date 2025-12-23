import { useEffect, useState } from 'react';
import { Button, Form, FormProps, Input, InputNumber, Modal } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreateChucVu, IUpdateChucVu } from '@models/danh-muc/chuc-vu.model';
import {
  clearSeletedChucVu,
  createChucVu,
  getChucVuById,
  resetStatusChucVu,
  updateChucVu
} from '@redux/feature/danhmucSlice';
import { ReduxStatus } from '@redux/const';
import { toast } from 'react-toastify';

type PositionModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
  refreshData: () => void;
};

const PositionModal: React.FC<PositionModalProps> = (props) => {
  const dispatch = useAppDispatch();
  const [form] = Form.useForm<ICreateChucVu>();
  const [title, setTitle] = useState<string>('');

  const { $selected, $create, $update } = useAppSelector((state) => state.danhmucState.chucVu);

  const isSaving = $create.status === ReduxStatus.LOADING || $update.status === ReduxStatus.LOADING;

  useEffect(() => {
    if (props.isModalOpen) {
      if (props.isUpdate) setTitle('Chỉnh sửa');
      else if (props.isView) setTitle('Chi tiết');
      else setTitle('Thêm mới');
    }
  }, [props.isModalOpen, props.isUpdate, props.isView]);

  useEffect(() => {
    if (props.isModalOpen && (props.isUpdate || props.isView) && $selected.id) {
      dispatch(getChucVuById($selected.id));
    }
  }, [props.isModalOpen, props.isUpdate, props.isView, $selected.id]);

  useEffect(() => {
    if ($selected.data) {
      form.setFieldsValue($selected.data);
    }
  }, [$selected.data]);

  useEffect(() => {
    if ($create.status === ReduxStatus.SUCCESS || $update.status === ReduxStatus.SUCCESS) {
      dispatch(resetStatusChucVu());
      dispatch(clearSeletedChucVu());
      form.resetFields();
      props.refreshData();
      props.setIsModalOpen(false);
    }
  }, [$create.status, $update.status]);

  const handleClose = () => {
    form.resetFields();
    dispatch(clearSeletedChucVu());
    props.setIsModalOpen(false);
  };

  const handleFinish: FormProps['onFinish'] = async (values: ICreateChucVu | IUpdateChucVu) => {
    try {
      if (props.isUpdate && $selected.id) {
        await dispatch(updateChucVu({ id: $selected.id, ...values })).unwrap();
        toast.success('Cập nhật thành công');
      } else {
        await dispatch(createChucVu(values)).unwrap();
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
      width="50%"
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
        name="chucvu"
        layout="vertical"
        form={form}
        onFinish={handleFinish}
        autoComplete="on"
        disabled={props.isView}
        labelCol={{ style: { fontWeight: 600 } }}
      >
        <div className="grid grid-cols-2 gap-x-5">
          <Form.Item<ICreateChucVu>
            label="Mã chức vụ"
            name="maChucVu"
            rules={[{ required: true, message: 'Vui lòng nhập mã chức vụ' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item<ICreateChucVu>
            label="Tên chức vụ"
            name="tenChucVu"
            rules={[{ required: true, message: 'Vui lòng nhập tên chức vụ' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item<ICreateChucVu> label="Hệ số chức vụ" name="hsChucVu">
            <InputNumber className="!w-full" min={0} step={0.01} stringMode precision={2} />
          </Form.Item>
          <Form.Item<ICreateChucVu> label="Hệ số trách nhiệm" name="hsTrachNhiem">
            <InputNumber className="!w-full" min={0} step={0.01} stringMode precision={2} />
          </Form.Item>
        </div>
      </Form>
    </Modal>
  );
};

export default PositionModal;
