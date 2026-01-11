import { useEffect, useState } from 'react';
import { Button, DatePicker, Form, FormProps, Input, InputNumber, Modal, Select } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { clearSeletedKpiTruong, resetStatusKpiTruong } from '@redux/feature/kpi/kpiSlice';
import { createKpiTruong, updateKpiTruong } from '@redux/feature/kpi/kpiThunk';
import { ReduxStatus } from '@redux/const';
import { toast } from 'react-toastify';
import { ICreateKpiTruong } from '@models/kpi/kpi-truong.model';
import dayjs from 'dayjs';
import { KpiLoaiConst } from '@/constants/kpi/kpiType.const';

type PositionModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
  onSuccess: () => void;
};

const PositionModal: React.FC<PositionModalProps> = (props) => {
  const dispatch = useAppDispatch();
  const [form] = Form.useForm<any>();
  const [title, setTitle] = useState<string>('Thêm mới Kpi Đơn vị');
  const { $selected, $create, $update } = useAppSelector((state) => state.kpiState.kpiTruong);
  const isSaving = $create.status === ReduxStatus.LOADING || $update.status === ReduxStatus.LOADING;
  const { isModalOpen, isUpdate, isView, setIsModalOpen } = props;

  useEffect(() => {
    if (isModalOpen) {
      if (isUpdate || isView) {
        setTitle(isView ? 'Xem thông tin KPI Trường' : 'Cập nhật KPI Trường');
      } else {
        setTitle('Thêm mới KPI Trường');
      }
    }
  }, [dispatch, isModalOpen, isUpdate, isView]);

  useEffect(() => {
    if (!$selected.data) return;
    const selectedData = $selected.data;
    form.setFieldsValue({
      ...selectedData,
      namHoc: selectedData.namHoc
        ? dayjs(selectedData.namHoc, 'YYYY')
        : undefined,
    });
  }, [$selected.data]);

  useEffect(() => {
    if ($create.status === ReduxStatus.SUCCESS || $update.status === ReduxStatus.SUCCESS) {
      dispatch(resetStatusKpiTruong());
      dispatch(clearSeletedKpiTruong());
      form.resetFields();
      setIsModalOpen(false);
      props.onSuccess();
    }
  }, [$create.status, $update.status, dispatch, form, setIsModalOpen]);

  const handleClose = () => {
    form.resetFields();
    dispatch(clearSeletedKpiTruong());
    setIsModalOpen(false);
  };

  const handleFinish: FormProps['onFinish'] = async (values: any) => {
    const payload = {
      ...values,
      namHoc: values.namHoc.format('YYYY'),
    };


    try {
      if (isUpdate && $selected.data) {
        await dispatch(
          updateKpiTruong({ id: $selected.data.id, ...payload })
        ).unwrap();
        toast.success('Cập nhật KPI Trường thành công');
      } else {
        console.log("Payload: ", payload);
        await dispatch(createKpiTruong(payload)).unwrap();
        toast.success('Thêm mới KPI Trường thành công');
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
        name="kpi-truong-form"
        layout="vertical"
        form={form}
        onFinish={handleFinish}
        autoComplete="on"
        disabled={isView}
        labelCol={{ style: { fontWeight: 600 } }}
      >
        <div className="grid grid-cols-2 gap-x-5">
          <Form.Item<ICreateKpiTruong>
            label="Lĩnh Vực"
            name="linhVuc"
            rules={[{ required: true, message: 'Vui lòng nhập lĩnh vực' }]}
          >
            <Input />
          </Form.Item>


          <Form.Item<ICreateKpiTruong>
            label="Chiến lược"
            name="chienLuoc"
            rules={[{ required: true, message: 'Vui lòng nhập Chiến lược' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item<ICreateKpiTruong>
            label="Tên KPI"
            name="kpi"
            rules={[{ required: true, message: 'Vui lòng nhập tên KPI' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item<ICreateKpiTruong>
            label="Mục tiêu"
            name="mucTieu"
            rules={[{ required: true, message: 'Vui lòng nhập mục tiêu' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item<ICreateKpiTruong>
            label="Trọng số"
            name="trongSo"
            rules={[{ required: true, message: 'Vui lòng nhập trọng số' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label="Loại KPI"
            name="loaiKpi"
            rules={[{ required: true, message: 'Vui lòng chọn loại KPI' }]}
          >
            <Select
              options={KpiLoaiConst.list.map(x => ({
                value: x.value,
                label: x.name,
              }))}
              placeholder="Chọn loại KPI"
            />
          </Form.Item>

          <Form.Item<ICreateKpiTruong>
            label="Năm học"
            name="namHoc"
            rules={[{ required: true, message: 'Vui lòng chọn năm học' }]}
          >
            <DatePicker
              picker="year"
              format="YYYY"
              className="!w-full"
            />
          </Form.Item>
        </div>
      </Form>
    </Modal>
  );
};

export default PositionModal;
