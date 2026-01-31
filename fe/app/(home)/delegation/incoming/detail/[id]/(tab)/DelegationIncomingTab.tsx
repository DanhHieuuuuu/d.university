'use client';  
 
import React, { useMemo, forwardRef, useImperativeHandle, useEffect } from 'react'; 
import dayjs from 'dayjs'; 
import { Form, Input, InputNumber, DatePicker, Select } from 'antd'; 
import type { FormInstance } from 'antd'; 
import DetailTable, { DetailRow } from '@components/hieu-custom/detail-table'; 
import { getListNhanSu, getListPhongBan, updateDoanVao } from '@redux/feature/delegation/delegationThunk'; 
import { useAppDispatch, useAppSelector } from '@redux/hooks'; 
import { toast } from 'react-toastify'; 
import { renderField } from '@utils/render-field.helper'; 
 
type DelegationIncomingTabProps = { 
  data: any; 
  isEdit?: boolean; 
  onUpdated?: () => void; 
}; 
 
const DelegationIncomingTab = forwardRef<FormInstance, DelegationIncomingTabProps>( 
  ({ data, isEdit = false, onUpdated }, ref) => { 
    const [form] = Form.useForm(); 
    const dispatch = useAppDispatch(); 
    useImperativeHandle(ref, () => form); 
    const listPhongBan = useAppSelector((state) => state.delegationState.listPhongBan); 
    const listNhanSu = useAppSelector((state) => state.delegationState.listNhanSu); 
    
    // Bước cuối cùng
    const onFinish = async (values: any) => { 
      try { 
        const formData = new FormData(); 
 
        formData.append('Id', data.id); 
        formData.append('Code', values.code ?? ''); 
        formData.append('Name', values.name ?? ''); 
        formData.append('Content', values.content ?? ''); 
        formData.append('IdPhongBan', values.idPhongBan ?? ''); 
        formData.append('Location', values.location ?? ''); 
        formData.append('IdStaffReception', values.idStaffReception ?? ''); 
        formData.append('TotalMoney', values.totalMoney?.toString() ?? '0'); 
        formData.append('TotalPerson', values.totalPerson?.toString() ?? '0'); 
        formData.append('PhoneNumber', values.phoneNumber ?? ''); 
 
        if (values.requestDate) { 
          formData.append('RequestDate', dayjs(values.requestDate).format('YYYY-MM-DD')); 
        } 
        if (values.receptionDate) { 
          formData.append('ReceptionDate', dayjs(values.receptionDate).format('YYYY-MM-DD')); 
        } 
 
        await dispatch(updateDoanVao(formData)).unwrap(); 
        onUpdated?.(); 
      } catch (err) {  
        throw err; 
      } 
    }; 
 
    useEffect(() => { 
      if (!listPhongBan || listPhongBan.length === 0) { 
        dispatch(getListPhongBan()); 
      } 
  
      if (!listNhanSu || listNhanSu.length === 0) { 
        dispatch(getListNhanSu()); 
      } 
    }, [dispatch, listPhongBan, listNhanSu]); 
 
    // danh sách cột
    const rows: DetailRow[] = useMemo(() => { 
      if (!data) return []; 
 
      const options = { isEdit: !!isEdit }; 
 
      return [ 
        { 
          label: 'Mã đoàn', 
          value: renderField('code', data.code, <Input disabled />, options) 
        }, 
        { 
          label: 'Tên đoàn vào', 
          value: renderField('name', data.name, <Input />, options) 
        }, 
        { 
          label: 'Nội dung', 
          value: renderField('content', data.content, <Input />, options), 
          full: true 
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
              allowClear 
            />, 
            { 
              isEdit, 
              displayValueFormatter: (val) => listPhongBan.find((pb) => pb.idPhongBan === val)?.tenPhongBan ?? '-' 
            } 
          ) 
        }, 
        { 
          label: 'Địa điểm', 
          value: renderField('location', data.location, <Input />, options) 
        }, 
        { 
          label: 'Nhân sự tiếp đón', 
          value: renderField( 
            'idStaffReception', 
            data.idStaffReception, 
            <Select 
              showSearch 
              placeholder="Chọn nhân sự" 
              optionFilterProp="label" 
              options={listNhanSu.map((ns) => ({ 
                value: ns.idNhanSu, 
                label: ns.tenNhanSu 
              }))} 
              allowClear 
            />, 
            { 
              isEdit, 
              displayValueFormatter: (val) => listNhanSu.find((ns) => ns.idNhanSu === val)?.tenNhanSu ?? '-' 
            } 
          ) 
        }, 
 
        { 
          label: 'Tổng chi phí ước tính (VNĐ)', 
          value: renderField('totalMoney', data.totalMoney, <InputNumber style={{ width: '100%' }} />, options) 
        }, 
        { 
          label: 'Tổng số người', 
          value: renderField('totalPerson', data.totalPerson, <InputNumber style={{ width: '100%' }} />, options) 
        }, 
        { 
          label: 'SĐT liên hệ', 
          value: renderField('phoneNumber', data.phoneNumber, <Input />, options) 
        }, 
        { 
          label: 'Ngày yêu cầu', 
          value: renderField( 
            'requestDate', 
            data.requestDate ? dayjs(data.requestDate) : undefined, 
            <DatePicker style={{ width: '100%' }} />, 
            options 
          ) 
        }, 
        { 
          label: 'Ngày tiếp đón', 
          value: renderField( 
            'receptionDate', 
            data.receptionDate ? dayjs(data.receptionDate) : undefined, 
            <DatePicker style={{ width: '100%' }} />,  
            options 
          ) 
        } 
      ]; 
    }, [data, isEdit, listNhanSu, listPhongBan]); 
 
    return ( 
      <Form form={form} layout="vertical" onFinish={onFinish}> 
        <DetailTable rows={rows} /> 
      </Form> 
    ); 
  } 
); 
 
DelegationIncomingTab.displayName = 'DelegationIncomingTab'; 
 
export default DelegationIncomingTab; 
