'use client';

import { useEffect, useRef, useState } from 'react';
import { toast } from 'react-toastify';
import { Button, Modal } from 'antd';
import { CloseOutlined, StopOutlined } from '@ant-design/icons';
import { MicroIcon } from '@components/custom-icon';

declare global {
  interface Window {
    webkitSpeechRecognition: any;
    SpeechRecognition: any;
  }
}

interface VoiceSearchModalProps {
  open: boolean;
  timeout?: number;
  onClose: (text?: string) => void;
}

const VoiceSearchModal = ({ open, timeout = 3000, onClose }: VoiceSearchModalProps) => {
  const [isListening, setIsListening] = useState(false);
  const [transcript, setTranscript] = useState('');

  const recognitionRef = useRef<any>(null);
  const silenceTimerRef = useRef<any>(null);

  // Init SpeechRecognition
  useEffect(() => {
    if (!open) return;

    setTranscript('');

    const SpeechRecognition = window.webkitSpeechRecognition || window.SpeechRecognition;

    if (!SpeechRecognition) {
      toast.error('Trình duyệt không hỗ trợ nhận diện giọng nói.');
      return;
    }

    const recognition = new SpeechRecognition();
    recognition.lang = 'vi-VN';
    recognition.continuous = true;
    recognition.interimResults = true;

    recognition.onresult = handleResult;
    recognition.onerror = handleError;
    recognition.onend = handleEnd;

    recognitionRef.current = recognition;

    return cleanupRecognition;
  }, [open]);

  const handleResult = (event: any) => {
    let text = '';
    for (let i = event.resultIndex; i < event.results.length; i++) {
      text += event.results[i][0].transcript;
    }

    setTranscript(text.trim());

    resetSilenceTimeout();
  };

  const handleError = (e: any) => {
    setIsListening(false);

    switch (e.error) {
      case 'not-allowed':
      case 'service-not-allowed':
        toast.error('Bạn đã từ chối quyền truy cập micro.');
        break;
      case 'no-speech':
        toast.warning('Không phát hiện giọng nói.');
        break;
      case 'audio-capture':
        toast.error('Không tìm thấy micro.');
        break;
      default:
        toast.error('Lỗi nhận diện giọng nói.');
    }
  };

  const handleEnd = () => {
    clearTimeout(silenceTimerRef.current);
    setIsListening(false);
  };

  const toggleRecording = async () => {
    if (!recognitionRef.current) return;

    if (!isListening) {
      const hasPermission = await checkMicroPermission();
      if (!hasPermission) return;

      startRecording();
    } else {
      stopRecording();
    }
  };

  const startRecording = () => {
    setTranscript('');
    setIsListening(true);

    try {
      recognitionRef.current.start();
    } catch {}

    resetSilenceTimeout();
  };

  const stopRecording = () => {
    recognitionRef.current?.stop();
    clearTimeout(silenceTimerRef.current);
    setIsListening(false);
    onClose(transcript);
  };

  const resetSilenceTimeout = () => {
    clearTimeout(silenceTimerRef.current);
    silenceTimerRef.current = setTimeout(stopRecording, timeout);
  };

  const checkMicroPermission = async () => {
    try {
      const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
      stream.getTracks().forEach((t) => t.stop());
      return true;
    } catch {
      toast.error('Website không có quyền truy cập micro.');
      return false;
    }
  };

  const cleanupRecognition = () => {
    try {
      recognitionRef.current?.stop();
    } catch {}
    recognitionRef.current = null;
    clearTimeout(silenceTimerRef.current);
  };

  return (
    <Modal centered open={open} title="Tìm kiếm bằng giọng nói" footer={null} onCancel={() => onClose()}>
      <div className="voice-popup-container text-center">
        <div className="content py-4">
          {transcript && <p className="text-result mb-2 text-base">{transcript}</p>}

          <p className="">{isListening ? 'Đang ghi âm ...' : 'Nhấn để bắt đầu ghi âm'}</p>
        </div>
        <div className="footer flex items-center justify-center gap-4">
          <Button
            className={`record-btn ${isListening ? 'active' : ''}`}
            icon={isListening ? <StopOutlined /> : <MicroIcon />}
            onClick={toggleRecording}
          >
            {isListening ? 'Dừng' : 'Ghi âm'}
          </Button>

          <Button className="btn-delete" icon={<CloseOutlined />} onClick={() => onClose()}>
            Đóng
          </Button>
        </div>
      </div>
    </Modal>
  );
};

export default VoiceSearchModal;
