'use client';
import { useState, useEffect } from 'react';
import { Modal, Form, Select, Input, Button, Table, Divider } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { toast } from 'react-toastify';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { getAllUserByKpiRole } from '@redux/feature/userSlice';
import { getNhanSuDaGiaoByKpiDonVi, giaoKpiDonVi } from '@redux/feature/kpi/kpiThunk';
import { NhanSuDaGiaoDto } from '@models/kpi/kpi-don-vi.model';

interface Props {
    open: boolean;
    onClose: () => void;
    kpiId?: number;
    donViId?: number;
}
const AssignKpiModal = ({ open, onClose, kpiId, donViId }: Props) => {
    const dispatch = useAppDispatch();
    const { list: users = [] } = useAppSelector(state => state.userState.byKpiRole);

    const [assignForm] = Form.useForm();
    const [nhanSuDaGiao, setNhanSuDaGiao] = useState<NhanSuDaGiaoDto[]>([]);

    useEffect(() => {
        if (open && donViId) {
            dispatch(getAllUserByKpiRole({ IdPhongBan: donViId, PageIndex: 1, PageSize: 1000 }));
            if (kpiId) {
                dispatch(getNhanSuDaGiaoByKpiDonVi(kpiId))
                    .unwrap()
                    .then(data => setNhanSuDaGiao(data || []));
            }
        }
    }, [open, donViId, kpiId, dispatch]);

    const handleSubmitAssign = async () => {
        if (!kpiId) return;
        const payload = {
            idKpiDonVi: kpiId,
            nhanSus: nhanSuDaGiao.map(x => ({ idNhanSu: x.idNhanSu, trongSo: x.trongSo })),
        };

        if (!payload.nhanSus.length) return toast.warning('Chưa có nhân sự nào được giao');
        if (payload.nhanSus.some(x => !x.trongSo)) return toast.warning('Có nhân sự chưa nhập trọng số');

        try {
            await dispatch(giaoKpiDonVi(payload)).unwrap();
            toast.success('Giao KPI thành công');
            onClose();
            if (kpiId) dispatch(getNhanSuDaGiaoByKpiDonVi(kpiId));
        } catch (err) {
            console.error('GIAO KPI ERROR', err);
            toast.error('Giao KPI thất bại');
        }
    };

    return (
        <Modal
            open={open}
            title={`Giao KPI`}
            onCancel={onClose}
            width={700}
            footer={null}
        >
            <Form form={assignForm} layout="inline" className="mb-4">
                <Form.Item name="idNhanSu" rules={[{ required: true, message: 'Chọn nhân sự' }]}>
                    <Select
                        placeholder="Chọn nhân sự"
                        style={{ width: 260 }}
                        options={users.map(u => ({ value: u.id, label: `${u.hoDem} ${u.ten}` }))}
                    />
                </Form.Item>
                <Form.Item name="trongSo" rules={[{ required: true, message: 'Nhập trọng số' }]}>
                    <Input type="number" placeholder="Trọng số (%)" style={{ width: 150 }} />
                </Form.Item>
                <Button
                    type="primary"
                    icon={<PlusOutlined />}
                    onClick={() => {
                        assignForm.validateFields().then(values => {
                            if (nhanSuDaGiao.find(x => x.idNhanSu === values.idNhanSu)) {
                                toast.warning('Nhân sự này đã được giao');
                                return;
                            }
                            const user = users.find(u => u.id === values.idNhanSu);
                            setNhanSuDaGiao(prev => [
                                ...prev,
                                { idNhanSu: values.idNhanSu, hoTen: `${user?.hoDem} ${user?.ten}`, trongSo: values.trongSo.toString() }
                            ]);
                            assignForm.resetFields();
                        });
                    }}
                >
                    Thêm
                </Button>
            </Form>

            <Table
                rowKey="idNhanSu"
                dataSource={nhanSuDaGiao}
                pagination={false}
                columns={[
                    { title: 'Nhân sự', dataIndex: 'hoTen' },
                    { title: 'Trọng số', dataIndex: 'trongSo' },
                    {
                        title: 'Thao tác',
                        render: (_, r) => (
                            <Button
                                danger
                                size="small"
                                onClick={() => setNhanSuDaGiao(prev => prev.filter(x => x.idNhanSu !== r.idNhanSu))}
                            >
                                Xóa
                            </Button>
                        ),
                    },
                ]}
            />

            <Divider />
            <div className="flex justify-end">
                <Button type="primary" onClick={handleSubmitAssign}>
                    Lưu giao KPI
                </Button>
            </div>
        </Modal>
    );
};

export default AssignKpiModal;
