'use client';

import { Button, Checkbox, Form, FormProps, Input, Spin } from 'antd';
import { colors } from '@styles/colors';
import { toast } from 'react-toastify';
import { GraduationCap, MessageSquarePlus, LogOut, Send, Menu, X } from 'lucide-react';
import React, { useState, useRef, useEffect } from 'react';
import ReactMarkdown from 'react-markdown';
import remarkGfm from 'remark-gfm';

import { useAppDispatch, useAppSelector } from '@redux/hooks';
import { sinhVienLogin, sinhVienLogout } from '@redux/feature/student/studentThunk';
import { ISinhVienLogin } from '@models/auth/sinhvien.model';
import { processApiMsgError } from '@utils/index';
import { ChatbotService, IChatSession, IChatMessage } from '@services/chatbot.service';

type LoginFormData = {
  mssv: string;
  password: string;
  remember: boolean;
};

interface ChatMessageUI {
  id: string;
  role: 'user' | 'assistant';
  content: string;
  timestamp: number;
}

function ChatbotPage() {
  const dispatch = useAppDispatch();
  const [loginLoading, setLoginLoading] = useState(false);
  const [studentForm] = Form.useForm();

  // Chat state
  const [sessions, setSessions] = useState<IChatSession[]>([]);
  const [activeSessionId, setActiveSessionId] = useState<string | null>(null);
  const [activeMessages, setActiveMessages] = useState<ChatMessageUI[]>([]);
  const [inputMessage, setInputMessage] = useState('');
  const [isSending, setIsSending] = useState(false);
  const [isLoadingSessions, setIsLoadingSessions] = useState(false);
  const [isLoadingMessages, setIsLoadingMessages] = useState(false);
  const [sidebarOpen, setSidebarOpen] = useState(true);
  const messagesEndRef = useRef<HTMLDivElement>(null);

  // Get student state from Redux
  const { isAuthenticated, user, $login } = useAppSelector((state) => state.studentState);

  // Fetch sessions from API
  const fetchSessions = async () => {
    setIsLoadingSessions(true);
    try {
      const data = await ChatbotService.getSessions();
      setSessions(data.sessions || []);
    } catch (error) {
      console.error('Failed to fetch sessions:', error);
    } finally {
      setIsLoadingSessions(false);
    }
  };

  // Fetch session history from API
  const fetchSessionHistory = async (sessionId: string) => {
    setIsLoadingMessages(true);
    try {
      const data = await ChatbotService.getSessionHistory(sessionId);
      const messages: ChatMessageUI[] = (data.history || []).map((msg: IChatMessage, index: number) => ({
        id: `${sessionId}-${index}`,
        role: msg.role,
        content: msg.content,
        timestamp: Date.now()
      }));
      setActiveMessages(messages);
    } catch (error) {
      console.error('Failed to fetch session history:', error);
      setActiveMessages([]);
    } finally {
      setIsLoadingMessages(false);
    }
  };

  // Load sessions on mount
  useEffect(() => {
    if (isAuthenticated) {
      fetchSessions();
    }
  }, [isAuthenticated]);

  // Load messages when active session changes
  useEffect(() => {
    if (activeSessionId) {
      fetchSessionHistory(activeSessionId);
    } else {
      setActiveMessages([]);
    }
  }, [activeSessionId]);

  // Scroll to bottom when messages change
  useEffect(() => {
    messagesEndRef.current?.scrollIntoView({ behavior: 'smooth' });
  }, [activeMessages]);

  // Handle student login
  const onStudentLogin: FormProps<LoginFormData>['onFinish'] = async (values) => {
    const body: ISinhVienLogin = {
      mssv: values.mssv!,
      password: values.password!,
      remember: values.remember || false
    };

    setLoginLoading(true);
    try {
      const data: any = await dispatch(sinhVienLogin(body)).unwrap();

      if (data.status == 1) {
        toast.success('Đăng nhập thành công');
      } else {
        toast.error(data.message || 'Đăng nhập thất bại');
      }
    } catch (err) {
      processApiMsgError(err, 'Đăng nhập thất bại vui lòng thử lại');
    } finally {
      setLoginLoading(false);
    }
  };

  // Handle logout
  const handleLogout = async () => {
    try {
      await dispatch(sinhVienLogout()).unwrap();
      toast.success('Đăng xuất thành công');
    } catch (err) {
      // Still clear local state even if API fails
      toast.info('Đã đăng xuất');
    }
  };

  // Create new conversation
  const createNewChat = () => {
    setActiveSessionId(null);
    setActiveMessages([]);
  };

  // Send message
  const sendMessage = async () => {
    if (!inputMessage.trim() || isSending) return;

    let sessionId = activeSessionId;

    // Create new session ID if none exists
    if (!sessionId) {
      sessionId = Date.now().toString();
      setActiveSessionId(sessionId);
    }

    const userMessage: ChatMessageUI = {
      id: Date.now().toString(),
      role: 'user',
      content: inputMessage,
      timestamp: Date.now()
    };

    // Add user message to UI immediately
    setActiveMessages((prev) => [...prev, userMessage]);

    const messageToSend = inputMessage;
    setInputMessage('');
    setIsSending(true);

    try {
      // Build conversation history for API
      const conversationHistory: IChatMessage[] = activeMessages.map((msg) => ({
        role: msg.role,
        content: msg.content
      }));

      // Call chat API using service
      const data = await ChatbotService.sendMessage({
        message: messageToSend,
        session_id: sessionId,
        conversation_history: conversationHistory
      });

      const botMessage: ChatMessageUI = {
        id: (Date.now() + 1).toString(),
        role: 'assistant',
        content: data.response || 'Xin loi, toi khong the tra loi luc nay.',
        timestamp: Date.now()
      };

      setActiveMessages((prev) => [...prev, botMessage]);

      // Refresh sessions list to update sidebar
      fetchSessions();
    } catch (error) {
      console.error('Chat API error:', error);
      toast.error('Khong the ket noi den chatbot. Vui long thu lai.');

      // Add error message
      const errorMessage: ChatMessageUI = {
        id: (Date.now() + 1).toString(),
        role: 'assistant',
        content: 'Xin loi, da co loi xay ra. Vui long thu lai sau.',
        timestamp: Date.now()
      };

      setActiveMessages((prev) => [...prev, errorMessage]);
    } finally {
      setIsSending(false);
    }
  };

  // Delete conversation
  const deleteConversation = async (sessionId: string, e: React.MouseEvent) => {
    e.stopPropagation();
    try {
      await ChatbotService.deleteSession(sessionId);
      setSessions((prev) => prev.filter((s) => s.session_id !== sessionId));
      if (activeSessionId === sessionId) {
        const remaining = sessions.filter((s) => s.session_id !== sessionId);
        setActiveSessionId(remaining.length > 0 ? remaining[0].session_id : null);
      }
    } catch (error) {
      console.error('Failed to delete session:', error);
      // Still remove from UI even if API fails
      setSessions((prev) => prev.filter((s) => s.session_id !== sessionId));
    }
  };

  // Login form component
  const renderLoginForm = () => (
    <div
      className="relative flex min-h-screen flex-col items-center justify-center overflow-hidden"
      style={{ backgroundColor: colors.white }}
    >
      {/* Background image with 10% opacity */}
      <img
        src="/images/school-869061_1280.jpg"
        alt="library background"
        className="absolute inset-0 z-0 h-full w-full object-cover opacity-10"
        style={{ pointerEvents: 'none' }}
      />
      {/* Overlay content */}
      <div
        className="relative z-10 flex w-full max-w-md flex-col items-center rounded-2xl p-8 shadow-lg"
        style={{ backgroundColor: colors.white }}
      >
        <GraduationCap className="mb-6 h-12 w-12" style={{ color: colors.primary }} />
        <h2 className="mb-2 text-2xl font-bold" style={{ color: colors.primaryNavy }}>
          Đăng nhập Chatbot
        </h2>
        <p className="mb-6 text-center" style={{ color: colors.gray }}>
          Đăng nhập để sử dụng trợ lý ảo
        </p>

        {loginLoading || $login.loading ? (
          <div className="p-24">
            <Spin size="large" />
          </div>
        ) : (
          <Form
            form={studentForm}
            name="student-login-form"
            layout="vertical"
            initialValues={{ remember: true }}
            onFinish={onStudentLogin}
            autoComplete="off"
            className="flex w-full flex-col"
          >
            <Form.Item<LoginFormData>
              label={<span className="text-sm font-medium text-gray-700">Mã số sinh viên</span>}
              name="mssv"
              rules={[{ required: true, message: 'Vui lòng nhập mã số sinh viên!' }]}
            >
              <Input
                className="w-full rounded-lg border px-4 py-2 focus:outline-none focus:ring-2"
                style={{ borderColor: colors.gray, boxShadow: `0 0 0 2px ${colors.primaryLight}33` }}
                placeholder="Nhập mã số sinh viên"
              />
            </Form.Item>

            <Form.Item<LoginFormData>
              label={<span className="text-sm font-medium text-gray-700">Mật khẩu</span>}
              name="password"
              rules={[{ required: true, message: 'Vui lòng nhập mật khẩu!' }]}
            >
              <Input.Password
                className="w-full rounded-lg border px-4 py-2 focus:outline-none focus:ring-2"
                style={{ borderColor: colors.gray, boxShadow: `0 0 0 2px ${colors.primaryLight}33` }}
                placeholder="Nhập mật khẩu"
              />
            </Form.Item>

            <div className="mb-4 flex items-center justify-between">
              <Form.Item<LoginFormData> name="remember" valuePropName="checked" label={null} className="mb-0">
                <Checkbox>Ghi nhớ đăng nhập</Checkbox>
              </Form.Item>
            </div>

            <Form.Item label={null}>
              <Button
                type="primary"
                htmlType="submit"
                loading={loginLoading}
                className="w-full rounded-lg py-2 font-semibold text-white transition"
                style={{ backgroundColor: colors.primary, borderColor: colors.primaryNavy }}
              >
                Đăng nhập
              </Button>
            </Form.Item>
          </Form>
        )}

        <div className="mt-6 text-center text-sm" style={{ color: colors.gray }}>
          <span>Bạn cần hỗ trợ? Liên hệ ngay </span>
          <a href="#" style={{ color: colors.primaryLight }} className="hover:underline">
            19001009
          </a>
        </div>
      </div>
      <footer className="relative z-10 mt-8 text-center text-xs" style={{ color: colors.gray }}>
        &copy; {new Date().getFullYear()} D.University. All rights reserved.
      </footer>
    </div>
  );

  // Chatbot interface
  const renderChatbot = () => (
    <div className="flex h-screen" style={{ backgroundColor: colors.grayLight }}>
      {/* Mobile menu button */}
      <button
        className="fixed left-4 top-4 z-50 rounded-lg p-2 md:hidden"
        style={{ backgroundColor: colors.primary }}
        onClick={() => setSidebarOpen(!sidebarOpen)}
      >
        {sidebarOpen ? <X size={24} color="white" /> : <Menu size={24} color="white" />}
      </button>

      {/* Sidebar */}
      <div
        className={`fixed z-40 h-full transition-transform duration-300 md:relative ${
          sidebarOpen ? 'translate-x-0' : '-translate-x-full md:translate-x-0'
        }`}
        style={{
          width: 280,
          backgroundColor: colors.white,
          borderRight: `1px solid ${colors.grayLight}`
        }}
      >
        <div className="flex h-full flex-col">
          {/* New chat button */}
          <div className="p-4">
            <Button
              type="primary"
              icon={<MessageSquarePlus size={18} />}
              onClick={createNewChat}
              className="flex w-full items-center justify-center gap-2"
              style={{ backgroundColor: colors.primary, borderColor: colors.primaryNavy }}
            >
              Tạo mới Chat
            </Button>
          </div>

          {/* Chat history */}
          <div className="flex-1 overflow-y-auto px-2">
            <p className="px-2 py-2 text-xs font-semibold uppercase" style={{ color: colors.gray }}>
              Lich su chat
            </p>
            {isLoadingSessions ? (
              <div className="flex justify-center py-4">
                <Spin size="small" />
              </div>
            ) : sessions.length === 0 ? (
              <p className="px-2 py-4 text-center text-sm" style={{ color: colors.gray }}>
                Chưa có cuộc trò chuyện nào
              </p>
            ) : (
              sessions.map((session: IChatSession) => (
                <div
                  key={session.session_id}
                  onClick={() => {
                    setActiveSessionId(session.session_id);
                    setSidebarOpen(false);
                  }}
                  className={`group mb-1 flex cursor-pointer items-center justify-between rounded-lg px-3 py-2 transition-colors ${
                    activeSessionId === session.session_id ? 'bg-blue-50' : 'hover:bg-gray-100'
                  }`}
                  style={{
                    backgroundColor: activeSessionId === session.session_id ? `${colors.primaryLight}15` : undefined
                  }}
                >
                  <span
                    className="truncate text-sm"
                    style={{ color: activeSessionId === session.session_id ? colors.primary : colors.black }}
                  >
                    {session.title}
                  </span>
                  <button
                    onClick={(e) => deleteConversation(session.session_id, e)}
                    className="rounded p-1 opacity-0 hover:bg-red-100 group-hover:opacity-100"
                  >
                    <X size={14} style={{ color: colors.red }} />
                  </button>
                </div>
              ))
            )}
          </div>

          {/* User info and logout */}
          <div className="border-t p-4" style={{ borderColor: colors.grayLight }}>
            {user && (
              <div className="mb-3 text-sm" style={{ color: colors.black }}>
                <p className="font-medium">
                  {user.hoDem && user.ten ? `${user.hoDem} ${user.ten}` : user.ten || 'Sinh vien'}
                </p>
                <p style={{ color: colors.gray }}>{user.mssv}</p>
              </div>
            )}
            <Button
              danger
              icon={<LogOut size={16} />}
              onClick={handleLogout}
              className="flex w-full items-center justify-center gap-2"
            >
              Đăng xuất
            </Button>
          </div>
        </div>
      </div>

      {/* Chat area */}
      <div className="flex h-full flex-1 flex-col md:ml-0">
        {/* Chat header */}
        <div
          className="flex items-center border-b px-6 py-4"
          style={{ backgroundColor: colors.white, borderColor: colors.grayLight }}
        >
          <h2 className="pl-12 text-lg font-semibold md:pl-0" style={{ color: colors.primaryNavy }}>
            {sessions.find((s) => s.session_id === activeSessionId)?.title || 'Chat mới'}
          </h2>
        </div>

        {/* Messages */}
        <div className="flex-1 overflow-y-auto p-6">
          {isLoadingMessages ? (
            <div className="flex h-full items-center justify-center">
              <Spin size="large" />
            </div>
          ) : activeMessages.length === 0 ? (
            <div className="flex h-full flex-col items-center justify-center text-center">
              <GraduationCap size={64} style={{ color: colors.primary }} className="mb-4 opacity-50" />
              <h3 className="mb-2 text-xl font-semibold" style={{ color: colors.primaryNavy }}>
                Chào mừng đến với Chatbot
              </h3>
              <p style={{ color: colors.gray }}>Hãy đặt câu hỏi để bắt đầu cuộc trò chuyện</p>
            </div>
          ) : (
            activeMessages.map((msg: ChatMessageUI) => (
              <div key={msg.id} className={`mb-4 flex ${msg.role === 'user' ? 'justify-end' : 'justify-start'}`}>
                <div
                  className={`max-w-[70%] rounded-2xl px-4 py-3 ${msg.role === 'assistant' ? 'markdown-content' : ''}`}
                  style={{
                    backgroundColor: msg.role === 'user' ? colors.primary : colors.white,
                    color: msg.role === 'user' ? colors.white : colors.black,
                    boxShadow: msg.role === 'assistant' ? '0 1px 3px rgba(0,0,0,0.1)' : undefined
                  }}
                >
                  {msg.role === 'assistant' ? (
                    <ReactMarkdown
                      remarkPlugins={[remarkGfm]}
                      components={{
                        h2: ({ children }) => (
                          <h2
                            style={{
                              fontSize: '1.25rem',
                              fontWeight: 'bold',
                              marginBottom: '0.75rem',
                              color: colors.primaryNavy
                            }}
                          >
                            {children}
                          </h2>
                        ),
                        h3: ({ children }) => (
                          <h3
                            style={{
                              fontSize: '1.1rem',
                              fontWeight: '600',
                              marginBottom: '0.5rem',
                              color: colors.primaryNavy
                            }}
                          >
                            {children}
                          </h3>
                        ),
                        p: ({ children }) => <p style={{ marginBottom: '0.5rem', lineHeight: '1.6' }}>{children}</p>,
                        strong: ({ children }) => (
                          <strong style={{ fontWeight: '600', color: colors.primary }}>{children}</strong>
                        ),
                        table: ({ children }) => (
                          <table
                            style={{
                              width: '100%',
                              borderCollapse: 'collapse',
                              marginTop: '0.75rem',
                              marginBottom: '0.75rem',
                              fontSize: '0.9rem'
                            }}
                          >
                            {children}
                          </table>
                        ),
                        thead: ({ children }) => (
                          <thead style={{ backgroundColor: colors.primaryLight + '20' }}>{children}</thead>
                        ),
                        th: ({ children }) => (
                          <th
                            style={{
                              padding: '0.5rem 0.75rem',
                              textAlign: 'left',
                              fontWeight: '600',
                              borderBottom: `2px solid ${colors.primary}`,
                              color: colors.primaryNavy
                            }}
                          >
                            {children}
                          </th>
                        ),
                        td: ({ children }) => (
                          <td style={{ padding: '0.5rem 0.75rem', borderBottom: `1px solid ${colors.grayLight}` }}>
                            {children}
                          </td>
                        ),
                        blockquote: ({ children }) => (
                          <blockquote
                            style={{
                              borderLeft: `4px solid ${colors.primary}`,
                              paddingLeft: '1rem',
                              marginTop: '0.75rem',
                              marginBottom: '0.75rem',
                              backgroundColor: colors.primaryLight + '10',
                              padding: '0.75rem 1rem',
                              borderRadius: '0 0.5rem 0.5rem 0',
                              fontStyle: 'italic'
                            }}
                          >
                            {children}
                          </blockquote>
                        ),
                        ul: ({ children }) => (
                          <ul style={{ paddingLeft: '1.25rem', marginBottom: '0.5rem' }}>{children}</ul>
                        ),
                        ol: ({ children }) => (
                          <ol style={{ paddingLeft: '1.25rem', marginBottom: '0.5rem' }}>{children}</ol>
                        ),
                        li: ({ children }) => <li style={{ marginBottom: '0.25rem' }}>{children}</li>,
                        code: ({ children }) => (
                          <code
                            style={{
                              backgroundColor: colors.grayLight,
                              padding: '0.125rem 0.375rem',
                              borderRadius: '0.25rem',
                              fontSize: '0.875rem',
                              fontFamily: 'monospace'
                            }}
                          >
                            {children}
                          </code>
                        )
                      }}
                    >
                      {msg.content}
                    </ReactMarkdown>
                  ) : (
                    msg.content
                  )}
                </div>
              </div>
            ))
          )}
          {isSending && (
            <div className="mb-4 flex justify-start">
              <div
                className="rounded-2xl px-4 py-3"
                style={{ backgroundColor: colors.white, boxShadow: '0 1px 3px rgba(0,0,0,0.1)' }}
              >
                <Spin size="small" />
              </div>
            </div>
          )}
          <div ref={messagesEndRef} />
        </div>

        {/* Input area */}
        <div className="border-t p-4" style={{ backgroundColor: colors.white, borderColor: colors.grayLight }}>
          <div className="mx-auto flex max-w-4xl gap-3">
            <Input
              value={inputMessage}
              onChange={(e) => setInputMessage(e.target.value)}
              onPressEnter={sendMessage}
              placeholder="Nhap tin nhan..."
              className="flex-1 rounded-xl px-4 py-2"
              style={{ borderColor: colors.gray }}
              disabled={isSending}
            />
            <Button
              type="primary"
              icon={<Send size={18} />}
              onClick={sendMessage}
              loading={isSending}
              className="rounded-xl px-6"
              style={{ backgroundColor: colors.primary, borderColor: colors.primaryNavy }}
            >
              Gửi
            </Button>
          </div>
        </div>
      </div>
    </div>
  );

  return isAuthenticated ? renderChatbot() : renderLoginForm();
}

export default ChatbotPage;
