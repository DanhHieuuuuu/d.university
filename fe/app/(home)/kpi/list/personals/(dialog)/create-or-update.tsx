import { useEffect, useMemo, useState } from 'react';
import { Button, DatePicker, Form, FormProps, Input, InputNumber, Modal, Select } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreateKpiCaNhan } from '@models/kpi/kpi-ca-nhan.model';
import UserSelect, { UserOption } from '@components/bthanh-custom/userSelect';
import { clearSeletedKpiCaNhan, resetStatusKpiCaNhan } from '@redux/feature/kpi/kpiSlice';
import { createKpiCaNhan, updateKpiCaNhan } from '@redux/feature/kpi/kpiThunk';
import { ReduxStatus } from '@redux/const';
import { toast } from 'react-toastify';
import dayjs from 'dayjs';
import { getAllUser } from '@redux/feature/userSlice';
import { KpiLoaiConst } from '@/constants/kpi/kpiType.const';
import { LIST_CONG_THUC } from '@/constants/kpi/kpiFormula.const';
import { LOAI_KET_QUA_OPTIONS } from '@/constants/kpi/loaiCongThuc.enum';
import { KpiRoleConst } from '@/constants/kpi/kpiRole.const';

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
  const [title, setTitle] = useState<string>('Thêm mới Kpi cá nhân');
  const { $selected, $create, $update } = useAppSelector((state) => state.kpiState.kpiCaNhan);
  const isSaving = $create.status === ReduxStatus.LOADING || $update.status === ReduxStatus.LOADING;
  const { isModalOpen, isUpdate, isView, setIsModalOpen } = props;
  const { list: users = [] } = useAppSelector(state => state.userState.byKpiRole);
  const status = useAppSelector((state) => state.userState.status);
  const selectedLoaiKpi = Form.useWatch('loaiKPI', form);
  const congThucOptions = useMemo(() => {
    if (!selectedLoaiKpi) return [];
    return LIST_CONG_THUC
      .filter(ct => ct.loaiKpiApDung.includes(selectedLoaiKpi))
      .map(ct => ({
        value: ct.id,
        label: ct.congThuc,
      }));
  }, [selectedLoaiKpi]);

  useEffect(() => {
    if (isModalOpen) {
      if (isUpdate || isView) {
        setTitle(isView ? 'Xem thông tin KPI Cá nhân' : 'Cập nhật KPI Cá nhân');
      } else {
        setTitle('Thêm mới KPI Cá nhân');
      }
    }
  }, [dispatch, isModalOpen, isUpdate, isView]);

  useEffect(() => {
    if (!$selected.data || users.length === 0) return;

    const selectedData = $selected.data;

    const nhanSuOption = selectedData.idNhanSu
      ? userOptions.find(u => u.value === selectedData.idNhanSu)
      : undefined;

    const congThucMatched = LIST_CONG_THUC.find(
      x => x.congThuc === selectedData.congThuc
    );

    form.setFieldsValue({
      ...selectedData,
      idNhanSu: nhanSuOption,
      loaiKPI: selectedData.loaiKpi,
      idCongThuc: congThucMatched?.id,
      congThucTinh: congThucMatched?.congThuc,
      role: selectedData?.role ?? '',
      namHoc: selectedData.namHoc
        ? dayjs(selectedData.namHoc, 'YYYY')
        : undefined,
    });
  }, [$selected.data, users]);

  useEffect(() => {
    if ($create.status === ReduxStatus.SUCCESS || $update.status === ReduxStatus.SUCCESS) {
      dispatch(resetStatusKpiCaNhan());
      dispatch(clearSeletedKpiCaNhan());
      form.resetFields();
      setIsModalOpen(false);
      props.onSuccess();
    }
  }, [$create.status, $update.status, dispatch, form, setIsModalOpen]);

  useEffect(() => {
    dispatch(getAllUser({ PageIndex: 1, PageSize: 2000 }));
  }, [dispatch]);

  const userOptions: UserOption[] = (users || []).map(u => ({
    value: u.id!,
    label: `${u.maNhanSu} - ${u.hoDem ?? ''} ${u.ten ?? ''} - ${u.tenPhongBan}- ${u.tenChucVu}`.trim(),
    searchText: `${u.maNhanSu} ${u.hoDem} ${u.ten} ${u.tenPhongBan}`
  }));

  const handleClose = () => {
    form.resetFields();
    dispatch(clearSeletedKpiCaNhan());
    setIsModalOpen(false);
  };

  const handleFinish: FormProps['onFinish'] = async (values: any) => {
    const payload = {
      ...values,
      idNhanSu: values.idNhanSu?.value,
      namHoc: values.namHoc.format('YYYY'),
      trongSo: values.trongSo != null ? values.trongSo.toString() : '0',
      idCongThuc: values.idCongThuc,
      congThucTinh: values.congThucTinh,
    };

    try {
      if (isUpdate && $selected.data) {
        await dispatch(
          updateKpiCaNhan({ id: $selected.data.id, ...payload })
        ).unwrap();
        toast.success('Cập nhật KPI cá nhân thành công');
      } else {
        await dispatch(createKpiCaNhan(payload)).unwrap();
        toast.success('Thêm mới KPI cá nhân thành công');
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
        name="kpi-ca-nhan-form"
        layout="vertical"
        form={form}
        onFinish={handleFinish}
        autoComplete="on"
        disabled={isView}
        labelCol={{ style: { fontWeight: 600 } }}
      >
        <div className="grid grid-cols-2 gap-x-5">
          <Form.Item<ICreateKpiCaNhan>
            label="Tên KPI"
            name="kpi"
            rules={[{ required: true, message: 'Vui lòng nhập tên KPI' }]}
          >
            <Input />
          </Form.Item>

          {/* <Form.Item<ICreateKpiCaNhan>
            label="Lĩnh Vực"
            name="linhVuc"
            rules={[{ required: true, message: 'Vui lòng nhập lĩnh vực' }]}
          >
            <Input />
          </Form.Item> */}


          <Form.Item<ICreateKpiCaNhan>
            label="Mục tiêu"
            name="mucTieu"
            rules={[{ required: true, message: 'Vui lòng nhập mục tiêu' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label="Trọng số"
            name="trongSo"
            rules={[
              { required: true, message: 'Vui lòng nhập trọng số' },
              {
                type: 'number',
                min: 0,
                max: 100,
                message: 'Trọng số phải là số ≥ 0',
              },
            ]}
          >
            <InputNumber
              className="!w-full"
              placeholder="Nhập trọng số"
              min={0}
              precision={2}
            />
          </Form.Item>


          <Form.Item
            label="Loại KPI"
            name="loaiKPI"
            rules={[{ required: true, message: 'Vui lòng chọn loại KPI' }]}
          >
            <Select
              placeholder="Chọn loại KPI"
              options={KpiLoaiConst.list.map(x => ({
                value: x.value,
                label: x.name,
              }))}
              onChange={() => {
                form.setFieldsValue({
                  idCongThuc: undefined,
                  congThucTinh: undefined,
                });
              }}
            />
          </Form.Item>

          <Form.Item
            label="Công thức tính KPI"
            name="idCongThuc"
            rules={[{ required: true, message: 'Vui lòng chọn công thức' }]}
          >
            <Select
              key={selectedLoaiKpi}
              placeholder={
                selectedLoaiKpi
                  ? 'Chọn công thức'
                  : 'Vui lòng chọn Loại KPI trước'
              }
              options={congThucOptions}
              disabled={isView || !selectedLoaiKpi}
              onChange={(value) => {
                const selected = LIST_CONG_THUC.find(x => x.id === value);
                form.setFieldsValue({
                  congThucTinh: selected?.congThuc,
                });
              }}
            />
          </Form.Item>
          <Form.Item
            label="Loại kết quả"
            name="loaiKetQua"
            rules={[{ required: true, message: 'Vui lòng chọn loại kết quả' }]}
          >
            <Select
              placeholder="Chọn loại kết quả"
              options={LOAI_KET_QUA_OPTIONS}
            />
          </Form.Item>
          <Form.Item
            name="congThucTinh"
            hidden
          >
            <Input />
          </Form.Item>


          <Form.Item<ICreateKpiCaNhan>
            label="Nhân sự"
            name="idNhanSu"
            rules={[{ required: true, message: 'Vui lòng chọn nhân sự' }]}
          >
            <UserSelect
              options={userOptions}
              loading={status === ReduxStatus.LOADING}
              onChange={(value) => {
                const selectedUser = users.find(u => u.id === value?.value);
                const roleEnum = KpiRoleConst.list.find(
                  x => x.name === selectedUser?.tenChucVu
                )?.value;
                form.setFieldsValue({
                  role: roleEnum ?? '',
                });
              }}
            />
          </Form.Item>
          <Form.Item<ICreateKpiCaNhan>
            label="Role"
            name="role"
          >
            <Input disabled />
          </Form.Item>


          <Form.Item<ICreateKpiCaNhan>
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
