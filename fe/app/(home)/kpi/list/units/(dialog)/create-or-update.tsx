import { useEffect, useState } from 'react';
import { Button, DatePicker, Form, FormProps, Input, InputNumber, Modal, Select } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { clearSeletedKpiDonVi, resetStatusKpiDonVi } from '@redux/feature/kpi/kpiSlice';
import { createKpiDonVi, updateKpiDonVi } from '@redux/feature/kpi/kpiThunk';
import { ReduxStatus } from '@redux/const';
import { toast } from 'react-toastify';
import dayjs from 'dayjs';
import { KpiLoaiConst } from '../../../const/kpiType.const';
import { getAllPhongBan } from '@redux/feature/danh-muc/danhmucThunk';
import { ICreateKpiDonVi } from '@models/kpi/kpi-don-vi.model';

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
  const { phongBan } = useAppSelector((state) => state.danhmucState);

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
    if (!$selected.data || phongBan.$list.data.length === 0) return;

    const selectedData = $selected.data;

    const donVi = phongBan.$list.data.find(
      pb => pb.id === selectedData.idDonVi
    );

    form.setFieldsValue({
      ...selectedData,
      idDonVi: donVi
        ? { value: donVi.id, label: donVi.tenPhongBan }
        : undefined,
      namHoc: selectedData.namHoc
        ? dayjs(selectedData.namHoc, 'YYYY')
        : undefined,
    });
  }, [$selected.data, phongBan.$list.data]);

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
    dispatch(getAllPhongBan({ PageIndex: 1, PageSize: 2000 }));
  }, [dispatch]);


  const handleClose = () => {
    form.resetFields();
    dispatch(clearSeletedKpiDonVi());
    setIsModalOpen(false);
  };

  const handleFinish: FormProps['onFinish'] = async (values: any) => {
    const payload = {
      ...values,
      idDonVi: values.idDonVi,
      namHoc: values.namHoc.format('YYYY'),
    };


    try {
      if (isUpdate && $selected.data) {
        await dispatch(
          updateKpiDonVi({ id: $selected.data.id, ...payload })
        ).unwrap();
        toast.success('Cập nhật KPI đơn vị thành công');
      } else {
        console.log("Payload: ",payload);
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


          <Form.Item<ICreateKpiDonVi>
            label="Đơn vị"
            name="idDonVi"
            rules={[{ required: true, message: 'Vui lòng chọn đơn vị' }]}
          >
            <Select
              options={phongBan.$list.data.map((pb) => ({
                value: pb.id,
                label: pb.tenPhongBan,
              }))}
              loading={phongBan.$list.status === ReduxStatus.LOADING}
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
