import React, { useState, useRef, useEffect } from 'react';
import { Button, Drawer, Input, List, Avatar, Spin, Tooltip } from 'antd';
import { RobotOutlined, SendOutlined, UserOutlined, DeleteOutlined } from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { askKpiAi } from '@redux/feature/kpi/kpiThunk';
import { addMessageToHistory, resetChat } from '@redux/feature/kpi/kpiSlice';

const KpiAiChat: React.FC = () => {
    const [visible, setVisible] = useState(false);
    const [inputValue, setInputValue] = useState('');
    const dispatch = useAppDispatch();
    const { history, status } = useAppSelector((state) => state.kpiState.kpiChat);
    const scrollRef = useRef<HTMLDivElement>(null);
    useEffect(() => {
        if (scrollRef.current) {
            scrollRef.current.scrollTop = scrollRef.current.scrollHeight;
        }
    }, [history]);

    const handleSend = () => {
        if (!inputValue.trim() || status === ReduxStatus.LOADING) return;
        dispatch(addMessageToHistory({ role: 'user', content: inputValue }));
        dispatch(askKpiAi({ question: inputValue }));

        setInputValue('');
    };

    const handleClearChat = () => {
        dispatch(resetChat());
    };

    return (
        <>
            {/* Nút bấm nổi ở góc màn hình */}
            <Tooltip title="Hỏi trợ lý KPI AI">
                <Button
                    type="primary"
                    shape="circle"
                    icon={<RobotOutlined style={{ fontSize: 24 }} />}
                    size="large"
                    onClick={() => setVisible(true)}
                    style={{
                        position: 'fixed',
                        bottom: 30,
                        right: 30,
                        zIndex: 999,
                        width: 60,
                        height: 60,
                        boxShadow: '0 4px 15px rgba(0,0,0,0.2)'
                    }}
                />
            </Tooltip>

            <Drawer
                title={
                    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                        <span><RobotOutlined /> Trợ lý KPI AI</span>
                        <Tooltip title="Xóa lịch sử chat">
                            <Button
                                type="text"
                                danger
                                icon={<DeleteOutlined />}
                                onClick={handleClearChat}
                            />
                        </Tooltip>
                    </div>
                }
                placement="right"
                onClose={() => setVisible(false)}
                open={visible}
                width={420}
                footer={
                    <div style={{ display: 'flex', gap: 8, padding: '8px 0' }}>
                        <Input
                            placeholder="Nhập câu hỏi về KPI của bạn..."
                            value={inputValue}
                            onChange={(e) => setInputValue(e.target.value)}
                            onPressEnter={handleSend}
                            disabled={status === ReduxStatus.LOADING}
                        />
                        <Button
                            type="primary"
                            icon={<SendOutlined />}
                            onClick={handleSend}
                            loading={status === ReduxStatus.LOADING}
                        />
                    </div>
                }
            >
                <div
                    ref={scrollRef}
                    style={{
                        height: '100%',
                        overflowY: 'auto',
                        display: 'flex',
                        flexDirection: 'column',
                        gap: 16
                    }}
                >
                    <List
                        dataSource={history}
                        split={false} // Ẩn đường kẻ chia dòng giữa các tin nhắn
                        locale={{ emptyText: <></> }} // Ẩn chữ "No Data" mặc định của Ant Design
                        renderItem={(item) => (
                            <div style={{
                                display: 'flex',
                                flexDirection: item.role === 'user' ? 'row-reverse' : 'row',
                                alignItems: 'start',
                                gap: 10,
                                marginBottom: 12
                            }}>
                                <Avatar
                                    style={{ backgroundColor: item.role === 'user' ? '#87d068' : '#1677ff', flexShrink: 0 }}
                                    icon={item.role === 'user' ? <UserOutlined /> : <RobotOutlined />}
                                />
                                <div style={{
                                    background: item.role === 'user' ? '#1677ff' : '#f5f5f5',
                                    color: item.role === 'user' ? 'white' : 'black',
                                    padding: '10px 14px',
                                    borderRadius: item.role === 'user' ? '18px 18px 0 18px' : '18px 18px 18px 0',
                                    maxWidth: '75%',
                                    wordBreak: 'break-word',
                                    userSelect: 'text',
                                    boxShadow: '0 2px 5px rgba(0,0,0,0.05)'
                                }}>
                                    {item.content}
                                </div>
                            </div>
                        )}
                    />

                    {status === ReduxStatus.LOADING && (
                        <div style={{ display: 'flex', gap: 10, alignItems: 'center' }}>
                            <Avatar icon={<RobotOutlined />} style={{ backgroundColor: '#1677ff' }} />
                            <Spin size="small" tip="AI đang phân tích dữ liệu..." />
                        </div>
                    )}

                    {history.length === 0 && (
                        <div style={{ textAlign: 'center', color: '#bfbfbf', marginTop: 40 }}>
                            <RobotOutlined style={{ fontSize: 40, marginBottom: 10 }} />
                            <p>Chào bạn! Hãy hỏi tôi bất cứ điều gì về KPI của bạn.</p>
                        </div>
                    )}
                </div>
            </Drawer>
        </>
    );
};

export default KpiAiChat;