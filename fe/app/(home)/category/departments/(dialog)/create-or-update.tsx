import { useEffect, useState } from 'react';
import { Button, DatePicker, Form, FormProps, Input, InputNumber, Modal, Select } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreatePhongBan, IUpdatePhongBan } from '@models/danh-muc/phong-ban.model';
import {
  createPhongBan,
  getPhongBanById,
  updatePhongBan,
  getAllLoaiPhongBan
} from '@redux/feature/danh-muc/danhmucThunk';
import { clearSelectedPhongBan, resetStatusPhongBan } from '@redux/feature/danh-muc/danhmucSlice';
import { ReduxStatus } from '@redux/const';
import { toast } from 'react-toastify';
import dayjs from 'dayjs';

type DepartmentModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
  refreshData: () => void;
};

const DepartmentModal: React.FC<DepartmentModalProps> = (props) => {
  const dispatch = useAppDispatch();
  const [form] = Form.useForm<ICreatePhongBan>();
  const [title, setTitle] = useState<string>('Thêm mới phòng ban');

  const { listLoaiPhongBan } = useAppSelector((state) => state.danhmucState);

  const { $selected, $create, $update } = useAppSelector((state) => state.danhmucState.phongBan);

  const isSaving = $create.status === ReduxStatus.LOADING || $update.status === ReduxStatus.LOADING;

  const { isModalOpen, isUpdate, isView, refreshData, setIsModalOpen } = props;

  // Khi mở modal thì nếu là update/view thì load chi tiết phòng ban
  useEffect(() => {
    if (isModalOpen) {
      dispatch(getAllLoaiPhongBan());
      if (isUpdate || isView) {
        if ($selected.id) dispatch(getPhongBanById($selected.id));
        setTitle(isView ? 'Xem thông tin phòng ban' : 'Cập nhật phòng ban');
      } else {
        setTitle('Thêm mới phòng ban');
      }
    }
  }, [dispatch, isModalOpen, isUpdate, isView, $selected.id]);

  // Gán dữ liệu vào form khi load xong
  useEffect(() => {
    if ($selected.data) {
      const selectedData = $selected.data;
      form.setFieldsValue({
        ...selectedData,
        ngayThanhLap: selectedData.ngayThanhLap ? dayjs(selectedData.ngayThanhLap) : undefined
      });
    }
  }, [$selected.data, form]);

  // Khi lưu thành công thì đóng modal, reset form
  useEffect(() => {
    if ($create.status === ReduxStatus.SUCCESS || $update.status === ReduxStatus.SUCCESS) {
      dispatch(resetStatusPhongBan());
      dispatch(clearSelectedPhongBan());
      form.resetFields();
      refreshData();
      setIsModalOpen(false);
    }
  }, [$create.status, $update.status, dispatch, form, refreshData, setIsModalOpen]);

  const handleClose = () => {
    form.resetFields();
    dispatch(clearSelectedPhongBan());
    setIsModalOpen(false);
  };

  const handleFinish: FormProps['onFinish'] = async (values: ICreatePhongBan | IUpdatePhongBan) => {
    try {
      if (isUpdate && $selected.id) {
        await dispatch(updatePhongBan({ id: $selected.id, ...values })).unwrap();
        toast.success('Cập nhật phòng ban thành công');
      } else {
        await dispatch(createPhongBan(values)).unwrap();
        toast.success('Thêm mới phòng ban thành công');
      }
    } catch {
      toast.error('Đã xảy ra lỗi, vui lòng thử lại!');
    }
  };

  return (
    <Modal
      title={title}
      className="app-modal"
      width="60%"
      open={isModalOpen}
      onCancel={handleClose}
      footer={
        <>
          {!isView && (
            <Button
              loading={isSaving}
              onClick={form.submit}
              icon={isUpdate ? <SaveOutlined /> : <PlusOutlined />}
              type="primary"
            >
              {isUpdate ? 'Lưu' : 'Tạo'}
            </Button>
          )}
          <Button color="default" variant="filled" onClick={handleClose} icon={<CloseOutlined />}>
            Đóng
          </Button>
        </>
      }
    >
      <Form
        name="phongBan"
        layout="vertical"
        form={form}
        onFinish={handleFinish}
        autoComplete="on"
        disabled={isView}
        labelCol={{ style: { fontWeight: 600 } }}
      >
        <div className="grid grid-cols-2 gap-x-5">
          <Form.Item<ICreatePhongBan>
            label="Mã phòng ban"
            name="maPhongBan"
            rules={[{ required: true, message: 'Vui lòng nhập mã phòng ban' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item<ICreatePhongBan>
            label="Tên phòng ban"
            name="tenPhongBan"
            rules={[{ required: true, message: 'Vui lòng nhập tên phòng ban' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item<ICreatePhongBan>
            label="Loại phòng ban"
            name="idLoaiPhongBan"
            rules={[{ required: true, message: 'Vui lòng chọn loại phòng ban' }]}
          >
            <Select
              placeholder="Chọn loại phòng ban"
              allowClear
              options={listLoaiPhongBan.map((item) => {
                return { label: item.tenLoaiPhongBan, value: item.id };
              })}
            />
          </Form.Item>

          <Form.Item<ICreatePhongBan>
            label="Địa chỉ"
            name="diaChi"
            rules={[{ required: true, message: 'Vui lòng nhập địa chỉ' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item<ICreatePhongBan>
            label="Hotline"
            name="hotline"
            rules={[{ required: true, message: 'Vui lòng nhập hotline' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item<ICreatePhongBan> label="Fax" name="fax">
            <Input />
          </Form.Item>

          <Form.Item<ICreatePhongBan>
            label="Ngày thành lập"
            name="ngayThanhLap"
            rules={[{ required: true, message: 'Vui lòng chọn ngày thành lập' }]}
          >
            <DatePicker format="DD/MM/YYYY" className="!w-full" />
          </Form.Item>

          <Form.Item<ICreatePhongBan> label="STT" name="stt">
            <InputNumber className="!w-full" min={0} />
          </Form.Item>

          {isView && (
            <>
              <Form.Item<ICreatePhongBan> name="nguoiDaiDien" label="Người đại diện">
                <Input />
              </Form.Item>

              <Form.Item<ICreatePhongBan> name="chucVuNguoiDaiDien" label="Chức vụ người đại diện">
                <Input />
              </Form.Item>
            </>
          )}
        </div>
      </Form>
    </Modal>
  );
};

export default DepartmentModal;
