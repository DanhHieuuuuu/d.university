import { useEffect, useMemo, useState } from 'react';
import { Button, DatePicker, Form, FormProps, Input, InputNumber, Modal, Select, Divider } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined, InfoCircleOutlined, ExperimentOutlined, TeamOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { clearSeletedKpiDonVi, resetStatusKpiDonVi } from '@redux/feature/kpi/kpiSlice';
import { createKpiDonVi, updateKpiDonVi, getListKpiCongThuc } from '@redux/feature/kpi/kpiThunk';
import { ReduxStatus } from '@redux/const';
import { toast } from 'react-toastify';
import dayjs from 'dayjs';
import { getAllPhongBanByKpiRole } from '@redux/feature/danh-muc/danhmucThunk';
import { KpiLoaiConst } from '@/constants/kpi/kpiType.const';
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
  const [title, setTitle] = useState<string>('KPI Đơn vị');
  const { $selected, $create, $update } = useAppSelector((state) => state.kpiState.kpiDonVi);
  const {listCongThuc } = useAppSelector((state) => state.kpiState);
  const sharedListCongThuc = useAppSelector((state) => (state.kpiState as any).listCongThuc);

  const isSaving = $create.status === ReduxStatus.LOADING || $update.status === ReduxStatus.LOADING;
  const { isModalOpen, isUpdate, isView, setIsModalOpen } = props;
  const { phongBanByKpiRole } = useAppSelector((state) => state.danhmucState);

  const congThucOptions = useMemo(() => {
    return (sharedListCongThuc?.data || []).map((ct: any) => ({
      value: ct.id,
      label: ct.tenCongThuc,
    }));
  }, [sharedListCongThuc?.data]);

  useEffect(() => {
    if (isModalOpen) {
      setTitle(isView ? 'Xem thông tin KPI Đơn vị' : isUpdate ? 'Cập nhật KPI Đơn vị' : 'Thêm mới KPI Đơn vị');
      dispatch(getAllPhongBanByKpiRole({ PageIndex: 1, PageSize: 2000 }));
      if (sharedListCongThuc?.data.length === 0) {
        dispatch(getListKpiCongThuc({}));
      }
    }
  }, [isModalOpen, isUpdate, isView, dispatch]);

  useEffect(() => {
    if (!$selected.data || !isModalOpen) return;
    const selectedData = $selected.data;

    form.setFieldsValue({
      ...selectedData,
      idDonVi: selectedData.idDonVi, 
      idCongThuc: selectedData.idCongThuc, 
      congThucTinh: selectedData.congThuc,
      namHoc: selectedData.namHoc ? dayjs(selectedData.namHoc, 'YYYY') : undefined,
    });
  }, [$selected.data, isModalOpen, form]);

  useEffect(() => {
    if ($create.status === ReduxStatus.SUCCESS || $update.status === ReduxStatus.SUCCESS) {
      dispatch(resetStatusKpiDonVi());
      dispatch(clearSeletedKpiDonVi());
      form.resetFields();
      setIsModalOpen(false);
      props.onSuccess();
    }
  }, [$create.status, $update.status, dispatch, form, setIsModalOpen, props]);

  const handleClose = () => {
    form.resetFields();
    dispatch(clearSeletedKpiDonVi());
    setIsModalOpen(false);
  };

  const handleFinish: FormProps['onFinish'] = async (values: any) => {
    const payload = {
      ...values,
      idDonVi: values.idDonVi?.value ?? values.idDonVi,
      namHoc: values.namHoc ? values.namHoc.format('YYYY') : undefined,
      trongSo: values.trongSo?.toString() || '0',
    };

    try {
      if (isUpdate && $selected.data) {
        await dispatch(updateKpiDonVi({ id: $selected.data.id, ...payload })).unwrap();
        toast.success('Cập nhật thành công');
      } else {
        await dispatch(createKpiDonVi(payload)).unwrap();
        toast.success('Thêm mới thành công');
      }
    } catch {
      toast.error('Đã xảy ra lỗi, vui lòng thử lại!');
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
          <div className="col-span-2 flex items-center gap-2 mb-2 text-blue-600 font-semibold">
            <InfoCircleOutlined /> THÔNG TIN KPI ĐƠN VỊ
          </div>

          <Form.Item label="Tên KPI" name="kpi" className="col-span-2" rules={[{ required: true, message: 'Nhập tên KPI' }]}>
            <Input.TextArea rows={2} placeholder="Nhập tên chỉ số KPI dành cho đơn vị" />
          </Form.Item>

          <Form.Item label="Mục tiêu" name="mucTieu" rules={[{ required: true, message: 'Nhập mục tiêu' }]}>
            <Input placeholder="Ví dụ: Đạt kết quả tốt..." />
          </Form.Item>

          <Form.Item label="Trọng số (%)" name="trongSo" rules={[{ required: true }]}>
            <InputNumber className="w-full" min={0} max={100} precision={2} placeholder="0.00" />
          </Form.Item>

          <Divider className="col-span-2 my-2" />

          <div className="col-span-2 flex items-center gap-2 mb-2 text-purple-600 font-semibold">
            <ExperimentOutlined /> CÔNG THỨC & ĐỊNH DẠNG
          </div>

          <Form.Item label="Loại KPI" name="loaiKpi" rules={[{ required: true }]}>
            <Select
              placeholder="Chọn loại"
              options={KpiLoaiConst.list.map(x => ({ value: x.value, label: x.name }))}
            />
          </Form.Item>

          <Form.Item label="Năm học" name="namHoc" rules={[{ required: true }]}>
            <DatePicker picker="year" format="YYYY" className="w-full" />
          </Form.Item>

          <Form.Item label="Công thức tính KPI" name="idCongThuc" rules={[{ required: true }]}>
            <Select
              placeholder="Chọn công thức từ hệ thống"
              options={congThucOptions}
              loading={sharedListCongThuc?.status === ReduxStatus.LOADING}
              onChange={(value) => {
                const selected = sharedListCongThuc?.data.find((x: any) => x.id === value);
                form.setFieldsValue({ congThucTinh: selected?.tenCongThuc });
              }}
            />
          </Form.Item>

          <Form.Item label="Loại kết quả" name="loaiKetQua" rules={[{ required: true }]}>
            <Select placeholder="Chọn loại kết quả" options={LOAI_KET_QUA_OPTIONS} />
          </Form.Item>

          <Divider className="col-span-2 my-2" />

          <div className="col-span-2 flex items-center gap-2 mb-2 text-green-600 font-semibold">
            <TeamOutlined /> ĐƠN VỊ THỰC HIỆN
          </div>

          <Form.Item label="Đơn vị chủ trì" name="idDonVi" className="col-span-2" rules={[{ required: true, message: 'Vui lòng chọn đơn vị' }]}>
            <Select
              options={phongBanByKpiRole.$list.data.map((pb) => ({
                value: pb.id,
                label: pb.tenPhongBan,
              }))}
              loading={phongBanByKpiRole.$list.status === ReduxStatus.LOADING}
              showSearch
              optionFilterProp="label"
              placeholder="Tìm kiếm và chọn đơn vị thực hiện"
            />
          </Form.Item>
          <Form.Item name="congThucTinh" hidden><Input /></Form.Item>
        </div>
      </Form>
    </Modal>
  );
};

export default PositionModal;