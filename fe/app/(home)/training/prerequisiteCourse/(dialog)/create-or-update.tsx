import { useEffect, useState } from 'react';
import { Button, Form, FormProps, Input, Modal, Select } from 'antd';
import { CloseOutlined, PlusOutlined, SaveOutlined } from '@ant-design/icons';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { ICreateMonTienQuyet, IUpdateMonTienQuyet } from '@models/dao-tao/monTienQuyet.model';
import { getAllMonHoc } from '@redux/feature/dao-tao/monHocThunk';
import { createMonTienQuyet, getMonTienQuyetById, updateMonTienQuyet } from '@redux/feature/dao-tao/monTienQuyetThunk';
import { clearSelectedMonTienQuyet, resetStatusMonTienQuyet } from '@redux/feature/dao-tao/daotaoSlice';
import { ReduxStatus } from '@redux/const';
import { toast } from 'react-toastify';

type PrerequisiteCourseModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  isUpdate: boolean;
  isView: boolean;
  refreshData: () => void;
};

const LOAI_DIEU_KIEN_OPTIONS = [
  { label: 'Tiên quyết', value: 'Tiên quyết' },
  { label: 'Song hành', value: 'Song hành' },
  { label: 'Học trước', value: 'Học trước' }
];

const PrerequisiteCourseModal: React.FC<PrerequisiteCourseModalProps> = (props) => {
  const dispatch = useAppDispatch();
  const [form] = Form.useForm<ICreateMonTienQuyet>();
  const [title, setTitle] = useState<string>('');

  const { $selected, $create, $update } = useAppSelector((state) => state.daotaoState.monTienQuyet);
  const listMonHoc = useAppSelector((state) => state.daotaoState.listMonHoc);

  const isSaving = $create.status === ReduxStatus.LOADING || $update.status === ReduxStatus.LOADING;

  useEffect(() => {
    if (props.isModalOpen) {
      // Load list MonHoc for dropdown
      dispatch(getAllMonHoc({ PageIndex: 1, PageSize: 100 }));

      if (props.isUpdate) setTitle('Chỉnh sửa môn tiên quyết');
      else if (props.isView) setTitle('Chi tiết môn tiên quyết');
      else setTitle('Thêm mới môn tiên quyết');
    }
  }, [props.isModalOpen, props.isUpdate, props.isView]);

  useEffect(() => {
    if (props.isModalOpen && (props.isUpdate || props.isView) && $selected.id) {
      dispatch(getMonTienQuyetById($selected.id));
    }
  }, [props.isModalOpen, props.isUpdate, props.isView, $selected.id]);

  useEffect(() => {
    if ($selected.data) {
      form.setFieldsValue($selected.data);
    }
  }, [$selected.data]);

  useEffect(() => {
    if ($create.status === ReduxStatus.SUCCESS || $update.status === ReduxStatus.SUCCESS) {
      dispatch(resetStatusMonTienQuyet());
      dispatch(clearSelectedMonTienQuyet());
      form.resetFields();
      props.refreshData();
      props.setIsModalOpen(false);
    }
  }, [$create.status, $update.status]);

  const handleClose = () => {
    form.resetFields();
    dispatch(clearSelectedMonTienQuyet());
    props.setIsModalOpen(false);
  };

  const handleFinish: FormProps['onFinish'] = async (values: ICreateMonTienQuyet | IUpdateMonTienQuyet) => {
    try {
      if (props.isUpdate && $selected.id) {
        // Only send editable fields for update
        const updatePayload: IUpdateMonTienQuyet = {
          id: $selected.id,
          loaiDieuKien: values.loaiDieuKien,
          ghiChu: values.ghiChu
        };
        await dispatch(updateMonTienQuyet(updatePayload)).unwrap();
        toast.success('Cập nhật thành công');
      } else {
        await dispatch(createMonTienQuyet(values as ICreateMonTienQuyet)).unwrap();
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
        name="monTienQuyet"
        layout="vertical"
        form={form}
        onFinish={handleFinish}
        autoComplete="on"
        disabled={props.isView}
        labelCol={{ style: { fontWeight: 600 } }}
      >
        <div className="grid grid-cols-2 gap-x-5">
          <Form.Item<ICreateMonTienQuyet>
            label="Môn học"
            name="monHocId"
            rules={[{ required: true, message: 'Vui lòng chọn môn học' }]}
          >
            <Select
              placeholder="Chọn môn học"
              disabled={props.isUpdate || props.isView}
              options={listMonHoc.map((monHoc) => ({
                label: `${monHoc.maMonHoc} - ${monHoc.tenMonHoc}`,
                value: monHoc.id
              }))}
              showSearch
              filterOption={(input, option) => (option?.label ?? '').toLowerCase().includes(input.toLowerCase())}
            />
          </Form.Item>
          <Form.Item<ICreateMonTienQuyet>
            label="Môn tiên quyết"
            name="monTienQuyetId"
            rules={[{ required: true, message: 'Vui lòng chọn môn tiên quyết' }]}
          >
            <Select
              placeholder="Chọn môn tiên quyết"
              disabled={props.isUpdate || props.isView}
              options={listMonHoc.map((monHoc) => ({
                label: `${monHoc.maMonHoc} - ${monHoc.tenMonHoc}`,
                value: monHoc.id
              }))}
              showSearch
              filterOption={(input, option) => (option?.label ?? '').toLowerCase().includes(input.toLowerCase())}
            />
          </Form.Item>
          <Form.Item<ICreateMonTienQuyet>
            label="Loại điều kiện"
            name="loaiDieuKien"
            rules={[{ required: true, message: 'Vui lòng chọn loại điều kiện' }]}
          >
            <Select placeholder="Chọn loại điều kiện" options={LOAI_DIEU_KIEN_OPTIONS} />
          </Form.Item>
          <Form.Item<ICreateMonTienQuyet> label="Ghi chú" name="ghiChu" className="col-span-2">
            <Input.TextArea rows={3} />
          </Form.Item>
        </div>
      </Form>
    </Modal>
  );
};

export default PrerequisiteCourseModal;
