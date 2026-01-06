import { Modal, Form, Input } from 'antd';
import { KpiTrangThaiConst } from '../const/kpiStatus.const';
import { useState } from 'react';


type ConfirmScoredModalProps = {
    open: boolean;
    title?: string;
    trangThai: number;
    onCancel: () => void;
    onSubmit: (note?: string) => Promise<void>;
};

export default function ConfimScoredModal({
    open,
    title = 'Chấm KPI',
    trangThai,
    onCancel,
    onSubmit,
}: ConfirmScoredModalProps) {
    const [note, setNote] = useState('');

    return (
        <Modal
            title={title}
            open={open}
            okText="Xác nhận"
            cancelText="Hủy"
            onCancel={() => {
                setNote('');
                onCancel();
            }}
            onOk={async () => {
                console.log('Note:', note);
                await onSubmit(note);
                setNote('');
            }}
        >
            <Form layout="vertical">
                <Form.Item label="Trạng thái">
                    <Input value={KpiTrangThaiConst.get(trangThai)?.text} disabled />
                </Form.Item>

                <Form.Item label="Ghi chú">
                    <Input.TextArea
                        rows={4}
                        value={note}
                        onChange={(e) => setNote(e.target.value)}
                        placeholder="Nhập ghi chú (nếu có)"
                    />
                </Form.Item>
            </Form>
        </Modal>
    );
}
