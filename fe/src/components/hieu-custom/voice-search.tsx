'use client';

import { useEffect, useRef, useState } from 'react';
import '@styles/voice-search.style.scss';

interface VoiceSearchProps {
  onResult?: (data: any[]) => void; // callback trả dữ liệu về parent
}

export default function VoiceSearch({ onResult }: VoiceSearchProps) {
  const [text, setText] = useState('');
  const [isListening, setIsListening] = useState(false);

  const recognitionRef = useRef<any>(null);
  const hasStartedRef = useRef(false);

  const startVoice = () => {
    if (!recognitionRef.current || isListening) return;
    setIsListening(true);
    recognitionRef.current.start();
  };

  const sendToBE = async (query: string) => {
    try {
      const res = await fetch(`${process.env.NEXT_PUBLIC_API_URL_SEARCH_AI}`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ query })
      });

      if (!res.ok) throw new Error(`Lỗi server: ${res.status} ${res.statusText}`);
      const data = await res.json();

      if (!Array.isArray(data.data)) data.data = [];

      // Trả dữ liệu về parent
      onResult?.(data.data);
    } catch (error: any) {
      console.error('Lỗi khi gọi API:', error);
      onResult?.([
        {
          id: 'error',
          code: '-',
          name: 'Lỗi',
          status: error.message || 'Có lỗi xảy ra',
          reception_date: '-',
          request_date: '-',
          total_money: '-'
        }
      ]);
    } finally {
      setIsListening(false);
    }
  };

  useEffect(() => {
    if (typeof window === 'undefined') return;

    const SpeechRecognition = (window as any).SpeechRecognition || (window as any).webkitSpeechRecognition;

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

      // Hiển thị lời nói
      setText(voiceText);

      // Gửi lên backend
      sendToBE(voiceText);

      recognition.stop();
    };

    recognition.onend = () => setIsListening(false);
    recognition.onerror = () => setIsListening(false);

    recognitionRef.current = recognition;

    // tự động start khi mount lần đầu
    if (!hasStartedRef.current) {
      hasStartedRef.current = true;
      setTimeout(() => startVoice(), 100);
    }

    return () => recognition.stop();
  }, []);

  return (
    <div className="voice-fixed-wrapper">
      <div className="voice-container">
        <button className={`siri-button ${isListening ? 'listening' : ''}`} onClick={startVoice}>
          <div className="siri-glow"></div>
          <div className="siri-ring"></div>
          <div className="siri-waves">
            {[...Array(7)].map((_, i) => (
              <div className="wave-bar" key={i}></div>
            ))}
          </div>
        </button>

        <div className="voice-text">{isListening ? 'Đang lắng nghe ...' : text}</div>
      </div>
    </div>
  );
}
