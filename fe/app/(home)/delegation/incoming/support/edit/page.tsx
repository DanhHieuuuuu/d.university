'use client';

import React, { useEffect, useMemo, useState } from 'react';
import { Form, Input, Button, Card, Select } from 'antd';
import { toast } from 'react-toastify';
import { useRouter, useSearchParams } from 'next/navigation';
import { ArrowLeftOutlined, EditOutlined } from '@ant-design/icons';
import DetailTable, { DetailRow } from '@components/hieu-custom/detail-table';
import { getListNhanSu } from '@redux/feature/delegation/delegationThunk';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { IUpdateDepartmentSupport } from '@models/delegation/delegation.model';
import { renderField } from '@utils/render-field.helper';
import {
  getByIdDepartmentSupport,
  updateDepartmentSupport
} from '@redux/feature/delegation/department/departmentThunk';

const DepartmentSupport = () => {
  const [form] = Form.useForm();
  const dispatch = useAppDispatch();
  const searchParams = useSearchParams();
  const departmentSupportId = searchParams.get('departmentSupportId');
  const { listNhanSu } = useAppSelector((state) => state.delegationState);
  const [detail, setDetail] = useState<any>(null);
  const [isEdit, setIsEdit] = useState(false);
  const router = useRouter();

  /* ===== Load detail ===== */
  useEffect(() => {
    if (!departmentSupportId) return;

    dispatch(getByIdDepartmentSupport(Number(departmentSupportId)))
      .unwrap()
      .then(setDetail)
      .catch(() => toast.error('Không lấy được dữ liệu'));
  }, [departmentSupportId]);

  /* ===== Bind form only when edit ===== */
  useEffect(() => {
    if (!detail || !isEdit) return;

    form.setFieldsValue({
      content: detail.content,
      supporters: detail.supporters?.map((s: any) => ({
        supporterId: s.supporterId,
        supporterCode: s.supporterCode
      }))
    });
  }, [detail, isEdit]);

  useEffect(() => {
    if (!listNhanSu || listNhanSu.length === 0) {
      dispatch(getListNhanSu());
    }
    console.log('listNs', listNhanSu);
  }, [listNhanSu, dispatch]);

  /* ===== Save ===== */
  const onFinish = async (values: any) => {
    try {
      const payload: IUpdateDepartmentSupport = {
        id: detail.id,
        departmentSupportId: detail.departmentSupportId,
        delegationIncomingId: detail.delegationIncomingId,
        content: values.content,
        supporters: (values.supporters ?? []).map((s: any) => ({
          supporterId: s.supporterId,
          supporterCode: s.supporterCode?.trim()
        }))
      };

      await dispatch(updateDepartmentSupport(payload)).unwrap();
      toast.success('Cập nhật thành công');

      setIsEdit(false);

      // reload detail
      dispatch(getByIdDepartmentSupport(Number(departmentSupportId)))
        .unwrap()
        .then(setDetail);
    } catch (err) {
      toast.error(String(err));
    }
  };
  // View and edit
  const rows: DetailRow[] = useMemo(() => {
    if (!detail) return [];
    const options = { isEdit };

    return [
      {
        label: 'Phòng ban hỗ trợ',
        value: renderField('departmentSupportName', detail.departmentSupportName, <Input disabled />, options)
      },
      {
        label: 'Đoàn vào',
        value: renderField('delegationIncomingName', detail.delegationIncomingName, <Input disabled />, options)
      },
      {
        label: 'Nội dung hỗ trợ',
        value: renderField('content', detail.content, <Input />, options),
        full: true
      },

      /* ===== Nhân sự hỗ trợ ===== */
      {
        label: 'Nhân sự hỗ trợ',
        full: true,
        value: !isEdit ? (
          renderField('supporters', detail.supporters, <Input />, {
            isEdit,
            displayValueFormatter: (supporters: any[]) => {
              if (!supporters?.length) return '----';

              return (
                <div style={{ lineHeight: 1.8 }}>
                  {supporters.map((s, i) => (
                    <div key={s.supporterId}>
                      {i + 1}. {s.supporterCode}
                    </div>
                  ))}
                </div>
              );
            }
          })
        ) : (
          <Form.List name="supporters">
            {(fields, { add, remove }) => {
              // LẤY DANH SÁCH supporterId ĐÃ CHỌN
              const selectedIds =
                form
                  .getFieldValue('supporters')
                  ?.map((s: any) => s?.supporterId)
                  .filter(Boolean) ?? [];

              return (
                <>
                  {fields.map(({ key, name }) => (
                    <div key={key} style={{ display: 'flex', gap: 8, marginBottom: 8 }}>
                      <Form.Item
                        name={[name, 'supporterId']}
                        rules={[{ required: true, message: 'Chọn nhân sự' }]}
                        style={{ flex: 2, marginBottom: 0, minWidth: 0 }}
                      >
                        <Select
                          placeholder="Chọn nhân sự"
                          showSearch
                          optionFilterProp="label"
                          options={listNhanSu.map((n) => ({
                            value: n.idNhanSu,
                            label: n.tenNhanSu,
                            disabled: selectedIds.includes(n.idNhanSu)
                          }))}
                          onChange={(value) => {
                            const selectedNhanSu = listNhanSu.find((n) => n.idNhanSu === value);

                            // set supporterId
                            form.setFieldValue(['supporters', name, 'supporterId'], value);

                            // auto fill supporterCode
                            form.setFieldValue(
                              ['supporters', name, 'supporterCode'],
                              selectedNhanSu?.supporterCode ?? ''
                            );
                          }}
                        />
                      </Form.Item>

                      <Form.Item
                        name={[name, 'supporterCode']}
                        rules={[{ required: true, message: 'Nhập mã NV' }]}
                        style={{ flex: 1, marginBottom: 0, minWidth: 0 }}
                      >
                        <Input placeholder="Mã NV" />
                      </Form.Item>

                      <Button danger onClick={() => remove(name)}>
                        Xoá
                      </Button>
                    </div>
                  ))}

                  <Button type="dashed" onClick={() => add()}>
                    + Thêm người hỗ trợ
                  </Button>
                </>
              );
            }}
          </Form.List>
        )
      }
    ];
  }, [detail, isEdit, listNhanSu]);

  return (
    <Card
      variant="borderless"
      className="h-full"
      title={
        <div style={{ display: 'flex', alignItems: 'center', gap: 10 }}>
          <div
            onClick={() => router.back()}
            style={{
              display: 'inline-flex',
              alignItems: 'center',
              justifyContent: 'center',
              width: 34,
              height: 34,
              borderRadius: 6,
              cursor: 'pointer',
              transition: '0.2s'
            }}
            className="hover-box"
          >
            <ArrowLeftOutlined />
          </div>
          <span>Chi tiết phòng ban hỗ trợ</span>
        </div>
      }
      extra={
        !isEdit ? (
          <Button type="primary" icon={<EditOutlined />} onClick={() => setIsEdit(true)}>
            Chỉnh sửa
          </Button>
        ) : (
          <div style={{ display: 'flex', gap: 8 }}>
            <Button
              onClick={() => {
                setIsEdit(false);
                form.resetFields();
              }}
            >
              Huỷ
            </Button>
            <Button type="primary" onClick={() => form.submit()}>
              Lưu
            </Button>
          </div>
        )
      }
    >
      {!isEdit ? (
        <DetailTable rows={rows} />
      ) : (
        <Form form={form} layout="vertical" onFinish={onFinish}>
          <DetailTable rows={rows} />
        </Form>
      )}
    </Card>
  );
};

export default DepartmentSupport;
