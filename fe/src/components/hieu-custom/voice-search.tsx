'use client';
import { formatDateView } from '@utils/index';
import { useEffect, useRef, useState } from 'react';
import '@styles/voice-search.style.scss';

export default function VoiceSearch() {
  const [text, setText] = useState('');
  const [list, setList] = useState<any[]>([]);
  const [isListening, setIsListening] = useState(false);
  const [showMic, setShowMic] = useState(false); // trạng thái hiển thị mic

  const recognitionRef = useRef<any>(null);
  const hideTimeoutRef = useRef<NodeJS.Timeout | null>(null);

  const startVoice = () => {
    if (!recognitionRef.current || isListening) return;

    setShowMic(true); // hiển thị mic khi bắt đầu
    setIsListening(true);

    // xóa timeout ẩn mic trước đó nếu có
    if (hideTimeoutRef.current) {
      clearTimeout(hideTimeoutRef.current);
      hideTimeoutRef.current = null;
    }

    recognitionRef.current.start();

    // tạo timeout 10s để tự ẩn mic nếu ko nói gì
    hideTimeoutRef.current = setTimeout(() => {
      setShowMic(false);
      setIsListening(false);
    }, 10000);
  };

const sendToBE = async (query: string) => {
  try {
    const res = await fetch('http://127.0.0.1:8000/api/ai-search', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ query })
    });

    if (!res.ok) {
      throw new Error(`Lỗi server: ${res.status} ${res.statusText}`);
    }

    const data = await res.json();

    if (!data.data || !Array.isArray(data.data) || data.data.length === 0) {
      throw new Error('Không có dữ liệu trả về từ server');
    }

    setList(data.data); // dữ liệu bình thường
  } catch (error: any) {
    console.error('Lỗi khi gọi API:', error);
    setList([
      {
        id: 'error',
        code: '-',
        name: 'Lỗi',
        status: error.message || 'Có lỗi xảy ra',
        reception_date: '-',
        request_date: '-',
        total_money: '-',
      }
    ]);
  }
};


  useEffect(() => {
    if (typeof window === 'undefined') return;

    const SpeechRecognition =
      (window as any).SpeechRecognition ||
      (window as any).webkitSpeechRecognition;

    if (!SpeechRecognition) {
      alert('Trình duyệt không hỗ trợ Speech Recognition');
      return;
    }

    const recognition = new SpeechRecognition();
    recognition.lang = 'vi-VN';
    recognition.continuous = false;
    recognition.interimResults = false;

    recognition.onresult = (event: any) => {
      const voiceText = event.results[0][0].transcript;
      setText(voiceText);
      sendToBE(voiceText);

      // người dùng đã nói => hủy timeout ẩn mic
      if (hideTimeoutRef.current) {
        clearTimeout(hideTimeoutRef.current);
        hideTimeoutRef.current = null;
      }
    };

    recognition.onend = () => {
      setIsListening(false);

      // nếu chưa nói gì thì mic sẽ tự ẩn sau timeout, nếu đã nói thì giữ hiển thị
    };

    recognition.onerror = () => {
      setIsListening(false);
      setShowMic(false);
    };

    recognitionRef.current = recognition;

    // ---------- Phím tắt Ctrl+1 ----------
    const handleKeyDown = (e: KeyboardEvent) => {
      if (e.ctrlKey && e.key === '1') {
        startVoice();
      }
    };
    window.addEventListener('keydown', handleKeyDown);

    return () => {
      window.removeEventListener('keydown', handleKeyDown);
      if (hideTimeoutRef.current) clearTimeout(hideTimeoutRef.current);
    };
  }, []);

  return (
    <div className="voice-container">
      <div className="voice-result">
        {list.map((item) => (
          <div className="voice-item" key={item.id}>
            <div><b>Mã:</b> {item.code}</div>
            <div><b>Tên:</b> {item.name}</div>
            <div><b>Trạng thái:</b> {item.status}</div>
            <div><b>Ngày tiếp nhận:</b> {formatDateView(item.reception_date)}</div>
            <div><b>Ngày yêu cầu:</b> {formatDateView(item.request_date)}</div>
            <div><b>Tổng tiền:</b> {item.total_money}</div>
          </div>
        ))}
      </div>

      {/* Mic chỉ hiển thị khi showMic = true */}
      {showMic && (
        <button
          className={`siri-button ${isListening ? 'listening' : ''}`}
          onClick={startVoice} // vẫn có thể click nếu muốn
        >
          <div className="siri-glow"></div>
          <div className="siri-ring"></div>
          <div className="siri-waves">
            <div className="wave-bar"></div>
            <div className="wave-bar"></div>
            <div className="wave-bar"></div>
            <div className="wave-bar"></div>
            <div className="wave-bar"></div>
            <div className="wave-bar"></div>
            <div className="wave-bar"></div>
          </div>
        </button>
      )}

      <div className="voice-text">{isListening ? 'Đang lắng nghe ...' : text}</div>
    </div>
  );
}
