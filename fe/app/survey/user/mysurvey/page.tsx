'use client';
import { useState, useEffect } from 'react';
import { Button, Card, Empty, Tag, Spin, Row, Col, Input, Select } from 'antd';
import { SearchOutlined, PlayCircleOutlined, CheckCircleOutlined, ClockCircleOutlined } from '@ant-design/icons';
import { toast } from 'react-toastify';
import { SurveyService } from '@services/survey.service';
import { IMySurveyItem, IQueryMySurvey } from '@models/survey/survey.model';
import { formatDateTimeView } from '@utils/index';
import { surveyStatusConst } from '@/constants/core/survey/surveyStatus.const';
import SurveyDetailDialog from './(dialog)/detail';

const { Search } = Input;

const Page = () => {
  const [surveys, setSurveys] = useState<IMySurveyItem[]>([]);
  const [loading, setLoading] = useState(false);
  const [selectedSurveyId, setSelectedSurveyId] = useState<number | null>(null);
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [query, setQuery] = useState<IQueryMySurvey>({
    PageIndex: 1,
    PageSize: 20,
    Keyword: ''
  });

  const loadMySurveys = async () => {
    setLoading(true);
    try {
      const response = await SurveyService.getMySurveys(query);
      if (response.status === 1) {
        setSurveys(response.data?.items || []);
      } else {
        toast.error(response.message || 'Không thể tải danh sách khảo sát');
      }
    } catch (error) {
      console.error('Error loading surveys:', error);
      toast.error('Không thể tải danh sách khảo sát');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadMySurveys();
  }, [query]);

  const handleStartSurvey = (surveyId: number) => {
    setSelectedSurveyId(surveyId);
    setIsDialogOpen(true);
  };

  const handleCloseDialog = () => {
    setIsDialogOpen(false);
    setSelectedSurveyId(null);
    loadMySurveys();
  };

  const isSurveyNotStarted = (survey: IMySurveyItem) => {
    const now = new Date();
    const startTime = new Date(survey.thoiGianBatDau);
    return now < startTime;
  };

  const isSurveyEnded = (survey: IMySurveyItem) => {
    const now = new Date();
    const endTime = new Date(survey.thoiGianKetThuc);
    return now > endTime;
  };

  const getStatusTag = (status: number, statusName: string) => {
    let color = 'default';
    let icon = null;

    if (status === surveyStatusConst.OPEN) {
      color = 'green';
      icon = <PlayCircleOutlined />;
    } else if (status === surveyStatusConst.CLOSE) {
      color = 'red';
      icon = <ClockCircleOutlined />;
    } else if (status === surveyStatusConst.COMPLETE) {
      color = 'blue';
      icon = <CheckCircleOutlined />;
    }

    return (
      <Tag color={color} icon={icon}>
        {statusName || 'N/A'}
      </Tag>
    );
  };

  return (
    <div className="p-6">
      <Card
        title={
          <div className="flex items-center justify-between">
            <span className="text-xl font-semibold">Khảo sát của tôi</span>
            <div className="flex gap-3">
              <Select
                placeholder="Lọc theo trạng thái"
                allowClear
                style={{ width: 200 }}
                onChange={(value) => {
                  const newQuery = { ...query, PageIndex: 1 };
                  if (value === undefined) {
                    delete newQuery.status;
                  } else {
                    newQuery.status = value;
                  }
                  setQuery(newQuery);
                }}
                options={[
                  { label: 'Đang mở', value: surveyStatusConst.OPEN },
                  { label: 'Đã đóng', value: surveyStatusConst.CLOSE },
                  { label: 'Hoàn thành', value: surveyStatusConst.COMPLETE }
                ]}
              />
              <Search
                placeholder="Tìm kiếm khảo sát..."
                allowClear
                style={{ width: 300 }}
                onSearch={(value) => setQuery({ ...query, Keyword: value, PageIndex: 1 })}
                prefix={<SearchOutlined />}
              />
            </div>
          </div>
        }
      >
        <Spin spinning={loading}>
          {surveys.length === 0 ? (
            <Empty description="Chưa có khảo sát nào" />
          ) : (
            <div className="space-y-2">
              {surveys.map((survey) => (
                <div
                  key={survey.id}
                  className="border border-gray-200 rounded-lg p-4 hover:shadow-md hover:border-blue-300 transition-all duration-200 bg-white"
                >
                  <div className="flex items-center justify-between gap-4">
                    {/* Survey Info */}
                    <div className="flex-1 min-w-0">
                      <div className="flex items-center gap-3 mb-2">
                        <h3 className="text-lg font-semibold text-gray-800 truncate">
                          {survey.tenKhaoSat}
                        </h3>
                        {getStatusTag(survey.status, survey.statusName)}
                      </div>
                      <div className="flex flex-wrap items-center gap-4 text-sm text-gray-600">
                        <span className="flex items-center gap-1">
                          <ClockCircleOutlined />
                          Bắt đầu: {formatDateTimeView(survey.thoiGianBatDau)}
                        </span>
                        <span className="flex items-center gap-1">
                          <ClockCircleOutlined />
                          Kết thúc: {formatDateTimeView(survey.thoiGianKetThuc)}
                        </span>
                        {survey.thoiGianNop && (
                          <span className="flex items-center gap-1 text-green-600 font-medium">
                            <CheckCircleOutlined />
                            Đã nộp: {formatDateTimeView(survey.thoiGianNop)}
                          </span>
                        )}
                      </div>
                    </div>

                    {/* Action Button */}
                    <div className="flex-shrink-0">
                      {survey.thoiGianNop ? (
                        <Button
                          icon={<CheckCircleOutlined />}
                          disabled
                          size="large"
                        >
                          Đã hoàn thành
                        </Button>
                      ) : isSurveyNotStarted(survey) ? (
                        <Button
                          icon={<ClockCircleOutlined />}
                          disabled
                          size="large"
                        >
                          Chưa bắt đầu
                        </Button>
                      ) : isSurveyEnded(survey) ? (
                        <Button
                          icon={<ClockCircleOutlined />}
                          disabled
                          size="large"
                        >
                          Đã kết thúc
                        </Button>
                      ) : survey.status === surveyStatusConst.OPEN ? (
                        <Button
                          type="primary"
                          onClick={() => handleStartSurvey(survey.id)}
                          size="large"
                          className="shadow-md hover:shadow-lg transition-shadow"
                        >
                          Bắt đầu
                        </Button>
                      ) : survey.status === surveyStatusConst.COMPLETE ? (
                        <Button
                          disabled
                          size="large"
                        >
                          Đã hoàn thành
                        </Button>
                      ) : (
                        <Button
                          disabled
                          size="large"
                        >
                          Đã đóng
                        </Button>
                      )}
                    </div>
                  </div>
                </div>
              ))}
            </div>
          )}
        </Spin>
      </Card>

      {selectedSurveyId && (
        <SurveyDetailDialog surveyId={selectedSurveyId} isOpen={isDialogOpen} onClose={handleCloseDialog} />
      )}
    </div>
  );
};

export default Page;
