import { useEffect, useState } from 'react';
import { Button, Form, FormProps, Input, Modal, Select, Switch } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreateChuyenNganh, IUpdateChuyenNganh } from '@models/dao-tao/chuyenNganh.model';
import { getAllNganh } from '@redux/feature/dao-tao/nganhThunk';
import { createChuyenNganh, getChuyenNganhById, updateChuyenNganh } from '@redux/feature/dao-tao/chuyenNganhThunk';
import { clearSelectedChuyenNganh, resetStatusChuyenNganh } from '@redux/feature/dao-tao/daotaoSlice';
import { ReduxStatus } from '@redux/const';
import { toast } from 'react-toastify';

type SpecializationModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
  refreshData: () => void;
};

const SpecializationModal: React.FC<SpecializationModalProps> = (props) => {
  const dispatch = useAppDispatch();
  const [form] = Form.useForm<ICreateChuyenNganh>();
  const [title, setTitle] = useState<string>('');

  const { $selected, $create, $update } = useAppSelector((state) => state.daotaoState.chuyenNganh);
  const listNganh = useAppSelector((state) => state.daotaoState.listNganh);

  const isSaving = $create.status === ReduxStatus.LOADING || $update.status === ReduxStatus.LOADING;

  useEffect(() => {
    if (props.isModalOpen) {
      // Load list Nganh for dropdown
      dispatch(getAllNganh({ PageIndex: 1, PageSize: 100 }));

      if (props.isUpdate) setTitle('Chỉnh sửa chuyên ngành');
      else if (props.isView) setTitle('Chi tiết chuyên ngành');
      else setTitle('Thêm mới chuyên ngành');
    }
  }, [props.isModalOpen, props.isUpdate, props.isView]);

  useEffect(() => {
    if (props.isModalOpen && (props.isUpdate || props.isView) && $selected.id) {
      dispatch(getChuyenNganhById($selected.id));
    }
  }, [props.isModalOpen, props.isUpdate, props.isView, $selected.id]);

  useEffect(() => {
    if ($selected.data) {
      form.setFieldsValue($selected.data);
    }
  }, [$selected.data]);

  useEffect(() => {
    if ($create.status === ReduxStatus.SUCCESS || $update.status === ReduxStatus.SUCCESS) {
      dispatch(resetStatusChuyenNganh());
      dispatch(clearSelectedChuyenNganh());
      form.resetFields();
      props.refreshData();
      props.setIsModalOpen(false);
    }
  }, [$create.status, $update.status]);

  const handleClose = () => {
    form.resetFields();
    dispatch(clearSelectedChuyenNganh());
    props.setIsModalOpen(false);
  };

  const handleFinish: FormProps['onFinish'] = async (values: ICreateChuyenNganh | IUpdateChuyenNganh) => {
    try {
      if (props.isUpdate && $selected.id) {
        await dispatch(updateChuyenNganh({ id: $selected.id, ...values })).unwrap();
        toast.success('Cập nhật thành công');
      } else {
        await dispatch(createChuyenNganh(values)).unwrap();
        toast.success('Thêm mới thành công');
      }
    } catch (error) {
      toast.error('Đã xảy ra lỗi, vui lòng thử lại!');
    }
  };

  return (
    <Modal
      title={title}
      className="app-modal"
      width="60%"
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
        name="chuyenNganh"
        layout="vertical"
        form={form}
        onFinish={handleFinish}
        autoComplete="on"
        disabled={props.isView}
        labelCol={{ style: { fontWeight: 600 } }}
        initialValues={{ trangThai: true }}
      >
        <div className="grid grid-cols-2 gap-x-5">
          <Form.Item<ICreateChuyenNganh>
            label="Mã chuyên ngành"
            name="maChuyenNganh"
            rules={[{ required: true, message: 'Vui lòng nhập mã chuyên ngành' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item<ICreateChuyenNganh>
            label="Tên chuyên ngành"
            name="tenChuyenNganh"
            rules={[{ required: true, message: 'Vui lòng nhập tên chuyên ngành' }]}
          >
            <Input />
          </Form.Item>
          <Form.Item<ICreateChuyenNganh> label="Tên tiếng Anh" name="tenTiengAnh">
            <Input />
          </Form.Item>
          <Form.Item<ICreateChuyenNganh>
            label="Ngành"
            name="nganhId"
            rules={[{ required: true, message: 'Vui lòng chọn ngành' }]}
          >
            <Select
              placeholder="Chọn ngành"
              options={listNganh.map((nganh) => ({
                label: nganh.tenNganh,
                value: nganh.id
              }))}
              showSearch
              filterOption={(input, option) => (option?.label ?? '').toLowerCase().includes(input.toLowerCase())}
            />
          </Form.Item>
          <Form.Item<ICreateChuyenNganh> label="Mô tả" name="moTa" className="col-span-2">
            <Input.TextArea rows={3} />
          </Form.Item>
          <Form.Item<ICreateChuyenNganh> label="Trạng thái" name="trangThai" valuePropName="checked">
            <Switch checkedChildren="Hoạt động" unCheckedChildren="Ngừng" />
          </Form.Item>
        </div>
      </Form>
    </Modal>
  );
};

export default SpecializationModal;
