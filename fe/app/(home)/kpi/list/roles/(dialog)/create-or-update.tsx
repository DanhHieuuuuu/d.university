import { useEffect, useMemo, useState } from 'react';
import { Button, Form, FormProps, Input, Modal, Select } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreateKpiRole } from '@models/kpi/kpi-role.model';
import UserSelect, { UserOption } from '@components/bthanh-custom/userSelect';
import { createKpiRole, updateKpiRole } from '@redux/feature/kpi/kpiThunk';
import { clearSeletedKpiRole, resetStatusKpiRole } from '@redux/feature/kpi/kpiSlice';
import { ReduxStatus } from '@redux/const';
import { toast } from 'react-toastify';
import { getAllUser } from '@redux/feature/userSlice';
import { getAllPhongBan } from '@redux/feature/danh-muc/danhmucThunk';
import { KpiRoleConst } from '@/constants/kpi/kpiRole.const';

type PositionModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
};

const PositionModal: React.FC<PositionModalProps> = ({ isModalOpen, setIsModalOpen, isUpdate, isView }) => {
  const dispatch = useAppDispatch();
  const [form] = Form.useForm();

  const { $selected, $create, $update } = useAppSelector((state) => state.kpiState.kpiRole);
  const { list: users = [] } = useAppSelector((state) => state.userState.all);
  const status = useAppSelector((state) => state.userState.status);
  const { phongBan } = useAppSelector((state) => state.danhmucState);

  const isSaving = $create.status === ReduxStatus.LOADING || $update.status === ReduxStatus.LOADING;

  const title = useMemo(() => {
    if (isView) return 'Xem thông tin KPI Role';
    if (isUpdate) return 'Cập nhật KPI Role';
    return 'Thêm mới KPI Role';
  }, [isView, isUpdate]);

  useEffect(() => {
    if (!isModalOpen) return;
    dispatch(getAllUser({ PageIndex: 1, PageSize: 2000 }));
    dispatch(getAllPhongBan({ PageIndex: 1, PageSize: 2000 }));
  }, [isModalOpen]);

  const userOptions: UserOption[] = useMemo(
    () =>
      (users || []).map((u) => ({
        value: u.id!,
        label: `${u.maNhanSu} - ${u.hoDem ?? ''} ${u.ten ?? ''} - ${u.tenPhongBan}`.trim(),
        searchText: `${u.maNhanSu} ${u.hoDem} ${u.ten} ${u.tenPhongBan}`
      })),
    [users]
  );
  useEffect(() => {
    if (!$selected.data || users.length === 0) return;
    const ns = users.find((u) => u.id === $selected.data!.idNhanSu);
    form.setFieldsValue({
      ...$selected.data,
      idNhanSu: ns
        ? {
            value: ns.id,
            label: `${ns.maNhanSu} - ${ns.hoDem ?? ''} ${ns.ten ?? ''} - ${ns.tenPhongBan}`.trim()
          }
        : null
    });
  }, [$selected.data, users, form]);

  useEffect(() => {
    if ($create.status === ReduxStatus.SUCCESS || $update.status === ReduxStatus.SUCCESS) {
      dispatch(resetStatusKpiRole());
      dispatch(clearSeletedKpiRole());
      form.resetFields();
      setIsModalOpen(false);
    }
  }, [$create.status, $update.status]);
  const handleClose = () => {
    form.resetFields();
    dispatch(clearSeletedKpiRole());
    setIsModalOpen(false);
  };

  const handleFinish: FormProps['onFinish'] = async (values) => {
    const payload = {
      ...values,
      idNhanSu: typeof values.idNhanSu === 'object' ? values.idNhanSu.value : values.idNhanSu
    };

    try {
      if (isUpdate && $selected.data) {
        await dispatch(updateKpiRole({ id: $selected.data.id, ...payload })).unwrap();
        toast.success('Cập nhật KPI Role thành công');
      } else {
        await dispatch(createKpiRole(payload)).unwrap();
        toast.success('Thêm mới KPI Role thành công');
      }
    } catch (err: any) {
      toast.error(err);
    }
  };
  return (
    <Modal
      title={title}
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
          <Button onClick={handleClose} icon={<CloseOutlined />}>
            Đóng
          </Button>
        </>
      }
    >
      <Form layout="vertical" form={form} onFinish={handleFinish} disabled={isView}>
        <div className="grid grid-cols-2 gap-x-5">
          <Form.Item label="Nhân sự" name="idNhanSu" rules={[{ required: true, message: 'Vui lòng chọn nhân sự' }]}>
            <UserSelect options={userOptions} loading={status === ReduxStatus.LOADING} />
          </Form.Item>

          <Form.Item
            label="Đơn vị kiêm nhiệm"
            name="idDonVi"
            rules={[{ required: true, message: 'Vui lòng chọn đơn vị' }]}
          >
            <Select
              options={phongBan.$list.data.map((pb) => ({
                value: pb.id,
                label: pb.tenPhongBan
              }))}
              loading={phongBan.$list.status === ReduxStatus.LOADING}
              showSearch
              optionFilterProp="label"
              placeholder="Chọn đơn vị kiêm nhiệm"
            />
          </Form.Item>

          <Form.Item label="Tỉ lệ" name="tiLe" rules={[{ required: true, message: 'Vui lòng nhập tỉ lệ' }]}>
            <Input />
          </Form.Item>

          <Form.Item label="Chức vụ" name="role" rules={[{ required: true, message: 'Chọn chức vụ' }]}>
            <Select
              options={KpiRoleConst.list.map((x) => ({
                value: x.value,
                label: x.name
              }))}
            />
          </Form.Item>
        </div>
      </Form>
    </Modal>
  );
};

export default PositionModal;
