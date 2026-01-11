import { useEffect, useMemo, useState } from 'react';
import { Button, DatePicker, Form, FormProps, Input, InputNumber, Modal, Select } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { clearSeletedKpiDonVi, resetStatusKpiDonVi } from '@redux/feature/kpi/kpiSlice';
import { createKpiDonVi, updateKpiDonVi } from '@redux/feature/kpi/kpiThunk';
import { ReduxStatus } from '@redux/const';
import { toast } from 'react-toastify';
import dayjs from 'dayjs';
import { getAllPhongBan, getAllPhongBanByKpiRole } from '@redux/feature/danh-muc/danhmucThunk';
import { ICreateKpiDonVi } from '@models/kpi/kpi-don-vi.model';
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
  const [title, setTitle] = useState<string>('Thêm mới Kpi Đơn vị');
  const { $selected, $create, $update } = useAppSelector((state) => state.kpiState.kpiDonVi);
  const isSaving = $create.status === ReduxStatus.LOADING || $update.status === ReduxStatus.LOADING;
  const { isModalOpen, isUpdate, isView, setIsModalOpen } = props;
  const { phongBanByKpiRole } = useAppSelector((state) => state.danhmucState);
  const selectedLoaiKpi = Form.useWatch('loaiKpi', form);
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
        setTitle(isView ? 'Xem thông tin KPI Đơn vị' : 'Cập nhật KPI Đơn vị');
      } else {
        setTitle('Thêm mới KPI đơn vị');
      }
    }
  }, [dispatch, isModalOpen, isUpdate, isView]);

  useEffect(() => {
    if (!$selected.data || phongBanByKpiRole.$list.data.length === 0) return;
    const selectedData = $selected.data;
    const donVi = phongBanByKpiRole.$list.data.find(
      pb => pb.id === selectedData.idDonVi
    );
    const congThucMatched = LIST_CONG_THUC.find(
      x => x.congThuc === selectedData.congThuc
    );
    form.setFieldsValue({
      ...selectedData,
      idDonVi: donVi
        ? { value: donVi.id, label: donVi.tenPhongBan }
        : undefined,
      loaiKPI: selectedData.loaiKpi,
      congThucTinh: congThucMatched?.congThuc,
      idCongThuc: congThucMatched?.id,
      namHoc: selectedData.namHoc
        ? dayjs(selectedData.namHoc, 'YYYY')
        : undefined,
    });
  }, [$selected.data, phongBanByKpiRole.$list.data]);

  useEffect(() => {
    if ($create.status === ReduxStatus.SUCCESS || $update.status === ReduxStatus.SUCCESS) {
      dispatch(resetStatusKpiDonVi());
      dispatch(clearSeletedKpiDonVi());
      form.resetFields();
      setIsModalOpen(false);
      props.onSuccess();
    }
  }, [$create.status, $update.status, dispatch, form, setIsModalOpen]);

  useEffect(() => {
    dispatch(getAllPhongBanByKpiRole({ PageIndex: 1, PageSize: 2000 }));
  }, [dispatch]);


  const handleClose = () => {
    form.resetFields();
    dispatch(clearSeletedKpiDonVi());
    setIsModalOpen(false);
  };

  const handleFinish: FormProps['onFinish'] = async (values: any) => {
    const payload = {
      ...values,
      idDonVi: values.idDonVi?.value ?? values.idDonVi,
      namHoc: values.namHoc.format('YYYY'),
      trongSo: values.trongSo != null ? values.trongSo.toString() : '0',
      idCongThuc: values.idCongThuc,
      congThucTinh: values.congThucTinh,
      loaiKetQua: values.loaiKetQua,
    };


    try {
      if (isUpdate && $selected.data) {
        await dispatch(
          updateKpiDonVi({ id: $selected.data.id, ...payload })
        ).unwrap();
        toast.success('Cập nhật KPI đơn vị thành công');
      } else {
        await dispatch(createKpiDonVi(payload)).unwrap();
        toast.success('Thêm mới KPI đơn vị thành công');
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
        name="kpi-don-vi-form"
        layout="vertical"
        form={form}
        onFinish={handleFinish}
        autoComplete="on"
        disabled={isView}
        labelCol={{ style: { fontWeight: 600 } }}
      >
        <div className="grid grid-cols-2 gap-x-5">
          <Form.Item<ICreateKpiDonVi>
            label="Tên KPI"
            name="kpi"
            rules={[{ required: true, message: 'Vui lòng nhập tên KPI' }]}
          >
            <Input />
          </Form.Item>

          {/* <Form.Item<ICreateKpiDonVi>
            label="Lĩnh Vực"
            name="linhVuc"
            rules={[{ required: true, message: 'Vui lòng nhập lĩnh vực' }]}
          >
            <Input />
          </Form.Item> */}


          <Form.Item<ICreateKpiDonVi>
            label="Mục tiêu"
            name="mucTieu"
            rules={[{ required: true, message: 'Vui lòng nhập mục tiêu' }]}
          >
            <Input />
          </Form.Item>

          <Form.Item<ICreateKpiDonVi>
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
            name="loaiKpi"
            rules={[{ required: true, message: 'Vui lòng chọn loại KPI' }]}
          >
            <Select
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

          <Form.Item<ICreateKpiDonVi>
            label="Đơn vị"
            name="idDonVi"
            rules={[{ required: true, message: 'Vui lòng chọn đơn vị' }]}
          >
            <Select
              options={phongBanByKpiRole.$list.data.map((pb) => ({
                value: pb.id,
                label: pb.tenPhongBan,
              }))}
              loading={phongBanByKpiRole.$list.status === ReduxStatus.LOADING}
              showSearch
              optionFilterProp="label"
              placeholder="Chọn đơn vị"
            />
          </Form.Item>


          <Form.Item<ICreateKpiDonVi>
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
