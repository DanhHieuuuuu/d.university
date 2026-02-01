'use client';

import React, { forwardRef, useImperativeHandle } from 'react';
import { Form, Input, Typography, Divider, Select, InputNumber } from 'antd';
import type { FormInstance } from 'antd';
import DetailTable, { DetailRow } from '@components/hieu-custom/detail-table';
import { renderField } from '@utils/render-field.helper';
import { IDetailDelegationIncoming } from '@models/delegation/delegation.model';
import { useAppDispatch } from '@redux/hooks';
import { updateDetailDelegation } from '@redux/feature/delegation/delegationThunk';

type DetailGuestGroupTabProps = {
  data: IDetailDelegationIncoming | null;
  isEdit?: boolean;
  onUpdated?: () => void;
  onUpdatedSuccess?: () => void;
};

const DetailGuestGroupTab = forwardRef<FormInstance, DetailGuestGroupTabProps>(
  ({ data, isEdit = false, onUpdated, onUpdatedSuccess }, ref) => {
    const [form] = Form.useForm();
    const dispatch = useAppDispatch();
    const currentYear = new Date().getFullYear();

    useImperativeHandle(ref, () => form);

    if (!data) return null;

    /* ================= THÔNG TIN ĐOÀN ================= */
    const delegationRows: DetailRow[] = [
      { label: 'Mã đoàn', value: data.delegationCode },
      { label: 'Tên đoàn', value: data.delegationName },
      { label: 'Tổng số thành viên', value: data.members?.length ?? 0 }
    ];

    /* ================= SUBMIT ================= */
    const onFinish = async (values: IDetailDelegationIncoming) => {
      const payloadItems = data.members.map((originMember, index) => {
        const editedMember = values.members?.[index] || {};

        return {
          id: originMember.id,
          delegationIncomingId: data.delegationIncomingId,
          code: editedMember.code ?? originMember.code,
          firstName: editedMember.firstName ?? originMember.firstName,
          lastName: editedMember.lastName ?? originMember.lastName,
          yearOfBirth: editedMember.yearOfBirth ?? originMember.yearOfBirth,
          phoneNumber: editedMember.phoneNumber ?? originMember.phoneNumber,
          email: editedMember.email ?? originMember.email,
          isLeader: editedMember.isLeader ?? originMember.isLeader
        };
      });

      await dispatch(updateDetailDelegation({ items: payloadItems })).unwrap();
      onUpdated?.();          // reload data
      onUpdatedSuccess?.();   // 👈 báo cha: OK
    };

    return (
      <Form form={form} layout="vertical" initialValues={data} onFinish={onFinish}>
        <DetailTable rows={delegationRows} />

        <Divider />

        {/* ===== DANH SÁCH THÀNH VIÊN ===== */}
        <Typography.Title level={5}>Danh sách thành viên</Typography.Title>

        {data.members?.length ? (
          data.members.map((member, index) => {
            const rows: DetailRow[] = [
              {
                label: 'Mã thành viên',
                value: renderField(['members', index, 'code'], member.code, <Input disabled/>, {isEdit, rules: [{ required: true, message: 'Mã thành viên không được để trống' }] })
              },
              {
                label: 'Họ',
                value: renderField(['members', index, 'firstName'], member.firstName, <Input />, {isEdit, rules: [{ required: true, message: 'Họ không được để trống' }] })
              },
              {
                label: 'Tên',
                value: renderField(['members', index, 'lastName'], member.lastName, <Input />, {isEdit, rules: [{ required: true, message: 'Tên không được để trống' }] })
              },
              {
                label: 'Năm sinh',
                value: renderField(['members', index, 'yearOfBirth'], member.yearOfBirth.toString(), <Input />, 
                {isEdit, rules: [{ required: true, message: 'Năm sinh không được để trống' }] }
              )
              },

              {
                label: 'Số điện thoại',
                value: renderField(['members', index, 'phoneNumber'], member.phoneNumber, <Input />, 
                  {isEdit, rules: [{ required: true, message: 'Số điện thoại không được để trống' }] }
                )
              },
              {
                label: 'Email',
                value: renderField(['members', index, 'email'], member.email, <Input />, 
                  {isEdit, rules: [{ required: true, message: 'Email không được để trống' }] }
                )
              },
              {
                label: 'Trưởng đoàn',
                value: isEdit ? (
                  <Form.Item name={['members', index, 'isLeader']} style={{ marginBottom: 0 }}>
                    <Select
                      options={[
                        { label: 'Có', value: true },
                        { label: 'Không', value: false }
                      ]}
                    />
                  </Form.Item>
                ) : member.isLeader ? (
                  'Có'
                ) : (
                  'Không'
                )
              }
            ];

            return (
              <div key={member.id} style={{ marginBottom: 24 }}>
                <DetailTable rows={rows} />
                <Divider />
              </div>
            );
          })
        ) : (
          <Typography.Text type="secondary">Không có dữ liệu thành viên tiếp đoàn</Typography.Text>
        )}
        {/* ===== PHÒNG BAN HỖ TRỢ ===== */}
        <Typography.Title level={5}>Phòng ban hỗ trợ</Typography.Title>

        {data.departmentSupports?.length ? (
          data.departmentSupports.map((dept) => {
            const rows: DetailRow[] = [
              {
                label: 'Tên phòng ban',
                value: dept.departmentSupportName
              },
              {
                label: 'Nhân sự hỗ trợ',
                value: dept.supporters?.length ? (
                  <div>
                    {dept.supporters.map((sp) => (
                      <div key={sp.id}>
                        {sp.supporterName} ({sp.supporterCode})
                      </div>
                    ))}
                  </div>
                ) : (
                  <Typography.Text type="secondary">Không có nhân sự hỗ trợ</Typography.Text>
                )
              }
            ];

            return (
              <div key={dept.departmentSupportId} style={{ marginBottom: 24 }}>
                <DetailTable rows={rows} />
                <Divider />
              </div>
            );
          })
        ) : (
          <Typography.Text type="secondary">Không có dữ liệu phòng ban hỗ trợ</Typography.Text>
        )}
      </Form>
    );
  }
);

DetailGuestGroupTab.displayName = 'DetailGuestGroupTab';

export default DetailGuestGroupTab;
