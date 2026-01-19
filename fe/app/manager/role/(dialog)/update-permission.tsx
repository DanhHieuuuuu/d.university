import { useEffect, useMemo } from 'react';
import { toast } from 'react-toastify';
import { Form, FormProps, Input, Modal, TreeSelect } from 'antd';
import { IUpdateRolePermission } from '@models/role';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getDetailRole, resetStatusRole, updateRolePermisison } from '@redux/feature/roleConfigSlice';
import {
  extractLeafPermissions,
  normalizePermissionIds
} from '@utils/permisson.utils';

type RolePermissionModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  refreshData: () => void;
};

const RolePermissionModal: React.FC<RolePermissionModalProps> = (props) => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { selected, permissionTree } = useAppSelector((state) => state.roleConfigState);

  useEffect(() => {
    if (!props.isModalOpen || !selected.id) {
      form.resetFields();
      return;
    }

    dispatch(getDetailRole(selected.id))
      .unwrap()
      .then((res) => {
        const leafPermissionIds = extractLeafPermissions(res.permissionIds, permissionTree);

        form.setFieldsValue({
          ...res,
          permissionIds: leafPermissionIds
        });
      })
      .catch(() => {
        toast.error('Không thể tải thông tin nhóm quyền');
      });
  }, [props.isModalOpen, selected.id, dispatch, form]);

  // chuyển đổi data phù hợp với TreeSelect AntD
  const treeData = useMemo(() => {
    const transform = (items: any[]): any[] =>
      items.map((item) => ({
        title: item.label,
        value: item.id,
        key: item.id,
        children: item.children?.length ? transform(item.children) : undefined
      }));

    return permissionTree?.length ? transform(permissionTree) : [];
  }, [permissionTree]);

  const handleCloseModal = () => {
    dispatch(resetStatusRole());
    form.resetFields();
    props.setIsModalOpen(false);
  };

  const handleSubmit: FormProps<IUpdateRolePermission>['onFinish'] = async (values) => {
    if (!selected.id) return;

    const permissionIds = normalizePermissionIds(values.permissionIds, permissionTree);

    const body: IUpdateRolePermission = {
      roleId: selected.id,
      permissionIds
    };

    try {
      const res = await dispatch(updateRolePermisison(body)).unwrap();
      if (res != undefined) {
        toast.success(res?.message || `Đã cập nhật quyền cho nhóm ${selected.data?.name}`);
      }

      props.refreshData();
      handleCloseModal();
    } catch (error: any) {
      toast.error(error?.message || `Không thể cập nhật quyền cho nhóm ${selected.data?.name}`);
    }
  };

  return (
    <Modal
      title="Cập nhật quyền cho vai trò"
      className="app-modal"
      width={700}
      open={props.isModalOpen}
      onCancel={handleCloseModal}
      onOk={() => form.submit()}
      okText="Lưu"
      cancelText="Hủy"
    >
      <Form form={form} layout="vertical" onFinish={handleSubmit}>
        <Form.Item name="name" label="Tên nhóm quyền">
          <Input disabled />
        </Form.Item>
        <Form.Item<IUpdateRolePermission> name="permissionIds" label="Các quyền">
          <TreeSelect
            treeLine
            treeCheckable
            allowClear
            maxTagCount="responsive"
            showCheckedStrategy={TreeSelect.SHOW_PARENT}
            placeholder="Chọn quyền"
            treeData={treeData}
            style={{ width: '100%' }}
          />
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default RolePermissionModal;
