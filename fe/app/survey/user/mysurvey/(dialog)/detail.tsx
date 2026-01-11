'use client';
import { useState, useEffect, useCallback, useRef } from 'react';
import { Modal, Button, Radio, Checkbox, Input, Form, Spin, Progress, message, Space } from 'antd';
import { SendOutlined, ClockCircleOutlined } from '@ant-design/icons';
import { toast } from 'react-toastify';
import { SurveyService } from '@services/survey.service';
import { IStartSurveyResponse, ISavedAnswer, ISurveyExam } from '@models/survey/survey.model';

const { TextArea } = Input;

interface SurveyDetailDialogProps {
  surveyId: number;
  isOpen: boolean;
  onClose: () => void;
}

const AUTO_SAVE_INTERVAL = 30000; // 30 seconds

const SurveyDetailDialog = ({ surveyId, isOpen, onClose }: SurveyDetailDialogProps) => {
  const [form] = Form.useForm();
  const [loading, setLoading] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [surveyData, setSurveyData] = useState<IStartSurveyResponse | null>(null);
  const [answers, setAnswers] = useState<Map<number, ISavedAnswer>>(new Map());
  const [lastSaveTime, setLastSaveTime] = useState<Date | null>(null);
  const autoSaveTimerRef = useRef<NodeJS.Timeout | null>(null);
  const [messageApi, contextHolder] = message.useMessage();

  // Load survey data when dialog opens
  useEffect(() => {
    if (isOpen && surveyId) {
      loadSurveyData();
    }
    return () => {
      // Clear auto-save timer when dialog closes
      if (autoSaveTimerRef.current) {
        clearInterval(autoSaveTimerRef.current);
      }
    };
  }, [isOpen, surveyId]);

  const handleClose = async () => {
    // Save draft before closing if there are answers
    if (surveyData && answers.size > 0) {
      try {
        const answersArray = Array.from(answers.values());
        await SurveyService.saveDraftSurvey({
          submissionId: surveyData.submissionId,
          answers: answersArray
        });
        messageApi.success('Đã lưu bản nháp', 1);
      } catch (error) {
        console.error('Save on close error:', error);
        // Don't block closing even if save fails
      }
    }
    onClose();
  };

  // Set up auto-save
  useEffect(() => {
    if (surveyData && isOpen) {
      // Clear existing timer
      if (autoSaveTimerRef.current) {
        clearInterval(autoSaveTimerRef.current);
      }

      // Set up new timer
      autoSaveTimerRef.current = setInterval(() => {
        handleAutoSave();
      }, AUTO_SAVE_INTERVAL);

      return () => {
        if (autoSaveTimerRef.current) {
          clearInterval(autoSaveTimerRef.current);
        }
      };
    }
  }, [surveyData, isOpen, answers]);

  const loadSurveyData = async () => {
    setLoading(true);
    try {
      const response = await SurveyService.startSurvey(surveyId);
      if (response.status === 1) {
        const data: IStartSurveyResponse = response.data;
        setSurveyData(data);

        // Load saved answers if any
        if (data.savedAnswers && data.savedAnswers.length > 0) {
          const savedAnswersMap = new Map<number, ISavedAnswer>();
          data.savedAnswers.forEach((answer) => {
            savedAnswersMap.set(answer.questionId, answer);
          });
          setAnswers(savedAnswersMap);

          // Set form values
          const formValues: any = {};
          data.savedAnswers.forEach((answer) => {
            if (answer.selectedAnswerId) {
              // For single choice (type 1)
              formValues[`question_${answer.questionId}`] = answer.selectedAnswerId;
            } else if (answer.selectedAnswerIds && Array.isArray(answer.selectedAnswerIds) && answer.selectedAnswerIds.length > 0) {
              // For multiple choice (type 2) - ensure it's a valid array
              formValues[`question_${answer.questionId}`] = answer.selectedAnswerIds;
            } else if (answer.textResponse) {
              // For essay (type 3)
              formValues[`question_${answer.questionId}`] = answer.textResponse;
            }
          });
          form.setFieldsValue(formValues);
        }
      } else {
        toast.error(response.message || 'Không thể tải khảo sát');
        onClose();
      }
    } catch (error) {
      console.error('Error loading survey:', error);
      toast.error('Không thể tải khảo sát');
      onClose();
    } finally {
      setLoading(false);
    }
  };

  const handleAnswerChange = (questionId: number, value: any, loaiCauHoi: number) => {
    const newAnswer: ISavedAnswer = {
      questionId,
      selectedAnswerId: loaiCauHoi === 1 ? value : undefined, // Single choice
      selectedAnswerIds: loaiCauHoi === 2 ? value : undefined, // Multiple choice
      textResponse: loaiCauHoi === 3 ? value : undefined // Essay
    };

    setAnswers((prev) => {
      const newMap = new Map(prev);
      newMap.set(questionId, newAnswer);
      return newMap;
    });
  };

  const handleAutoSave = async () => {
    if (!surveyData || answers.size === 0) return;

    try {
      const answersArray = Array.from(answers.values());
      await SurveyService.saveDraftSurvey({
        submissionId: surveyData.submissionId,
        answers: answersArray
      });
      setLastSaveTime(new Date());
      messageApi.success('Đã tự động lưu bản nháp', 2);
    } catch (error) {
      console.error('Auto-save error:', error);
      // Don't show error toast for auto-save failures to avoid annoying users
    }
  };



  const handleSubmit = async () => {
    if (!surveyData) return;

    // Validate all questions are answered
    const unansweredQuestions = surveyData.questions.filter(
      (q) => !answers.has(q.id)
    );

    if (unansweredQuestions.length > 0) {
      toast.warning(`Vui lòng trả lời tất cả các câu hỏi (còn ${unansweredQuestions.length} câu chưa trả lời)`);
      return;
    }

    if (!confirm('Bạn có chắc chắn muốn nộp bài khảo sát? Sau khi nộp bạn không thể chỉnh sửa.')) {
      return;
    }

    setSubmitting(true);
    try {
      const answersArray = Array.from(answers.values());
      const response = await SurveyService.submitSurvey({
        submissionId: surveyData.submissionId,
        answers: answersArray
      });

      if (response.status === 1) {
        toast.success('Nộp bài khảo sát thành công!');
        onClose();
      } else {
        toast.error(response.message || 'Nộp bài thất bại');
      }
    } catch (error) {
      console.error('Submit error:', error);
      toast.error('Nộp bài thất bại');
    } finally {
      setSubmitting(false);
    }
  };

  const getProgress = () => {
    if (!surveyData) return 0;
    return Math.round((answers.size / surveyData.questions.length) * 100);
  };

  const renderQuestion = (question: ISurveyExam, index: number) => {
    const isSingleChoice = question.loaiCauHoi === 1; // Radio
    const isMultipleChoice = question.loaiCauHoi === 2; // Checkbox
    const isEssay = question.loaiCauHoi === 3; // TextArea

    return (
      <div key={question.id} className="mb-6 p-4 border rounded-lg bg-white">
        <div className="mb-3">
          <span className="font-semibold text-lg">
            Câu {index + 1}: {question.noiDung}
          </span>
          <span className="text-red-500 ml-1">*</span>
          <span className="text-xs text-gray-500 ml-2">
            ({isSingleChoice ? 'Chọn 1 đáp án' : isMultipleChoice ? 'Chọn nhiều đáp án' : 'Tự luận'})
          </span>
        </div>

        {/* Single Choice - Radio */}
        {isSingleChoice && (
          <Form.Item
            name={`question_${question.id}`}
            rules={[{ required: true, message: 'Vui lòng chọn câu trả lời' }]}
          >
            <Radio.Group
              onChange={(e) => handleAnswerChange(question.id, e.target.value, question.loaiCauHoi)}
              className="w-full"
            >
              <Space direction="vertical" className="w-full">
                {question.answers.map((answer) => (
                  <Radio key={answer.id} value={answer.id} className="w-full p-2 hover:bg-gray-50 rounded">
                    {answer.noiDung}
                  </Radio>
                ))}
              </Space>
            </Radio.Group>
          </Form.Item>
        )}

        {/* Multiple Choice - Checkbox */}
        {isMultipleChoice && (
          <Form.Item
            name={`question_${question.id}`}
            rules={[{ required: true, message: 'Vui lòng chọn ít nhất 1 đáp án' }]}
          >
            <Checkbox.Group
              onChange={(values) => handleAnswerChange(question.id, values, question.loaiCauHoi)}
              className="w-full"
            >
              <Space direction="vertical" className="w-full">
                {question.answers.map((answer) => (
                  <Checkbox key={answer.id} value={answer.id} className="w-full p-2 hover:bg-gray-50 rounded">
                    {answer.noiDung}
                  </Checkbox>
                ))}
              </Space>
            </Checkbox.Group>
          </Form.Item>
        )}

        {/* Essay - TextArea */}
        {isEssay && (
          <Form.Item
            name={`question_${question.id}`}
            rules={[{ required: true, message: 'Vui lòng nhập câu trả lời' }]}
          >
            <TextArea
              rows={4}
              placeholder="Nhập câu trả lời của bạn..."
              onChange={(e) => handleAnswerChange(question.id, e.target.value, question.loaiCauHoi)}
            />
          </Form.Item>
        )}
      </div>
    );
  };

  return (
    <>
      {contextHolder}
      <Modal
        title={
          <div>
            <div className="text-xl font-bold">{surveyData?.tenKhaoSat || 'Khảo sát'}</div>
            {surveyData && (
              <div className="text-sm text-gray-500 mt-1">
                Tổng số câu hỏi: {surveyData.questions.length}
              </div>
            )}
          </div>
        }
        open={isOpen}
        onCancel={handleClose}
        width={800}
        footer={null}
        maskClosable={false}
      >
        {loading ? (
          <div className="flex justify-center items-center py-20">
            <Spin size="large" tip="Đang tải khảo sát..." />
          </div>
        ) : surveyData ? (
          <div>
            {/* Progress Bar */}
            <div className="mb-6 p-4 bg-gray-50 rounded-lg">
              <div className="flex items-center justify-between mb-2">
                <span className="text-sm font-medium">Tiến độ hoàn thành</span>
                <span className="text-sm text-gray-600">
                  {answers.size}/{surveyData.questions.length} câu
                </span>
              </div>
              <Progress percent={getProgress()} status="active" />
              {lastSaveTime && (
                <div className="text-xs text-gray-500 mt-2 flex items-center gap-1">
                  <ClockCircleOutlined />
                  Lưu lần cuối: {lastSaveTime.toLocaleTimeString('vi-VN')}
                </div>
              )}
            </div>

            {/* Questions */}
            <Form form={form} layout="vertical">
              <div className="max-h-[500px] overflow-y-auto pr-2">
                {surveyData.questions.map((question, index) => renderQuestion(question, index))}
              </div>
            </Form>

            {/* Actions */}
            <div className="flex justify-end items-center mt-6 pt-4 border-t">
              <Space>
                <Button onClick={handleClose}>Đóng</Button>
                <Button
                  type="primary"
                  icon={<SendOutlined />}
                  onClick={handleSubmit}
                  loading={submitting}
                  disabled={answers.size !== surveyData.questions.length}
                >
                  Nộp bài
                </Button>
              </Space>
            </div>
          </div>
        ) : null}
      </Modal>
    </>
  );
};

export default SurveyDetailDialog;
