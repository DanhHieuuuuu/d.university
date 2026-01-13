import { useEffect, useMemo, useState } from 'react';
import { Button, DatePicker, Form, FormProps, Input, InputNumber, Modal, Select, Divider } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined, InfoCircleOutlined, ExperimentOutlined, BankOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { clearSeletedKpiTruong, resetStatusKpiTruong } from '@redux/feature/kpi/kpiSlice';
import { createKpiTruong, updateKpiTruong } from '@redux/feature/kpi/kpiThunk';
import { ReduxStatus } from '@redux/const';
import { toast } from 'react-toastify';
import { ICreateKpiTruong } from '@models/kpi/kpi-truong.model';
import dayjs from 'dayjs';
import { KpiLoaiConst } from '@/constants/kpi/kpiType.const';
import { LIST_CONG_THUC } from '@/constants/kpi/kpiFormula.const';
import { LOAI_KET_QUA_OPTIONS } from '@/constants/kpi/loaiCongThuc.enum';

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
  const [title, setTitle] = useState<string>('KPI Trường');
  const { $selected, $create, $update } = useAppSelector((state) => state.kpiState.kpiTruong);
  const isSaving = $create.status === ReduxStatus.LOADING || $update.status === ReduxStatus.LOADING;
  const { isModalOpen, isUpdate, isView, setIsModalOpen } = props;
  const selectedLoaiKpi = Form.useWatch('loaiKpi', form);

  const loaiKpiOptions = useMemo(() => {
    return KpiLoaiConst.list
      .filter(x => x.value === 1 || x.value === 2)
      .map(x => ({ value: x.value, label: x.name }));
  }, []);

  // Lọc Công thức
  const congThucOptions = useMemo(() => {
    if (!selectedLoaiKpi) return [];
    return LIST_CONG_THUC
      .filter(ct => ct.loaiKpiApDung.includes(selectedLoaiKpi))
      .map(ct => ({ value: ct.id, label: ct.congThuc }));
  }, [selectedLoaiKpi]);

  useEffect(() => {
    if (isModalOpen) {
      setTitle(isView ? 'Xem thông tin KPI Trường' : isUpdate ? 'Cập nhật KPI Trường' : 'Thêm mới KPI Trường');
    }
  }, [isModalOpen, isUpdate, isView]);

  useEffect(() => {
    if (!$selected.data || !isModalOpen) return;
    const selectedData = $selected.data;
    const congThucMatched = LIST_CONG_THUC.find(x => x.congThuc === selectedData.congThuc);

    form.setFieldsValue({
      ...selectedData,
      idCongThuc: congThucMatched?.id,
      congThucTinh: selectedData.congThuc,
      namHoc: selectedData.namHoc ? dayjs(selectedData.namHoc, 'YYYY') : undefined,
    });
  }, [$selected.data, isModalOpen, form]);

  useEffect(() => {
    if ($create.status === ReduxStatus.SUCCESS || $update.status === ReduxStatus.SUCCESS) {
      dispatch(resetStatusKpiTruong());
      dispatch(clearSeletedKpiTruong());
      form.resetFields();
      setIsModalOpen(false);
      props.onSuccess();
    }
  }, [$create.status, $update.status, dispatch, form, setIsModalOpen, props]);

  const handleClose = () => {
    form.resetFields();
    dispatch(clearSeletedKpiTruong());
    setIsModalOpen(false);
  };

  const handleFinish: FormProps['onFinish'] = async (values: any) => {
    const payload = {
      ...values,
      namHoc: values.namHoc.format('YYYY'),
      trongSo: values.trongSo?.toString() || '0',
    };
    try {
      if (isUpdate && $selected.data) {
        await dispatch(updateKpiTruong({ id: $selected.data.id, ...payload })).unwrap();
        toast.success('Cập nhật thành công');
      } else {
        await dispatch(createKpiTruong(payload)).unwrap();
        toast.success('Thêm mới thành công');
      }
    } catch {
      toast.error('Đã xảy ra lỗi!');
    }
  };

  return (
    <Modal
      title={<span className="text-xl font-bold text-blue-700">{title}</span>}
      width={950} 
      open={isModalOpen}
      onCancel={handleClose}
      centered
      footer={[
        <Button key="close" onClick={handleClose} icon={<CloseOutlined />}>Đóng</Button>,
        !isView && (
          <Button
            key="submit"
            loading={isSaving}
            onClick={form.submit}
            icon={isUpdate ? <SaveOutlined /> : <PlusOutlined />}
            type="primary"
            className="bg-blue-600 shadow-md"
          >
            {isUpdate ? 'Lưu thay đổi' : 'Tạo mới'}
          </Button>
        ),
      ]}
    >
      <Form
        form={form}
        layout="vertical"
        onFinish={handleFinish}
        disabled={isView}
        className="mt-4"
        requiredMark="optional"
      >
        <div className="grid grid-cols-2 gap-x-6">
          {/* PHẦN 1: THÔNG TIN CHIẾN LƯỢC */}
          <div className="col-span-2 flex items-center gap-2 mb-2 text-blue-600 font-semibold">
            <BankOutlined /> THÔNG TIN CHIẾN LƯỢC TRƯỜNG
          </div>

          <Form.Item label="Lĩnh Vực" name="linhVuc" rules={[{ required: true, message: 'Nhập lĩnh vực' }]}>
            <Input placeholder="Ví dụ: Đào tạo, Nghiên cứu..." />
          </Form.Item>

          <Form.Item label="Chiến lược" name="chienLuoc" rules={[{ required: true, message: 'Nhập chiến lược' }]}>
            <Input placeholder="Nhập mục tiêu chiến lược của trường" />
          </Form.Item>

          <Form.Item label="Tên KPI" name="kpi" className="col-span-2" rules={[{ required: true, message: 'Nhập tên KPI' }]}>
            <Input.TextArea rows={2} placeholder="Nhập tên chỉ số KPI cấp Trường" />
          </Form.Item>

          <Divider className="col-span-2 my-2" />

          {/* PHẦN 2: CÔNG THỨC & CHỈ SỐ */}
          <div className="col-span-2 flex items-center gap-2 mb-2 text-purple-600 font-semibold">
            <ExperimentOutlined /> CÔNG THỨC & ĐỊNH DẠNG
          </div>

          <Form.Item label="Loại KPI" name="loaiKpi" rules={[{ required: true }]}>
            <Select 
              placeholder="Chọn loại" 
              options={loaiKpiOptions} 
              onChange={() => form.setFieldsValue({ idCongThuc: undefined, congThucTinh: undefined })}
            />
          </Form.Item>

          <Form.Item label="Năm học" name="namHoc" rules={[{ required: true }]}>
            <DatePicker picker="year" format="YYYY" className="w-full" />
          </Form.Item>

          <Form.Item label="Công thức tính KPI" name="idCongThuc" rules={[{ required: true }]}>
            <Select
              placeholder={selectedLoaiKpi ? 'Chọn công thức' : 'Chọn Loại KPI trước'}
              options={congThucOptions}
              disabled={!selectedLoaiKpi}
              onChange={(value) => {
                const selected = LIST_CONG_THUC.find(x => x.id === value);
                form.setFieldsValue({ congThucTinh: selected?.congThuc });
              }}
            />
          </Form.Item>

          <Form.Item label="Loại kết quả" name="loaiKetQua" rules={[{ required: true }]}>
            <Select placeholder="Chọn loại kết quả" options={LOAI_KET_QUA_OPTIONS} />
          </Form.Item>

          <Form.Item label="Mục tiêu giá trị" name="mucTieu" rules={[{ required: true }]}>
            <Input placeholder="Ví dụ: >= 90% hoặc 50 bài báo..." />
          </Form.Item>

          <Form.Item label="Trọng số (%)" name="trongSo" rules={[{ required: true }]}>
            <InputNumber className="w-full" min={0} max={100} precision={2} placeholder="0.00" />
          </Form.Item>
          <Form.Item name="congThucTinh" hidden><Input /></Form.Item>
        </div>
      </Form>
    </Modal>
  );
};

export default PositionModal;