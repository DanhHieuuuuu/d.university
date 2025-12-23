import { useEffect, useState } from 'react';
import { Button, Form, FormProps, Input, InputNumber, Modal, Select, Switch } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreateMonHoc, IUpdateMonHoc } from '@models/dao-tao/monHoc.model';
import {
  clearSelectedMonHoc,
  createMonHoc,
  getMonHocById,
  resetStatusMonHoc,
  updateMonHoc
} from '@redux/feature/daotaoSlice';
import { getAllToBoMon } from '@redux/feature/danh-muc/danhmucThunk';
import { ReduxStatus } from '@redux/const';
import { toast } from 'react-toastify';

type CourseModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
  refreshData: () => void;
};

const CourseModal: React.FC<CourseModalProps> = (props) => {
  const dispatch = useAppDispatch();
  const [form] = Form.useForm<ICreateMonHoc>();
  const [title, setTitle] = useState<string>('');

  const { $selected, $create, $update } = useAppSelector((state) => state.daotaoState.monHoc);
  const listToBoMon = useAppSelector((state) => state.danhmucState.listToBoMon);

  const isSaving = $create.status === ReduxStatus.LOADING || $update.status === ReduxStatus.LOADING;

  useEffect(() => {
    if (props.isModalOpen) {
      // Load list ToBoMon for dropdown
      dispatch(getAllToBoMon({ PageIndex: 1, PageSize: 100 }));

      if (props.isUpdate) setTitle('Chỉnh sửa môn học');
      else if (props.isView) setTitle('Chi tiết môn học');
      else setTitle('Thêm mới môn học');
    }
  }, [props.isModalOpen, props.isUpdate, props.isView]);

  useEffect(() => {
    if (props.isModalOpen && (props.isUpdate || props.isView) && $selected.id) {
      dispatch(getMonHocById($selected.id));
    }
  }, [props.isModalOpen, props.isUpdate, props.isView, $selected.id]);

  useEffect(() => {
    if ($selected.data) {
      form.setFieldsValue($selected.data);
    }
  }, [$selected.data]);

  useEffect(() => {
    if ($create.status === ReduxStatus.SUCCESS || $update.status === ReduxStatus.SUCCESS) {
      dispatch(resetStatusMonHoc());
      dispatch(clearSelectedMonHoc());
      form.resetFields();
      props.refreshData();
      props.setIsModalOpen(false);
    }
  }, [$create.status, $update.status]);

  const handleClose = () => {
    form.resetFields();
    dispatch(clearSelectedMonHoc());
    props.setIsModalOpen(false);
  };

  const handleFinish: FormProps['onFinish'] = async (values: ICreateMonHoc | IUpdateMonHoc) => {
    try {
      if (props.isUpdate && $selected.id) {
        await dispatch(updateMonHoc({ id: $selected.id, ...values })).unwrap();
        toast.success('Cập nhật thành công');
      } else {
        await dispatch(createMonHoc(values)).unwrap();
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
        name="monHoc"
        layout="vertical"
        form={form}
        onFinish={handleFinish}
        autoComplete="on"
        disabled={props.isView}
        labelCol={{ style: { fontWeight: 600 } }}
        initialValues={{ trangThai: true, soTinChi: 0, soTietLyThuyet: 0, soTietThucHanh: 0 }}
      >
        <div className="grid grid-cols-2 gap-x-5">
          <Form.Item<ICreateMonHoc>
            label="Mã môn học"
            name="maMonHoc"
            rules={[{ required: true, message: 'Vui lòng nhập mã môn học' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item<ICreateMonHoc>
            label="Tên môn học"
            name="tenMonHoc"
            rules={[{ required: true, message: 'Vui lòng nhập tên môn học' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item<ICreateMonHoc>
            label="Tổ bộ môn"
            name="toBoMonId"
            rules={[{ required: true, message: 'Vui lòng chọn tổ bộ môn' }]}
          >
            <Select
              placeholder="Chọn tổ bộ môn"
              options={listToBoMon.map((toBoMon) => ({
                label: toBoMon.tenBoMon,
                value: toBoMon.id
              }))}
              showSearch
              filterOption={(input, option) =>
                (option?.label ?? '').toLowerCase().includes(input.toLowerCase())
              }
            />
          </Form.Item>
          <Form.Item<ICreateMonHoc>
            label="Số tín chỉ"
            name="soTinChi"
            rules={[{ required: true, message: 'Vui lòng nhập số tín chỉ' }]}
          >
            <InputNumber className="!w-full" min={0} max={20} />
          </Form.Item>
          <Form.Item<ICreateMonHoc>
            label="Số tiết lý thuyết"
            name="soTietLyThuyet"
          >
            <InputNumber className="!w-full" min={0} max={100} />
          </Form.Item>
          <Form.Item<ICreateMonHoc>
            label="Số tiết thực hành"
            name="soTietThucHanh"
          >
            <InputNumber className="!w-full" min={0} max={100} />
          </Form.Item>
          <Form.Item<ICreateMonHoc> label="Mô tả" name="moTa" className="col-span-2">
            <Input.TextArea rows={3} />
          </Form.Item>
          <Form.Item<ICreateMonHoc> label="Trạng thái" name="trangThai" valuePropName="checked">
            <Switch checkedChildren="Hoạt động" unCheckedChildren="Ngừng" />
          </Form.Item>
        </div>
      </Form>
    </Modal>
  );
};

export default CourseModal;
