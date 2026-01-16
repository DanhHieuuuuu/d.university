import { useEffect, useMemo, useState } from 'react';
import { Button, DatePicker, Form, FormProps, Input, InputNumber, Modal, Select, Divider } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined, InfoCircleOutlined, UserOutlined, ExperimentOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { clearSeletedKpiCaNhan, resetStatusKpiCaNhan } from '@redux/feature/kpi/kpiSlice';
import { createKpiCaNhan, updateKpiCaNhan } from '@redux/feature/kpi/kpiThunk';
import { getListKpiCongThuc } from '@redux/feature/kpi/kpiThunk'; // Import thunk lấy từ DB
import { ReduxStatus } from '@redux/const';
import { toast } from 'react-toastify';
import dayjs from 'dayjs';
import { getAllUser } from '@redux/feature/userSlice';
import { KpiLoaiConst } from '@/constants/kpi/kpiType.const';
import { LOAI_KET_QUA_OPTIONS } from '@/constants/kpi/loaiCongThuc.enum';
import { KpiRoleConst } from '@/constants/kpi/kpiRole.const';
import UserSelect, { UserOption } from '@components/bthanh-custom/userSelect';

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
  const [title, setTitle] = useState<string>('KPI Cá nhân');
  const { listCongThuc } = useAppSelector((state) => state.kpiState);
  const { $selected, $create, $update } = useAppSelector((state) => state.kpiState.kpiCaNhan);
  const isSaving = $create.status === ReduxStatus.LOADING || $update.status === ReduxStatus.LOADING;
  const { isModalOpen, isUpdate, isView, setIsModalOpen } = props;
  const { list: users = [] } = useAppSelector((state) => state.userState.byKpiRole);
  const userStatus = useAppSelector((state) => state.userState.status);
  const { data: listPhongBan } = useAppSelector((state) => state.danhmucState.phongBanByKpiRole.$list);
  const watchIdPhongBan = Form.useWatch('idPhongBan', form);

  const congThucOptions = useMemo(() => {
    return listCongThuc.data.map((ct) => ({ 
      value: ct.id, 
      label: ct.tenCongThuc 
    }));
  }, [listCongThuc.data]);

  const userOptions: UserOption[] = useMemo(() => {
    return (users || [])
      .filter(u => !watchIdPhongBan || u.idPhongBan === watchIdPhongBan)
      .map((u) => {
        const roleValue = KpiRoleConst.list.find(
          (x) => x.name.trim().toLowerCase() === u.tenChucVu?.trim().toLowerCase()
        )?.value;
        const tenChucVuChuan = KpiRoleConst.getName(roleValue) || u.tenChucVu;
        return {
          value: u.id!,
          label: `${u.maNhanSu} - ${u.hoDem ?? ''} ${u.ten ?? ''} - ${tenChucVuChuan}`.trim(),
          searchText: `${u.maNhanSu} ${u.hoDem} ${u.ten}`,
        };
      });
  }, [users, watchIdPhongBan]);

  useEffect(() => {
    if (isModalOpen) {
      setTitle(isView ? 'Xem thông tin KPI Cá nhân' : isUpdate ? 'Cập nhật KPI Cá nhân' : 'Thêm mới KPI Cá nhân');
      dispatch(getAllUser({ PageIndex: 1, PageSize: 2000 }));
    
      if (listCongThuc.data.length === 0) {
        dispatch(getListKpiCongThuc({})); 
      }
    }
  }, [isModalOpen, isUpdate, isView, dispatch, listCongThuc.data.length]);

  useEffect(() => {
    if (!$selected.data || users.length === 0 || !isModalOpen) return;
    const selectedData = $selected.data;
    const nhanSuOption = selectedData.idNhanSu ? userOptions.find((u) => u.value === selectedData.idNhanSu) : undefined;

    form.setFieldsValue({
      ...selectedData,
      idNhanSu: selectedData.idNhanSu,
      idPhongBan: selectedData.idPhongBan, 
      loaiKPI: selectedData.loaiKpi,
      idCongThuc: selectedData.idCongThuc,
      congThucTinh: selectedData.congThuc,
      namHoc: selectedData.namHoc ? dayjs(selectedData.namHoc, 'YYYY') : undefined,
    });
  }, [$selected.data, users, isModalOpen, userOptions, form]);

  useEffect(() => {
    if ($create.status === ReduxStatus.SUCCESS || $update.status === ReduxStatus.SUCCESS) {
      dispatch(resetStatusKpiCaNhan());
      dispatch(clearSeletedKpiCaNhan());
      form.resetFields();
      setIsModalOpen(false);
      props.onSuccess();
    }
  }, [$create.status, $update.status, dispatch, form, setIsModalOpen, props]);

  const handleClose = () => {
    form.resetFields();
    dispatch(clearSeletedKpiCaNhan());
    setIsModalOpen(false);
  };

  const handleFinish: FormProps['onFinish'] = async (values: any) => {
    const payload = {
      ...values,
      idNhanSu: values.idNhanSu?.value ?? values.idNhanSu,
      namHoc: values.namHoc.format('YYYY'),
      trongSo: values.trongSo?.toString() || '0',
    };
    try {
      if (isUpdate && $selected.data) {
        await dispatch(updateKpiCaNhan({ id: $selected.data.id, ...payload })).unwrap();
        toast.success('Cập nhật thành công');
      } else {
        await dispatch(createKpiCaNhan(payload)).unwrap();
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
          <div className="col-span-2 flex items-center gap-2 mb-2 text-blue-600 font-semibold">
            <InfoCircleOutlined /> THÔNG TIN CHỈ SỐ KPI
          </div>

          <Form.Item label="Tên KPI" name="kpi" className="col-span-2" rules={[{ required: true, message: 'Nhập tên KPI' }]}>
            <Input.TextArea rows={2} placeholder="Nhập tên chỉ số KPI cá nhân" />
          </Form.Item>

          <Form.Item label="Mục tiêu" name="mucTieu" rules={[{ required: true, message: 'Nhập mục tiêu' }]}>
            <Input placeholder="Ví dụ: Hoàn thành 100%..." />
          </Form.Item>

          <Form.Item label="Trọng số (%)" name="trongSo" rules={[{ required: true }]}>
            <InputNumber className="w-full" min={0} max={100} precision={2} placeholder="0.00" />
          </Form.Item>

          <Divider className="col-span-2 my-2" />

          <div className="col-span-2 flex items-center gap-2 mb-2 text-purple-600 font-semibold">
            <ExperimentOutlined /> CÔNG THỨC & ĐỊNH DẠNG
          </div>

          <Form.Item label="Loại KPI" name="loaiKPI" rules={[{ required: true }]}>
            <Select
              placeholder="Chọn loại"
              options={KpiLoaiConst.list.map(x => ({ value: x.value, label: x.name }))}
            />
          </Form.Item>

          <Form.Item label="Năm học" name="namHoc" rules={[{ required: true }]}>
            <DatePicker picker="year" format="YYYY" className="w-full" />
          </Form.Item>

          <Form.Item label="Công thức tính" name="idCongThuc" rules={[{ required: true }]}>
            <Select
              placeholder="Chọn công thức từ hệ thống"
              options={congThucOptions}
              loading={listCongThuc.status === ReduxStatus.LOADING}
              onChange={(value) => {
                const selected = listCongThuc.data.find(x => x.id === value);
                form.setFieldsValue({ congThucTinh: selected?.tenCongThuc });
              }}
            />
          </Form.Item>

          <Form.Item label="Loại kết quả" name="loaiKetQua" rules={[{ required: true }]}>
            <Select placeholder="Chọn loại kết quả" options={LOAI_KET_QUA_OPTIONS} />
          </Form.Item>

          {/* Trường ẩn để gửi tên công thức lên BE nếu BE vẫn yêu cầu cả text */}
          <Form.Item name="congThucTinh" hidden><Input /></Form.Item>

          <Divider className="col-span-2 my-2" />
          <div className="col-span-2 flex items-center gap-2 mb-2 text-green-600 font-semibold">
            <UserOutlined /> NHÂN SỰ THỰC HIỆN
          </div>

          <Form.Item label="Đơn vị / Phòng ban" name="idPhongBan" rules={[{ required: true }]}>
            <Select
              placeholder="Lọc theo đơn vị"
              allowClear
              options={listPhongBan?.map(x => ({ value: x.id, label: x.tenPhongBan }))}
              onChange={() => form.setFieldsValue({ idNhanSu: undefined, role: '' })}
            />
          </Form.Item>

          <Form.Item label="Nhân sự cụ thể" name="idNhanSu" rules={[{ required: true }]}>
            <UserSelect
              options={userOptions}
              loading={userStatus === ReduxStatus.LOADING}
              onChange={(value) => {
                const selectedUser = users.find((u) => u.id === value?.value);
                const roleEnum = KpiRoleConst.list.find((x) => x.name === selectedUser?.tenChucVu)?.value;
                form.setFieldsValue({ role: roleEnum ?? '' });
              }}
            />
          </Form.Item>

          <Form.Item label="Quyền hạn hệ thống (Role)" name="role" className="col-span-2">
            <Input disabled className="bg-gray-50 font-bold text-blue-600" placeholder="Tự động hiển thị theo nhân sự" />
          </Form.Item>
        </div>
      </Form>
    </Modal>
  );
};

export default PositionModal;