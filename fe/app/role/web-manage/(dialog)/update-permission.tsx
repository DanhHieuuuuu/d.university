import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { Form, FormProps, Input, Modal, TreeSelect } from 'antd';
import { IUpdateRolePermission } from '@models/role';
import { RoleService } from '@services/role.service';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getDetailRole } from '@redux/feature/roleConfigSlice';

type RolePermissionModalProps = {
  isModalOpen: boolean;
  setIsModalOpen: (value: boolean) => void;
  refreshData: () => void;
};

const RolePermissionModal: React.FC<RolePermissionModalProps> = (props) => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const { selected } = useAppSelector((state) => state.roleConfigState);

  const [treeData, setTreeData] = useState<any[]>([]);

  useEffect(() => {
    const fetchData = async () => {
      if (props.isModalOpen && selected.id) {
        initPermission();
        await dispatch(getDetailRole(selected.id))
          .unwrap()
          .then((res) => {
            form.setFieldsValue(res);
          })
          .catch(() => toast.error('Không thể tải thông tin nhóm quyền'));
      } else {
        form.resetFields();
      }
    };

    fetchData();
  }, [props.isModalOpen]);

  const transformToTreeData = (items: any[]): any[] => {
    return items.map((item) => ({
      title: item.label,
      value: item.id,
      key: item.id,
      children: item.children && item.children.length > 0 ? transformToTreeData(item.children) : undefined
    }));
  };

  const initPermission = async () => {
    try {
      const res = await RoleService.getPermissionTree();
      const transformed = transformToTreeData(res.data || []);
      setTreeData(transformed);
    } catch (error) {
      console.error(error);
    }
  };

  const handleSubmit: FormProps<IUpdateRolePermission>['onFinish'] = async (values) => {
    const body: IUpdateRolePermission = {
      roleId: selected.id!,
      permissionIds: values.permissionIds
    };
    console.log('Submit:', body);
  };

  return (
    <Modal
      title="Cập nhật quyền cho vai trò"
      className="app-modal"
      width={700}
      open={props.isModalOpen}
      onCancel={() => props.setIsModalOpen(false)}
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
            treeDefaultExpandAll
            showCheckedStrategy={TreeSelect.SHOW_ALL}
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
