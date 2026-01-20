import React, { useState, useRef, useEffect } from 'react';
import { Button, Drawer, Input, List, Avatar, Spin, Tooltip, Typography, message } from 'antd';
import { RobotOutlined, SendOutlined, UserOutlined, DeleteOutlined, CopyOutlined, CheckOutlined } from '@ant-design/icons';
import { ReduxStatus } from '@redux/const';
import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { askKpiAi } from '@redux/feature/kpi/kpiThunk';
import { addMessageToHistory, resetChat } from '@redux/feature/kpi/kpiSlice';
import type { InputRef } from 'antd';

const { Text } = Typography;

const KpiAiChat: React.FC = () => {
    const [visible, setVisible] = useState(false);
    const [inputValue, setInputValue] = useState('');
    const [copiedIndex, setCopiedIndex] = useState<number | null>(null);
    const dispatch = useAppDispatch();
    const { history, status } = useAppSelector((state) => state.kpiState.kpiChat);
    const scrollRef = useRef<HTMLDivElement>(null);
    const inputRef = useRef<InputRef>(null);
    useEffect(() => {
        if (visible && scrollRef.current) {
            setTimeout(() => {
                scrollRef.current?.scrollTo({ top: scrollRef.current.scrollHeight, behavior: 'smooth' });
                inputRef.current?.focus({ cursor: 'end' });
            }, 100);
        }
    }, [history.length, status, visible]);

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

    const handleCopy = (content: string, index: number) => {
        navigator.clipboard.writeText(content);
        setCopiedIndex(index);
        message.success('Đã sao chép');
        setTimeout(() => setCopiedIndex(null), 2000);
    };
    const formatText = (text: string) => {
        const parts = text.split(/(\*\*.*?\*\*)/g); 
        return parts.map((part, index) => {
            if (part.startsWith('**') && part.endsWith('**')) {
                return <span key={index} style={{ fontWeight: 'bold' }}>{part.slice(2, -2)}</span>;
            }
            return part;
        });
    };

    const renderMessageContent = (text: string) => {
        if (!text) return null;
        const lines = text.split('\n');
        const result: JSX.Element[] = [];
        let i = 0;

        while (i < lines.length) {
            const line = lines[i].trim();
            if (line.startsWith('|') && line.endsWith('|')) {
                const tableRows: string[] = [];
                while (i < lines.length && lines[i].trim().startsWith('|') && lines[i].trim().endsWith('|')) {
                    tableRows.push(lines[i].trim()); i++;
                }
                
                if (tableRows.length >= 2) {
                    const headers = tableRows[0].split('|').filter(c => c).map(c => c.trim());
                    const bodyRows = tableRows.slice(2).map(row => row.split('|').filter(c => c !== undefined && c !== '').map(c => c.trim()));
                    
                    result.push(
                        <div key={`tbl-${i}`} style={{ overflowX: 'auto', margin: '10px 0', border: '1px solid #d9d9d9', borderRadius: '8px' }}>
                            <table style={{ width: '100%', borderCollapse: 'collapse', fontSize: '13px', backgroundColor: '#fff' }}>
                                <thead style={{ backgroundColor: '#fafafa' }}>
                                    <tr>
                                        {headers.map((h, idx) => (
                                            <th key={idx} style={{ padding: '8px 12px', borderBottom: '1px solid #f0f0f0', borderRight: '1px solid #f0f0f0', fontWeight: 'bold', color: '#1f1f1f', whiteSpace: 'nowrap' }}>
                                                {formatText(h)}
                                            </th>
                                        ))}
                                    </tr>
                                </thead>
                                <tbody>
                                    {bodyRows.map((row, rIdx) => (
                                        <tr key={rIdx} style={{ borderBottom: rIdx === bodyRows.length - 1 ? 'none' : '1px solid #f0f0f0' }}>
                                            {row.map((cell, cIdx) => (
                                                <td key={cIdx} style={{ padding: '8px 12px', borderRight: cIdx === row.length - 1 ? 'none' : '1px solid #f0f0f0', color: '#595959', verticalAlign: 'top' }}>
                                                    {formatText(cell)}
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
                if (line) {
                    if (line.startsWith('* ') || line.startsWith('- ')) {
                         result.push(
                            <div key={`li-${i}`} style={{ display: 'flex', gap: '8px', marginLeft: '4px', marginBottom: '4px' }}>
                                <span style={{ color: '#8c8c8c' }}>•</span>
                                <span style={{ flex: 1, lineHeight: 1.6 }}>{formatText(line.substring(2))}</span>
                            </div>
                        );
                    } else {
                        result.push(
                            <div key={`line-${i}`} style={{ minHeight: '22px', marginBottom: '4px', lineHeight: 1.6, whiteSpace: 'pre-wrap' }}>
                                {formatText(lines[i])}
                            </div>
                        );
                    }
                } else {
                    result.push(<div key={`br-${i}`} style={{ height: '8px' }} />);
                }
                i++;
            }
        }
        return result;
    };

    return (
        <>
            <Tooltip title="Trợ lý KPI AI">
                <Button 
                    type="primary" shape="circle" icon={<RobotOutlined style={{ fontSize: 24 }} />} size="large" onClick={() => setVisible(true)} 
                    style={{ position: 'fixed', bottom: 30, right: 30, zIndex: 1000, width: 60, height: 60, boxShadow: '0 4px 12px rgba(22, 119, 255, 0.35)', border: 'none' }} 
                />
            </Tooltip>

            <Drawer
                title={<span style={{ fontWeight: 600, fontSize: 16 }}><RobotOutlined style={{ marginRight: 8, color: '#1677ff' }} />Trợ lý KPI</span>}
                placement="right" onClose={() => setVisible(false)} open={visible} width={500}
                extra={<Tooltip title="Xóa đoạn chat"><Button type="text" shape="circle" icon={<DeleteOutlined style={{color: '#ff4d4f'}} />} onClick={handleClearChat} /></Tooltip>}
                styles={{ 
                    header: { padding: '16px 24px', borderBottom: '1px solid #f0f0f0' },
                    body: { padding: 0, display: 'flex', flexDirection: 'column', backgroundColor: '#f0f2f5' }, 
                    footer: { padding: '12px 16px', background: '#fff', borderTop: '1px solid #f0f0f0' }
                }}
                footer={
                    <div style={{ display: 'flex', gap: 10, alignItems: 'flex-end' }}>
                        <Input.TextArea 
                            ref={inputRef} placeholder="Nhập câu hỏi của bạn..." autoSize={{ minRows: 1, maxRows: 4 }} 
                            value={inputValue} onChange={e => setInputValue(e.target.value)} 
                            onKeyDown={e => { if (e.key === 'Enter' && !e.shiftKey) { e.preventDefault(); handleSend(); }}} 
                            style={{ borderRadius: '8px', padding: '8px 12px', resize: 'none' }}
                        />
                        <Button type="primary" shape="circle" size="large" icon={<SendOutlined />} onClick={handleSend} loading={status === ReduxStatus.LOADING} disabled={!inputValue.trim()} />
                    </div>
                }
            >
                <div ref={scrollRef} style={{ flex: 1, padding: '20px', overflowY: 'auto' }}>
                    {history.length === 0 ? (
                        <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', justifyContent: 'center', height: '100%', opacity: 0.6 }}>
                            <Avatar size={80} icon={<RobotOutlined />} style={{ backgroundColor: '#e6f4ff', color: '#1677ff', marginBottom: 16 }} />
                            <h3 style={{ fontSize: 18, fontWeight: 600, color: '#1f1f1f', marginBottom: 8 }}>Xin chào!</h3>
                            <p style={{ color: '#595959', margin: 0 }}>Tôi có thể giúp gì về số liệu KPI của bạn?</p>
                        </div>
                    ) : (
                        <List dataSource={history} split={false} renderItem={(item, index) => {
                            const isUser = item.role === 'user';
                            return (
                                <div key={index} style={{ 
                                    display: 'flex', 
                                    flexDirection: isUser ? 'row-reverse' : 'row',
                                    marginBottom: 20, 
                                    gap: 12 
                                }}>
                                    <Avatar 
                                        size={36} 
                                        icon={isUser ? <UserOutlined /> : <RobotOutlined />} 
                                        style={{ 
                                            backgroundColor: isUser ? '#1677ff' : '#fff', 
                                            color: isUser ? '#fff' : '#1677ff',
                                            boxShadow: isUser ? 'none' : '0 2px 6px rgba(0,0,0,0.08)',
                                            flexShrink: 0 
                                        }} 
                                    />
                                
                                    <div style={{ 
                                        backgroundColor: isUser ? '#1677ff' : '#fff', 
                                        color: isUser ? '#fff' : '#1f1f1f',
                                        padding: '12px 16px',
                                        borderRadius: isUser ? '16px 16px 4px 16px' : '16px 16px 16px 4px', 
                                        maxWidth: '85%',
                                        boxShadow: '0 2px 5px rgba(0,0,0,0.05)',
                                        fontSize: '14px',
                                        position: 'relative',
                                        wordBreak: 'break-word'
                                    }}>
                                        {isUser ? item.content : (
                                            <>
                                                {renderMessageContent(item.content)}
                                                <div style={{ height: 16 }} /> 
                                                <Tooltip title={copiedIndex === index ? "Đã sao chép" : "Sao chép"}>
                                                    <Button 
                                                        type="text" size="small" 
                                                        icon={copiedIndex === index ? <CheckOutlined style={{ color: '#52c41a' }} /> : <CopyOutlined style={{ color: '#8c8c8c' }} />} 
                                                        onClick={() => handleCopy(item.content, index)} 
                                                        style={{ position: 'absolute', bottom: 4, right: 4, opacity: 0.8 }} 
                                                    />
                                                </Tooltip>
                                            </>
                                        )}
                                    </div>
                                </div>
                            );
                        }} />
                    )}
                
                    {status === ReduxStatus.LOADING && (
                        <div style={{ display: 'flex', gap: 12, marginLeft: 4 }}>
                           <Avatar size={36} icon={<RobotOutlined style={{ color: '#1677ff' }} />} style={{ backgroundColor: '#fff', boxShadow: '0 2px 6px rgba(0,0,0,0.08)' }} />
                           <div style={{ backgroundColor: '#fff', padding: '12px 16px', borderRadius: '16px 16px 16px 4px', boxShadow: '0 2px 5px rgba(0,0,0,0.05)' }}>
                                <div style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
                                    <Spin size="small" /> <span style={{ color: '#8c8c8c', fontSize: 13 }}>AI đang phân tích...</span>
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