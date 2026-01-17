import React, { useState, useRef, useEffect } from 'react';
import { Button, Drawer, Input, List, Avatar, Spin, Tooltip, Typography, message } from 'antd';
import { RobotOutlined, SendOutlined, UserOutlined, DeleteOutlined, CopyOutlined, CheckOutlined } from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { askKpiAi } from '@redux/feature/kpi/kpiThunk';
import { addMessageToHistory, resetChat } from '@redux/feature/kpi/kpiSlice';

const { Text } = Typography;

const KpiAiChat: React.FC = () => {
    const [visible, setVisible] = useState(false);
    const [inputValue, setInputValue] = useState('');
    const [copiedIndex, setCopiedIndex] = useState<number | null>(null);
    const dispatch = useAppDispatch();
    const { history, status } = useAppSelector((state) => state.kpiState.kpiChat);
    const scrollRef = useRef<HTMLDivElement>(null);

    useEffect(() => {
        if (scrollRef.current) {
            scrollRef.current.scrollTo({
                top: scrollRef.current.scrollHeight,
                behavior: 'smooth'
            });
        }
    }, [history, status]);

    const handleSend = () => {
        if (!inputValue.trim() || status === ReduxStatus.LOADING) return;
        dispatch(addMessageToHistory({ role: 'user', content: inputValue }));
        dispatch(askKpiAi({ question: inputValue }));
        setInputValue('');
    };

    const handleClearChat = () => {
        dispatch(resetChat());
        message.success('Đã xóa lịch sử');
    };

    const handleCopy = async (content: string, index: number) => {
        try {
            await navigator.clipboard.writeText(content);
            setCopiedIndex(index);
            message.success('Đã sao chép!');
            setTimeout(() => setCopiedIndex(null), 2000);
        } catch (err) {
            message.error('Không thể sao chép');
        }
    };

    const parseMarkdownTable = (text: string) => {
        const lines = text.split('\n');
        const result: JSX.Element[] = [];
        let i = 0;
        let key = 0;

        while (i < lines.length) {
            const line = lines[i];

            if (line.trim().startsWith('|') && line.trim().endsWith('|')) {
                const tableLines: string[] = [];
                while (i < lines.length && lines[i].trim().startsWith('|') && lines[i].trim().endsWith('|')) {
                    tableLines.push(lines[i]);
                    i++;
                }

                if (tableLines.length >= 2) {
                    const headers = tableLines[0]
                        .split('|')
                        .map(h => h.trim())
                        .filter(h => h);

                    const dataRows = tableLines.slice(2).map(row =>
                        row.split('|')
                            .map(cell => cell.trim())
                            .filter(cell => cell)
                    );

                    result.push(
                        <div key={key++} style={{
                            overflowX: 'auto',
                            margin: '12px 0',
                            border: '1px solid #d9d9d9',
                            borderRadius: '4px'
                        }}>
                            <table style={{
                                width: '100%',
                                borderCollapse: 'collapse',
                                fontSize: '13px',
                                backgroundColor: '#fff'
                            }}>
                                <thead>
                                    <tr style={{ backgroundColor: '#fafafa', borderBottom: '1px solid #d9d9d9' }}>
                                        {headers.map((header, idx) => (
                                            <th key={idx} style={{
                                                padding: '10px 12px',
                                                textAlign: 'left',
                                                fontWeight: '600',
                                                color: '#262626'
                                            }}>
                                                {header}
                                            </th>
                                        ))}
                                    </tr>
                                </thead>
                                <tbody>
                                    {dataRows.map((row, rowIdx) => (
                                        <tr key={rowIdx} style={{
                                            borderBottom: rowIdx < dataRows.length - 1 ? '1px solid #f0f0f0' : 'none'
                                        }}>
                                            {row.map((cell, cellIdx) => (
                                                <td key={cellIdx} style={{
                                                    padding: '10px 12px',
                                                    color: '#595959'
                                                }}>
                                                    {cell}
                                                </td>
                                            ))}
                                        </tr>
                                    ))}
                                </tbody>
                            </table>
                        </div>
                    );
                }
            } else {
                if (line.trim()) {
                    result.push(
                        <div key={key++} style={{ marginBottom: '6px', lineHeight: '1.6' }}>
                            {line}
                        </div>
                    );
                } else {
                    result.push(<br key={key++} />);
                }
                i++;
            }
        }

        return result;
    };

    const ContentRenderer = ({ content }: { content: string }) => {
        return <div>{parseMarkdownTable(content)}</div>;
    };

    const WelcomeScreen = () => (
        <div style={{
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',
            justifyContent: 'center',
            height: '100%',
            padding: '40px 20px',
            textAlign: 'center'
        }}>
            <Avatar
                size={64}
                icon={<RobotOutlined />}
                style={{ backgroundColor: '#1677ff', marginBottom: 16 }}
            />

            <h3 style={{
                fontSize: 16,
                fontWeight: 600,
                color: '#262626',
                marginBottom: 8
            }}>
                KPI AI Assistant
            </h3>

            <p style={{
                color: '#8c8c8c',
                fontSize: 14,
                margin: 0
            }}>
                Hỏi tôi về KPI, phân tích và so sánh dữ liệu
            </p>
        </div>
    );

    return (
        <>
            <Tooltip title="Trợ lý KPI AI">
                <Button
                    type="primary"
                    shape="circle"
                    icon={<RobotOutlined style={{ fontSize: 20 }} />}
                    size="large"
                    onClick={() => setVisible(true)}
                    style={{
                        position: 'fixed',
                        bottom: 24,
                        right: 24,
                        zIndex: 999,
                        width: 56,
                        height: 56
                    }}
                />
            </Tooltip>

            <Drawer
                title={
                    <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                        <div style={{ display: 'flex', alignItems: 'center', gap: 10 }}>
                            <Avatar size={32} icon={<RobotOutlined />} style={{ backgroundColor: '#1677ff' }} />
                            <span style={{ fontWeight: 600, fontSize: 15 }}>KPI AI Assistant</span>
                        </div>
                        <Tooltip title="Xóa lịch sử">
                            <Button
                                type="text"
                                shape="circle"
                                size="small"
                                icon={<DeleteOutlined />}
                                onClick={handleClearChat}
                            />
                        </Tooltip>
                    </div>
                }
                placement="right"
                onClose={() => setVisible(false)}
                open={visible}
                width={480}
                styles={{
                    header: { padding: '14px 20px' },
                    body: { padding: 0, display: 'flex', flexDirection: 'column', backgroundColor: '#f5f5f5' },
                    footer: { padding: '14px 20px' }
                }}
                footer={
                    <div style={{ display: 'flex', gap: 8 }}>
                        <Input.TextArea
                            placeholder="Nhập câu hỏi về KPI..."
                            autoSize={{ minRows: 1, maxRows: 3 }}
                            value={inputValue}
                            onChange={(e) => setInputValue(e.target.value)}
                            onPressEnter={(e) => {
                                if (!e.shiftKey) {
                                    e.preventDefault();
                                    handleSend();
                                }
                            }}
                        />
                        <Button
                            type="primary"
                            icon={<SendOutlined />}
                            onClick={handleSend}
                            loading={status === ReduxStatus.LOADING}
                            disabled={!inputValue.trim()}
                        />
                    </div>
                }
            >
                <div ref={scrollRef} style={{ flex: 1, padding: '16px', overflowY: 'auto' }}>
                    {history.length === 0 && status !== ReduxStatus.LOADING ? (
                        <WelcomeScreen />
                    ) : (
                        <List
                            dataSource={history}
                            split={false}
                            locale={{ emptyText: <></> }}
                            renderItem={(item, index) => (
                                <div style={{
                                    display: 'flex',
                                    flexDirection: item.role === 'user' ? 'row-reverse' : 'row',
                                    marginBottom: 16,
                                    gap: 8
                                }}>
                                    <Avatar
                                        size={32}
                                        style={{
                                            backgroundColor: item.role === 'user' ? '#1677ff' : '#f0f0f0',
                                            flexShrink: 0
                                        }}
                                        icon={item.role === 'user'
                                            ? <UserOutlined style={{ color: '#fff' }} />
                                            : <RobotOutlined style={{ color: '#1677ff' }} />
                                        }
                                    />
                                    <div style={{
                                        background: item.role === 'user' ? '#1677ff' : '#ffffff',
                                        color: item.role === 'user' ? '#fff' : '#262626',
                                        padding: '10px 14px',
                                        borderRadius: '6px',
                                        maxWidth: '75%',
                                        fontSize: '14px',
                                        position: 'relative',
                                        border: item.role === 'user' ? 'none' : '1px solid #d9d9d9'
                                    }}>
                                        {item.role === 'user' ? (
                                            <div>{item.content}</div>
                                        ) : (
                                            <>
                                                <ContentRenderer content={item.content} />
                                                <Tooltip title={copiedIndex === index ? "Đã sao chép" : "Sao chép"}>
                                                    <Button
                                                        type="text"
                                                        size="small"
                                                        icon={copiedIndex === index
                                                            ? <CheckOutlined style={{ color: '#52c41a' }} />
                                                            : <CopyOutlined />
                                                        }
                                                        onClick={() => handleCopy(item.content, index)}
                                                        style={{
                                                            position: 'absolute',
                                                            bottom: 4,
                                                            right: 4,
                                                            fontSize: 12
                                                        }}
                                                    />
                                                </Tooltip>
                                            </>
                                        )}
                                    </div>
                                </div>
                            )}
                        />
                    )}

                    {status === ReduxStatus.LOADING && (
                        <div style={{ display: 'flex', gap: 8, marginBottom: 16 }}>
                            <Avatar
                                size={32}
                                icon={<RobotOutlined style={{ color: '#1677ff' }} />}
                                style={{ backgroundColor: '#f0f0f0' }}
                            />
                            <div style={{
                                background: '#fff',
                                padding: '10px 14px',
                                borderRadius: '6px',
                                border: '1px solid #d9d9d9'
                            }}>
                                <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
                                    <Spin size="small" />
                                    <Text type="secondary">Đang xử lý...</Text>
                                </div>
                            </div>
                        </div>
                    )}
                </div>
            </Drawer>
        </>
    );
};

export default KpiAiChat;