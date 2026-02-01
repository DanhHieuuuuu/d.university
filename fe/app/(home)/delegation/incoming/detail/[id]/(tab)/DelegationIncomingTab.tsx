'use client';

import React, { useMemo, forwardRef, useImperativeHandle, useEffect } from 'react';
import dayjs from 'dayjs';
import { Form, Input, InputNumber, DatePicker, Select } from 'antd';
import type { FormInstance } from 'antd';
import DetailTable, { DetailRow } from '@components/hieu-custom/detail-table';
import { getListNhanSu, getListPhongBan, updateDoanVao } from '@redux/feature/delegation/delegationThunk';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { renderField } from '@utils/render-field.helper';

type DelegationIncomingTabProps = {
  data: any;
  isEdit?: boolean;
  onUpdated?: () => void;
  onUpdatedSuccess?: () => void;
};

const REQUIRED_RULE = [{ required: true, message: 'Trường này không được để trống' }];

const DelegationIncomingTab = forwardRef<FormInstance, DelegationIncomingTabProps>(
  ({ data, isEdit = false, onUpdated, onUpdatedSuccess }, ref) => {
    const [form] = Form.useForm();
    const selectedPhongBan = Form.useWatch("idPhongBan", form);
    const dispatch = useAppDispatch();
    useImperativeHandle(ref, () => form);

    const listPhongBan = useAppSelector((state) => state.delegationState.listPhongBan);
    const listNhanSu = useAppSelector((state) => state.delegationState.listNhanSu);

    const onFinish = async (values: any) => {
      const formData = new FormData();

      formData.append('Id', data.id);
      formData.append('Code', values.code);
      formData.append('Name', values.name);
      formData.append('Content', values.content ?? null);
      formData.append('IdPhongBan', values.idPhongBan ?? null);
      formData.append('Location', values.location ?? null);
      formData.append('IdStaffReception', values.idStaffReception ?? null);
      formData.append('TotalMoney', values.totalMoney.toString());
      formData.append('TotalPerson', values.totalPerson.toString());
      formData.append('PhoneNumber', values.phoneNumber);

      formData.append('RequestDate', dayjs(values.requestDate).format('YYYY-MM-DD'));
      formData.append('ReceptionDate', dayjs(values.receptionDate).format('YYYY-MM-DD'));

      await dispatch(updateDoanVao(formData)).unwrap();
      onUpdated?.();
      onUpdatedSuccess?.();
    };

    useEffect(() => {
      if (!listPhongBan?.length) dispatch(getListPhongBan());
      if (!listNhanSu?.length) dispatch(getListNhanSu());
    }, [dispatch, listPhongBan, listNhanSu]);

    const rows: DetailRow[] = useMemo(() => {
      if (!data) return [];

      return [
        {
          label: 'Mã đoàn',
          value: renderField('code', data.code, <Input disabled />, {
            isEdit,
            rules: [{ required: true, message: 'Trường này không được để trống' }]
          })
        },
        {
          label: 'Tên đoàn vào',
          value: renderField('name', data.name, <Input />, {
            isEdit,
            rules: [{ required: true, message: 'Tên đoàn vào không được để trống' }]
          })
        },
        {
          label: 'Nội dung',
          full: true,
          value: renderField('content', data.content, <Input />, { isEdit })
        },
        {
          label: 'Phòng ban phụ trách',
          value: renderField(
            'idPhongBan',
            data.idPhongBan,
            <Select
              placeholder="Chọn phòng ban"
              options={listPhongBan.map((pb) => ({
                value: pb.idPhongBan,
                label: pb.tenPhongBan
              }))}
              onChange={() => {
                form.setFieldValue('idStaffReception', undefined);
              }}
            />,
            {
              isEdit,
              rules: REQUIRED_RULE,
              displayValueFormatter: (val) => listPhongBan.find((pb) => pb.idPhongBan === val)?.tenPhongBan ?? '-'
            }
          )
        },
        {
          label: 'Địa điểm',
          value: renderField('location', data.location, <Input />, { isEdit })
        },
        {
          label: 'Nhân sự tiếp đón',
          value: renderField(
            'idStaffReception',
            data.idStaffReception,
            <Select
              showSearch
              optionFilterProp="label"
              placeholder="Chọn nhân sự"
              options={listNhanSu
                .filter((ns) => ns.idPhongBan === selectedPhongBan)
                .map((ns) => ({
                  value: ns.idNhanSu,
                  label: ns.tenNhanSu
                }))}
            />,
            {
              isEdit,
              displayValueFormatter: (val) => listNhanSu.find((ns) => ns.idNhanSu === val)?.tenNhanSu ?? '-'
            }
          )
        },
        {
          label: 'Tổng chi phí ước tính (VNĐ)',
          value: renderField('totalMoney', data.totalMoney, <InputNumber style={{ width: '100%' }} />, { isEdit })
        },
        {
          label: 'Tổng số người',
          value: renderField('totalPerson', data.totalPerson, <InputNumber style={{ width: '100%' }} disabled />, {
            isEdit,
            rules: REQUIRED_RULE
          })
        },
        {
          label: 'SĐT liên hệ',
          value: renderField('phoneNumber', data.phoneNumber, <Input />, {
            isEdit,
            rules: [{ required: true, message: 'Số điện thoại liên hệ không được để trống' }]
          })
        },
        {
          label: 'Ngày yêu cầu',
          value: renderField('requestDate', dayjs(data.requestDate), <DatePicker style={{ width: '100%' }} />, {
            isEdit,
            rules: [{ required: true, message: 'Ngày yêu cầu không được để trống' }]
          })
        },
        {
          label: 'Ngày tiếp đón',
          value: renderField('receptionDate', dayjs(data.receptionDate), <DatePicker style={{ width: '100%' }} />, {
            isEdit,
            rules: [{ required: true, message: 'Ngày tiếp đón không được để trống' }]
          })
        }
      ];
    }, [data, isEdit, listPhongBan, listNhanSu, selectedPhongBan]);

    return (
      <Form
        form={form}
        layout="vertical"
        onFinish={onFinish}
        scrollToFirstError
      >
        <DetailTable rows={rows} />
      </Form>
    );
  }
);

DelegationIncomingTab.displayName = 'DelegationIncomingTab';
export default DelegationIncomingTab;
