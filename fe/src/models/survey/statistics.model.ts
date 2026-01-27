export interface ISurveyStatistics {
  surveyRequests: ISurveyRequestStats;
  surveys: ISurveyStats;
}

export interface ISurveyRequestStats {
  total: number;
  byStatus: IStatusCount[];
}

export interface ISurveyStats {
  total: number;
  byStatus: IStatusCount[];
}

export interface IStatusCount {
  status: number;
  statusName: string;
  count: number;
}
