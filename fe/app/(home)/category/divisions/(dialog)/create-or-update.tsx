import { useEffect, useState } from 'react';
import { Button, DatePicker, Form, FormProps, Input, InputNumber, Modal, Select } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreateToBoMon, IUpdateToBoMon } from '@models/danh-muc/to-bo-mon.model';
import { createToBoMon, getAllPhongBan, getToBoMonById, updateToBoMon } from '@redux/feature/danh-muc/danhmucThunk';
import { clearSeletedToBoMon, resetStatusToBoMon } from '@redux/feature/danh-muc/danhmucSlice';
import { ReduxStatus } from '@redux/const';
import { toast } from 'react-toastify';
import dayjs from 'dayjs';

type DivisionModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
  refreshData: () => void;
};

const DivisionModal: React.FC<DivisionModalProps> = (props) => {
  const dispatch = useAppDispatch();
  const [form] = Form.useForm<ICreateToBoMon>();
  const [title] = useState<string>('');

  const { $selected, $create, $update } = useAppSelector((state) => state.danhmucState.toBoMon);

  const isSaving = $create.status === ReduxStatus.LOADING || $update.status === ReduxStatus.LOADING;

  const { isModalOpen, isUpdate, isView, refreshData, setIsModalOpen } = props;
  const phongBanList = useAppSelector((state) => state.danhmucState.phongBan.$list.data || []);

  useEffect(() => {
    if (isModalOpen) {
      dispatch(getAllPhongBan());
      if ((isUpdate || isView) && $selected.id) dispatch(getToBoMonById($selected.id));
    }
  }, [dispatch, isModalOpen, isUpdate, isView, $selected.id]);

  useEffect(() => {
    if ($selected.data) {
      const selectedData = $selected.data;
      const idPhongBan = phongBanList.find((pb) => pb.tenPhongBan === selectedData.phongBan)?.id;
      form.setFieldsValue({
        ...selectedData,
        ngayThanhLap: selectedData.ngayThanhLap ? dayjs(selectedData.ngayThanhLap) : undefined,
        idPhongBan
      });
    }
  }, [$selected.data, phongBanList, form]);

  useEffect(() => {
    if ($create.status === ReduxStatus.SUCCESS || $update.status === ReduxStatus.SUCCESS) {
      dispatch(resetStatusToBoMon());
      dispatch(clearSeletedToBoMon());
      form.resetFields();
      refreshData();
      setIsModalOpen(false);
    }
  }, [$create.status, $update.status, dispatch, form, refreshData, setIsModalOpen]);

  const handleClose = () => {
    form.resetFields();
    dispatch(clearSeletedToBoMon());
    props.setIsModalOpen(false);
  };

  const handleFinish: FormProps['onFinish'] = async (values: ICreateToBoMon | IUpdateToBoMon) => {
    try {
      if (props.isUpdate && $selected.id) {
        await dispatch(updateToBoMon({ id: $selected.id, ...values })).unwrap();
        toast.success('Cập nhật thành công');
      } else {
        await dispatch(createToBoMon(values)).unwrap();
        toast.success('Thêm mới thành công');
      }
    } catch {
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
        name="toBoMon"
        layout="vertical"
        form={form}
        onFinish={handleFinish}
        autoComplete="on"
        disabled={props.isView}
        labelCol={{ style: { fontWeight: 600 } }}
      >
        <div className="grid grid-cols-2 gap-x-5">
          <Form.Item<ICreateToBoMon>
            label="Mã bộ môn"
            name="maBoMon"
            rules={[{ required: true, message: 'Vui lòng nhập mã bộ môn' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item<ICreateToBoMon>
            label="Tên bộ môn"
            name="tenBoMon"
            rules={[{ required: true, message: 'Vui lòng nhập tên bộ môn' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item<ICreateToBoMon>
            label="Ngày thành lập"
            name="ngayThanhLap"
            rules={[{ required: true, message: 'Không được để trống!' }]}
          >
            <DatePicker format="DD/MM/YYYY" needConfirm className="!w-full" />
          </Form.Item>

          <Form.Item<ICreateToBoMon>
            label="Phòng ban"
            name="idPhongBan"
            rules={[{ required: true, message: 'Vui lòng chọn phòng ban' }]}
          >
            <Select
              placeholder="Chọn phòng ban"
              showSearch
              optionFilterProp="children"
              filterOption={(input, option) =>
                (option?.children as unknown as string).toLowerCase().includes(input.toLowerCase())
              }
            >
              {phongBanList.map((pb) => (
                <Select.Option key={pb.id} value={pb.id}>
                  {pb.tenPhongBan}
                </Select.Option>
              ))}
            </Select>
          </Form.Item>
        </div>
      </Form>
    </Modal>
  );
};

export default DivisionModal;
