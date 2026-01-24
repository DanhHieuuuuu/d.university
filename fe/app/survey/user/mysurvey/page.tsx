'use client';
import { useState, useEffect } from 'react';
import { Button, Card, Empty, Tag, Spin, Row, Col, Input, Select } from 'antd';
import { SearchOutlined, PlayCircleOutlined, CheckCircleOutlined, ClockCircleOutlined } from '@ant-design/icons';
import { toast } from 'react-toastify';
import { SurveyService } from '@services/survey.service';
import { IMySurveyItem, IQueryMySurvey } from '@models/survey/survey.model';
import { formatDateTimeView } from '@utils/index';
import { surveyStatusConst } from '@/app/(home)/survey/const/surveyStatus.const';
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
    // Reload surveys to update status
    loadMySurveys();
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
        {loading ? (
          <div className="flex items-center justify-center py-20">
            <Spin size="large" tip="Đang tải danh sách khảo sát..." />
          </div>
        ) : surveys.length === 0 ? (
          <Empty description="Không có khảo sát nào" image={Empty.PRESENTED_IMAGE_SIMPLE} />
        ) : (
          <Row gutter={[16, 16]}>
            {surveys.map((survey) => (
              <Col xs={24} sm={12} lg={8} xl={6} key={survey.id}>
                <Card
                  hoverable
                  className="h-full"
                  actions={[
                    <Button
                      key="start"
                      type="primary"
                      icon={<PlayCircleOutlined />}
                      onClick={() => handleStartSurvey(survey.id)}
                      disabled={survey.status !== surveyStatusConst.OPEN}
                      block
                    >
                      {survey.status === surveyStatusConst.OPEN ? 'Bắt đầu' : 'Không khả dụng'}
                    </Button>
                  ]}
                >
                  <div className="space-y-3">
                    <div>
                      <div className="mb-1 text-xs text-gray-500">Mã khảo sát</div>
                      <div className="font-semibold text-blue-600">{survey.maKhaoSat}</div>
                    </div>

                    <div>
                      <div className="mb-1 text-xs text-gray-500">Tên khảo sát</div>
                      <div className="line-clamp-2 font-medium" title={survey.tenKhaoSat}>
                        {survey.tenKhaoSat}
                      </div>
                    </div>

                    <div>
                      <div className="mb-1 text-xs text-gray-500">Thời gian</div>
                      <div className="text-sm">
                        <div>Bắt đầu: {formatDateTimeView(survey.thoiGianBatDau)}</div>
                        <div>Kết thúc: {formatDateTimeView(survey.thoiGianKetThuc)}</div>
                      </div>
                    </div>

                    <div>
                      <div className="mb-1 text-xs text-gray-500">Trạng thái</div>
                      {getStatusTag(survey.status, survey.statusName)}
                    </div>
                  </div>
                </Card>
              </Col>
            ))}
          </Row>
        )}
      </Card>

      {selectedSurveyId && (
        <SurveyDetailDialog surveyId={selectedSurveyId} isOpen={isDialogOpen} onClose={handleCloseDialog} />
      )}
    </div>
  );
};

export default Page;
